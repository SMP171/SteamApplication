using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class FriendMessageService
    {
        #region INSERT

        public void CreateFriendMsg(FriendMessage friendMessage)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            //создаем параметры
            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = friendMessage.UserId;

            DbParameter friendIdParameter = command.CreateParameter();
            friendIdParameter.DbType = System.Data.DbType.Int32;
            friendIdParameter.IsNullable = false;
            friendIdParameter.ParameterName = "@FriendId";
            friendIdParameter.Value = friendMessage.FriendId;

            DbParameter messageParameter = command.CreateParameter();
            messageParameter.DbType = System.Data.DbType.String;
            messageParameter.IsNullable = false;
            messageParameter.ParameterName = "@Message";
            messageParameter.Value = friendMessage.Message;

            command.Parameters.AddRange(new DbParameter[] { userIdParameter, friendIdParameter, messageParameter });
            command.CommandText = @"INSERT INTO [dbo].[friend_messages]
                   ([user_id], [friend_id], [message]) VALUES
                   (@UserId, @FriendId, @Message)";

            SqlConncetionHelper.ExecuteCommands(command);
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region SELECT

        public List<FriendMessage> SelectAllFriendMessages()
        {
            //достаем все письма
            List<FriendMessage> friendMessages = new List<FriendMessage>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from [dbo].[friend_messages]";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                friendMessages.Add(
                    new FriendMessage
                    {
                        Id = (int)reader["FM_id"],
                        UserId = (int)reader["user_id"],
                        FriendId = (int)reader["friend_id"],
                        Message = reader["message"].ToString(),
                        SendDate = (DateTime)reader["send_date"]
                    }
                );
            }

            return friendMessages;
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------

        #region DELETE
        //DELETE не нужен
        #endregion

        //---------------------------------------------------------------------------------------------------------------

        //UPDATE не нужен
    }
}
