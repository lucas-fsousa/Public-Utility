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
      //int exitCode = All.XSystem.RunCmdScript(new string[] { "echo TESTE CARAI >> teste.txt", "echo nova linha >> teste.txt" });
      #endregion

      #region TESTE CLASS XSCREEN
      //IntPtr handle = All.XSystem.GetAllProcessByName("opera", true).Where(x => x.MainWindowTitle == "Picture in Picture").First().MainWindowHandle;
      //All.XScreen.ClickAt(All.XScreen.DesktopWidth, All.XScreen.DesktopHeight);
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

      //sqlCommand.CommandText = "insert into _TB values (@valor)"; // VOID NO RESULT
      //sqlCommand.Parameters.AddWithValue("@valor", "Val"); // PARAMS

      //sqlCommand.CommandText = "select * from _TB"; // RESULT DATA TABLE SELECT

      //All.XSql xSql = new All.XSql(@"your connection string here", sqlCommand);
      //DataTable dataTable = xSql.ReturnData(out errorMessage);
      #endregion


    }



  }

}
