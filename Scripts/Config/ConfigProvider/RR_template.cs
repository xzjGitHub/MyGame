using LskConfig;



/// <summary>
/// RR_templateConfig配置表
/// </summary>
public partial class RR_templateConfig : TxtConfig<RR_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "RR_template";
    }

    public static RR_template GetTemplate(int id)
    {
        return Config._RR_template.Find(a => a.zoneID == id);
    }
}
