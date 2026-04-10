using System;
using System.Collections.Generic;
using System.Text;

namespace EducationCentreManagement
{
    public abstract class Person
    {
        // Encapsulation: Using properties with getters and setters
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Constructor to initialize common data
        public Person(string name, string phone, string email, string role)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Role = role;
        }

        /// Polymorphism: Virtual method to be overridden by derived classes.
        /// Provides a standard way to display information.
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"-----------------------------------------");
            Console.WriteLine($"Role: {Role.ToUpper()}");
            Console.WriteLine($"Name: {Name} | Phone: {Phone} | Email: {Email}");
        }
    }
}
