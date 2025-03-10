////// See https://aka.ms/new-console-template for more information
////Console.WriteLine("Hello, World!");









//Dog adog = new Dog("burek", "kundel bury ");
//Dog bdog = new Dog("burek", "Kundel Bury ");

//Schronisko s = new Schronisko(new List<Dog> { adog, bdog });

//IPipelineElement<WaterFlow, List<string>> d = new duzeLiteryPipeElement();

//SimpleOut simpleOut = new SimpleOut();
//SimpleIn simpleIn = new SimpleIn();

////d.setNext((IPipelineElement<List<string>, object>)simpleOut);
//simpleIn.SetFirstElement((IPipelineElement<WaterFlow, >)d);


//Pipe<WaterFlow, List<string>, List<string>> p = new Pipe<WaterFlow, List<string>, List<string>>(simpleIn, simpleOut);

//simpleOut.Output(d.Process(s));




//public class Pipe<T, R, O>
//{
//    public IPipeInput<T, R> input;
//    public IPipeOutput<O> output;

//    public Pipe(IPipeInput<T, R> input, IPipeOutput<O> output)
//    {
//        this.input = input;
//        this.output = output;
//    }




//}






//public interface IPipelineElement<I, O> // in I, out O for variance (input, output)
//{
//    IPipelineElement<O, Type> Next { get; set; } // Next element takes output O as input

//    public void setNext(IPipelineElement<O, Type> next);
//    //public void setNext(IPipeOutput<O> next);/

//    O Process(I input);
//}

//public interface IPipeOutput<in T> // in T for variance
//{
//    void Output(T result);
//}

//public interface IPipeInput<T, R> // in T, out R for variance
//{
//    void SetFirstElement(IPipelineElement<T, R> first); // Set the first element
//    void SetOutputHandler(IPipeOutput<R> output); // Set the output handler
//    void Input(T inputData); // Start the pipeline
//}


//public interface WaterFlow
//{
//    public IEnumerable<Residue> getResidues();
//}
//public interface Residue
//{
//    public string getStringRespresentation();
//}
//public class duzeLiteryPipeElement : IPipelineElement<WaterFlow, List<string>>
//{
//    public IPipelineElement<List<string>, Type> Next { get; set; }

//    string process(Residue input)
//    {
//        string r = "";

//        foreach (var a in input.getStringRespresentation())
//        {
//            //only uppercase 
//            if (a.CompareTo('a') >= 0)
//            {
//                r += a;
//            }

//        }


//        return r;
//    }
//    public List<string> Process(WaterFlow input)
//    {
//        List<string> output = new List<string>();

//        foreach (Residue i in input.getResidues())
//        {
//            output.Add(process(i));
//        }
//        return output;

//    }

//    public void setNext(IPipelineElement<List<string>, Type> next)
//    {
//        this.Next = next;
//        //throw new NotImplementedException();
//    }

//    //public void setNext(IPipeOutput<List<string>> next)
//    //{
//    //    //this.Next = next;
//    //    //throw new NotImplementedException();
//    //}

//    //public List<string> Process(List<WaterFlow> input)
//    //{
//    //    throw new NotImplementedException();
//    //}
//}



//public class SimpleOut : IPipeOutput<List<string>>
//{
//    //public void output (string a )
//    //{
//    //    Console.WriteLine(a);
//    //}
//    public void Output(List<string> a)
//    {
//        foreach (var b in a)
//        {
//            Console.WriteLine(b);

//        }
//    }
//}

//public class SimpleIn : IPipeInput<WaterFlow, List<string>>
//{
//    IPipelineElement<WaterFlow, List<string>> input;


//    public void Input(WaterFlow inputData)
//    {


//        throw new NotImplementedException();
//    }

//    //public void SetFirstElement(IPipelineElement<WaterFlow, List<string>> first)
//    //{
//    //    throw new NotImplementedException();
//    //}

//    //public void SetFirstElement(IPipelineElement<WaterFlow, List<string> first)
//    //{
//    //    throw new NotImplementedException();
//    //}

