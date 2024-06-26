public static class Bubblicious
{
    /// <summary>
    /// A "Bubblicious Number" is an integer that is prime and whose
    /// hexadecimal representation has a last digit of B.
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>If the number is Bubblicious.</returns>
    public static bool IsBubblicious(int number)
    {
        const uint SUFFIX = 0x0B;
        return ((uint)number & SUFFIX) == SUFFIX && IsPrime(number);
    }

    private static bool IsEven(int number) => (number & 1) != 1;

    private static bool IsPrime(int n)
    {
        if (n == 2 || n == 3)
            return true;

        if (n <= 1 || IsEven(n))
            return false;

        if (n % 3 == 0)
            return false;

        for (int i = 5; i <= Math.Sqrt(n); i = i + 6)
        {
            if (n % i == 0 || n % (i + 2) == 0)
            {
                return false;
            }
        }

        return true;
    }
}
