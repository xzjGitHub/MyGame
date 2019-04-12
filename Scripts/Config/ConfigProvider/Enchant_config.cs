using LskConfig;



/// <summary>
/// Enchant_configConfig配置表
/// </summary>
public partial class Enchant_configConfig : TxtConfig<Enchant_configConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Enchant_config";
    }

    public static Enchant_config GetEnchant_Config()
    {
        return Config._Enchant_config[0];
    }
}