//    public void SetFirstElement(IPipelineElement<WaterFlow, List<string>> first)
//    {
//        throw new NotImplementedException();
//    }

//    //public void SetOutputHandler(IPipeOutput<object> output)
//    //{
//    //    throw new NotImplementedException();
//    //}

//    public void SetOutputHandler(IPipeOutput<List<string>> output)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class Dog : Residue // residue
//{
//    public string name;
//    public string description;

//    public Dog(string name, string desc)
//    {
//        this.name = name;
//        this.description = desc;
//    }

//    public string getStringRespresentation()
//    {
//        return name + " " + description;
//    }

//    public string zwrocPsa()
//    {
//        return name + " " + description;
//    }

//}
//public class Schronisko : WaterFlow // river 
//{
//    public List<Dog> dogs;

//    public Schronisko(List<Dog> d)
//    {
//        dogs = d;
//    }

//    public IEnumerable<Residue> getResidues()
//    {
//        return dogs;
//    }

//    public List<Dog> zwrocPsy()
//    {
//        return dogs;
//    }

//}


//////public class Pipe<T,T1,T2>
//////{
//////    //List<PipeElement> elements;
//////    public PipeOutput<T2> PipeOutput;
//////    public PipeInput<T> PipeInput;

//////    public Pipe(PipeInput<T> pipeInput, PipeOutput<T2> output)
//////    {
//////        //this.elements = elements;
//////        this.PipeOutput = output;
//////        this.PipeInput = pipeInput;
//////    }


//////    public List<string> input(List<Residue> i )
//////    {


//////        //foreach ( PipeElement  p in elements) { 
//////        //    i = p.process(i);            
//////        //}
//////        //return i; 
//////    }
//////    public void output()
//////    {

//////    }

//////}






//////public interface el<T,T1,T2> // input output outputnext   
//////{
//////    public el<T1, T2 > next;
//////    public T1 process(T input);
//////}

//////public interface PipeElement<T,T1,T2>  : el<T, T1, T2> 
//////{
//////    //public  next
//////    public T1 process(T input);
//////}
//////public interface PipeOutput<T>
//////{
//////    public void  output(T output);
//////}

//////public interface PipeInput<T,T1,T2>
//////{

//////    public PipeElement<T1,T2> first { get; set; }
//////    public void input(T a);
//////}


////using System;
////using System.Collections.Generic;



////// 1. Define Generic Interfaces for Pipeline Elements, Input, and Output

////// IPipeInput: Interface for elements that accept input of type T
////public interface IPipeInput<in T>
////{
////    void SetInput(T input);
////}

////// IPipeOutput: Interface for elements that produce output of type T
////public interface IPipeOutput<out T>
////{
////    T Output { get; }
////}

////// IPipelineElement: Interface for a generic pipeline element that transforms input of type TIn to output of type TOut
////public interface IPipelineElement<in TIn, out TOut> : IPipeInput<TIn>, IPipeOutput<TOut>
////{
////    void Process(); // Executes the element's operation
////}

////// 2. Implement Concrete Pipeline Elements for Numerical Operations

////// AddElement: Adds a constant value to the input
////public class AddElement<T> : IPipelineElement<T, T> where T : struct, IConvertible
////{
////    private T _input;
////    private T _output;
////    public T ValueToAdd { get; set; }

////    public AddElement(T valueToAdd)
////    {
////        ValueToAdd = valueToAdd;
////    }

////    public void SetInput(T input)
////    {
////        _input = input;
////    }

////    public T Output => _output;

////    public void Process()
////    {
////        // Convert to double for numerical operations (assuming T is convertible to double)
////        double inputValue = Convert.ToDouble(_input);
////        double valueToAddDouble = Convert.ToDouble(ValueToAdd);
////        _output = (T)Convert.ChangeType(inputValue + valueToAddDouble, typeof(T));
////    }
////}

////// MultiplyElement: Multiplies the input by a constant value
////public class MultiplyElement<T> : IPipelineElement<T, T> where T : struct, IConvertible
////{
////    private T _input;
////    private T _output;
////    public T ValueToMultiply { get; set; }

