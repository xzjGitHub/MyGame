using LskConfig;



/// <summary>
/// Char_displayConfig配置表
/// </summary>
public partial class Char_displayConfig : TxtConfig<Char_displayConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Char_display";
    }

    public static Char_display GetChar_display(int _id)
    {
        return Config._Char_display.Find(a => a.attributeID == _id);
    }
}
