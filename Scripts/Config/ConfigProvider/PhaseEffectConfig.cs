using LskConfig;



/// <summary>
/// PhaseEffectConfigConfig配置表
/// </summary>
public partial class PhaseEffectConfigConfig : TxtConfig<PhaseEffectConfigConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "PhaseEffectConfig";
    }

    public static PhaseEffectConfig GetPhaseEffectConfig(int id)
    {
        return Config._PhaseEffectConfig.Find(a => a.phaseEffectID == id);
    }
}
