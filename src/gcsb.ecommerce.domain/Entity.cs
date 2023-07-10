using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace gcsb.ecommerce.domain
{
    public abstract class Entity 
    {
        public bool IsValid { get; private set; }
        public ValidationResult? ValidationResult { get; private set; }
        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            this.ValidationResult = validator.Validate(model);
 
            return IsValid = ValidationResult.IsValid;
        }
        public void AddValidationResultItem(ValidationFailure validationFailure)
        {
            if (ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult(new List<ValidationFailure>());
            }

            this.ValidationResult.Errors.Add(validationFailure);
            this.IsValid = false;
        }
    }
}