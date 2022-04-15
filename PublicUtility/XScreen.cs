using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PublicUtility.Xnm;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Class that helps for work involving screens <br></br>
  /// [PT-BR]: Classe que auxiliar para trabalhos envolvendo telas
  /// </summary>
  public static class XScreen {

    private const int LEFTDOWN = 0x02;
    private const int RIGHTDOWN = 0x08;
    private const int LEFTUP = 0x04;
    private const int RIGHTUP = 0x10;

    #region INTEROPT DLL IMPORTS

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint data, uint dwExtraInfo);

    [DllImport("User32.Dll")]
    private static extern bool ClientToScreen(IntPtr hWnd, ref Point point);

    [DllImport("User32.Dll")]
    private static extern long SetCursorPos(int x, int y);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type = 3);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, ref Rectangle rectangle);

    [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern int GetSystemMetrics(int nIndex);

    #endregion

    /// <summary>
    /// [EN]: Simulates a left mouse button click on the current X,Y position on the screen <br></br>
    /// [PT-BR]: Simula um click com o botão esquerdo do mouse na posição atual X, Y da tela
    /// </summary>
    public static void LeftClick() => mouse_event(LEFTDOWN | LEFTUP, 0, 0, 0, 0);

    /// <summary>
    /// [EN]: Simulates a right mouse click at the current X,Y position on the screen<br></br>
    /// [PT-BR]: Simula um click com o botão direito do mouse na posição atual X, Y da tela
    /// </summary>
    public static void RightClick() => mouse_event(RIGHTDOWN | RIGHTUP, 0, 0, 0, 0);

    /// <summary>
    /// [EN]: Make a combination of MouseMove() and LeftClick() or RightClick() to move the mouse to a certain position and trigger the click<br></br>
    /// [PT-BR]: Faz uma combinação de MouseMove() e LeftClick() ou RightClick() para movimentar o mouse a uma determinada posição e acionar o click
    /// </summary>
    /// <param name="x">
    /// [EN]: X position relative to the current screen<br></br>
    /// [PT-BR]: Posição X em relação a tela atual
    /// </param>
    /// <param name="y">
    /// [EN]: Y position relative to the current screen<br></br>
    /// [PT-BR]: Posição Y em relação a tela atual
    /// </param>
    /// <param name="leftbtn">
    /// [EN]: Indicates which mouse button will be used to click. Default is true for left, if marked as false, right button will be triggered<br></br>
    /// [PT-BR]: Indica qual botão do mouse será utilizado para efetur o click. O padrão é true para esquerdo, se marcado como false, o botão direito será acionado
    /// </param>
    public static void MoveToAndClick(int x, int y, bool leftbtn = true) {
      MouseMoveTo(x, y);
      if(leftbtn)
        LeftClick();

      else
        RightClick();
    }

    /// <summary>
    /// [EN]: Converts a color image to grayscale<br></br>
    /// [PT-BR]: Converte uma imagem colorida para escalas de cinza
    /// </summary>
    /// <param name="filePath">
    /// [EN]: Image to be converted <br></br>
    /// [PT-BR]: Imagem a ser convertida
    /// </param>
    /// <returns>
    /// [EN]: Returns the image converted to grayscale <br></br>
    /// [PT-BR]: Returns the image converted to grayscale
    /// </returns>
    public static Image<Gray, byte> ToGrayImage(string filePath) {
      string path = string.Format(@"C:\MyDocs\printscreen.png");
      Emgu.CV.Image<Gray, byte> _GrayImage;
      Emgu.CV.Image<Bgr, byte> _input = new Emgu.CV.Image<Bgr, byte>(filePath);

      _GrayImage = _input.Convert<Gray, byte>();
      return _GrayImage;
    }

    /// <summary>
    /// [EN]: Converts a color image to grayscale<br></br>
    /// [PT-BR]: Converte uma imagem colorida para escalas de cinza
    /// </summary>
    /// <param name="image">
    /// [EN]: Image to be converted <br></br>
    /// [PT-BR]: Imagem a ser convertida
    /// </param>
    /// <returns>
    /// [EN]: Returns the image converted to grayscale <br></br>
    /// [PT-BR]: Returns the image converted to grayscale
    /// </returns>
    public static Image<Gray, byte> ToGrayImage(this Image<Bgr, byte> image) {
      return image.Convert<Gray, byte>();
    }

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
    /// [EN]: Invokes an action to make a mouse move at the indicated X,Y position. <br></br>
    /// [PT-BR]: Invoca a ação que realiza o movimento do mouse para as coordenadas X, Y
    /// </summary>
    /// <param name="x">
    /// [EN]: Location of X on screen <br></br>
    /// [PT-BR]: Localização de X na tela
    /// </param>
    /// <param name="y">
    /// [EN]: Location of Y on screen <br></br>
    /// [PT-BR]: Localização de Y na tela
    /// </param>
    public static void MouseMoveTo(int x, int y) => SetCursorPos(x, y);

    /// <summary>
    /// [EN]: Capture the screen dimensions of an application <br></br>
    /// [PT-BR]: Captura as dimensões da tele de uma aplicação
    /// </summary>
    /// <param name="handle">
    /// [EN]: Application handle identifier <br></br>
    /// [PT-BR]: Identificador handle da aplicação</param>
    /// <returns>
    /// [EN]: Returns a box with the application screen dimensions <br></br>
    /// [PT-BR]: Retorna uma caixa com as dimensões da tela da aplicação
    /// </returns>
    public static Box GetDemensionByHandle(IntPtr handle) {
      Rectangle rect = new();
      Box box = new();

      GetWindowRect(handle, ref rect);

      Size size = new Size((rect.Width - rect.Location.X), (rect.Height - rect.Location.Y));
      Point point = new Point(rect.Location.X, rect.Location.Y);

      box.Location = point;
      box.Size = size;
      return box;
    }

    /// <summary>
    /// [EN]: Take a screenshot (currently working for Windows only) <br></br>
    /// [PT-BR]: Faça uma captura de tela (atualmente trabalhando apenas para Windows)
    /// </summary>
    /// <returns>
    /// [EN]: Returns a low-level object that can be converted to the object type needed to render the image <br></br>
    /// [PT-BR]: Retorna um objeto de baixo nivel que pode ser convertido para o tipo de objeto necessário para renderizar a imagem
    /// </returns>
    /// <remarks>
    /// [Use example for Windows - Exemplo de uso para Windows]:<br></br>
    /// <code><br></br>
    ///   object response = XScreen.PrintScreen();
    ///   if(response.GetType().Name == "Bitmap") {
    ///    Bitmap printScreen = (Bitmap)response; // cast the object
    ///    printScreen.Save(@"C:\MyDocs\printscreen.png", ImageFormat.Png); // save the image in the desired folder
    ///   }
    /// </code>
    /// </remarks>
    public static object PrintScreen() {
      Size size = GetSize();
      if(OperatingSystem.IsWindows()) {
        Bitmap bmp = new(size.Width, size.Height);
        Graphics graphics = Graphics.FromImage(bmp);
        graphics.CopyFromScreen(0, 0, 0, 0, bmp.Size);
        return bmp;
      }

      return null;
    }

    /// <summary>
    /// [EN]: Take a screenshot (currently working for Windows only) <br></br>
    /// [PT-BR]: Faça uma captura de tela (atualmente trabalhando apenas para Windows)
    /// </summary>
    /// <param name="box">
    /// [EN]: Box containing the dimensions of the image<br></br>
    /// [PT-BR]: Caixa contendo as dimensões da imagem
    /// </param>
    /// <returns>
    /// [EN]: Returns a low-level object that can be converted to the object type needed to render the image <br></br>
    /// [PT-BR]: Retorna um objeto de baixo nivel que pode ser convertido para o tipo de objeto necessário para renderizar a imagem
    /// </returns>
    public static object PrintScreen(Box box) {

      if(OperatingSystem.IsWindows()) {
        Bitmap bmp = new(box.Size.Width, box.Size.Height);
        Graphics graphics = Graphics.FromImage(bmp);
        graphics.CopyFromScreen(box.Location.X, box.Location.Y, 0, 0, bmp.Size);
        return bmp;
      }

      return null;
    }

    /// <summary>
    /// [EN]: Get the current size of the main screen <br></br>
    /// [PT-BR]: Obtém o tamanho atual da tela principal
    /// </summary>
    /// <returns>
    /// [EN]: Returns a structure containing the screen resolution<br></br>
    /// [PT-BR]: Retorna uma estrutura contendo a resolução da tela
    /// </returns>
    public static Size GetSize() => new Size(GetSystemMetrics(0), GetSystemMetrics(1));

    /// <summary>
    /// [EN]: Finds an image that is on the screen at the time of the call to action<br></br>
    /// [PT-BR]: Localiza uma imagem que está na tela no momento da chamada da ação
    /// </summary>
    /// <param name="imagePath">
    /// [EN]: Image location path<br></br>
    /// [PT-BR]: Caminho de localização da imagem
    /// </param>
    /// <param name="confidence">
    /// [EN]: How confident should you be to indicate whether the image is on screen or not<br></br>
    /// [PT-BR]: O quão confiante deve estar para indicar se a imagem está na tela ou não
    /// </param>
    /// <returns>
    /// [EN]: Returns a box with width, height, X point and Y point of the image on the screen<br></br>
    /// [PT-BR]: Retorna uma caixa com largura, altura, ponto X e ponto Y da imagem na tela
    /// </returns>
    public static Box LocateOnScreen(string imagePath, double confidence = 0.90) {
      Box response = new();
      try {
        Bitmap screenshot = (Bitmap)PrintScreen();
        var template = screenshot.ToImage<Gray, byte>();
        var source = new Image<Gray, byte>(imagePath);

        Image<Gray, float> imgMatch = template.MatchTemplate(source, TemplateMatchingType.CcoeffNormed);

        float[,,] matches = imgMatch.Data;
        for(int y = 0; y < matches.GetLength(0); y++) {
          for(int x = 0; x < matches.GetLength(1); x++) {
            double matchScore = matches[y, x, 0];

            if(matchScore >= confidence) {
              response = new(source.Width, source.Height, x, y);
              break;
            }

          }

          if(!response.Size.IsEmpty && !response.Location.IsEmpty)
            break;
        }
      } catch(Exception) { }

      return response;
    }

    #region Overload GetXY

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
    public static Point GetXY(IntPtr handle) {
      Point point = new Point();
      ClientToScreen(handle, ref point);
      return point;
    }

    /// <summary>
    /// [EN]: Get X,Y location using RGB color<br></br>
    /// [PT-BR]: Obtém a localização X, Y utilizando cor RGB
    /// </summary>
    /// <param name="color">
    /// [EN]: Object containing the information of the color to be located on the screen<br></br>
    /// [PT-BR]: Objecto contendo as informações da cor a ser localizada na tela
    /// </param>
    /// <returns>
    /// [EN]: Returns an XY point with RGB color coordinates<br></br>
    /// [PT-BR]: Retorna um ponto XY com as coordenadas da cor RGB
    /// </returns>
    /// <remarks>
    /// [USE EXAMPLE - EXEMPLO DE USO]:
    /// <code>
    ///   Color rgb = Color.FromArgb(255, 170, 35); // ORANGE COLOR
    ///   Point xy = XScreen.GetXY(rgb);
    /// </code>
    /// </remarks>
    public static Point GetXY(Color color) {
      if(OperatingSystem.IsWindows()) {
        Bitmap screen = (Bitmap)PrintScreen();

        for(int x = 0; x < screen.Width; x++) {
          for(int y = 0; y < screen.Height; y++) {

            Color pixel = screen.GetPixel(x, y);
            if(pixel.R == color.R && pixel.G == color.G && pixel.B == color.B) {
              return new Point(x, y);
            }

          }
        }
      }

      return new Point(0, 0);
    }

    #endregion

  }
}
