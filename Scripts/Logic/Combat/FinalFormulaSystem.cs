/// <summary>
/// 最终公式系统
/// </summary>
public class FinalFormulaSystem
{
    /// <summary>
    /// 精度
    /// </summary>
    private static float accuracy = RandomBuilder.RandomMaxFactor;


    /// <summary>
    /// 得到最终值
    /// </summary>
    /// <param name="formulaId">公式编号</param>
    /// <param name="skillID">技能ID</param>
    /// <param name="charAttribute">攻击方角色属性</param>
    /// <returns></returns>
    public static float GetFinalValue(int formulaId, int skillID, CharAttribute charAttribute = null)
    {
        Combatskill_template combatskill_template = Combatskill_templateConfig.GetCombatskill_template(skillID);
        switch (formulaId)
        {
            case 1001:
                //return RandomBuilder.RandomNum(charAttribute.finalMaxDamage, charAttribute.finalMinDamage) * combatskill_template.skillCoeSet1[0] *
                //               (1 + combatskill_template.skillCoeSet2[0] * 1) * (1 + 0) * (1 + charAttribute.finalSkillDB1) *
                //               (1 + charAttribute.finalSkillDB2);
                break;
            case 1002:
                //return
                //    RandomBuilder.RandomNum(charAttribute.finalMaxDamage, charAttribute.finalMinDamage) *
                //    combatskill_template.skillCoeSet1[0] *
                //    (1 + combatskill_template.skillCoeSet2[0] * 1) *
                //    (1 + 0) * (1 + charAttribute.finalSkillDB1) * (1 + charAttribute.finalSkillDB2);
                break;
            case 1003:
                //return
                //    RandomBuilder.RandomNum(charAttribute.finalHealing, charAttribute.finalHealing) *
                //    combatskill_template.skillCoeSet1[0] *
                //    (1 + combatskill_template.skillCoeSet2[0] * 1) * (1 + charAttribute.finalSkillHB1) * (1 + charAttribute.finalSkillHB2);
                break;
            case 9001:
                //return combatskill_template.skillCoeSet1[0];
                break;
            default:
                return 0;
        }
        return 0;
    }


}

