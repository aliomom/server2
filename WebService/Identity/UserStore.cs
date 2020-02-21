using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WebService.RoleService;
using WebService.UserService;

namespace WebService.Identity
{
    public class UserStore : IUserLoginStore<IdentityUser, Guid>, IUserClaimStore<IdentityUser, Guid>, IUserRoleStore<IdentityUser, Guid>, IUserPasswordStore<IdentityUser, Guid>, IUserSecurityStampStore<IdentityUser, Guid>, IUserStore<IdentityUser, Guid>, IUserEmailStore<IdentityUser, Guid>, IUserLockoutStore<IdentityUser, Guid>, IUserTwoFactorStore<IdentityUser, Guid>, IUserPhoneNumberStore<IdentityUser, Guid>
    {

        IUserService _userservice;

        public UserStore()
        {

            var binding = new System.ServiceModel.BasicHttpBinding();

            binding.Name = "BasicHttpBinding_IUserService";
            //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            //binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;
            binding.TransferMode = TransferMode.Streamed;
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReceiveTimeout = TimeSpan.MaxValue;
            binding.OpenTimeout = TimeSpan.MaxValue;
            binding.SendTimeout = TimeSpan.MaxValue;
           
           


            var endpoint = new EndpointAddress("http://localhost:8081/user/");
            var factory = new ChannelFactory<IUserService>(binding, endpoint);
            _userservice = factory.CreateChannel();
           



        }


        #region IUserStore<IdentityUser, Guid> Members
        public Task CreateAsync(IdentityUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
                
            var u = getUser(user);
            return _userservice.AddAsync(u);
        }

        public Task DeleteAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = getUser(user).Id;

           ;
            return _userservice.DeleteAsync(u);
        }

        public Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            var user = _userservice.GetById(userId);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {

            var user = _userservice.GetByName(userName);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public Task UpdateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentException("user");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            populateUser(u, user);


            return _userservice.EditAsync(u); ;
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IUserClaimStore<IdentityUser, Guid> Members
        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var u = _userservice.GetById(getUser(user).Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var c = new UserService.AspNetUserClaim
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                AspNetUser = u
            };
            u.AspNetUserClaims.Add(c);


            return _userservice.EditAsync(u);
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<Claim>>(u.AspNetUserClaims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList());
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            var u = _userservice.GetById(getUser(user).Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var c = u.AspNetUserClaims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            u.AspNetUserClaims.Remove(c);


            return _userservice.EditAsync(u); ;
        }
        #endregion

        #region IUserLoginStore<IdentityUser, Guid> Members
        public Task AddLoginAsync(IdentityUser user, Microsoft.AspNet.Identity.UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var l = new UserService.AspNetUserLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                AspNetUser = u
            };
            u.AspNetUserLogins.Add(l);


            return _userservice.EditAsync(u); ;
        }

        public Task<IdentityUser> FindAsync(Microsoft.AspNet.Identity.UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var identityUser = default(IdentityUser);

            var l = _userservice.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);
            if (l != null)
                identityUser = getIdentityUser(l.AspNetUser);

            return Task.FromResult<IdentityUser>(identityUser);
        }

        public Task<IList<Microsoft.AspNet.Identity.UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<Microsoft.AspNet.Identity.UserLoginInfo>>(u.AspNetUserLogins.Select(x => new Microsoft.AspNet.Identity.UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
        }

        public Task RemoveLoginAsync(IdentityUser user, Microsoft.AspNet.Identity.UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var l = u.AspNetUserLogins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            u.AspNetUserLogins.Remove(l);

          
            return _userservice.EditAsync(u);
        }
        #endregion

        #region IUserRoleStore<IdentityUser, Guid> Members
        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

            var u = _userservice.GetById(getUser(user).Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            var r = _userservice.GetRolebyName(roleName);
            if (r == null)
                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");

            u.AspNetRoles.Add(r);
         
            return _userservice.EditAsync(u);
        }



        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<string>>(u.AspNetRoles.Select(x => x.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<bool>(u.AspNetRoles.Any(x => x.Name == roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u = _userservice.GetById(user.Id);
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var r = u.AspNetRoles.FirstOrDefault(x => x.Name == roleName);
            u.AspNetRoles.Remove(r);

           ;
            return _userservice.EditAsync(u);
        }
        #endregion

        #region IUserPasswordStore<IdentityUser, Guid> Members
        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore<IdentityUser, Guid> Members
        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region Private Methods
        private UserService.AspNetUser getUser(IdentityUser identityUser)
        {
            if (identityUser == null)
                return null;

            var user = new UserService.AspNetUser();
            populateUser(user, identityUser);

            return user;
        }

        private void populateUser(UserService.AspNetUser user, IdentityUser identityUser)
        {
            //Mapper.Map<IdentityUser, Entities.User>(identityUser, user);
            user.Id = identityUser.Id;
            user.UserName = identityUser.UserName;
            user.PasswordHash = identityUser.PasswordHash;
            user.SecurityStamp = identityUser.SecurityStamp;
            user.Email = identityUser.Email;
            user.EmailConfirmed = identityUser.EmailConfirmed;
            user.LockoutEnabled = identityUser.LockoutEnabled;
            user.LockoutEndDateUtc = identityUser.LockoutEndDateUtc;
            user.AccessFailedCount = identityUser.AccessFailedCount;
            user.TwoFactorEnabled = identityUser.TwoFactorEnabled;
            user.PhoneNumberConfirmed = identityUser.PhoneNumberConfirmed;
            user.FullName = identityUser.FullName;


        }

        private IdentityUser getIdentityUser(UserService.AspNetUser user)
        {
            if (user == null)
                return null;

            var identityUser = new IdentityUser();
            populateIdentityUser(identityUser, user);

            return identityUser;
        }

        private void populateIdentityUser(IdentityUser identityUser, UserService.AspNetUser user)
        {

            identityUser.Id = user.Id;
            identityUser.UserName = user.UserName;
            identityUser.PasswordHash = user.PasswordHash;
            identityUser.SecurityStamp = user.SecurityStamp;
            identityUser.Email = user.Email;
            identityUser.EmailConfirmed = user.EmailConfirmed;
            identityUser.LockoutEnabled = user.LockoutEnabled;
            identityUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
            identityUser.AccessFailedCount = user.AccessFailedCount;
            identityUser.TwoFactorEnabled = user.TwoFactorEnabled;
            identityUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            identityUser.FullName = user.FullName;
        }

        //private void populate(IdenityService.identityUser, Idenity.User.AspNetUser user)
        //{

        //    identityUser.Id = user.Id;
        //    identityUser.UserName = user.UserName;
        //    identityUser.PasswordHash = user.PasswordHash;
        //    identityUser.SecurityStamp = user.SecurityStamp;
        //    identityUser.Email = user.Email;
        //    identityUser.EmailConfirmed = user.EmailConfirmed;
        //    identityUser.LockoutEnabled = user.LockoutEnabled;
        //    identityUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
        //    identityUser.AccessFailedCount = user.AccessFailedCount;
        //    identityUser.TwoFactorEnabled = user.TwoFactorEnabled;
        //    identityUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
        //    identityUser.FullName = user.FullName;
        //}
        #endregion

        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            var user = _userservice.GetByEmail(email);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(user.EmailConfirmed);
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<int>(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(user.LockoutEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.LockoutEndDateUtc ?? new DateTimeOffset());
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            user.AccessFailedCount = user.AccessFailedCount++;
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDateUtc = new DateTime(lockoutEnd.Ticks, DateTimeKind.Utc);
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }



        public Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }
    }
}

