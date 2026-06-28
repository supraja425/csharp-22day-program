using System;

namespace CS03_Vital_signs_monitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("           VITAL SIGNS MONITOR");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Enter Patient Name: ");
            string patientName = Console.ReadLine();

            double temperature = GetTemperature();
            int oxygenLevel = GetOxygenLevel();
            int pulseRate = GetPulseRate();

            Console.WriteLine("\n[Analyzing Data...]\n");

            string status = CheckStatus(temperature, oxygenLevel, pulseRate);

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("       MEDICAL ASSESSMENT REPORT");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("Patient: " + patientName);
            Console.WriteLine();

            Console.WriteLine("Vitals Recorded:");
            Console.WriteLine("- Temp:   " + temperature + " C");
            Console.WriteLine("- Oxygen: " + oxygenLevel + " %");
            Console.WriteLine("- Pulse:  " + pulseRate + " BPM");
            Console.WriteLine();

            Console.WriteLine("Status Assessment: " + status);

            if (status == "CRITICAL / EMERGENCY")
            {
                Console.WriteLine("Action: Immediate medical attention required.");
            }
            else if (status == "OBSERVATION NEEDED")
            {
                Console.WriteLine("Action: Nurse to monitor every hour.");
            }
            else
            {
                Console.WriteLine("Action: Patient is stable.");
            }

            Console.WriteLine("--------------------------------------------------");

            Console.ReadKey();
        }

        // Method to get valid temperature
        static double GetTemperature()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Temperature (C): ");
                    double temp = Convert.ToDouble(Console.ReadLine());

                    if (temp > 25 && temp < 45)
                    {
                        return temp;
                    }

                    Console.WriteLine("Temperature must be between 25 and 45 Celsius.");
                }
                catch
                {
                    Console.WriteLine("Please enter a valid temperature.");
                }
            }
        }

        // Method to get valid oxygen level
        static int GetOxygenLevel()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Oxygen Level (%): ");
                    int oxygen = Convert.ToInt32(Console.ReadLine());

                    if (oxygen >= 0 && oxygen <= 100)
                    {
                        return oxygen;
                    }

                    Console.WriteLine("Oxygen level must be between 0 and 100.");
                }
                catch
                {
                    Console.WriteLine("Please enter a valid oxygen level.");
                }
            }
        }

        // Method to get valid pulse rate
        static int GetPulseRate()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Pulse Rate (BPM): ");
                    int pulse = Convert.ToInt32(Console.ReadLine());

                    if (pulse > 0 && pulse < 250)
                    {
                        return pulse;
                    }

                    Console.WriteLine("Pulse rate must be a positive number.");
                }
                catch
                {
                    Console.WriteLine("Please enter a valid pulse rate.");
                }
            }
        }

        // Method to check patient status
        static string CheckStatus(double temp, int oxygen, int pulse)
        {
            if (temp > 39.0 || oxygen < 90 || pulse < 50 || pulse > 120)
            {
                return "CRITICAL / EMERGENCY";
            }
            else if (temp > 37.5 || oxygen < 95 || pulse > 100)
            {
                return "OBSERVATION NEEDED";
            }
            else
            {
                return "NORMAL";
            }
        }
    }
}