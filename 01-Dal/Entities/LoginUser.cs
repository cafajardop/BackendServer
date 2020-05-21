using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _01_Dal.Entities
{
    public class LoginUser
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string Password { get; set; }

    }   
}
