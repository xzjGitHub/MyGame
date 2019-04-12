using LskConfig;



/// <summary>
/// ActionEffectConfigConfig配置表
/// </summary>
public partial class Action_templateConfig : TxtConfig<Action_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Action_template";
    }

    public static Action_template GetActionEffectConfig(int id)
    {
        return Config._Action_template.Find(a => a.actionID == id);
    }
}
