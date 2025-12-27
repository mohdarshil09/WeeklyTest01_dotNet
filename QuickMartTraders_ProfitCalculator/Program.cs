namespace QuickMartTraders_ProfitCalculator
{
    internal class Program
    {
        /// <summary>
        /// Represents the last transaction.
        /// </summary>
        static SaleTransaction LastTransaction;
        static bool HasLastTransaction = false;

        static void Main(string[] args)
        {
            while (true)
            {
                /// <summary>
                /// Display the menu.
                /// </summary>
                ShowMenu();
                /// <summary>
                /// Parse the input and check if it's a valid option.
                /// </summary>
                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1:
                            CreateNewTransaction();
                            break;
                        case 2:
                            ViewLastTransaction();
                            break;
                        case 3:
                            CalculateProfitLoss();
                            break;
                        case 4:
                            Console.WriteLine("\nThank you. Application closed normally.");
                            return;
                        default:
                            Console.WriteLine("\nInvalid option. Please select a valid option from the menu.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid option. Please select a valid option from the menu.");
                }
            }
        }
        /// <summary>
        /// Displays the menu.
        /// </summary>  
        static void ShowMenu()
        {
            Console.WriteLine("\n================== QuickMart Traders ==================");
            Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
            Console.WriteLine("2. View Last Transaction");
            Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        static void CreateNewTransaction()
        {
            SaleTransaction transaction = new SaleTransaction();

            Console.Write("\nEnter Invoice No: ");
            string? invoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                Console.WriteLine("Error: Invoice No cannot be empty.");
                return;
            }
            transaction.InvoiceNo = invoiceNo;

            Console.Write("Enter Customer Name: ");
            transaction.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            transaction.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Error: Quantity must be greater than 0.");
                return;
            }
            transaction.Quantity = quantity;

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchaseAmount) || purchaseAmount <= 0)
            {
                Console.WriteLine("Error: Purchase Amount must be greater than 0.");
                return;
            }
            transaction.PurchaseAmount = purchaseAmount;

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal sellingAmount) || sellingAmount < 0)
            {
                Console.WriteLine("Error: Selling Amount must be greater than or equal to 0.");
                return;
            }
            transaction.SellingAmount = sellingAmount;

            CalculateProfitLossForTransaction(transaction);

            /// <summary>
            /// Save the transaction.
            /// </summary>
            LastTransaction = transaction;
            HasLastTransaction = true;

            Console.WriteLine("\nTransaction saved successfully.");
            Console.WriteLine($"Status: {transaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {transaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {transaction.ProfitMarginPercent:F2}");
            Console.WriteLine("------------------------------------------------------");
        }
        
        /// <summary>
        /// Views the last transaction.
        /// </summary>
        static void ViewLastTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("\nNo transaction available. Please create a new transaction first.");
                return;
            }

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}");
            Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
            Console.WriteLine($"Item: {LastTransaction.ItemName}");
            Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
            Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}");
            Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("------------------------------------------------------");
        }

        /// <summary>
        /// Calculates the profit/loss for the last transaction.
        /// </summary>
        static void CalculateProfitLoss()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("\nNo transaction available. Please create a new transaction first.");
                return;
            }

            CalculateProfitLossForTransaction(LastTransaction);

            Console.WriteLine($"\nStatus: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
            Console.WriteLine("------------------------------------------------------");
        }
        /// <summary>
        /// Calculates the profit/loss for the given transaction.
        /// </summary>
        static void CalculateProfitLossForTransaction(SaleTransaction transaction)
        {
            if (transaction.SellingAmount > transaction.PurchaseAmount)
            {
                transaction.ProfitOrLossStatus = "PROFIT";
                transaction.ProfitOrLossAmount = transaction.SellingAmount - transaction.PurchaseAmount;
            }
            else if (transaction.SellingAmount < transaction.PurchaseAmount)
            {
                transaction.ProfitOrLossStatus = "LOSS";
                transaction.ProfitOrLossAmount = transaction.PurchaseAmount - transaction.SellingAmount;
            }
            else
            {
                transaction.ProfitOrLossStatus = "BREAK-EVEN";
                transaction.ProfitOrLossAmount = 0;
            }

            if (transaction.PurchaseAmount > 0)
            {
                transaction.ProfitMarginPercent = (transaction.ProfitOrLossAmount / transaction.PurchaseAmount) * 100;
            }
            else
            {
                transaction.ProfitMarginPercent = 0;
            }
        }
        /// <summary>
        /// Clears the last transaction.
        /// </summary>
        static void ClearLastTransaction()
        {
            LastTransaction = null;
            HasLastTransaction = false;
        }
        /// <summary>
        /// Exits the program.
        /// </summary>
        static void ExitProgram()
        {
            Console.WriteLine("\nThank you. Application closed normally.");
            return;
        }
        /// <summary>
        /// Displays the invalid option message.
        /// </summary>
        static void DisplayInvalidOptionMessage()
        {
            Console.WriteLine("\nInvalid option. Please select a valid option from the menu.");
        }
    }
    }
}
