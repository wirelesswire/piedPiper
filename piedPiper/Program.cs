




using piedPiper;
using System;
using System.Collections.Generic;

// --- The Single Containing Class ---
public partial class PipelineSystem
{
    // --- 1. Context Class (Nested) ---
    public class Context
    {
        public Context()
        {
            UniqueToken = Guid.NewGuid();
            Logs = new List<string>();
            // Initialize start time here or let Execute handle it
            // ProcessStartedAt = DateTime.Now; // Option 1: Initialize here
        }

        public Guid UniqueToken { get; }
        public DateTime ProcessStartedAt { get; set; }
        public DateTime ProcessEndedAt { get; set; }

        public long ProcessTimeInMilliseconds
        {
            get
            {
                // Handle cases where End might not be set yet if accessed prematurely
                if (ProcessEndedAt < ProcessStartedAt) return -1;
                return (long)ProcessEndedAt.Subtract(ProcessStartedAt).TotalMilliseconds;
            }
        }

        public List<string> Logs { get; set; }

        public void Log(string message)
        {
            Logs.Add($"{DateTime.Now:O} [{UniqueToken}] - {message}");
        }
    }

    // --- 2. Processor Interface (Nested) ---
    public interface IProcessor<InputType, OutputType>
    {
        OutputType Process(InputType input, Context context);
    }

    // --- 3. Pipeline Interface (Nested) ---
    public interface IPipeline<InputType, OutputType>
    {
        /// <summary>
        /// Executes the full pipeline, creating a new context.
        /// </summary>
        OutputType Execute(InputType input, out Context context);

        /// <summary>
        /// Executes the pipeline steps using a provided context.
        /// (Could be internal or protected if needed)
        /// </summary>
        OutputType ExecuteSubPipeline(InputType input, Context context);
    }



    // --- 5. Terminal Pipeline Implementation (Nested) ---
    // Represents the first step in the pipeline
    public class TerminalPipeline<InputType, OutputType> : PipelineBase<InputType, InputType, OutputType>
    {
        public TerminalPipeline(IProcessor<InputType, OutputType> processor)
            : base(processor) // Pass processor to base constructor
        { }

        /// <summary>
        /// Executes only the first processor in the chain.
        /// </summary>
        public override OutputType ExecuteSubPipeline(InputType input, Context context)
        {
            context.Log($"Executing terminal processor: {currentProcessor.GetType().Name}");
            var result = currentProcessor.Process(input, context);
            context.Log($"Terminal processor finished.");
            return result;
        }
    }



    public class Hipek
    {
        public string name;
        public int height;
        public string eyeColor;
        public string hairColor;

        public Hipek (string name, int height, string eyeColor, string hairColor)
        {
            this.name = name;
            this.height = height;
            this.eyeColor = eyeColor;
            this.hairColor = hairColor;
        }   
    }
    public class ocenionyHipek
    {
        public Hipek hipek;
        public float ocena;
        public ocenionyHipek(Hipek hipek, float ocena)
        {
            this.hipek = hipek; 
            this.ocena = ocena;
        }
    }

    public class HipekProcessorInput :IProcessor<Hipek, ocenionyHipek>
    {
        public ocenionyHipek Process(Hipek input, Context context)
        {

            return new ocenionyHipek(input, 0);
        }

    }
    public class HipekHeightProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {
        private float waga = 0;

        public HipekHeightProcessor(float waga)
        {
            this.waga = waga;   
        }

        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if(input == null)
            {
                throw new ArgumentNullException("input");
            }

            if(input.hipek.height > 170)
            {
                input.ocena += 1*waga;
            }

            return input;

