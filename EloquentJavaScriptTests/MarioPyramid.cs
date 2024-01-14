using System.Runtime.Intrinsics.X86;

namespace EloquentJavaScriptTests
{
    public static class MarioPyramid
    {
        public static void BuildPyramid()
        {
            Console.Write("Please Enter Height: ");
            int hgt = Convert.ToInt32(Console.ReadLine());

            if (hgt <= 0)
                return;

            int spcCnt = hgt;

            hgt *= 2;

            for (string sym = "#"; sym.Length <= hgt; sym += "#")
            {
                for (spc = " "; spc.Length <= spcCnt; spc += " ") ;

                Console.WriteLine(spc + sym);

                sym += "#";
                spc = "";
                spcCnt -= 1;
            }
        }
    }
}