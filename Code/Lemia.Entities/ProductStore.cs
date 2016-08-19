using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.Entities
{
    public class ProductStore
    {
        
        public long Id { set; get; }
        public int ProductId { set; get; }
        public int StoreId { set; get; }
        public int Counter { set; get; }
        public int Status { set; get; }
        public DateTime CreateDate { set; get; }
        public int CreateBy { set; get; }
        
    }
}

