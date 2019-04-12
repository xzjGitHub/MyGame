using LskConfig;



/// <summary>
/// Invasion_configConfig配置表
/// </summary>
public partial class Invasion_configConfig : TxtConfig<Invasion_configConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Invasion_config";
    }

    public static Invasion_config GetConfig()
    {
        return Config._Invasion_config[0];
    }
}
