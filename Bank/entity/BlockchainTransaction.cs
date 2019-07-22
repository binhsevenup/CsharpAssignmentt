namespace Bank.entity
{
    public class BlockchainTransaction
    {
        public string TransactionId { get; set; }
        public string SenderId { get; set; }
        public static string ReceiverId { get; set; }
        public double Amount { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        


        public BlockchainTransaction(string transactionId, string senderId, string receiverId, double amount)
        {
            TransactionId = transactionId;
            SenderId = senderId;
            ReceiverId = receiverId;
            Amount = amount;
        }
    }
}