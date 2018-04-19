using DomainModel;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class WalletService
    {
        public void CreateWallet(Wallet wallet)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter balanceParameter = command.CreateParameter();
            balanceParameter.DbType = System.Data.DbType.Decimal;
            balanceParameter.IsNullable = false;
            balanceParameter.ParameterName = "@balance";
            balanceParameter.Value = wallet.Balance;

            DbParameter idParameter = command.CreateParameter();
            idParameter.DbType = System.Data.DbType.Int32;
            idParameter.IsNullable = false;
            idParameter.ParameterName = "@Id";
            idParameter.Value = wallet.Id;

            command.Parameters.AddRange(new DbParameter[] { balanceParameter, idParameter });
            command.CommandText = @"INSERT INTO [dbo].[wallets]
                                            ([balance],[wallet_id]) VALUES (@balance, @Id)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public void UpdateWallet(Wallet wallet)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter balanceParameter = command.CreateParameter();
            balanceParameter.DbType = System.Data.DbType.Decimal;
            balanceParameter.IsNullable = false;
            balanceParameter.ParameterName = "@balance";
            balanceParameter.Value = wallet.Balance;

            DbParameter idParameter = command.CreateParameter();
            idParameter.DbType = System.Data.DbType.Int32;
            idParameter.IsNullable = false;
            idParameter.ParameterName = "@Id";
            idParameter.Value = wallet.Id;

            command.Parameters.AddRange(new DbParameter[] { balanceParameter, idParameter });
            command.CommandText = @"Update [dbo].[wallets]
                                            Set balance = @balance where id = @Id";

            SqlConncetionHelper.ExecuteCommands(command);
        }


        public List<Wallet> SelectAllWallet()
        {
            List<Wallet> wallets = new List<Wallet>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from wallets";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                wallets.Add(
                    new Wallet
                    {
                        Id = (int)reader["wallet_id"],
                        Balance = (decimal)reader["balance"],
                    });
            }

            return wallets;
        }
    }
}
