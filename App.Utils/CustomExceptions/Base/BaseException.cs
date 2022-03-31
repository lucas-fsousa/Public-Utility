using System;

namespace App.Utils.CustomExceptions.Base {
  /// <summary>
  /// [EN]: Base class for custom exceptions <br></br>
  /// [PT-BR]: Classe base para exceções personalizadas
  /// </summary>
  public abstract class BaseException: Exception {
    public DateTime ErrorDateTime { get; }
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
      InvalidFormat
    }
  }

}
