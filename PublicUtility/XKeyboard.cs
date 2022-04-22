using PublicUtility.Xnm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Handles keyboard events in a virtual way to combine keys, press mod keys, long digits, short digits. Currently in experimental / testing phase<br></br>
  /// [PT-BR]: Manipula eventos do teclado de forma virtual para combinar teclas, pressionar cahves de modificação, digitos longos, digitos curtos. Atualmente em fase experimental  / testes
  /// </summary>
  public static class XKeyboard {

    #region INTEROPT DLL IMPORTS

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern short GetKeyState(int keyCode);

    #endregion 

    #region PRIVATE METHODS

    private static void CombineMultiKeys(KeyAction action, List<Key> keys) {
      List<Key> blacklist = new List<Key>() { Key.Shif, Key.Alt, Key.Ctrl, Key.Tab, Key.Esc };

      foreach(Key key in keys) {
        if(action == KeyAction.Press) {
          PressKey(key);

        } else {
          UnPressKey(key);

        }

      }

    }


    #endregion 

    /// <summary>
    /// [EN]: Press a key using the virtual keyboard<br></br>
    /// [PT-BR]: Pressiona um tecla utilizando o teclado virtual
    /// </summary>
    /// <param name="key">
    /// [EN]: Key to press<br></br>
    /// [PT-BR]: Tecla a ser pressionada
    /// </param>
    public static void PressKey(Key key) {
      keybd_event((uint)key, 0, (uint)KeyAction.Press, 0);
      Thread.Sleep(500);
    }

    /// <summary>
    /// [EN]: Release a key that is pressed<br></br>
    /// [PT-BR]: Solta uma tecla que está pressionada
    /// </summary>
    /// <param name="key">
    /// [EN]: Key to be released<br></br>
    /// [PT-BR]: chave a ser liberada
    /// </param>
    public static void UnPressKey(Key key) {
      keybd_event((uint)key, 0, (uint)KeyAction.Drop, 0);
      Thread.Sleep(500);
    }

    /// <summary>
    /// [EN]: Performs a key press and release action<br></br>
    /// [PT-BR]: Efetua uma ação de pressionar e soltar uma tecla
    /// </summary>
    /// <param name="key">
    /// [EN]: Key to be pressed and released<br></br>
    /// [PT-BR]: Tecla a ser pressionada e liberada
    /// </param>
    public static void SigleDigit(Key key) {
      PressKey(key);
      UnPressKey(key);
    }

    /// <summary>
    /// [EN]: Combine two or more keys to perform keyboard actions<br></br>
    /// [PT-BR]: Combina duas ou mais teclas  para executar ações do teclado
    /// </summary>
    /// <param name="action">
    /// [EN]: Action to take (press or release)<br></br>
    /// [PT-BR]: Ação a ser feita (pressionar ou soltar)
    /// </param>
    /// <param name="keys">
    /// [EN]: Keys that will be used<br></br>
    /// [PT-BR]: Chaves que serão usadas
    /// </param>
    /// <remarks>
    /// IN TESTING PHASE!
    /// </remarks>
    public static void KeyCombine(KeyAction action, Key[] keys) {
      CombineMultiKeys(action, keys.ToList());
    }

    /// <summary>
    /// [EN]: Combine two or more keys to perform keyboard actions<br></br>
    /// [PT-BR]: Combina duas ou mais teclas  para executar ações do teclado
    /// </summary>
    /// <param name="action">
    /// [EN]: Action to take (press or release)<br></br>
    /// [PT-BR]: Ação a ser feita (pressionar ou soltar)
    /// </param>
    /// <param name="keys">
    /// [EN]: Keys that will be used<br></br>
    /// [PT-BR]: Chaves que serão usadas
    /// </param>
    /// <remarks>
    /// IN TESTING PHASE!
    /// </remarks>
    public static void KeyCombine(KeyAction action, List<Key> keys) {
      CombineMultiKeys(action, keys);
    }

    /// <summary>
    /// [EN]: Get information about the current state of a key<br></br>
    /// [PT-BR]: Obtém a informação sobre o atual estado de uma tecla
    /// </summary>
    /// <param name="key">
    /// [EN]: Destination key<br></br>
    /// [PT-BR]: Tecla de destino
    /// </param>
    /// <returns>
    /// [EN]: Returns a struct that contains the key details<br></br>
    /// [PT-BR]: Retorna uma estrutura que contém os detalhes da tecla
    /// </returns>
    public static KeyDetails KeyStateInfo(Key key) {
      short value = GetKeyState((int)key);
      byte[] bits = BitConverter.GetBytes(value);
      bool toggled = bits[0] > 0, pressed = bits[1] > 0;

      return new(pressed, toggled);
    }



  }

}
