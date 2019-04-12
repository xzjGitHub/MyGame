using LskConfig;



/// <summary>
/// Core_lvupConfig配置表
/// </summary>
public partial class Core_lvupConfig: TxtConfig<Core_lvupConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Core_lvup";
    }

    public static Core_lvup GetCore_lvup(int level)
    {
        return Config._Core_lvup.Find(a => a.coreLevel == level);
    }

    public static int GetMinAddEnchantLevel(Core_lvup core)
    {
        if(core == null)
        {
            return 0;
        }
        else
        {
            return core.addEnchantLevel.Count < 2 ? 0 : core.addEnchantLevel[0];
        }
    }

    public static int GetMaxAddEnchantLevel(Core_lvup core)
    {
        if(core == null)
        {
            return 0;
        }
        return core.addEnchantLevel.Count < 2 ? 0 : core.addEnchantLevel[1];
    }

    public static float GetMaxLevelNeedPower()
    {
        return Config._Core_lvup[Config._Core_lvup.Count - 2].lvupCorePower;
    }
}
