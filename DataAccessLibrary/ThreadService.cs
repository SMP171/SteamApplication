using System;
using System.Collections.Generic;
using System.Data.Common;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class ThreadService
    {
        public void CreateThread(thread thread)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = System.Data.DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = thread.name;

            DbParameter textParameter = command.CreateParameter();
            textParameter.DbType = System.Data.DbType.String;
            textParameter.IsNullable = false;
            textParameter.ParameterName = "@Text";
            textParameter.Value = thread.text;

            DbParameter creatorIdParameter = command.CreateParameter();
            creatorIdParameter.DbType = System.Data.DbType.Int32;
            creatorIdParameter.IsNullable = false;
            creatorIdParameter.ParameterName = "@CreatorId";
            creatorIdParameter.Value = thread.creator_id;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, textParameter, creatorIdParameter });

            command.CommandText = @"INSERT INTO [dbo].[threads]([name],[text],[creator_id]) VALUES
                                        (@Name, @Text, @CreatorId)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<thread> SelectAllThreads()
        {
            List<thread> threads = new List<thread>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from threads";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                threads.Add(
                    new thread
                    {
                        thread_id = (int)reader["thread_id"],
                        name = reader["name"].ToString(),
                        text = reader["text"].ToString(),
                        creator_id = (int)reader["creator_id"],
                        create_date = (DateTime)reader["create_date"],
                        IsDeleted = (byte)reader["IsDeleted"]
                    });
            }

            return threads;
        }

        public List<thread> GetUserThreads(user user)
        {
            List<thread> threads = new List<thread>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = user.user_id;

            command.Parameters.Add(userIdParameter);
            command.CommandText = @"select * from threads where creator_id = @UserId";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                threads.Add(
                    new thread
                    {
                        thread_id = (int)reader["thread_id"],
                        name = reader["name"].ToString(),
                        text = reader["text"].ToString(),
                        creator_id = (int)reader["creator_id"],
                        create_date = (DateTime)reader["create_date"],
                        IsDeleted = (byte)reader["IsDeleted"]
                    });
            }

            return threads;
        }

        public void DeleteThread(thread thread)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = $@"UPDATE [dbo].[threads] SET IsDeleted = 1 WHERE thread_id = {thread.thread_id}";

            SqlConncetionHelper.ExecuteCommands(command);
        }
    }
}
