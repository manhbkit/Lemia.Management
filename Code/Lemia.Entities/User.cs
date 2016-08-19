using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.Entities
{
    public class User
    {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Phone { set; get; }
        public int Status { set; get; }
        public int RoleId { set; get; }
        public DateTime CreateDate { set; get; }
        public string Description { set; get; }

    }
}

