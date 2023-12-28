using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// This is a collection of small test functions I created
// for either school assignments or from the book Eloquent JavaScript
// 2020 - 2023

namespace EloquentJavaScriptTests
{
    internal static class Program
    {
        private static string? m_ccType;

        // program's entry point
        private static void Main(string[] args)
        {
            Console.WriteLine("The min value is " + Min(3, 1));

            Console.WriteLine("The number is even " + IsEvenRecursive(2));

            Console.WriteLine("The number is even " + IsEvenModulus(5));

            int[] range = (int[])GetRange(10, 0);

            for (int i = 0; i < range.Length; i++)
                Console.WriteLine("All numbers in the range = " + range[i]);

            Console.WriteLine("Sum of the range = " + Sum(range));

            //CreditCardValidator();

            //Transform matrixes with ChatGPT

            //pixels to mm
            double pixelsPerMicron = 3;

            double[,] transformationMatrix = GetTransformationMatrix(pixelsPerMicron);

            Console.WriteLine("Transformation Matrix:");
            PrintMatrix(transformationMatrix);

            //Transform data and outputs
            double x_offset = 2;
            double y_offset = 3;
            double theta_offset = 45; // in degrees

            double[] inputPoint = { 1, 1 };

            //X, Y and T offsets.
            // Get the transformation matrix
            double[,] matrix = GetTransformationMatrix(x_offset, y_offset, theta_offset);

            // Apply the transformation to the input point
            double[] outputPoint = ApplyTransformation(inputPoint, matrix);

            Console.WriteLine($"Input Point: [{string.Join(", ", inputPoint)}]");
            Console.WriteLine($"Output Point: [{string.Join(", ", outputPoint)}]");
        }

        //finds minimum between two numbers
        public static int Min(int n1, int n2)
        {
            if (n1 < n2)
                return n1;
            else
                return n2;
        }

        //recursive call to find an even number
        public static bool IsEvenRecursive(int n)
        {
            if (n == 0)
                return true;

            if (n < 0 || n == 1)
                return false;
            else
                return IsEvenRecursive(n - 2);
        }

        //check if number is even with modulous
        public static bool IsEvenModulus(int n)
        {
            if (n % 2 == 0)
                return true;

            return false;
        }

        //Get a range from two numbers
        public static object GetRange(int start, int end)
        {
            int step = start < end ? 1 : -1;

            //var array = new int[80];

            int[] array = Array.Empty<int>();

            int arraySize = start - end;

            //turn it to positive
            if (arraySize < 0)
                arraySize *= -1;

            Array.Resize(ref array, arraySize + 1);

            if (step < 0)
            {
                for (int i = arraySize; i >= 0; i += step)
                {
                    array[i] = start - i;
                }
            }
            else
            {
                for (int i = 0; i <= arraySize; i += step)
                {
                    array[i] = start + i;
                }
            }

            return array;
        }

        public static int Sum(int[] array)
        {
            var total = 0;
            foreach (int value in array)
            {
                total += value;
            }
            return total;
        }

        //cc validator
        public static void CreditCardValidator()
        {
            do
            {
                Console.Write("Please enter a valid CC number ");
                string ccNum = Console.ReadLine();

                if (ccNum == null)
                    return;

                VerifyCCNum(ccNum);
            } while (true);
        }

        //check the cc numbers valid
        private static void VerifyCCNum(string cc)
        {
            if (CCNumValid(cc))
            {
                Console.Write("CC Number is valid. Type = ");
                Console.WriteLine(m_ccType);
            }
            else
            {
                Console.Write("CC Number is not valid. Type = ");
            }

            Console.WriteLine();
        }

        //checks to see if CC is valid or not
        private static bool CCNumValid(string cc)
        {
            //remove any periods at the end
            string trimmedCC = cc.Trim(new Char[] { '.' });

            //get rid of the dashes
            trimmedCC = Regex.Replace(trimmedCC, @"-", string.Empty);

            //remove all white spaces
            trimmedCC = Regex.Replace(trimmedCC, @"\s", string.Empty);

            //remove any left over spaces at begining and end
            trimmedCC = trimmedCC.Trim();

            //if length isn't right or something isn't a digit reject it
            if (trimmedCC.Length != 16 || !IsDigitsOnly(trimmedCC))
                return false;

            if (!CheckSum(trimmedCC))
                return false;

            string firstTwoNums = trimmedCC.Substring(0, 2);

            if (firstTwoNums.Substring(0, 1) == "4")
                firstTwoNums = "4";

            return CCValid(firstTwoNums); ;
        }

        public static bool CheckSum(string cc)
        {
            int ccLen = cc.Length / 2;
            int[] ccNum = new int[ccLen];
            int[] ccNum1 = new int[ccLen];
            //int[] ccNum2 = new int[ccLen / 2];

            int step = 2;
            int cnt = 0;
            int checksum = 0;

            for (int i = 0; i < ccLen; i++)
            {
                ccNum[i] = Int32.Parse(cc[cnt].ToString()) * 2;
                ccNum1[i] = Int32.Parse(cc[cnt + 1].ToString());

                //Console.WriteLine("ccNum values " + ccNum[i]);
                //Console.WriteLine("ccNum1 values " + ccNum1[i]);

                if (ccNum[i] >= 10)
                {
                    int num1 = ccNum[i] / 10;
                    int num2 = ccNum[i] % 10;
                    ccNum[i] = num1 + num2;
                    //Console.WriteLine("ccNum if >= 10 added values " + ccNum[i]);
                }

                checksum += ccNum[i] + ccNum1[i];
                //Console.WriteLine("checksum value " + checksum);
                //Console.WriteLine();

                cnt += 2;
            }

            return checksum % 10 == 0;
        }

        //sets CC Type or Invalid
        private static bool CCValid(string ccNums)
        {
            //check valid

            switch (ccNums)
            {
                case "34":
                case "37":

                    m_ccType = "AMEX";
                    return true;

                case "4":

                    m_ccType = "VISA";
                    return true;

                case "51":
                case "52":
                case "53":
                case "54":
                case "55":
                    m_ccType = "MASTERCARD";
                    return true;

                default:
                    m_ccType = "INVALID";
                    return false;
            }
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// /////////////////////////////////////////// Transform algorythms from ChatGPT //////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="pixelsPerMicron"></param>
        /// <returns></returns>

        //pixels to mm
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