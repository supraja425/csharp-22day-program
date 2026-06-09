using System;

namespace AppointmentSystem
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            List<string> departments = new List<string>
            {
                "General Medicine",
                "Dental",
                "Orthopedics"
            };

            Dictionary<string, List<string>> doctorsMap = new Dictionary<string, List<string>>
            {
                { "General Medicine", new List<string> { "Dr. A. Kumar", "Dr. B. Singh" } },
                { "Dental", new List<string> { "Dr. C. Roy", "Dr. D. Gupta" } },
                { "Orthopedics", new List<string> { "Dr. E. Sharma", "Dr. F. Patel" } }
            };

            List<string> timeSlots = new List<string>
            {
                "10:00 AM",
                "11:00 AM",
                "12:00 PM"
            };

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       APPOINTMENT BOOKING SYSTEM");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Enter Patient Name: ");
            string name = Console.ReadLine();

            int deptChoice;
            while (true)
            {
                Console.WriteLine("\nSelect Department:");
                for (int i = 0; i < departments.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {departments[i]}");
                }

                Console.Write("Enter Choice: ");
                if (int.TryParse(Console.ReadLine(), out deptChoice) &&
                    deptChoice >= 1 && deptChoice <= departments.Count)
                {
                    break;
                }

                Console.WriteLine("Error: Invalid department choice. Try again.");
            }

            string selectedDept = departments[deptChoice - 1];
            
            List<string> doctors = doctorsMap[selectedDept];
            int docChoice;

            while (true)
            {
                Console.WriteLine("\nSelect Doctor:");
                for (int i = 0; i < doctors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {doctors[i]}");
                }

                Console.Write("Enter Choice: ");
                if (int.TryParse(Console.ReadLine(), out docChoice) &&
                    docChoice >= 1 && docChoice <= doctors.Count)
                {
                    break;
                }

                Console.WriteLine("Error: Invalid doctor choice. Try again.");
            }

            string selectedDoctor = doctors[docChoice - 1];
           
            int timeChoice;

            while (true)
            {
                Console.WriteLine("\nSelect Time Slot:");
                for (int i = 0; i < timeSlots.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {timeSlots[i]}");
                }

                Console.Write("Enter Choice: ");
                if (int.TryParse(Console.ReadLine(), out timeChoice) &&
                    timeChoice >= 1 && timeChoice <= timeSlots.Count)
                {
                    break;
                }

                Console.WriteLine("Error: Invalid time slot. Try again.");
            }

            string selectedTime = timeSlots[timeChoice - 1];
           
            Appointment appointment = new Appointment
            {
                PatientName = name,
                Department = selectedDept,
                Doctor = selectedDoctor,
                Time = selectedTime
            };

            Console.WriteLine("\n[Booking Confirmed]\n");
            
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            APPOINTMENT TICKET");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine($"Patient:    {appointment.PatientName}");
            Console.WriteLine($"Department: {appointment.Department}");
            Console.WriteLine($"Doctor:     {appointment.Doctor}");
            Console.WriteLine($"Time:       {appointment.Time}");
            Console.WriteLine($"Status:     Confirmed");

            Console.WriteLine("\nPlease arrive 15 mins before your slot.");
            Console.WriteLine("--------------------------------------------------");

            Console.ReadLine();

        }

        class Appointment
        {
            public string PatientName { get; set; }
            public string Department { get; set; }
            public string Doctor { get; set; }
            public string Time { get; set; }
        }

    }
}