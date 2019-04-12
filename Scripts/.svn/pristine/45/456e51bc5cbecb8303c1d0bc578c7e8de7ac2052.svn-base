using LskConfig;



/// <summary>
/// TargetsetEffectConfigConfig配置表
/// </summary>
public partial class ActionEffectConfig : TxtConfig<ActionEffectConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "ActionEffect";
    }

    //public static ActionEffect GeTargetsetEffectConfig(int id)
    //{
    //    return Config._ActionEffectConfig.Find(a => a.targetSetID == id);
    //}
    public static ActionEffect GetConfig(int id)
    {
        return Config._ActionEffect.Find(a => a.targetSetID == id);
    }

}
