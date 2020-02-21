using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.validator;

namespace WebService.Dto
{
    [Validator(typeof(RegisterUserValidator))]
    public class RegisterUserDto
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
