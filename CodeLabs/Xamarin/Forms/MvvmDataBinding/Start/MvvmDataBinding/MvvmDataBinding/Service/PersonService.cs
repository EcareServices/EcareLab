using System;
using System.Collections.Generic;
using MvvmDataBinding.Model;

namespace MvvmDataBinding.Service
{
    public class PersonService {
        private static readonly string[] FirstNames = {
            "Ciske", "Maloe", "Romeo", "Anne-Marie", "Najoua", "Ino", "Harman",
            "Fabianne", "Claudia", "Nouria", "Raymond", "Valerie", "Zander"
        };
        private static readonly string[] LastNames = {
            "van Luttikhuizen", "Vlemmings", "Knoester", "Lohman", "Ritsma", "Leijnse", "van Wijngaarden",
            "Koeslag", "Bakermans", "Haagsma", "Hereijgers", "Vromen", "van Buul"
        };


        public static IEnumerable<Person> Get(int take)
        {
            var random  = new Random();

            for (var i = 0; i < take; i++) {
                var person = new Person {
                    FullName =
                        $"{FirstNames[random.Next(FirstNames.Length)]} {LastNames[random.Next(FirstNames.Length)]}"
                };

                person.Email = person.FullName.Replace(" ", "") + "@gmail.com";
                person.Phone = $"06-{random.Next(1000000, 9999999)}";
                person.Id = Guid.NewGuid();

                yield return person;
            }
        }

    }
}
