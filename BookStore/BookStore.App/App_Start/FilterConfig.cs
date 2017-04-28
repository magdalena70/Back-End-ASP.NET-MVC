using System;
using System.Web.Mvc;

namespace BookStore.App
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()
            {
                ExceptionType = typeof(Exception),
                View = "CustomErrors"
            });
        }
    }
}
