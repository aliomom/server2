using System;
using System.Web.Mvc;
using WebService.Identity;

namespace WebApp.Controllers
{
    public class  BaseAuthorizeController : Controller
    {
        private WebService.Identity.ApplicationSignInManager _signInManager;
        private WebService.Identity.ApplicationUserManager _userManager;

        public BaseAuthorizeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager;
            }
        }

        protected Guid getGuid(String value)
        {
            var result = default(Guid);
            Guid.TryParse(value, out result);
            return result;
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}