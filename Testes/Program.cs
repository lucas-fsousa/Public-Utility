using System;
using System.IO;
using App.Utils;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
using App.AssistantCompile;

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
      string[] comands = { "", "" };
      int exitCode = All.XSystem.RunCmdScript(new string[] { "echo TESTE CARAI >> teste.txt", "echo nova linha >> teste.txt" });
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

      //sqlCommand.CommandText = "insert into yourTable values (@stringExample, @integerExample, @floatExample)";
      //sqlCommand.Parameters.AddWithValue("@stringExample", "Hello"); // PARAM
      //sqlCommand.Parameters.AddWithValue("@integerExample", 1); // PARAM
      //sqlCommand.Parameters.AddWithValue("@floatExample", 5.5); // PARAM

      //sqlCommand.CommandText = "select * from _TB"; // RESULT DATA TABLE SELECT

      //All.XSql xSql = new All.XSql(@"your connection string here", sqlCommand);
      //xSql.GoExec(out errorMessage);
      #endregion


      //ExecCompile();
    }

    public static void ExecCompile() {
      Build.CompileCpp("shared_lib.cpp", "SHARED_LIB", @"C:\Users\Lucas\source\repos\Testes\App.AssistantCompile\CppDlls\");
    }



  }

}
