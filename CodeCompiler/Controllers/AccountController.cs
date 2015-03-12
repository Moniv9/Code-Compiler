using System.Web;
using System.Web.Mvc;

namespace CodeCompiler.Controllers
{
     public class AccountController : Controller
     {

          /// <summary>
          /// Login page
          /// </summary>
          /// <returns></returns>
          public ActionResult Index()
          {
               return View();
          }


          /// <summary>
          /// View for user signUp
          /// </summary>
          /// <returns></returns>
          public ActionResult SignUp()
          {
               return View();
          }


          /// <summary>
          /// Process signup of users with valid credentails
          /// </summary>
          /// <returns></returns>
          [HttpPost]
          [ValidateAntiForgeryToken]
          public JsonResult CompleteSignUp()
          {
               return Json( new { Data = string.Empty, status = 200 } );
          }


          /// <summary>
          /// Process login of users with valid credentails
          /// </summary>
          /// <returns></returns>
          [HttpPost]
          [ValidateAntiForgeryToken]
          public JsonResult Login()
          {
               return Json( new { Data = string.Empty, status = 200 } );
          }

     }
}
