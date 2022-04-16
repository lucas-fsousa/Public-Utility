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
    OutOfBounds
  }

  public enum Speed {
    X1 = 1,
    X2 = 2,
    X3 = 3,
    X4 = 4
  }

}
