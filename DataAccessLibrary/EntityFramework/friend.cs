namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class friend
    {
        public int id { get; set; }

        public int user_id { get; set; }

        public int friend_id { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }
    }
}
