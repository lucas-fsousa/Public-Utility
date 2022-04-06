using PublicUtility.CustomExceptions.Base;
using System;

namespace PublicUtility.CustomExceptions {
  [Serializable]
  public class RequiredParamsException: BaseException {
    private static string ErrorMessage => "Required parameters not filled in correctly";
    public Situations Situation { get; }
    public string ParamName { get; }
    public RequiredParamsException(Situations situation, string paramName) : base(ErrorMessage) {
      this.Situation = situation;
      this.ParamName = paramName;
    }
  }
}
