using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class ProductService
    {
        public bool UpdateProduct(product product)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = System.Data.DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = product.name;

            DbParameter descriptionParameter = command.CreateParameter();
            descriptionParameter.DbType = System.Data.DbType.String;
            descriptionParameter.IsNullable = true;
            descriptionParameter.ParameterName = "@Description";
            descriptionParameter.Value = product.description;

            DbParameter priceParameter = command.CreateParameter();
            priceParameter.DbType = System.Data.DbType.Decimal;
            priceParameter.IsNullable = false;
            priceParameter.ParameterName = "@Price";
            priceParameter.Value = product.price;

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@productId";
            productIdParameter.Value = product.devoloper_id;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, descriptionParameter, priceParameter, productIdParameter });
            command.CommandText = "UPDATE dbo.Products SET Name = @Name Description = @Description Price = @Price WHERE ProductID = @productId";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public bool CreateProduct(product product)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = System.Data.DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = product.name;

            DbParameter descriptionParameter = command.CreateParameter();
            descriptionParameter.DbType = System.Data.DbType.String;
            descriptionParameter.IsNullable = true;
            descriptionParameter.ParameterName = "@Description";
            descriptionParameter.Value = product.description;

            DbParameter developerIdParameter = command.CreateParameter();
            developerIdParameter.DbType = System.Data.DbType.Int32;
            developerIdParameter.IsNullable = false;
            developerIdParameter.ParameterName = "@DeveloperId";
            developerIdParameter.Value = product.devoloper_id;

            DbParameter priceParameter = command.CreateParameter();
            priceParameter.DbType = System.Data.DbType.Decimal;
            priceParameter.IsNullable = false;
            priceParameter.ParameterName = "@Price";
            priceParameter.Value = product.price;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, descriptionParameter, developerIdParameter,
                                                                priceParameter});
            command.CommandText = @"insert into dbo.Developers
                        ([Name], [Description], [Developer_Id], [Price]) values
                        (@Name, @Descrtiption, @DeveloperId, @Price)";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<product> SelectAllProducts()
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            List<product> products = new List<product>();

            command.CommandText = "select * from dbo.Products";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(
                    new product
                    {
                        products_id = (int)reader["Product_id"],
                        name = reader["Name"].ToString(),
                        description = reader["Description"].ToString(),
                        positive_marks = (int)reader["Positive_Marks"],
                        negative_marks = (int)reader["Negative_Marks"],
                        devoloper_id = (int)reader["Developer_id"],
                        price = (decimal)reader["Price"],
                        IsDeleted = (byte)reader["IsDeleted"],
                        create_date = (DateTime)reader["create_date"]
                    });
            }
            return products;
        }

        public bool DeleteProduct(product product)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@ProductId";
            productIdParameter.Value = product.products_id;

            command.Parameters.Add(productIdParameter);
            command.CommandText = "UPDATE dbo.Products SET IsDelted = 1 WHERE ProductID = @ProductId";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<product_comments> GetProductComments(product product)
        {
            List<product_comments> productComments = new List<product_comments>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@ProductId";
            productIdParameter.Value = product.products_id;
            command.Parameters.Add(productIdParameter);

            command.CommandText = "select * from dbo.Product_comments where product_id = @ProductId";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productComments.Add(
                    new product_comments
                    {
                        PC_id = (int)reader["pc_id"],
                        product_id = (int)reader["product_id"],
                        user_id = (int)reader["user_id"],
                        text = reader["text"].ToString(),
                        send_date = (DateTime)reader["Send_date"]
                    });
            }

            return productComments;
        }
    }
}
