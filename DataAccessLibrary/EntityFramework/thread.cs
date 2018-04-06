namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class thread
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public thread()
        {
            thread_answers = new HashSet<thread_answers>();
            threads_users = new HashSet<threads_users>();
        }

        [Key]
        public int thread_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int creator_id { get; set; }

        [Required]
        [StringLength(1000)]
        public string text { get; set; }

        public DateTime create_date { get; set; }

        public byte IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<thread_answers> thread_answers { get; set; }

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<threads_users> threads_users { get; set; }
    }
}
