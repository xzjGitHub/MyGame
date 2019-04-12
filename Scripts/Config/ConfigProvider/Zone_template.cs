using LskConfig;



/// <summary>
/// Zone_templateConfig配置表
/// </summary>
public partial class Zone_templateConfig : TxtConfig<Zone_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Zone_template";
    }

    public static Zone_template GetZoneTemplate(int id)
    {
        return Config._Zone_template.Find(a => a.zoneID == id);
    }
}
