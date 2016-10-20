// This code is wrong, cause it was not allowed to use BigInteger. Not confirmed but seems to be the problem

//using System;
//using System.Linq;
//using System.Numerics;

//public class FactorialTrailingZeros : IFactorialTrailingZeros
//{
//    public new static IFactorialTrailingZeros GetInstance()
//    {
//         factorialInstance = new FactorialTrailingZeros();

//         return factorialInstance;
//    }

//    public override int CalculateCount(int intNumber, int b)
//    {
//        BigInteger number = CalculateFactorial(intNumber);

//        var inString = DecimalToArbitrarySystem(number, b);

//        int count = CountLastZeros(inString);

//        return count;
//    }

//    private int CountLastZeros(string inString)
//    {
//        var reversed = new string(inString.ToCharArray().Reverse().ToArray());

//        int sum = 0;

//        foreach (var zeroOrNot in reversed)
//        {
//            if (zeroOrNot != '0')
//            {
//                break;
//            }

//            sum++;
//        }

//        return sum;
//    }

//    public BigInteger CalculateFactorial(int intNumber)
//    {
//        if (intNumber == 0 || intNumber == 1)
//        {
//            return 1;
//        }

//        BigInteger result = 1;

//        // This '<=' below is crucial
//        for (int i = 2; i <= intNumber; i++)
//        {
//            result *= i;
//        }

//        return result;
//    }

//    /// <summary>
//    /// Copy Pasted from http://stackoverflow.com/questions/923771/quickest-way-to-convert-a-base-10-number-to-any-base-in-net/10981113#10981113
//    /// and changed to BigDecimal
//    /// </summary>
//    /// <returns></returns>
//    public static string DecimalToArbitrarySystem(BigInteger currentNumber, int radix)
//    {
//        const int bitsInLong = 64;
//        const string digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

//        if (radix < 2 || radix > digits.Length)
//            throw new ArgumentException("The radix must be >= 2 and <= " + digits.Length.ToString());

//        if (currentNumber == 0)
//            return "0";

//        int index = bitsInLong - 1;
//        char[] charArray = new char[bitsInLong];

//        while (currentNumber != 0)
//        {
//            int remainder = (int)(currentNumber % radix);
//            charArray[index--] = digits[remainder];
//            currentNumber = currentNumber / radix;
//        }

//        string result = new string(charArray, index + 1, bitsInLong - index - 1);
//        if (currentNumber < 0)
//        {
//            result = "-" + result;
//        }

//        return result;
//    }
//}