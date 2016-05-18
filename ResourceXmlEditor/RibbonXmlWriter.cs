using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;

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
          Trace.TraceWarning($"Element ID \"{item.Id}\" not found.");
          continue;
        }

        UpdateOrInsertAttr(element, "label", item.Label);
        UpdateOrInsertAttr(element, "screentip", item.Screentip);
        UpdateOrInsertAttr(element, "supertip", item.Supertip);

      }

      if (!Validate(doc))
        throw new XmlSchemaValidationException();

      doc.Save(FileName, SaveOptions.None);
    }

    private bool Validate(XDocument doc)
    {
      var schemaSet = new XmlSchemaSet();
      AddSchema(schemaSet, @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Xml\Schemas\1033\customUI.xsd");
      AddSchema(schemaSet, @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Xml\Schemas\1033\customUI14.xsd");
      
      var errors = new List<string>();
     
      doc.Validate(schemaSet, (sender, e) =>
      {
        var xobj = ((XObject) sender);
        string element = xobj.ToString();
        string name = xobj.Parent?.ToString();

        string error = $"Error: {e.Message}\tSeverity:{e.Severity}\tElement:{element}\tName:{name}";
        errors.Add(error);

        Trace.TraceWarning(error);
      });

      return !errors.Any();
    }

    private void AddSchema(XmlSchemaSet schemaSet, string filePath)
    {
      using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
      {
        schemaSet.Add(XmlSchema.Read(fs, null));
      }
     
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
