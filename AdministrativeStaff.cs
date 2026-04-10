using System;

namespace EducationCentreManagement
{
    public class AdministrativeStaff : Person
    {
        public double Salary { get; set; }
        public string EmploymentType { get; set; } // Full-time / Part-time
        public int WorkingHours { get; set; }

        public AdministrativeStaff(string name, string phone, string email, double salary, string empType, int hours)
            : base(name, phone, email, "Admin")
        {
            Salary = salary;
            EmploymentType = empType;
            WorkingHours = hours;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Salary: {Salary:N0} VND | Type: {EmploymentType} | Hours: {WorkingHours}/week");
        }
    }
}
