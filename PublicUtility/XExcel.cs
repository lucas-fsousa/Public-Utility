using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicUtility {
  public class Cell {
    public string Position { get; set; }
    public string Value { get; set; }
  }

  public class TableStyle {
    public string ColumnColor { get; set; }
    public string FirstLineColor { get; set; }
    public string SecondLineColor { get; set; }
  }

  public class Table {
    public TableStyle Style { get; set; }
    public int NumberOfColumns { get; set; }
    public List<Cell> Cells { get; set; }
  }

  public class XExcel {
    public string PlanName { get; set; }
    public List<Table> Tables { get; set; }

    public void Generate(string filepath) {
      // Make sure the extension is compatible with the file
      if(!filepath.EndsWith(".xlsx"))
        filepath += ".xlsx";

      using(XLWorkbook workb = new()) {
        var plan = workb.Worksheets.Add(string.IsNullOrEmpty(this.PlanName) ? "PLAN1" : this.PlanName); // Create the Worksheet
        string cellFirstColumn;
        string cellLastColumn;
        int countCells;

        foreach(Table table in this.Tables) {
          cellFirstColumn = table.Cells.FirstOrDefault().Position;
          cellLastColumn = table.Cells.LastOrDefault().Position;
          countCells = 0;

          if(table.Style == null) {
            TableStyle style = new TableStyle();
            style.ColumnColor = "#252525";
            style.FirstLineColor = "#959595";
            style.SecondLineColor = "#C0C0C0";

            table.Style = style;
          }

          // create the columns and format the background color
          while(countCells < table.NumberOfColumns) {
            plan.Cell(table.Cells[countCells].Position).SetValue(table.Cells[countCells].Value);
            plan.Cell(table.Cells[countCells].Position).Style.Fill.BackgroundColor = XLColor.FromHtml(table.Style.ColumnColor);
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
            countCells++;
            breakLine++;
          }

          // convert cells to table format
          var tableRange = plan.Range($"{cellFirstColumn}:{cellLastColumn}");
          tableRange.CreateTable();

        }

        // worksheet configuration
        plan.TabColor = XLColor.DarkViolet;
        plan.Rows().AdjustToContents();
        plan.Rows().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        plan.Columns().AdjustToContents();
        plan.Columns().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        plan.Style.Alignment.JustifyLastLine = true;

        // Save file
        workb.SaveAs(filepath);
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