////    public MultiplyElement(T valueToMultiply)
////    {
////        ValueToMultiply = valueToMultiply;
////    }

////    public void SetInput(T input)
////    {
////        _input = input;
////    }

////    public T Output => _output;

////    public void Process()
////    {
////        double inputValue = Convert.ToDouble(_input);
////        double valueToMultiplyDouble = Convert.ToDouble(ValueToMultiply);
////        _output = (T)Convert.ChangeType(inputValue * valueToMultiplyDouble, typeof(T));
////    }
////}

////// SquareRootElement: Calculates the square root of the input
////public class SquareRootElement<T> : IPipelineElement<T, T> where T : struct, IConvertible
////{
////    private T _input;
////    private T _output;

////    public void SetInput(T input)
////    {
////        _input = input;
////    }

////    public T Output => _output;

////    public void Process()
////    {
////        double inputValue = Convert.ToDouble(_input);
////        if (inputValue < 0)
////        {
////            throw new ArgumentOutOfRangeException("Input must be non-negative for square root.");
////        }
////        _output = (T)Convert.ChangeType(Math.Sqrt(inputValue), typeof(T));
////    }
////}
////// (Interfaces IPipeInput, IPipeOutput, IPipelineElement and numerical element classes remain the same as in the previous example)
////// ... (Copy interfaces and AddElement, MultiplyElement, SquareRootElement classes from the previous example here)

////// 1. Pipe Class Definition
////public class Pipe
////{
////    private readonly List<object> _elements = new List<object>(); // Store elements as objects

////    // 2. AddElement Method (Generic)
////    public Pipe AddElement<TIn, TOut>(IPipelineElement<TIn, TOut> element)
////    {
////        if (_elements.Count > 0)
////        {
////            // Type check: Ensure input type of the new element matches the output type of the last element
////            var lastElement = _elements[_elements.Count - 1];
////            Type lastOutputType = GetOutputType(lastElement);
////            Type currentInputType = typeof(TIn);

////            if (lastOutputType != currentInputType)
////            {
////                throw new ArgumentException($"Type mismatch: Input type of element '{element.GetType().Name}' ({currentInputType.Name}) does not match the output type of the previous element ({lastOutputType.Name}).");
////            }
////        }
////        _elements.Add(element);
////        return this; // For method chaining
////    }

////    // Helper method to get Output type using reflection (less ideal, but works for this example)
////    //private static Type GetOutputType(object element)
////    //{
////    //    if (element is IPipeOutput<object> pipeOutput) // Check if it's an IPipeOutput (using object as a fallback)
////    //    {
////    //        // Assuming IPipeOutput<T> has a property 'Output' of type T
////    //        var outputProperty = element.GetType().GetProperty("Output");
////    //        if (outputProperty != null)
////    //        {
////    //            return outputProperty.PropertyType;
////    //        }
////    //    }
////    //    throw new ArgumentException($"Could not determine output type of element: {element.GetType().Name}. Ensure it implements IPipeOutput<T> correctly.");
////    //}
////    private static Type GetOutputType(object element)
////    {
////        Type elementType = element.GetType();
////        foreach (Type iface in elementType.GetInterfaces())
////        {
////            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IPipeOutput<>))
////            {
////                // Found IPipeOutput<T>, get the T (generic type argument)
////                return iface.GetGenericArguments()[0];
////            }
////        }
////        throw new ArgumentException($"Could not determine output type of element: {element.GetType().Name}. Ensure it implements IPipeOutput<T> correctly.");
////    }


////    // 3. Run Method (Generic Input)
////    public TOutput Run<TInput, TOutput>(TInput input)
////    {
////        if (_elements.Count == 0)
////        {
////            throw new InvalidOperationException("Pipe is empty. Add elements before running.");
////        }

////        object currentInput = input; // Start with the initial input

////        foreach (var elementObj in _elements)
////        {
////            if (elementObj is IPipeInput<object> pipeInput) // Use object as a fallback for IPipeInput
////            {
////                pipeInput.SetInput(currentInput);
////            }
////            else
////            {
////                throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not implement IPipeInput.");
////            }

