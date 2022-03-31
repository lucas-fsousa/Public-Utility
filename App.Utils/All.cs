using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using App.Utils.CustomExceptions;
using Situation = App.Utils.CustomExceptions.Base.BaseException.Situations;

namespace App.Utils {

  /// <summary>
  /// [EN]: Public utility class that contains several methods to aid in application development <br></br>
  /// [PT-BR]: Classe de utilidade publica que contém diversos métodos para auxiliar no desenvolvimento de aplicações
  /// </summary>
  /// <remarks>
  /// [EN]: This class may throw an incorrectly formatted Image exception. To fix, go to consoleapp properties and change the "Platform target" to [x86] which by default is marked with [Any CPU] <br></br>
  /// [PT-BR]: Esta classe pode gerar exceção de Imagem com formato incorreto. Para corrigir, acesse as propriedades do consoleapp e altere o "Destino da plataforma" para [x86] que por padrão está marcada com [Any CPU]
  /// </remarks>
  /// <exception cref="BadImageFormatException"></exception>
  public static class All {

    #region INTEROPT DLL IMPORTS

    #region WINDOWS

    [DllImport("User32.Dll")]
    private static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

    [DllImport("User32.Dll")]
    private static extern long SetCursorPos(int x, int y);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type = 3);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, ref Rectangle rectangle);
    #endregion

    #endregion

    #region CLASS XEMAIL

    /// <summary>
    /// [EN]: Class that assists in notifications through emails<br></br>
    /// [PT-BR]: Classe que auxilia nas notificações através de emails
    /// </summary>
    public class XEmail {

      /// <summary>
      /// [EN]: Character that will separate the email string when there is more than one item to be notified. The default is ';'. Ex: "test@gmail.com;test2@gmail.com" will be converted to "test@gmail.com" "test2@gmail.com"<br></br>
      /// [PT-BR]: Caractere que separará a string do email quando houver mais de um item a ser notificado. O padrão é ';'. Ex: "test@gmail.com;test2@gmail.com" será convertido para "test@gmail.com" "test2@gmail.com"
      /// </summary>
      public readonly char MailsSeparator = ';';
      public readonly string CredentialEmail;
      public readonly string CredentialPassword;
      public readonly string PresentationName;

      public string To { get; set; }
      public string Subject { get; set; }
      public string Body { get; set; }
      public MailPriority Priority { get; set; }

      /// <summary>
      /// [EN]: Constructor method with mandatory filling of the credentials of the e-mail that will be used to originate the notifications.<br></br>
      /// [PT-BR]: Método construtor com preenchimento obrigatório das credenciais do email que será utilizado para originar as notificações.
      /// </summary>
      /// <param name="credentialPassword">
      /// [EN]: email password<br></br>
      /// [PT-BR]: senha de acesso do e-mail
      /// </param>
      /// <param name="credentialEmail">
      /// [EN]: email address ex: mycorpemail@mycorp.com <br></br>
      /// [PT-BR]: Endereco do e-mail ex: mycorpemail@mycorp.com
      /// </param>
      /// <param name="presentationName">
      /// [EN]: E-mail display name.<br></br>
      /// [PT-BR]: Nome de apresentação do e-mail
      /// </param>
      /// <exception cref="RequiredParamsException"></exception>
      public XEmail(string credentialPassword, string credentialEmail, string presentationName) {
        this.CredentialPassword = credentialPassword;
        this.CredentialEmail = credentialEmail;
        this.PresentationName = presentationName;
        this.Priority = MailPriority.Normal;

        if(!IsValid(credentialEmail)) {
          throw new RequiredParamsException(Situation.InvalidFormat, "credentialEmail");

        } else if(string.IsNullOrEmpty(credentialPassword)) {
          throw new RequiredParamsException(Situation.IsNullOrEmpty, "credentialPassword");

        } else if(string.IsNullOrEmpty(presentationName)) {
          throw new RequiredParamsException(Situation.IsNullOrEmpty, "PresentationName");

        }
      }

      /// <summary>
      /// [EN]: Send an email to one or more addresses<br></br>
      /// [PT-BR]: Envia um email para um ou varios endereços
      /// </summary>
      /// <param name="message">
      /// [EN]: operation return message<br></br>
      /// [PT-BR]: Mensagem de retorno da operação
      /// </param>
      /// <returns>
      /// [EN]: Will return a boolean informing if it was successful.<br></br>
      /// [PT-BR]: Poderá retornar booleano informando se houve sucesso no envio do email</returns>
      /// <remarks>
      /// <code>
      ///   All.XEmail email = new All.XEmail("mypassword12345", "autoreply@gmail.com", "Notification");
      ///   email.To = "destinationEmail@gmail.com";
      ///   email.Body = "<p><strong>One Body :D</strong></p>";
      ///   email.Priority = MailPriority.High;
      ///   email.Subject = "Hello";
      ///
      ///   string message;
      ///   bool response = email.SendMail(out message);
      /// </code>
      /// </remarks>
      /// <exception cref="RequiredParamsException"></exception>
      public bool SendMail(out string message) {
        SmtpClient client = new SmtpClient();
        MailMessage mail = new MailMessage();

        // CONFIG TO SEND
        mail.Sender = new MailAddress(CredentialEmail, PresentationName);
        mail.From = new MailAddress(CredentialEmail, PresentationName);

        this.To = this.To.RemoveWhiteSpaces();
        if(string.IsNullOrEmpty(this.To)) {
          throw new RequiredParamsException(Situation.IsNullOrEmpty, "Destination Emails");
        }

        // CONFIG TO RECEPT
        foreach(string email in this.To.Split(MailsSeparator)) {
          if(!IsValid(email)) {
            throw new RequiredParamsException(Situation.InvalidFormat, "Destination Email");
          }

          mail.To.Add(new MailAddress(email));
        }

        // SERVER CONFIG
        client.Host = "smtp.office365.com";
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(CredentialEmail, CredentialPassword);

        // CONTENT CONFIG
        if(string.IsNullOrEmpty(this.Body)) {
          this.Body = string.Format("<p>Oops <strong>no body</strong> has been defined for the email. This is default content. <strong>:D</strong></p>");
        }

        if(string.IsNullOrEmpty(this.Subject)) {
          this.Subject = string.Format("Default");
        }

        mail.Body = this.Body;
        mail.Subject = this.Subject;
        mail.Priority = this.Priority;
        mail.IsBodyHtml = true;

        try {
          client.Send(mail);
          message = string.Format($"## ACTION EMAIL ## SUCCESS ## {DateTime.Now} ## OK ##");
          return true;
        } catch(Exception ex) {
          message = string.Format($"## ACTION EMAIL ## FAILED ## {DateTime.Now} ## {ex.Message} ##");
          throw new Exception(message);

        }
      }

      #region PRIVATE METHODS

      /// <summary>
      /// [EN]: Does a basic string format validation to identify if it's a valid email<br></br>
      /// [PT-BR]: Faz uma validação básica do formato da string para identificar se é um email válido
      /// </summary>
      /// <param name="email">
      /// [EN]: String to be parsed<br></br>
      /// [PT-BR]: Cadeia de caracteres a ser analisada
      /// </param>
      /// <returns>
      /// [EN]: Returns a boolean indicating whether the email address string is valid<br></br>
      /// [PT-BR]: Retorna um booleano indicando se a string do endereço email é válido
      /// </returns>
      private bool IsValid(string email) {

        if(email.Length == 0) {
          return false;
        }

        Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

        if(!rg.IsMatch(email)) {
          return false;
        }

        string preAtSign = email.Split("@")[0];
        string posAtSign = email.Split("@")[1];

        if(preAtSign.Count() < 5 || preAtSign.Count() > 64)
          return false;

        else if(posAtSign.Count() < 7)
          return false;
        return true;
      }

      #endregion

    }

    #endregion

    #region CLASS XSYSTEM

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
          name = name[1..]; // substring this name / remove first character
          allProcess.Where(x => x.ProcessName.Contains(name.ToLower())).ToList().ForEach(x => list.Add(x));
          allProcess.Where(x => x.ProcessName.Contains(name.ToUpper())).ToList().ForEach(x => list.Add(x));
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
      /// [EN]: Path of system files and folders. By default there is no padding. If filled with a path, CMD will run in the destination folder.<br></br>
      /// [PT-BR]: Caminho de pastas e arquivos do sistema. Por padrão não tem preenchimento. Caso seja preenchido com um caminho, o CMD será executado na pasta de destino.
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
      public static int RunCmdScript(IEnumerable<string> commands, string privatePath = "", bool shellExecute = false) {
        int exitCode = 1;

        if(commands.Count() <= 0) {
          throw new RequiredParamsException(Situation.LessThanZero, "commands");
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

    #endregion

    #region CLASS XSCREEN

    /// <summary>
    /// [EN]: Class that helps for work involving screens <br></br>
    /// [PT-BR]: Classe que auxiliar para trabalhos envolvendo telas
    /// </summary>
    public class XScreen {

      #region PRIVATE METHODS

      #endregion

      /// <summary>
      /// [EN]: Function to display a graphical MessageBox<br></br>
      /// [PT-BR]: Mostra uma janela de mensagem em formato gráfico.
      /// </summary>
      /// <param name="caption">
      /// [EN]: Window title displayed<br></br>
      /// [PT-BR]: Titulo da janela em amostra
      /// </param>
      /// <param name="text">
      /// [EN]: Text to be displayed when action is called<br></br>
      /// [PT-BR]: Texto que será exibido quando a ação for chamada</param>
      /// <param name="type">
      /// [EN]: Type of buttons that will be displayed in the window ex: [OK / CANCEL] <br></br>
      /// [PT-BR]: Tipo dos botões que a janela vai conter ex: [OK / CANCEL]
      /// </param>
      /// <returns>
      /// [EN]: Returns an integer value representing the selected button<br></br>
      /// [PT-BR]: Retorna um inteiro representando o botão selecionado na janela
      /// </returns>
      public static int ShowMessageBox(string caption, string text, uint type=1) => MessageBox(new IntPtr(0), text, caption, type);

      /// <summary>
      /// [EN]: Capture the start of a window through the Handle and convert it to X,Y coordinates<br></br>
      /// [PT-BR]: Captura o ponto de origem de uma janela através do handle e converte em coordenadas X, Y
      /// </summary>
      /// <param name="handle">
      /// [EN]: Application Handle Address <br></br>
      /// [PT-BR]: Endereço handle da aplicação</param>
      /// <returns>
      /// [EN]: Returns a pointer with the X,Y coordinates of the handle <br></br>
      /// [PT-BR]: Retorna um Ponto com coordenadas X, Y obtidas de um handle
      /// </returns>
      public static Point GetXyByHandle(IntPtr handle) {
        Point point = new Point();
        ClientToScreen(handle, ref point);
        return point;
      }

      /// <summary>
      /// [EN]: Invokes an action to make a mouse click at the indicated X,Y position. <br></br>
      /// [PT-BR]: Invoca a ação que realiza o click do mouse nas coordenadas X, Y
      /// </summary>
      /// <param name="x">
      /// [EN]: Location of X on screen <br></br>
      /// [PT-BR]: Localização de X na tela
      /// </param>
      /// <param name="y">
      /// [EN]: Location of Y on screen <br></br>
      /// [PT-BR]: Localização de Y na tela
      /// </param>
      public static void ClickAt(int x, int y) => SetCursorPos(x, y);

      /// <summary>
      /// [EN]: Capture the screen dimensions of an application <br></br>
      /// [PT-BR]: Captura as dimensões da tele de uma aplicação
      /// </summary>
      /// <param name="handle">
      /// [EN]: Application handle identifier <br></br>
      /// [PT-BR]: Identificador handle da aplicação</param>
      /// <returns>
      /// [EN]: Returns a rectangle with the application screen dimensions <br></br>
      /// [PT-BR]: Retorna um retangulo com as dimensões da tela da aplicação
      /// </returns>
      public static Rectangle GetDemensionByHandle(IntPtr handle) {
        var rect = new Rectangle();
        GetWindowRect(handle, ref rect);
        return rect;
      }

    }

    #endregion

    #region CLASS XSQL

    /// <summary>
    /// [EN]: Helper class to work with data via SqlServer <br></br>
    /// [PT-BR]: Classe auxiliar para trabalhar com dados via SqlServer
    /// </summary>
    public class XSql {
      private SqlConnection con = null;
      private SqlTransaction tran = null;
      private SqlCommand cmd = null;

      /// <summary>
      /// [EN]: Constructor method with required parameters<br></br>
      /// [PT-BR]: Método construtor com parametros obrigatórios
      /// </summary>
      /// <param name="connectionString">
      /// [EN]: String referring to database connection<br></br>
      /// [PT-BR]: Cadeia de caracteres referente a conexão com o banco de dados
      /// </param>
      /// <param name="sqlCommand">
      /// [EN]: Transaction object containing the basic information of the activity.<br></br>
      /// [PT-BR]: Objeto de transação contendo as informações básicas da atividade.
      /// </param>
      /// <exception cref="RequiredParamsException"></exception>
      public XSql(string connectionString, SqlCommand sqlCommand) {
        string invalidParamName = IsValid(connectionString, sqlCommand);
        if(!string.IsNullOrEmpty(invalidParamName)) {
          throw new RequiredParamsException(Situation.IsNullOrEmpty, invalidParamName);
        }

        this.con = new SqlConnection(connectionString);
        this.cmd = sqlCommand;
      }

      #region PRIVATE METHODS
      /// <summary>
      /// [EN]: Go back to changes made to the bank and cancel all updates.<br></br>
      /// [PT-BR]: Volta atrás das modificações realizadas no banco e cancela todas as atualizações.
      /// </summary>
      private void RollBack() {
        if(this.cmd.Transaction != null) {
          this.cmd.Transaction.Rollback();
        }
      }

      /// <summary>
      /// [EN]: Commits an open transaction.<br></br>
      /// [PT-BR]: Faz o commit de uma transação em aberto.
      /// </summary>
      private void Commit() {
        if(this.cmd.Transaction != null) {
          this.cmd.Transaction.Commit();
        }
      }

      /// <summary>
      /// [EN]: Open SQL connection <br></br>
      /// [PT-BR]: Abre a conexão SQL
      /// </summary>
      /// <returns></returns>
      private SqlConnection Open() {
        if(con.State == ConnectionState.Closed) {
          con.Open();
          tran = con.BeginTransaction();
        }
        return con;
      }

      /// <summary>
      /// [EN]: Close the SQL connection <br></br>
      /// [PT-BR]: Fecha a conexão SQL
      /// </summary>
      private void Close() {
        if(con.State == ConnectionState.Open)
          con.Close();
      }

      /// <summary>
      /// [EN]: Single instance constructor with required parameters <br></br>
      /// [PT-BR]: Construtor de instância unico com parametros obrigatórios 
      /// </summary>
      /// <param name="connectionString">
      /// [EN]: String for connecting to the database <br></br>
      /// [PT-BR]: Cadeia de caracteres para conexão com a base de dados
      /// </param>
      /// <param name="cmd">
      /// [EN]: SQL command helper that contains information necessary to perform activities in the database.<br></br>
      /// [PT-BR]: Auxiliar de comando sql que contém informações necessárias para realizar as atividades na base de dados.
      /// </param>
      /// <exception cref="RequiredParamsException"></exception>
      private string IsValid(string connectionString, SqlCommand sqlCommand) {
        string notValidName = string.Empty;

        if(sqlCommand == null)
          notValidName = string.Format("SqlCommand");

        else if(string.IsNullOrEmpty(sqlCommand.CommandText)) 
          notValidName = string.Format("SqlCommand.CommandText");
        
        else if(string.IsNullOrEmpty(connectionString)) 
          notValidName = string.Format("connectionString");

        return notValidName;
      }
      #endregion

      /// <summary>
      /// [EN]: Execute an SQL command without returning data<br></br>
      /// [PT-BR]: Executa um comando SQL sem retorno de dados
      /// </summary>
      /// <param name="execMessage">
      /// [EN]: Variable that will receive the return message from the execution <br></br>
      /// [PT-BR]: Variavel que receberá a mensagem de retorno da execução
      /// </param>
      /// <remarks>
      /// <code>
      /// [EX]:
      ///   SqlCommand sqlCommand= new SqlCommand();
      ///   string errorMessage;
      ///
      ///   sqlCommand.CommandText = "insert into yourTable values (@dateTimeNow)";
      ///   sqlCommand.Parameters.AddWithValue("@dateTimeNow", DateTime.Now);
      ///
      ///   All.XSql xSql = new All.XSql(@"your connection string here", sqlCommand);
      ///   xSql.GoExec(out errorMessage);
      /// </code>
      /// </remarks>
      public void GoExec(out string execMessage) {
        try {
          using(con) {
            this.cmd.Connection = this.Open();
            this.cmd.Transaction = tran;
            this.cmd.ExecuteNonQuery();
            this.Commit();
          }
          execMessage = string.Format($"## SUCCESS ## {DateTime.Now} ## OK ##");
        } catch(Exception ex) {
          execMessage = string.Format($"## ERRO ## {DateTime.Now} ## {ex.Message} ##");
          this.RollBack();

        } finally {
          this.Close();
        }
      }

      /// <summary>
      /// [EN]: Executes a select sql query<br></br>
      /// [PT-BR]: Executa uma atividade de consulta sql
      /// </summary>
      /// <param name="execMessage">
      /// [EN]: Variable that will receive the return message from the execution <br></br>
      /// [PT-BR]: Variavel que receberá a mensagem de retorno da execução
      /// </param>
      /// <remarks>
      /// [EX]: 
      /// <code>
      ///   SqlCommand sqlCommand= new SqlCommand();
      ///   string errorMessage;
      ///
      ///   sqlCommand.CommandText = "select * from yourTable";
      ///
      ///   All.XSql xSql = new All.XSql(@"your connection string here", sqlCommand);
      ///   DataTable dataTable = xSql.ReturnData(out errorMessage);
      /// </code>
      /// </remarks>
      /// <returns>
      /// [EN]: Returns an object of type DataTable  <br></br>
      /// [PT-BR]: Retorna um objeto do tipo DataTable
      /// </returns>
      public DataTable ReturnData(out string execMessage) {
        DataTable table = new DataTable();
        try {
          SqlDataAdapter adapter = new SqlDataAdapter();
          using(con) {
            this.cmd.Connection = this.Open();
            this.cmd.Transaction = tran;

            adapter.SelectCommand = this.cmd;
            adapter.Fill(table);

            this.Commit();
          }

          execMessage = string.Format($"## SUCCESS ## {DateTime.Now} ## OK ##");
        } catch(Exception ex) {
          table = null;
          execMessage = string.Format($"## ERRO ## {DateTime.Now} ## {ex.Message} ##");
          this.RollBack();

        } finally {
          this.Close();

        }
        return table;
      }

    }
    #endregion

    #region MAIN CLASS EXTENSION METHODS

    /// <summary>
    /// [EN]: Remove all whitespace from string <br></br>
    /// [PT-BR]: Remove todos os espaços em branco da string
    /// </summary>
    /// <param name="str">
    /// [EN]: string to be refactored <br></br>
    /// [PT-BR]: Cadeia de caracteres a ser refatorada
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string with no whitespace <br></br>
    /// [PT-BR]: Retorna uma nova string sem espaços em branco
    /// </returns>
    public static string RemoveWhiteSpaces(this string str) {
      string newStr = string.Empty;

      if(!string.IsNullOrEmpty(str)) {
        for(int i = 0; i < str.Length; i++) {
          if(str[i] == ' ') {
            continue;
          }
          newStr += str[i]; // Concatenates characters to form a string with no white spaces
        }
      }

      return newStr;
    }




    #endregion

  }
}
