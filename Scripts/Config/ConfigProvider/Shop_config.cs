using LskConfig;
using System.Collections.Generic;


/// <summary>
/// Shop_configConfig配置表
/// </summary>
public partial class Shop_configConfig : TxtConfig<Shop_configConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Shop_config";
    }

    public static Shop_config GetShop_Config()
    {
        return Config._Shop_config[0];
    }

    public static List<Shop_config> GetShop_Configs()
    {
        return Config._Shop_config;
    }
}
