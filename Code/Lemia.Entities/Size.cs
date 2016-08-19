using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.Entities
{
    public class Size
    {
        public int Id { set; get; }
        public string SizeName { set; get; }
        public string SizeAlias { set; get; }
        public int Status { set; get; }
        public DateTime CreateDate { set; get; }
    }
}
