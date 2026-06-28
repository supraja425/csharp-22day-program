using System;
using System.Collections.Generic;

namespace HospitalReports
{
    // PatientRecord Class
    class PatientRecord
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal BillAmount { get; set; }
        public string Status { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create and populate patient list
            List<PatientRecord> patients = new List<PatientRecord>()
            {
                new PatientRecord
                {
                    Name = "John Doe",
                    Department = "General",
                    BillAmount = 500,
                    Status = "Discharged"
                },
                new PatientRecord
                {
                    Name = "Jane Smith",
                    Department = "Dental",
                    BillAmount = 1200,
                    Status = "Admitted"
                },
                new PatientRecord
                {
                    Name = "Bob Brown",
                    Department = "General",
                    BillAmount = 400,
                    Status = "Discharged"
                },
                new PatientRecord
                {
                    Name = "Alice Wilson",
                    Department = "Ortho",
                    BillAmount = 2500,
                    Status = "Admitted"
                },
                new PatientRecord
                {
                    Name = "Sam Kumar",
                    Department = "Dental",
                    BillAmount = 800,
                    Status = "Discharged"
                },
                new PatientRecord
                {
                    Name = "Priya Reddy",
                    Department = "Cardiology",
                    BillAmount = 1800,
                    Status = "Admitted"
                }
            };

            int totalPatients = patients.Count;
            decimal totalRevenue = 0;

            Dictionary<string, int> departmentCount =
                new Dictionary<string, int>();

            // Calculate statistics using foreach
            foreach (PatientRecord patient in patients)
            {
                totalRevenue += patient.BillAmount;

                if (departmentCount.ContainsKey(patient.Department))
                {
                    departmentCount[patient.Department]++;
                }
                else
                {
                    departmentCount[patient.Department] = 1;
                }
            }

            // Display Report
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       DAILY HOSPITAL ACTIVITY REPORT");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Date: " + DateTime.Now.ToShortDateString());
            Console.WriteLine();

            Console.WriteLine("Patient List:");

            int serialNo = 1;

            foreach (PatientRecord patient in patients)
            {
                Console.WriteLine(
                    serialNo + ". " +
                    patient.Name + " - " +
                    patient.Department + " - ₹" +
                    patient.BillAmount);

                serialNo++;
            }

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("SUMMARY STATISTICS");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("Total Patients Visited: " + totalPatients);
            Console.WriteLine("Total Revenue: ₹" + totalRevenue);

            Console.WriteLine();
            Console.WriteLine("Traffic by Department:");

            foreach (var department in departmentCount)
            {
                Console.WriteLine("- " +
                                  department.Key +
                                  ": " +
                                  department.Value);
            }

            Console.WriteLine();
            Console.WriteLine("End of Report.");
            Console.WriteLine("--------------------------------------------------");

            Console.ReadKey();
        }
    }
}