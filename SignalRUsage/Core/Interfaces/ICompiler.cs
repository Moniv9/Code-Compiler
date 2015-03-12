using CodeCompiler.Models;

namespace CodeCompiler.Core
{
     public interface ICompiler
     {
          Output Compile(FileDetail file, string language);
          Output ProgramOutput(FileDetail file, string input, string language);
     }
}
