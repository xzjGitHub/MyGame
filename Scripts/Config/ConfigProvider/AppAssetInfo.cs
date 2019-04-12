using LskConfig;



/// <summary>
/// AppAssetInfoConfig配置表
/// </summary>
public partial class AppAssetInfoConfig : TxtConfig<AppAssetInfoConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "AppAssetInfo";
    }

    public static AppAssetInfo GetAssetInfo()
    {
        return Config._AppAssetInfo[0];
    }
}
