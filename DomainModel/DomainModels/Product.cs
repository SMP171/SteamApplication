using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class Product
    {
        //ставим int? для того чтобы можно было null
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PositiveMarks { get; set; }
        public int NegativeMarks { get; set; }
        public int DeveloperId { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
