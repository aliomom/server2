using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Identity;
using WebService.UserService;

namespace WebService.Dto
{
    public static class DtoMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                // ENTITY TO DTO
                cfg.CreateMap<AspNetUser, IdentityUser>()
                      .ForMember(dest => dest.Id,
                          opts => opts.MapFrom(src => src.Id));



                cfg.CreateMap<AspNetUser, UserDto>();//.ForMember(des=>des.Role ,opt=>opt.MapFrom(src=>src.Roles.ToList()[0].Name));




                // DTO TO ENTITY
                cfg.CreateMap<IdentityUser, AspNetUser>().ForMember(dest => dest.Id,opts => opts.MapFrom(src => src.Id));
                   
                    
                cfg.CreateMap<UserDto, AspNetUser>();//.ForMember(des=>des.Role ,opt=>opt.MapFrom(src=>src.Roles.ToList()[0].Name));



            });
        }
    }
}
