﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;

using System;
using System.Collections.Generic;
using System.Drawing.Imaging; // Required using
using System.IO; // Required usingusing System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics.Metrics;
using piedPiper.pipeline;

namespace piedPiper.implementacje.obrazy
{
    /// <summary>
    /// Saves a Bitmap object to a file with a specified extension and base name.
    /// Generates a unique filename using the context's UniqueToken.
    /// Input: Bitmap
    /// Output: string (full path of the saved file)
    /// </summary>
    public class BitmapSaveProcessor : IProcessor<Bitmap, string>
    {
        private readonly string _outputDirectory;
        private readonly string _baseFilename;
        private readonly string _extension; // e.g., ".png", ".jpg"
        private readonly ImageFormat _imageFormat;
        private static int _counter = 0; // Simple counter for unique filenames

        /// <summary>
        /// Initializes the processor.
        /// </summary>
        /// <param name="outputDirectory">The directory to save the image in.</param>
        /// <param name="baseFilename">Base name for the file (unique ID and extension will be added).</param>
        /// <param name="extension">The desired file extension (e.g., ".png", ".jpg", ".bmp"). Determines the save format.</param>
        public BitmapSaveProcessor(string outputDirectory, string baseFilename, string extension)
        {
            if (string.IsNullOrWhiteSpace(outputDirectory))
                throw new ArgumentNullException(nameof(outputDirectory));
            if (string.IsNullOrWhiteSpace(baseFilename))
                throw new ArgumentNullException(nameof(baseFilename));
            if (string.IsNullOrWhiteSpace(extension) || !extension.StartsWith("."))
                throw new ArgumentException("Extension must start with a dot (e.g., '.png').", nameof(extension));

            _outputDirectory = outputDirectory;
            _baseFilename = baseFilename;
            _extension = extension.ToLowerInvariant(); // Normalize extension

            // Determine ImageFormat based on extension
            _imageFormat = GetImageFormatFromExtension(_extension);
        }

        private ImageFormat GetImageFormatFromExtension(string ext)
        {
            switch (ext)
            {
                case ".png": return ImageFormat.Png;
                case ".jpg":
                case ".jpeg": return ImageFormat.Jpeg;
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".tif":
                case ".tiff": return ImageFormat.Tiff;
                default:
                    Console.WriteLine($"Warning: Unknown extension '{ext}'. Defaulting to PNG format."); // Log warning
                    return ImageFormat.Png; // Default to PNG for unknown formats
            }
        }

        
    public string Process(System.Drawing.Bitmap bitmap, Context context)
        {
            // Use a unique name for each save operation based on context token and a counter
            string fileName = $"{_baseFilename}_{context.UniqueToken.ToString().Substring(0, 4)}_{Interlocked.Increment(ref _counter)}{_extension}";
            string fullPath = Path.Combine(_outputDirectory, fileName);

            context.Log($"BitmapSaveProcessor: Saving image to '{fullPath}'.");
            try
            {
                bitmap.Save(fullPath);
                context.Log($"BitmapSaveProcessor: Image saved successfully.");
                return fullPath; // Return the path to the saved image
            }
            catch (Exception ex)
            {
                context.Log($"BitmapSaveProcessor: Failed to save image: {ex.Message}");
                throw;
            }
        }

        // OPTIONAL: If you want to specify a name explicitly each time
        public string Process(System.Drawing.Bitmap bitmap, Context context, string specificFileName)
        {
            string fullPath = Path.Combine(_outputDirectory, specificFileName);
            context.Log($"BitmapSaveProcessor: Saving image to '{fullPath}'.");
            try
            {
                bitmap.Save(fullPath);
                context.Log($"BitmapSaveProcessor: Image saved successfully.");
                return fullPath; // Return the path to the saved image
            }
            catch (Exception ex)
            {
                context.Log($"BitmapSaveProcessor: Failed to save image: {ex.Message}");
                throw;
            }
        }
    }
}







