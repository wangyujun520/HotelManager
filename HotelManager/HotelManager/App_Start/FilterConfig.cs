using HotelManager.Filter;
using System.Web;
using System.Web.Mvc;

namespace HotelManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogExceptionFilter());//作用于整个项目
        }
    }
}
