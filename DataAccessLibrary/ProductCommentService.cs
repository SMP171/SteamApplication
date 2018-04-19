using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.EntityFramework;

namespace DataAccessLibrary
{
    public class ProductCommentService
    {
        public bool CreateProductComment(product_comments comment)
        {
            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter productIdParameter = command.CreateParameter();
            productIdParameter.DbType = System.Data.DbType.Int32;
            productIdParameter.IsNullable = false;
            productIdParameter.ParameterName = "@productId";
            productIdParameter.Value = comment.product_id;

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@userId";
            userIdParameter.Value = comment.user_id;

            DbParameter textParameter = command.CreateParameter();
            textParameter.DbType = System.Data.DbType.String;
            textParameter.IsNullable = false;
            textParameter.ParameterName = "@text";
            textParameter.Value = comment.text;

            DbParameter markIdParameter = command.CreateParameter();
            markIdParameter.DbType = System.Data.DbType.Int32;
            markIdParameter.IsNullable = false;
            markIdParameter.ParameterName = "@markId";
            //markIdParameter.Value = comment.id;

            command.Parameters.AddRange(new DbParameter[] { productIdParameter, userIdParameter, textParameter, markIdParameter });
            command.CommandText = @"insert into dbo.product_comments
                        ([product_id],[user_id], [text], [mark_id]) values
                        (@productId, @userId, @text, @markId)";

            return SqlConncetionHelper.ExecuteCommands(command);
        }

        public List<product_comments> SelectAllProductComments()
        {
            List<product_comments> comments = new List<product_comments>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            command.CommandText = "select * from dbo.Product_comments";

            DbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comments.Add(
                    new product_comments
                    {
                        PC_id = (int)reader["PC_id"],
                        product_id = (int)reader["Product_id"],
                        user_id = (int)reader["User_id"],
                        text = reader["Text"].ToString(),
                        send_date = (DateTime)reader["Send_date"],
                        comment_mark = (int)reader["Mark_id"]
                    });
            }

            return comments;
        }

        public List<product_comments> SelectUserProductComments(user user)
        {
            List<product_comments> comments = new List<product_comments>();

            DbCommand command = SqlConncetionHelper.Connection.CreateCommand();

            DbParameter userIdParameter = command.CreateParameter();
            userIdParameter.DbType = System.Data.DbType.Int32;
            userIdParameter.IsNullable = false;
            userIdParameter.ParameterName = "@userId";
            userIdParameter.Value = user.user_id;
            command.Parameters.Add(userIdParameter);

            command.CommandText = "select * from dbo.Product_comments where user_id = @userId";

            DbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comments.Add(
                    new product_comments
                    {
                        PC_id = (int)reader["PC_id"],
                        product_id = (int)reader["Product_id"],
                        user_id = (int)reader["User_id"],
                        text = reader["Text"].ToString(),
                        send_date = (DateTime)reader["Send_date"],
                        comment_mark = (int)reader["Mark_id"]
                    });
            }

            return comments;
        }
    }
}
