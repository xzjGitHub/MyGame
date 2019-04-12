using LskConfig;



/// <summary>
/// Equip_templateConfig配置表
/// </summary>
public partial class Equip_templateConfig : TxtConfig<Equip_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Equip_template";
    }


    public static Equip_template GetEquip_template(int id)
    {
        return Config._Equip_template.Find(a => a.templateID == id);
    }
}
