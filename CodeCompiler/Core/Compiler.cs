using System;
using System.Diagnostics;
using System.Configuration;
using CodeCompiler.Models;

namespace CodeCompiler.Core
{
     public class Compiler : ICompiler
     {
          private readonly string _compilerExecutablePath;
          private readonly string _workingDirectory;
          private readonly int _processKillTime;


          public Compiler(string language)
          {
               _compilerExecutablePath = ConfigurationManager.AppSettings[language];
               _workingDirectory = ConfigurationManager.AppSettings["workingdirectory"];
               _processKillTime = Convert.ToInt32(ConfigurationManager.AppSettings["processkilltime"]);
          }


          public Output Compile(FileDetail file, string language)
          {
               using (Process compiler = new Process()) {
                    compiler.StartInfo = PrepareProcessStartInfo(file.folder, _compilerExecutablePath);
                    compiler.StartInfo.Arguments = file.filename;

                    compiler.Start();
                    compiler.WaitForExit();

                    string result = compiler.StandardOutput.ReadToEnd();
                    string error = compiler.StandardError.ReadToEnd();

                    //note down build erros if any
                    var output = new Output() {
                         executionTime = compiler.TotalProcessorTime,
                         status = compiler.ExitCode == 0,
                         message = language == "csharp" ? result : error
                    };

                    compiler.Close();
                    compiler.Dispose();

                    return output;
               }
          }


          public Output ProgramOutput(FileDetail file, string input, string language)
          {
               using (Process compiler = new Process()) {
                    string executableFileName = GetExecutableFileName(file.fullpath, language);
                    compiler.StartInfo = PrepareProcessStartInfo(file.folder, executableFileName);

                    if (language != "csharp") //incase we have to get ouput from class file
                         compiler.StartInfo.Arguments = "Program";

                    compiler.Start();
                    compiler.WaitForExit();

                    compiler.StandardInput.WriteLine(input);
                    compiler.StandardInput.Flush();

                    string result = compiler.StandardOutput.ReadToEnd();

                    var output = new Output() {
                         executionTime = compiler.TotalProcessorTime,
                         status = true,
                         message = result.Remove(result.Length - 2, 2)
                    };

                    compiler.Close();
                    compiler.Dispose();

                    return output;
               }
          }



          private string GetExecutableFileName(string filename, string language)
          {
               switch (language) {

                    case "csharp":
                         return filename.Replace(".cs", ".exe");
                    case "java":
                         return "java";
                    case "scala":
                         return "scala";
                    default:
                         return string.Empty;
               }

          }


          private ProcessStartInfo PrepareProcessStartInfo(string folder, string executableFilePath)
          {
               ProcessStartInfo startInfo = new ProcessStartInfo();
               startInfo.FileName = executableFilePath;
               startInfo.WorkingDirectory = folder;
               startInfo.UseShellExecute = false;
               startInfo.RedirectStandardOutput = true;
               startInfo.RedirectStandardInput = true;
               startInfo.RedirectStandardError = true;
               startInfo.CreateNoWindow = true;

               return startInfo;
          }
     }
}