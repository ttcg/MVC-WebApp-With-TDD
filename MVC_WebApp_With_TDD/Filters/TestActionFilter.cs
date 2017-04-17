using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_WebApp_With_TDD.Filters
{
    public class TestActionFilter : ActionFilterAttribute
    {
    
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            System.Diagnostics.Debug.WriteLine("In Action Filter");

            string userId = HttpContext.Current.User.Identity.Name;
            if (userId != null)
            {
                var result = true;
                if (!result)
                {
 
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary{{ "controller", "Account" },
                                          { "action", "Login" }
 
                                         });
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary{{ "controller", "Account" },
                                          { "action", "Login" }
 
                                         });
 
            }
            base.OnActionExecuting(filterContext);
        }
 
 
    }
}