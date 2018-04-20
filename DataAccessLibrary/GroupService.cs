using DataAccessLibrary.EntityFramework;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DataAccessLibrary
{
    public class GroupService
    {
        public void CreateGroup(group group)
        {
            #region Подключенный режим
            //DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            //DbParameter nameParameter = command.CreateParameter();
            //nameParameter.DbType = System.Data.DbType.String;
            //nameParameter.IsNullable = false;
            //nameParameter.ParameterName = "@Name";
            //nameParameter.Value = group.Name;

            //DbParameter userIdParameter = command.CreateParameter();
            //userIdParameter.DbType = System.Data.DbType.Int32;
            //userIdParameter.IsNullable = false;
            //userIdParameter.ParameterName = "@UserId";
            //userIdParameter.Value = group.UserId;

            //command.Parameters.AddRange(new DbParameter[] { nameParameter, userIdParameter });
            //command.CommandText = @"INSERT INTO [dbo].[groups]([group_name],[user_id]) VALUES
            //                            (@Name, @UserId)";

            //return SqlConncetionHelper.ExecuteCommands(command);
            #endregion

            #region Отключенный режим
            DbDataAdapter adapter = SqlConncetionHelper.DataAdapter;
            DbCommand selectCommand = SqlConncetionHelper.Connection.CreateCommand();
            selectCommand.CommandText = "select * from dbo.groups";
            DbCommandBuilder builder = SqlConncetionHelper.ProviderFactory.CreateCommandBuilder();
            builder.DataAdapter = adapter;

            adapter.SelectCommand = selectCommand;
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            DataTable table = ds.Tables[0];
            DataRow newRow = table.NewRow();
            newRow["group_name"] = group.Group_name;
            newRow["user_id"] = group.User_id;
            newRow["created_date"] = DateTime.Now;
            table.Rows.Add(newRow);

            adapter.Update(ds);
            ds.AcceptChanges();
            #endregion
        }

        public List<group> SelectAllGroups()
        {
            List<group> groups = new List<group>();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();

                DbCommand command = connection.CreateCommand();
                command.CommandText = "select * from groups where IsDeleted = 0";

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    group newGroup = new group()
                    {
                        Group_id = (int)reader["group_id"],
                        Group_name = reader["group_name"].ToString(),
                        Created_date = DateTime.Parse(reader["created_date"].ToString()),
                        User_id = (int)reader["user_id"],
                        IsDeleted = (byte)(reader["IsDeleted"]),
                    };
                    //newGroup.Members = SelectGroupMembers(newGroup);

                    groups.Add(newGroup);
                }
            }

            return groups;
        }

        //Entity Framework implementation
        public List<group> SelectUserGroups(User user)
        {
            #region Подключенный режим
            //List<Group> groups = new List<Group>();

            //DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            //DbParameter userIdParameter = command.CreateParameter();
            //userIdParameter.DbType = System.Data.DbType.Int32;
            //userIdParameter.ParameterName = "@UserId";
            //userIdParameter.Value = user.Id;

            //command.Parameters.Add(userIdParameter);
            //command.CommandText = "select * from groups where user_id = @UserId";
            //DbDataReader reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    groups.Add(new Group
            //    {
            //        Id = (int)reader["group_id"],
            //        Name = reader["group_name"].ToString(),
            //        CreatedDate = (DateTime)reader["created_date"],
            //        UserId = (int)reader["user_id"],
            //        IsDeleted = (bool)reader["IsDeleted"]
            //    });
            //}

            //return groups;
            #endregion

            using (var context = new SteamContext())
            {
                List<group> groups = new List<group>();
                groups = context.Groups.Where(x => x.User_id == user.Id).ToList();

                return groups;
            }
        }

        public List<user> SelectGroupMembers(group group)
        {
            List<user> members = new List<user>();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter userIdParameter = command.CreateParameter();
                userIdParameter.DbType = DbType.Int32;
                userIdParameter.ParameterName = "@GroupId";
                userIdParameter.Value = group.Group_id;

                command.Parameters.Add(userIdParameter);
                command.CommandText = "exec GetGroupMembers @GroupId";
                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    members.Add(new user
                    {
                        user_id = (int)reader["user_id"],
                        nickname = reader["nickname"].ToString(),
                        password = reader["password"].ToString(),
                        register_date = DateTime.Parse(reader["register_date"].ToString()),
                        wallet_id = (int)reader["wallet_id"],
                        status_id = (int)reader["status_id"],
                        IsDeleted = (byte)(reader["IsDeleted"])
                    });
                }
            }

            return members;
        }

        public bool DeleteGroup(group group)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter groupIdParameter = command.CreateParameter();
            groupIdParameter.DbType = DbType.Int32;
            groupIdParameter.ParameterName = "@GroupId";
            groupIdParameter.Value = group.Group_id;

            command.Parameters.Add(groupIdParameter);
            command.CommandText = "update groups set IsDeleted = 1 where group_id = @GroupId";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public group FindGroupByName(string Name)
        {
            group group = new group();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();

                DbCommand command = connection.CreateCommand();
                DbParameter nameParameter = command.CreateParameter();
                nameParameter.DbType = DbType.String;
                nameParameter.Value = Name;
                nameParameter.ParameterName = "@name";

                command.Parameters.Add(nameParameter);
                command.CommandText = @"select * from groups where group_name = @name";

                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    group.Group_id = (int)reader["group_id"];
                    group.Group_name = reader["group_name"].ToString();
                    group.Created_date = DateTime.Parse(reader["created_date"].ToString());
                    group.User_id = (int)reader["user_id"];
                    group.IsDeleted = (byte)(reader["IsDeleted"]);
                }
            }

            return group;
        }

        public void AddUserToGroup(group group, user user)
        {
            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter userIdParameter = command.CreateParameter();
                userIdParameter.DbType = DbType.Int32;
                userIdParameter.Value = user.user_id;
                userIdParameter.ParameterName = "@userId";

                DbParameter groupIdParameter = command.CreateParameter();
                groupIdParameter.DbType = DbType.Int32;
                groupIdParameter.Value = group.Group_id;
                groupIdParameter.ParameterName = "@groupId";

                command.Parameters.AddRange(new DbParameter[] { userIdParameter, groupIdParameter });
                command.CommandText = @"insert into groups_users values (@userId, @groupId)";

                SqlConncetionHelper.ExecuteCommands(command);
            }
        }

        public void DeleteUserFromGroup(group group, user user)
        {
            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter userIdParameter = command.CreateParameter();
                userIdParameter.DbType = DbType.Int32;
                userIdParameter.Value = user.user_id;
                userIdParameter.ParameterName = "@userId";

                DbParameter groupIdParameter = command.CreateParameter();
                groupIdParameter.DbType = DbType.Int32;
                groupIdParameter.Value = group.Group_id;
                groupIdParameter.ParameterName = "@groupId";

                command.Parameters.AddRange(new DbParameter[] { userIdParameter, groupIdParameter });
                command.CommandText = @"delete groups_users where group_id = @groupId and user_id = @userId";

                SqlConncetionHelper.ExecuteCommands(command);
            }
        }
    }
}
