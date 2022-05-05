using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicUtility.CustomExceptions;
using PublicUtility.Xnm;

namespace PublicUtility {
  public static class XTextFile {

    #region PRIVATE METHODS

    private static List<string> GetAllTextLines(string filePath, string lineDelimiter, int uniqueLineNumber = 0, int startIndex = 0, int endIndex = 0, List<int> multiLines = null) {

      List<string> response = new List<string>();
      List<string> lines = File.ReadAllText(filePath).Split(lineDelimiter).ToList();


      if(uniqueLineNumber > 0) {
        if(lines.Count >= uniqueLineNumber)
          response.Add(lines[uniqueLineNumber - 1]);

      } else if(startIndex > 0 && endIndex > 0) {

        if(lines.Count >= startIndex) {
          int id = 0;
          lines.ForEach(x => {
            id++;

            if(id >= startIndex && id <= endIndex)
              response.Add(x);
          });
        }

      } else if(multiLines != null) {

        int id = 0;
        lines.ForEach(x => {
          id++;

          if(multiLines.Contains(id))
            response.Add(x);
        });

      }

      return response;
    }

    private static void BaseReplaceLine(string filePath, string newTextLine, string delimiter, string oldTextLine = "", int oldTextLineIndex = 0, bool onlyFirstLocated = false) {
      string[] textLines = File.ReadAllText(filePath).Split(delimiter);
      bool change = false;

      #region REPLACE TEXTLINE FOR TEXTLINE

      if(!string.IsNullOrEmpty(oldTextLine)) {
        for(int i = 0; i < textLines.Length; i++) {

          if(textLines[i] == oldTextLine) {
            change = true;
            textLines[i] = newTextLine;

            if(onlyFirstLocated)
              break;
          }
        }

        if(change) {
          File.WriteAllText(filePath, "");
          AppendMultLines(filePath, textLines);
        }


        return;
      }

      #endregion

      #region REPLACE TEXLINE BY INDEX OF LINE

      if(oldTextLineIndex > 0) {
        for(int i = 0; i < textLines.Length; i++) {

          if(i + 1 == oldTextLineIndex) {
            change = true;
            textLines[i] = newTextLine;

            break;
          }
        }

        if(change) {
          File.WriteAllText(filePath, "");
          AppendMultLines(filePath, textLines);
        }

        return;
      }

      #endregion

    }

    #endregion

    public static string GetTextLine(string filePath, int lineNumber, string lineDelimiter = "\r\n") {

      #region PARAM VALIDATION

      if(lineNumber < 0)
        throw new RequiredParamsException(Situation.LessThanZero, nameof(lineNumber));

      if(string.IsNullOrEmpty(filePath))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(filePath));

      if(!File.Exists(filePath))
        throw new RequiredParamsException(Situation.NotExists, nameof(filePath));

      #endregion

      return GetAllTextLines(filePath, lineDelimiter, uniqueLineNumber: lineNumber).FirstOrDefault();
    }

    public static void AppendLine(string filePath, string newTextLine) => File.AppendAllText(filePath, newTextLine + "\r\n");

    public static void AppendMultLines(string filePath, IEnumerable<string> lines) {
      int i = 0;
      foreach(string line in lines) {
        i++;

        if(i == lines.Count() && string.IsNullOrEmpty(line)) // ignore blank line in end text
          continue;

        AppendLine(filePath, line);
      }

    }

    public static string GetText(string filePath) => File.ReadAllText(filePath);

    public static void CreateText(string filePath, string text) => File.WriteAllText(filePath, text);

    #region OVERLOAD REPLACELINE

    public static void ReplaceLine(string filePath, string oldTextLine, string newTextLine, bool onlyFirstLocated, string lineDelimiter = "\r\n") {
      
      #region PARAM VALIDATION

      if(string.IsNullOrEmpty(filePath))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(filePath));

      if(!File.Exists(filePath))
        throw new RequiredParamsException(Situation.NotExists, nameof(filePath));

      if(newTextLine == null)
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(newTextLine));

      if(oldTextLine == null)
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(oldTextLine));

      #endregion

      BaseReplaceLine(filePath, newTextLine, delimiter: lineDelimiter, oldTextLine, onlyFirstLocated: onlyFirstLocated);
    }

    public static void ReplaceLine(string filePath, int oldTextLineNumber, string newTextLine, string lineDelimiter = "\r\n") {
      #region PARAM VALIDATION

      if(string.IsNullOrEmpty(filePath))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(filePath));

      if(!File.Exists(filePath))
        throw new RequiredParamsException(Situation.NotExists, nameof(filePath));

      if(oldTextLineNumber < 0)
        throw new RequiredParamsException(Situation.LessThanZero, nameof(oldTextLineNumber));

      #endregion

      BaseReplaceLine(filePath, newTextLine, delimiter: lineDelimiter, oldTextLineIndex: oldTextLineNumber);
    }

    #endregion

    #region OVERLOAD GETTEXTLINES

    public static List<string> GetTextLines(string filePath, int startAt, int finishIn, string lineDelimiter = "\r\n") {

      #region PARAM VALIDATION

      if(startAt < 0)
        throw new RequiredParamsException(Situation.LessThanZero, nameof(startAt));

      if(finishIn < 0)
        throw new RequiredParamsException(Situation.LessThanZero, nameof(finishIn));

      if(string.IsNullOrEmpty(filePath))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(filePath));

      if(!File.Exists(filePath))
        throw new RequiredParamsException(Situation.NotExists, nameof(filePath));

      #endregion

      return GetAllTextLines(filePath, lineDelimiter, startIndex: startAt, endIndex: finishIn);
    }

    public static List<string> GetTextLines(string filePath, List<int> SpecificLines, string lineDelimiter = "\r\n") {

      #region PARAM VALIDATION

      if(SpecificLines.Count < 0)
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(SpecificLines));

      if(string.IsNullOrEmpty(filePath))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(filePath));

      if(!File.Exists(filePath))
        throw new RequiredParamsException(Situation.NotExists, nameof(filePath));

      #endregion

      return GetAllTextLines(filePath, lineDelimiter, multiLines: SpecificLines);
    }

    public static List<string> GetTextLines(string filePath, int[] SpecificLines, string lineDelimiter = "\r\n") {

      #region PARAM VALIDATION

      if(SpecificLines.Length < 0)
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(SpecificLines));

      if(string.IsNullOrEmpty(filePath))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(filePath));

      if(!File.Exists(filePath))
        throw new RequiredParamsException(Situation.NotExists, nameof(filePath));

      #endregion

      return GetAllTextLines(filePath, lineDelimiter, multiLines: SpecificLines.ToList());
    }

    #endregion
  }
}
