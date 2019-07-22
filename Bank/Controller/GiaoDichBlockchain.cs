using System;
using Bank.entity;
using Bank.model;

namespace Bank
{
    public class GiaoDichBlockchain : GiaoDich
    {
        public bool deposit()
        {
            throw new System.NotImplementedException();
        }

        public bool withdrawal()
        {
            throw new System.NotImplementedException();
        }

        public bool transfer()
        {
            throw new System.NotImplementedException();
        }


        public void Login()
        {
            Program.CurrentLoggedInAccount = null;
            Console.Clear();
            Console.WriteLine("Login Blockchain System.");
            Console.WriteLine("Address: ");
            var address = Console.ReadLine();
            Console.WriteLine("Private Key: ");
            var privatekey = Console.ReadLine();
            var blockchainAccount = BlockchainAddressModel.FindByAddrssAndPrivateKey(address, privatekey);
            if (blockchainAccount == null)
            {
                Console.WriteLine("Wrong Address or Private Key. Please try again.");
                Console.WriteLine("Press any key to continue.");
                Console.Read();
                return;
            }

            Program.CurrentLoggedInAccount = blockchainAccount as SHBAccount;

        }

        public void Withdrawal()
        {
            if (Program.CurrentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("===== Withdrawal on Blockchain =====");
                Console.WriteLine("Enter your amount: ");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("Your amount is unavaiable. Please try again.");
                    return;
                }

                var transaction = new SHBTransaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    SenderAccountId = Program.CurrentLoggedInAccount.AccountNumber,
                    ReceiverAccountId = Program.CurrentLoggedInAccount.AccountNumber,
                    Type = 1,
                    Message = "Withdrawal completed: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 1
                };
                BlockchainTransaction blockchainTransaction;
                bool result =
                    BlockchainAddressModel.UpdateBalance(Program.CurrentLoggedInAccount, typeof(BlockchainTransaction));
            }
            else
            {
                Console.WriteLine("Please login your account to use this function.");
            }
        }

        public void Deposit()
        {
            if (Program.CurrentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("===== Deposit on Blockchain =====");
                Console.WriteLine("Enter your amount: ");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("Your amount is unavaiable. Please try again.");
                    return;
                }

                var transaction = new SHBTransaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    SenderAccountId = Program.CurrentLoggedInAccount.AccountNumber,
                    ReceiverAccountId = Program.CurrentLoggedInAccount.AccountNumber,
                    Type = 2,
                    Message = "Deposit completed: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 2
                };
                BlockchainTransaction blockchainTransaction;
                bool result =
                    BlockchainAddressModel.UpdateBalance(Program.CurrentLoggedInAccount, typeof(BlockchainTransaction));
            }
            else
            {
                Console.WriteLine("Please login your account to use this function.");
            }

            void Transfer()
            {
               
            }

        }
        
        public void Transfer()
        {
            if (Program.CurrentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("===== Transfer on Blockchain =====");
                Console.WriteLine("Enter your amount");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("Your amount is unavaiable. Please try again.");
                    return;
                }

                var transaction = new SHBTransaction
                {
                    TransactionId = Guid.NewGuid().ToString(),
                    SenderAccountId = Program.CurrentLoggedInAccount.AccountNumber,
                    ReceiverAccountId = Program.CurrentLoggedInAccount.AccountNumber,
                    Type = 3,
                    Message = "Transfer completed: " + amount,
                    Amount = amount,
                    CreatedAtMLS = DateTime.Now.Ticks,
                    UpdatedAtMLS = DateTime.Now.Ticks,
                    Status = 3
                };
                BlockchainTransaction blockchainTransaction;
                bool result = BlockchainAddressModel.UpdateBalance(Program.CurrentLoggedInAccount,
                    typeof(BlockchainTransaction));
            }
            else
            {
                Console.WriteLine("Please login your account to use this function.");
            }
        }
    }
}
