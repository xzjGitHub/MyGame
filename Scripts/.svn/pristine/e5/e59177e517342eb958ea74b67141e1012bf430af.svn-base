using LskConfig;



/// <summary>
/// State_templateConfig配置表
/// </summary>
public partial class State_templateConfig : TxtConfig<State_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "State_template";
    }

    public static State_template GetState_template(int id)
    {
        return Config._State_template.Find(a => a.stateID == id);
    }
}
