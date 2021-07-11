using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_webapi.Filters
{
    public class AddHeaderAttribute :ResultFilterAttribute
    {
        // Header :  name and value
        private readonly string _name;
        private readonly string _value;
        public AddHeaderAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            // Modified the Response.
            context.HttpContext.Response.Headers.Add(_name, new string[] { _value });             
        }
        

    }
}
