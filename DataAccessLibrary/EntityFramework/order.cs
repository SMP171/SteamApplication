namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        [Key]
        public int order_id { get; set; }

        public int user_id { get; set; }

        public int product_id { get; set; }

        public DateTime? order_date { get; set; }

        public int status_id { get; set; }

        public virtual order_statuses order_statuses { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
