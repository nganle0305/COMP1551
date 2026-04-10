using System;

namespace EducationCentreManagement
{
    public class Student : Person
    {
        public string[] Subjects { get; set; }

        public Student(string name, string phone, string email, string[] subjects)
            : base(name, phone, email, "Student")
        {
            Subjects = subjects;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo(); // Call base display
            Console.WriteLine($"Subjects: {string.Join(", ", Subjects)}");
        }
    }
}
