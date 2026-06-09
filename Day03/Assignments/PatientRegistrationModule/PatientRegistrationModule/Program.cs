using System;

namespace PatientRegistrationModule
{
    internal class program
    {
        public static void Main(string[] args)
        {
            Patient patient = RegistrationManager.RegisterPatient();
            RegistrationManager.PrintSlip(patient);
        }

    }
    class RegistrationManager
    {
        public static Patient RegisterPatient()
        {
            Patient patient = new Patient();
            Console.WriteLine("PATIENT REGISTRATION SYSTEM");
            while (true)
            {
                Console.WriteLine("Enter Patient Name: ");
                patient.Name = Console.ReadLine();
                if(!string.IsNullOrWhiteSpace(patient.Name))
                    break;

                Console.WriteLine("Error: Name cannot be empty.");
            }
            while (true)
            {
                Console.WriteLine("Enter Patient Age: ");
                try
                {
                    int age = int.Parse(Console.ReadLine());

                    if (age > 0 && age < 120)
                    {
                        patient.Age = age;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Age must be between 1 and 120.");
                    }
                }
                catch
                {
                    Console.WriteLine("Error: Please enter a valid numeric age.");
                }
            }
                Console.Write("Enter Gender (Male/Female/Other): ");
                patient.Gender = Console.ReadLine();

                while (true)
                {
                    Console.Write("Enter Phone Number: ");
                    string phone = Console.ReadLine();

                    if (phone.Length == 10 && long.TryParse(phone, out _))
                    {
                        patient.PhoneNumber = phone;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error: Phone number must be exactly 10 digits.");
                    }
                }

                Console.Write("Enter City: ");
                patient.City = Console.ReadLine();

                patient.PatientId = GeneratePatientID();

                Console.WriteLine("\n[Registration Complete]\n");

                return patient;
        }

        private static string GeneratePatientID()
        {
            return $"PAT-{DateTime.Now.Year}-{new Random().Next(100, 999)}";
        }

        public static void PrintSlip(Patient patient)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("        PATIENT REGISTRATION SLIP");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Date: {DateTime.Now.ToShortDateString()}");
            Console.WriteLine();
            Console.WriteLine($"Patient ID: {patient.PatientId}");
            Console.WriteLine($"Name:       {patient.Name}");
            Console.WriteLine($"Age:        {patient.Age} years");
            Console.WriteLine($"Gender:     {patient.Gender}");
            Console.WriteLine($"Contact:    {patient.PhoneNumber}");
            Console.WriteLine($"Location:   {patient.City}");

            Console.WriteLine("\nInstructions:");
            Console.WriteLine("Please proceed to the waiting area.");
            Console.WriteLine("--------------------------------------------------");
        }

    }
    class Patient
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }   
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
    }
}