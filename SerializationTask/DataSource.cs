using System;
using System.Text;
using SerializationTask.Models;

namespace SerializationTask
{
    static class DataSource
    {
        private static readonly Random _random = new();

        private static readonly string[] _maleFirstNames = { "Dmitry", "Petr", "Alexander", "Alexey", "Maxim", "Pavel", "Vladimir" };
        
        private static readonly string[] _femaleFirstNames = { "Anna", "Victoria", "Kristina", "Olga", "Polina", "Maria", "Arina" };
        
        private static readonly string[] _lastNames = { "Pavlov", "Ivanov", "Petrov", "Zhidkov", "Slobodin", "Koksharov", "Pechkin" };

        private static Int32 _currentId = 1;
        
        public static Int32 NextId()
        {
            return _currentId++;
        }
        
        public static string NextFirstName(Gender gender)
        {
            return gender == Gender.Male
                ? NextMaleFirstName()
                : NextFemaleFirstName();
        }
        
        public static string NextLastName(Gender gender)
        {
            var lastName = NextLastName();
            
            return gender == Gender.Male
                ? lastName
                : $"{lastName}a";
        }
        
        public static string NextChildLastName(Person parent, Child child)
        {
            if (parent.Gender != child.Gender)
            {
                return parent.Gender == Gender.Male 
                    ? $"{parent.LastName}a" 
                    : parent.LastName.Substring(0, parent.LastName.Length - 1);
            }
            
            return parent.LastName;
        }

        public static Int32 NextSequenceId()
        {
            return _random.Next(1, 101);
        }
        
        public static string[] NextCreditCardNumbers()
        {
            var creditCardNumbers = new string[_random.Next(1, 4)];
            
            for (var i = 0; i < creditCardNumbers.Length; i++)
            {
                creditCardNumbers[i] = NextCreditCardNumber();
            }
            
            return creditCardNumbers;
        }

        public static string[] NextPhoneNumbers()
        {
            var phoneNumbers = new string[_random.Next(1, 3)];
            
            for (var i = 0; i < phoneNumbers.Length; i++)
            {
                phoneNumbers[i] = NextPhoneNumber();
            }
            
            return phoneNumbers;
        }
        
        public static Int64 NextPersonBirthdate()
        {
            var dateNow = DateTime.UtcNow;
            var dateFrom = dateNow.Subtract(TimeSpan.FromDays(365 * 100));
            var dateTo = dateNow.Subtract(TimeSpan.FromDays(365 * 19));
            
            return Posix.RandomDateBetween(dateFrom, dateTo).Value;
        }
        
        public static Int64 NextChildBirthdate(Int64 parentBirthdate)
        {
            var dateFrom = Posix
                .ToDateTime(parentBirthdate)
                .Add(TimeSpan.FromDays(365 * 18));
            
            return Posix.RandomDateBetween(dateFrom, DateTime.UtcNow).Value;
        }

        public static double NextSalary()
        {
            return Math.Round(_random.NextDouble() * 10000, 2);
        }
        
        public static bool NextMarred()
        {
            return _random.Next(2) == 0;
        }
        
        public static Gender NextGender()
        {
            return _random.Next(2) == 0
                ? Gender.Male
                : Gender.Female;
        }
        
        private static string NextMaleFirstName()
        {
            return _maleFirstNames[_random.Next(0, _maleFirstNames.Length)];
        }

        private static string NextFemaleFirstName()
        {
            return _femaleFirstNames[_random.Next(0, _femaleFirstNames.Length)];
        }

        private static string NextLastName()
        {
            return _lastNames[_random.Next(0, _lastNames.Length)];
        }
        
        private static string NextCreditCardNumber()
        {
            return new StringBuilder()
                .AppendRandomNumbers(16)
                .ToString();
        }

        private static string NextPhoneNumber()
        {
            return new StringBuilder("+79")
                .AppendRandomNumbers(9)
                .ToString();
        }
    }
}