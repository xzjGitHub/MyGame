using LskConfig;



/// <summary>
/// Effect_templateConfig配置表
/// </summary>
public partial class Effect_templateConfig : TxtConfig<Effect_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Effect_template";
    }

    public static Effect_template GetTemplate(int id)
    {
        return Config._Effect_template.Find(a => a.effectID == id);
    }
}
