using LskConfig;



/// <summary>
/// Char_qualityupConfig配置表
/// </summary>
public partial class Char_qualityupConfig : TxtConfig<Char_qualityupConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Char_qualityup";
    }

    public static Char_qualityup GetChar_qualityup(int id)
    {
        return Config._Char_qualityup.Find(a => a.charQuality == id);
    }
}
