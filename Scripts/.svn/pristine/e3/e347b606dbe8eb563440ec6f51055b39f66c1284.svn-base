using LskConfig;



/// <summary>
/// EffectPlayTypeConfig配置表
/// </summary>
public partial class EffectPlayTypeConfig : TxtConfig<EffectPlayTypeConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "EffectPlayType";
    }

    public static EffectPlayType GetPlayType(int id)
    {
        return Config._EffectPlayType.Find(a => a.playType == id);
    }
}