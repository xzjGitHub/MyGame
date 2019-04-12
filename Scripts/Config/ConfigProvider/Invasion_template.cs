using LskConfig;



/// <summary>
/// Invasion_templateConfig配置表
/// </summary>
public partial class Invasion_templateConfig : TxtConfig<Invasion_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Invasion_template";
    }

    public static Invasion_template GetInvasionTemplate(int _id)
    {
        return Config._Invasion_template.Find(a => a.invasionID == _id);
    }
}
