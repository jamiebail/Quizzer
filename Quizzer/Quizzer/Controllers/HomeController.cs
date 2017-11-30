using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Quizzer.Logic;

namespace Quizzer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Chat()
        {
            await QuizManager.GetQuestions();
            return View();
        }

        public PartialViewResult ReturnView(string view)
        {
            if (view == "_TeamsPartial")
            {
                return PartialView($"Partials/{view}", QuizManager.Teams);
            }
            return PartialView($"Partials/{view}");
        }

        public async Task<ActionResult> Teams()
        {
            return View(QuizManager.Teams);
        }
    }
}