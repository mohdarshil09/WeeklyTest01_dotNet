namespace MediSureClinic
{
    internal class Program
    {
      
        static PatientBill LastBill;
        static bool HasLastBill = false;

        static void Main(string[] args)
        {
            bool running = true;
           /// <summary>
           /// Main loop of the program.
           /// </summary>
            while (running)
            {
              /// <summary>
              /// Display the menu.
              /// </summary>
                DisplayMenu();
                String choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewBill();
                        break;
                    case "2":
                        ViewLastBill();
                        break;
                    case "3":
                        ClearLastBill();
                        break;
                    case "4":
                        Console.WriteLine("\n Thank you. Application closed normally.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\n Invalid option. Please select a valid menu option.");
                        break;
                }
            }
        }


        /// <summary>
        /// Displays the menu.
        /// </summary>

        static void DisplayMenu()
        {
            Console.WriteLine("\n================== MediSure Clinic Billing ==================");
            Console.WriteLine("1. Create New Bill (Enter Patient Details)");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");

        }


        /// <summary>
        /// Creates a new bill.
        /// </summary>
        static void CreateNewBill()
        {
            PatientBill bill = new PatientBill();

            Console.Write("\nEnter Bill Id: ");
            string billId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(billId))
            {
                Console.WriteLine("Bill Id cannot be empty. Bill creation cancelled.");
                return;
            }

            bill.BillId = billId;

            Console.Write("Enter Patient Name: ");
            string patientName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(patientName))
            {
                Console.WriteLine("Patient Name cannot be empty. Bill creation cancelled.");
                return;
            }
            bill.PatientName = patientName;

            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine();
            if (insuranceInput != null && (insuranceInput.ToUpper() == "Y" || insuranceInput.ToUpper() == "YES"))
            { 
            
                bill.HasInsurance = true;
            }
            else
            {
                bill.HasInsurance = false;
            }

            Console.Write("Enter Consultation Fee: ");
            string consultationInput = Console.ReadLine();
            if (!decimal.TryParse(consultationInput, out decimal consultationFee) || consultationFee <= 0)
            {
                Console.WriteLine("Consultation Fee must be greater than 0. Bill creation cancelled.");
                return;
            }
            bill.ConsultationFee = consultationFee;

            Console.Write("Enter Lab Charges: ");
            string labInput = Console.ReadLine();
            if (!decimal.TryParse(labInput, out decimal labCharges) || labCharges < 0)
            {
                Console.WriteLine("Lab Charges must be greater than or equal to 0. Bill creation cancelled.");
                return;
            }
            bill.LabCharges = labCharges;


            Console.Write("Enter Medicine Charges: ");
            string medicineInput = Console.ReadLine();
            if (!decimal.TryParse(medicineInput, out decimal medicineCharges) || medicineCharges < 0)
            {
                Console.WriteLine("Medicine Charges must be greater than or equal to 0. Bill creation cancelled.");
                return;
            }
            bill.MedicineCharges = medicineCharges;


            bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges;

            if (bill.HasInsurance)
            {
                bill.DiscountAmount = bill.GrossAmount * 0.10m;
            }
            else
            {
                bill.DiscountAmount = 0;
            }


            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;


            /// <summary>
            /// Save the bill.
            /// </summary>
            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------");
        }
        

        /// <summary>
        /// Views the last bill.
        /// </summary>
        static void ViewLastBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("\nNo bill available. Please create a new bill first.");
                Console.WriteLine("------------------------------------------------------------");
                return;
            }

            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Insured: {(LastBill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {LastBill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("------------------------------------------------------------");
        }
        /// <summary>
        /// Clears the last bill.
        /// </summary>
        static void ClearLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("\nLast bill cleared.");
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
