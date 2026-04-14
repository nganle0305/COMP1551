using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationCentreManagement
{
    /// Main Program: Controls the logic and data structures of the application.
    class Program
    {
        // Data Structure: List can store an unknown number of objects (Task 3 requirement)
        static List<Person> personList = new List<Person>();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                // Text-based menu for administrative staff interaction
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

        /// Functionality: Adding new records with data validation.
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


        /// Functionality: Displaying all objects using Polymorphism.
        static void ViewAll()
        {
            if (personList.Count == 0) { Console.WriteLine("No records found."); return; }
            foreach (var person in personList)
            {
                person.DisplayInfo(); // Polymorphic call
            }
        }

        /// Functionality: Filtering objects by their rol
        static void FilterByRole()
        {
            Console.Write("Enter Role to filter (Student/Teacher/Admin): ");
            string role = Console.ReadLine();
            var filtered = personList.Where(p => p.Role.Equals(role, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filtered.Count == 0) Console.WriteLine("No matches found.");
            else foreach (var p in filtered) p.DisplayInfo();
        }

        /// Functionality: Deleting a record from the list.
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

        // --- VALIDATION HELPER METHODS ---
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