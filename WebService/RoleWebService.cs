using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WebService.Identity;
using WebService.RoleService;

namespace WebService
{
    public class RoleWebService
    {
         IRoleService _RoleService;

        public RoleWebService()
        {
            var binding = new System.ServiceModel.BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8081/roleservice/");
            var factory = new ChannelFactory<IRoleService>(binding, endpoint);
             _RoleService = factory.CreateChannel();
        }

        #region IRoleStore<IdentityRole, Guid> Members
        public System.Threading.Tasks.Task CreateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r =  getRole(role);
           

            return _RoleService.CreateAsync(Mapper.Map<IdentityRole, AspNetRole>(role));
        }
     
        public System.Threading.Tasks.Task DeleteAsync(IdentityRole role)
        {
            if (role == null) throw new ArgumentNullException("role");        
            var r = getRole(role);
            return   _RoleService.DeleteAsync(r);
        }

        public System.Threading.Tasks.Task<IdentityRole> FindByIdAsync(Guid roleId)
        {
            var role = _RoleService.FindById(roleId);
            return Task.FromResult<IdentityRole>(getIdentityRole(role));
        }

        public System.Threading.Tasks.Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var role = _RoleService.FindByName(roleName);
            return Task.FromResult<IdentityRole>(getIdentityRole(role));
        }

        public System.Threading.Tasks.Task UpdateAsync(IdentityRole role)
        {
            if (role == null) throw new ArgumentNullException("role");
               
            var r = getRole(role);
            
            return _RoleService.UpdateAsync(r);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IQueryableRoleStore<IdentityRole, Guid> Members
        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return _RoleService.Roles().Select(x => getIdentityRole(x)) .AsQueryable();
                    
                    
                   
            }
        }
        #endregion

        #region Private Methods
        private AspNetRole getRole(IdentityRole identityRole)
        {
            if (identityRole == null)
                return null;
            return new AspNetRole
            {
                Id = identityRole.Id,
                Name = identityRole.Name
            };
        }

        

        private IdentityRole getIdentityRole(AspNetRole role)
        {
            if (role == null)
                return null;
            return new IdentityRole
            {
                Id = role.Id,
                Name = role.Name
            };
        }
        #endregion
    }
}
