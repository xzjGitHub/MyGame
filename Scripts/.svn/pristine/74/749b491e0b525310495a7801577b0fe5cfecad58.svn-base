using System.Collections.Generic;
using LskConfig;



/// <summary>
/// PassiveSkill_templateConfig配置表
/// </summary>
public partial class PassiveSkill_templateConfig : TxtConfig<PassiveSkill_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "PassiveSkill_template";
    }

    public static PassiveSkill_template GetPassiveSkill_Template(int _id)
    {
        return Config._PassiveSkill_template.Find(a => a.passiveSkillID == _id);
    }

    public static List<PassiveSkill_template> GetPassiveSkill_Templates()
    {
        return Config._PassiveSkill_template;
    }
}
