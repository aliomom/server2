using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using WebService.UserService;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            UserServiceClient vom = new UserServiceClient();

            Guid vf = new Guid();

            vom.GetById(vf);

           
        }
    }
}
