using System;
using System.Collections.Generic;
using SerializationTask;
using Xunit;

namespace SerializationTaskTests
{
    public class PosixTimeTests
    {
        [Theory]
        [MemberData(nameof(DatesHigherThanUnixEpoch))]
        public void Parse_WhenDateIsHigherThanUnixEpoch_WorksCorrect(Int64 expectedPosixValue, DateTime dateToParse)
        {
            ParseTest(expectedPosixValue, dateToParse);
        }
        
        [Theory]
        [MemberData(nameof(DatesLowerThanUnixEpoch))]
        public void Parse_WhenDateIsLowerThanUnixEpoch_WorksCorrect(Int64 expectedPosixValue, DateTime dateToParse)
        {
            ParseTest(expectedPosixValue, dateToParse);
        }
        
        [Fact]
        public void Parse_WhenDateIsEqualToUnixEpoch_WorksCorrect()
        {
            ParseTest(0, DateTime.UnixEpoch);
        }
        
        [Theory]
        [MemberData(nameof(DatesHigherThanUnixEpoch))]
        public void ToDateTime_WhenPosixValueIsPositive_WorksCorrect(Int64 posixValue, DateTime expectedDate)
        {
            ToDateTimeTest(posixValue, expectedDate);
        }
        
        [Theory]
        [MemberData(nameof(DatesLowerThanUnixEpoch))]
        public void ToDateTime_WhenPosixValueIsNegative_WorksCorrect(Int64 posixValue, DateTime expectedDate)
        {
            ToDateTimeTest(posixValue, expectedDate);
        }
        
        [Fact]
        public void ToDateTime_WhenPosixValueIsZero_WorksCorrect()
        {
            ToDateTimeTest(0, DateTime.UnixEpoch);
        }

        [Theory]
        [MemberData(nameof(DataForTestYearsBetweenWhenFirstDateIsLowerThanSecondDate))]
        public void YearsBetween_WhenFirstDateIsLowerThanSecondDate_WorksCorrect(DateTime firstDate, Posix secondDate, Int32 expectedYears)
        {
            YearsBetweenTest(firstDate, secondDate, expectedYears);
        }
        
        [Theory]
        [MemberData(nameof(DataForTestYearsBetweenWhenFirstDateIsHigherThanSecondDate))]
        public void YearsBetween_WhenFirstDateIsHigherThanSecondDate_WorksCorrect(DateTime firstDate, Posix secondDate, Int32 expectedYears)
        {
            YearsBetweenTest(firstDate, secondDate, expectedYears);
        }
        
        [Fact]
        public void YearsBetween_WhenFirstDateIsEqualToSecondDate_ReturnZero()
        {
            var date = new DateTime(2022, 1, 1);
            
            YearsBetweenTest(date, Posix.Parse(date), 0);
        }

        [Fact]
        public void RandomDateBetween_WhenFirstDateIsHigherThanSecondDate_WorksCorrect()
        {
            var firstDate = new DateTime(2022, 1, 1);
            var secondDate = new DateTime(2019, 1, 1);
            
            var randomDateBetween = Posix.ToDateTime(Posix.RandomDateBetween(firstDate, secondDate));
            
            Assert.True(randomDateBetween <= firstDate && randomDateBetween >= secondDate);
        }
        
        [Fact]
        public void RandomDateBetween_WhenFirstDateIsLowerThanSecondDate_WorksCorrect()
        {
            var firstDate = new DateTime(2019, 1, 1);
            var secondDate = new DateTime(2022, 1, 1);
            
            
            var randomDateBetween = Posix.ToDateTime(Posix.RandomDateBetween(firstDate, secondDate));
            
            Assert.True(randomDateBetween >= firstDate && randomDateBetween <= secondDate);
        }
        
        [Fact]
        public void RandomDateBetween_WhenFirstDateIsEqualToSecondDate_WorksCorrect()
        {
            var date = new DateTime(2019, 1, 1);
            
            var randomDateBetween = Posix.ToDateTime(Posix.RandomDateBetween(date, date));
            
            Assert.True(randomDateBetween == date);
        }
        
        private static void ParseTest(Int64 expectedPosixValue, DateTime dateToParse)
        {
            var posixTime = Posix.Parse(dateToParse);
            
            Assert.Equal(expectedPosixValue, posixTime.Value);
        }
        
        private static void ToDateTimeTest(Int64 posixValue, DateTime expectedDate)
        {
            var date = Posix.ToDateTime(posixValue);
            
            Assert.Equal(expectedDate, date);
        }
        
        private static void YearsBetweenTest(DateTime firstDate, Posix secondDate, Int32 expectedYears)
        {
            var years = Posix.YearsBetween(firstDate, secondDate);
            
            Assert.Equal(expectedYears, years);
        }
        
        public static IEnumerable<object[]> DatesHigherThanUnixEpoch => 
            new List<object[]>
            {
                new object[] { 1095292800, new DateTime(2004, 9, 16) },
                new object[] { 1071619200, new DateTime(2003, 12, 17) },
                new object[] { 1095357343, new DateTime(2004, 9, 16, 17, 55 , 43) }
            };
        
        public static IEnumerable<object[]> DatesLowerThanUnixEpoch => 
            new List<object[]>
            {
                new object[] { -386380800, new DateTime(1957, 10, 4) },
                new object[] { -2147483648, new DateTime(1901, 12, 13, 20, 45, 52) }
            };
        
        public static IEnumerable<object[]> DataForTestYearsBetweenWhenFirstDateIsLowerThanSecondDate => 
            new List<object[]>
            {
                new object[] { new DateTime(2019, 1, 1), Posix.Parse(new DateTime(2022, 1, 1)), 3 },
                new object[] { new DateTime(2019, 1, 11), Posix.Parse(new DateTime(2022, 1, 1)), 2 },
                new object[] { new DateTime(2018, 12, 21), Posix.Parse(new DateTime(2022, 1, 1)), 3 }
            };
        
        public static IEnumerable<object[]> DataForTestYearsBetweenWhenFirstDateIsHigherThanSecondDate => 
            new List<object[]>
            {
                new object[] { new DateTime(2025, 1, 1), Posix.Parse(new DateTime(2022, 1, 1)), 3 },
                new object[] { new DateTime(2025, 1, 11), Posix.Parse(new DateTime(2022, 1, 1)), 3 },
                new object[] { new DateTime(2024, 12, 21), Posix.Parse(new DateTime(2022, 1, 1)), 2 }
            };
    }
}