////            if (elementObj is IPipelineElement<object, object> pipelineElement) // Use object as fallback for IPipelineElement
////            {
////                pipelineElement.Process();
////                if (elementObj is IPipeOutput<object> pipeOutput) // And IPipeOutput
////                {
////                    currentInput = pipeOutput.Output;
////                }
////                else
////                {
////                    throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not implement IPipeOutput.");
////                }
////            }
////            else if (elementObj is IPipelineElement<object, object> genericPipelineElement) // More robust handling for generic IPipelineElement
////            {
////                // Dynamically invoke Process and Output using reflection to handle generics
////                var processMethod = genericPipelineElement.GetType().GetMethod("Process");
////                if (processMethod != null)
////                {
////                    processMethod.Invoke(genericPipelineElement, null);
////                }
////                else
////                {
////                    throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not have a Process method.");
////                }

////                var outputProperty = genericPipelineElement.GetType().GetProperty("Output");
////                if (outputProperty != null)
////                {
////                    currentInput = outputProperty.GetValue(genericPipelineElement);
////                }
////                else
////                {
////                    throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not have an Output property.");
////                }
////            }
////            else
////            {
////                throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' is not a valid IPipelineElement.");
////            }
////        }

////        // Attempt to cast the final output to the expected TOutput type
////        if (currentInput is TOutput finalOutput)
////        {
////            return finalOutput;
////        }
////        else
////        {
////            throw new InvalidCastException($"Final output type is '{currentInput?.GetType().Name ?? "null"}', but expected type is '{typeof(TOutput).Name}'.");
////        }
////    }
////}


////public class NumericalPipelineExampleWithPipe
////{
////    public static void Main(string[] args)
////    {
////        Console.WriteLine("Running pipeline with double type using Pipe class:");
////        RunPipelineWithPipe<double>();

////        Console.WriteLine("\nRunning pipeline with decimal type using Pipe class:");
////        RunPipelineWithPipe<decimal>();
////    }

////    public static void RunPipelineWithPipe<T>() where T : struct, IConvertible
////    {
////        Console.WriteLine($"\nPipeline type: {typeof(T).Name}");

////        // 3.1. Create Pipeline Elements (same as before)
////        var addElement = new AddElement<T>((T)Convert.ChangeType(5, typeof(T))); // Add 5
////        var multiplyElement = new MultiplyElement<T>((T)Convert.ChangeType(2, typeof(T))); // Multiply by 2
////        var sqrtElement = new SquareRootElement<T>(); // Square root

////        // 3.2. Define Input
////        T inputNumber = (T)Convert.ChangeType(10, typeof(T));
////        Console.WriteLine($"Input: {inputNumber}");

////        // 3.3. Build and Run the Pipeline using Pipe class
////        var pipe = new Pipe()
////            .AddElement(addElement)
////            .AddElement(multiplyElement)
////            .AddElement(sqrtElement);

////        try
////        {
////            T finalResult = pipe.Run<T, T>(inputNumber); // Specify input and output types for Run
////            Console.WriteLine($"Final Output of Pipeline: {finalResult}");
////        }
////        catch (Exception ex)
////        {
////            Console.WriteLine($"Error during pipeline execution: {ex.Message}");
////            if (ex is ArgumentException || ex is InvalidOperationException || ex is InvalidCastException)
////            {
////                // Expected pipeline errors, no need for full stack trace in this example
////            }
////            else
////            {
////                Console.WriteLine(ex); // For other unexpected exceptions, show full details
////            }
////        }
////    }
////}




















////using System;
////using System.Collections.Generic;

////// (Interfaces IPipeInput, IPipeOutput, IPipelineElement and numerical element classes remain the same as in the previous example)
////// ... (Copy interfaces and AddElement, MultiplyElement, SquareRootElement classes from the previous example here)
////// IPipeInput: Interface for elements that accept input of type T
////public interface IPipeInput<in T>
////{
////    void SetInput(T input);
////}

////// IPipeOutput: Interface for elements that produce output of type T
////public interface IPipeOutput<out T>
////{
////    T Output { get; }
////}

