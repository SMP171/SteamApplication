namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class friend_messages
    {
        [Key]
        public int FM_id { get; set; }

        public int user_id { get; set; }

        public int friend_id { get; set; }

        [Required]
        public string message { get; set; }

        public DateTime send_date { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }
    }
}
