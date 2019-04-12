using LskConfig;



/// <summary>
/// Combat_configConfig配置表
/// </summary>
public partial class Combat_configConfig : TxtConfig<Combat_configConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Combat_config";
    }

    public static Combat_config GetCombat_config(int id=0)
    {
        return Config._Combat_config[id];
    }
}
