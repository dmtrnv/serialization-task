using System;
using System.Text;

namespace SerializationTask
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendRandomNumbers(this StringBuilder sb, int numbersCount)
        {
            var random = new Random();
            
            for (var i = 0; i < numbersCount; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            
            return sb;
        }
    }
}