using App.AssistantCompile;
using App.Utils.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using App.Utils;
using Situation = App.Utils.CustomExceptions.Base.BaseException.Situations;
using System.Data.SqlClient;

namespace Testes {
  internal class Program {
    static void Main(string[] args) {

      #region TESTE CLASS XSYSTEM
      //string temp = All.XSystem.GetVariableByName("TEMP");
      //Process process = All.XSystem.GetAllProcessByName("opera", true).Where(x => x.MainWindowTitle == "Picture in Picture").First();
      //Process processId = All.XSystem.GetProcessById(process.Id);
      //List<FileInfo> files = All.XSystem.GrabFilesFromFolder(@"C:\MyDocs");
      //bool is64Bits = All.XSystem.IsOS64Bits();
      //All.XSystem.Exit();,
      //string[] comands = { "", "" };
      //int exitCode = All.XSystem.RunCmdScript(new string[] { "echo TESTE CARAI >> teste.txt", "echo nova linha >> teste.txt" });
      #endregion

      #region TESTE CLASS XSCREEN
      //IntPtr handle = All.XSystem.GetAllProcessByName("opera", true).Where(x => x.MainWindowTitle == "Picture in Picture").First().MainWindowHandle;
      //All.XScreen.ClickAt(All.XScreen.DesktopWidth / 2, All.XScreen.DesktopHeight / 2);
      //int codigoRetorno = All.XScreen.ShowMessageBox("INFO", "UMA CAIXA DE TESTE NOVA", 3);
      //Point point = All.XScreen.GetXyByHandle(handle);
      //Rectangle rectangle = All.XScreen.GetDemensionByHandle(handle);
      #endregion

      #region TESTE CLASS XEMAIL
      //All.XEmail email = new All.XEmail("mypassword12345", "autoreply@gmail.com", "Notification");
      //email.To = "destinationEmail@gmail.com";
      //email.Body = "<p><strong>One Body :D</strong></p>";
      //email.Priority = MailPriority.High;
      //email.Subject = "Hello";
      //string message;
      //bool teste = email.SendMail(out message);
      #endregion

      #region TESTE CLASS XSQL
      //SqlCommand sqlCommand= new SqlCommand();
      //string errorMessage;


      //sqlCommand.CommandText = "select * from _TB"; // RESULT DATA TABLE SELECT

      //XSql xSql = new XSql(@"Data Source=LUCAS\SQLEXPRESS;Initial Catalog=TEMPDT;Integrated Security=True", sqlCommand);
      //var tb = xSql.ReturnData(out errorMessage);
      #endregion

      // get a hash
      string oneHash = XSecurity.GetHash();

      // Get an MD5 encryption standard
      string strHashMd5 = XSecurity.GetHashMD5("KeUabaN!a=a%@15LNBaiQ");

      // Encrypting a string with a numeric key 
      string encrypt = XSecurity.Encrypt("string for encrypto", "12345678");

      // Decrypting a string encrypted by a numeric key
      string decrypt = XSecurity.Decrypt(encrypt, "12345678");

    }



    public static void ExecCompile() {
      Build.CompileCpp("shared_lib.cpp", "SHARED_LIB", @"C:\Users\Lucas\source\repos\Testes\App.AssistantCompile\CppDlls\");
    }



  }

}
