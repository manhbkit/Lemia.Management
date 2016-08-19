using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.Entities
{
    public class Sale
    {
        
        public int Id { set; get; }
        public string SaleName { set; get; }
        public string SaleAlias { set; get; }
        public string SaleAvatar { set; get; }
        public string Description { set; get; }
        public string TitleSEO { set; get; }
        public string MetaDes { set; get; }

        public string MetaKey { set; get; }
        public int Status { set; get; }
        public DateTime CreateDate { set; get; }
        public int CreateBy { set; get; }
    }
}
