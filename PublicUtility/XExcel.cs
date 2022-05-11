using ClosedXML.Excel;
using PublicUtility.CustomExceptions;
using PublicUtility.Xnm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicUtility {

  /// <summary>
  /// [EN]: Represents a cell of an Excel worksheet<br></br>
  /// [PT-BR]: Representa uma célula de uma planilha Excel
  /// </summary>
  public class XCell {
    /// <summary>
    /// [EN]: Represents the position of the cell in the worksheet<br></br>
    /// [PT-BR]: Representa a posição da célula da na planilha
    /// </summary>
    public string Position { get; set; }
    /// <summary>
    /// [EN]: Represents the value of the cell in the worksheet<br></br>
    /// [PT-BR]: Representa o valor da célula da na planilha
    /// </summary>
    public string Value { get; set; }
  }

  /// <summary>
  /// [EN]: Represents excel table styling<br></br>
  /// [PT-BR]: Representa a estilização da tabela excel
  /// </summary>
  public class XTableStyle {

    /// <summary>
    /// [EN]: Represents the color of the columns<br></br>
    /// [PT-BR]: Representa a cor das colunas
    /// </summary>
    public string ColumnColor { get; set; }

    /// <summary>
    /// [EN]: Represents the color of the first line that will compose a zebra shape<br></br>
    /// [PT-BR]: Representa a cor da primeira linha que fará a composição de um formato zebrado
    /// </summary>
    public string FirstLineColor { get; set; }

    /// <summary>
    /// [EN]: Represents the color of the second line that will compose a zebra shape<br></br>
    /// [PT-BR]: Representa a cor da segunda linha que fará a composição de um formato zebrado
    /// </summary>
    public string SecondLineColor { get; set; }

    public string FontLineColor { get; set; }

    public string FontColumnColor { get; set; }
  }

  /// <summary>
  /// [EN]: Represents an Excel table<br></br>
  /// [PT-BR]: Representa uma tabela Excel
  /// </summary>
  public class XTable {

    /// <summary>
    /// [EN]: Represents excel table styling<br></br>
    /// [PT-BR]: Representa a estilização da tabela excel
    /// </summary>
    public XTableStyle Style { get; set; }

    /// <summary>
    /// [EN]: Number of columns that tables have<br></br>
    /// [PT-BR]: Numero de colunas que as tabelas possuem
    /// </summary>
    public int NumberOfColumns { get; set; }

    /// <summary>
    /// [EN]: Represents excel cells.<br></br>
    /// [PT-BR]: Representa as células do excel.
    /// </summary>
    public List<XCell> Cells { get; set; }
  }

  /// <summary>
  /// [EN]: Represents a excel worksheet<br></br>
  /// [PT-BR]: Representa uma planilha do excel
  /// </summary>
  public class XWorkSheet {

    /// <summary>
    /// [EN]: Excel sheet name<br></br>
    /// [PT-BR]: Nome da planilha do excel
    /// </summary>
    public string WorksheetName { get; set; }
    public string WorkSheetColor { get; set; }

    /// <summary>
    /// [EN]: Represents tables to be inserted into the worksheet<br></br>
    /// [PT-BR]: Representa tabelas a serem inseridas na planilha
    /// </summary>
    public List<XTable> Tables { get; set; }
  }
  /// <summary>
  /// [EN]: Represents an Excel Spreadsheet<br></br>
  /// [PT-BR]: Representa uma Planilha do Excel
  /// </summary>
  public class XExcel {

    /// <summary>
    /// [EN]: Sheets to be added to the .xlsx document<br></br>
    /// [PT-BR]: Planilhas a serem adicionadas no documento .xlsx
    /// </summary>
    public List<XWorkSheet> WorkSheets { get; set; }

    /// <summary>
    /// [EN]: Generates the excel spreadsheet with all the tables informed<br></br>
    /// [PT-BR]: Gera a planilha do excel com todas as tabelas informadas
    /// </summary>
    /// <param name="filepath">
    /// [EN]: Path including the name of the file that will be used to save the file.<br></br>
    /// [PT-BR]: Caminho incluindo o nome do arquivo que será utilizado para salvar o arquivo.
    /// </param>
    public void Generate(string filepath) {
      // Make sure the extension is compatible with the file
      if(!filepath.EndsWith(".xlsx"))
        filepath += ".xlsx";

      using(XLWorkbook workb = new()) {
        foreach(XWorkSheet sheet in WorkSheets) {
          var plan = workb.Worksheets.Add(string.IsNullOrEmpty(sheet.WorksheetName) ? "PLAN1" : sheet.WorksheetName); // Create the Worksheet
          string cellFirstColumn;
          string cellLastColumn;
          int countCells;

          foreach(XTable table in sheet.Tables) {
            cellFirstColumn = table.Cells.FirstOrDefault().Position;
            cellLastColumn = table.Cells.LastOrDefault().Position;
            countCells = 0;

            if(table.Style == null) {
              XTableStyle style = new XTableStyle();
              style.ColumnColor = "#252525";
              style.FirstLineColor = "#959595";
              style.SecondLineColor = "#C0C0C0";
              style.FontColumnColor = "#EE0000";
              style.FontLineColor = "#000";

              table.Style = style;
            }

            // create the columns and format the background color
            while(countCells < table.NumberOfColumns) {
              plan.Cell(table.Cells[countCells].Position).SetValue(table.Cells[countCells].Value);
              plan.Cell(table.Cells[countCells].Position).Style.Fill.BackgroundColor = XLColor.FromHtml(table.Style.ColumnColor);
              plan.Cell(table.Cells[countCells].Position).Style.Font.FontColor = XLColor.FromHtml(table.Style.FontColumnColor);
              countCells++;
            }

            // create the lines and format the background color
            string color = table.Style.FirstLineColor;
            int breakLine = 0;
            while(countCells < table.Cells.Count) {
              if(breakLine == table.NumberOfColumns) {
                color = color == table.Style.FirstLineColor ? table.Style.SecondLineColor : table.Style.FirstLineColor;
                breakLine = 0;
              }

              plan.Cell(table.Cells[countCells].Position).SetValue(table.Cells[countCells].Value);
              plan.Cell(table.Cells[countCells].Position).Style.Fill.BackgroundColor = XLColor.FromHtml(color);
              plan.Cell(table.Cells[countCells].Position).Style.Font.FontColor = XLColor.FromHtml(table.Style.FontLineColor);
              countCells++;
              breakLine++;
            }

            // convert cells to table format
            var tableRange = plan.Range($"{cellFirstColumn}:{cellLastColumn}");
            tableRange.CreateTable();

          }

          // worksheet configuration
          if(string.IsNullOrEmpty(sheet.WorkSheetColor))
            sheet.WorkSheetColor = "#fff";

          plan.TabColor = XLColor.FromHtml(sheet.WorkSheetColor);
          plan.Rows().AdjustToContents();
          plan.Rows().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
          plan.Columns().AdjustToContents();
          plan.Columns().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
          plan.Style.Alignment.JustifyLastLine = true;
        }
        
        // Save file
        workb.SaveAs(filepath);
      }
    }

    /// <summary>
    /// [EN]: Gets the location of the next column in the worksheet<br></br>
    /// [PT-BR]: Obtém a localização da próxima coluna da planilha
    /// </summary>
    /// <param name="currentColumn">
    /// [EN]: Value corresponding to the current column location<br></br>
    /// [PT-BR]: Valor correspondente a localização da coluna atual
    /// </param>
    /// <returns>
    /// [EN]: Returns the value of the next column in the worksheet<br></br>
    /// [PT-BR]: Retorna o valor da proxima coluna da planilha
    /// </returns>
    /// <exception cref="RequiredParamsException"></exception>
    public static string GetNextColumn(string currentColumn = null) {
      char[] chars = string.Format("ABCDEFGHIJKLMNOPQRSTUVWXYZ").ToArray();
      string number;
      string nextColumn = string.Empty;
      string aux;
      char lastChar;

      if(string.IsNullOrEmpty(currentColumn))
        return "A1";

      number = X.GetOnlyNumbers(currentColumn).ToString();
      aux = X.GetOnlyLetters(currentColumn);
      lastChar = aux.ToArray().Last();

      if(string.IsNullOrEmpty(number))
        throw new RequiredParamsException(Situation.IsNullOrEmpty, nameof(number));

      if(lastChar == 'Z') {
        nextColumn = currentColumn += $"A{number}";
        return nextColumn;

      } else {
        for(int i = 0; i < chars.Length; i++) {
          if(chars[i] == lastChar) {
            nextColumn = aux[0..^1] + chars[i + 1];
            break;
          }
        }

        return nextColumn += number;
      }


    }

    /// <summary>
    /// [EN]: Gets the value of the next row in the worksheet<br></br>
    /// [PT-BR]: Obtém o valor da próxima linha da planilha
    /// </summary>
    /// <param name="currentLine">
    /// [EN]: Value corresponding to the current line location<br></br>
    /// [PT-BR]: Valor correspondente a localização da linha atual
    /// </param>
    /// <returns>
    /// [EN]: Returns the value of the next line in the worksheet<br></br>
    /// [PT-BR]: Retorna o valor da proxima linha da planilha
    /// </returns>
    public static string GetNextLine(string currentLine = null) {
      string lineNextNumber;
      string aux;
      if(string.IsNullOrEmpty(currentLine))
        return "A1";

      lineNextNumber = (Convert.ToInt32(X.GetOnlyNumbers(currentLine)) + 1).ToString();
      aux = X.GetOnlyLetters(currentLine);

      return aux + lineNextNumber;
    }
  }
}
