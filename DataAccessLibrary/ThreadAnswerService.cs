using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class ThreadAnswerService
    {
        public void CreateThreadAnswer(ThreadAnswer answer)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter threadIdParameter = command.CreateParameter();
            threadIdParameter.DbType = System.Data.DbType.Int32;
            threadIdParameter.IsNullable = false;
            threadIdParameter.ParameterName = "@Thread_id";
            threadIdParameter.Value = answer.ThreadId;

            DbParameter textParameter = command.CreateParameter();
            textParameter.DbType = System.Data.DbType.String;
            textParameter.IsNullable = false;
            textParameter.ParameterName = "@Text";
            textParameter.Value = answer.Text;

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = answer.UserId;

            command.Parameters.AddRange(new DbParameter[] { threadIdParameter, textParameter, userIdParameter });

            command.CommandText = @"INSERT INTO [dbo].[thread_answers]([thread_id],[user_id],[text]) VALUES
                                        (@Thread_id, @UserId, @Text)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<ThreadAnswer> GetAllThreadsAnswers()
        {
            List<ThreadAnswer> answers = new List<ThreadAnswer>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from thread_answers";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                answers.Add(new ThreadAnswer
                {
                    Id = (int)reader["TA_id"],
                    UserId = (int)reader["user_id"],
                    ThreadId = (int)reader["thread_id"],
                    Text = reader["text"].ToString(),
                    SendDate = (DateTime)reader["send_date"]
                });
            }

            return answers;
        }

        // Для получения всех ответов конкретного Thread'а
        public List<ThreadAnswer> GetThreadAnswers(Thread thread)
        {
            List<ThreadAnswer> answers = new List<ThreadAnswer>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter threadIdParameter = command.CreateParameter();
            threadIdParameter.DbType = System.Data.DbType.Int32;
            threadIdParameter.ParameterName = "@ThreadId";
            threadIdParameter.Value = thread.Id;

            command.Parameters.Add(threadIdParameter);
            command.CommandText = @"select * from thread_answers where thread_id = @ThreadId";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                answers.Add(new ThreadAnswer
                {
                    Id = (int)reader["TA_id"],
                    UserId = (int)reader["user_id"],
                    ThreadId = (int)reader["thread_id"],
                    Text = reader["text"].ToString(),
                    SendDate = (DateTime)reader["send_date"]
                });
            }

            return answers;
        }
    }
}
