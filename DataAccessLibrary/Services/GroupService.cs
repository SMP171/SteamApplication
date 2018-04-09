using DataAccessLibrary.EntityFramework;
using DomainModel.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DataAccessLibrary.Services
{
    public class GroupService
    {
        public void CreateGroup(Group group)
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
            newRow["group_name"] = group.Name;
            newRow["user_id"] = group.UserId;
            newRow["created_date"] = DateTime.Now;
            table.Rows.Add(newRow);

            adapter.Update(ds);
            ds.AcceptChanges();
            #endregion
        }

        public List<Group> SelectAllGroups()
        {
            List<Group> groups = new List<Group>();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();

                DbCommand command = connection.CreateCommand();
                command.CommandText = "select * from groups";

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    groups.Add(new Group
                    {
                        Id = (int)reader["group_id"],
                        Name = reader["group_name"].ToString(),
                        CreatedDate = DateTime.Parse(reader["created_date"].ToString()),
                        UserId = (int)reader["user_id"],
                        IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                    });
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

        public List<User> SelectGroupMembers(Group group)
        {
            List<User> members = new List<User>();

            using (var connection = SqlConncetionHelper.Connection)
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();

                DbParameter userIdParameter = command.CreateParameter();
                userIdParameter.DbType = DbType.Int32;
                userIdParameter.ParameterName = "@GroupId";
                userIdParameter.Value = group.Id;

                command.Parameters.Add(userIdParameter);
                command.CommandText = "exec GetGroupMembers @GroupId";
                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    members.Add(new User
                    {
                        Id = (int)reader["user_id"],
                        Nickname = reader["nickname"].ToString(),
                        Password = reader["password"].ToString(),
                        RegisterDate = DateTime.Parse(reader["register_date"].ToString()),
                        WalletId = (int)reader["wallet_id"],
                        StatusId = (int)reader["status_id"],
                        IsDeleted = Convert.ToBoolean(reader["IsDeleted"])
                    });
                }
            }

            return members;
        }

        public bool DeleteGroup(Group group)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter groupIdParameter = command.CreateParameter();
            groupIdParameter.DbType = DbType.Int32;
            groupIdParameter.ParameterName = "@GroupId";
            groupIdParameter.Value = group.Id;

            command.Parameters.Add(groupIdParameter);
            command.CommandText = "update groups set IsDeleted = 1 where group_id = @GroupId";

            return SqlConncetionHelper.ExecuteCommands(command);
        }
    }
}
