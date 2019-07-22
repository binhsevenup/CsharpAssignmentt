using System;
using System.Runtime.InteropServices;
using Bank.entity;
using MySql.Data.MySqlClient;


namespace Bank.model
{
    public class BlockchainAddressModel
    {
        public BlockchainAddress GetByAddressAndPrivateKey(string address, string privatekey)
        {
            
            var cmd = new MySqlCommand("select * from BlockchainAddress where address = @address And privateKey = @privateKey ",
                ConnectionHelper.OpenConnection());
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@privateKey", privatekey);
            BlockchainAddress blockchainAddress = null;

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                blockchainAddress = new BlockchainAddress();
                {
                    Address = reader.GetString("address");
                    PrivateKey = reader.GetString("privatekey");
                    Balance = reader.GetString("balance");

                };
                                                 

            }

            ConnectionHelper.OpenConnection();

            
            return blockchainAddress;
        }

        private void GetConnection()
        {
            throw new NotImplementedException();
        }

        public string Balance { get; set; }

        public string PrivateKey { get; set; }

        public string Address { get; set; }

        public bool UpdateBalance(BlockchainAddress currentLoggedInAccount, BlockchainTransaction blockchainTransaction)
        {
            ConnectionHelper.OpenConnection();
            MySqlConnection mySqlConnection;
            var tran = ConnectionHelper.OpenConnection().BeginTransaction();
            try
            {
                var cmd = new MySqlCommand("select * from BlockchainAddress where address = @address",
                    ConnectionHelper.OpenConnection());
                cmd.Parameters.AddWithValue("@address", currentLoggedInAccount.Address);
                BlockchainAddress blockchainAddress = null;
                var reader = cmd.ExecuteReader();
                double currentAddressBalance = 0;

                if (reader.Read())
                {
                    currentAddressBalance = reader.GetDouble("balance");
                }
                reader.Close();
                if (currentAddressBalance < 0)
                {
                    Console.WriteLine("Not enough money");
                    return false;
                }

                if ((blockchainTransaction.Type = 1) != 0)
                {
                    if (currentAddressBalance < blockchainTransaction.Amount)
                    {
                        Console.WriteLine("Not enough money");
                        return false;
                    }

                    currentAddressBalance -= blockchainTransaction.Amount;
                }
                else if (blockchainTransaction.Type == 2)
                {
                    currentAddressBalance += blockchainTransaction.Amount;
                }

                var updateQuery =
                    "update `BlockchainAddress` set `balance` = @balance where address = @address";
                var sqlCmd = new MySqlCommand(updateQuery, ConnectionHelper.OpenConnection());
                sqlCmd.Parameters.AddWithValue("@balance", currentAddressBalance);
                sqlCmd.Parameters.AddWithValue("@address", currentLoggedInAccount.Address);
                var updateResult = sqlCmd.ExecuteNonQuery();
                var historyTransactionQuery =
                    "insert into `BlockchainTransaction` (transactionId, type, senderId, receiverId, amount, status)" +
                    "values(@transactionId, @type, @senderId, @receiverId, @amount, @status)";
                var historyTransactionCmd = new MySqlCommand(historyTransactionQuery, ConnectionHelper.OpenConnection());
                historyTransactionCmd.Parameters.AddWithValue("@transactionId", blockchainTransaction.TransactionId );
                historyTransactionCmd.Parameters.AddWithValue("@amount", blockchainTransaction.Amount);
                historyTransactionCmd.Parameters.AddWithValue("@type", blockchainTransaction.Type);
                historyTransactionCmd.Parameters.AddWithValue("@message", blockchainTransaction.Status);
                historyTransactionCmd.Parameters.AddWithValue("@senderId",
                    blockchainTransaction.SenderId);
                historyTransactionCmd.Parameters.AddWithValue("@receiverId",
                    BlockchainTransaction.ReceiverId);
                var historyResult = historyTransactionCmd.ExecuteNonQuery();
                if (updateResult != 1 || historyResult != 1)
                {
                    throw new Exception("Can not transaction or update your account.");
                }
                tran.Commit();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tran.Rollback();
                return false;
            }

            ConnectionHelper.OpenConnection();
            return true;
        }

        public static object FindByAddrssAndPrivateKey(object address, object privatekey)
        {
            throw new NotImplementedException();
        }

        public static bool UpdateBalance(SHBAccount currentLoggedInAccount, object blockchainTransaction)
        {
            throw new NotImplementedException();
        }
    }
}