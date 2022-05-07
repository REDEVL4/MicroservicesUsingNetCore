using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; set; }
        public ValidationException()
        :base("one or more validation failures occured!")
        {
            Errors=new Dictionary<string, string[]>();
        }
        public ValidationException(IEnumerable<ValidationFailure> validationFailures)
        :this()
        {
            Errors = validationFailures.GroupBy(c => c.PropertyName, c => c.ErrorMessage)
                .ToDictionary(failures => failures.Key, failures => failures.ToArray());
        }
    }
}
