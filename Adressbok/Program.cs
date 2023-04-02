using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AddressBook
{
    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    class Program
    {
        private static readonly string _filePath = "addressbook.json";

        public static void ShowMenu()
        {
            Console.WriteLine("Välkommen!");
            Console.WriteLine("[1] Lägg till ny kontaktinfo");
            Console.WriteLine("[2] Skriv ut alla kontakter");
            Console.WriteLine("[3] Sök person");
            Console.WriteLine("[4] Ta bort kontaktinfo");
            Console.WriteLine("[5] Avsluta program!");
        }

        static void Main(string[] args)
        {
            List<Person> addressBook = new List<Person>();

            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                addressBook = JsonConvert.DeserializeObject<List<Person>>(json);
            }

            DateTime date = DateTime.Now;
            Console.WriteLine(date);

            bool showingMenu = true;
            while (showingMenu)
            {
                ShowMenu();

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {


                    switch (choice)
                    {
                        case 1:
                            {
                                Console.WriteLine("Ange namn:");
                                string name = Console.ReadLine();

                                Console.WriteLine("Ange email:");
                                string email = Console.ReadLine();

                                Console.WriteLine("Ange telefonnummer:");
                                string phoneNumber = Console.ReadLine();

                                Console.WriteLine("Ange adress:");
                                string address = Console.ReadLine();

                                Person newPerson = new Person { Name = name, Email = email, PhoneNumber = phoneNumber, Address = address };
                                addressBook.Add(newPerson);

                                Console.WriteLine("Kontaktinformation har lagts till!");
                                Console.WriteLine();
                                break;
                            }

                        case 2:
                            {
                                Console.WriteLine("Alla kontakter:");
                                foreach (Person person in addressBook)
                                {
                                    Console.WriteLine($"Namn: {person.Name}\nEmail: {person.Email}\nTelefonnummer: {person.PhoneNumber}\nAdress: {person.Address}\n");
                                }
                                break;
                            }

                        case 3:
                            {
                                Console.WriteLine("Sök efter person:");
                                string searchTerm = Console.ReadLine();

                                List<Person> searchResults = addressBook.FindAll(person => person.Name.Contains(searchTerm));
                                if (searchResults.Count == 0)
                                {
                                    Console.WriteLine($"Kunde inte hitta någon som matchar \"{searchTerm}\"");
                                }
                                else
                                {
                                    Console.WriteLine($"Hittade {searchResults.Count} person(er):");
                                    foreach (Person person in searchResults)
                                    {
                                        Console.WriteLine($"Namn: {person.Name}\nEmail: {person.Email}\nTelefonnummer: {person.PhoneNumber}\nAdress: {person.Address}\n");
                                    }
                                }
                                break;
                            }

                        case 4:
                            {
                                Console.WriteLine("Ange namn på personen du vill ta bort:");
                                string nameToDelete = Console.ReadLine();

                                Person personToDelete = addressBook.Find(person => person.Name == nameToDelete);
                                if (personToDelete != null)
                                {
                                    addressBook.Remove(personToDelete);
                                    Console.WriteLine($"Kontaktinformation för {nameToDelete} har tagits bort.");
                                }
                                else
                                {
                                    Console.WriteLine($"Kunde inte hitta {nameToDelete} i Adressboken.");
                                }
                                break;
                            }

                            case 5:
                                {
                                    Console.WriteLine("Program avslutad, klicka valfri knapp för att stänga!");
                                    Environment.Exit(0);//För att avsluta programmet när användaren väljer val 5.

                                    break;
                                }
                        }

                }

            }

        }
    }
}