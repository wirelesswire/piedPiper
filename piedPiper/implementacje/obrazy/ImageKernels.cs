using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piedPiper.implementacje.obrazy
{
    public  class ImageKernels
    {
        public static readonly float[,] Gaussian3x3 = new float[,]
        {
            { 1, 2, 1 },
            { 2, 4, 2 },
            { 1, 2, 1 }
        };
        public static readonly float Gaussian3x3Divisor = 16f;

        public static readonly float[,] SobelX = new float[,]
        {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

        public static readonly float[,] SobelY = new float[,]
        {
            { -1, -2, -1 },
            {  0,  0,  0 },
            {  1,  2,  1 }
        };

        public static readonly float[,] SharpenKernel = new float[,]
        {
            { 0, -1, 0 },
            { -1, 5, -1 },
            { 0, -1, 0 }
        };

    }
}
