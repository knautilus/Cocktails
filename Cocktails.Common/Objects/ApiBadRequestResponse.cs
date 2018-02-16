using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cocktails.Common.Objects
{
    public class ApiBadRequestResponse : ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base(400)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();
        }
    }
}
