using LskConfig;



/// <summary>
/// EffectSetConfigConfig配置表
/// </summary>
public partial class EffectSetConfigConfig : TxtConfig<EffectSetConfigConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "EffectSetConfig";
    }

    public static EffectSetConfig GetEffectSetConfig(int id)
    {
        return Config._EffectSetConfig.Find(a => a.effectSetID == id);
    }

}
