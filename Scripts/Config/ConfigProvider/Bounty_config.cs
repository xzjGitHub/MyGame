using LskConfig;



/// <summary>
/// Bounty_configConfig配置表
/// </summary>
public partial class Bounty_configConfig : TxtConfig<Bounty_configConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Bounty_config";
    }

    public static Bounty_config GetConfig()
    {
        return Config._Bounty_config[0];
    }
}
