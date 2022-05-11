using PublicUtility.CustomExceptions;
using PublicUtility.Xnm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using zip = System.IO.Compression;

namespace PublicUtility {
#pragma warning disable CS8632

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

    #region PRIVATE METHODS

    private static string ConvertToDate(this object data) => data.GetSafeValue<DateTime>().ToString("s");

    private static Dictionary<string, char> GetSeparators() {
      Dictionary<string, char> separators = new Dictionary<string, char>();
      separators.Add("Dot", '.');
      separators.Add("Hyphen", '-');
      separators.Add("Bar", '/');
      separators.Add("Semicolon", ';');
      separators.Add("Colon", ':');

      return separators;
    }

    private static IEnumerable<T> OddNumbers<T>(IEnumerable<T> enumrable) {
      List<T> oddNumbers = new List<T>();
      foreach(var value in enumrable) {
        if(value.ToString().GetOnlyNumbers().Length <= 0) {
          string valueContainedInInput;
          throw new RequiredParamsException(Situation.NotANumber, nameof(valueContainedInInput));
        } else {
          if(Convert.ToInt64(value) % 2 == 0)
            oddNumbers.Add(value);
        }

      }
      return oddNumbers;
    }

    private static IEnumerable<T> EvenNumbers<T>(IEnumerable<T> enumrable) {
      List<T> oddNumbers = new List<T>();
      foreach(var value in enumrable) {
        if(value.ToString().GetOnlyNumbers().Length <= 0) {
          string valueContainedInInput;
          throw new RequiredParamsException(Situation.NotANumber, nameof(valueContainedInInput));
        } else {
          if(Convert.ToInt64(value) % 2 == 1)
            oddNumbers.Add(value);
        }

      }
      return oddNumbers;
    }

    private static int OneIndex<T>(IEnumerable<T> enumrable, T itemToLoc) {
      int index = -1, i = 0;
      foreach(var value in enumrable) {
        if(value.Equals(itemToLoc)) {
          index = i;
          break;
        }
        i++;
      }
      return index;
    }

    private static string GetOnly(IEnumerable<char> enumrable, string searchFor) {
      string newStr = string.Empty;
      foreach(char c in enumrable) {
        if(searchFor.Equals("symbol")) {
          if(char.IsSymbol(c))
            newStr += c;

        } else if(searchFor.Equals("number")) {
          if(char.IsNumber(c))
            newStr += c;

        } else if(searchFor.Equals("letter")) {
          if(char.IsLetter(c))
            newStr += c;

        } else if(searchFor.Equals("letterAndNumber")) {
          if(char.IsLetter(c) || char.IsNumber(c))
            newStr += c;

        } else if(searchFor.Equals("special")) {
          if(char.IsPunctuation(c))
            newStr += c;

        } else if(searchFor.Equals("upper")) {
          if(char.IsUpper(c))
            newStr += c;

        } else if(searchFor.Equals("lower")) {
          if(char.IsLower(c))
            newStr += c;

        } else if(searchFor.Equals("whitespace")) {
          if(char.IsWhiteSpace(c))
            newStr += c;

        }

      }
      return newStr;
    }

    private static T GetOneValue<T>(IEnumerable<T> enumrable) {
      return enumrable.ToList()[new Random().Next(0, enumrable.Count())];
    }

    private static Dictionary<string, object> PrimitiveTypes() {
      Dictionary<string, object> validInputs = new Dictionary<string, object>();
      validInputs.Add("datetime", typeof(DateTime));
      validInputs.Add("decimal", typeof(decimal));
      validInputs.Add("object", typeof(object));
      validInputs.Add("double", typeof(double));
      validInputs.Add("string", typeof(string));
      validInputs.Add("ushort", typeof(ushort));
      validInputs.Add("ulong", typeof(ulong));
      validInputs.Add("float", typeof(float));
      validInputs.Add("nuint", typeof(nuint));
      validInputs.Add("short", typeof(short));
      validInputs.Add("sbyte", typeof(sbyte));
      validInputs.Add("nint", typeof(nint));
      validInputs.Add("long", typeof(long));
      validInputs.Add("uint", typeof(uint));
      validInputs.Add("char", typeof(char));
      validInputs.Add("byte", typeof(byte));
      validInputs.Add("bool", typeof(bool));
      validInputs.Add("int", typeof(int));

      return validInputs;
    }

