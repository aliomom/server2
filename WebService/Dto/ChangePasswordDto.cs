using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Dto
{
    public class ChangePasswordDto
    {
        public string oldPassword { set; get; }
        public string newPassword { set; get; }
        public string confirmNewPassword { set; get; }
        public string UserName { set; get; }
        public string fullname { set; get; }
        public bool wantEditpassword { set; get; }
    }
}
