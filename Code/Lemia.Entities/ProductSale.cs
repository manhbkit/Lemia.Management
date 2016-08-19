using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.Entities
{
    public class ProductSale
    {
       
        public long Id { set; get; }
        public int ProductId { set; get; }
        public int SaleId { set; get; }
        public int Counter { set; get; }
        public int Price { set; get; }
        public int Status { set; get; }
        public DateTime CreateDate { set; get; }
        public int CreateBy { set; get; }

    }
}
