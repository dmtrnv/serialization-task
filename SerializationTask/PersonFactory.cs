using System;
using System.Collections.Generic;
using SerializationTask.Models;

namespace SerializationTask
{
    static class PersonFactory
    {
        private static readonly Random _random = new();
        
        public static Person Create()
        {
            var person = new Person
            {
                Id = DataSource.NextId(),
                TransportId = Guid.NewGuid(),
                SequenceId = DataSource.NextSequenceId(),
                CreditCardNumbers = DataSource.NextCreditCardNumbers(),
                Phones = DataSource.NextPhoneNumbers(),
                BirthDate = DataSource.NextPersonBirthdate(),
                Salary = DataSource.NextSalary(),
                IsMarred = DataSource.NextMarred(),
                Gender = DataSource.NextGender()
            };
            
            person.FirstName = DataSource.NextFirstName(person.Gender);
            person.LastName = DataSource.NextLastName(person.Gender);
            person.Age = Posix.YearsBetween(DateTime.UtcNow, person.BirthDate);
            person.Children = ChildFactory.CreateArray(person, _random.Next(6));

            return person;
        }
        
        public static List<Person> CreateList(int capacity)
        {
            var persons = new List<Person>(capacity);
            
            for (var i = 0; i < persons.Capacity; i++)
            {
                persons.Add(Create());
            }
            
            return persons;
        }
    }
}