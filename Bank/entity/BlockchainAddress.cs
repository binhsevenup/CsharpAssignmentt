using System;
namespace Bank.entity
{
    public class BlockchainAddress
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }
        public double Balance { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }
        public string AccountNumber { get; set; }

        public BlockchainAddress(string address, string privateKey, double balance)
        {
            Address = address;
            PrivateKey = privateKey;
            Balance = balance;
        }

        public BlockchainAddress()
        {
            throw new NotImplementedException();
        }
    }
}