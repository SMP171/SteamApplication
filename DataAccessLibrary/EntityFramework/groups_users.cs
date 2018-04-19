namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class groups_users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int gu_id { get; set; }

        public int user_id { get; set; }

        public int group_id { get; set; }

        public virtual group group { get; set; }

        public virtual user user { get; set; }
    }
}
