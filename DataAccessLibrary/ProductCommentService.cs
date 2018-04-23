using DataAccessLibrary.EntityFramework;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccessLibrary
{
    public class ProductCommentService
    {
        public bool CreateProductComment(product_comments comment)
        {
            SteamContext context = new SteamContext();
            product_comments newComment = new product_comments();

            newComment.product_id = comment.product_id;
            newComment.user_id = comment.user_id;
            newComment.text = comment.text;
            newComment.send_date = comment.send_date;
            newComment.comment_mark = comment.comment_mark;

            context.Product_comments.Add(newComment);

            if (context.SaveChanges() == 0)
            {
                return false;
            }
            return true;
        }

        public List<product_comments> SelectAllProductComments()
        {
            SteamContext context = new SteamContext();
            return context.Product_comments.ToList();
        }

        public List<product_comments> SelectUserProductComments(user user)
        {
            List<ProductComment> comments = new List<ProductComment>();
            SteamContext context = new SteamContext();
            return context.Product_comments.Where(comment => comment.user_id == user.user_id).ToList();
        }

        public List<product_comments> SelectProductComments(product product)
        {
            List<ProductComment> comments = new List<ProductComment>();
            SteamContext context = new SteamContext();
            return context.Product_comments.Where(comment => comment.product_id == product.products_id).ToList();
        }
    }
}