            //throw new NotImplementedException();
        }
    }

    public class HipekEyeProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {
        private float waga = 0;

        public HipekEyeProcessor(float waga)
        {
            this.waga = waga;
        }
        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.hipek.eyeColor =="niebieski")
            {
                input.ocena += 1 * waga;
            }
            return input;
        }
    }
    public class HipekHairProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {
        private float waga = 0;

        public HipekHairProcessor(int waga)
        {
            this.waga = waga;
        }
        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.hipek.eyeColor == "blond")
            {
                input.ocena += 1 * waga;
            }
            return input;
        }
    }

    public class HipekOcenaOgolnaProcessor : IProcessor<ocenionyHipek, string>
    {
        private float passingScore = 0;

        public HipekOcenaOgolnaProcessor(int passingScore)
        {
            this.passingScore= passingScore;
        }
        public string Process(ocenionyHipek input, Context context)
        {
            if(input.ocena >= passingScore)
            {
                return "porządny obywatel";
            }
            else
            {
                return "subhuman filth";
            }

        }
    }





    // --- Now the direct chaining in Main should work ---
    public class Program
    {
        static string inputImagePath = "blurry-moon.tif"; // <--- CHANGE TO YOUR INPUT IMAGE PATH
        static string outputDirectory = "output";
        static string intermediateBaseName = "processed_step";
        static string finalBaseName = "final_output";
        public static void mainforimaghes()
        {
            var pipeline = Pipeline.Create(new PathToBitmapProcessor())
                .AppendProcessor(new ConvolutionProcessor("srednia", new float[,] {
                    { 1, 1, 1},
                    { 1, 1, 1 },
                    { 1, 1, 1} },1,1))
                .AppendProcessor(new LaplacianProcessor())
                .AppendProcessor(new BitmapSaveProcessor(outputDirectory, intermediateBaseName, ".png"));
            Context ctx;
            //try
            //{
                var result = pipeline.Execute(inputImagePath, out ctx); // Execute is part of IPipeline, which IBuildablePipeline inherits

                Console.WriteLine("\n--- Execution Summary ---");
                Console.WriteLine($"Pipeline final result: {result}");
                Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
                Console.WriteLine("\n--- Execution Logs ---");
                foreach (var log in ctx.Logs) { Console.WriteLine(log); }
            //}
            //catch (Exception ex) {
            //    Console.WriteLine(ex);
            //    /* ... error handling ... */ }
            Console.WriteLine("\nPipeline execution finished.");
        }


        public static void Main()
        {
            mainforimaghes();
            return;
            bool a = true;

            if (a)
            {

                List<Hipek> hipki = new List<Hipek>
                {
                    new Hipek("Josef",180,"niebieski","blond"),
                    new Hipek("Jose",169,"brązowy" , "brązowy")
                };

                Console.WriteLine("Building extended pipeline with direct chaining (modified design)...");

                // This should now compile and work
                var pipeline = PipelineSystem.Pipeline // Create returns IBuildablePipeline
                    .Create(new HipekProcessorInput()) // Result: IBuildablePipeline<float, string>
                    .AppendProcessor(new HipekEyeProcessor(1)) // Called on IBuildablePipeline, returns IBuildablePipeline<float, string>
                    .AppendProcessor(new HipekHeightProcessor(1))
                    .AppendProcessor(new HipekHairProcessor(1))
                    .AppendProcessor(new HipekOcenaOgolnaProcessor(2))

                    ; // Called on IBuildablePipeline, returns IBuildablePipeline<float, int>

                Console.WriteLine("Executing extended pipeline with input: 5.0f");
                Console.WriteLine($"Expected final output type: {pipeline.GetType().GenericTypeArguments[2]}"); // Check the inferred type


                PipelineSystem.Context ctx;
                try
                {
                    var result = pipeline.Execute(hipki[1], out ctx); // Execute is part of IPipeline, which IBuildablePipeline inherits

                    Console.WriteLine("\n--- Execution Summary ---");
                    Console.WriteLine($"Pipeline final result: {result}");
                    Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
                    Console.WriteLine("\n--- Execution Logs ---");
                    foreach (var log in ctx.Logs) { Console.WriteLine(log); }
                }
                catch (Exception ex) { /* ... error handling ... */ }
                Console.WriteLine("\nPipeline execution finished.");



            }
            else
            {
                Console.WriteLine("Building extended pipeline with direct chaining (modified design)...");

                // This should now compile and work
                var finalPipeline = PipelineSystem.Pipeline // Create returns IBuildablePipeline
                    .Create(new PipelineSystem.FloatToStringProcessor()) // Result: IBuildablePipeline<float, string>
                    .AppendProcessor(new PipelineSystem.RepeatStringProcessor()) // Called on IBuildablePipeline, returns IBuildablePipeline<float, string>
                    .AppendProcessor(new PipelineSystem.StringLengthProcessor()); // Called on IBuildablePipeline, returns IBuildablePipeline<float, int>

                Console.WriteLine("Executing extended pipeline with input: 5.0f");
                Console.WriteLine($"Expected final output type: {finalPipeline.GetType().GenericTypeArguments[2]}"); // Check the inferred type


                PipelineSystem.Context ctx;
                try
                {
                    int result = finalPipeline.Execute(5.0f, out ctx); // Execute is part of IPipeline, which IBuildablePipeline inherits

                    Console.WriteLine("\n--- Execution Summary ---");
                    Console.WriteLine($"Pipeline final result: {result}");
                    Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
                    Console.WriteLine("\n--- Execution Logs ---");
                    foreach (var log in ctx.Logs) { Console.WriteLine(log); }
                }
                catch (Exception ex) { /* ... error handling ... */ }
                Console.WriteLine("\nPipeline execution finished.");
            }

           
        }
    }
}