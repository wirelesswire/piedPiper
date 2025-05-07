using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using FuzzySharp;
using FuzzySharp.MembershipFunctions;
using FuzzySharp.MembershipFunctions.Functions;
using FuzzySharp.Operators;
using FuzzySharp.Operators.SNorm;
using FuzzySharp.Operators.TNorm;

namespace DrawingSamples
{
    public partial class Form1 : Form
    {
        private static float LeftFunc(float x)
        {
            return float.CreateTruncating(Math.Abs(Math.Cos(x)));
        }

        private static float RightFunc(float x)
        {
            return float.CreateTruncating(Math.Abs(Math.Sin(x)));
        }

        static IEnumerable<float> GenerateFloatRange(float start, float end, float step)
        {
            List<float> result = new List<float>();

            for (float value = start; value <= end; value += step) result.Add(value);

            return result;
        }

        public static FuzzySet<float> Combine(FuzzySet<float> a, FuzzySet<float> b, INormOperation<float> tNorm)
        {
            var combined = new Dictionary<float, float>();

            foreach (var kvp in a.Values)
            {
                if (b.Values.TryGetValue(kvp.Key, out var bValue))
                {
                    combined[kvp.Key] = tNorm.Calculate(kvp.Value, bValue);
                }
            }

            return new FuzzySet<float>(combined);
        }

        public static Bitmap DrawFuzzySet(FuzzySet<float> set, int width = 600, int height = 300)
        {
            var bmp = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            Pen axisPen = Pens.Black;
            Pen curvePen = Pens.Blue;
            Font labelFont = new Font("Arial", 8);
            Brush labelBrush = Brushes.Black;

            // Get value range for X axis
            float minX = set.Values.Keys.Min();
            float maxX = set.Values.Keys.Max();

            // Padding
            int padding = 40;

            // Draw axes
            g.DrawLine(axisPen, padding, height - padding, width - padding, height - padding); // X-axis
            g.DrawLine(axisPen, padding, padding, padding, height - padding); // Y-axis

            // Draw labels
            g.DrawString("1.0", labelFont, labelBrush, 5, padding - 5);
            g.DrawString("0.0", labelFont, labelBrush, 5, height - padding - 5);

            // Scale functions
            float ScaleX(float x) => padding + (x - minX) / (maxX - minX) * (width - 2 * padding);
            float ScaleY(float y) => height - padding - y * (height - 2 * padding);

            // Draw fuzzy set as connected lines
            var sorted = set.Values.OrderBy(kv => kv.Key).ToList();
            for (int i = 1; i < sorted.Count; i++)
            {
                var p1 = new PointF(ScaleX(sorted[i - 1].Key), ScaleY(sorted[i - 1].Value));
                var p2 = new PointF(ScaleX(sorted[i].Key), ScaleY(sorted[i].Value));
                g.DrawLine(curvePen, p1, p2);
            }

            return bmp;
        }

        private static Dictionary<string, IMembershipFunction<float>> membershipFunctions = new()
        {
            {"Bell", new BellMembershipFunction<float>(2f,1.5f,0f)},
            {"Binary", new BinaryMembershipFunction<float>(0f, false)},
            {"Class L", new ClassLMembershipFunction<float>(-5f, 5f)},
            {"Class S", new ClassSMembershipFunction<float>(-5f, 5f)},
            {"Gamma", new GammaMembershipFunction<float>(-5f, 5f)},
            {"Gauss", new GaussMembershipFunction<float>(0f, 5f, 1f)},
            {"Left-Right", new LeftRightMembershipFunction<float>(0f, 2f, 2f, LeftFunc, RightFunc)},
            {"Sigmoid", new SigmoidMembershipFunction<float>(0f, 2f)},
            {"Step", new StepMembershipFunction<float>([-0.5f, 0f, 1f, 2.5f])},
            {"Trapeze", new TrapezeMembershipFunction<float>(-5f, -2f, 2f, 5f)},
            {"Triangle", new TriangleMembershipFunction<float>(-2f, 0f, 2f)},
            {"Zadeh", new ZadehMembershipFunction<float>(0f)},
        };

        private static readonly Dictionary<string, INormOperation<float>> normOperations = new()
        {
            { "Algebraic Product", new NormOperationAlgebraicProd<float>() },
            { "Bounded Difference", new NormOperationBoundedDiff<float>() },
            { "Drastic Product", new NormOperationDrasticProd<float>() },
            { "Einstein Product", new NormOperationEinsteinProd<float>() },
            { "Hamacher Product", new NormOperationHamacherProd<float>() },
            { "Min", new NormOperationMin<float>() },
            { "Yager T-Norm", new NormOperationYagerOperatorComplement<float>(2f)},
            { "Algebraic Sum", new NormOperationAlgebraicSum<float>() },
            { "Bounded Sum", new NormOperationBoundedSum<float>() },
            { "Drastic Sum", new NormOperationDrasticSum<float>() },
            { "Einstein Sum", new NormOperationEinsteinSum<float>() },
            { "Hamacher Sum", new NormOperationHamacherSum<float>() },
            { "Max", new NormOperationMax<float>() },
            { "Yager S-Norm", new NormOperationYagerOperator<float>(2f) }
        };

        private static IEnumerable<float> dataRange = GenerateFloatRange(-10f, 10f, 0.1f);

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDrawDiagram_Click(object sender, EventArgs e)
        {
            var fuzzySetFirst = new FuzzySet<float>(dataRange
                .ToDictionary(
                    x => x,
                    x => membershipFunctions[comboBoxMembershipFunctionFirst.SelectedItem.ToString()].GetMembership(x)
                ));

            var fuzzySetSecond = new FuzzySet<float>(dataRange
                .ToDictionary(
                    x => x,
                    x => membershipFunctions[comboBoxMembershipFunctionSecond.SelectedItem.ToString()].GetMembership(x)
                ));

            var combine = Combine(fuzzySetFirst, fuzzySetSecond, normOperations[comboBoxOperator.SelectedItem.ToString()]);

            Bitmap fuzzyImage = DrawFuzzySet(combine);

            pictureBoxDiagram.Image = fuzzyImage;
            pictureBoxDiagram.SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
