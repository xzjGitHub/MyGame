using LskConfig;



/// <summary>
/// Refine_templateConfig配置表
/// </summary>
public partial class Refine_templateConfig : TxtConfig<Refine_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Refine_template";
    }

    public static Refine_template GetRefine_Template(int id)
    {
        return Config._Refine_template.Find(a => a.refineID == id);
    }
}
