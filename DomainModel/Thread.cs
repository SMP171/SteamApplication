using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Thread
    {
        public int Id { get; set; }
        public int Creator_id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
