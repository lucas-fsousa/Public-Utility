using App.Utils.CustomExceptions.Base;
using System;

namespace App.Utils.CustomExceptions {
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
