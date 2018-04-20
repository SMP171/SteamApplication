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
            Group_comments = new HashSet<group_comments>();
            Groups_users = new HashSet<groups_users>();
        }

        [Key]
        public int Group_id { get; set; }

        [Required]
        [StringLength(255)]
        public string Group_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime Created_date { get; set; }

        public int User_id { get; set; }

        public byte IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<group_comments> Group_comments { get; set; }

        public virtual user User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<groups_users> Groups_users { get; set; }
    }
}
