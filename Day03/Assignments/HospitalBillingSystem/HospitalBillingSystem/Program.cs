using System;

namespace HospitalBillingSystem
{
    // Bill Class
    class Bill
    {
        // Fee Constants
        public const decimal ConsultationFee = 500m;
        public const decimal BloodTestFee = 200m;
        public const decimal XRayFee = 1000m;
        public const decimal AdmissionFee = 2000m;

        // Properties
        public string PatientName { get; set; }
        public int Age { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }

        // Method to Calculate Bill
        public void CalculateBill()
        {
            decimal discountRate = 0;

            // Senior Citizen Discount
            if (Age > 60)
            {
                discountRate = 0.20m;
                DiscountAmount = BaseAmount * discountRate;
            }
            // Child Discount (Consultation Only)
            else if (Age < 10)
            {
                if (BaseAmount >= ConsultationFee)
                {
                    DiscountAmount = ConsultationFee * 0.50m;
                }
            }

            decimal amountAfterDiscount = BaseAmount - DiscountAmount;

            // 5% Tax
            TaxAmount = amountAfterDiscount * 0.05m;

            NetAmount = amountAfterDiscount + TaxAmount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bill bill = new Bill();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       HOSPITAL BILLING CALCULATOR");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Patient Name: ");
            bill.PatientName = Console.ReadLine();

            // Age Validation
            while (true)
            {
                try
                {
                    Console.Write("Patient Age: ");
                    bill.Age = Convert.ToInt32(Console.ReadLine());

                    if (bill.Age > 0 && bill.Age < 120)
                        break;

                    Console.WriteLine("Please enter a valid age.");
                }
                catch
                {
                    Console.WriteLine("Age must be numeric.");
                }
            }

            int choice;

            Console.WriteLine("\nAdd Services:");

            while (true)
            {
                Console.WriteLine("\n1. Consultation (500)");
                Console.WriteLine("2. Blood Test (200)");
                Console.WriteLine("3. X-Ray (1000)");
                Console.WriteLine("4. Admission (2000)");
                Console.WriteLine("5. Done");

                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        bill.BaseAmount += Bill.ConsultationFee;
                        Console.WriteLine("[Added Consultation]");
                        break;

                    case 2:
                        bill.BaseAmount += Bill.BloodTestFee;
                        Console.WriteLine("[Added Blood Test]");
                        break;

                    case 3:
                        bill.BaseAmount += Bill.XRayFee;
                        Console.WriteLine("[Added X-Ray]");
                        break;

                    case 4:
                        bill.BaseAmount += Bill.AdmissionFee;
                        Console.WriteLine("[Added Admission]");
                        break;

                    case 5:
                        goto Calculate;

                    default:
                        Console.WriteLine("Please select a valid option.");
                        break;
                }
            }

        Calculate:

            Console.WriteLine("\n[Calculating Bill...]\n");

            bill.CalculateBill();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("            FINAL BILL INVOICE");
            Console.WriteLine("--------------------------------------------------");

            string category = "";

            if (bill.Age > 60)
                category = " (Senior Citizen)";
            else if (bill.Age < 10)
                category = " (Child)";

            Console.WriteLine($"Patient: {bill.PatientName}{category}");
            Console.WriteLine();

            Console.WriteLine($"Base Amount:      {bill.BaseAmount:F2}");
            Console.WriteLine($"Discount:        -{bill.DiscountAmount:F2}");
            Console.WriteLine($"Tax (5%):        +{bill.TaxAmount:F2}");

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"TOTAL PAYABLE:    {bill.NetAmount:F2}");
            Console.WriteLine("--------------------------------------------------");

            Console.ReadKey();
        }
    }
}