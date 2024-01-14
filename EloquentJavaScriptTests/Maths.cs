public static class Maths
{
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
}