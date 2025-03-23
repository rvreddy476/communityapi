using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Models.Models
{
    public class UserLoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
}
