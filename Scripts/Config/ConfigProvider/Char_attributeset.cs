using LskConfig;



/// <summary>
/// Char_attributesetConfig配置表
/// </summary>
public partial class Char_attributesetConfig : TxtConfig<Char_attributesetConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Char_attributeset";
    }

    public static Char_attributeset GetChar_attribute(int id)
    {
        return Config._Char_attributeset.Find(a => a.attributeID == id);
    }
}
