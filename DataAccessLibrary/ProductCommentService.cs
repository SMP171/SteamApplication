using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLibrary
{
    public class ProductCommentService
    {
        public bool CreateProductComment(ProductComment comment)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@productId";
            productIdParameter.Value = comment.ProductId;

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@userId";
            userIdParameter.Value = comment.UserId;

            DbParameter textParameter = command.CreateParameter();
            textParameter.DbType = System.Data.DbType.String;
            textParameter.IsNullable = false;
            textParameter.ParameterName = "@text";
            textParameter.Value = comment.Text;

            DbParameter markIdParameter = command.CreateParameter();
            markIdParameter.DbType = System.Data.DbType.Int32;
            markIdParameter.IsNullable = false;
            markIdParameter.ParameterName = "@markId";
            markIdParameter.Value = comment.MarkId;

            command.Parameters.AddRange(new DbParameter[] { productIdParameter, userIdParameter, textParameter, markIdParameter });
            command.CommandText = @"insert into dbo.product_comments
                        ([product_id],[user_id], [text], [mark_id]) values
                        (@productId, @userId, @text, @markId)";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<ProductComment> SelectAllProductComments()
        {
            List<ProductComment> comments = new List<ProductComment>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            command.CommandText = "select * from dbo.Product_comments";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comments.Add(
                    new ProductComment
                    {
                        ProductCommentId = (int)reader["PC_id"],
                        ProductId = (int)reader["Product_id"],
                        UserId = (int)reader["User_id"],
                        Text = reader["Text"].ToString(),
                        DateSent = (DateTime)reader["Send_date"],
                        MarkId = (int)reader["Mark_id"]
                    });
            }

            return comments;
        }

        public List<ProductComment> SelectUserProductComments(User user)
        {
            List<ProductComment> comments = new List<ProductComment>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@userId";
            userIdParameter.Value = user.Id;
            command.Parameters.Add(userIdParameter);

            command.CommandText = "select * from dbo.Product_comments where user_id = @userId";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comments.Add(
                    new ProductComment
                    {
                        ProductCommentId = (int)reader["PC_id"],
                        ProductId = (int)reader["Product_id"],
                        UserId = (int)reader["User_id"],
                        Text = reader["Text"].ToString(),
                        DateSent = (DateTime)reader["Send_date"],
                        MarkId = (int)reader["Mark_id"]
                    });
            }

            return comments;
        }
    }
}
