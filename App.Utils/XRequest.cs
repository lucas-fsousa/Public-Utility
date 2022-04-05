using App.Utils.CustomExceptions;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using static App.Utils.CustomExceptions.Base.BaseException;

namespace App.Utils {
  public static class XRequest {

    /// <summary>
    /// [EN]: Performs the action of Delete through the HTTP protocol<br></br>
    /// [PT-BR]: Realiza a ação de Delete através do protocolo HTTP
    /// </summary>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> HttpDelete(Uri url) {
      if(url == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.DeleteAsync(url);
        return message;
      }
    }

    /// <summary>
    /// [EN]: Performs a post action via the HTTP protocol and can undo a JSON object<br></br>
    /// [PT-BR]: Executa uma ação de postagem por meio do protocolo HTTP e pode desfazer um objeto JSON
    /// </summary>
    /// <typeparam name="Type">
    /// [EN]: Object type to json serealize <br></br>
    /// [PT-BR]: Tipo de objeto para serealize json
    /// </typeparam>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <param name="value">
    /// [EN]: Content to be sent to the server<br></br>
    /// [PT-BR]: Conteudo a ser enviado para o servidor
    /// </param>
    /// <param name="jsonOptions">
    /// [EN]: Optional settings for json handling<br></br>
    /// [PT-BR]: Configurações opecionais para o tratamento json
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> HttpPost<Type>(Uri url, Type value, JsonSerializerOptions jsonOptions = null) {
      if(url == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.PostAsJsonAsync(url, value, jsonOptions);
        return message;
      }
    }

    /// <summary>
    /// [EN]: Performs a post action via the HTTP protocol<br></br>
    /// [PT-BR]: Executa uma ação de postagem por meio do protocolo HTTP
    /// </summary>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <param name="value">
    /// [EN]: Content to be sent to the server<br></br>
    /// [PT-BR]: Conteudo a ser enviado para o servidor
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> HttpPost(Uri url, HttpContent value) {
      if(url == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      if(value == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(value));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.PostAsync(url, value);
        return message;
      }
    }

    /// <summary>
    /// [EN]: Performs a put action via the HTTP protocol and can undo a JSON object<br></br>
    /// [PT-BR]: Executa uma ação de put/atualização simples por meio do protocolo HTTP e pode desfazer um objeto JSON
    /// </summary>
    /// <typeparam name="Type">
    /// [EN]: Object type to json serealize <br></br>
    /// [PT-BR]: Tipo de objeto para serealize json
    /// </typeparam>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <param name="value">
    /// [EN]: Content to be sent to the server<br></br>
    /// [PT-BR]: Conteudo a ser enviado para o servidor
    /// </param>
    /// <param name="jsonOptions">
    /// [EN]: Optional settings for json handling<br></br>
    /// [PT-BR]: Configurações opecionais para o tratamento json
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> HttpPut<Type>(string url, Type value, JsonSerializerOptions jsonOptions = null) {
      if(string.IsNullOrEmpty(url))
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.PutAsJsonAsync(url, value, jsonOptions);
        return message;
      }
    }

    /// <summary>
    /// [EN]: Performs a put action via the HTTP protocol<br></br>
    /// [PT-BR]: Executa uma ação de put/atualização simples por meio do protocolo HTTP
    /// </summary>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <param name="value">
    /// [EN]: Content to be sent to the server<br></br>
    /// [PT-BR]: Conteudo a ser enviado para o servidor
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> HttpPut(Uri url, HttpContent value) {
      if(url == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      if(value == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(value));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.PutAsync(url, value);
        return message;
      }
    }

    /// <summary>
    /// [EN]: Make an HTTP request to get information from the server<br></br>
    /// [PT-BR]: Faz uma requisição HTTP para pegar informações do servidor
    /// </summary>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> HttpGet(Uri url) {
      if(url == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.GetAsync(url);
        return message;
      }
    }

    /// <summary>
    /// [EN]: Make an HTTP request to get information from the server<br></br>
    /// [PT-BR]: Faz uma requisição HTTP para pegar informações do servidor
    /// </summary>
    /// <typeparam name="Type">
    /// [EN]: Object type to json serealize <br></br>
    /// [PT-BR]: Tipo de objeto para serealize json
    /// </typeparam>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <param name="jsonOptions">
    /// [EN]: Optional settings for json handling<br></br>
    /// [PT-BR]: Configurações opecionais para o tratamento json
    /// </param>
    /// <returns>
    /// [EN]: Returns a json undone and converted to the object informed during the action call<br></br>
    /// [PT-BR]: Retorna um json desfeito e convertido para o objeto informado durante a chamada da ação
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<Type> HttpGet<Type>(Uri url, JsonSerializerOptions jsonOptions = null) {
      if(url == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      using(HttpClient http = new HttpClient()) {
        Type objectDeserealized = await http.GetFromJsonAsync<Type>(url, jsonOptions);
        return objectDeserealized;
      }
    }

    /// <summary>
    /// [EN]: Makes a complete server update by passing an object that can be serealize<br></br>
    /// [PT-BR]: Faz uma atualização no servidor de forma completa repassando um objeto que pode ser serealizado
    /// </summary>
    /// <typeparam name="Type">
    /// [EN]: Object type to json serealize <br></br>
    /// [PT-BR]: Tipo de objeto para serealize json
    /// </typeparam>
    /// <param name="url">
    /// [EN]: URL - Endpoint para e entrega a requisição HTTP<br></br>
    /// [PT-BR]: URL - Endpoint para e efetuar a requisição HTTP
    /// </param>
    /// <param name="value">
    /// [EN]: Content to be sent to the server<br></br>
    /// [PT-BR]: Conteudo a ser enviado para o servidor
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> HttpPatchAsync<Type>(Uri url, HttpContent value) {
      if(url == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(url));

      if(value == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(value));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.PatchAsync(url, value);
        return message;
      }
    }

    /// <summary>
    /// [EN]: Performs a fully customized http request by the user.<br></br>
    /// [PT-BR]: Realiza uma requisição http totalmente personalizada pelo usuário.
    /// </summary>
    /// <param name="request">
    /// [EN]: Necessary settings to make the custom request.<br></br>
    /// [PT-BR]: Configurações necessárias para efetuar a requisição personalizada.
    /// </param>
    /// <returns>
    /// [EN]: Retorna um HttpResponseMessage contendo informações da requisição<br></br>
    /// [PT-BR]: Retorna um HttpResponseMessage contendo informações da requisição
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static async Task<HttpResponseMessage> Custom(HttpRequestMessage request) {
      if(request == null)
        throw new RequiredParamsException(Situations.IsNullOrEmpty, nameof(request));

      using(HttpClient http = new HttpClient()) {
        HttpResponseMessage message = await http.SendAsync(request);
        return message;
      }

    }

  }
}
