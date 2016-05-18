using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ResourceXmlEditor
{
  public class ClipboardExport
  {
    public const string ExportHeader = "Id\tType\tLabel\tScreentip\tSupertip";

    public void Export(IEnumerable<RibbonItem> items)
    {
      var export = new StringBuilder();
      
      export.Append(ExportHeader);
      export.AppendLine();

      foreach (var item in items)
      {
        export.Append(item.ToString());
        export.AppendLine();
      }

      Clipboard.Clear();
      Clipboard.SetText(export.ToString(),TextDataFormat.Text);
    }
  }
}
