using LskConfig;



/// <summary>
/// Enchant_templateConfig配置表
/// </summary>
public partial class Enchant_templateConfig : TxtConfig<Enchant_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Enchant_template";
    }

    public static Enchant_template GetEnchant_Template(int id)
    {
        return Config._Enchant_template.Find(a => a.templateID == id);
    }
}
