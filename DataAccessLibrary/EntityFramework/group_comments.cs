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
        public int Gc_id { get; set; }

        public int Group_id { get; set; }

        public int User_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Comment_text { get; set; }

        public DateTime? Send_date { get; set; }

        public virtual group Group { get; set; }

        public virtual user User { get; set; }
    }
}
