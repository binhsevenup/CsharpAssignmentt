using System;
using Bank.entity;
using Bank.model;

namespace Bank
{
    public class GiaoDichSHB : GiaoDich
    {
        private static SHBAccountModel shbAccountModel;
        private GiaoDich _giaoDichImplementation;

        public GiaoDichSHB()
        {
            shbAccountModel = new SHBAccountModel();
        }

        public bool deposit()
        {
            throw new NotImplementedException();
        }

        public bool withdrawal()
        {
            throw new NotImplementedException();
        }

        public bool transfer()
        {
            throw new NotImplementedException();
        }

        public void Transfer()
        {
            _giaoDichImplementation.Transfer();
        }

        public void Login()
        {
            Program.CurrentLoggedInAccount = null;
            Console.Clear();
            Console.WriteLine("Login SHB System");
            Console.WriteLine("Username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Password: ");
            var password = Console.ReadLine();
            SHBAccount shbAccount = shbAccountModel.FindByUsernameAndPassword(username, password);
            if (shbAccount == null)
            {
                Console.WriteLine("Login failed. Please try again");
                Console.WriteLine("Press any key to continue");
                Console.Read();
                return;
            }

            Program.CurrentLoggedInAccount = shbAccount;
        }

        public void Withdrawal()
        {
            if (Program.CurrentLoggedInAccount != null)
            {
                Console.Clear();
                Console.WriteLine("===== Withdrawal on SHB =====");
                Console.WriteLine("Enter your amount: ");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("Your amount is unavaiable. Please try again");
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
                bool result = shbAccountModel.UpdateBalance(Program.CurrentLoggedInAccount, transaction);
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
                Console.WriteLine("===== Deposit on SHB =====");
                Console.WriteLine("Enter your amount: ");
                var amount = double.Parse(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("Your amount is unavaiable. Please try again");
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
                bool result = shbAccountModel.UpdateBalance(Program.CurrentLoggedInAccount, transaction);
            }
            else
            {
                Console.WriteLine("Please login your account to use this function.");
            }

            void ChuyenKhoan()
            {

                if (Program.CurrentLoggedInAccount != null)
                {
                    Console.Clear();
                    Console.WriteLine("===== Transfer on SHB =====");
                    Console.WriteLine("Enter your amount: ");
                    var amount = double.Parse(Console.ReadLine());
                    if (amount <= 0)
                    {
                        Console.WriteLine("Your amount is unavaiable. Please try again");
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
                    bool result = shbAccountModel.UpdateBalance(Program.CurrentLoggedInAccount, transaction);
                }
                else
                {
                    Console.WriteLine("Please login your account to use this function.");
                }
            }
        }
    }
}