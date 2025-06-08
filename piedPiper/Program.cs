using piedPiper;
using System;
using System.Collections.Generic;
using FuzzySharp;
using System.Reflection.Metadata;
using FuzzySharp.MembershipFunctions.Functions;
using piedPiper.implementacje.obrazy;
using piedPiper.implementacje.Hipki;
using piedPiper.implementacje.stringi;
using piedPiper.pipeline;

public  class PipelineSystem
{
    public class Program
    {
        static string inputImagePath = "blurry-moon.tif"; 
        static string outputDirectory = "output";
        static string intermediateBaseName = "processed_step";
        static string finalBaseName = "final_output";
        public static  void Logger<T>(Context ctx, T result, bool showLogs = true )
        {
            Console.WriteLine("\n--- Execution Summary ---");
            Console.WriteLine($"Pipeline final result: {result}");
            Console.WriteLine($"Pipeline execution took {ctx.ProcessTimeInMilliseconds} milliseconds");
            if (showLogs)
            {
            Console.WriteLine("\n--- Execution Logs ---");
            foreach (var log in ctx.Logs) { Console.WriteLine(log); }

            }
            Console.WriteLine("\nPipeline execution finished.");
        }

        public static void stringiTest()
        {
            Console.WriteLine("Building extended pipeline with direct chaining (modified design)...");

            var finalPipeline = Pipeline 
                .Create(new FloatToStringProcessor()) 
                .AppendProcessor(new RepeatStringProcessor(13))
                .AppendProcessor(new RepeatStringProcessor(2))
                .AppendProcessor(new StringLengthProcessor()); 

            Console.WriteLine("Executing extended pipeline with input: 5.0f");
            Console.WriteLine($"Expected final output type: {finalPipeline.GetType().GenericTypeArguments[2]}"); 


            Context ctx;
            int result = finalPipeline.Execute(5.0f, out ctx); 
            
            Logger(ctx, result); 
        }

        public static void hipkiTest()
        {
            List<Hipek> hipki = new List<Hipek>
                {
                    new Hipek("Josef",180,"niebieski","blond"),
                    new Hipek("Jose",169,"brązowy" , "brązowy")
                };

            Console.WriteLine("Building extended pipeline with direct chaining (modified design)...");
            IBuildablePipeline<Hipek,string> pipeline = Pipeline                
                .Create(new HipekProcessorInput()) 
                .AppendProcessor(new HipekEyeProcessor()) 
                .AppendProcessor(new HipekHeightProcessor(0.5f, 0.7f, 1f))
                .AppendProcessor(new HipekHairProcessor())
                .AppendProcessor(new HipekOcenaOgolnaProcessor(0.7f))
                ; 
            Console.WriteLine($"Expected final output type: {pipeline.GetType().GenericTypeArguments[2]}"); // Check the inferred type
            Context ctx;
            var result = pipeline.Execute(hipki[1], out ctx);

            Logger(ctx, result);
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


            if (true    )
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

                //List<PipelineExtensions.PipelineExecutionResult<string, string>> batchResults = 
                //    pipeline.ExecuteBatchParallel(inputs).ToList(); 

                //foreach (var item in batchResults)
                //{
                //    Console.WriteLine("\n--- Execution Summary ---");
                //    Console.WriteLine($"Pipeline final result: {item.Output}");
                //    Console.WriteLine($"Pipeline execution took {item.Context.ProcessTimeInMilliseconds} milliseconds");
                //    Console.WriteLine("\n--- Execution Logs ---");
                //    foreach (var log in item.Context.Logs)
                //    {
                //        Console.WriteLine(log);
                //    }

                //}
            }

            DateTime end = DateTime.Now;

            Console.WriteLine($"Total execution time of program: {(end - start).TotalMilliseconds} milliseconds");



        }

        public static void mainforimages()
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            Console.WriteLine("--- Building Nested Split Image Pipeline ---");

            var imageSaver = new BitmapSaveProcessor(outputDirectory, "image_output", ".png");

