using LskConfig;



/// <summary>
/// ER_templateConfig配置表
/// </summary>
public partial class ER_templateConfig : TxtConfig<ER_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "ER_template";
    }

    public static ER_template GetER_template(int id)
    {
        return Config._ER_template.Find(a => a.templateID == id);
    }
}
