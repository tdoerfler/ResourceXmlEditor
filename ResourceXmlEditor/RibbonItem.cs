namespace ResourceXmlEditor
{
  public class RibbonItem
  {
    public string Id { get; set; }
    public string Label { get; set; }
    public string Screentip { get; set; }
    public string Supertip { get; set; }
    public string ItemType { get; set; }

    public override string ToString()
    {
      return $"{Id}\t{ItemType}\t{Label}\t{Screentip}\t{Supertip}";
    }
  }
}
