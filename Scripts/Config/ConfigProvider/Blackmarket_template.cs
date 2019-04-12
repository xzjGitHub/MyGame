using LskConfig;



/// <summary>
/// Blackmarket_templateConfig配置表
/// </summary>
public partial class Blackmarket_templateConfig : TxtConfig<Blackmarket_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Blackmarket_template";
    }


    public static Blackmarket_template GetBlackmarket_Template(int id)
    {
        return Config._Blackmarket_template.Find(a => a.zoneID == id);
    }
}
