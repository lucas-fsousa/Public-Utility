﻿using System.Text.RegularExpressions;
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
using Emgu.CV.Structure;
using PublicUtility.Xnm;
using System.Threading;
using System.Net.Http;
using System.Net.Mail;
using System.Drawing;
using PublicUtility;
using Emgu.CV.Dnn;
using System.Linq;
using System.Text;
using System.IO;
using Emgu.CV;
using System;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using System.Net.NetworkInformation;
using System.Net;

namespace Testes {
  internal class Program {


    static void Main(string[] args) {

      #region TESTE CLASS XKEYBOARD

      //Thread.Sleep(5000);

      //// Executes the key-by-key keyboard manipulation action with output result: hello!
      //XKeyboard.PressKey(Key.H);
      //XKeyboard.UnPressKey(Key.H);
      //XKeyboard.PressKey(Key.E);
      //XKeyboard.UnPressKey(Key.E);
      //XKeyboard.PressKey(Key.L);
      //XKeyboard.UnPressKey(Key.L);
      //XKeyboard.PressKey(Key.L);
      //XKeyboard.UnPressKey(Key.L);
      //XKeyboard.PressKey(Key.O);
      //XKeyboard.UnPressKey(Key.O);
      //XKeyboard.PressKey(Key.Shif);
      //XKeyboard.PressKey(Key.N1);
      //XKeyboard.UnPressKey(Key.N1);
      //XKeyboard.UnPressKey(Key.Shif);

      //// Executes the action of manipulating the keys creating the following output: hello!
      //// Single digit combines Presskey with UnpressKey
      //XKeyboard.SigleDigit(Key.H);
      //XKeyboard.SigleDigit(Key.E);
      //XKeyboard.SigleDigit(Key.L);
      //XKeyboard.SigleDigit(Key.L);
      //XKeyboard.SigleDigit(Key.O);
      //XKeyboard.PressKey(Key.Shif);
      //XKeyboard.SigleDigit(Key.N1);
      //XKeyboard.UnPressKey(Key.Shif);

      //// The following method is efficient to perform multiple key actions as long as it is indicated which action will be taken (press or release)
      //// Combines multiple keys - Keyboard output: "hello!" 
      //XKeyboard.KeyCombine(KeyAction.Press, new Key[] { Key.H, Key.E, Key.L, Key.L, Key.O, Key.Shif, Key.N1 });

      //// releases all keys that have been pressed
      //XKeyboard.KeyCombine(KeyAction.Drop, new Key[] { Key.H, Key.E, Key.L, Key.L, Key.O, Key.Shif, Key.N1 });

      //// Checks if the specified key is pressed returning boolean as response
      //bool keyDetail = XKeyboard.KeyStateInfo(Key.Shif).IsPressed;

      #endregion

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

      #region TESTE CLASS XTEXTFILE

      //string path = @"C:\MyDocs\doc.txt";

      //XTextFile.AppendMultLines(path, new[] { "TESTE MULTI LINE 01", "TESTE MULTI LINE 02", "TESTE MULTI LINE 03", "TESTE MULTI LINE 04" }); // Insert multiple lines into the file consecutively

      //XTextFile.AppendLine(path, $"TEST AT {DateTime.Now}"); // Inserts a new line in the text document

      //XTextFile.ReplaceLine(path, 4, "NEW ITEM FOR INSERT IN LINE 5 IF THERE ARE 5 LINES IN THE FILE"); // Change the value of a row based on its location number (min is 1)

      //XTextFile.ReplaceLine(path, "TESTE MULTI LINE 02", "FIRST LOCATED - REPLACED", true); // Change the value of the old line if match (can be applied to all lines found or just the first line)

      //string text01 = XTextFile.GetTextLine(path, 2); // get the text of line 2 from the destination file

      //List<string> textLines = XTextFile.GetTextLines(path, new[] { 1, 5, 3 }); // Get specific text lines from the file ( line 1, line 5 and 3 for example)

      //List<string> otherTextLines = XTextFile.GetTextLines(path, 4, 6); // Gets the text of lines in the desired range

      //XTextFile.CreateText(@"C:\MyDocs\newdoc.txt", "LONG TEXT HERE"); // Creates a new Text file or replaces an existing one

      //string recText = XTextFile.GetText(@"C:\MyDocs\newdoc.txt"); // Get all the text from a file

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

      #region TESTE CLASS XMOUSE

      // Moves the mouse to the specified screen coordinate instantly, without motion animation.
      ////XMouse.MoveTo(590, 299, MouseSpeed.Full);

      //Point start = new(10, 15); // start x y coordinate point
      //Point end = new(220, 500); // end x y coordinate point
      //// Performs drag from start point and drop to end point with x2 move animation
      //XMouse.Drag(start, end, MouseSpeed.X2);

      //// get the current cursor position
      //Point currentPos = XMouse.GetPosition();

      //// triggers the mouse scroll by scrolling down and then scrolling up
      //XMouse.RollDown(600);
      //XMouse.RollUp(100);

      //// quickly moves to the indicated location and performs a click
      //XMouse.MoveToAndClick(10, 10, MouseSpeed.X4);

      //// performs a click on the current mouse position (left button)
      //XMouse.LeftClick();

      //// performs a double-click in the current mouse position (left button)
      //XMouse.LeftClick(true);

      //// performs a click on the current mouse position (right button)
      //XMouse.RightClick();

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

      //var ping = X.GetPing("8.8.8.8").Result;
      //ping.ToString().Print();

      #endregion


    }



    public static void showImage() {
      string path = string.Format(@"C:\MyDocs\printscreen.png");
      var im1 = new Image<Bgr, byte>(path);
      CvInvoke.Imshow("Image", im1);
      CvInvoke.WaitKey(0);
    }



  }

}
