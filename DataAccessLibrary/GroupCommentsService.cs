//using DomainModel;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLibrary
//{
//    public class GroupCommentsService
//    {
//        public void CreateGroupComment(GroupComments groupComment)
//        {
//            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

//            DbParameter groupIdParameter = command.CreateParameter();
//            groupIdParameter.DbType = System.Data.DbType.Int32;
//            groupIdParameter.IsNullable = false;
//            groupIdParameter.ParameterName = "@GroupId";
//            groupIdParameter.Value = groupComment.GroupId;

//            DbParameter userIdParameter = command.CreateParameter();
//            userIdParameter.DbType = System.Data.DbType.Int32;
//            userIdParameter.IsNullable = false;
//            userIdParameter.ParameterName = "@UserId";
//            userIdParameter.Value = groupComment.UserId;

//            DbParameter textParameter = command.CreateParameter();
//            textParameter.DbType = System.Data.DbType.String;
//            textParameter.IsNullable = false;
//            textParameter.ParameterName = "@Text";
//            textParameter.Value = groupComment.Text;

//            command.Parameters.AddRange(new DbParameter[] { groupIdParameter, userIdParameter, textParameter });
//            command.CommandText = @"INSERT INTO [dbo].[group_comments]([group_id],[user_id],[comment_text]) VALUES
//                                        (@GroupId, @UserId, @Text)";

//            SqlConncetionHelper.ExecuteCommands(command);
//        }

//        public List<GroupComments> SelectAllGroupComments()
//        {
//            List<GroupComments> groupComments = new List<GroupComments>();

//            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

//            command.CommandText = "select * from group_comments";

//            DbDataReader reader = command.ExecuteReader();

//            while (reader.Read())
//            {
//                groupComments.Add(new GroupComments
//                {
//                    Id = (int)reader["gc_id"],
//                    UserId = (int)reader["user_id"],
//                    GroupId = (int)reader["group_id"],
//                    Text = reader["comment_text"].ToString(),
//                    SendDate = (DateTime)reader["send_date"]
//                });
//            }

//            return groupComments;
//        }

//        /// <summary>
//        /// Метод для получения комментариев конкретной группы
//        /// </summary>
//        /// <param name="group">Группа, комментарии которой нужно получить</param>
//        /// <returns></returns>
//        public List<GroupComments> SelectGroupComments(Group group)
//        {
//            List<GroupComments> groupComments = new List<GroupComments>();

//            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

//            DbParameter groupParameter = command.CreateParameter();
//            groupParameter.DbType = System.Data.DbType.Int32;
//            groupParameter.ParameterName = "@GroupId";
//            groupParameter.Value = group.Id;

//            command.Parameters.Add(groupParameter);
//            command.CommandText = "select * from group_comments" +
//                                  " where group_id = @GroupId";

//            DbDataReader reader = command.ExecuteReader();

//            while (reader.Read())
//            {
//                groupComments.Add(new GroupComments
//                {
//                    Id = (int)reader["gc_id"],
//                    UserId = (int)reader["user_id"],
//                    GroupId = (int)reader["group_id"],
//                    Text = reader["comment_text"].ToString(),
//                    SendDate = (DateTime)reader["send_date"]
//                });
//            }

//            return groupComments;
//        }
//    }
//}
