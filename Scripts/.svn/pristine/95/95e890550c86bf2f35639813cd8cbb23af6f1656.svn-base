using LskConfig;



/// <summary>
/// CommonEffectConfigConfig配置表
/// </summary>
public partial class CommonEffectConfigConfig : TxtConfig<CommonEffectConfigConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "CommonEffectConfig";
    }

    public static CommonEffectConfig GetCommonEffectConfig(int id)
    {
        return Config._CommonEffectConfig.Find(a => a.commonEffectID == id);
    }
}
