﻿using static PublicUtility.CustomExceptions.Base.BaseException;
using PublicUtility.CustomExceptions;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System;
using PublicUtility.Xnm;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Class with useful system features<br></br>
  /// [PT-BR]: Classe com funcionalidades uteis do sistema
  /// </summary>
  public static class XSystem {

    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;

    #region INTEROPT DLLS

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    #endregion

    /// <summary>
    /// [EN]: Hides the application's console so that it runs unnoticed<br></br>
    /// [PT-BR]: Ocultar o console do aplicativo para que ele seja executado despercebido
    /// </summary>
    public static void HideConsole() => ShowWindow(GetConsoleWindow(), SW_HIDE);

    /// <summary>
    /// [EN]: Makes the app's console fully visible <br></br>
    /// [PT-BR]: Torna o console do aplicativo totalmente visível
    /// </summary>
    public static void ShowConsole() => ShowWindow(GetConsoleWindow(), SW_SHOW);

    /// <summary>
    /// [EN]: Uses the app identifier to display it if it is hidden <br></br>
    /// [PT-BR]: Usa o identificador do aplicativo para exibi-lo se estiver oculto
    /// </summary>
    /// <param name="handle">
    /// [EN]: Handle identifier of the application that will be displayed <br></br>
    /// [PT-BR]: Identificador handle da aplicação que será exibida
    /// </param>
    public static void ShowWindow(IntPtr handle) => ShowWindow(handle, SW_SHOW);

    /// <summary>
    /// [EN]: Hide an application window using the handle identifier <br></br>
    /// [PT-BR]: Oculta uma janela de aplicação utilizando o handle identificador
    /// </summary>
    /// <param name="handle">
    /// [EN]: Identifier of the application to be hidden <br></br>
    /// [PT-BR]: Identificador da aplicação que será ocultada
    /// </param>
    public static void HideWindow(IntPtr handle) => ShowWindow(handle, SW_HIDE);

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
    /// <exception cref="OperationCanceledException"></exception>
    public static void RunCmdScript(IEnumerable<string> commands, string privatePath = @"", bool shellExecute = false) {
      ProcessStartInfo psi = new ProcessStartInfo();

      privatePath = Path.Combine(privatePath, "NewCommandsForExec.bat");

      if(!commands.Any())
        throw new RequiredParamsException(Situation.LessThanZero, nameof(commands));

      File.WriteAllLines(privatePath, commands);

      psi.FileName = privatePath;
      psi.UseShellExecute = shellExecute;
      psi.CreateNoWindow = true;
      psi.WindowStyle = ProcessWindowStyle.Hidden;

      using Process process = Process.Start(psi);
      process.WaitForExit();

      // Delete the file after finishing the compilation
      if(File.Exists(privatePath)) {
        File.Delete(privatePath);
      }

    }

  }
}
