using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            textParameter.IsNullable = true;
            textParameter.ParameterName = "@Text";
            textParameter.Value = thread.Text;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, textParameter });

            command.CommandText = @"INSERT INTO [dbo].[thread]([name],[text]) VALUES
                                        (@Name, @Text)";

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

        public void DeleteThread(Thread thread)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = $@"UPDATE [dbo].[threads] SET IsDeleted = 1 WHERE thread_id = {thread.Id}";

            SqlConncetionHelper.ExecuteCommands(command);
        }
    }
}
