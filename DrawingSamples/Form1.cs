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
    public delegate FuzzySet<float> FuzzySetCombiner(FuzzySet<float> a, FuzzySet<float> b);

    public partial class Form1 : Form
    {
        private static float LeftFunc(float x) => float.CreateTruncating(Math.Abs(Math.Cos(x)));

        private static float RightFunc(float x) => float.CreateTruncating(Math.Abs(Math.Sin(x)));
        
        private static float LeftFuncOne(float x) => 1.0f;

        private static float RightFuncOne(float x) => 1.0f;

        static IEnumerable<float> GenerateFloatRange(float start, float end, float step)
        {
            List<float> result = new List<float>();

            for (float value = start; value <= end; value += step) result.Add(value);

            return result;
        }

        public static FuzzySet<float> Combine<TNorm>(
            FuzzySet<float> a,
            FuzzySet<float> b)
            where TNorm : INormOperation<TNorm, float>
        {
            var combined = new Dictionary<float, float>();

            foreach (var kvp in a.Values)
            {
                if (b.Values.TryGetValue(kvp.Key, out var bValue))
                {
                    combined[kvp.Key] = TNorm.Calculate(kvp.Value, bValue);
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
            {"Bell", new BellMembershipFunction<float>(3f,1.5f,0f)},
            {"Binary", new BinaryMembershipFunction<float>(0f, false)},
            {"Class L", new ClassLMembershipFunction<float>(-5f, 5f)},
            {"Class S", new ClassSMembershipFunction<float>(-5f, 5f)},
            {"Gamma", new GammaMembershipFunction<float>(-5f, 5f)},
            {"Gauss", new GaussMembershipFunction<float>(0f, 1f, 1.5f)},
            {"Left-Right", new LeftRightMembershipFunction<float>(0f, 2f, 2f, LeftFunc, RightFunc)},
            {"LR-One", new LeftRightMembershipFunction<float>(0f, 2f, 2f, LeftFuncOne, RightFuncOne)},
            {"Sigmoid", new SigmoidMembershipFunction<float>(0f, 2f)},
            {"Step", new StepMembershipFunction<float>([-8.0f, -5.0f, 0f, 1f, 8.0f])},
            {"Trapeze", new TrapezeMembershipFunction<float>(-5f, -2f, 2f, 7f)},
            {"Triangle", new TriangleMembershipFunction<float>(-2f, 0f, 2.7f)},
            {"Zadeh", new ZadehMembershipFunction<float>(0f)},
        };

        public static readonly Dictionary<string, FuzzySetCombiner> NormCombines = new()
        {
            { "Min", (a, b) => Combine<NormOperationMin<float>>(a, b) },
            { "Max", (a, b) => Combine<NormOperationMax<float>>(a, b) },
            { "Algebraic Product", (a, b) => Combine<NormOperationAlgebraicProd<float>>(a, b) },
            { "Bounded Difference", (a, b) => Combine<NormOperationBoundedDiff<float>>(a, b) },
            { "Drastic Product", (a, b) => Combine<NormOperationDrasticProd<float>>(a, b) },
            { "Einstein Product", (a, b) => Combine<NormOperationEinsteinProd<float>>(a, b) },
            { "Hamacher Product", (a, b) => Combine<NormOperationHamacherProd<float>>(a, b) },
            { "Yager T-Norm", (a, b) => Combine<NormOperationYagerOperatorComplement<float>>(a, b) },
            { "Algebraic Sum", (a, b) => Combine<NormOperationAlgebraicSum<float>>(a, b) },
            { "Bounded Sum", (a, b) => Combine<NormOperationBoundedSum<float>>(a, b) },
            { "Drastic Sum", (a, b) => Combine<NormOperationDrasticSum<float>>(a, b) },
            { "Einstein Sum", (a, b) => Combine<NormOperationEinsteinSum<float>>(a, b) },
            { "Hamacher Sum", (a, b) => Combine<NormOperationHamacherSum<float>>(a, b) },
            { "Yager S-Norm", (a, b) => Combine<NormOperationYagerOperator<float>>(a, b) },
        };

        private static IEnumerable<float> dataRange = GenerateFloatRange(-10f, 10f, 0.1f);

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDrawDiagram_Click(object sender, EventArgs e)
        {
            var fuzzySetFirstKey = comboBoxMembershipFunctionFirst.SelectedItem?.ToString() ?? "LR-One";
            var fuzzySetSecondKey = comboBoxMembershipFunctionSecond.SelectedItem?.ToString() ?? "LR-One";

            var fuzzySetFirst = new FuzzySet<float>(dataRange
                .ToDictionary(
                    x => x,
                    x => membershipFunctions[fuzzySetFirstKey].GetMembership(x)
                ));

            var fuzzySetSecond = new FuzzySet<float>(dataRange
                .ToDictionary(
                    x => x,
                    x => membershipFunctions[fuzzySetSecondKey].GetMembership(x)
                ));


            var selectedNorm = comboBoxOperator.SelectedItem?.ToString() ?? "Min";

            if (NormCombines.TryGetValue(selectedNorm, out var combineFunc))
            {
                var combinedSet = combineFunc(fuzzySetFirst, fuzzySetSecond);
                Bitmap fuzzyImage = DrawFuzzySet(combinedSet);
                pictureBoxDiagram.Image = fuzzyImage;
                pictureBoxDiagram.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }
    }
}
