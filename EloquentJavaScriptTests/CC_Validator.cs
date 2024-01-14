using System.Text.RegularExpressions;

namespace EloquentJavaScriptTests
{
    public static class CCValidator
    {
        private static string? m_ccType;

        //cc validator
        public static void CreditCardValidator()
        {
            Console.WriteLine("Validating a cc number");
            do
            {
                Console.Write("Please enter a valid CC number or E/e to End ");
                string ccNum = Console.ReadLine();

                if (ccNum == null || ccNum == "E" || ccNum == "e")
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

        private static bool CheckSum(string cc)
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

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}