            var nestedImagePipeline = Pipeline
                .Create(new PathToBitmapProcessor()) // Input: string (image path), Output: Bitmap
                .AppendProcessor(new ConvolutionProcessor("pre_split_sharpen", ImageKernels.SharpenKernel, 1, 1)) // Basic sharpening
                .AppendProcessor(imageSaver) // Save sharpened image, output is path string
                .Split( // Outer Split: Input is string (path to sharpened image), Branches output string (paths)
                        // Outer Branch 1: Gaussian Blur + Inner Split
                    outerBranch1 => outerBranch1 // Input: string (path)
                        .AppendProcessor(new PathToBitmapProcessor()) // string -> Bitmap
                        .AppendProcessor(new ConvolutionProcessor("gaussian_blur", ImageKernels.Gaussian3x3, ImageKernels.Gaussian3x3Divisor, 1)) // Bitmap -> Bitmap
                        .AppendProcessor(imageSaver) // Save blurred image, output is path string
                        .Split( // Inner Split: Input is string (path to blurred image), Branches output string (paths)
                            innerBranch1_1 => innerBranch1_1 // Input: string (path)
                                .AppendProcessor(new PathToBitmapProcessor()) // string -> Bitmap
                                .AppendProcessor(new ConvolutionProcessor("sobel_x", ImageKernels.SobelX, 1, 1)) // Bitmap -> Bitmap
                                .AppendProcessor(imageSaver), // Save Sobel X image, output is path string
                            innerBranch1_2 => innerBranch1_2 // Input: string (path)
                                .AppendProcessor(new PathToBitmapProcessor()) // string -> Bitmap
                                .AppendProcessor(new ConvolutionProcessor("sobel_y", ImageKernels.SobelY, 1, 1)) // Bitmap -> Bitmap
                                .AppendProcessor(imageSaver) // Save Sobel Y image, output is path string
                        ) // Inner Split output: IEnumerable<string> (paths to Sobel X and Sobel Y images)
                        .AppendProcessor(new CombineStringsJoinProcessor(" & ")), // Join inner results: Single string path (e.g., "path/sobel_x.png & path/sobel_y.png")

                    // Outer Branch 2: Unsharp Mask
                    outerBranch2 => outerBranch2 // Input: string (path)
                        .AppendProcessor(new PathToBitmapProcessor()) // string -> Bitmap
                        .AppendProcessor(new ConvolutionProcessor("unsharp_mask", ImageKernels.SharpenKernel, 1, 1)) // Using sharpen as proxy for unsharp for simplicity
                        .AppendProcessor(imageSaver) // Save unsharp image, output is path string
                ) // Outer Split output: IEnumerable<string> (contains two strings, one from each outer branch)
                .AppendProcessor(new CombineStringsJoinProcessor(" || ")); // Final Join: combines results from outer branches

            string[] inputs = { "blurry-moon.tif" }; // Use a single image for clearer testing

            DateTime start = DateTime.Now;

            // Execute the pipeline
            List<PipelineExtensions.PipelineExecutionResult<string, string>> batchResults =
                nestedImagePipeline.ExecuteBatchParallel(inputs).ToList();

            foreach (var item in batchResults)
            {
                Console.WriteLine("\n--- Image Pipeline Execution Summary ---");
                Console.WriteLine($"Input: {item.Input}");
                Console.WriteLine($"Pipeline final result (combined paths): {item.Output}");
                if (item.Exception != null)
                {
                    Console.WriteLine($"ERROR: {item.Exception.Message}");
                    if (item.Exception is AggregateException aggEx)
                    {
                        foreach (var inner in aggEx.InnerExceptions)
                        {
                            Console.WriteLine($"  Inner Exception: {inner.Message}");
                        }
                    }
                }
                Console.WriteLine($"Pipeline execution took {item.Context.ProcessTimeInMilliseconds} milliseconds");
                Console.WriteLine("\n--- Image Pipeline Execution Logs ---");
                foreach (var log in item.Context.Logs)
                {
                    Console.WriteLine(log);
                }
            }

            DateTime end = DateTime.Now;
            Console.WriteLine($"Total program execution time: {(end - start).TotalMilliseconds} milliseconds");
        }

        public static void Main()
        {
            string a = "hipki";
            //a = "stringi";
            a = "obrazki2";
            //a= "splitjoin"; // New test case
            switch (a)
            {
                case "hipki":
                    hipkiTest();
                    break;
                case "stringi":
                    stringiTest();
                    break;
                case "obrazki":
                    //mainforimaghes();
                    break;
                case "obrazki2":
                    mainforimages();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter 'hipki', 'stringi', or 'obrazki'.");
                    break;
            }
        }
    }
}