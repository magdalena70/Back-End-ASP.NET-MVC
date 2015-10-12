
namespace Twitter.Web.App_Start
{
    using System.Web.Mvc;

    public static class ViewEngineConfig
    {
        // clear WebFormViewEngine and add to using only RazorViewEngine
        public static void AddViewEngines(ViewEngineCollection viewEngines)
        {
            viewEngines.Clear();
            viewEngines.Add(new RazorViewEngine());
        }
    }
}