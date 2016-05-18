using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace ResourceXmlEditor
{
  public class RibbonXmlWriter
  {

    public RibbonXmlWriter(string fileName)
    {
      FileName = fileName;
    }

    private string FileName { get; set; }

    public void WriteItems(IEnumerable<RibbonItem> items)
    {
      XDocument doc = XDocument.Load(FileName);

      foreach (var item in items)
      {
        var element = doc.Descendants().SingleOrDefault(e => e.Attributes("id").Any() && 
                                                        e.Attribute("id").Value == item.Id);

        if (element == null)
        {
          Trace.TraceWarning(@"Element ID ""{item.Id}"" not found.");
          continue;
        }

        UpdateOrInsertAttr(element, "label", item.Label);
        UpdateOrInsertAttr(element, "screentip", item.Screentip);
        UpdateOrInsertAttr(element, "supertip", item.Supertip);

      }

      doc.Save(FileName, SaveOptions.None);
    }

    private void UpdateOrInsertAttr(XElement elem, string name, string value)
    {
      if (!String.IsNullOrEmpty(value))
      {
        if(elem.Attribute(name) != null)
        {
          elem.Attribute(name).Value = value;
        }
        else
        {
          elem.Add(new XAttribute(name, value));
        }
      }
    }
  }
}
