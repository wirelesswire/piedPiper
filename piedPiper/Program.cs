




using piedPiper;
using System;
using System.Collections.Generic;
using FuzzySharp;
using System.Reflection.Metadata;
using FuzzySharp.MembershipFunctions.Functions;
using piedPiper.implementacje.obrazy;
//using piedPiper.implementacje.Hipki;
using piedPiper.implementacje.Hipki;



// --- The Single Containing Class ---
public partial class PipelineSystem
{

    public class Program
    {
        static string inputImagePath = "blurry-moon.tif"; // <--- CHANGE TO YOUR INPUT IMAGE PATH
        static string outputDirectory = "output";
        static string intermediateBaseName = "processed_step";
        static string finalBaseName = "final_output";



        public static void stringiTest()
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
            //try
            //{
            int result = finalPipeline.Execute(5.0f, out ctx); // Execute is part of IPipeline, which IBuildablePipeline inherits

            Console.WriteLine("\n--- Execution Summary ---");
            Console.WriteLine($"Pipeline final result: {result}");
            Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
            Console.WriteLine("\n--- Execution Logs ---");
            foreach (var log in ctx.Logs) { Console.WriteLine(log); }
            //}
            //catch (Exception ex) { /* ... error handling ... */ }
            Console.WriteLine("\nPipeline execution finished.");
        }


        public static void hipkiTest()
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
                .AppendProcessor(new HipekHeightProcessor(0.5f, 0.7f, 1f, 1))
                .AppendProcessor(new HipekHairProcessor(1))
                .AppendProcessor(new HipekOcenaOgolnaProcessor(2))

                ; // Called on IBuildablePipeline, returns IBuildablePipeline<float, int>

            //Console.WriteLine("Executing extended pipeline with input: 5.0f");
            Console.WriteLine($"Expected final output type: {pipeline.GetType().GenericTypeArguments[2]}"); // Check the inferred type

            PipelineSystem.Context ctx;
            //try
            //{
            var result = pipeline.Execute(hipki[1], out ctx); // Execute is part of IPipeline, which IBuildablePipeline inherits

            Console.WriteLine("\n--- Execution Summary ---");
            Console.WriteLine($"Pipeline final result: {result}");
            Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
            Console.WriteLine("\n--- Execution Logs ---");
            foreach (var log in ctx.Logs) { Console.WriteLine(log); }
            //}
            //catch (Exception ex) { /* ... error handling ... */ }
            Console.WriteLine("\nPipeline execution finished.");
        }

        public static void mainforimaghes()
        {
            var pipeline = Pipeline.Create(new PathToBitmapProcessor())
                .AppendProcessor(new ConvolutionProcessor("srednia", new float[,] {
                    { 1, 1, 1},
                    { 1, 1, 1 },
                    { 1, 1, 1} }, 1, 1))
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
            //mainforimaghes();
            //return;
            string a = "hipki ";
            a = "stringi";
            //a = "obrazki";
            
            switch (a)
            {
                case "hipki":
                    hipkiTest();
                    break;
                case "stringi":
                    stringiTest();
                    break;
                case "obrazki":
                    mainforimaghes();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter 'hipki', 'stringi', or 'obrazki'.");
                    break;
            }
        }
    }
}