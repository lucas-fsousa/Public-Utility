using PublicUtility.CustomExceptions;
using PublicUtility.Xnm;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace PublicUtility {
  public class XMouse {

    #region INTEROPT DLL IMPORTS

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint data, uint dwExtraInfo);

    [DllImport("User32.Dll")]
    private static extern long SetCursorPos(int x, int y);

    #endregion

    #region PRIVATE METHODS

    private static bool CheckCorner(Point point) {
      Size size = XScreen.GetSize();
      bool response = false;

      if(point.X < 0)
        return response;

      else if(point.Y < 0)
        return response;

      else if(point.X > size.Width)
        return response;

      else if(point.Y > size.Height)
        return response;

      else
        response = true;

      return response;
    }

    private static void MouseMoveControl(Point start, Point end, Speed speed) {
      bool startOk = CheckCorner(start);
      bool endOk = CheckCorner(end);

      if(!startOk)
        throw new RequiredParamsException(Situation.OutOfBounds, nameof(start));

      if(!endOk)
        throw new RequiredParamsException(Situation.OutOfBounds, nameof(end));

      int x = start.X, y = start.Y, cc = 0;
      bool endx = false, endy = false;

      // Sets instantaneous movement to the end position of the cursor.
      if(speed == Speed.Full) {
        SetCursorPos(end.X, end.Y);
        return;
      }

      while(!endx || !endy) {

        /*Uses the Speed Variable value as an increment/decrement that changes the velocity at which drag occurs
         controls X to change the value of the looping stop variable
         */
        if(start.X > end.X) {
          if(x >= end.X)
            x -= (int)speed;
          else
            endx = true;

        } else {
          if(x < end.X)
            x += (int)speed;
          else
            endx = true;

        }

        /*Uses the Speed Variable value as an increment/decrement that changes the velocity at which drag occurs
         controls Y to change the value of the looping stop variable
         */
        if(start.Y > end.Y) {
          if(y >= end.Y)
            y -= (int)speed;
          else
            endy = true;

        } else {
          if(y < end.Y)
            y += (int)speed;
          else
            endy = true;

        }

        // Adds a delay so that the drag is not too fast and ends up crashing the application or not working properly
        if(cc % 4 == 0)
          Thread.Sleep(10);

        SetCursorPos(x, y);
        cc++;
      }
    }

    #endregion

    /// <summary>
    /// [EN]: Performs the action of scrolling the mouse scroll down<br></br>
    /// [PT-BR]: Executa a ação de rolar o scroll do mouse para baixo
    /// </summary>
    /// <param name="clicks">
    /// [EN]: Number of "clicks" to be executed.<br></br>
    /// [PT-BR]: Quantidade de "clicks" a serem executados.
    /// </param>
    public static void RollDown(uint clicks) {
      if(clicks < 0)
        throw new RequiredParamsException(Situation.LessThanZero, nameof(clicks));

      mouse_event(XConst.MOUSE_ABSOLUTE | XConst.MOUSE_WHEEL, 0, 0, (uint)-clicks, 0);
    }

    /// <summary>
    /// [EN]: Performs the action of scrolling the mouse scroll up<br></br>
    /// [PT-BR]: Executa a ação de rolar o scroll do mouse para cima
    /// </summary>
    /// <param name="clicks">
    /// [EN]: Number of "clicks" to be executed.<br></br>
    /// [PT-BR]: Quantidade de "clicks" a serem executados.
    /// </param>
    public static void RollUp(uint clicks) {
      if(clicks < 0)
        throw new RequiredParamsException(Situation.LessThanZero, nameof(clicks));

      mouse_event(XConst.MOUSE_WHEEL, 0, 0, clicks, 0);
    }

    /// <summary>
    /// [EN]: Capture current mouse position <br></br>
    /// [PT-BR]: Captura a posição atual do mouse
    /// </summary>
    /// <returns>
    /// [EN]: Returns a point with the current mouse XY coordinates <br></br>
    /// [PT-BR]: Retorna um ponto com as coordenadas XY atuais do mouse 
    /// </returns>
    public static Point GetPosition() {
      Point current = new Point();
      try {
        GetCursorPos(out current);
      } catch(Exception) { }
      return current;
    }

    /// <summary>
    /// [EN]: Perform mouse drag and drop motion<br></br>
    /// [PT-BR] Executa o movimento de arrastar e soltar do mouse
    /// </summary>
    /// <param name="start">
    /// [EN]: Start position X, Y to drag from<br></br>
    /// [PT-BR] Posição inicial X, Y de onde arrastar
    /// </param>
    /// <param name="end">
    /// [EN]: End position to drop<br></br>
    /// [PT-BR] Posição final para soltar
    /// </param>
    /// <param name="speed">
    /// [EN]: Mouse movement execution speed<br></br>
    /// [PT-BR]: velocidade da execução de movimentação do mouse
    /// </param>
    public static void Drag(Point start, Point end, Speed speed = Speed.X1) {
      MoveTo(start, speed);
      Thread.Sleep(300);
      mouse_event(XConst.MOUSE_LEFTDOWN, 0, 0, 0, 0);
      MoveTo(end, speed);
      Thread.Sleep(300);
      mouse_event(XConst.MOUSE_LEFTUP, 0, 0, 0, 0);
    }

    /// <summary>
    /// [EN]: Simulates a left mouse button click on the current X,Y position on the screen <br></br>
    /// [PT-BR]: Simula um click com o botão esquerdo do mouse na posição atual X, Y da tela
    /// </summary>
    /// <param name="doubleClick">
    /// [EN]: If marked true, double-click the location<br></br>
    /// [PT-BR]: Se marcado como verdadeiro, executa um click duplo no local
    /// </param>
    public static void LeftClick(bool doubleClick = false) {
      if(doubleClick) {
        mouse_event(XConst.MOUSE_LEFTDOWN | XConst.MOUSE_LEFTUP, 0, 0, 0, 0);
        Thread.Sleep(100);
        mouse_event(XConst.MOUSE_LEFTDOWN | XConst.MOUSE_LEFTUP, 0, 0, 0, 0);
        return;
      }
      mouse_event(XConst.MOUSE_LEFTDOWN | XConst.MOUSE_LEFTUP, 0, 0, 0, 0);

    }

    /// <summary>
    /// [EN]: Simulates a right mouse click at the current X,Y position on the screen<br></br>
    /// [PT-BR]: Simula um click com o botão direito do mouse na posição atual X, Y da tela
    /// </summary>
    /// <param name="doubleClick">
    /// [EN]: If marked true, double-click the location<br></br>
    /// [PT-BR]: Se marcado como verdadeiro, executa um click duplo no local
    /// </param>
    public static void RightClick(bool doubleClick = false) {
      if(doubleClick) {
        mouse_event(XConst.MOUSE_RIGHTDOWN | XConst.MOUSE_RIGHTUP, 0, 0, 0, 0);
        Thread.Sleep(100);
        mouse_event(XConst.MOUSE_RIGHTDOWN | XConst.MOUSE_RIGHTUP, 0, 0, 0, 0);
        return;
      }
      mouse_event(XConst.MOUSE_RIGHTDOWN | XConst.MOUSE_RIGHTUP, 0, 0, 0, 0);

    }

    #region OVERLOAD MOVETOANDCLICK

    /// <summary>
    /// [EN]: Make a combination of MoveTo() and LeftClick() or RightClick() to move the mouse to a certain position and trigger the click<br></br>
    /// [PT-BR]: Faz uma combinação de MoveTo() e LeftClick() ou RightClick() para movimentar o mouse a uma determinada posição e acionar o click
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
    /// <param name="speed">
    /// [EN]: Mouse movement execution speed<br></br>
    /// [PT-BR]: velocidade da execução de movimentação do mouse
    /// </param>
    /// /// <param name="doubleClick">
    /// [EN]: If marked true, double-click the location<br></br>
    /// [PT-BR]: Se marcado como verdadeiro, executa um click duplo no local
    /// </param>
    public static void MoveToAndClick(int x, int y, Speed speed = Speed.X2, bool doubleClick = false, bool leftbtn = true) {
      MoveTo(x, y, speed);
      if(leftbtn)
        LeftClick(doubleClick);
      else
        RightClick(doubleClick);

    }

    /// <summary>
    /// [EN]: Make a combination of MoveTo() and LeftClick() or RightClick() to move the mouse to a certain position and trigger the click<br></br>
    /// [PT-BR]: Faz uma combinação de MoveTo() e LeftClick() ou RightClick() para movimentar o mouse a uma determinada posição e acionar o click
    /// </summary>
    /// <param name="point">
    /// [EN]: Point with X Y coordinates to move<br></br>
    /// [PT-BR]: Ponto com corrdenadas X Y para mover
    /// </param>
    /// <param name="leftbtn">
    /// [EN]: Indicates which mouse button will be used to click. Default is true for left, if marked as false, right button will be triggered<br></br>
    /// [PT-BR]: Indica qual botão do mouse será utilizado para efetur o click. O padrão é true para esquerdo, se marcado como false, o botão direito será acionado
    /// </param>
    /// <param name="speed">
    /// [EN]: Mouse movement execution speed<br></br>
    /// [PT-BR]: velocidade da execução de movimentação do mouse
    /// </param>
    /// /// <param name="doubleClick">
    /// [EN]: If marked true, double-click the location<br></br>
    /// [PT-BR]: Se marcado como verdadeiro, executa um click duplo no local
    /// </param>
    public static void MoveToAndClick(Point point, Speed speed = Speed.X2, bool doubleClick = false, bool leftbtn = true) {
      MoveTo(point, speed);
      if(leftbtn)
        LeftClick(doubleClick);
      else
        RightClick(doubleClick);

    }

    #endregion

    #region OVERLOAD MOVETO

    /// <summary>
    /// [EN]: Invokes an action to make a mouse move at the indicated Point position. <br></br>
    /// [PT-BR]: Invoca a ação que realiza o movimento do mouse para as coordenadas do ponto indicado
    /// </summary>
    /// <param name="point">
    /// [EN]: Point with X Y coordinates to move<br></br>
    /// [PT-BR]: Ponto com corrdenadas X Y para mover
    /// </param>
    /// <param name="speed">
    /// [EN]: Mouse movement execution speed<br></br>
    /// [PT-BR]: velocidade da execução de movimentação do mouse
    /// </param>
    public static void MoveTo(Point point, Speed speed = Speed.X1) {
      Point start = GetPosition();
      MouseMoveControl(start, point, speed);
    }

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
    /// <param name="speed">
    /// [EN]: Mouse movement execution speed<br></br>
    /// [PT-BR]: velocidade da execução de movimentação do mouse
    /// </param>
    public static void MoveTo(int x, int y, Speed speed = Speed.X1) {
      Point start = GetPosition();
      MouseMoveControl(start, new(x, y), speed);
    }

    #endregion
  }
}
