using LskConfig;



/// <summary>
/// CharEffectConfigConfig配置表
/// </summary>
public partial class CharEffectConfigConfig : TxtConfig<CharEffectConfigConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "CharEffectConfig";
    }

    public static CharEffectConfig GetCharEffectConfig(int id)
    {
        return Config._CharEffectConfig.Find(a => a.CharID == id);
    }
}
