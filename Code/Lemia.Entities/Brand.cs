using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.Entities
{
    public class Brand
    {
        public int Id { set; get; }
        public string BrandName { set; get; }
        public string BrandAlias { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public int Status { set; get; }
        public DateTime CreateDate { set; get; }
        public string Description { set; get; }


    }
}