    #endregion

    /// <summary>
    /// [EN]: Captures Keyboard input and converts it to the object type given during the method call <br></br>
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
    /// [EN]: Defines if the Keyboard input will be written in the console <br></br>
    /// [PT-BR]: Define se o input do teclado será escrito no console
    /// </param>
    /// <returns>
    /// [EN]: Returns an object of the type that was informed during the method call <br></br>
    /// [PT-BR]: Retorna um objeto do tipo que foi informado durante a chamada do método
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static T Input<T>(string messageToPrint = "", bool hidden = false) {
      T response = default;

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
      } catch(Exception) {
        throw new RequiredParamsException(Situation.InvalidFormat, nameof(T));
      }

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

    /// <summary>
    /// [EN]: Requests a response from the server to check the status of the connection.<br></br>
    /// [PT-BR]: Solicita uma resposta do servidor para verificar o status da conexão.
    /// </summary>
    /// <param name="ipOrHostname">
    /// [EN]: IP address or hostname of the location that will be requested<br></br>
    /// [PT-BR]: Endereço IP ou hostname da localidade que será requisitada
    /// </param>
    /// <param name="timeout">
    /// [EN]: Time to wait until timeout is considered<br></br>
    /// [PT-BR]: Tempo de espera até que o tempo limite seja considerado
    /// </param>
    /// <returns>
    /// [EN]: Returns a struct containing the request details<br></br>
    /// [PT-BR]: Retorna uma estrutura contendo os detalhes da solicitação
    /// </returns>
    public async static Task<PingDetail> GetPing(string ipOrHostname, int timeout = 999) {
      int tlt = -1;
      long ms = -1;
      bool timeOver = false;
      bool dontFragmented = false;
      string status = string.Empty;
      IPAddress address = null;

      try {
        Ping ping = new();
        PingReply response = await ping.SendPingAsync(ipOrHostname, timeout);
        status = response.Status.ToString();

        if(status.ToLower() == "timedout") {
          status = "Timeout";
          timeOver = true;
          address = IPAddress.Parse(ipOrHostname);
          ms = -1;
          tlt = -1;
          dontFragmented = false;

        } else {
          address = response.Address;
          ms = response.RoundtripTime;
          tlt = response.Options.Ttl;
          dontFragmented = response.Options.DontFragment;

        }

      } catch(Exception) { }
      return new(ms, status, address, tlt, dontFragmented, timeOver);
    }

    /// <summary>
    /// [EN]: Adds a new item to the end of the array keeping the same reference<br></br>
    /// [PT-BR]: Adiciona um novo item ao final do array mantendo a mesma referencia
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of value being worked on<br></br>
    /// [PT-BR]: Tipo de valor que está sendo trabalhado
    /// </typeparam>
    /// <param name="array">
    /// [EN]: Array that will receive the value<br></br>
    /// [PT-BR]: Matriz que receberá o valor
    /// </param>
    /// <param name="value">
    /// [EN]: Value to insert into the array<br></br>
    /// [PT-BR]: Valor a ser inserido na matriz
    /// </param>
    public static void AddOnArray<T>(ref T[] array, T value) {
      Array.Resize(ref array, array.Length + 1);
      array[^1] = value;
    }

    #endregion

    #region EXTENSION

