using System;

namespace App.Utils.CustomExceptions.Base {
  /// <summary>
  /// [EN]: Base class for custom exceptions <br></br>
  /// [PT-BR]: Classe base para exceções personalizadas
  /// </summary>
  public abstract class BaseException: Exception {
    /// <summary>
    /// [EN]: Date of occurrence<br></br>
    /// [PT-BR]: Data da ocorrência
    /// </summary>
    public DateTime ErrorDateTime { get; }

    /// <summary>
    /// [EN]: Required unique constructor of custom exception base class <br></br>
    /// [PT-BR]: Construtor unico obrigatório da classe base de exceções personalizada
    /// </summary> 
    /// <param name="message">
    /// [EN]: Message to be displayed when exception is thrown<br></br>
    /// [PT-BR]: Mensagem a ser exibida quando a exceção for lançada
    /// </param>
    public BaseException(string message) : base(message) {
      this.ErrorDateTime = DateTime.Now;
    }

    /// <summary>
    /// [EN]: Possible situations that caused the exception <br></br>
    /// [PT-BR]: Possiveis situações que ocasionaram a exceção
    /// </summary>
    public enum Situations {
      IsNullOrEmpty,
      LessThanZero,
      AboveTheAllowed,
      BelowTheNecessary,
      InvalidFormat,
      NotANumber,
      InvalidType,
      NotExists
    }
  }

}
