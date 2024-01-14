//////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////
// This is a collection of small functions I created
// for either school assignments or from the book Eloquent JavaScript but in c#
// 2020 - 2023
//

namespace EloquentJavaScriptTests
{
    internal static class Program
    {
        // program's entry point
        private static void Main(string[] args)
        {
            //Enter a cc number and see if it's valid
            CCValidator.CreditCardValidator();

            Console.WriteLine("The min value is " + Maths.Min(3, 1));

            Console.WriteLine("The number is even " + Maths.IsEvenRecursive(2));

            Console.WriteLine("The number is even " + Maths.IsEvenModulus(5));

            int[] range = (int[])Maths.GetRange(10, 0);

            for (int i = 0; i < range.Length; i++)
                Console.WriteLine("All numbers in the range = " + range[i]);

            Console.WriteLine("Sum of the range = " + Maths.Sum(range));

            //Transform matrixes with ChatGPT
            Maths.PixTo_mm();
            Maths.Transform_XYT_Offsets();

            MarioPyramid.BuildPyramid();
        }
    }
}