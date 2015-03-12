using System;
using System.Web;
using System.Web.Mvc;
using CodeCompiler.Core;
using CodeCompiler.Models;
using Newtonsoft.Json;

namespace CodeCompiler.Controllers
{
     public class HomeController : Controller
     {

          public ActionResult Index()
          {
               return View(new Output());
          }


          [ValidateInput(false)]
          public string CompileCode(string code, string language)
          {
               try {
                    var fileDetail = FileHandler.ConvertTextToCodeFile(null, code, language);
                    var output = new Compiler(language).Compile(fileDetail, language);

                    if (output.status) {
                         output.message = "Compiled Successfully";
                         return JsonConvert.SerializeObject(output);
                    }
                    else
                         return JsonConvert.SerializeObject(output);

               }

               catch {
                    return JsonConvert.SerializeObject(new Output() { status = false, message = "Compiler is busy" });
               }

          }


          [ValidateInput(false)]
          public string RunCode(string code, string language)
          {
               try {
                    var fileDetail = FileHandler.ConvertTextToCodeFile(null, code, language);
                    ICompiler compiler = new Compiler(language);
                    var output = compiler.Compile(fileDetail, language);

                    if (output.status) {
                         string inputData = FileHandler.GetInput();
                         string outputData = FileHandler.GetActualOutput();

                         output = compiler.ProgramOutput(fileDetail, inputData, language);
                         output.message = FileHandler.CleanText(output.message);
                         outputData = FileHandler.CleanText(outputData);

                         output.status = output.message == outputData;

                         return JsonConvert.SerializeObject(output);
                    }

                    else
                         return JsonConvert.SerializeObject(output);

               }

               catch {
                    return JsonConvert.SerializeObject(new Output() { status = false, message = "Compiler is busy" });
               }
          }


     }
}
