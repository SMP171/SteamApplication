namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class users_products
    {
        [Key]
        public int up_id { get; set; }

        public int user_id { get; set; }

        public int? product_id { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
