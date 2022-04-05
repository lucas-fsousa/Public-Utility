using App.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Testes {
  public class XPdf {
    public static string GetText(byte[] buffer) => GetTextBase(new MemoryStream(buffer));

    public static string GetText(string filePath) => GetTextBase(File.OpenRead(filePath));

    public static string GetText(Uri uri) {
      HttpResponseMessage response = XRequest.HttpGet(uri).GetAwaiter().GetResult();
      string text = string.Empty;
      if(response.IsSuccessStatusCode) {
        text = GetTextBase(response.Content.ReadAsStream());

      } else {
        text = string.Format($"## ERRO ## REQUEST STATUS CODE {response.StatusCode} ## {DateTime.Now} ##");
      }
      return text;
    }


    private static string Decompress(byte[] input) {
      byte[] cutinput = new byte[input.Length - 2];
      Array.Copy(input, 2, cutinput, 0, cutinput.Length);

      var stream = new MemoryStream();

      using(var compressStream = new MemoryStream(cutinput))
      using(var decompressor = new DeflateStream(compressStream, CompressionMode.Decompress))
        decompressor.CopyTo(stream);

      return Encoding.Default.GetString(stream.ToArray());
    }

    private static byte[] DecryptPDF(byte[] pdfEncryptBuffer) {
      return Array.Empty<byte>();
    }

    private static string GetTextBase(Stream stream) {
      string text = string.Empty;

      using(Stream stm = stream) {
        byte[] buffer = new byte[stm.Length];
        stm.Read(buffer, 0, buffer.Length);
        
        byte[] unformatedTextBuffer = DecryptPDF(buffer);

        string unformatedText = Decompress(unformatedTextBuffer);

        text = ExtractText(unformatedText);
        return text;
      }

    }

    private static string ExtractText(string unformatText) {
      //const string _endToinitTxtLine = "Td\r";
      const string _endTxtLine = ") Tj\r";
      string text = string.Empty;

      IEnumerable<string> lines = unformatText.Split("\n");

      lines.ToList().ForEach(line => {
        //if(line.EndsWith(_endToinitTxtLine)) { }
        if(line.EndsWith(_endTxtLine))
          text += line[1..^_endTxtLine.Length];

      });

      text = text.Trim().Replace("\r", "").Replace("\t", "");
      return text;
    }

  }
}
