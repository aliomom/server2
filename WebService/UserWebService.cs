using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WebService.Dto;
using WebService.UserService;

namespace WebService
{
    public class UserWebService : IUserWebService
    {
        IUserService _iuserService;


        public UserWebService()
        {
            var binding = new System.ServiceModel.BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8081/user/");
            var factory = new ChannelFactory<IUserService>(binding, endpoint);
            _iuserService = factory.CreateChannel();
          //  _iuserService = new UserServiceClient();
        }

        public Guid Add(UserDto dto)
        {
            var model = Mapper.Map<UserDto, AspNetUser>(dto);
            _iuserService.Add(model);

            return model.Id;
        }
        public void editForAdm(Guid id, string fullname, string userName)
        {
            _iuserService.editForAdm(id, fullname, userName);
        }
        public bool Edit(UserDto dto)
        {
            var model = Mapper.Map<UserDto, AspNetUser>(dto);
            _iuserService.Edit(model);
            return true;
        }

        public bool Delete(Guid id)
        {
            _iuserService.Delete(id);
            return true;
        }

        public UserDto GetById(Guid id)
        {
            var model = _iuserService.GetById(id);
            return Mapper.Map<AspNetUser, UserDto>(model);
        }

        public List<UserDto> GetAll()
        {
            return Mapper.Map<List<AspNetUser>, List<UserDto>>(_iuserService.GetAll().ToList());
        }
        public bool HasRole(Guid id, String role)
        {

            return _iuserService.HasRole(id, role);
        }



        public List<UserDto> GetUsersByRole(string role)
        {
            return Mapper.Map<List<AspNetUser>, List<UserDto>>(_iuserService.GetUsersByRole(role).ToList());
        }

        public bool Exists(Guid id)
        {
            return _iuserService.Exists(id);
        }

        public bool IsEmailUnique(string email)
        {
            return _iuserService.IsEmailUnique(email);
        }
    }

}

