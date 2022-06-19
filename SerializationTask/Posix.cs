using System;

namespace SerializationTask
{
    public readonly struct Posix
    {
        public Int64 Value { get; }

        public Posix(Int64 posixValue)
        {
            Value = posixValue;
        }
        
        public static Posix Parse(DateTime date)
        {
            return new Posix((Int64)date.Subtract(DateTime.UnixEpoch).TotalSeconds);
        }
        
        public static DateTime ToDateTime(Int64 posixValue)
        {
            return DateTime.UnixEpoch.AddSeconds(posixValue);
        }
        
        public static DateTime ToDateTime(Posix posix)
        {
            return DateTime.UnixEpoch.AddSeconds(posix.Value);
        }

        public static Int32 YearsBetween(DateTime firstDate, Posix secondDate)
        {
            var secondDateTime = ToDateTime(secondDate);
            
            return Math.Abs((Int32)firstDate.Subtract(secondDateTime).TotalDays / 365);
        }
        
        public static Int32 YearsBetween(DateTime firstDate, Int64 posixValue)
        {
            return YearsBetween(firstDate, new Posix(posixValue));
        }
        
        public static Posix RandomDateBetween(DateTime firstDate, DateTime secondDate)
        {
            if (firstDate > secondDate)
            {
                (secondDate, firstDate) = (firstDate, secondDate);
            }
            
            var posixFromValue = Parse(firstDate).Value;
            var posixToValue = Parse(secondDate).Value;
            
            var posixValueRange  = Math.Abs(posixToValue - posixFromValue); 
            var maxRandomValueToAdd = posixValueRange > Int32.MaxValue
                ? Int32.MaxValue
                : (int)posixValueRange;
                
            var random = new Random();
            
            return new Posix(posixFromValue + random.Next(maxRandomValueToAdd));
        }
    }
}