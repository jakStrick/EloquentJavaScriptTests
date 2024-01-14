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

    /// <summary>
    /// /////////////////////////////////////////// Transform algorythms from ChatGPT //////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="pixelsPerMicron"></param>
    /// <returns></returns>

    //pixels to mm
    public static void PixTo_mm()
    {
        double pixelsPerMicron = 3;

        double[,] transformationMatrix = GetTransformationMatrix(pixelsPerMicron);

        Console.WriteLine("Transformation Matrix:");
        PrintMatrix(transformationMatrix);
    }

    public static void Transform_XYT_Offsets()
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