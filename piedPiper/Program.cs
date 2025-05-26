




using piedPiper;
using System;
using System.Collections.Generic;
using FuzzySharp;
using System.Reflection.Metadata;
using FuzzySharp.MembershipFunctions.Functions;
using piedPiper.implementacje.obrazy;
using piedPiper.implementacje.Hipki;
using piedPiper.pipeline;



public partial class PipelineSystem
{

    public class Program
    {
        static string inputImagePath = "blurry-moon.tif"; // <--- YOUR INPUT IMAGE PATH
        static string outputDirectory = "output";
        static string intermediateBaseName = "processed_step";
        static string finalBaseName = "final_output";



        public static void stringiTest()
        {
            Console.WriteLine("Building extended pipeline with direct chaining (modified design)...");

            var finalPipeline = PipelineSystem.Pipeline 
                .Create(new PipelineSystem.FloatToStringProcessor()) 
                .AppendProcessor(new PipelineSystem.RepeatStringProcessor()) 
                .AppendProcessor(new PipelineSystem.StringLengthProcessor()); 

            Console.WriteLine("Executing extended pipeline with input: 5.0f");
            Console.WriteLine($"Expected final output type: {finalPipeline.GetType().GenericTypeArguments[2]}"); 


            PipelineSystem.Context ctx;
            int result = finalPipeline.Execute(5.0f, out ctx); 
            
            Console.WriteLine("\n--- Execution Summary ---");
            Console.WriteLine($"Pipeline final result: {result}");
            Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
            Console.WriteLine("\n--- Execution Logs ---");
            foreach (var log in ctx.Logs) { Console.WriteLine(log); }
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

            IBuildablePipeline<Hipek,string> pipeline = PipelineSystem.Pipeline                
                .Create(new HipekProcessorInput()) 
                .AppendProcessor(new HipekEyeProcessor()) 
                .AppendProcessor(new HipekHeightProcessor(0.5f, 0.7f, 1f))
                .AppendProcessor(new HipekHairProcessor())
                .AppendProcessor(new HipekOcenaOgolnaProcessor(0.7f))


                ; 

            Console.WriteLine($"Expected final output type: {pipeline.GetType().GenericTypeArguments[2]}"); // Check the inferred type

            PipelineSystem.Context ctx;
            var result = pipeline.Execute(hipki[1], out ctx); 

            Console.WriteLine("\n--- Execution Summary ---");
            Console.WriteLine($"Pipeline final result: {result}");
            Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
            Console.WriteLine("\n--- Execution Logs ---");
            foreach (var log in ctx.Logs) { Console.WriteLine(log); }
            
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


            string currentDirectory = Directory.GetCurrentDirectory();

            string searchPattern = "*.tif";

            IEnumerable<string> tifFiles = Directory.EnumerateFiles(currentDirectory, searchPattern);

            string[] inputs = {"blurry-moon.tif","bonescan.tif"};
            inputs = tifFiles.ToArray();
            DateTime start = DateTime.Now;  


            if (false   )
            {
                foreach (var inputImagePath in inputs)
                {
                    Console.WriteLine($"Executing extended pipeline with input: {inputImagePath}");
                    Console.WriteLine($"Expected final output type: {pipeline.GetType().GenericTypeArguments[2]}"); 
                    var result = pipeline.Execute(inputImagePath, out ctx); 
                    Console.WriteLine("\n--- Execution Summary ---");
                    Console.WriteLine($"Pipeline final result: {result}");
                    Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
                    Console.WriteLine("\n--- Execution Logs ---");
                    foreach (var log in ctx.Logs) { Console.WriteLine(log); }

                    Console.WriteLine("\nPipeline execution finished.");
                }
            }
            else
            {

                List<PipelineExtensions.PipelineExecutionResult<string, string>> batchResults = 
                    pipeline.ExecuteBatchParallel(inputs).ToList(); 

                foreach (var item in batchResults)
                {
                    Console.WriteLine("\n--- Execution Summary ---");
                    Console.WriteLine($"Pipeline final result: {item.Output}");
                    Console.WriteLine($"Pipeline execution took {item.Context.ProcessTimeInMilliseconds} milliseconds");
                    Console.WriteLine("\n--- Execution Logs ---");
                    foreach (var log in item.Context.Logs)
                    {
                        Console.WriteLine(log);
                    }

                }
            }

            DateTime end = DateTime.Now;

            Console.WriteLine($"Total execution time of program: {(end - start).TotalMilliseconds} milliseconds");



        }


        public static void Main()
        {
            //mainforimaghes();
            //return;
            string a = "hipki";
            //a = "stringi";
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