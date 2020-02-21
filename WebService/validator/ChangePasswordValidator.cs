using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Dto;

namespace WebService.validator
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            CommonRules();
        }
        private void CommonRules()
        {
            RuleFor(m => m.oldPassword).NotEmpty().WithMessage("يجب ادخال كلمة المرور القديمة");
            RuleFor(m => m.fullname).NotEmpty().WithMessage("يجب ادخال الاسم الكامل");
            RuleFor(m => m.UserName).NotEmpty().WithMessage("يجب ادخال اسم المستخدم");
            RuleFor(m => m.newPassword).NotEmpty().WithMessage("يجب ادخال كلمة المرور الجديدة").Length(6, 100).WithMessage("ينبغي ان يكون طول الكلمة 6 حروف على الأقل");
            RuleFor(m => m.confirmNewPassword).Equal(x => x.newPassword).WithMessage("الكلمتان غير متطابقتان");
        }
    }
}
