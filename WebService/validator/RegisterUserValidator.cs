using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Dto;
using WebService.UserService;


namespace WebService.validator
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
      
        public RegisterUserValidator()
        {
           
            RuleSet("Register", () =>
            {
                CommonRules();
            });

         

        }
        private void CommonRules()
        {
            RuleFor(m => m.FullName)
                .NotEmpty().WithMessage("يرجى ادخال الاسم الكامل")
                .Matches("^[a-zA-Z0-9_.-]*$").WithMessage("يرجى ادخال ارقام واحرف باللغه الانكليزيه فقط");
            RuleFor(m => m.Password)
                .NotEmpty().WithMessage("يرجى ادخال كلمة المرور");
            RuleFor(m => m.ConfirmPassword)
                .NotEmpty().WithMessage("يرجى تاكيد كلمة المرور").Equal(x => x.Password).WithMessage("الكلمتان غير متطابقتان");
      
        }
     
    }
}
