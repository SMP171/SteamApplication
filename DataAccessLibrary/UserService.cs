//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using DataAccessLibrary.EntityFramework;

//namespace DataAccessLibrary
//{
//    public class UserService
//    {
//        public void CreateUsers(user user)
//        {
//            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

//            DbParameter nicknameParametr = command.CreateParameter();
//            nicknameParametr.DbType = System.Data.DbType.String;
//            nicknameParametr.IsNullable = false;
//            nicknameParametr.ParameterName = "@Nickname";
//            nicknameParametr.Value = user.nickname;

//            DbParameter passwordParametr = command.CreateParameter();
//            passwordParametr.DbType = System.Data.DbType.String;
//            passwordParametr.IsNullable = false;
//            passwordParametr.ParameterName = "@password";
//            passwordParametr.Value = user.password;

//            command.Parameters.AddRange(new DbParameter[] { nicknameParametr, passwordParametr });
//            command.CommandText = @"INSERT INTO [dbo].[users]
//           ([nickname],[password]) VALUES (@Nickname, @password)";

//            SqlConncetionHelper.ExecuteCommands(command);
//        }

//        public List<user> SelectAllUsers()
//        {
//            List<user> users = new List<user>();

//            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
//            command.CommandText = "select * from users";

//            DbDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//            {
//                users.Add(
//                    new user
//                    {
//                        user_id = (int)reader["user_id"],
//                        nickname = reader["nickname"].ToString(),
//                        wallet_id = (int)reader["wallet_id"],
//                        password = reader["password"].ToString(),
//                        status_id = (int)reader["status_id"],
//                        register_date = (DateTime)reader["register_date"],
//                        IsDeleted = (byte)reader["IsDeleted"],
//                    });
//            }

//            return users;
//        }

//        public void UpdatePassword(user user, string password)
//        {
//            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

//            DbParameter userIdParameter = command.CreateParameter();
//            userIdParameter.DbType = System.Data.DbType.Int32;
//            userIdParameter.Value = user.user_id;
//            userIdParameter.ParameterName = "@UserId";

//            DbParameter passwordParameter = command.CreateParameter();
//            passwordParameter.DbType = System.Data.DbType.String;
//            passwordParameter.Value = password;
//            passwordParameter.ParameterName = "@Password";

//            command.Parameters.AddRange(new DbParameter[] { userIdParameter, passwordParameter });
//            command.CommandText = @"UPDATE [dbo].[users] 
//                    SET [password] = @Password WHERE [user_id] = @UserId";

//            SqlConncetionHelper.ExecuteCommands(command);
//        }

//        public void DeleteUser(user user)
//        {
//            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

//            DbParameter userIdParameter = command.CreateParameter();
//            userIdParameter.DbType = System.Data.DbType.Int32;
//            userIdParameter.Value = user.Id;
//            userIdParameter.ParameterName = "@UserId";

//            command.Parameters.Add(userIdParameter);
//            command.CommandText = @"UPDATE [dbo].[users]
//                   SET IsDeleted = 1 WHERE user_id = @UserId";

//            SqlConncetionHelper.ExecuteCommands(command);
//        }
//    }
//}