////// IPipelineElement: Interface for a generic pipeline element that transforms input of type TIn to output of type TOut
////public interface IPipelineElement<in TIn, out TOut> : IPipeInput<TIn>, IPipeOutput<TOut>
////{
////    void Process(); // Executes the element's operation
////}

////// 2. Implement Concrete Pipeline Elements for Numerical Operations

////// AddElement: Adds a constant value to the input
////public class AddElement<T> : IPipelineElement<T, T> where T : struct, IConvertible
////{
////    private T _input;
////    private T _output;
////    public T ValueToAdd { get; set; }

////    public AddElement(T valueToAdd)
////    {
////        ValueToAdd = valueToAdd;
////    }

////    public void SetInput(T input)
////    {
////        _input = input;
////    }

////    public T Output => _output;

////    public void Process()
////    {
////        // Convert to double for numerical operations (assuming T is convertible to double)
////        double inputValue = Convert.ToDouble(_input);
////        double valueToAddDouble = Convert.ToDouble(ValueToAdd);
////        _output = (T)Convert.ChangeType(inputValue + valueToAddDouble, typeof(T));
////    }
////}

////// MultiplyElement: Multiplies the input by a constant value
////public class MultiplyElement<T> : IPipelineElement<T, T> where T : struct, IConvertible
////{
////    private T _input;
////    private T _output;
////    public T ValueToMultiply { get; set; }

////    public MultiplyElement(T valueToMultiply)
////    {
////        ValueToMultiply = valueToMultiply;
////    }

////    public void SetInput(T input)
////    {
////        _input = input;
////    }

////    public T Output => _output;

////    public void Process()
////    {
////        double inputValue = Convert.ToDouble(_input);
////        double valueToMultiplyDouble = Convert.ToDouble(ValueToMultiply);
////        _output = (T)Convert.ChangeType(inputValue * valueToMultiplyDouble, typeof(T));
////    }
////}

////// SquareRootElement: Calculates the square root of the input
////public class SquareRootElement<T> : IPipelineElement<T, T> where T : struct, IConvertible
////{
////    private T _input;
////    private T _output;

////    public void SetInput(T input)
////    {
////        _input = input;
////    }

////    public T Output => _output;

////    public void Process()
////    {
////        double inputValue = Convert.ToDouble(_input);
////        if (inputValue < 0)
////        {
////            throw new ArgumentOutOfRangeException("Input must be non-negative for square root.");
////        }
////        _output = (T)Convert.ChangeType(Math.Sqrt(inputValue), typeof(T));
////    }
////}

////// 1. Pipe Class Definition
////public class Pipe
////{
////    private readonly List<object> _elements = new List<object>(); // Store elements as objects

////    // 2. AddElement Method (Generic)
////    public Pipe AddElement<TIn, TOut>(IPipelineElement<TIn, TOut> element)
////    {
////        if (_elements.Count > 0)
////        {
////            // Type check: Ensure input type of the new element matches the output type of the last element
////            var lastElement = _elements[_elements.Count - 1];
////            Type lastOutputType = GetOutputType(lastElement);
////            Type currentInputType = typeof(TIn);

////            if (lastOutputType != currentInputType)
////            {
////                throw new ArgumentException($"Type mismatch: Input type of element '{element.GetType().Name}' ({currentInputType.Name}) does not match the output type of the previous element ({lastOutputType.Name}).");
////            }
////        }
////        _elements.Add(element);
////        return this; // For method chaining
////    }

////    // Helper method to get Output type using reflection (less ideal, but works for this example)
////    private static Type GetOutputType(object element)
////    {
////        Type elementType = element.GetType();
////        foreach (Type iface in elementType.GetInterfaces())
////        {
////            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IPipeOutput<>))
////            {
////                // Found IPipeOutput<T>, get the T (generic type argument)
////                return iface.GetGenericArguments()[0];
////            }
////        }
////        throw new ArgumentException($"Could not determine output type of element: {element.GetType().Name}. Ensure it implements IPipeOutput<T> correctly.");
////    }


