using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ResourceXmlEditor
{
  public class RibbonXmlReader
  {

    public RibbonXmlReader(string fileName)
    {
      FileName = fileName;
    }

    private string FileName { get; set; }

    public IEnumerable<RibbonItem> GetItems()
    {
      XDocument doc = XDocument.Load(FileName);

      return from x in doc.Descendants()
             where x.Attributes("id").Any()
             select new RibbonItem
             {
               Id = x.Attribute("id").Value,
               ItemType = x.Name.LocalName,
               Label = x.Attribute("label")?.Value,
               Screentip = x.Attribute("screentip")?.Value,
               Supertip = x.Attribute("supertip")?.Value
             };

    }
  }
}
