using LskConfig;



/// <summary>
/// Forge_templateConfig配置表
/// </summary>
public partial class Forge_templateConfig : TxtConfig<Forge_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Forge_template";
    }
    
    public static Forge_template GetForge_template(int id)
    {
        return Config._Forge_template.Find(a => a.formulaID == id);
    }
}
