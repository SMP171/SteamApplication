using System;
using System.Collections.Generic;
using System.Data.Common;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class ThreadAnswerService
    {
        public void CreateThreadAnswer(thread_answers answer)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter threadIdParameter = command.CreateParameter();
            threadIdParameter.DbType = System.Data.DbType.Int32;
            threadIdParameter.IsNullable = false;
            threadIdParameter.ParameterName = "@Thread_id";
            threadIdParameter.Value = answer.thread_id;

            DbParameter textParameter = command.CreateParameter();
            textParameter.DbType = System.Data.DbType.String;
            textParameter.IsNullable = false;
            textParameter.ParameterName = "@Text";
            textParameter.Value = answer.text;

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = answer.user_id;

            command.Parameters.AddRange(new DbParameter[] { threadIdParameter, textParameter, userIdParameter });

            command.CommandText = @"INSERT INTO [dbo].[thread_answers]([thread_id],[user_id],[text]) VALUES
                                        (@Thread_id, @UserId, @Text)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<thread_answers> GetAllThreadsAnswers()
        {
            List<thread_answers> answers = new List<thread_answers>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = "select * from thread_answers";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                answers.Add(new thread_answers
                {
                    TA_id = (int)reader["TA_id"],
                    user_id = (int)reader["user_id"],
                    thread_id = (int)reader["thread_id"],
                    text = reader["text"].ToString(),
                    send_date = (DateTime)reader["send_date"]
                });
            }

            return answers;
        }

        // Для получения всех ответов конкретного Thread'а
        public List<thread_answers> GetThreadAnswers(thread thread)
        {
            List<thread_answers> answers = new List<thread_answers>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter threadIdParameter = command.CreateParameter();
            threadIdParameter.DbType = System.Data.DbType.Int32;
            threadIdParameter.ParameterName = "@ThreadId";
            threadIdParameter.Value = thread.thread_id;

            command.Parameters.Add(threadIdParameter);
            command.CommandText = @"select * from thread_answers where thread_id = @ThreadId";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                answers.Add(new thread_answers
                {
                    TA_id = (int)reader["TA_id"],
                    user_id = (int)reader["user_id"],
                    thread_id = (int)reader["thread_id"],
                    text = reader["text"].ToString(),
                    send_date = (DateTime)reader["send_date"]
                });
            }

            return answers;
        }
    }
}
