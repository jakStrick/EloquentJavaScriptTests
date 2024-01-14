// This is a collection of small functions I created
// for either school assignments or from the book Eloquent JavaScript
// 2020 - 2023

namespace EloquentJavaScriptTests
{
    internal static class Program
    {
        // program's entry point
        private static void Main(string[] args)
        {
            CCValidator.CreditCardValidator();

            Console.WriteLine("The min value is " + Maths.Min(3, 1));

            Console.WriteLine("The number is even " + Maths.IsEvenRecursive(2));

            Console.WriteLine("The number is even " + Maths.IsEvenModulus(5));

            int[] range = (int[])Maths.GetRange(10, 0);

            for (int i = 0; i < range.Length; i++)
                Console.WriteLine("All numbers in the range = " + range[i]);

            Console.WriteLine("Sum of the range = " + Maths.Sum(range));

            //Transform matrixes with ChatGPT
            PixTo_mm();
            Transform_XYT_Offsets();
        }

        /// <summary>
        /// /////////////////////////////////////////// Transform algorythms from ChatGPT //////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="pixelsPerMicron"></param>
        /// <returns></returns>

        //pixels to mm
        private static void PixTo_mm()
        {
            double pixelsPerMicron = 3;

            double[,] transformationMatrix = GetTransformationMatrix(pixelsPerMicron);

            Console.WriteLine("Transformation Matrix:");
            PrintMatrix(transformationMatrix);
        }

        private static void Transform_XYT_Offsets()
        {
            //X, Y and T offsets.
            // Get the transformation matrix
            //Transform data and outputs
            double x_offset = 2;
            double y_offset = 3;
            double theta_offset = 45; // in degrees

            double[] inputPoint = { 1, 1 };

            double[,] matrix = GetTransformationMatrix(x_offset, y_offset, theta_offset);

            // Apply the transformation to the input point
            double[] outputPoint = ApplyTransformation(inputPoint, matrix);

            Console.WriteLine($"Input Point: [{string.Join(", ", inputPoint)}]");
            Console.WriteLine($"Output Point: [{string.Join(", ", outputPoint)}]");
        }

        private static double[,] GetTransformationMatrix(double pixelsPerMicron)
        {
            double scalingFactor = 1.0 / pixelsPerMicron;
            double rotationAngle = 0.01; // 10 milliradians

            double cosTheta = Math.Cos(rotationAngle);
            double sinTheta = Math.Sin(rotationAngle);

            double[,] matrix = {
            { scalingFactor * cosTheta, -scalingFactor * sinTheta, 0 },
            { scalingFactor * sinTheta, scalingFactor * cosTheta, 0 },
            { 0, 0, 1 }
        };

            return matrix;
        }

        private static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j],8:F6} ");
                }
                Console.WriteLine();
            }
        }

        //X, Y and T offsets
        private static double[,] GetTransformationMatrix(double x_offset, double y_offset, double theta_offset)
        {
            double theta = Math.PI * theta_offset / 180.0;
            double cos_theta = Math.Cos(theta);
            double sin_theta = Math.Sin(theta);

            double[,] matrix = {
            { cos_theta, -sin_theta, x_offset },
            { sin_theta, cos_theta, y_offset },
            { 0, 0, 1 }
        };

            return matrix;
        }

        private static double[] ApplyTransformation(double[] inputPoint, double[,] matrix)
        {
            double[] inputPointHomogeneous = { inputPoint[0], inputPoint[1], 1 };

            double[] outputPointHomogeneous = new double[3];

            // Perform the matrix multiplication
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    outputPointHomogeneous[i] += matrix[i, j] * inputPointHomogeneous[j];
                }
            }

            // Convert back to Cartesian coordinates
            double[] outputPoint = { outputPointHomogeneous[0], outputPointHomogeneous[1] };

            return outputPoint;
        }
    }
}