using System;

namespace Truudus
{
    class RandomNumber
    {
        internal static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());

            return s;
        }
    }
}
