using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SerializationTask.Models;

namespace SerializationTask
{
    class Program
    {
        static void Main()
        {
            var persons = PersonFactory.CreateList(10000);
            
            var serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var personsAsJson = JsonSerializer.Serialize(persons, serializerOptions);
            
            var filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\\Persons.json";
            File.WriteAllText(filePath, personsAsJson);
            
            persons.Clear();
            
            persons.AddRange(JsonSerializer.Deserialize<IEnumerable<Person>>(File.ReadAllText(filePath), serializerOptions)!);
            
            var personsCreditCardCount = persons.Sum(p => p.CreditCardNumbers.Length);
            var averageChildAge = Math.Round(persons
                .SelectMany(p => p.Children
                    .Select(c => Posix.YearsBetween(DateTime.UtcNow, c.BirthDate)))
                .DefaultIfEmpty()
                .Average(), 2);

            Console.WriteLine($"Persons count: {persons.Count}");
            Console.WriteLine($"Persons credit card count: {personsCreditCardCount}");
            Console.WriteLine($"Average value of child age: {averageChildAge}");
        }
    }
}