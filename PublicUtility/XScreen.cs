using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Class that helps for work involving screens <br></br>
  /// [PT-BR]: Classe que auxiliar para trabalhos envolvendo telas
  /// </summary>
  public static class XScreen {

    #region INTEROPT DLL IMPORTS

    [DllImport("User32.Dll")]
    private static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

    [DllImport("User32.Dll")]
    private static extern long SetCursorPos(int x, int y);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type = 3);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, ref Rectangle rectangle);

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
    public static int ShowMessageBox(string caption, string text, uint type = 1) => MessageBox(new IntPtr(0), text, caption, type);

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

    /// <summary>
    /// [EN]: Take a screenshot of the current screen <br></br>
    /// [PT-BR]: faz uma captura da tela atual
    /// </summary>
    /// <param name="saveasFileName">
    /// [EN]: Optional parameter that when filled in, the screenshot is saved as a file <br></br>
    /// [PT-BR]: Parametro opcional que quando preenchido, o screenshot é salvo como arquivo
    /// </param>
    /// <returns>
    /// [EN]: Returns a bitmap with the screenshot <br></br>
    /// [PT-BR]: Retorna um bitmap com o screenshot
    /// </returns>
    public static Bitmap TakeScreenShot(string saveasFileName = "") {
      using(Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)) {
        Graphics g = Graphics.FromImage(bmp);
        g.CopyFromScreen(0, 0, 0, 0, bmp.Size);

        if(!string.IsNullOrEmpty(saveasFileName)) {
          saveasFileName = saveasFileName.Split('.')[0];

          saveasFileName = string.Concat(saveasFileName, ".png");

          bmp.Save(saveasFileName, ImageFormat.Png);
        }

        return bmp;
      }
    }

    /// <summary>
    /// [EN]: Get the main screen size <br></br>
    /// [PT-BR]: Obtém o tamanho da tela principal
    /// </summary>
    /// <returns>
    /// [EN]: Returns a struct containing screen width and height <br></br>
    /// [PT-BR]: Retorna uma estrutura contendo largura e altura da tela
    /// </returns>
    public static Size ScreenSize() => new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

  }
}
