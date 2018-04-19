using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class WalletService
    {
        public void CreateWallet(wallet wallet)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter balanceParameter = command.CreateParameter();
            balanceParameter.DbType = System.Data.DbType.Decimal;
            balanceParameter.IsNullable = false;
            balanceParameter.ParameterName = "@balance";
            balanceParameter.Value = wallet.balance;

            DbParameter idParameter = command.CreateParameter();
            idParameter.DbType = System.Data.DbType.Int32;
            idParameter.IsNullable = false;
            idParameter.ParameterName = "@Id";
            idParameter.Value = wallet.wallet_id;

            command.Parameters.AddRange(new DbParameter[] { balanceParameter, idParameter });
            command.CommandText = @"INSERT INTO [dbo].[wallets]
                                            ([balance],[wallet_id]) VALUES (@balance, @Id)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public void UpdateWallet(wallet wallet)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter balanceParameter = command.CreateParameter();
            balanceParameter.DbType = System.Data.DbType.Decimal;
            balanceParameter.IsNullable = false;
            balanceParameter.ParameterName = "@balance";
            balanceParameter.Value = wallet.balance;

            DbParameter idParameter = command.CreateParameter();
            idParameter.DbType = System.Data.DbType.Int32;
            idParameter.IsNullable = false;
            idParameter.ParameterName = "@Id";
            idParameter.Value = wallet.wallet_id;

            command.Parameters.AddRange(new DbParameter[] { balanceParameter, idParameter });
            command.CommandText = @"Update [dbo].[wallets]
                                            Set balance = @balance where id = @Id";

            SqlConncetionHelper.ExecuteCommands(command);
        }


        public List<wallet> SelectAllWallet()
        {
            List<wallet> wallets = new List<wallet>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from wallets";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                wallets.Add(
                    new wallet
                    {
                        wallet_id = (int)reader["wallet_id"],
                        balance = (decimal)reader["balance"],
                    });
            }

            return wallets;
        }
    }
}
