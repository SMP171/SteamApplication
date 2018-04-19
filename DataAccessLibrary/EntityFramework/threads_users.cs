namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class threads_users
    {
        [Key]
        public int tu_id { get; set; }

        public int user_id { get; set; }

        public int thread_id { get; set; }

        public virtual thread thread { get; set; }

        public virtual user user { get; set; }
    }
}
