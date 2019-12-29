using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWEF2.CustomValidation
{
    public class SkillsValidate : Attribute, IModelValidatior
    {
        public string[] Allowed { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<ModelValidationResult> Validate(ModelValidateContext context)
        {
            if (Allowed.Contains(context.Model as string))
                return Enumerable.Empty<ModelValidationResult>();
            else
                return new List<ModelValidationResult> {
                new ModelValidationResult("", ErrorMessage)};
        }

    }
}
