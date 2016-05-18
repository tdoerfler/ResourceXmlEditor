using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace ResourceXmlEditor.Tests
{
  [TestClass]
  public class RibbonXmlReaderTest
  {
    [TestMethod]
    public void LoadItems()
    {
      var reader = new RibbonXmlReader(System.IO.Path.Combine("Data", "MainRibbon.xml"));
      var items = reader.GetItems();

      Assert.IsTrue(items.Any());
    }

    [TestMethod]
    public void ExportToClipboard()
    {
      var reader = new RibbonXmlReader(System.IO.Path.Combine("Data", "MainRibbon.xml"));
      var items = reader.GetItems();

      var export = new ClipboardExport();
      export.Export(items);

      var text = Clipboard.GetText();
      var lines = text.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);

      Assert.AreEqual(items.Count() + 1, lines.Length);
    }

    [TestMethod]
    public void ClipboardImportTest()
    {
      var import = new ClipboardImport();
      var items = import.Import();

      Assert.IsTrue(items.Any());
    }

    [TestMethod]
    public void WriteResourceFromClipboard()
    {
      var import = new ClipboardImport();

      if (!import.CanImport)
      {
        Assert.Fail();
      }

      var items = import.Import();
      var writer = new RibbonXmlWriter(System.IO.Path.Combine("Data", "MainRibbon.xml"));

      writer.WriteItems(items);

      Assert.IsTrue(items.Any());
    }
  }
}
