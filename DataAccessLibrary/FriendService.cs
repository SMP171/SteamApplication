using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class FriendService
    {
        public DbConnection CreateConnection()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["SteamContext"].ProviderName);
            DbConnection connection = providerFactory.CreateConnection();

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["SteamContext"].ConnectionString;
            return connection;
        }
        #region INSERT

        public void CreateFriendUser(user userSender, user userReciever)
        {
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                DbCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    //создаем 2 параметра
                    DbParameter userIdParameter = command.CreateParameter();
                    userIdParameter.DbType = System.Data.DbType.Int32;
                    userIdParameter.IsNullable = false;
                    userIdParameter.ParameterName = "@UserId";
                    userIdParameter.Value = userSender.user_id;

                    DbParameter friendIdParameter = command.CreateParameter();
                    friendIdParameter.DbType = System.Data.DbType.Int32;
                    friendIdParameter.IsNullable = false;
                    friendIdParameter.ParameterName = "@FriendId";
                    friendIdParameter.Value = userReciever.user_id;

                    command.CommandText = String.Format("INSERT INTO [dbo].[friends] ([user_id],[friend_id]) VALUES ({0},{1})", userIdParameter.Value, friendIdParameter.Value);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (DbException ex)
                {
                    transaction.Rollback();
                }
            }

            //-------------------------------------------------------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------------------------------------------------------

            //DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            ////создаем 2 параметра
            //DbParameter userIdParameter = command.CreateParameter();
            //userIdParameter.DbType = System.Data.DbType.Int32;
            //userIdParameter.IsNullable = false;
            //userIdParameter.ParameterName = "@UserId";
            //userIdParameter.Value = userSender.Id;

            //DbParameter friendIdParameter = command.CreateParameter();
            //friendIdParameter.DbType = System.Data.DbType.Int32;
            //friendIdParameter.IsNullable = false;
            //friendIdParameter.ParameterName = "@FriendId";
            //friendIdParameter.Value = userReciever.Id;

            //command.Parameters.AddRange(new DbParameter[] { userIdParameter, friendIdParameter });
            ////command.CommandText = @"INSERT INTO [dbo].[friends]
            ////       ([user_id]
            ////       ,[friend_id])
            ////        VALUES
            ////       (@UserId, @FriendId)";
            //command.CommandText = String.Format("INSERT INTO [dbo].[friends] ([user_id],[friend_id]) VALUES ({0},{1})", userIdParameter.Value, friendIdParameter.Value);

            //SqlConncetionHelper.ExecuteCommands(command);
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region SELECT

        public List<friend> SelectAllFriends()
        {
            List<friend> friends = new List<friend>();

            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                command.CommandText = "select * from [dbo].[friends]";

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    friends.Add(
                        new friend
                        {
                            id = (int)reader["Id"],
                            user_id = (int)reader["user_id"],
                            friend_id = (int)reader["friend_id"]
                        });
                }

            }
            return friends;

        }

        public List<user> GetUserFriends(user user)
        {
            //достаем всех друзей юзера (не удаленных)
            //using (SteamContext db = new SteamContext())
            //{
            //    var users = db.Users.ToList();

            //    var innerQuery = (from friends in db.Friends
            //                     where friends.user_id == user.user_id
            //                     select friends.friend_id).ToList();

            //    List<string> userFriends = new List<string>();

            //    for (int i = 0; i < users.Count; i++)
            //    {
            //        for (int j = 0; j < innerQuery.Count; j++)
            //        {
            //            if (users[i].user_id == innerQuery[j])
            //            {
            //                userFriends.Add(users[i].nickname);
            //            }
            //        }
            //    }

            //    //var result = (from users in db.Users
            //    //              where innerQuery.Contains(users.user_id)
            //    //              select users).ToList();

            //    return userFriends;
            //}

            using (SteamContext db = new SteamContext())
            {
                var users = db.Users.ToList();

                var innerQuery = (from friends in db.Friends
                                  where friends.user_id == user.user_id
                                  select friends.friend_id).ToList();

                List<user> userFriends = new List<user>();

                for (int i = 0; i < users.Count; i++)
                {
                    for (int j = 0; j < innerQuery.Count; j++)
                    {
                        if (users[i].user_id == innerQuery[j])
                        {
                            userFriends.Add(users[i]);
                        }
                    }
                }

                //var result = (from users in db.Users
                //              where innerQuery.Contains(users.user_id)
                //              select users).ToList();

                return userFriends;
            }

            //------------------------------------------------------------------------------------------------------------------------
            //List<User> friends = new List<User>();

            //DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            //DbParameter UserIdParameter = command.CreateParameter();
            //UserIdParameter.DbType = System.Data.DbType.Int32;
            //UserIdParameter.IsNullable = false;
            //UserIdParameter.ParameterName = "@UserId";
            //UserIdParameter.Value = user.Id;
            //command.Parameters.Add(UserIdParameter);

            //command.CommandText = "select * from [dbo].[users] where user_id IN (select friend_id from [dbo].[friends] user_id = @UserId)";

            //DbDataReader reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    friends.Add(
            //        new User
            //        {
            //            Id = (int)reader["user_id"],
            //            Nickname = (string)reader["nickname"],
            //            RegisterDate = (DateTime)reader["register_date"],
            //            WalletId = (int)reader["wallet_id"],
            //            StatusId = (int)reader["status_id"],
            //            IsDeleted = (bool)reader["IsDeleted"],
            //            Password = (string)reader["password"]
            //        }
            //    );
            //}

            //return friends;
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region DELETE
        public void DeleteFriends(user userFriend)
        {
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                DbCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = String.Format("DELETE [dbo].[friends] WHERE friend_id = {0}", userFriend.user_id);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (DbException ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
    #endregion

    //---------------------------------------------------------------------------------------------------------------

    //UPDATE не нужен
}
