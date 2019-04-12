using LskConfig;



/// <summary>
/// Targetset_templateConfig配置表
/// </summary>
public partial class Targetset_templateConfig : TxtConfig<Targetset_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Targetset_template";
    }

    public static Targetset_template GetTargetset_template(int id)
    {
        return Config._Targetset_template.Find(a => a.targetSetID == id);
    }
}
