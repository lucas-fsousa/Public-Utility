﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes {
  public class Cell {
    public string Position { get; set; }
    public string Value { get; set; }
  }

  public class TableStyle {
    public string ColumnColor { get; set; }
    public string FirstLineColor { get; set; }
    public string SecondLineColor { get; set; }
  }

  public class Excel {
    public string PlanName { get; set; }
    public int NumberOfColumns { get; set; }
    public List<Cell> Cells { get; set; }
    public TableStyle TableStyle { get; set; }

    public void GerarExcel(string caminhoArquivoParaSalvar) {
      using(XLWorkbook workb = new()) {
        string cellFirstColumn = string.Empty;
        string cellLastColumn = string.Empty;
        int countCells = 0;

        // INICIO DE CHECAGENS
        if(this == null)
          return;

        if(this.Cells.Count > 0) {
          cellFirstColumn = this.Cells.FirstOrDefault().Position;
          cellLastColumn = this.Cells.LastOrDefault().Position;
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
        while(countCells < this.NumberOfColumns) {
          plan.Cell(this.Cells[countCells].Position).SetValue(this.Cells[countCells].Value);
          plan.Cell(this.Cells[countCells].Position).Style.Fill.BackgroundColor = XLColor.FromHtml(this.TableStyle.ColumnColor);
          countCells++;
        }

        // CRIA LINHAS E FORMATA A COR DA LINHA
        string color = this.TableStyle.FirstLineColor;
        int breakLine = 0;
        while(countCells < this.Cells.Count) {
          if(breakLine == this.NumberOfColumns) {
            color = color == this.TableStyle.FirstLineColor ? this.TableStyle.SecondLineColor : this.TableStyle.FirstLineColor;
            breakLine = 0;
          }

          plan.Cell(this.Cells[countCells].Position).SetValue(this.Cells[countCells].Value);
          plan.Cell(this.Cells[countCells].Position).Style.Fill.BackgroundColor = XLColor.FromHtml(color);
          countCells++;
          breakLine++;
        }


        // DEFINE OS ITENS PARA FORMATO DE TABELA
        var tableRange = plan.Range($"{cellFirstColumn}:{cellLastColumn}");
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
