using System;
using System.Text;
using System.IO;
using System.Configuration;

namespace CodeCompiler.Core
{
     public class FileHandler
     {
          public static string GetInput()
          {
               return File.ReadAllText(ConfigurationManager.AppSettings["program1_input"]) ?? string.Empty;
          }


          public static string GetActualOutput()
          {
               return File.ReadAllText(ConfigurationManager.AppSettings["program1_output"]) ?? string.Empty;
          }


          public static FileDetail ConvertTextToCodeFile(string username, string text, string language)
          {
               string workingDirectory = ConfigurationManager.AppSettings["workingdirectory"];
               string fileName = Math.Abs(text.GetHashCode()) + GetLanguageExtension(language);
               string folder = workingDirectory + (username ?? "default");
               string fullPath = folder + "\\" + fileName;

               Directory.CreateDirectory(folder);

               using (var file = File.Create(fullPath)) {
                    byte[] bytes = ASCIIEncoding.ASCII.GetBytes(text);
                    file.Write(bytes, 0, bytes.Length);
                    file.Flush();

                    return new FileDetail() {
                         fullpath = fullPath,
                         folder = folder,
                         filename = fileName
                    };
               }
          }



          private static string GetLanguageExtension(string language)
          {
               if (language == "csharp")
                    return ".cs";

               return "." + language.ToLower();
          }


          public static string CleanText(string text)
          {
               if (string.IsNullOrEmpty(text))
                    return string.Empty;

               text = text.Replace("\r\n", "<br/>");

               if (text[text.Length - 1] == '-')
                    return text.Remove(text.Length - 1, 1);
               else
                    return text;

          }



     }
}