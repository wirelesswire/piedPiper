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
using piedPiper.pipeline.piedPiper.pipeline;

public class PipelineSystem
{
    public class Program
    {
        static string inputImagePath = "blurry-moon.tif";
        static string outputDirectory = "output";
        static string intermediateBaseName = "processed_step";
        static string finalBaseName = "final_output";
        public static void Logger<IN, OUT>(IBuildablePipeline<IN, OUT> p, out Context ctx, IN input, OUT result, bool showLogs = false)
        {

            Console.WriteLine("Executing extended pipeline with input: 5.0f");
            Console.WriteLine($"Expected final output type: {p.GetType().GenericTypeArguments[2]}");

            result = p.Execute(input, out ctx);


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

        public static void stringiSplitTest()
        {
            var finalPipeline = Pipeline
                .Create(new FloatToStringProcessor())
                .AppendProcessor(new RepeatStringProcessor(2))
                .Split(
                    branch1 => branch1
                         .AppendProcessor(new AddSuffixProcessor("ala")),
                    branch2 => branch2
                        .AppendProcessor(new AddPrefixProcessor("kota ma"))
                )
                .AppendProcessor(new CombineStringsJoinProcessor(" | "));
            Context ctx;
            string result = "";
            Logger(finalPipeline, out ctx, 6, result, true);
            string res = "";
            finalPipeline = finalPipeline.AppendProcessor(new AppendStringProcessor("Lorem ipsum "));
            Logger(finalPipeline, out ctx, 5, res);
        }
        public static void stringSimpleTest()
        {
            Console.WriteLine("--- Scenariusz Testowy Potoku Stringowego ---");

            string initialInput = "Hello world! This is a test with some numbers 123.";
            var stringPipeline = Pipeline.Create(new ToUpperCaseProcessor())
                .AppendProcessor(new ReverseStringProcessor())
                .AppendProcessor(new ReplaceSpacesWithUnderscoresProcessor())
                .AppendProcessor(new AddPrefixProcessor("->START<-"))
                .AppendProcessor(new AddSuffixProcessor("-END<-"))
                .AppendProcessor(new RemoveDigitsProcessor());
            Console.WriteLine($"Początkowy input: '{initialInput}'\n");
            Context context;
            string finalOutput = null;

            finalOutput = stringPipeline.Execute(initialInput, out context);

            Console.WriteLine($"\nFinalny output potoku: '{finalOutput}'");

            Console.WriteLine("\n--- Logi Potoku Stringowego ---");
            foreach (var log in context.Logs)
            {
                Console.WriteLine(log);
            }

        }
        public static void hipkiTest()
        {
            List<Hipek> hipki = new List<Hipek>
                {
                    new Hipek("Josef",180,"niebieski","blond"),
                    new Hipek("Jose",169,"brązowy" , "brązowy")
                };

            Console.WriteLine("Building extended pipeline with direct chaining (modified design)...");
            IBuildablePipeline<Hipek, string> pipeline = Pipeline
                .Create(new HipekProcessorInput())
                .AppendProcessor(new HipekEyeProcessor())
                .AppendProcessor(new HipekHeightProcessor(0.5f, 0.7f, 1f))
                .AppendProcessor(new HipekHairProcessor())
                .AppendProcessor(new HipekOcenaOgolnaProcessor(0.7f))
                ;
            Console.WriteLine($"Expected final output type: {pipeline.GetType().GenericTypeArguments[2]}"); // Check the inferred type
            Context ctx;
            //var result = pipeline.Execute(hipki[1], out ctx);
            string result = "";
            Logger(pipeline, out ctx, hipki[1], result, true);
        }

        public static void doSprawozdania()
        {

            var p = Pipeline.Create(new AddPrefixProcessor(" ala ma kota "));

            Context c = new Context();
            string result = p.Execute("tekst", out c);
            Console.WriteLine(result);

        }
        public static void RunHipekFuzzyPipelineScenario()
        {
            Console.WriteLine("--- Scenariusz Testowy Potoku Fuzzy dla Hipków ---");
            List<Hipek> hipki = new List<Hipek>
        {
            new Hipek("Jan Kowalski", 185, "niebieskie", "blond"), 
            new Hipek("Piotr Nowak", 195, "brązowe", "czarne"),    
            new Hipek("Marek Lis", 170, "piwne", "brązowe"),      
            new Hipek("Tomek Wilk", 155, "niebieskie", "rude")    
        };

            var hipekEvaluationPipeline = Pipeline.Create(new HipekValidatorProcessor()) // 1. Walidacja
                .AppendProcessor(new HipekNormalizerProcessor())                         // 2. Normalizacja
                .AppendProcessor(new HipekFuzzyEvaluatorProcessor())                     // 3. Ocena Rozmyta (zwraca ocenionyHipek)
                .AppendProcessor(new EvaluatedHipekLoggerProcessor())                    // 4. Logowanie wyniku (przekazuje dalej ocenionyHipek)
                .AppendProcessor(new ScoreCategorizerProcessor())                        // 5. Kategoryzacja (zwraca string)
                .AppendProcessor(new FinalMessageBuilderProcessor());                    // 6. Budowa finalnej wiadomości (zwraca string)
            foreach (var hipek in hipki)
            {
                Console.WriteLine($"\n--- Ocena Hipka: {hipek.name} ---");
                Context context = null;
                string finalMessage = null;
                try
                {
                    finalMessage = hipekEvaluationPipeline.Execute(hipek, out context);

                    Console.WriteLine($"\nWynik dla {hipek.name}: {finalMessage}");
                    Console.WriteLine(context.Logs[context.Logs.Count - 4]);
                    //Console.WriteLine("\n--- Logi Potoku ---");
                    //foreach (var log in context.Logs)
                    //{
                    //    Console.WriteLine(log);
                    //}
                    //Console.WriteLine($"Całkowity czas wykonania dla {hipek.name}: {context.ProcessTimeInMilliseconds} ms.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nBłąd podczas oceny Hipka {hipek.name}: {ex.Message}");
                    if (context != null)
                    {
                        Console.WriteLine("\n--- Logi Potoku (częściowe) ---");
                        foreach (var log in context.Logs)
                        {
                            Console.WriteLine(log);
                        }
                    }
                }
            }
            Console.WriteLine("\n--- Koniec Scenariusza Testowego ---");
        }




        public static void Main()
        {
            //doSprawka();
            //return;
            string a = "hipki";
            a = "hipkiFuzzy";
            //a = "stringi";
            //a="stringSimpleTest";
            //a = "obrazki";
            //a = "obrazki2";
            switch (a)
            {
                case "hipki":
                    hipkiTest();
                    break;
                case "hipkiFuzzy":
                    RunHipekFuzzyPipelineScenario();
                    break;
                case "stringSimpleTest":
                    stringSimpleTest();
                    break;
                case "stringi":
                    stringiSplitTest();
                    break;
                //case "obrazki":
                //    //mainforimaghes();
                //    break;
                //case "obrazki2":
                //    mainforimages();
                //    break;
                default:
                    Console.WriteLine("Invalid input. Please enter 'hipki', 'stringi', or 'obrazki'.");
                    break;
            }
        }
    }
}