using LskConfig;



/// <summary>
/// Map_templateConfig配置表
/// </summary>
public partial class Map_templateConfig : TxtConfig<Map_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Map_template";
    }

    public static Map_template GetMap_templat(int id)
    {
        return Config._Map_template.Find(a => a.mapID == id);
    }
}
