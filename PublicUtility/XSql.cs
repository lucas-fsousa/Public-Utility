using System;
using System.Data;
using System.Data.SqlClient;
using PublicUtility.CustomExceptions;
using static PublicUtility.CustomExceptions.Base.BaseException;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Helper class to work with data via SqlServer <br></br>
  /// [PT-BR]: Classe auxiliar para trabalhar com dados via SqlServer
  /// </summary>
  public class XSql {
    private readonly SqlConnection con = null;
    private readonly SqlCommand cmd = null;
    private SqlTransaction tran = null;

    /// <summary>
    /// [EN]: Constructor method with required parameters<br></br>
    /// [PT-BR]: Método construtor com parametros obrigatórios
    /// </summary>
    /// <param name="connectionString">
    /// [EN]: String referring to database connection<br></br>
    /// [PT-BR]: Cadeia de caracteres referente a conexão com o banco de dados
    /// </param>
    /// <param name="sqlCommand">
    /// [EN]: Transaction object containing the basic information of the activity.<br></br>
    /// [PT-BR]: Objeto de transação contendo as informações básicas da atividade.
    /// </param>
    /// <exception cref="RequiredParamsException"></exception>
    public XSql(string connectionString, SqlCommand sqlCommand) {
      string invalidParamName = IsValid(connectionString, sqlCommand);
      if(!string.IsNullOrEmpty(invalidParamName)) {
        throw new RequiredParamsException(Situations.IsNullOrEmpty, invalidParamName);
      }

      this.con = new SqlConnection(connectionString);
      this.cmd = sqlCommand;
    }

    /// <summary>
    /// [EN]: Go back to changes made to the bank and cancel all updates.<br></br>
    /// [PT-BR]: Volta atrás das modificações realizadas no banco e cancela todas as atualizações.
    /// </summary>
    private void RollBack() {
      if(this.cmd.Transaction != null) {
        this.cmd.Transaction.Rollback();
      }
    }

    /// <summary>
    /// [EN]: Commits an open transaction.<br></br>
    /// [PT-BR]: Faz o commit de uma transação em aberto.
    /// </summary>
    private void Commit() {
      if(this.cmd.Transaction != null) {
        this.cmd.Transaction.Commit();
      }
    }

    /// <summary>
    /// [EN]: Open SQL connection <br></br>
    /// [PT-BR]: Abre a conexão SQL
    /// </summary>
    /// <returns></returns>
    private SqlConnection Open() {
      if(con.State == ConnectionState.Closed) {
        con.Open();
        tran = con.BeginTransaction();
      }
      return con;
    }

    /// <summary>
    /// [EN]: Close the SQL connection <br></br>
    /// [PT-BR]: Fecha a conexão SQL
    /// </summary>
    private void Close() {
      if(con.State == ConnectionState.Open)
        con.Close();
    }

    /// <summary>
    /// [EN]: Single instance constructor with required parameters <br></br>
    /// [PT-BR]: Construtor de instância unico com parametros obrigatórios 
    /// </summary>
    /// <param name="connectionString">
    /// [EN]: String for connecting to the database <br></br>
    /// [PT-BR]: Cadeia de caracteres para conexão com a base de dados
    /// </param>
    /// <param name="sqlCommand">
    /// [EN]: SQL command helper that contains information necessary to perform activities in the database.<br></br>
    /// [PT-BR]: Auxiliar de comando sql que contém informações necessárias para realizar as atividades na base de dados.
    /// </param>
    /// <exception cref="RequiredParamsException"></exception>
    private static string IsValid(string connectionString, SqlCommand sqlCommand) {
      string notValidName = string.Empty;

      if(sqlCommand == null)
        notValidName = string.Format("SqlCommand");

      else if(string.IsNullOrEmpty(sqlCommand.CommandText))
        notValidName = string.Format("SqlCommand.CommandText");

      else if(string.IsNullOrEmpty(connectionString))
        notValidName = string.Format("connectionString");

      return notValidName;
    }

    /// <summary>
    /// [EN]: Execute an SQL command without returning data<br></br>
    /// [PT-BR]: Executa um comando SQL sem retorno de dados
    /// </summary>
    /// <param name="execMessage">
    /// [EN]: Variable that will receive the return message from the execution <br></br>
    /// [PT-BR]: Variavel que receberá a mensagem de retorno da execução
    /// </param>
    /// <remarks>
    /// <code>
    /// [EX]:
    ///   SqlCommand sqlCommand= new SqlCommand();
    ///   string errorMessage;
    ///
    ///   sqlCommand.CommandText = "insert into yourTable values (@dateTimeNow)";
    ///   sqlCommand.Parameters.AddWithValue("@dateTimeNow", DateTime.Now);
    ///
    ///   XSql xSql = new XSql(@"your connection string here", sqlCommand);
    ///   xSql.GoExec(out errorMessage);
    /// </code>
    /// </remarks>
    public void GoExec(out string execMessage) {
      try {
        using(con) {
          this.cmd.Connection = this.Open();
          this.cmd.Transaction = tran;
          this.cmd.ExecuteNonQuery();
          this.Commit();
        }
        execMessage = string.Format($"## SUCCESS ## {DateTime.Now} ## OK ##");
      } catch(Exception ex) {
        execMessage = string.Format($"## ERRO ## {DateTime.Now} ## {ex.Message} ##");
        this.RollBack();

      } finally {
        this.Close();
      }
    }

    /// <summary>
    /// [EN]: Executes a select sql query<br></br>
    /// [PT-BR]: Executa uma atividade de consulta sql
    /// </summary>
    /// <param name="execMessage">
    /// [EN]: Variable that will receive the return message from the execution <br></br>
    /// [PT-BR]: Variavel que receberá a mensagem de retorno da execução
    /// </param>
    /// <remarks>
    /// [EX]: 
    /// <code>
    ///   SqlCommand sqlCommand= new SqlCommand();
    ///   string errorMessage;
    ///
    ///   sqlCommand.CommandText = "select * from yourTable";
    ///
    ///   XSql xSql = new XSql(@"your connection string here", sqlCommand);
    ///   DataTable dataTable = xSql.ReturnData(out errorMessage);
    /// </code>
    /// </remarks>
    /// <returns>
    /// [EN]: Returns an object of type DataTable  <br></br>
    /// [PT-BR]: Retorna um objeto do tipo DataTable
    /// </returns>
    public DataTable ReturnData(out string execMessage) {
      DataTable table = new DataTable();
      try {
        SqlDataAdapter adapter = new SqlDataAdapter();
        using(con) {
          this.cmd.Connection = this.Open();
          this.cmd.Transaction = tran;

          adapter.SelectCommand = this.cmd;
          adapter.Fill(table);

          this.Commit();
        }

        execMessage = string.Format($"## SUCCESS ## {DateTime.Now} ## OK ##");
      } catch(Exception ex) {
        table = null;
        execMessage = string.Format($"## ERRO ## {DateTime.Now} ## {ex.Message} ##");
        this.RollBack();

      } finally {
        this.Close();

      }
      return table;
    }

  }

}
