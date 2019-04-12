using LskConfig;
using System.Collections.Generic;



/// <summary>
/// Perk_templateConfig配置表
/// </summary>
public partial class Perk_templateConfig : TxtConfig<Perk_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Perk_template";
    }

    public static Perk_template GetPerk_Template(int _id)
    {
        return Config._Perk_template.Find(a => a.perkID == _id);
    }

    public static List<Perk_template> GetAll()
    {
        return Config._Perk_template;

    }
}
