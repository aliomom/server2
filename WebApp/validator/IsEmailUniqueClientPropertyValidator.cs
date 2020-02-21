using FluentValidation.Internal;
using FluentValidation.Mvc;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Controllers;

namespace WebService.validator
{
    public class IsEmailUniqueClientPropertyValidator : FluentValidationPropertyValidator
    {
        public IsEmailUniqueClientPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator)
            : base(metadata, controllerContext, rule, validator)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (!this.ShouldGenerateClientSideRules())
                yield break;
            var formatter = new MessageFormatter().AppendPropertyName(Rule.PropertyName);
            string message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString(null));

            var rule = new ModelClientValidationRule
            {
                ValidationType = "remote",
                ErrorMessage = "Username already exists"
            };
            rule.ValidationParameters.Add("url", Utils.API_PATH + "/api/Validation/IsEmailUnique");
            //rule.ValidationParameters.Add("additionalfields", "*.Id");
            yield return rule;
        }

    }
}
