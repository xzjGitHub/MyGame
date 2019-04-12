using System.Collections.Generic;
using LskConfig;



/// <summary>
/// Personality_templateConfig配置表
/// </summary>
public partial class Personality_templateConfig : TxtConfig<Personality_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Personality_template";
    }

    public static Personality_template GetTemplate(int id)
    {
        return Config._Personality_template.Find(a => a.personalityID == id);
    }

    public static List<Personality_template>  GetTemplateAll()
    {
        return Config._Personality_template;
    }
}
