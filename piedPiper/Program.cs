using System;
using System.Collections.Generic;

public struct TransformationPipeline
{
    private List<Func<object, object>> _transformations = new List<Func<object, object>>();

    public TransformationPipeline(List<Func<object, object>> transformations = null)
    {
        _transformations = transformations ?? new List<Func<object, object>>();
    }

    /// <summary>
    /// Adds a transformation function to the pipeline.
    /// </summary>
    /// <param name="transformation">A function that takes an object and returns another object.</param>
    public void AddTransformation(Func<object, object> transformation)
    {
        if (transformation == null)
        {
            throw new ArgumentNullException(nameof(transformation), "Transformation function cannot be null.");
        }
        _transformations.Add(transformation);
    }

    /// <summary>
    /// Processes an input object through the pipeline of transformations.
    /// </summary>
    /// <param name="input">The object to be processed.</param>
    /// <returns>The object after being processed by all transformations in the pipeline.</returns>
    public object Process(object input)
    {
        object currentObject = input;
        foreach (var transformation in _transformations)
        {
            currentObject = transformation(currentObject);
        }
        return currentObject;
    }

    /// <summary>
    /// Gets the number of transformations in the pipeline.
    /// </summary>
    public int Count => _transformations.Count;

    /// <summary>
    /// Clears all transformations from the pipeline.
    /// </summary>
    public void Clear()
    {
        _transformations.Clear();
    }

    /// <summary>
    /// Gets the transformation at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the transformation to get.</param>
    /// <returns>The transformation at the specified index.</returns>
    public Func<object, object> this[int index]
    {
        get
        {
            if (index < 0 || index >= _transformations.Count)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            return _transformations[index];
        }
    }
}

// Example Usage:
public class Example
{
    public static void Main(string[] args)
    {
        TransformationPipeline pipeline = new TransformationPipeline(new List<Func<object, object>>() {obj =>
        {
            if (obj == null) return null;
            return obj.ToString().ToUpper();
        } });



        // Transformation 1: Convert to string and uppercase
        //pipeline.AddTransformation(o);

        // Transformation 2: Add a prefix
        pipeline.AddTransformation(obj =>
        {
            if (obj == null) return null;
            return "Processed: " + obj;
        });

        // Transformation 3:  Reverse the string (if it's a string)
        pipeline.AddTransformation(obj =>
        {
            if (obj is string str)
            {
                char[] charArray = str.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }
            return obj; // If not a string, return as is
        });

        object inputData = 123;
        object result = pipeline.Process(inputData);
        Console.WriteLine($"Input: {inputData}, Output: {result}"); // Output: Input: 123, Output: :DESSESCORP :321

        inputData = "hello world";
        result = pipeline.Process(inputData);
        Console.WriteLine($"Input: {inputData}, Output: {result}"); // Output: Input: hello world, Output: dlrow olleH :DESSESCORP :DLROW OLLEH

        // You can access transformations by index
        Console.WriteLine($"Transformation at index 0: {pipeline[0].Method.Name}");

        // Clear the pipeline
        pipeline.Clear();
        Console.WriteLine($"Transformation count after clear: {pipeline.Count}"); // Output: Transformation count after clear: 0
    }
}
