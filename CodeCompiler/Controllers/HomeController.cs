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

          [ValidateInput( false )]
          public JsonResult CompileCode(string code, string language)
          {
               try
               {
                    var fileDetail = FileHandler.ConvertTextToCodeFile( null, code, language );
                    var output = new Compiler( language ).Compile( fileDetail, language );

                    if (output.status)
                    {
                         output.message = "Compiled Successfully";
                         return Json( new { Data = string.Empty, status = 200 } );
                    }
                    else
                         return Json( new { Data = string.Empty, status = 200 } );

               }

               catch
               {
                    return Json( new { Data = string.Empty, status = 500 } );
               }

          }


          [ValidateInput( false )]
          public JsonResult RunCode(string code, string language)
          {
               try
               {
                    var fileDetail = FileHandler.ConvertTextToCodeFile( null, code, language );
                    ICompiler compiler = new Compiler( language );
                    var output = compiler.Compile( fileDetail, language );

                    if (output.status)
                    {
                         string inputData = FileHandler.GetInput();
                         string outputData = FileHandler.GetActualOutput();

                         output = compiler.ProgramOutput( fileDetail, inputData, language );
                         output.message = FileHandler.CleanText( output.message );
                         outputData = FileHandler.CleanText( outputData );

                         output.status = output.message == outputData;

                         return Json( new { Data = string.Empty, status = 500 } );
                    }

                    else
                         return Json( new { Data = string.Empty, status = 500 } );

               }

               catch
               {
                    return Json( new { Data = string.Empty, status = 500 } );
               }
          }


     }
}
