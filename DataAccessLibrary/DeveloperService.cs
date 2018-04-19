using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DomainModel;

namespace DataAccessLibrary
{
    public class DeveloperService
    {
        public void CreateDeveloper(Developer developer)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = developer.Name;

            DbParameter webSiteParameter = command.CreateParameter();
            webSiteParameter.DbType = DbType.String;
            webSiteParameter.IsNullable = true;
            webSiteParameter.ParameterName = "@WebSite";
            webSiteParameter.Value = developer.WebSite;

            command.Parameters.AddRange(new DbParameter[] { webSiteParameter, webSiteParameter });
            command.CommandText = @"INSERT INTO [dbo].[developers]([name],[website]) VALUES
                                        (@Name, @WebSite)";

            SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<Developer> SelectAllDevelopers()
        {
            List<Developer> developers = new List<Developer>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            command.CommandText = "select * from Developers";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                developers.Add(new Developer
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    WebSite = reader["Website"].ToString(),
                    IsDeleted = (bool)reader["IsDeleted"]
                });
            }

            return developers;
        }

        public List<Product> SelectDeveloperProducts(Developer developer)
        {
            List<Product> products = new List<Product>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter devIdParameter = command.CreateParameter();
            devIdParameter.DbType = DbType.Int32;
            devIdParameter.Value = developer.Id;
            devIdParameter.ParameterName = @"devId";

            command.CommandText = $@"select * from products where developer_id in (select developers_id from developers " +
                                                                        "where developers_id = @devId)";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductId = (int)reader["products_id"],
                    Name = reader["name"].ToString(),
                    Description = reader["description"].ToString(),
                    DeveloperId = (int)reader["developer_id"],
                    PositiveMarks = (int)reader["positive_marks"],
                    NegativeMarks = (int)reader["negative_marks"],
                    Price = (decimal)reader["price"],
                    IsDeleted = (bool)reader["IsDeleted"],
                    CreateDate = (DateTime)reader["create_date"]
                });
            }

            return products;
        }

        public void DeleteDeveloper(Developer developer)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();
            command.CommandText = $@"UPDATE [dbo].[developers] SET IsDeleted = 1 WHERE developers_id = {developer.Id}";

            SqlConncetionHelper.ExecuteCommands(command);
        }
    }
}
