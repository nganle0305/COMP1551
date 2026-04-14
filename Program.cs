using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationCentreManagement
{
    // Domain classes
    public abstract class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public Person(string name, string phone, string email, string role)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Role = role;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"-----------------------------------------");
            Console.WriteLine($"Role: {Role.ToUpper()}");
            Console.WriteLine($"Name: {Name} | Phone: {Phone} | Email: {Email}");
        }
    }

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
            base.DisplayInfo();
            Console.WriteLine($"Subjects: {string.Join(", ", Subjects)}");
        }
    }

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

    public class AdministrativeStaff : Person
    {
        public double Salary { get; set; }
        public string EmploymentType { get; set; }
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

    // Main program
    class Program
    {
        // Store all records in a list
        static List<Person> personList = new List<Person>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n===== EDUCATION CENTRE MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Add New Record");
                Console.WriteLine("2. View All Records");
                Console.WriteLine("3. Filter Records by Role");
                Console.WriteLine("4. Edit Record");
                Console.WriteLine("5. Delete Record");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option (1-6): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddRecord(); break;
                    case "2": ViewAll(); break;
                    case "3": FilterByRole(); break;
                    case "4": EditRecord(); break;
                    case "5": DeleteRecord(); break;
                    case "6": running = false; break;
                    default: Console.WriteLine("Invalid option. Please try again."); break;
                }
            }
        }

        // Add new record
        static void AddRecord()
        {
            Console.WriteLine("\nChoose Role to Add: 1. Student | 2. Teacher | 3. Admin");
            string roleChoice = Console.ReadLine();

            Console.Write("Enter Name: "); string name = Console.ReadLine();
            Console.Write("Enter Phone: "); string phone = Console.ReadLine();
            Console.Write("Enter Email: "); string email = Console.ReadLine();

            if (roleChoice == "1") // Add Student
            {
                string[] subs = new string[3];
                for (int i = 0; i < 3; i++) { Console.Write($"Subject {i + 1}: "); subs[i] = Console.ReadLine(); }
                personList.Add(new Student(name, phone, email, subs));
            }
            else if (roleChoice == "2") // Add Teacher
            {
                double salary = ValidateDouble("Enter Salary: ");
                string[] subs = new string[2];
                for (int i = 0; i < 2; i++) { Console.Write($"Subject Taught {i + 1}: "); subs[i] = Console.ReadLine(); }
                personList.Add(new Teacher(name, phone, email, salary, subs));
            }
            else if (roleChoice == "3") // Add Admin
            {
                double salary = ValidateDouble("Enter Salary: ");
                Console.Write("Employment Type (Full-time/Part-time): "); string type = Console.ReadLine();
                int hours = ValidateInt("Working Hours: ");
                personList.Add(new AdministrativeStaff(name, phone, email, salary, type, hours));
            }
            Console.WriteLine("Record added successfully!");
        }


        // Display all records
        static void ViewAll()
        {
            if (personList.Count == 0) { Console.WriteLine("No records found."); return; }
            foreach (var person in personList)
            {
                person.DisplayInfo(); // Polymorphic call
            }
        }

        // Filter records by role
        static void FilterByRole()
        {
            Console.Write("Enter Role to filter (Student/Teacher/Admin): ");
            string role = Console.ReadLine();
            var filtered = personList.Where(p => p.Role.Equals(role, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filtered.Count == 0) Console.WriteLine("No matches found.");
            else foreach (var p in filtered) p.DisplayInfo();
        }

        // Delete a record
        static void DeleteRecord()
        {
            Console.Write("Enter Name of the person to delete: ");
            string name = Console.ReadLine();
            var person = personList.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (person != null)
            {
                personList.Remove(person);
                Console.WriteLine("Record deleted successfully.");
            }
            else Console.WriteLine("Person not found.");
        }

        // Edit an existing record
        static void EditRecord()
        {
            Console.Write("Enter name to edit: ");
            string name = Console.ReadLine();

            var person = personList.FirstOrDefault(p =>
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (person == null)
            {
                Console.WriteLine("Person not found.");
                return;
            }

            Console.WriteLine("\n--- Editing ---");
            Console.WriteLine("Press Enter to keep current value.");

            // Common fields
            Console.Write($"Name ({person.Name}): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                person.Name = newName;

            Console.Write($"Phone ({person.Phone}): ");
            string newPhone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPhone))
                person.Phone = newPhone;

            Console.Write($"Email ({person.Email}): ");
            string newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
                person.Email = newEmail;

            // Student
            if (person is Student student)
            {
                for (int i = 0; i < student.Subjects.Length; i++)
                {
                    Console.Write($"Subject {i + 1} ({student.Subjects[i]}): ");
                    string input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input))
                        student.Subjects[i] = input;
                }
            }
            // Teacher
            else if (person is Teacher teacher)
            {
                Console.Write($"Salary ({teacher.Salary}): ");
                string salaryInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(salaryInput) && double.TryParse(salaryInput, out double s))
                    teacher.Salary = s;

                for (int i = 0; i < teacher.SubjectsTaught.Length; i++)
                {
                    Console.Write($"Subject {i + 1} ({teacher.SubjectsTaught[i]}): ");
                    string input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input))
                        teacher.SubjectsTaught[i] = input;
                }
            }
            // Admin
            else if (person is AdministrativeStaff admin)
            {
                Console.Write($"Salary ({admin.Salary}): ");
                string salaryInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(salaryInput) && double.TryParse(salaryInput, out double s))
                    admin.Salary = s;

                Console.Write($"Employment Type ({admin.EmploymentType}): ");
                string typeInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(typeInput))
                    admin.EmploymentType = typeInput;

                Console.Write($"Working Hours ({admin.WorkingHours}): ");
                string hourInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(hourInput) && int.TryParse(hourInput, out int h))
                    admin.WorkingHours = h;
            }

            Console.WriteLine("Record updated.");
        }

        // Validation methods
        static double ValidateDouble(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out result)) return result;
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        static int ValidateInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result)) return result;
                Console.WriteLine("Invalid input. Please enter an integer.");
            }
        }
    }
}
