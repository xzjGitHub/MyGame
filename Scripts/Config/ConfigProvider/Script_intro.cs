using LskConfig;



/// <summary>
/// Script_introConfig配置表
/// </summary>
public partial class Script_introConfig : TxtConfig<Script_introConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Script_intro";
    }


    public static Script_intro GetScript_intro()
    {
        return Config._Script_intro[0];
    }
}
