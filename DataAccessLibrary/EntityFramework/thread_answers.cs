namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class thread_answers
    {
        [Key]
        public int TA_id { get; set; }

        public int user_id { get; set; }

        public int thread_id { get; set; }

        public DateTime send_date { get; set; }

        [Required]
        [StringLength(1000)]
        public string text { get; set; }

        public virtual thread thread { get; set; }

        public virtual user user { get; set; }
    }
}
