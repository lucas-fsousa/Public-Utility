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
      List<char> numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' , '.'};
      int countFloatPoint = 0;

      foreach(char c in input.ToList()) {
        if(c == '.')
          countFloatPoint++;

        if(countFloatPoint > 1)
          return false;

        if(!numbers.Contains(c))
          return false;
      }

      return true;
    }

    #region Overload MaxMin

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this float value, float min, float max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this decimal value, decimal min, decimal max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this double value, double min, double max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this long value, long min, long max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this short value, short min, short max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this ushort value, ushort min, ushort max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this int value, int min, int max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this nint value, nint min, nint max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this uint value, uint min, uint max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this nuint value, nuint min, nuint max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this ulong value, ulong min, ulong max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this byte value, byte min, byte max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    /// <summary>
    /// [EN]: Checks if the value entered is between the maximum and the minimum<br></br>
    /// [PT-BR]: Verifica se o valor informado está entre o maximo e o minimo
    /// </summary>
    /// <param name="value">
    /// [EN]: Value to be compared<br></br>
    /// [PT-BR]: valor a ser comparado
    /// </param>
    /// <param name="min">
    /// [EN]: Minimum expected<br></br>
    /// [PT-BR]: Minimo esperado
    /// </param>
    /// <param name="max">
    /// [EN]: Expected maximum<br></br>
    /// [PT-BR]: Maximo esperado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean if the value entered is between the highest and lowest expected<br></br>
    /// [PT-BR]: Retorna booleano se o valor entrado estiver entre o maior e o menor esperado
    /// </returns>
    public static bool MaxMin(this sbyte value, sbyte min, sbyte max) {
      Func<bool> checkValue = () => value <= max && value >= min;
      return checkValue();
    }

    #endregion


  }
}
