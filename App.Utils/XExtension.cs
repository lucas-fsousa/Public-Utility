using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Utils {

  /// <summary>
  /// [EN]: Public utility class that contains several methods to aid in application development <br></br>
  /// [PT-BR]: Classe de utilidade publica que contém diversos métodos para auxiliar no desenvolvimento de aplicações
  /// </summary>
  /// <remarks>
  /// [EN]: This class may throw an incorrectly formatted Image exception. To fix, go to consoleapp properties and change the "Platform target" to [x86] which by default is marked with [Any CPU] <br></br>
  /// [PT-BR]: Esta classe pode gerar exceção de Imagem com formato incorreto. Para corrigir, acesse as propriedades do consoleapp e altere o "Destino da plataforma" para [x86] que por padrão está marcada com [Any CPU]
  /// </remarks>
  /// <exception cref="BadImageFormatException"></exception>
  public static class XExtension {

    /// <summary>
    /// [EN]: Remove all whitespace from string <br></br>
    /// [PT-BR]: Remove todos os espaços em branco da string
    /// </summary>
    /// <param name="str">
    /// [EN]: string to be refactored <br></br>
    /// [PT-BR]: Cadeia de caracteres a ser refatorada
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string with no whitespace <br></br>
    /// [PT-BR]: Retorna uma nova string sem espaços em branco
    /// </returns>
    public static string RemoveWhiteSpaces(this string str) {
      string newStr = string.Empty;

      if(!string.IsNullOrEmpty(str)) {
        for(int i = 0; i < str.Length; i++) {
          if(str[i] == ' ') {
            continue;
          }
          newStr += str[i]; // Concatenates characters to form a string with no white spaces
        }
      }

      return newStr;
    }

    /// <summary>
    /// [EN]: Checks if the string has a valid numeric format containing no letters or special characters <br></br>
    /// [PT-BR]: verifica se a cadeia de caracteres possui formato numérico válido não contendo letras ou caracteres especiais
    /// </summary>
    /// <param name="input">
    /// </param>
    /// <returns>
    /// [EN]: Returns a boolean value <br></br>
    /// [PT-BR]: Returns a boolean value
    /// </returns>
    public static bool IsNumber(this string input) {
      List<char> numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

      foreach(char c in input.ToList()) {
        if(!numbers.Contains(c))
          return false;
      }

      return true;
    }


  }
}
