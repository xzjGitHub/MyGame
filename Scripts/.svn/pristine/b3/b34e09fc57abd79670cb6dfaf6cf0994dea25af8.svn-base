using LskConfig;



/// <summary>
/// Mob_templateConfig配置表
/// </summary>
public partial class Mob_templateConfig : TxtConfig<Mob_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Mob_template";
    }

    public static Mob_template GetTemplate(int _id)
    {
        return Config._Mob_template.Find(a => a.templateID == _id);
    }
}
