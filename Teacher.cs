using System;

namespace EducationCentreManagement
{
    public class Teacher : Person
    {
        public double Salary { get; set; }
        public string[] SubjectsTaught { get; set; }

        public Teacher(string name, string phone, string email, double salary, string[] subjects)
            : base(name, phone, email, "Teacher")
        {
            Salary = salary;
            SubjectsTaught = subjects;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Salary: {Salary:N0} VND | Subjects: {string.Join(", ", SubjectsTaught)}");
        }
    }
}
