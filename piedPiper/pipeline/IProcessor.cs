﻿namespace piedPiper.pipeline
{
    // --- 2. Processor Interface (Nested) ---
    public interface IProcessor<InputType, OutputType>
    {
        OutputType Process(InputType input, Context context);
    }
}