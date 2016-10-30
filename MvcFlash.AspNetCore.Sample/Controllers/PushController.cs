using Microsoft.AspNetCore.Mvc;
using MvcFlash.AspNetCore.Sample.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcFlash.AspNetCore.Sample.Controllers
{
    public class PushController : Controller
    {
        private readonly Flash flash;

        public PushController(Flash flash)
        {
            this.flash = flash;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            for (var i = 0; i < 10; i++)
            {
                flash.Success($"Hello World with {i}!");
            }

            flash.Push(new MessageWithData
            {
                Content = "test",
                Type = "success-extra",
                Data =
                {
                    { "test", "yep" }
                }
            });

            return RedirectToAction("index", "pop");
        }

    }
}
