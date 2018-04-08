using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class ProductComment
    {
        public int ProductCommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateSent { get; set; }
        public int MarkId { get; set; }
    }
}