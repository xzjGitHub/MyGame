using LskConfig;



/// <summary>
/// WP_setupConfig配置表
/// </summary>
public partial class WP_setupConfig : TxtConfig<WP_setupConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "WP_setup";
    }

    public static WP_setup GetWPSetup(int id)
    {
        return Config._WP_setup.Find(a => a.ESID == id);
    }


}
