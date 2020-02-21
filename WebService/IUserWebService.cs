using System;
using System.Collections.Generic;
using WebService.Dto;

namespace WebService
{
    public interface IUserWebService
    {
        Guid Add(UserDto dto);
        bool Delete(Guid id);
        bool Edit(UserDto dto);
        void editForAdm(Guid id, string fullname, string userName);
        bool Exists(Guid id);
        List<UserDto> GetAll();
        UserDto GetById(Guid id);
        List<UserDto> GetUsersByRole(string role);
        bool HasRole(Guid id, string role);
        bool IsEmailUnique(string email);
    }
}