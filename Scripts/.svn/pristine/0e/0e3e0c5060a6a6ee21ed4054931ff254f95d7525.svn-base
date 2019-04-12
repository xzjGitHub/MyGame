using LskConfig;



/// <summary>
/// StateEffectConfigConfig配置表
/// </summary>
public partial class StateEffectConfigConfig : TxtConfig<StateEffectConfigConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "StateEffectConfig";
    }

    public static StateEffectConfig GetStateEffectConfig(int id)
    {
        return Config._StateEffectConfig.Find(a => a.stateEffectID == id);
    }
}
