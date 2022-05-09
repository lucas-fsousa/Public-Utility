using PublicUtility.CustomExceptions;
using PublicUtility.Xnm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Class that assists in notifications through emails<br></br>
  /// [PT-BR]: Classe que auxilia nas notificações através de emails
  /// </summary>
  public class XEmail {

    /// <summary>
    /// [EN]: Character that will separate the email string when there is more than one item to be notified. The default is ';'. Ex: "test@gmail.com;test2@gmail.com" will be converted to "test@gmail.com" "test2@gmail.com"<br></br>
    /// [PT-BR]: Caractere que separará a string do email quando houver mais de um item a ser notificado. O padrão é ';'. Ex: "test@gmail.com;test2@gmail.com" será convertido para "test@gmail.com" "test2@gmail.com"
    /// </summary>
    public readonly char MailsSeparator = ';';
    public readonly string CredentialEmail;
    public readonly string CredentialPassword;
    public readonly string PresentationName;

    public string To { get; set; }
    public string Copy { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    public MailPriority Priority { get; set; }
    public List<Attachment> Attachment { get; set; }

    /// <summary>
    /// [EN]: Constructor method with mandatory filling of the credentials of the e-mail that will be used to originate the notifications.<br></br>
    /// [PT-BR]: Método construtor com preenchimento obrigatório das credenciais do email que será utilizado para originar as notificações.
    /// </summary>
    /// <param name="credentialPassword">
    /// [EN]: email password<br></br>
    /// [PT-BR]: senha de acesso do e-mail
    /// </param>
    /// <param name="credentialEmail">
    /// [EN]: email address ex: mycorpemail@mycorp.com <br></br>
    /// [PT-BR]: Endereco do e-mail ex: mycorpemail@mycorp.com
    /// </param>
    /// <param name="presentationName">
    /// [EN]: E-mail display name.<br></br>
    /// [PT-BR]: Nome de apresentação do e-mail
    /// </param>
    /// <exception cref="RequiredParamsException"></exception>
    public XEmail(string credentialPassword, string credentialEmail, string presentationName) {
      this.CredentialPassword = credentialPassword;
      this.CredentialEmail = credentialEmail;
      this.PresentationName = presentationName;
      this.Priority = MailPriority.Normal;

      if(!IsValid(credentialEmail)) {
        throw new RequiredParamsException(Situation.InvalidFormat, "credentialEmail");

      } else if(string.IsNullOrEmpty(credentialPassword)) {
        throw new RequiredParamsException(Situation.IsNullOrEmpty, "credentialPassword");

      } else if(string.IsNullOrEmpty(presentationName)) {
        throw new RequiredParamsException(Situation.IsNullOrEmpty, "PresentationName");

      }
    }

    /// <summary>
    /// [EN]: Send an email to one or more addresses<br></br>
    /// [PT-BR]: Envia um email para um ou varios endereços
    /// </summary>
    /// <param name="message">
    /// [EN]: operation return message<br></br>
    /// [PT-BR]: Mensagem de retorno da operação
    /// </param>
    /// <returns>
    /// [EN]: Will return a boolean informing if it was successful.<br></br>
    /// [PT-BR]: Poderá retornar booleano informando se houve sucesso no envio do email</returns>
    /// <remarks>
    /// <code>
    ///   All.XEmail email = new All.XEmail("mypassword12345", "autoreply@gmail.com", "Notification");
    ///   email.To = "destinationEmail@gmail.com";
    ///   email.Body = "<p><strong>One Body :D</strong></p>";
    ///   email.Priority = MailPriority.High;
    ///   email.Subject = "Hello";
    ///
    ///   string message;
    ///   bool response = email.SendMail(out message);
    /// </code>
    /// </remarks>
    /// <exception cref="RequiredParamsException"></exception>
    public bool SendMail(out string message) {
      MailMessage mail = new MailMessage();
      SmtpClient client = new SmtpClient();

      // CONFIG TO SEND
      mail.Sender = new MailAddress(CredentialEmail, PresentationName);
      mail.From = new MailAddress(CredentialEmail, PresentationName);

      // CONFIG TO RECEPT
      this.To = this.To.RemoveWhiteSpaces();

      if(string.IsNullOrEmpty(this.To))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, "Destination Emails");

      foreach(string email in this.To.Split(MailsSeparator)) {
        if(string.IsNullOrEmpty(email))
          continue;

        if(!IsValid(email))
          throw new RequiredParamsException(Situation.InvalidFormat, "Destination Email");

        mail.To.Add(new MailAddress(email));
      }

      if(this.Copy != null) {
        this.Copy = this.Copy.RemoveWhiteSpaces();

        if(string.IsNullOrEmpty(this.Copy))
          throw new RequiredParamsException(Situation.IsNullOrEmpty, "Destination Copy Email");

        foreach(string email in this.Copy.Split(MailsSeparator)) {
          if(string.IsNullOrEmpty(email))
            continue;

          if(!IsValid(email))
            throw new RequiredParamsException(Situation.InvalidFormat, "Destination Copy Email");

          mail.CC.Add(new MailAddress(email));
        }
      }
      //END CONFIG TO RECEPT

      // SERVER CONFIG
      client.Host = "smtp.office365.com";
      client.EnableSsl = true;
      client.Credentials = new NetworkCredential(CredentialEmail, CredentialPassword);

      // CONTENT CONFIG
      if(string.IsNullOrEmpty(this.Body))
        this.Body = string.Format("<p>Oops <strong>no body</strong> has been defined for the email. This is default content. <strong>:D</strong></p>");

      if(string.IsNullOrEmpty(this.Subject))
        this.Subject = string.Format("Default");

      mail.Body = this.Body;
      mail.Subject = this.Subject;
      mail.Priority = this.Priority;
      mail.IsBodyHtml = true;

      // ATTACHMENTS CONFIG
      if(this.Attachment != null) {
        foreach(Attachment att in this.Attachment)
          mail.Attachments.Add(att);
      }


      try {
        client.Send(mail);
        message = string.Format($"## ACTION EMAIL ## SUCCESS ## {DateTime.Now} ## OK ##");
        mail.Dispose();
        return true;
      } catch(Exception ex) {
        message = string.Format($"## ACTION EMAIL ## FAILED ## {DateTime.Now} ## {ex.Message} ##");
        throw new Exception(message);

      }
    }

    /// <summary>
    /// [EN]: Does a basic string format validation to identify if it's a valid email<br></br>
    /// [PT-BR]: Faz uma validação básica do formato da string para identificar se é um email válido
    /// </summary>
    /// <param name="email">
    /// [EN]: String to be parsed<br></br>
    /// [PT-BR]: Cadeia de caracteres a ser analisada
    /// </param>
    /// <returns>
    /// [EN]: Returns a boolean indicating whether the email address string is valid<br></br>
    /// [PT-BR]: Retorna um booleano indicando se a string do endereço email é válido
    /// </returns>
    private bool IsValid(string email) {

      if(email.Length == 0) {
        return false;
      }

      Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

      if(!rg.IsMatch(email)) {
        return false;
      }

      string preAtSign = email.Split("@")[0];
      string posAtSign = email.Split("@")[1];

      if(preAtSign.Count() < 5 || preAtSign.Count() > 64)
        return false;

      else if(posAtSign.Count() < 7)
        return false;
      return true;
    }

  }

}
