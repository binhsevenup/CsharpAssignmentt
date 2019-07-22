using System;
using Bank.entity;
using MySql.Data.MySqlClient;

namespace Bank.model
{
    public class SHBAccountModel
    {
       
        public SHBAccount FindByUsernameAndPassword(string username, string password)
        {
                       
            var cmd = new MySqlCommand("select * from account where username = @username And password = @password",
                ConnectionHelper.OpenConnection());
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            
            SHBAccount shbAccount = null;
                      
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                shbAccount = new SHBAccount
                {
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    Balance = reader.GetDouble("balance")
                };
            }

            ConnectionHelper.CloseConnection();
        
            return shbAccount;
        }

        public bool UpdateBalance(SHBAccount currentLoggedInAccount, SHBTransaction transaction)
        {
            ConnectionHelper.OpenConnection();
            MySqlConnection mySqlConnection;
            var tran = ConnectionHelper.OpenConnection().BeginTransaction();
            try
            {
                var cmd = new MySqlCommand("select * from account where username = @username",
                    ConnectionHelper.OpenConnection());
                cmd.Parameters.AddWithValue("@Username", currentLoggedInAccount.AccountNumber);
                SHBAccount shbAccount = null;
                var reader = cmd.ExecuteReader();
                double currentAccountBalance = 0;

                if (reader.Read())
                {
                    currentAccountBalance = reader.GetDouble("balance");
                }

                reader.Close();
                if (currentAccountBalance < 0)
                {
                    Console.WriteLine("You have not enough money");
                    return false;
                }

                if (transaction.Type == 1)
                {
                    if (currentAccountBalance < transaction.Amount)
                    {
                        Console.WriteLine("Not enough money");
                        return false;
                    }
                    currentAccountBalance -= transaction.Amount;
                }
                else if (transaction.Type == 2)
                {
                    currentAccountBalance += transaction.Amount;
                }

                var updateQuery =
                    "update `account` set `balance` = @balance where accountId = @accountId";
                var sqlCmd = new MySqlCommand(updateQuery, ConnectionHelper.OpenConnection());
                sqlCmd.Parameters.AddWithValue("@balance", currentAccountBalance);
                sqlCmd.Parameters.AddWithValue("@accountId", currentLoggedInAccount.AccountNumber);
                var updateResult = sqlCmd.ExecuteNonQuery();
                var historyTransactionQuery =
                    "insert into `SHB` (transactionId, type, senderId, receiverId, amount, message) " +
                    "values (@transactionId, @type, @senderId, @receiverId, @amount, @message)";
                var historyTransactionCmd =
                    new MySqlCommand(historyTransactionQuery, ConnectionHelper.OpenConnection());
                historyTransactionCmd.Parameters.AddWithValue("@transactionId", transaction.TransactionId);
                historyTransactionCmd.Parameters.AddWithValue("@amount", transaction.Amount);
                historyTransactionCmd.Parameters.AddWithValue("@type", transaction.Type);
                historyTransactionCmd.Parameters.AddWithValue("@message", transaction.Message);
                historyTransactionCmd.Parameters.AddWithValue("@senderId",
                    transaction.SenderAccountId);
                historyTransactionCmd.Parameters.AddWithValue("@receiverId",
                    transaction.ReceiverAccountId);
                var historyResult = historyTransactionCmd.ExecuteNonQuery();

                if (updateResult != 1 || historyResult != 1)
                {
                    throw new Exception("Can not transaction or update your account");
                }

                tran.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                tran.Rollback();                
                return false;
            }

            ConnectionHelper.CloseConnection();
            return true;
        }
    }
}