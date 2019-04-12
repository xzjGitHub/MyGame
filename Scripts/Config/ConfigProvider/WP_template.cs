using LskConfig;



/// <summary>
/// WP_templateConfig配置表
/// </summary>
public partial class WP_templateConfig : TxtConfig<WP_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "WP_template";
    }

    public static WP_template GetWpTemplate(int _id)
    {
        return Config._WP_template.Find(a => a.WPID == _id);
    }

}