    /// <summary>
    /// [EN]: Checks if the item has a valid Datetime format<br></br>
    /// [PT-BR]: Verifica se o item possui formato de Data tempo válido
    /// </summary>
    /// <param name="input">
    /// [EN]: Item to be analyzed<br></br>
    /// [PT-BR]: Item a ser analisado
    /// </param>
    /// <returns>
    /// [EN]: Returns a boolean value indicating whether it is a valid or invalid datetime format.<br></br>
    /// [PT-BR]: Retorna valor booleano indicando se é um formato data tempo válido ou inválido.
    /// </returns>
    public static bool IsAnyDate(this string input) {
      try {
        if(string.IsNullOrEmpty(input))
          return false;

        DateTime newDate = Convert.ToDateTime(input);
        if(newDate.IsDefault())
          return false;

      } catch(Exception) { return false; }

      return true;
    }

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
        foreach(char c in str) {
          if(!char.IsWhiteSpace(c))
            newStr += c;
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

    /// <summary>
    /// [EN]: Checks if the value is the default of the specified type<br></br>
    /// [PT-BR]: Verifica se o valor é o padrão do tipo especificado
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of object to be analyzed<br></br>
    /// [PT-BR]: Tipo do objeto a ser analisado
    /// </typeparam>
    /// <param name="value">
    /// [EN]: Value to be analyzed<br></br>
    /// [PT-BR]: Valor a ser analisado
    /// </param>
    /// <returns>
    /// [EN]: Returns boolean representing the answer whether the value is the type default or not the type default<br></br>
    /// [PT-BR]: Retorna booleano representando a resposta se o valor é o padrão do tipo ou não é o padrão do tipo
    /// </returns>
    public static bool IsDefault<T>(this T value) {
      T def = default;
      if(value.Equals(def))
        return true;
      return false;
    }

    /// <summary>
    /// [EN]: Converts a data table into an object of the estimated type.<br></br>
    /// [PT-BR]: Converte uma tabela de dados em um objeto do tipo estimado.
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of object to be returned<br></br>
    /// [PT-BR]: Tipo do objeto que será retornado
    /// </typeparam>
    /// <param name="table">
    /// [EN]: Table containing all data to be converted<br></br>
    /// [PT-BR]: Tabela que contém todos os dados a serem convertidos
    /// </param>
    /// <param name="numTypes">
    /// [EN]: This item will be used to check if the returned type is available for conversion (in case the table has columns with fixed or floating point numbers)<br></br>
    /// [PT-BR]: Este item será usado para checar se o tipo retornado está disponivel para conversão (para casos que a tabela possuir colunas com numeros fixos ou com pontos flutuantes)
    /// </param>
    /// <returns> 
    /// [EN]: Returns the table data in the informed object format<br></br>
    /// [PT-BR]: Retorna os dados da tabela no formado do objeto informado 
    /// </returns>
    /// <remarks>
    /// [EN]: Does not guarantee conversion of dates to DateTime format, only to string format<br></br>
    /// [PT-BR]: Não garante a conversão de datas para formato DateTime, apenas para formato de cadeia de caracteres
    /// </remarks>
    public static T DeserializeTable<T>(this DataTable table, List<Type> numTypes = null) {
      var json = new StringBuilder();

      if(numTypes == null)
        numTypes = new List<Type> { typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(int), typeof(long), typeof(byte) };

      if(table == null)
        return default;

      if(table.Rows.Count <= 0)
        return default;

      json.Append('['); // START JSON LIST

      int countRow = 0;
      foreach(DataRow row in table.Rows) {
        countRow++;

        json.Append('{'); // start json object

        int countCol = 0;
        foreach(DataColumn col in table.Columns) {
          countCol++;

          // TO END THE JSON OBJECT
          if(countCol == table.Columns.Count) {
            // ONLY NUMBERS OR DATES
            if(numTypes.Contains(row[col].GetType())) {
              json.Append($"\"{col.ColumnName}\" : {row[col].ToString().Replace(',', '.')}"); // close json line last item (NUMBER ONLY)

            } else if(row[col].ToString().IsAnyDate()) {
              json.Append($"\"{col.ColumnName}\" : \"{row[col].ConvertToDate()}\""); // close json line last item (DATETIME ONLY)

            } else {
              
              json.Append($"\"{col.ColumnName}\" : \"{row[col]}\""); // close json object last item

            }

            continue; // prevents json from getting unformatted and skips to next item
          }

          // FOR ITEMS THAT ARE ON THE LIST WAITING TO BE INSERTED
          if(numTypes.Contains(row[col].GetType())) {
            json.Append($"\"{col.ColumnName}\" : {row[col].ToString().Replace(',', '.')},"); // terminate json line with multiple items (NUMBER ONLY)

          } else if(row[col].ToString().IsAnyDate()) {
            json.Append($"\"{col.ColumnName}\" : \"{row[col].ConvertToDate()}\","); // terminate json line with multiple items (DATETIME ONLY)

          } else {
            json.Append($"\"{col.ColumnName}\" : \"{row[col]}\","); // terminate json line with multiple items

          }

        }

        if(countRow == table.Rows.Count)
          json.Append('}'); // close json object last item

        else
          json.Append("},"); // terminate json object with multiple items

      }

      json.Append(']'); // END JSON LIST

      string jsonConvert = json.ToString(); // redundant - for test only
      return JsonSerializer.Deserialize<T>(jsonConvert);
    }

    #region OVERLOAD PRINT

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
    /// <param name="arg">
    /// [EN]: Object to be written to the console along with the message <br></br>
    /// [PT-BR]: Objeto a ser escrito no console juntamente com a mensagem
    /// </param>

    public static void Print(this string message, object? arg) => Console.WriteLine(message, arg);

    /// <summary>
    /// [EN]: Console.WriteLine() simplification / Write to console <br></br>
    /// [PT-BR]: Simplificação do Console.WriteLine() / Escreve no console
    /// </summary>
    /// <param name="obj">
    /// [EN]: Object to be written to the console<br></br>
    /// [PT-BR]: Objeto a ser escrito no console
    /// </param>
    public static void Print(this object obj) => Console.WriteLine(obj);

    /// <summary>
    /// [EN]: Console.WriteLine() simplification / Write to console <br></br>
    /// [PT-BR]: Simplificação do Console.WriteLine() / Escreve no console
    /// </summary>
    public static void Print() => Console.WriteLine();

    #endregion

    #region OVERLOAD MAXMIN

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

    #region OVERLOAD GETONLY

    /// <summary>
    /// [EN]: Get only the numbers and letters contained in the string<br></br>
    /// [PT-BR]: Obtém apenas os numeros e as letras contidas na cadeia de caracteres
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string containing only numeric values and letters .<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caracteres contendo apenas os valores numericos e as letras.
    /// </returns>
    public static string GetOnlyLetterAndNumber(this string input) => GetOnly(input, "letterAndNumber");

    /// <summary>
    /// [EN]: Get only whitespace from a string<br></br>
    /// [PT-BR]: Obtém apenas os espaços em branco de uma cadeia de caracteres
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string with only whitespace<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caracteres com apenas os espaços em branco
    /// </returns>
    public static string GetOnlyWhiteSpace(this string input) => GetOnly(input, "whitespace");

    /// <summary>
    /// [EN]: Gets only special chars contained in the string.<br></br>
    /// [PT-BR]: Obtém apenas os caracteres especiais contidas na cadeia de caracteres.
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string containing only special chars.<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caracteres contendo apenas os caracteres especiais.
    /// </returns>
    public static string GetOnlySpecialChars(this string input) => GetOnly(input, "special");

    /// <summary>
    /// [EN]: Gets only the numbers contained in the string.<br></br>
    /// [PT-BR]: Obtém apenas os numeros contidos na cadeia de caracteres.
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string containing only numeric values.<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caracteres contendo apenas os valores numericos.
    /// </returns>
    public static string GetOnlyNumbers(this string input) => GetOnly(input, "number");

    /// <summary>
    /// [EN]: Gets only the letters contained in the string.<br></br>
    /// [PT-BR]: Obtém apenas as letras contidas na cadeia de caracteres.
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string containing only letters.<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caracteres contendo apenas as letras.
    /// </returns>
    public static string GetOnlyLetters(this string input) => GetOnly(input, "letter");

    /// <summary>
    /// [EN]: Get only uppercase letters<br></br>
    /// [PT-BR]: Obtém apenas as letras em caixa alta
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string containing only uppercase letters<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caracteres contendo apenas as letras em caixa alta
    /// </returns>
    public static string GetOnlyUpperCase(this string input) => GetOnly(input, "upper");

    /// <summary>
    /// [EN]: Get only lowercase letters<br></br>
    /// [PT-BR]: Obtém apenas as letras em caixa baixa
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string containing only lowercase letters<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caracteres contendo apenas as letras em caixa baixa
    /// </returns>
    public static string GetOnlyLowerCase(this string input) => GetOnly(input, "lower");

    /// <summary>
    /// [EN]: Get only the symbols contained in the string<br></br>
    /// [PT-BR]: Obtém apenas os simbolos contidos na cadeia de caractere
    /// </summary>
    /// <param name="input">
    /// [EN]: Input string for checking.<br></br>
    /// [PT-BR]: Cadeia de caracteres de entrada para checagem.
    /// </param>
    /// <returns>
    /// [EN]: Returns a new string containing only symbols.<br></br>
    /// [PT-BR]: Retorna uma nova cadeia de caractere contendo apenas simbolos.
    /// </returns>
    public static string GetOnlySymbol(this string input) => GetOnly(input, "symbol");

    #endregion

    #region OVERLOAD GETINDEX

    /// <summary>
    /// [EN]: Get the index of an item in an array, if it doesn't exist, it will return -1<br></br>
    /// [PT-BR]: Obtém o indice de um item em uma array, se ele não existir o retorno será -1
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to find<br></br>
    /// [PT-BR]: Tipo do item a ser localizado
    /// </typeparam>
    /// <param name="array">
    /// [EN]: Array containing items to find<br></br>
    /// [PT-BR]: Matriz que contém itens a serem localizados
    /// </param>
    /// <param name="itemToLoc">
    /// [EN]: Object to find in array<br></br>
    /// [PT-BR]: Objeto a ser localizado na matriz
    /// </param>
    /// <returns>
    /// [EN]: Returns the index of the item in the array if it exists or -1 for non-existent.<br></br>
    /// [PT-BR]: Retorna o indice do item na matriz caso ele exista ou -1 para não existente.
    /// </returns>
    public static int GetIndex<T>(this T[] array, T itemToLoc) => OneIndex(array, itemToLoc);

    /// <summary>
    /// [EN]: Get the index of an item in an array, if it doesn't exist, it will return -1<br></br>
    /// [PT-BR]: Obtém o indice de um item em uma array, se ele não existir o retorno será -1
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to find<br></br>
    /// [PT-BR]: Tipo do item a ser localizado
    /// </typeparam>
    /// <param name="list">
    /// [EN]: List containing items to find<br></br>
    /// [PT-BR]: Lista que contém itens a serem localizados
    /// </param>
    /// <param name="itemToLoc">
    /// [EN]: Object to find in list<br></br>
    /// [PT-BR]: Objeto a ser localizado na lista
    /// </param>
    /// <returns>
    /// [EN]: Returns the index of the item in the list if it exists or -1 for non-existent.<br></br>
    /// [PT-BR]: Retorna o indice do item na lista caso ele exista ou -1 para não existente.
    /// </returns>
    public static int GetIndex<T>(this List<T> list, T itemToLoc) => OneIndex(list, itemToLoc);

    /// <summary>
    /// [EN]: Get the index of an item in an enumerable, if it doesn't exist, it will return -1<br></br>
    /// [PT-BR]: Obtém o indice de um item em um enumerador, se ele não existir o retorno será -1
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to find<br></br>
    /// [PT-BR]: Tipo do item a ser localizado
    /// </typeparam>
    /// <param name="enumerable">
    /// [EN]: Enumerable containing items to find<br></br>
    /// [PT-BR]: Enumerador que contém itens a serem localizados
    /// </param>
    /// <param name="itemToLoc">
    /// [EN]: Object to find in list<br></br>
    /// [PT-BR]: Objeto a ser localizado na lista
    /// </param>
    /// <returns>
    /// [EN]: Returns the index of the item in the enumerador if it exists or -1 for non-existent.<br></br>
    /// [PT-BR]: Retorna o indice do item no enumerador caso ele exista ou -1 para não existente.
    /// </returns>
    public static int GetIndex<T>(this IEnumerable<T> enumerable, T itemToLoc) => OneIndex(enumerable, itemToLoc);

    #endregion

    #region OVERLOAD GETSAFEVALUE

    /// <summary>
    /// [EN]: Attempts to cast the object to the specified type. Always returns a safe value.<br></br>
    /// [PT-BR]: Tenta fazer uma conversão do objeto para o tipo especificado. Sempre retorna um valor seguro.
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of file to return<br></br>
    /// [PT-BR]: Tipo do arquivo a ser retornado
    /// </typeparam>
    /// <param name="value">
    /// [EN]: Value to be converted<br></br>
    /// [PT-BR]: Valor a ser convertido
    /// </param>
    /// <returns>
    /// [EN]: Returns a safe value of the given object's conversion<br></br>
    /// [PT-BR]: Retorna um valor seguro da conversão do objeto informado
    /// </returns>
    public static T GetSafeValue<T>(this T value) {
      try { return (T)Convert.ChangeType(value, typeof(T)); } catch(Exception) { return default; }
    }

    /// <summary>
    /// [EN]: Attempts to cast the object to the specified type. Always returns a safe value.<br></br>
    /// [PT-BR]: Tenta fazer uma conversão do objeto para o tipo especificado. Sempre retorna um valor seguro.
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of file to return<br></br>
    /// [PT-BR]: Tipo do arquivo a ser retornado
    /// </typeparam>
    /// <param name="value">
    /// [EN]: Value to be converted<br></br>
    /// [PT-BR]: Valor a ser convertido
    /// </param>
    /// <returns>
    /// [EN]: Returns a safe value of the given object's conversion<br></br>
    /// [PT-BR]: Retorna um valor seguro da conversão do objeto informado
    /// </returns>
    public static T GetSafeValue<T>(this object value) {
      try { return (T)Convert.ChangeType(value, typeof(T)); } catch(Exception) { return default; }
    }

    #endregion

    #region OVERLOAD GETNEGATIVES

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

    #region OVERLOAD GETPOSITIVES

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

    #region OVERLOAD GETODDNUMBERS

    /// <summary>
    /// [EN]: Gets an Array containing only odd values<br></br>
    /// [PT-BR]: Obtém um Array contendo apenas valores impares
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to be analyzed<br></br>
    /// [PT-BR]: Tipo do item a ser analisado
    /// </typeparam>
    /// <param name="input">
    /// [EN]: Input of data to be analyzed<br></br>
    /// [PT-BR]: Entrada dos dados a serem analisados
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array containing only the odd values<br></br>
    /// [PT-BR]: Retorna uma nova matriz contendo somente os valores impares
    /// </returns>
    public static T[] GetOddNumbers<T>(T[] input) => OddNumbers(input).ToArray();

    /// <summary>
    /// [EN]: Gets an list containing only odd values<br></br>
    /// [PT-BR]: Obtém uma lista contendo apenas valores impares
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to be analyzed<br></br>
    /// [PT-BR]: Tipo do item a ser analisado
    /// </typeparam>
    /// <param name="input">
    /// [EN]: Input of data to be analyzed<br></br>
    /// [PT-BR]: Entrada dos dados a serem analisados
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list containing only the odd values<br></br>
    /// [PT-BR]: Retorna uma nova lista contendo somente os valores impares
    /// </returns>
    public static List<T> GetOddNumbers<T>(List<T> input) => OddNumbers(input).ToList();

    /// <summary>
    /// [EN]: Gets an enumerable containing only odd values<br></br>
    /// [PT-BR]: Obtém um enumerador contendo apenas valores impares
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to be analyzed<br></br>
    /// [PT-BR]: Tipo do item a ser analisado
    /// </typeparam>
    /// <param name="input">
    /// [EN]: Input of data to be analyzed<br></br>
    /// [PT-BR]: Entrada dos dados a serem analisados
    /// </param>
    /// <returns>
    /// [EN]: Returns a new enumerable containing only the odd values<br></br>
    /// [PT-BR]: Retorna um novo enumerador contendo somente os valores impares
    /// </returns>
    public static IEnumerable<T> GetOddNumbers<T>(IEnumerable<T> input) => OddNumbers(input);

    #endregion

    #region OVERLOAD GETRANDOMVALUE

    /// <summary>
    /// [EN]: Get a random value contained in an list<br></br>
    /// [PT-BR]: Obtém um valor randomico contido em uma lista
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Object type to return<br></br>
    /// [PT-BR]: Tipo de objeto a ser retornado
    /// </typeparam>
    /// <param name="list">
    /// [EN]: List that contains the values<br></br>
    /// [PT-BR]: Lista que contém os valores
    /// </param>
    /// <returns>
    /// [EN]: Returns a single item from the randomly chosen list.<br></br>
    /// [PT-BR]: Retorna um unico item da lista escolhido de forma randomica.
    /// </returns>
    public static T GetRandomValue<T>(this List<T> list) => GetOneValue(list);

    /// <summary>
    /// [EN]: Get a random value contained in an array<br></br>
    /// [PT-BR]: Obtém um valor randomico contido em uma matriz
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Object type to return<br></br>
    /// [PT-BR]: Tipo de objeto a ser retornado
    /// </typeparam>
    /// <param name="array">
    /// [EN]: Array that contains the values<br></br>
    /// [PT-BR]: Matriz que contém os valores
    /// </param>
    /// <returns>
    /// [EN]: Returns a single item from the randomly chosen array.<br></br>
    /// [PT-BR]: Retorna um unico item da matriz escolhido de forma randomica.
    /// </returns>
    public static T GetRandomValue<T>(this T[] array) => GetOneValue(array);

    /// <summary>
    /// [EN]: Get a random value contained in an enumerable<br></br>
    /// [PT-BR]: Obtém um valor randomico contido em um enumerador
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Object type to return<br></br>
    /// [PT-BR]: Tipo de objeto a ser retornado
    /// </typeparam>
    /// <param name="enumerable">
    /// [EN]: Enumerable that contains the values<br></br>
    /// [PT-BR]: Enumerador que contém os valores
    /// </param>
    /// <returns>
    /// [EN]: Returns a single item from the randomly chosen enumerable.<br></br>
    /// [PT-BR]: Retorna um unico item do enumerador escolhido de forma randomica.
    /// </returns>
    public static T GetRandomValue<T>(this IEnumerable<T> enumerable) => GetOneValue(enumerable);

    #endregion

    #region OVERLOAD GETEVENNUMBERS

    /// <summary>
    /// [EN]: Gets an Array containing only even values<br></br>
    /// [PT-BR]: Obtém um Array contendo apenas valores pares
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to be analyzed<br></br>
    /// [PT-BR]: Tipo do item a ser analisado
    /// </typeparam>
    /// <param name="input">
    /// [EN]: Input of data to be analyzed<br></br>
    /// [PT-BR]: Entrada dos dados a serem analisados
    /// </param>
    /// <returns>
    /// [EN]: Returns a new array containing only the even values<br></br>
    /// [PT-BR]: Retorna uma nova matriz contendo somente os valores pares
    /// </returns>
    public static T[] GetEvenNumbers<T>(T[] input) => EvenNumbers(input).ToArray();

    /// <summary>
    /// [EN]: Gets an list containing only even values<br></br>
    /// [PT-BR]: Obtém uma lista contendo apenas valores pares
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to be analyzed<br></br>
    /// [PT-BR]: Tipo do item a ser analisado
    /// </typeparam>
    /// <param name="input">
    /// [EN]: Input of data to be analyzed<br></br>
    /// [PT-BR]: Entrada dos dados a serem analisados
    /// </param>
    /// <returns>
    /// [EN]: Returns a new list containing only the even values<br></br>
    /// [PT-BR]: Retorna uma nova lista contendo somente os valores pares
    /// </returns>
    public static List<T> GetEvenNumbers<T>(List<T> input) => EvenNumbers(input).ToList();

    /// <summary>
    /// [EN]: Gets an enumerable containing only even values<br></br>
    /// [PT-BR]: Obtém um enumerador contendo apenas valores pares
    /// </summary>
    /// <typeparam name="T">
    /// [EN]: Type of item to be analyzed<br></br>
    /// [PT-BR]: Tipo do item a ser analisado
    /// </typeparam>
    /// <param name="input">
    /// [EN]: Input of data to be analyzed<br></br>
    /// [PT-BR]: Entrada dos dados a serem analisados
    /// </param>
    /// <returns>
    /// [EN]: Returns a new enumerable containing only the even values<br></br>
    /// [PT-BR]: Retorna um novo enumerador contendo somente os valores pares
    /// </returns>
    public static IEnumerable<T> GetEvenNumbers<T>(IEnumerable<T> input) => EvenNumbers(input);

    #endregion

    #endregion

  }
}
