using LskConfig;



/// <summary>
/// SkillActionConfigConfig配置表
/// </summary>
public partial class SkillActionConfig : TxtConfig<SkillActionConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "SkillAction";
    }

    public static SkillAction GetSkillActionConfig(int id)
    {
        return Config._SkillAction.Find(a => a.skillID == id);
    }
}
