using LskConfig;
using System.Collections.Generic;



/// <summary>
/// Char_templateConfig配置表
/// </summary>
public partial class Char_templateConfig : TxtConfig<Char_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Char_template";
    }

    public static Char_template GetTemplate(int id)
    {
        return Config._Char_template.Find(a => a.templateID == id);
    }


    public static List<Char_template> GetTemplates()
    {
        return Config._Char_template;
    }
}
