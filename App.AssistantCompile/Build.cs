using App.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace App.AssistantCompile {
  public static class Build {

    /// <summary>
    /// [EN]: Responsible for compiling and generating DLLS from codes created in c++<br></br>
    /// [PT-BR]: Responsável por compilar e gerar DLLS de códigos criados em c++
    /// </summary>
    /// <param name="cppFileName">
    /// [EN]: File name
    /// [PT-BR]: Nome do arquivo
    /// </param>
    /// <param name="__declspec">
    /// [EN]: Constant that references the import or export condition<br></br>
    /// EX: In this example, the constant "MATHLIBRARY_EXPORTS" is used as a reference.<br></br>
    /// [PT-BR]: Constante que referencia a condição de importação ou exportação<br></br>
    /// EX: Neste exemplo é utilizada a constante "MATHLIBRARY_EXPORTS" como referencia <br></br><br></br>
    /// EX: <br></br>
    /// #ifdef MATHLIBRARY_EXPORTS <br></br>
    /// #define MATHLIBRARY_API __declspec(dllexport) <br></br>
    /// #else <br></br>
    /// #define MATHLIBRARY_API __declspec(dllimport) <br></br>
    /// #endif <br></br>
    /// </param>
    /// <param name="filePath">
    /// [EN]: path to file <br></br>
    /// [PT-BR]: Caminho do arquivo
    /// </param>
    /// <remarks>
    /// [ATTENTION - ATENÇÃO]<br></br>
    /// [EN]: Keep the .h and .cpp files in the same folder<br></br>
    /// [PT-BR]: Manter o arquivo .h e .cpp na mesma pasta
    /// </remarks>
    /// 
    /// <exception cref="FormatException"></exception>
    public static void CompileCpp(string cppFileName, string __declspec, string filePath="") {
      if(string.IsNullOrEmpty(Path.Combine(filePath, cppFileName)))
        throw new FormatException("Invalid filename or extension");

      if(cppFileName.Contains(Path.PathSeparator))
        throw new FormatException("The file name contains an invalid character.");

      if(cppFileName.Substring(cppFileName.Length - 4).Contains(".cpp"))
        cppFileName = cppFileName[0..^4];

      List<string> stringBuilder = new List<string>();

      stringBuilder.Add($"cd {filePath}");
      stringBuilder.Add($"g++ -c -D {__declspec} {cppFileName}.cpp");
      stringBuilder.Add($"g++ -shared -o {cppFileName}.dll {cppFileName}.o -Wl,--out-implib,lib{cppFileName}.a");

      XSystem.RunCmdScript(stringBuilder, filePath);

    }
  }
}
