using static PublicUtility.CustomExceptions.Base.BaseException;
using PublicUtility.CustomExceptions;
using System.Collections.Generic;
using System.Linq;
using System;
using zip = System.IO.Compression;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Public utility class that contains several methods to aid in application development <br></br>
  /// [PT-BR]: Classe de utilidade publica que contém diversos métodos para auxiliar no desenvolvimento de aplicações
  /// </summary>
  /// <remarks>
  /// [EN]: This class may throw an incorrectly formatted Image exception. To fix, go to consoleapp properties and change the "Platform target" to [x86] which by default is marked with [Any CPU] <br></br>
  /// [PT-BR]: Esta classe pode gerar exceção de Imagem com formato incorreto. Para corrigir, acesse as propriedades do consoleapp e altere o "Destino da plataforma" para [x86] que por padrão está marcada com [Any CPU]
  /// </remarks>
  /// <exception cref="BadImageFormatException"></exception>
  public static class X {

    #region OTHERS
    
    /// <summary>
    /// [EN]: Captures keyboard input and converts it to the object type given during the method call <br></br>
    /// [PT-BR]: Captura a entrada do teclado e converte para o tipo de objeto informado durante a chamada do método
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of object to be input and returned <br></br>
    /// [PT-BR]: Tipo do objeto a ser inputado e retornado
    /// </typeparam>
    /// <param name="messageToPrint">
    /// [EN]: Informational message to be displayed on the console <br></br>
    /// [PT-BR]: mensagem informativa a ser exibida no console
    /// </param>
    /// <param name="hidden">
    /// [EN]: Defines if the keyboard input will be written in the console <br></br>
    /// [PT-BR]: Define se o input do teclado será escrito no console
    /// </param>
    /// <returns>
    /// [EN]: Returns an object of the type that was informed during the method call <br></br>
    /// [PT-BR]: Retorna um objeto do tipo que foi informado durante a chamada do método
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static T Input<T>(string messageToPrint = "", bool hidden = false) {
      T response = default;

      #region PRE-VALIDATION OF VALID INPUTS

      Dictionary<string, object> validInputs = new Dictionary<string, object>();
      validInputs.Add("datetime", typeof(DateTime));
      validInputs.Add("object", typeof(object));
      validInputs.Add("ushort", typeof(ushort));
      validInputs.Add("string", typeof(string));
      validInputs.Add("ulong", typeof(ulong));
      validInputs.Add("short", typeof(short));
      validInputs.Add("nuint", typeof(nuint));
      validInputs.Add("sbyte", typeof(sbyte));
      validInputs.Add("uint", typeof(uint));
      validInputs.Add("long", typeof(long));
      validInputs.Add("char", typeof(char));
      validInputs.Add("byte", typeof(byte));
      validInputs.Add("nint", typeof(nint));
      validInputs.Add("bool", typeof(bool));
      validInputs.Add("int", typeof(int));

      if(!validInputs.ContainsValue(typeof(T)))
        throw new RequiredParamsException(Situations.InvalidType, nameof(T));

      #endregion

      if(string.IsNullOrEmpty(messageToPrint))
        messageToPrint = string.Format(">> ");

      Console.Write(messageToPrint);

      try {
        string reader = string.Empty;

        if(hidden) {
          ConsoleKeyInfo enterKey;
          do {
            enterKey = Console.ReadKey(true);

            if(enterKey.Key == ConsoleKey.Enter) {
              Console.Write("\n");
              break;
            }

            reader += enterKey.KeyChar;
            Console.Write('*');
            GC.Collect();
          } while(true);
        } else {
          reader = Console.ReadLine();

        }

        response = (T)Convert.ChangeType(reader, typeof(T));
      } catch(Exception) { }

      return response;
    }
    
    /// <summary>
    /// [EN]: Unzip a .zip file in the destination folder<br></br>
    /// [PT-BR]: Descompacta um arquivo .zip na pasta de destino
    /// </summary>
    /// <param name="zipFilePath">
    /// [EN]: caminho de onde o arquivo zip está localizado.<br></br>
    /// [PT-BR]: Caminho de onde o arquivo zip está localizado.
    /// </param>
    /// <param name="destinationDir">
    /// [EN]: Destination folder address<br></br>
    /// [PT-BR]: Endereço da pasta de destino
    /// </param>
    public static void UnzipFile(string zipFilePath, string destinationDir) => zip.ZipFile.ExtractToDirectory(zipFilePath, destinationDir);
    
    /// <summary>
    /// [EN]: Compress a file to .zip<br></br>
    /// [PT-BR]: Compacta um arquivo para .zip
    /// </summary>
    /// <param name="fileToZipPath">
    /// [EN]: Path of the file to be compressed<br></br>
    /// [PT-BR]: Caminho do arquivo a ser compactado
    /// </param>
    /// <param name="fileZipDir">
    /// [EN]: Path of the folder where the file will be saved<br></br>
    /// [PT-BR]: Caminho da pasta onde o arquivo será salvo
    /// </param>
    public static void ZipFile(string fileToZipPath, string fileZipDir) => zip.ZipFile.CreateFromDirectory(fileToZipPath, fileZipDir);

    #region Overload Print

    /// <summary>
    /// [EN]: Console.WriteLine() simplification / Write to console <br></br>
    /// [PT-BR]: Simplificação do Console.WriteLine() / Escreve no console
    /// </summary>
    /// <param name="message">
    /// [EN]: Message to be written to console <br></br>
    /// [PT-BR]: Mensagem a ser escrita no console
    /// </param>
    public static void Print(this string message) => Console.WriteLine(message);

    /// <summary>
    /// [EN]: Console.WriteLine() simplification / Write to console <br></br>
    /// [PT-BR]: Simplificação do Console.WriteLine() / Escreve no console
    /// </summary>
    /// <param name="message">
    /// [EN]: Message to be written to console <br></br>
    /// [PT-BR]: Mensagem a ser escrita no console
    /// </param>
    /// <param name="args">
    /// [EN]: Array of additional arguments to be written to the console <br></br>
    /// [PT-BR]: Array de argumentos adicionais para ser escrito no console
    /// </param>
    public static void Print(this string message, params object[] args) => Console.WriteLine(message, args);

    /// <summary>
    /// [EN]: Console.WriteLine() simplification / Write to console <br></br>
    /// [PT-BR]: Simplificação do Console.WriteLine() / Escreve no console
    /// </summary>
    /// <param name="message">
    /// [EN]: Message to be written to console <br></br>
    /// [PT-BR]: Mensagem a ser escrita no console
    /// </param>
    /// <param name="obj">
    /// [EN]: Object to be written to the console along with the message <br></br>
    /// [PT-BR]: Objeto a ser escrito no console juntamente com a mensagem
    /// </param>
    public static void Print(this string message, object obj) => Console.WriteLine(message, obj);

    /// <summary>
    /// [EN]: Console.WriteLine() simplification / Write to console <br></br>
    /// [PT-BR]: Simplificação do Console.WriteLine() / Escreve no console
    /// </summary>
    /// <param name="obj">
    /// [EN]: Object to be written to the console<br></br>
    /// [PT-BR]: Objeto a ser escrito no console
    /// </param>
    public static void Print(this object obj) => Console.WriteLine(obj);
    
    #endregion
    
    #endregion

    #region EXTENSION

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
      List<char> numbers = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
      int countFloatPoint = 0;

      if(string.IsNullOrEmpty(input))
        return false;

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

    /// <summary>
    /// [EN]: Checks if the string has some boolean format <br></br>
    /// [PT-BR]: Verifica se a cadeia de caracteres possui algum formato booleano
    /// </summary>
    /// <param name="input">
    /// [EN]: String to be checked <br></br>
    /// [PT-BR]: Cadeia de caracteres a ser verificada
    /// </param>
    /// <returns>
    /// [EN]: returns a boolean value indicating whether the string has boolean format <br></br>
    /// [PT-BR]: retorna um valor booleano indicando se a string possui formato boolean
    /// </returns>
    public static bool IsSomeBool(this string input) {
      if(string.IsNullOrEmpty(input))
        return false;

      else if(input == string.Format("false") || input == string.Format("False"))
        return true;

      else if(input == string.Format("true") || input == string.Format("True"))
        return true;

      else
        return false;
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

    #region Overload GetNegatives

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static int[] GetNegatives(this int[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<int> GetNegatives(this List<int> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static decimal[] GetNegatives(this decimal[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<decimal> GetNegatives(this List<decimal> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static float[] GetNegatives(this float[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<float> GetNegatives(this List<float> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static double[] GetNegatives(this double[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<double> GetNegatives(this List<double> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static long[] GetNegatives(this long[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<long> GetNegatives(this List<long> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static nint[] GetNegatives(this nint[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<nint> GetNegatives(this List<nint> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static short[] GetNegatives(this short[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<short> GetNegatives(this List<short> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all negative numbers from Array<br></br>
    /// [PT-BR]: Retorna todos os numeros negativos do Array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only negative numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros negativos
    /// </returns>
    public static sbyte[] GetNegatives(this sbyte[] array) {
      return array.Where(x => x < 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all nevative numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros nevativos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only nevative numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros negativos
    /// </returns>
    public static List<sbyte> GetNegatives(this List<sbyte> list) {
      return list.Where(x => x < 0).OrderByDescending(x => x).ToList();
    }

    #endregion

    #region Overload GetPositives

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<int> GetPositives(this List<int> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static int[] GetPositives(this int[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static decimal[] GetPositives(this decimal[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<decimal> GetPositives(this List<decimal> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static float[] GetPositives(this float[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<float> GetPositives(this List<float> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static double[] GetPositives(this double[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<double> GetPositives(this List<double> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static long[] GetPositives(this long[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<long> GetPositives(this List<long> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static nint[] GetPositives(this nint[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<nint> GetPositives(this List<nint> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<short> GetPositives(this List<short> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static short[] GetPositives(this short[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from list<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos da lista
    /// </summary>
    /// <param name="list">
    /// [EN]: List of numbers<br></br>
    /// [PT-BR]: Lista de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list with only positive numbers<br></br>
    /// [PT-BR]: Retorna uma nova lista somente com os numeros positive
    /// </returns>
    public static List<sbyte> GetPositives(this List<sbyte> list) {
      return list.Where(x => x >= 0).OrderByDescending(x => x).ToList();
    }

    /// <summary>
    /// [EN]: Returns all positive numbers from array<br></br>
    /// [PT-BR]: Retorna todos os numeros positivos do array
    /// </summary>
    /// <param name="array">
    /// [EN]: Array of numbers<br></br>
    /// [PT-BR]: Array de numeros
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array with only positive numbers<br></br>
    /// [PT-BR]: Retorna um novo array somente com os numeros positivos
    /// </returns>
    public static sbyte[] GetPositives(this sbyte[] array) {
      return array.Where(x => x >= 0).OrderByDescending(x => x).ToArray();
    }

    #endregion

    #endregion

  }
}
