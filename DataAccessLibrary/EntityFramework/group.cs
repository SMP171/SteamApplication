namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class group
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public group()
        {
            group_comments = new HashSet<group_comments>();
            groups_users = new HashSet<groups_users>();
        }

        [Key]
        public int group_id { get; set; }

        [Required]
        [StringLength(255)]
        public string group_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime created_date { get; set; }

        public int user_id { get; set; }

        public byte IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<group_comments> group_comments { get; set; }

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<groups_users> groups_users { get; set; }
    }
}
