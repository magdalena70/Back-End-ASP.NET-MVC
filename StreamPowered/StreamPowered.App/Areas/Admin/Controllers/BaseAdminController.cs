
namespace StreamPowered.App.Areas.Admin
{
    using StreamPowered.App.Controllers;
    using StreamPowered.Data.UnitOfWork;
    using System.Web.Mvc;

    [Authorize(Roles="Admin")]
    public class BaseAdminController : BaseController
    {
        public BaseAdminController(IStreamPoweredData data)
            : base(data)
        {
        }
    }
}