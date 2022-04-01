using App.Utils.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Utils.CustomExceptions.Base.BaseException;

namespace App.Utils {

  /// <summary>
  /// [EN]: Class with useful system features<br></br>
  /// [PT-BR]: Classe com funcionalidades uteis do sistema
  /// </summary>
  public class XSystem {

    /// <summary>
    /// [EN]: Search for a process running on the current machine<br></br>
    /// [PT-BR]: Procura um processo em execução na máquina atual
    /// </summary>
    /// <param name="id">
    /// [EN]: unique process identifier <br></br>
    /// [PT-BR]: Identificador unico do processo
    /// </param>
    /// <returns>
    /// [EN]: Returns a process <br></br>
    /// [PT-BR]: Retorna um processo
    /// </returns>
    public static Process GetProcessById(int id) => Process.GetProcessById(id);

    /// <summary>
    /// [EN]: Search for a process running on the current machine<br></br>
    /// [PT-BR]: Procura um processo em execução na máquina atual
    /// </summary>
    /// <param name="name">
    /// [EN]: Name of running process to find<br></br>
    /// [PT-BR]: Nome do processo em execução a ser encontrado
    /// </param>
    /// <param name="exactName">
    /// [EN]: Checks if the search result should bring process results with similar names or exactly the same as the one informed<br></br>
    /// [PT-BR]: Checa se o resultado deve ser de uma busca por processos com nomes similares ou se deve ser uma busca exata pelo processo informado
    /// </param>
    /// <returns>
    /// [EN]: Returns a list of all processes identified by the given name<br></br>
    /// [PT-BR]: Retorna uma lista com todos os processos identificados pelo nome informado
    /// </returns>
    public static List<Process> GetAllProcessByName(string name, bool exactName = true) {
      List<Process> list = new List<Process>();
      Process[] allProcess = Process.GetProcesses();

      allProcess.Where(x => x.ProcessName.Contains(name)).ToList().ForEach(x => list.Add(x));
      if(!exactName) {
        name = name[1..].ToLower(); // substring this name / remove first character
        allProcess.Where(x => x.ProcessName.ToLower().Contains(name)).ToList().ForEach(x => list.Add(x));
      }

      list = list.Distinct().ToList();
      return list;
    }

    /// <summary>
    /// [EN]: Search the system to find an environment variable<br></br>
    /// [PT-BR]: Faz uma busca no sistema para localizar uma variavel de ambiente
    /// </summary>
    /// <param name="name">
    /// [EN]: Parameter representing the name of the environment variable to be located<br></br>
    /// [PT-BR]: Parametro que representa o nome da variavel de ambiente a ser localizada
    /// </param>
    /// <returns>
    /// [EN]: Returns a string with the value of the environment variable or null if not found <br></br>
    /// [PT-BR]: Retorna uma string com o valor da variavel de ambiente ou nulo caso não seja localizada
    /// </returns>
    public static string GetVariableByName(string name) => Environment.GetEnvironmentVariable(name);

    /// <summary>
    /// [EN]: Ends the current Process activity (System Exit)<br></br>
    /// [PT-BR]: Encerra a atividade do Processo atual (Sai do sistema)
    /// </summary>
    public static void Exit() => Environment.Exit(0);

    /// <summary>
    /// [EN]: Search and capture all files contained in the destination folder <br></br>
    /// [PT-BR]: Faz uma busca  e captura todos os arquivos contidos na pasta de destino
    /// </summary>
    /// <param name="dirPath">
    /// [EN]: folder path <br></br>
    /// [PT-BR]: caminho da pasta
    /// </param>
    /// <returns>
    /// [EN]: returns a list of files sorted by creation date <br></br>
    /// [PT-BR]: retorna uma lista de arquivos ordenadas por data de criação
    /// </returns>
    public static List<FileInfo> GrabFilesFromFolder(string dirPath) => new DirectoryInfo(dirPath).GetFiles().OrderBy(x => x.CreationTime).ToList();

    /// <summary>
    /// [EN]: Checks if the operating system is 64bits type <br></br>
    /// [PT-BR]: Verifica se o sistema operacional é do tipo 64bits
    /// </summary>
    /// <returns>
    /// [EN]: returns a boolean <br></br>
    /// [PT-BR]: retorna um booleano
    /// </returns>
    public static bool IsOS64Bits() => Environment.Is64BitOperatingSystem;

    /// <summary>
    /// [EN]: Run one or multiple commands by prompt or powershell <br></br>
    /// [PT-BR]: Executa um ou multiplos comandos através do prompt ou powershell
    /// </summary>
    /// <param name="commands">
    /// [EN]: list of commands to run in DOS<br></br>
    /// [PT-BR]: lista de comandos a serem executados no DOS
    /// </param>
    /// <param name="privatePath">
    /// [EN]: Path of system files and folders. By default there is C:\. If filled another path, CMD will run in the destination folder.<br></br>
    /// [PT-BR]: Caminho de pastas e arquivos do sistema. Por padrão é C:\. Caso seja preenchido com outro caminho, o CMD será executado na pasta de destino.
    /// </param>
    /// <param name="shellExecute">
    /// [EN]: If enabled, execution will happen through powerShell <br></br>
    /// [PT-BR]: Se habilitado, a execução acontecerá através do powerShell
    /// </param>
    /// <returns>
    /// [EN]: Returns an integer value representing the exit status of the console execution <br></br>
    /// [PT-BR]: Retorna um valor inteiro que representa o status de saida da execução no console
    /// </returns>
    /// <exception cref="OperationCanceledException"></exception>
    public static int RunCmdScript(IEnumerable<string> commands, string privatePath = @"C:\", bool shellExecute = false) {
      int exitCode = 1;

      if(commands.Count() <= 0) {
        throw new RequiredParamsException(Situations.LessThanZero, nameof(commands));
      }

      ProcessStartInfo psi = new ProcessStartInfo();

      privatePath = Path.Combine(privatePath, "NewCommandsForExec.bat");

      File.WriteAllLines(privatePath, commands);

      psi.FileName = privatePath;
      psi.UseShellExecute = shellExecute;
      psi.CreateNoWindow = true;
      psi.WindowStyle = ProcessWindowStyle.Hidden;

      using(Process process = Process.Start(psi)) {
        process.WaitForExit();
        exitCode = process.ExitCode;
      }

      // Delete the file after finishing the compilation
      if(File.Exists(privatePath)) {
        File.Delete(privatePath);
      }

      return exitCode;
    }

  }
}
