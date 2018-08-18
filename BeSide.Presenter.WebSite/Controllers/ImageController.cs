using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeSide.BusinessLogic.Construct;

namespace BeSide.Presenter.WebSite.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        // GET: Image
        public ActionResult Index(int id)
        {
            var photo = imageService.GetById(id);
            return File(photo.Content, photo.ContentType);
        }
    }
}