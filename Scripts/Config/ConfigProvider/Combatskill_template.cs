using System.Collections.Generic;
using LskConfig;



/// <summary>
/// Combatskill_templateConfig配置表
/// </summary>
public partial class Combatskill_templateConfig : TxtConfig<Combatskill_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Combatskill_template";
    }


    public static Combatskill_template GetCombatskill_template(int id)
    {
        
        return Config._Combatskill_template.Find(a => a.skillID == id);
    }

}
