using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class GroupService
    {
        public bool CreateGroup(Group group)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = System.Data.DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = group.Name;

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = group.UserId;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, userIdParameter });
            command.CommandText = @"INSERT INTO [dbo].[groups]([group_name],[user_id]) VALUES
                                        (@Name, @UserId)";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<Group> SelectAllGroups()
        {
            List<Group> groups = new List<Group>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            command.CommandText = "select * from groups";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                groups.Add(new Group
                {
                    Id = (int)reader["group_id"],
                    Name = reader["group_name"].ToString(),
                    CreatedDate = (DateTime)reader["created_date"],
                    UserId = (int)reader["user_id"],
                    IsDeleted = (bool)reader["IsDeleted"]
                });
            }

            return groups;
        }

        public List<Group> SelectUserGroups(User user)
        {
            List<Group> groups = new List<Group>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.ParameterName = "@UserId";
            userIdParameter.Value = user.Id;

            command.Parameters.Add(userIdParameter);
            command.CommandText = "select * from groups where user_id = @UserId";
            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                groups.Add(new Group
                {
                    Id = (int)reader["group_id"],
                    Name = reader["group_name"].ToString(),
                    CreatedDate = (DateTime)reader["created_date"],
                    UserId = (int)reader["user_id"],
                    IsDeleted = (bool)reader["IsDeleted"]
                });
            }

            return groups;
        }

        public List<User> SelectGroupMembers(Group group)
        {
            List<User> members = new List<User>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
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
                    RegisterDate = (DateTime)reader["register_date"],
                    WalletId = (int)reader["wallet_id"],
                    StatusId = (int)reader["status_id"],
                    IsDeleted = (bool)reader["IsDeleted"]
                });
            }

            return members;
        }

        public bool DeleteGroup(Group group)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter groupIdParameter = command.CreateParameter();
            groupIdParameter.DbType = System.Data.DbType.Int32;
            groupIdParameter.ParameterName = "@GroupId";
            groupIdParameter.Value = group.Id;

            command.Parameters.Add(groupIdParameter);
            command.CommandText = "update groups set IsDeleted = 1 where group_id = @GroupId";

            return SqlConncetionHelper.ExecuteCommands(command);
        }
    }
}
