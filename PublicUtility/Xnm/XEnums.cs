using System;
using System.Collections.Generic;

namespace PublicUtility.Xnm {
  /// <summary>
  /// [EN]: Possible situations that caused the exception <br></br>
  /// [PT-BR]: Possiveis situações que ocasionaram a exceção
  /// </summary>
  public enum Situation {
    IsNullOrEmpty,
    LessThanZero,
    AboveTheAllowed,
    BelowTheNecessary,
    InvalidFormat,
    NotANumber,
    InvalidType,
    NotExists,
    OutOfBounds,
    KeyNotFound
  }

  /// <summary>
  /// [EN]: Represents mouse movement speed<br></br>
  /// [PT-BR]: Representa a velocidade de movimentação do mouse 
  /// </summary>
  public enum MouseSpeed {
    X1 = 1,
    X2 = 2,
    X3 = 3,
    X4 = 4,
    Full = 5
  }

  /// <summary>
  /// [EN]: Represents the action of Pressing or Releasing a key<br></br>
  /// [PT-BR]: Representa a ação de Pressionar ou Soltar uma tecla
  /// </summary>
  [Flags]
  public enum KeyAction {
    Press = 0,
    Drop = 0x2,
  }

  /// <summary>
  /// [EN]: Represents the main virtual keys that can be used to simulate Keyboard input <br></br>
  /// [PT-BR]: Representa as principais teclas virtuais que podem ser utilizadas para simular um input do teclado
  /// </summary>
  [Flags]
  public enum Key {
    A = 0x41,
    B = 0x42,
    C = 0x43,
    D = 0x44,
    E = 0x45,
    F = 0x46,
    G = 0x47,
    H = 0x48,
    I = 0x49,
    J = 0x4A,
    K = 0x4B,
    L = 0x4C,
    M = 0x4D,
    N = 0x4E,
    O = 0x4F,
    P = 0x50,
    Q = 0x51,
    R = 0x52,
    S = 0x53,
    T = 0x54,
    U = 0x55,
    V = 0x56,
    W = 0x57,
    X = 0x58,
    Y = 0x59,
    Z = 0x5A,
    Sleep = 0x5F,
    Num0 = 0x60,
    Num1 = 0x61,
    Num2 = 0x62,
    Num3 = 0x63,
    Num4 = 0x64,
    Num5 = 0x65,
    Num6 = 0x66,
    Num7 = 0x67,
    Num8 = 0x68,
    Num9 = 0x69,
    F1 = 0x70,
    F2 = 0x71,
    F3 = 0x72,
    F4 = 0x73,
    F5 = 0x74,
    F6 = 0x75,
    F7 = 0x76,
    F8 = 0x77,
    F9 = 0x78,
    F10 = 0x79,
    F11 = 0x7A,
    F12 = 0x7B,
    F13 = 0x7C,
    F14 = 0x7D,
    F15 = 0x7E,
    F16 = 0x7F,
    F17 = 0x80,
    F18 = 0x81,
    F19 = 0x82,
    F20 = 0x83,
    F21 = 0x84,
    F22 = 0x85,
    F23 = 0x86,
    F24 = 0x87,
    NumLock = 0x90,
    ScrollLock = 0x91,
    Shif = 0x10,
    Ctrl = 0x11,
    Alt = 0x12,
    CapsLock = 0x14,
    Space = 0x20,
    PageUP = 0x21,
    PageDown = 0x22,
    End = 0x23,
    Home = 0x24,
    LeftArrow = 0x25,
    RightArrow = 0x27,
    DownArrow = 0x28,
    UpArrow = 0x26,
    Delete = 0x2E,
    Enter = 0x0D,
    PrtSc = 0x2C,
    Tab = 0x09,
    Backspace = 0x08,
    Esc = 0x1B,
    Div = 0x6F,
    Sub = 0x6D,
    Sum = 0x6B,
    Comma = 0x6E,
    Dot = 0xBE,
    Semicolon = 0xBA,
    Insert = 0x2D,
    Separator = 0x6C,
    Mult = 0x6A,
    N0 = 0x30,
    N1 = 0x31,
    N2 = 0x32,
    N3 = 0x33,
    N4 = 0x34,
    N5 = 0x35,
    N6 = 0x36,
    N7 = 0x37,
    N8 = 0x38,
    N9 = 0x39
  }

  /// <summary>
  /// [EN]: Represents the current state of a window (hidden or displayed)<br></br>
  /// [PT-BR]: Representa o estado atual de uma janela(oculta ou exibida)
  /// </summary>
  public enum WidowMode {
    Hide = 0,
    Show = 5
  }

  /// <summary>
  /// [EN]: Represents the action the mouse will take, be it clicking, moving, scrolling, etc.<br></br>
  /// [PT-BR]: Representa a ação que o mouse tomará, seja clicar, mover, rolar e etc.s
  /// </summary>
  public enum MouseAction {
    Wheel = 0x0800,
    HWhell = 0x01000,
    LeftDown = 0x02,
    LeftUp = 0x04,
    RightDown = 0x08,
    RightUp = 0x10,
    Absolute = 0x8000,
    Move = 0x0001
  }
  
}
