using LskConfig;



/// <summary>
/// Script_configConfig配置表
/// </summary>
public partial class Script_configConfig : TxtConfig<Script_configConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Script_config";
    }

    public static int GetDaySecond()
    {
        return Config._Script_config[0].timeUnit;
    }

    public static Script_config GetScript_config(){
        return Config._Script_config[0];
    }
}
