using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class FriendMessageService
    {
        #region INSERT

        public DbConnection CreateConnection()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["SteamContext"].ProviderName);
            DbConnection connection = providerFactory.CreateConnection();

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["SteamContext"].ConnectionString;
            return connection;
        }
        public void CreateFriendMsg(friend_messages friendMessage)
        {
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                DbCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    //создаем параметры
                    DbParameter userIdParameter = command.CreateParameter();
                    userIdParameter.DbType = System.Data.DbType.Int32;
                    userIdParameter.IsNullable = false;
                    userIdParameter.ParameterName = "@UserId";
                    userIdParameter.Value = friendMessage.user_id;

                    DbParameter friendIdParameter = command.CreateParameter();
                    friendIdParameter.DbType = System.Data.DbType.Int32;
                    friendIdParameter.IsNullable = false;
                    friendIdParameter.ParameterName = "@FriendId";
                    friendIdParameter.Value = friendMessage.friend_id;

                    DbParameter messageParameter = command.CreateParameter();
                    messageParameter.DbType = System.Data.DbType.String;
                    messageParameter.IsNullable = false;
                    messageParameter.ParameterName = "@Message";
                    messageParameter.Value = friendMessage.message;

                    command.Parameters.AddRange(new DbParameter[] { userIdParameter, friendIdParameter, messageParameter });

                    command.CommandText = $@"INSERT INTO [dbo].[friend_messages]
                   ([user_id], [friend_id], [message]) VALUES
                   (@UserId, @FriendId, @Message)";

                   // command.CommandText = $@"INSERT INTO [dbo].[friend_messages]
                   //([user_id], [friend_id], [message]) VALUES
                   //(@UserId, @FriendId, @Message, {friendMessage.send_date.ToString("yyyy-MM-dd HH:mm:ss")})";

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (DbException ex)
                {
                    transaction.Rollback();
                }
            }

        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region SELECT

        public List<friend_messages> SelectAllFriendMessages()
        {
            using (SteamContext db = new SteamContext())
            {
                var result = db.Friend_messages.ToList();
                return result;
            }
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region DELETE
        //DELETE не нужен
        #endregion

        //---------------------------------------------------------------------------------------------------------------

        //UPDATE не нужен

        //---------------------------------------------------------------------------------------------------------------

        public string GetChatBetweenTwoUsers(user user, user friend)
        {
            List<friend_messages> allMsgs = SelectAllFriendMessages();
            List<friend_messages> chat = new List<friend_messages>();

            foreach (var item in allMsgs)
            {
                if ((item.user_id == user.user_id && item.friend_id == friend.user_id) || (item.user_id == friend.user_id && item.friend_id == user.user_id))
                {
                    chat.Add(item);
                }
            }

            string result = "";

            foreach (var item in chat)
            {
                result = result + GetUserNameById(item.user_id) + "\n" + item.send_date + "\n" + item.message + "\n\n\n";
            }

            return result;
        }

        public string GetUserNameById(int user_id)
        {
            using (SteamContext db = new SteamContext())
            {
                var result = (from users in db.Users
                             where users.user_id == user_id
                             select users.nickname).ToList();

                return result[0];
            }
        }
    }
}
