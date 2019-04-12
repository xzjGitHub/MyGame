using LskConfig;



/// <summary>
/// Equip_qualityupConfig配置表
/// </summary>
public partial class Equip_qualityupConfig : TxtConfig<Equip_qualityupConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Equip_qualityup";
    }

    public static Equip_qualityup GetQualityup(int equipQuality)
    {
        return Config._Equip_qualityup.Find(a => a.equipQuality == equipQuality);
    }
}
