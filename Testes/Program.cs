using static PublicUtility.CustomExceptions.Base.BaseException;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using PublicUtility.CustomExceptions;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Threading;
using System.Net.Http;
using System.Net.Mail;
using System.Drawing;
using PublicUtility;
using System.Linq;
using System.Text;
using System.IO;
using Emgu.CV;
using System;
using Emgu.CV.Structure;
using Emgu.CV.Dnn;
using PublicUtility.Xnm;

namespace Testes {
  internal class Program {

    static void Main(string[] args) {

      #region TESTE CLASS XSECURITY

      // get a hash
      //string oneHash = XSecurity.GetHash();

      // Get an MD5 encryption standard
      //string strHashMd5 = XSecurity.GetHashMD5("KeUabaN!a=a%@15LNBaiQ");

      // Encrypting a string with a numeric key 
      //string encrypt = XSecurity.Encrypt("string for encrypto", "12345678");

      // Decrypting a string encrypted by a numeric key
      //string decrypt = XSecurity.Decrypt(encrypt, "12345678");

      #endregion

      #region TESTE CLASS XSYSTEM

      //string temp = XSystem.GetVariableByName("TEMP");
      //Process process = XSystem.GetAllProcessByName("opera", true).Where(x => x.MainWindowTitle == "Picture in Picture").First();
      //Process processId = XSystem.GetProcessById(process.Id);
      //List<FileInfo> files = XSystem.GrabFilesFromFolder(@"C:\MyDocs");
      //bool is64Bits = XSystem.IsOS64Bits();
      //XSystem.Exit();
      //string[] comands = { "", "" };
      //int exitCode = XSystem.RunCmdScript(new string[] { $"echo TESTE CARAI {DateTime.Now} >> teste.txt", $"echo nova linha {DateTime.Now} >> teste.txt" });



      #endregion

      #region TESTE CLASS XSCREEN

      //IntPtr handle = XSystem.GetAllProcessByName("opera", true).Where(x => x.MainWindowTitle == "Picture in Picture").First().MainWindowHandle;
      //XScreen.ClickAt(950, 540);
      //int codigoRetorno = XScreen.ShowMessageBox("INFO", "UMA CAIXA DE TESTE NOVA", 3);
      //Point point = XScreen.GetXyByHandle(handle);
      //Rectangle rectangle = XScreen.GetDemensionByHandle(handle);
      //XScreen.TakeScreenShot(@"C:\MyDocs\img.png", ImageFormat.Jpeg);

      #endregion

      #region TESTE CLASS XEMAIL

      //XEmail email = new XEmail("c3NBqo7a", "autoreplynewpassword@hotmail.com", "Notification");
      //email.To = "lucasads18@outlook.com";
      //email.Body = "<p><strong>One Body :D</strong></p>";
      //email.Priority = MailPriority.High;
      //email.Subject = "Hello";
      //string message;
      //bool teste = email.SendMail(out message);

      #endregion

      #region TESTE CLASS XSQL

      //SqlCommand sqlCommand= new SqlCommand();
      //string errorMessage;
      //sqlCommand.CommandText = "insert into _TB values (@anyv)";
      //sqlCommand.Parameters.AddWithValue("@anyv", "HTTPS");
      //XSql xSql = new XSql(@"Data Source=LUCAS\SQLEXPRESS;Initial Catalog=TEMPDT;Integrated Security=True", sqlCommand);
      //xSql.GoExec(out errorMessage);

      //sqlCommand.CommandText = "select * from _TB"; // RESULT DATA TABLE SELECT
      //XSql xSql = new XSql(@"Data Source=LUCAS\SQLEXPRESS;Initial Catalog=TEMPDT;Integrated Security=True", sqlCommand);
      //var tb = xSql.ReturnData(out errorMessage);

      #endregion

      #region TESTE CLASS X

      //int it = 1145161;
      //bool bi = it.MaxMin(-101031, 90000000); 

      //byte us = 250;
      //bool ok = us.MaxMin(0, 255);

      //sbyte b = -20;
      //bool bt = b.MaxMin(-128, 127);

      //int[] ai = new int[] { -10, 410, -5, -11, -12, 20 };
      //var aineg = ai.GetNegatives();
      //var aipos = ai.GetPositives();

      //List<int> li = new List<int> { -10, 10, -5, -11, -12, 20 };
      //var lineg = li.GetNegatives();
      //var lipos = li.GetPositives();

      /* the Input has 2 optional parameters, being the message to be written
       * in the console, and the hiding of the console to not display the typed
       * characters.*/

      // Example with 2 parameters
      //string password = X.Input<string>("Password: ", true);

      // Example with 0 parameter
      //int anyNumber = X.Input<int>();

      // Example with 1 parameter
      //string anyChar = X.Input<string>("password: ", true);

      #endregion

      #region TESTE CLASS XREQUEST

      //// Makes a Get on the given URL and returns a content in json format / Can be used asynchronously
      //string url = string.Format("http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1"); // URL of a free API for testing
      //Root response = XRequest.HttpGet<Root>(url).GetAwaiter().GetResult();

      //// Make a Post request by sending an object to the server via URL
      //string url = string.Format("http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

      //Root root = new Root();
      //root.success = true;
      //root.deck_id = "3p40paa87x90";
      //root.remaining = 52;
      //root.shuffled = true;

      //HttpResponseMessage response = XRequest.HttpPost(url, root).GetAwaiter().GetResult();

      #endregion

      TTTT();


    }

