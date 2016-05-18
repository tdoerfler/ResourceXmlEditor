using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceXmlEditor
{
  public class ClipboardImport
  {
    public bool CanImport
    {
      get
      {
        return Clipboard.ContainsText(TextDataFormat.Text);
      }
    }

    public IEnumerable<RibbonItem> Import()
    {
      if (!CanImport)
      {
        throw new InvalidOperationException();
      }

      var items = new List<RibbonItem>();

      var importText = Clipboard.GetText(TextDataFormat.Text);

      var textLines = importText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

      if (textLines[0] == ClipboardExport.ExportHeader)
      {
        for (int i = 1; i < textLines.Count(); i++)
        {
          var fields = textLines[i].Split('\t');

          items.Add(new RibbonItem
                {
                  Id = fields[0],
                  ItemType = fields[1],
                  Label = fields[2],
                  Screentip = fields[3],
                  Supertip = fields[4]
                });
        }
      }

      return items;
    }
  }
}
