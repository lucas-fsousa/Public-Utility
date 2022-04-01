using App.Utils.CustomExceptions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Situation = App.Utils.CustomExceptions.Base.BaseException.Situations;

namespace App.Utils {

  /// <summary>
  /// [EN]: Security focused class containing encryption and decryption methods <br></br>
  /// [PT-BR]: Classe direcionada a segurança contendo métodos de criptografia e descriptografia
  /// </summary>
  public static class XSecurity {

    /// <summary>
    /// [EN]: Encrypt a string with MD5 algorithm that cannot be reversed <br></br>
    /// [PT-BR]: Criptografa uma string com algoritmo MD5 que não pode ser revertido
    /// </summary>
    /// <param name="str">
    /// [EN]: Value to be encrypted <br></br>
    /// [PT-BR]: Valor a ser criptografado
    /// </param>
    /// <returns>
    /// [EN]: Returns an encrypted string <br></br>
    /// [PT-BR]: Retorna uma cadeia de caracteres criptografada
    /// </returns>
    public static string GetHashMD5(string str) {
      MD5 md5Hash = MD5.Create();
      byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

      StringBuilder sBuilder = new StringBuilder();

      for(int i = 0; i < data.Length; i++) {
        sBuilder.Append(data[i].ToString("x2"));
      }

      return sBuilder.ToString();
    }

    /// <summary>
    /// [EN]: Encrypt a string with Pbkdf2 algorithm that cannot be reversed <br></br>
    /// [PT-BR]: Criptografa uma string com algoritmo Pbkdf2 que não pode ser revertido
    /// </summary>
    /// <param name="str">
    /// [EN]: Value to be encrypted <br></br>
    /// [PT-BR]: Valor a ser criptografado
    /// </param>
    /// <returns>
    /// [EN]: Returns an encrypted string <br></br>
    /// [PT-BR]: Retorna uma cadeia de caracteres criptografada
    /// </returns>
    public static string GetHashPbkdf2(string str) {
      byte[] salt = new byte[128 / 8];
      byte[] codify = KeyDerivation.Pbkdf2(str, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8);
      return Convert.ToBase64String(codify);
    }

    /// <summary>
    /// [EN]: Get a hash through a unique identifier<br></br>
    /// [PT-BR]: Obtém um hash através de um identificador único
    /// </summary>
    /// <returns>
    /// [EN]: Returns an encrypted string<br></br>
    /// [PT-BR]: Retorna uma cadeia de caracteres criptografada
    /// </returns>
    public static string GetHash() {
      Guid guid = Guid.NewGuid();
      return Convert.ToBase64String(guid.ToByteArray());
    }

    /// <summary>
    /// [EN]: Decrypt a string using the private key used to encrypt it previously <br></br>
    /// [PT-BR]: decrypt a string using the private key used to encrypt it previously
    /// </summary>
    /// <param name="str">
    /// [EN]: Value to be decrypted <br></br>
    /// [PT-BR]: Valor a ser descriptografado
    /// </param>
    /// <param name="privateKeyNumber">
    /// [EN]: Private key with 8 numeric digits <br></br>
    /// [PT-BR]: Chave privada com 8 digitos numéricos
    /// </param>
    /// <returns>
    /// [EN]: Returns an decrypted string <br></br>
    /// [PT-BR]: Retorna uma cadeia de caracteres descriptografada
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static string Decrypt(string str, string privateKeyNumber) {
      string response = string.Empty;
      string publicKey = string.Empty;

      Dictionary<string, Situation> result = CheckCryptInput(str, privateKeyNumber);

      /* a null value is expected for CheckCryptInput with no adverse situation.
        If the value is filled in, it is understood that it was filled in incorrectly.*/
      if(result != null)
        throw new RequiredParamsException(result.Values.First(), result.Keys.First());

      privateKeyNumber.Reverse().ToList().ForEach(x => publicKey += x);

      try {
        byte[] privatekeyByte = Encoding.UTF8.GetBytes(privateKeyNumber);
        byte[] publickeybyte = Encoding.UTF8.GetBytes(publicKey);
        byte[] byteArray = new byte[str.Replace(" ", "+").Length];

        byteArray = Convert.FromBase64String(str.Replace(" ", "+"));
        using(DESCryptoServiceProvider des = new DESCryptoServiceProvider()) {
          using(MemoryStream ms = new MemoryStream()) {
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
            cs.Write(byteArray, 0, byteArray.Length);
            cs.FlushFinalBlock();
            response = Encoding.UTF8.GetString(ms.ToArray());
          }

        }

      } catch(Exception) { }
      return response;
    }

    /// <summary>
    /// [EN]: Encrypts a string using a private key that can be used to reverse the encryption <br></br>
    /// [PT-BR]: Criptografa uma cadeia de caracteres através de uma chave privada que pode ser utilizada para reverter a criptografia
    /// </summary>
    /// <param name="str">
    /// [EN]: Value to be encrypted <br></br>
    /// [PT-BR]: Valor a ser criptografado
    /// </param>
    /// <param name="privateKeyNumber">
    /// [EN]: Private key with 8 numeric digits <br></br>
    /// [PT-BR]: Chave privada com 8 digitos numéricos
    /// </param>
    /// <returns>
    /// [EN]: Returns an encrypted string <br></br>
    /// [PT-BR]: Retorna uma cadeia de caracteres criptografada
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static string Encrypt(string str, string privateKeyNumber) {
      string response = string.Empty;
      string publicKey = string.Empty;

      Dictionary<string, Situation> result = CheckCryptInput(str, privateKeyNumber);

      /* a null value is expected for CheckCryptInput with no adverse situation.
        If the value is filled in, it is understood that it was filled in incorrectly.*/
      if(result != null)
        throw new RequiredParamsException(result.Values.First(), result.Keys.First());

      privateKeyNumber.Reverse().ToList().ForEach(x => publicKey += x);

      try {
        byte[] privatekeyByte = Encoding.UTF8.GetBytes(privateKeyNumber);
        byte[] publickeybyte = Encoding.UTF8.GetBytes(publicKey);
        byte[] byteArray = Encoding.UTF8.GetBytes(str);

        using(DESCryptoServiceProvider des = new DESCryptoServiceProvider()) {
          using(MemoryStream ms = new MemoryStream()) {
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
            cs.Write(byteArray, 0, byteArray.Length);
            cs.FlushFinalBlock();
            response = Convert.ToBase64String(ms.ToArray());
          }
        }

      } catch(Exception) { }
      return response;
    }

    private static Dictionary<string, Situation> CheckCryptInput(string input, string privateKeyNumber) {
      Dictionary<string, Situation> result = new Dictionary<string, Situation>();

      if(string.IsNullOrEmpty(input))
        result.Add(nameof(input), Situation.IsNullOrEmpty);

      else if(string.IsNullOrEmpty(privateKeyNumber))
        result.Add(nameof(privateKeyNumber), Situation.IsNullOrEmpty);

      else if(!privateKeyNumber.IsNumber())
        result.Add(nameof(privateKeyNumber), Situation.NotANumber);

      else if(privateKeyNumber.Length > 8)
        result.Add(nameof(privateKeyNumber), Situation.AboveTheAllowed);

      else if(privateKeyNumber.Length < 8)
        result.Add(nameof(privateKeyNumber), Situation.BelowTheNecessary);

      else
        result = null;

      return result;
    }
  }

}
