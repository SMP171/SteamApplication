namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_comments
    {
        [Key]
        public int PC_id { get; set; }

        public int product_id { get; set; }

        public int user_id { get; set; }

        [Required]
        public string text { get; set; }

        public DateTime send_date { get; set; }

        public int comment_mark { get; set; }

        public virtual productComment_marks productComment_marks { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
