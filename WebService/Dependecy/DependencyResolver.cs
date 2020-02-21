using FluentValidation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Dto;
using WebService.Identity;
using WebService.UserService;
using WebService.validator;

namespace WebService.Dependecy
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {   registerComponent.RegisterTypeWithTransientLifetimeManager<IUserStore<IdentityUser, Guid>, UserStore>();
            registerComponent.RegisterTypeWithTransientLifetimeManager<IRoleStore<IdentityRole, Guid>, RoleStore>();
            registerComponent.RegisterType<WebService.IUserWebService, UserWebService>();
            // Services


            // Validators

         registerComponent.RegisterType<IValidator<RegisterUserDto>, RegisterUserValidator>();

         //  registerComponent.RegisterType<IValidator<ChangePasswordDto>, ChangePasswordValidator>();

        }
    }
}
