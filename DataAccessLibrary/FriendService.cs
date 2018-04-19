using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class FriendService
    {
        #region INSERT

        public void CreateFriendUser(User userSender, User userReciever)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            //создаем 2 параметра
            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = userSender.Id;

            DbParameter friendIdParameter = command.CreateParameter();
            friendIdParameter.DbType = System.Data.DbType.Int32;
            friendIdParameter.IsNullable = false;
            friendIdParameter.ParameterName = "@FriendId";
            friendIdParameter.Value = userReciever.Id;

            command.Parameters.AddRange(new DbParameter[] { userIdParameter, friendIdParameter });
            command.CommandText = @"INSERT INTO [dbo].[friends]
                   ([user_id]
                   ,[friend_id])
                    VALUES
                   (@UserId, @FriendId)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region SELECT

        public List<Friend> SelectAllFriends()
        {
            //достаем всех друзей (не удаленных)
            List<Friend> friends = new List<Friend>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from [dbo].[friends]";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                friends.Add(
                    new Friend
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["user_id"],
                        FriendId = (int)reader["friend_id"]
                    });
            }

            return friends;
        }

        public List<User> GetUserFriends(User user)
        {
            //достаем всех друзей юзера (не удаленных)
            List<User> friends = new List<User>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter UserIdParameter = command.CreateParameter();
            UserIdParameter.DbType = System.Data.DbType.Int32;
            UserIdParameter.IsNullable = false;
            UserIdParameter.ParameterName = "@UserId";
            UserIdParameter.Value = user.Id;
            command.Parameters.Add(UserIdParameter);

            command.CommandText = "select * from [dbo].[users] where user_id IN (select friend_id from [dbo].[friends] user_id = @UserId)";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                friends.Add(
                    new User
                    {
                        Id = (int)reader["user_id"],
                        Nickname = (string)reader["nickname"],
                        RegisterDate = (DateTime)reader["register_date"],
                        WalletId = (int)reader["wallet_id"],
                        StatusId = (int)reader["status_id"],
                        IsDeleted = (bool)reader["IsDeleted"],
                        Password = (string)reader["password"]
                    }
                );
            }

            return friends;
        }

        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region DELETE
        public void DeleteFriends(User userFriend)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = $@"DELETE [dbo].[friends]
                   WHERE friend_id = {userFriend.Id}";

            SqlConncetionHelper.ExecuteCommands(command);
        }
    }
    #endregion

    //---------------------------------------------------------------------------------------------------------------

    //UPDATE не нужен
}
