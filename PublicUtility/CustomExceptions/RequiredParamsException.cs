using PublicUtility.CustomExceptions.Base;
using PublicUtility.Xnm;
using System;

namespace PublicUtility.CustomExceptions {

  [Serializable]
  public class RequiredParamsException: BaseException {
    private static string ErrorMessage => "Required parameters not filled in correctly";
    public Situation Situation { get; }
    public string ParamName { get; }
    public RequiredParamsException(Situation situation, string paramName) : base(ErrorMessage) {
      this.Situation = situation;
      this.ParamName = paramName;
    }
  }
}
