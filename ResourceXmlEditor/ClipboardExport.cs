using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ResourceXmlEditor
{
  public class ClipboardExport
  {
    public void Export(IEnumerable<RibbonItem> items)
    {
      const string exportHeader = "Id\tType\tLabel\tScreentip\tSupertip\n";

      var export = new StringBuilder();
      
      export.Append(exportHeader);

      foreach (var item in items)
      {
        export.Append($"{item}\n");
      }

      Clipboard.Clear();
      Clipboard.SetText(export.ToString(),TextDataFormat.Text);
    }
  }
}
