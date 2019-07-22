using System;
namespace Bank.entity
{
    public class SHBAccount
    {
    
        public string Username { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }
        public string AccountNumber { get; set; }

        public SHBAccount(string username, string password, double balance, string accountNumber)
        {
            Username = username;
            Password = password;
            Balance = balance;
            AccountNumber = accountNumber;
        }

        public SHBAccount()
        {
            
        }
    }
    }
