using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesServices
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) :
            base(message)
        {

        }
    }
}
