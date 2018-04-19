using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class ProductService
    {
        public bool UpdateProduct(Product product)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = System.Data.DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = product.Name;

            DbParameter descriptionParameter = command.CreateParameter();
            descriptionParameter.DbType = System.Data.DbType.String;
            descriptionParameter.IsNullable = true;
            descriptionParameter.ParameterName = "@Description";
            descriptionParameter.Value = product.Description;

            DbParameter priceParameter = command.CreateParameter();
            priceParameter.DbType = System.Data.DbType.Decimal;
            priceParameter.IsNullable = false;
            priceParameter.ParameterName = "@Price";
            priceParameter.Value = product.Price;

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@productId";
            productIdParameter.Value = product.DeveloperId;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, descriptionParameter, priceParameter, productIdParameter });
            command.CommandText = "UPDATE dbo.Products SET Name = @Name Description = @Description Price = @Price WHERE ProductID = @productId";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public bool CreateProduct(Product product)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter nameParameter = command.CreateParameter();
            nameParameter.DbType = System.Data.DbType.String;
            nameParameter.IsNullable = false;
            nameParameter.ParameterName = "@Name";
            nameParameter.Value = product.Name;

            DbParameter descriptionParameter = command.CreateParameter();
            descriptionParameter.DbType = System.Data.DbType.String;
            descriptionParameter.IsNullable = true;
            descriptionParameter.ParameterName = "@Description";
            descriptionParameter.Value = product.Description;

            DbParameter developerIdParameter = command.CreateParameter();
            developerIdParameter.DbType = System.Data.DbType.Int32;
            developerIdParameter.IsNullable = false;
            developerIdParameter.ParameterName = "@DeveloperId";
            developerIdParameter.Value = product.DeveloperId;

            DbParameter priceParameter = command.CreateParameter();
            priceParameter.DbType = System.Data.DbType.Decimal;
            priceParameter.IsNullable = false;
            priceParameter.ParameterName = "@Price";
            priceParameter.Value = product.Price;

            command.Parameters.AddRange(new DbParameter[] { nameParameter, descriptionParameter, developerIdParameter,
                                                                priceParameter});
            command.CommandText = @"insert into dbo.Developers
                        ([Name], [Description], [Developer_Id], [Price]) values
                        (@Name, @Descrtiption, @DeveloperId, @Price)";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<Product> SelectAllProducts()
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            List<Product> products = new List<Product>();

            command.CommandText = "select * from dbo.Products";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(
                    new Product
                    {
                        ProductId = (int)reader["Product_id"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        PositiveMarks = (int)reader["Positive_Marks"],
                        NegativeMarks = (int)reader["Negative_Marks"],
                        DeveloperId = (int)reader["Developer_id"],
                        Price = (decimal)reader["Price"],
                        IsDeleted = (bool)reader["IsDeleted"],
                        CreateDate = (DateTime)reader["create_date"]
                    });
            }
            return products;
        }

        public bool DeleteProduct(Product product)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@ProductId";
            productIdParameter.Value = product.ProductId;

            command.Parameters.Add(productIdParameter);
            command.CommandText = "UPDATE dbo.Products SET IsDelted = 1 WHERE ProductID = @ProductId";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<ProductComment> GetProductComments(Product product)
        {
            List<ProductComment> productComments = new List<ProductComment>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@ProductId";
            productIdParameter.Value = product.ProductId;
            command.Parameters.Add(productIdParameter);

            command.CommandText = "select * from dbo.Product_comments where product_id = @ProductId";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productComments.Add(
                    new ProductComment
                    {
                        ProductCommentId = (int)reader["pc_id"],
                        ProductId = (int)reader["product_id"],
                        UserId = (int)reader["user_id"],
                        Text = reader["text"].ToString(),
                        DateSent = (DateTime)reader["Send_date"]
                    });
            }

            return productComments;
        }
    }
}
