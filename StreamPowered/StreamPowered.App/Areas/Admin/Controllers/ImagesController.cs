
namespace StreamPowered.App.Areas.Admin.Controllers
{
    using AutoMapper;
    using StreamPowered.Data.UnitOfWork;
    using StreamPowered.Models;
    //using System.Drawing;
    using System.Web.Mvc;
    using System.Linq;
    using StreamPowered.App.Areas.Admin.Models.ViewModels;

    public class ImagesController : BaseAdminController
    {
        public ImagesController(IStreamPoweredData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImage(Image imageModel, int id)
        {
            if (imageModel != null)
            {
                var image = new Image()
                {
                    Url = imageModel.Url,
                    Game = this.Data.Games.Find(id)
                };

                this.Data.ImageUrls.Add(image);
                this.Data.SaveChanges();

                var imageDb = this.Data.ImageUrls.All()
                    .FirstOrDefault(c => c.Id == image.Id);
                var model = Mapper.Map<ImageViewModel>(imageDb);

                return this.PartialView("DisplayTemplates/ImageViewModel", model);
            }

            return this.Json("Error");
        }
    }
}