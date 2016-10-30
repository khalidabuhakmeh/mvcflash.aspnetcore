using Microsoft.AspNetCore.Mvc;

namespace MvcFlash.AspNetCore.Sample.Controllers
{
    public class PopController : Controller
    {
        private readonly Flash flash;

        public PopController(Flash flash)
        {
            this.flash = flash;
        }

        public IActionResult Index()
        {
            var message = flash.Pop();
            return Json(message);
        }
    }
}