namespace Bank.entity
{
    public class SHBTransaction
    {
        public string TransactionId { get; set; }
        public int Type { get; set; } 
        public string SenderAccountId { get; set; }
        public string ReceiverAccountId { get; set; }
        public double Amount { get; set; }
        public string Message { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }
        public int Status { get; set; }

        public SHBTransaction()
        {
        }

        public SHBTransaction(string senderAccountId, string receiverAccountId, double amount, string message)
        {
            SenderAccountId = senderAccountId;
            ReceiverAccountId = receiverAccountId;
            Amount = amount;
            Message = message;
        }
    }
}