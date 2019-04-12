using LskConfig;
using System.Collections.Generic;


/// <summary>
/// Forge_configConfig配置表
/// </summary>
public partial class Forge_configConfig : TxtConfig<Forge_configConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Forge_config";
    }

    public static Forge_config GetForge_config(int id)
    {
        return Config._Forge_config.Find(a => a.forgeTypeID == id);
    }


    public static List<Forge_config> GetForge_Configs()
    {
        return Config._Forge_config;
    }
}
