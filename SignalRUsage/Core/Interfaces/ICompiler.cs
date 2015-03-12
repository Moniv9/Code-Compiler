using WebEditor.Models;

namespace WebEditor.Core
{
     public interface ICompiler
     {
          Output Compile(FileDetail file, string language);
          Output ProgramOutput(FileDetail file, string input, string language);
     }
}