////    // 3. Run Method (Generic Input)
////    public TOutput Run<TInput, TOutput>(TInput input)
////    {
////        if (_elements.Count == 0)
////        {
////            throw new InvalidOperationException("Pipe is empty. Add elements before running.");
////        }

////        object currentInput = input; // Start with the initial input

////        foreach (var elementObj in _elements)
////        {
////            if (elementObj is IPipeInput<object> pipeInput) // Use object as a fallback for IPipeInput
////            {
////                pipeInput.SetInput(currentInput);
////            }
////            else
////            {
////                throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not implement IPipeInput.");
////            }

////            if (elementObj is IPipelineElement<object, object> pipelineElement) // Use object as fallback for IPipelineElement
////            {
////                pipelineElement.Process();
////                if (elementObj is IPipeOutput<object> pipeOutput) // And IPipeOutput
////                {
////                    currentInput = pipeOutput.Output;
////                }
////                else
////                {
////                    throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not implement IPipeOutput.");
////                }
////            }
////            else if (elementObj is IPipelineElement<object, object> genericPipelineElement) // More robust handling for generic IPipelineElement
////            {
////                // Dynamically invoke Process and Output using reflection to handle generics
////                var processMethod = genericPipelineElement.GetType().GetMethod("Process");
////                if (processMethod != null)
////                {
////                    processMethod.Invoke(genericPipelineElement, null);
////                }
////                else
////                {
////                    throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not have a Process method.");
////                }

////                var outputProperty = genericPipelineElement.GetType().GetProperty("Output");
////                if (outputProperty != null)
////                {
////                    currentInput = outputProperty.GetValue(genericPipelineElement);
////                }
////                else
////                {
////                    throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' does not have an Output property.");
////                }
////            }
////            else
////            {
////                throw new InvalidOperationException($"Element '{elementObj.GetType().Name}' is not a valid IPipelineElement.");
////            }
////        }

////        // Attempt to cast the final output to the expected TOutput type
////        if (currentInput is TOutput finalOutput)
////        {
////            return finalOutput;
////        }
////        else
////        {
////            throw new InvalidCastException($"Final output type is '{currentInput?.GetType().Name ?? "null"}', but expected type is '{typeof(TOutput).Name}'.");
////        }
////    }
////}


////public class NumericalPipelineExampleWithPipe
////{
////    public static void Main(string[] args)
////    {
////        Console.WriteLine("Running pipeline with double type using Pipe class:");
////        RunPipelineWithPipe<double>();

////        Console.WriteLine("\nRunning pipeline with decimal type using Pipe class:");
////        RunPipelineWithPipe<decimal>();
////    }

////    public static void RunPipelineWithPipe<T>() where T : struct, IConvertible
////    {
////        Console.WriteLine($"\nPipeline type: {typeof(T).Name}");

////        // 3.1. Create Pipeline Elements (same as before)
////        var addElement = new AddElement<T>((T)Convert.ChangeType(5, typeof(T))); // Add 5
////        var multiplyElement = new MultiplyElement<T>((T)Convert.ChangeType(2, typeof(T))); // Multiply by 2
////        var sqrtElement = new SquareRootElement<T>(); // Square root

////        // 3.2. Define Input
////        T inputNumber = (T)Convert.ChangeType(10, typeof(T));
////        Console.WriteLine($"Input: {inputNumber}");

////        // 3.3. Build and Run the Pipeline using Pipe class
////        var pipe = new Pipe()
////            .AddElement(addElement)
////            .AddElement(multiplyElement)
////            .AddElement(sqrtElement);

////        try
////        {
////            T finalResult = pipe.Run<T, T>(inputNumber); // Specify input and output types for Run
////            Console.WriteLine($"Final Output of Pipeline: {finalResult}");
////        }
////        catch (Exception ex)
////        {
////            Console.WriteLine($"Error during pipeline execution: {ex.Message}");
////            if (ex is ArgumentException || ex is InvalidOperationException || ex is InvalidCastException)
////            {
////                // Expected pipeline errors, no need for full stack trace in this example
////            }
////            else
////            {
////                Console.WriteLine(ex); // For other unexpected exceptions, show full details
////            }
////        }
////    }
////}