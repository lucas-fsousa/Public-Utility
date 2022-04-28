using System;
using System.Drawing;
using System.Net;
using System.Text;

namespace PublicUtility.Xnm {

  /// <summary>
  /// [EN]: A box containing the dimensions and coordinates<br></br>
  /// [PT-BR]: Uma caixa contendo as dimensões e coordenadas 
  /// </summary>
  public struct Box {
    public Size Size { get; set; }
    public Point Location { get; set; }
    
    public Box(int w, int h, int x, int y) {
      Size = new Size(w, h);
      Location = new Point(x, y);
    }

    /// <summary>
    /// [EN]: Checks if the item has been filled<br></br>
    /// [PT-BR]:Verifica se o item foi preenchido
    /// </summary>
    /// <returns>
    /// [EN]: Returns the answer / true or false for the fill situation<br></br>
    /// [PT-BR]: Retorna a resposta /  verdadeiro ou falso para a situação de preenchimento
    /// </returns>
    public bool IsFilled() {
      if(Location.IsEmpty || Size.IsEmpty) 
        return false;
      return true;
    }

    /// <summary>
    /// [EN]: Gets the coordinates representing the center of the box<br></br>
    /// [PT-BR]: Obtém as coordendas que representam o centro da caixa
    /// </summary>
    /// <returns>
    /// [EN]: Returns a point with screen coordinates<br></br>
    /// [PT-BR]: Retorna um ponto com coordenadas de tela
    /// </returns>
    public Point GetCenterBox() {
      Point xy = new();
      xy.X = Location.X + (Size.Width / 2);
      xy.Y = Location.Y + (Size.Height / 2);
      return xy;
    }

    /// <summary>
    /// [EN]: Get the point where the box ends<br></br>
    /// [PT-BR]: Obtém o ponto onde a caixa chega ao fim
    /// </summary>
    /// <returns>
    /// [EN]: Returns a point with screen coordinates<br></br>
    /// [PT-BR]: Retorna um ponto com coordenadas de tela
    /// </returns>
    public Point GetEndBox() => new(Location.X + Size.Width, Location.Y + Size.Height);

    /// <summary>
    /// [EN]: Get the point at which the start of the box is located<br></br>
    /// [PT-BR]: Obtém o ponto no qual está localizado o inicio da caixa
    /// </summary>
    /// <returns>
    /// [EN]: Returns a point with screen coordinates<br></br>
    /// [PT-BR]: Retorna um ponto com coordenadas de tela
    /// </returns>
    public Point GetStartBox() => new(Location.X, Location.Y);
    
  }

  /// <summary>
  /// [EN]: Displays the breakdown of a virtual keyboard key<br></br>
  /// [PT-BR]: Apresenta o detalhamento de uma tecla do teclado virtual
  /// </summary>
  public struct KeyDetails {
    public bool IsPressed { get; }
    public bool IsToggled { get; }

    public KeyDetails(bool isPressed, bool isToggled) { 
      IsPressed = isPressed;
      IsToggled = isToggled;
    }
  }

  /// <summary>
  /// [EN]: Get server connection details<br></br>
  /// [PT-BR]: Obtém detalhes da conexão com o servidor
  /// </summary>
  public struct PingDetail {
    public long Ms { get; }
    public bool Timeout { get; }
    public string Status { get; }
    public IPAddress Address { get; }
    public int TimeToLive { get; }
    public bool DontFragmented { get; }

    /// <summary>
    /// [EN]: Required constructor to correctly fill in the attributes<br></br>
    /// [PT-BR]: Construtor necessário para preenchimento correto dos atributos
    /// </summary>
    /// <param name="ms">
    /// [EN]: Time in milliseconds spent on the request<br></br>
    /// [PT-BR]: Tempo em milessegundos gastos na requisição
    /// </param>
    /// <param name="status">
    /// [EN]: Status of the response obtained from the server<br></br>
    /// [PT-BR]: Status da resposta obtida do servidor
    /// </param>
    /// <param name="address">
    /// [EN]: IP address or Hostname of the service / server<br></br>
    /// [PT-BR]: Endereço IP ou Hostname do serviço / servidor
    /// </param>
    /// <param name="timeToLive">
    /// [EN]: Determines how long the record will be cached on servers<br></br>
    /// [PT-BR]: Determina por quanto tempo o registro será armazenado no cache dos servidores
    /// </param>
    /// <param name="dontFragmented">
    /// [EN]: Represents the response fragmentation action<br></br>
    /// [PT-BR]: Representa a ação de fragmentação da resposta
    /// </param>
    /// /// <param name="timeout">
    /// [EN]: Defines if there was a timeout or not<br></br>
    /// [PT-BR]: Define se houve estouro de tempo ou não
    /// </param>
    public PingDetail(long ms, string status, IPAddress address, int timeToLive, bool dontFragmented, bool timeout) {
      this.Status = status; ;
      this.Ms = ms;
      this.Address = address;
      this.TimeToLive = timeToLive;
      this.DontFragmented = dontFragmented;
      this.Timeout = timeout;
    }

    /// <summary>
    /// [EN]: Checks if the item has been filled<br></br>
    /// [PT-BR]:Verifica se o item foi preenchido
    /// </summary>
    /// <returns>
    /// [EN]: Returns the answer / true or false for the fill situation<br></br>
    /// [PT-BR]: Retorna a resposta /  verdadeiro ou falso para a situação de preenchimento
    /// </returns>
    public bool IsFilled() {
      if(Ms == -1 && string.IsNullOrEmpty(Status) && Address == null && TimeToLive == -1) {
        return false;
      }

      return true;
    }

    public override string ToString() {
      return string.Format($"[ IP: {Address} | Fragmented: {DontFragmented} | Status: {Status} | TTL: {TimeToLive} | Ms: {Ms} ]");
    }
  }


}
