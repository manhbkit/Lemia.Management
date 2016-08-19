using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.Entities
{
    public class Store
    {
        public int Id { set; get; }
        public string StoreName { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Longitude { set; get; }
        public string Latitude { set; get; }
        public string Avatar { set; get; }

    }
}
