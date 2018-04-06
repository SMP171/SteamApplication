namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class group_comments
    {
        [Key]
        public int gc_id { get; set; }

        public int group_id { get; set; }

        public int user_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string comment_text { get; set; }

        public DateTime? send_date { get; set; }

        public virtual group group { get; set; }

        public virtual user user { get; set; }
    }
}
