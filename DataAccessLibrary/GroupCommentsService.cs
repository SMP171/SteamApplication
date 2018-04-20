using DataAccessLibrary.EntityFramework;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DataAccessLibrary
{
    public class GroupCommentsService
    {
        public void CreateGroupComment(group_comments groupComment)
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
            newRow["group_id"] = groupComment.Group_id;
            newRow["user_id"] = groupComment.User_id;
            newRow["comment_text"] = groupComment.Comment_text;

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
        public List<group_comments> SelectGroupComments(group group)
        {
            List<group_comments> groupComments = new List<group_comments>();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter groupParameter = command.CreateParameter();
                groupParameter.DbType = DbType.Int32;
                groupParameter.ParameterName = "@GroupId";
                groupParameter.Value = group.Group_id;

                command.Parameters.Add(groupParameter);
                command.CommandText = "select * from group_comments" +
                                      " where group_id = @GroupId";

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    group_comments comment = new group_comments()
                    {
                        Gc_id = (int)reader["gc_id"],
                        User_id = (int)reader["user_id"],
                        Group_id = (int)reader["group_id"],
                        Comment_text = reader["comment_text"].ToString(),
                        Send_date = (DateTime)reader["send_date"]
                    };
                    comment.User = GetGroupCommentUser(comment);

                    groupComments.Add(comment);
                }
            }

            return groupComments;
        }

        public user GetGroupCommentUser(group_comments comment)
        {
            user user = new user();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter groupParameter = command.CreateParameter();
                groupParameter.DbType = DbType.Int32;
                groupParameter.ParameterName = "@userId";
                groupParameter.Value = comment.User_id;

                command.Parameters.Add(groupParameter);
                command.CommandText = "select * from users" +
                                      " where user_id = @userId";

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.user_id = (int)reader["user_id"];
                    user.nickname = reader["nickname"].ToString();
                    user.password = reader["password"].ToString();
                    user.register_date = DateTime.Parse(reader["register_date"].ToString());
                    user.wallet_id = (int)reader["wallet_id"];
                    user.status_id = (int)reader["status_id"];
                    user.IsDeleted = (byte)(reader["IsDeleted"]);
                }
            }

            return user;
        }
    }
}
