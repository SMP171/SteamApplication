using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class DeveloperService
    {
        public void CreateDeveloper(developer developer)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = developer.name;

            DbParameter webSiteParameter = command.CreateParameter();
            webSiteParameter.DbType = DbType.String;
            webSiteParameter.IsNullable = true;
            webSiteParameter.ParameterName = "@WebSite";
            webSiteParameter.Value = developer.website;

            command.Parameters.AddRange(new DbParameter[] { webSiteParameter, webSiteParameter });
            command.CommandText = @"INSERT INTO [dbo].[developers]([name],[website]) VALUES
                                        (@Name, @WebSite)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<developer> SelectAllDevelopers()
        {
            List<developer> developers = new List<developer>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            command.CommandText = "select * from Developers";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                developers.Add(new developer
                {
                    developers_id = (int)reader["Id"],
                    name = reader["Name"].ToString(),
                    website = reader["Website"].ToString(),
                    IsDeleted = (byte)reader["IsDeleted"]
                });
            }

            return developers;
        }

        public List<product> SelectDeveloperProducts(developer developer)
        {
            List<product> products = new List<product>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter devIdParameter = command.CreateParameter();
            devIdParameter.DbType = DbType.Int32;
            devIdParameter.Value = developer.developers_id;
            devIdParameter.ParameterName = @"devId";

            command.CommandText = $@"select * from products where developer_id in (select developers_id from developers " +
                                                                        "where developers_id = @devId)";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new product
                {
                    products_id = (int)reader["products_id"],
                    name = reader["name"].ToString(),
                    description = reader["description"].ToString(),
                    devoloper_id = (int)reader["developer_id"],
                    positive_marks = (int)reader["positive_marks"],
                    negative_marks = (int)reader["negative_marks"],
                    price = (decimal)reader["price"],
                    IsDeleted = (byte)reader["IsDeleted"],
                    create_date = (DateTime)reader["create_date"]
                });
            }

            return products;
        }

        public void DeleteDeveloper(developer developer)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = $@"UPDATE [dbo].[developers] SET IsDeleted = 1 WHERE developers_id = {developer.developers_id}";

            SqlConncetionHelper.ExecuteCommands(command);
        }
    }

    public class Developer
    {
    }
}
