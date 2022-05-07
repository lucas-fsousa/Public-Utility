using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes {
  public class ExcelColumn {
    public string CellPosition { get; set; }
    public string ColumnName { get; set; }
  }

  public class TableStyle {
    public string ColumnColor { get; set; }
    public string FirstLineColor { get; set; }
    public string SecondLineColor { get; set; }
  }

  public class ExcelLine {
    public string CellPosition { get; set; }
    public string CellValue { get; set; }
  }
  public class Excel {
    public string PlanName { get; set; }
    public List<ExcelColumn> ExcelColumns { get; set; }
    public List<ExcelLine> ExcelLines { get; set; }
    public TableStyle TableStyle { get; set; }

    public void GerarExcel(string caminhoArquivoParaSalvar) {
      using(XLWorkbook workb = new()) {
        string cellFirstColumn = string.Empty;
        string cellLastColumn = string.Empty;
        string cellFirstLine = string.Empty;
        string cellLastLine = string.Empty;
        int startColumnIndex = 0;
        int endColumnIndex = 0;
        int startLineIndex = 0;
        int endCoLineIndex = 0;

        // INICIO DE CHECAGENS
        if(this == null)
          return;

        // GARANTE QUE NENHUMA LINHA SOBREPONHA UMA COLUNA
        List<ExcelLine> linesToRemove = new List<ExcelLine>();
        foreach(ExcelLine line in this.ExcelLines) {
          foreach(var item in this.ExcelColumns.Where(x => x.CellPosition == line.CellPosition).ToList()) {
            linesToRemove.Add(line);
          }
        }

        // REMOVE TODAS AS LINHAS QUE SOBREPOREM UMA COLUNA
        linesToRemove.ForEach(line => {
          this.ExcelLines.Remove(line);
        });

        if(this.ExcelColumns.Count > 0) {
          cellFirstColumn = this.ExcelColumns.OrderBy(x => x.CellPosition).FirstOrDefault().CellPosition;
          cellLastColumn = this.ExcelColumns.OrderBy(x => x.CellPosition).LastOrDefault().CellPosition;
          startColumnIndex = GetOnlyNumbers(cellFirstColumn);
          endColumnIndex = GetOnlyNumbers(cellLastColumn);
        }

        if(this.ExcelLines.Count > 0) {
          cellFirstLine = this.ExcelLines.OrderBy(x => x.CellPosition).FirstOrDefault().CellPosition;
          cellLastLine = this.ExcelLines.OrderBy(x => x.CellPosition).LastOrDefault().CellPosition;
          startLineIndex = GetOnlyNumbers(cellFirstLine);
          endCoLineIndex = GetOnlyNumbers(cellLastLine);
        }


        if(this.TableStyle == null) {
          TableStyle style = new TableStyle();
          style.ColumnColor = "#252525";
          style.FirstLineColor = "#959595";
          style.SecondLineColor = "#C0C0C0";

          this.TableStyle = style;
        }
        // FIM CHECAGENS

        var plan = workb.Worksheets.Add(this.PlanName); // Cria planilha excel

        // CRIA COLUNAS E FORMATA A COR DA COLUNA
        foreach(var columns in this.ExcelColumns) {
          plan.Cell(columns.CellPosition).SetValue(columns.ColumnName);
          plan.Cell(columns.CellPosition).Style.Alignment.SetJustifyLastLine();
          plan.Cell(columns.CellPosition).Style.Fill.BackgroundColor = XLColor.FromHtml(this.TableStyle.ColumnColor);

          // CRIA LINHAS COM VALORES DEFINIDOS E FORMATA COR DA CELULA
          int i = 0;
          string color = this.TableStyle.FirstLineColor;
          foreach(var lines in this.ExcelLines) {
            plan.Cell(lines.CellPosition).SetValue(lines.CellValue);

            if(i == this.ExcelColumns.Count) {
              color = color == this.TableStyle.FirstLineColor ? this.TableStyle.SecondLineColor : this.TableStyle.FirstLineColor;
              i = 0;
            }
            plan.Cell(lines.CellPosition).Style.Fill.BackgroundColor = XLColor.FromHtml(color);
            i++;
          }
          
        }

        // DEFINE OS ITENS PARA FORMATO DE TABELA
        var tableRange = plan.Range($"{cellFirstColumn}:{cellLastLine}");
        tableRange.CreateTable();

        // CONFIG DA PLANILHA
        plan.TabColor = XLColor.DarkViolet;
        plan.Rows().AdjustToContents();
        plan.Rows().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        plan.Columns().AdjustToContents();
        plan.Columns().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        plan.Style.Alignment.JustifyLastLine = true;
        //plan.Style.Alignment.TopToBottom  = true;

        // SALVA O ARQUIVO
        workb.SaveAs(caminhoArquivoParaSalvar);
      }
    }

    public static int GetOnlyNumbers(string str) {
      string newStr = string.Empty;
      foreach(char c in str) {
        if(char.IsNumber(c))
          newStr += c;
      }

      return Convert.ToInt32(newStr);
    }

    public static string GetOnlyChars(string str) {
      string newStr = string.Empty;
      foreach(char c in str) {
        if(char.IsLetter(c))
          newStr += c;
      }

      return newStr;
    }

    public static string GetNextColumn(string currentColumn = null) {
      char[] chars = string.Format("ABCDEFGHIJKLMNOPQRSTUVWXYZ").ToArray();
      string number;
      string nextColumn = string.Empty;
      string aux;
      char lastChar;

      if(string.IsNullOrEmpty(currentColumn))
        return "A1";

      number = GetOnlyNumbers(currentColumn).ToString();
      aux = GetOnlyChars(currentColumn);
      lastChar = aux.ToArray().Last();

      if(string.IsNullOrEmpty(number))
        throw new Exception("NUMERO DA LINHA NÃO INFORMADO");

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

    public static string GetNextLine(string currentLine = null) {
      string lineNextNumber;
      string aux;
      if(string.IsNullOrEmpty(currentLine))
        return "A1";

      lineNextNumber = (GetOnlyNumbers(currentLine) + 1).ToString();
      aux = GetOnlyChars(currentLine);

      return aux + lineNextNumber;
    }

  }
}
