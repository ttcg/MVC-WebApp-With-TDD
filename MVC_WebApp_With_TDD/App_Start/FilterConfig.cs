using MVC_WebApp_With_TDD.Filters;
using System.Web;
using System.Web.Mvc;

namespace MVC_WebApp_With_TDD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TestActionFilter());
        }
    }
}
