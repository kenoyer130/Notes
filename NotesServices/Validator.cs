using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesServices
{
    public class Validator<T>
    {
        public void Validate(T model){
            ValidationContext context = new ValidationContext(model, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            bool valid = Validator.TryValidateObject(model, context, results, true);

            if(!valid)
                throw new ValidationException(string.Join(",", results.Select(p => p.ErrorMessage)));
        }
    }
}
