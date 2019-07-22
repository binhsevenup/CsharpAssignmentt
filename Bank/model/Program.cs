using System;
using System.ComponentModel.DataAnnotations;
using Bank.entity;


namespace Bank
{
     class Program
    {
        public static SHBAccount CurrentLoggedInAccount;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                GiaoDich giaoDich = null;
                Console.WriteLine("======== Choose type of System Transactions ========");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Spring Hero Bank.");
                Console.WriteLine("2. Blockchain System.");
                Console.WriteLine("============================================");
                Console.WriteLine("Enter your choice: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        giaoDich = new GiaoDichSHB();
                        break;
                    case 2:
                        giaoDich = new GiaoDichBlockchain();
                        break;
                    default:
                        Console.WriteLine("Wrong login procedure");
                        break;
                }

                
                giaoDich.Login();
                if (CurrentLoggedInAccount != null)
                {
                    Console.WriteLine("Logged in!!");
                    Console.WriteLine($"Username: {CurrentLoggedInAccount.Username}");
                    Console.WriteLine($"Balance: {CurrentLoggedInAccount.Balance}");
                    Console.WriteLine("Press any key to start your transactions.");
                    Console.Read();
                    GenerateTransactionMenu(giaoDich);
                }
            }
        }

        private static void GenerateTransactionMenu(GiaoDich giaoDich)
        {
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("Transaction Options");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Withdraw.");
                Console.WriteLine("2. Deposit.");
                Console.WriteLine("3. Transfer.");
                Console.WriteLine("4. Exit.");
                Console.WriteLine("============================================");
                Console.WriteLine("Enter your choice: ");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        giaoDich.Withdrawal();
                        break;
                    case 2:
                        giaoDich.Deposit();
                        break;
                    case 3:
                        giaoDich.Transfer();
                        break;
                    case 4:
                        Console.WriteLine("Exit completed");
                        break;
                    default:
                        Console.WriteLine("Wrong option. Please try again.");
                        break;
                }

                if (choice == 4)
                {
                    break;
                }
            }
        }
    }

    internal partial interface GiaoDich
    {
        void Withdrawal();
        void Deposit();
        void Transfer();
        void Login();
    }
}