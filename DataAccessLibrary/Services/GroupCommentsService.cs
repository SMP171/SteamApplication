using DataAccessLibrary.EntityFramework;
using DomainModel.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DataAccessLibrary.Services
{
    public class GroupCommentsService
    {
        public void CreateGroupComment(GroupComments groupComment)
        {
            #region Подключенный режим
            //DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            //DbParameter groupIdParameter = command.CreateParameter();
            //groupIdParameter.DbType = System.Data.DbType.Int32;
            //groupIdParameter.IsNullable = false;
            //groupIdParameter.ParameterName = "@GroupId";
            //groupIdParameter.Value = groupComment.GroupId;

            //DbParameter userIdParameter = command.CreateParameter();
            //userIdParameter.DbType = System.Data.DbType.Int32;
            //userIdParameter.IsNullable = false;
            //userIdParameter.ParameterName = "@UserId";
            //userIdParameter.Value = groupComment.UserId;

            //DbParameter textParameter = command.CreateParameter();
            //textParameter.DbType = System.Data.DbType.String;
            //textParameter.IsNullable = false;
            //textParameter.ParameterName = "@Text";
            //textParameter.Value = groupComment.Text;

            //command.Parameters.AddRange(new DbParameter[] { groupIdParameter, userIdParameter, textParameter });
            //command.CommandText = @"INSERT INTO [dbo].[group_comments]([group_id],[user_id],[comment_text]) VALUES
            //                            (@GroupId, @UserId, @Text)";

            //SqlConncetionHelper.ExecuteCommands(command);
            #endregion

            //Отключеный режим
            DbDataAdapter adapter = SqlConncetionHelper.DataAdapter;
            DbCommand selectCommand = SqlConncetionHelper.Connection.CreateCommand();
            selectCommand.CommandText = "select * from group_comments";
            DbCommandBuilder builder = SqlConncetionHelper.ProviderFactory.CreateCommandBuilder();
            builder.DataAdapter = adapter;
            adapter.SelectCommand = selectCommand;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            DataTable table = ds.Tables[0];
            DataRow newRow = table.NewRow();
            newRow["group_id"] = groupComment.GroupId;
            newRow["user_id"] = groupComment.UserId;
            newRow["comment_text"] = groupComment.Text;

            table.Rows.Add(newRow);
            adapter.Update(ds);
            ds.AcceptChanges();
        }

        public List<group_comments> SelectAllGroupComments()
        {
            #region Подключенный режим
            //List<GroupComments> groupComments = new List<GroupComments>();

            //DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            //command.CommandText = "select * from group_comments";

            //DbDataReader reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    groupComments.Add(new GroupComments
            //    {
            //        Id = (int)reader["gc_id"],
            //        UserId = (int)reader["user_id"],
            //        GroupId = (int)reader["group_id"],
            //        Text = reader["comment_text"].ToString(),
            //        SendDate = (DateTime)reader["send_date"]
            //    });
            //}
            #endregion

            //Entity Framwork implementation
            List<group_comments> comments = new List<group_comments>();

            using (var context = new SteamContext())
            {
                comments = context.Group_comments.ToList();
            }

            return comments;
        }

        /// <summary>
        /// Метод для получения комментариев конкретной группы
        /// </summary>
        /// <param name="group">Группа, комментарии которой нужно получить</param>
        /// <returns></returns>
        public List<GroupComments> SelectGroupComments(Group group)
        {
            List<GroupComments> groupComments = new List<GroupComments>();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter groupParameter = command.CreateParameter();
                groupParameter.DbType = DbType.Int32;
                groupParameter.ParameterName = "@GroupId";
                groupParameter.Value = group.Id;

                command.Parameters.Add(groupParameter);
                command.CommandText = "select * from group_comments" +
                                      " where group_id = @GroupId";

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    groupComments.Add(new GroupComments
                    {
                        Id = (int)reader["gc_id"],
                        UserId = (int)reader["user_id"],
                        GroupId = (int)reader["group_id"],
                        Text = reader["comment_text"].ToString(),
                        SendDate = (DateTime)reader["send_date"]
                    });
                }
            }

            return groupComments;
        }
    }
}
