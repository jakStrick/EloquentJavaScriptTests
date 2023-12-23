// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;

namespace EloquentJavaScriptTests
{
    internal static class Program
    {
        private static string m_ccType;

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

            CreditCardChecker();
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

        public static bool IsEvenModulus(int n)
        {
            if (n % 2 == 0)
                return true;

            return false;
        }

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

        //cc checker

        public static void CreditCardChecker()
        {
            do
            {
                Console.Write("Please enter a valid CC number ");
                string ccNum = Console.ReadLine();

                if (ccNum == null)
                    return;

                ValidateCCNum(ccNum);
                Console.WriteLine(m_ccType);
                Console.WriteLine();
            } while (true);
        }

        //let's get started checking the cc numbers
        private static void ValidateCCNum(string cc)
        {
            ChkFraudulent(cc);
        }

        //checks to see if CC is valid or not
        private static void ChkFraudulent(string cc)
        {
            int s = cc.Length;
            long[] n = new long[s];
            long[] firstSet = new long[s / 2];

            for (int i = 0; i < s; i++)
            {
                n[i] = long.Parse(cc.Substring(i, 1)); //fill the array with the cc numbers
            }

            int cnt = 0;
            int len = 0;
            string stemp = "";
            int itemp = 0;

            for (int i = s - 2; i >= 0; i -= 2)
            {
                if (i > 0)
                {
                    firstSet[cnt] = n[i] * 2;
                    len = firstSet[cnt].ToString().Length;

                    if (len > 1)
                    {
                        stemp = firstSet[cnt].ToString();
                        itemp += (int)n[i];

                        for (int j = 0; j < len; j++)
                        {
                            itemp += int.Parse(stemp.Substring(j, 1));
                        }
                        len = 0;
                    }
                    else
                    {
                        itemp += (int)firstSet[cnt];
                        itemp += (int)n[i];
                    }
                }
                else
                {
                    if (i == 0)
                        itemp += (int)n[i];
                    //Console.WriteLine("Value of itemp last one - " + itemp);
                    ///Console.WriteLine("Value of n[" + n[cnt] + "] i = 0");
                }

                cnt++;
            }

            stemp = itemp.ToString();
            GetCCType(cc, stemp, n);
        }

        //sets CC Type or Invalid
        private static void GetCCType(string a, string t, long[] n)
        {
            int size = a.Length;
            int len = t.Length;

            if (len > 1)
                t = t.Substring(len - 1);

            bool validSize = size >= 13 && size <= 16;

            //Console.WriteLine("Value of t inside GetCCType - " + t);

            if (t == "0" && validSize)
            {
                string cc = n[0].ToString();

                if (n[0] == 3 || n[0] == 5)
                    cc += n[1].ToString();

                //check valid size

                switch (cc)
                {
                    case "34":
                    case "37":

                        m_ccType = "AMEX";
                        break;

                    case "4":

                        m_ccType = "VISA";
                        break;

                    case "51":
                    case "52":
                    case "53":
                    case "54":
                    case "55":
                        m_ccType = "MASTERCARD";
                        break;

                    default:
                        m_ccType = "INVALID";
                        break;
                }
            }
            else
            {
                m_ccType = "INVALID";
            }
        }
    }
}