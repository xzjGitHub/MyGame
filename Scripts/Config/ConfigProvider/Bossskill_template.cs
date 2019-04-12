using LskConfig;



/// <summary>
/// Bossskill_templateConfig配置表
/// </summary>
public partial class Bossskill_templateConfig : TxtConfig<Bossskill_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Bossskill_template";
    }

    public static Bossskill_template GeTemplate(int id)
    {
        return Config._Bossskill_template.Find(a => a.skillID == id);
    }
}



/// <summary>
/// Bossskill_template配置表
/// </summary>
public partial class Bossskill_template
{
    public Combatskill_template Combatskill
    {
        get { return Combatskill_templateConfig.GetCombatskill_template(skillID); }
    }
}