    public static void TTTT() {
      if(OperatingSystem.IsWindows()) {
        Net model;
        string path = string.Format(@"C:\MyDocs\captura2.png");
        var img = new Image<Bgr, byte>(path);
        var input = DnnInvoke.BlobFromImage(img, 1 / 255.0);
        


      }
    }


    public static Box LocateOnScreen(string imagePath, float confidence = 0.5f) {
      Box response = new();
      try {
        if(OperatingSystem.IsWindows()) {




        }
      } catch(Exception) {
        throw;
      }

      return response;
    }


    //public static XScreen.Box LocateOnScreen(string imagePath, float confidence = 0.5f) {
    //  XScreen.Box response = new();
    //  float prop = 0.0f;
    //  float maxProp = 0.0f;
    //  long match = 0;
    //  long maxMatch = 0;
    //  try {
    //    if(OperatingSystem.IsWindows()) {
    //      Size resolution = XScreen.GetSize();
    //      List<string> screenMap = new List<string>();
    //      Bitmap imBase = (Bitmap)XScreen.SetImageToGrayScale(XScreen.PrintScreen());
    //      Bitmap imToLocate = (Bitmap)XScreen.SetImageToGrayScale(new Bitmap(imagePath));

    //      int countx = 0, county = 0;
    //      while(countx < imBase.Width && county < imBase.Height) {
    //        for(int x = 0; x < imToLocate.Width; x++, countx++) {

    //          if(countx >= imBase.Width) {
    //            break;
    //          }

    //          for(int y = 0; y < imToLocate.Height; y++, county++) {

    //            if(county >= imBase.Height) {
    //              county = 0;
    //            }

    //            // ARGB TO LOCATE
    //            byte r = imToLocate.GetPixel(x, y).R;
    //            byte g = imToLocate.GetPixel(x, y).G;
    //            byte b = imToLocate.GetPixel(x, y).B;

    //            // ARGB SCREENSHOT BASE
    //            byte rr = imBase.GetPixel(countx, county).R;
    //            byte gg = imBase.GetPixel(countx, county).G;
    //            byte bb = imBase.GetPixel(countx, county).B;

    //            if(rr == r && gg == g && bb == b) {
    //              screenMap.Add($"{countx};{county}"); // concatenates X coordinate and Y coordinate separating by ';'
    //              match++;
    //            }

    //          }
    //        }

    //        if(match > maxMatch)
    //          maxMatch = match;

    //        if(prop > maxProp)
    //          maxProp = prop;
    //      } // while end

    //      // tries to calculate percentage to handle division by zero attempts
    //      try { prop = (match / (imToLocate.Height * imToLocate.Width)) * 0.01f; } catch(Exception) { }

    //    }
    //  } catch(Exception) {
    //    throw;
    //  }

    //  return response;
    //}






  }

}
