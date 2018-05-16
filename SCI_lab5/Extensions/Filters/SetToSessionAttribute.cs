using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Extensions.Filters
{
    public class SetToSessionAttribute : Attribute, IActionFilter
    {

        private string key;

        public SetToSessionAttribute(string key)
        {
            this.key = key;
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            
            if (context.ModelState != null)
            {
                foreach (var item in context.ModelState)
                {
                    dict.Add(item.Key, item.Value.AttemptedValue);
                }
                context.HttpContext.Session.Set(key, dict);
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
