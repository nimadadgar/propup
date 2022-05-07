using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application.Models
{
    [Serializable]
    public class ValidationException : Exception
    {

        public List<ValidationExceptionModel> errors { get; }
        public ValidationException(List<ValidationExceptionModel> models)
        {
            errors = models;
        }

        public ValidationException(FluentValidation.Results.ValidationResult validations)
        {
            errors= validations.Errors.Select(d => new ValidationExceptionModel
            {
                key = d.PropertyName,
                value = d.ErrorMessage
            }).ToList();


        }

    }
}
