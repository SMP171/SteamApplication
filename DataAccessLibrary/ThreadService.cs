using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class ThreadService
    {
        public void CreateThread(Thread thread)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = System.Data.DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = thread.Name;

            DbParameter textParameter = command.CreateParameter();
            textParameter.DbType = System.Data.DbType.String;
            textParameter.IsNullable = false;
            textParameter.ParameterName = "@Text";
            textParameter.Value = thread.Text;

            DbParameter creatorIdParameter = command.CreateParameter();
            creatorIdParameter.DbType = System.Data.DbType.Int32;
            creatorIdParameter.IsNullable = false;
            creatorIdParameter.ParameterName = "@CreatorId";
            creatorIdParameter.Value = thread.Creator_id;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, textParameter, creatorIdParameter });

            command.CommandText = @"INSERT INTO [dbo].[threads]([name],[text],[creator_id]) VALUES
                                        (@Name, @Text, @CreatorId)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<Thread> SelectAllThreads()
        {
            List<Thread> threads = new List<Thread>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from threads";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                threads.Add(
                    new Thread
                    {
                        Id = (int)reader["thread_id"],
                        Name = reader["name"].ToString(),
                        Text = reader["text"].ToString(),
                        Creator_id = (int)reader["creator_id"],
                        CreateDate = (DateTime)reader["create_date"],
                        IsDeleted = (bool)reader["IsDeleted"]
                    });
            }

            return threads;
        }

        public List<Thread> GetUserThreads(User user)
        {
            List<Thread> threads = new List<Thread>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = user.Id;

            command.Parameters.Add(userIdParameter);
            command.CommandText = @"select * from threads where creator_id = @UserId";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                threads.Add(
                    new Thread
                    {
                        Id = (int)reader["thread_id"],
                        Name = reader["name"].ToString(),
                        Text = reader["text"].ToString(),
                        Creator_id = (int)reader["creator_id"],
                        CreateDate = (DateTime)reader["create_date"],
                        IsDeleted = (bool)reader["IsDeleted"]
                    });
            }

            return threads;
        }

        public void DeleteThread(Thread thread)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = $@"UPDATE [dbo].[threads] SET IsDeleted = 1 WHERE thread_id = {thread.Id}";

            SqlConncetionHelper.ExecuteCommands(command);
        }
    }
}
