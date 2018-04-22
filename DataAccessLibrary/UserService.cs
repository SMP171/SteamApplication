using DataAccessLibrary.EntityFramework;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccessLibrary
{
    public class UserService
    {
        public void CreateUsers(User user)
        {
            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();

                DbCommand command = connection.CreateCommand();

                DbParameter nicknameParametr = command.CreateParameter();
                nicknameParametr.DbType = System.Data.DbType.String;
                nicknameParametr.IsNullable = false;
                nicknameParametr.ParameterName = "@Nickname";
                nicknameParametr.Value = user.Nickname;

                DbParameter passwordParametr = command.CreateParameter();
                passwordParametr.DbType = System.Data.DbType.String;
                passwordParametr.IsNullable = false;
                passwordParametr.ParameterName = "@password";
                passwordParametr.Value = user.Password;

                command.Parameters.AddRange(new DbParameter[] { nicknameParametr, passwordParametr });
                command.CommandText = @"INSERT INTO [dbo].[users]
                    ([nickname],[password]) VALUES (@Nickname, @password)";

                SqlConncetionHelper.ExecuteCommands(command);
            }
        }

        public List<User> SelectAllUsers()
        {
            List<User> users = new List<User>();
            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();

                DbCommand command = connection.CreateCommand();
                command.CommandText = "select * from users";

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(
                        new User
                        {
                            Id = (int)reader["user_id"],
                            Nickname = reader["nickname"].ToString(),
                            WalletId = (int)reader["wallet_id"],
                            Password = reader["password"].ToString(),
                            StatusId = (int)reader["status_id"],
                            RegisterDate = (DateTime)reader["register_date"],
                            IsDeleted = (bool)reader["IsDeleted"],
                        });
                }
            }

            return users;
        }

        public void UpdatePassword(User user, string password)
        {
            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter userIdParameter = command.CreateParameter();
                userIdParameter.DbType = System.Data.DbType.Int32;
                userIdParameter.Value = user.Id;
                userIdParameter.ParameterName = "@UserId";

                DbParameter passwordParameter = command.CreateParameter();
                passwordParameter.DbType = System.Data.DbType.String;
                passwordParameter.Value = password;
                passwordParameter.ParameterName = "@Password";

                command.Parameters.AddRange(new DbParameter[] { userIdParameter, passwordParameter });
                command.CommandText = @"UPDATE [dbo].[users] 
                    SET [password] = @Password WHERE [user_id] = @UserId";

                SqlConncetionHelper.ExecuteCommands(command);
            }
        }

        public void DeleteUser(User user)
        {
            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter userIdParameter = command.CreateParameter();
                userIdParameter.DbType = System.Data.DbType.Int32;
                userIdParameter.Value = user.Id;
                userIdParameter.ParameterName = "@UserId";

                command.Parameters.Add(userIdParameter);
                command.CommandText = @"UPDATE [dbo].[users]
                   SET IsDeleted = 1 WHERE user_id = @UserId";

                SqlConncetionHelper.ExecuteCommands(command);
            }
        }

        public user FindUserByNickname(string nickname)
        {
            user findingUser = new user();

            using (var context = new SteamContext())
            {
                findingUser = context.Users.SingleOrDefault(x => x.nickname == nickname);
            }

            return findingUser;
        }

        public List<user> SelectUser(user user)
        {
            using (var context = new SteamContext())
            {
                List<user> users = new List<user>();
                users = context.Users.Where(x => x.status_id == user.status_id).ToList();

                return users;
            }
        }

        public List<user> SelectUserWalletUser(user user)
        {
            using (var context = new SteamContext())
            {
                List<user> users = new List<user>();
                users = context.Users.Where(x => x.wallet_id == user.wallet_id).ToList();

                return users;
            }
        }

        public decimal AddWallet(user user, long money)
        {
            using (var context = new SteamContext())
            {
                wallet wallet = new wallet();
                decimal walletMoney = wallet.balance += money;
                return walletMoney;
            }
        }
    }
}
