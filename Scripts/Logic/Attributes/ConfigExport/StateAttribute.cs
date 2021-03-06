using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// StateAttribute
/// </summary>
public partial class StateAttribute
{
    /// <summary>
    /// 战斗配置
    /// </summary>
    public Combat_config combat_config;
    /// <summary>
    /// 命中模板
    /// </summary>
    public Targetset_template targetset_template;
    /// <summary>
    /// 状态属性
    /// </summary>
    public State_template state_template;
    /// <summary>
    /// 角色属性
    /// </summary>
    public CharAttribute charAttribute;
    /// <summary>
    /// 目标属性
    /// </summary>
    public CharAttribute targetAttribute;
    /// <summary>
    /// 最终盾伤
    /// </summary>
    public float finalShieldDB
    {
        get
        {
            return (float)(charAttribute.finalShieldDB + state_template.stateShieldDB);
        }
    }
    /// <summary>
    /// 最终甲伤
    /// </summary>
    public float finalArmorDB
    {
        get
        {
            return (float)(charAttribute.finalArmorDB + state_template.stateArmorDB);
        }
    }
    /// <summary>
    /// 最终血伤
    /// </summary>
    public float finalHPDB
    {
        get
        {
            return (float)(charAttribute.finalHPDB + state_template.stateHPDB);
        }
    }
    /// <summary>
    /// 基本物攻
    /// </summary>
    public float baseValue_1
    {
        get
        {
            return (float)(charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalAPBonus));
        }
    }
    /// <summary>
    /// 基本法攻
    /// </summary>
    public float baseValue_2
    {
        get
        {
            return (float)(charAttribute.preSP * (1 + charAttribute.fvDB + charAttribute.finalSPBonus));
        }
    }
    /// <summary>
    /// 普通治疗
    /// </summary>
    public float baseValue_3
    {
        get
        {
            return (float)(charAttribute.preSP * (1 + charAttribute.fvDB + charAttribute.finalSPBonus));
        }
    }
    /// <summary>
    /// 额外物攻
    /// </summary>
    public float baseValue_4
    {
        get
        {
            return (float)(charAttribute.addPDMG * (1 + charAttribute.fvDB + charAttribute.finalAPBonus));
        }
    }
    /// <summary>
    /// 额外物攻/群攻
    /// </summary>
    public float baseValue_5
    {
        get
        {
            return (float)(charAttribute.addPDMG * (1 + charAttribute.fvDB + charAttribute.finalAPBonus) / 3);
        }
    }
    /// <summary>
    /// 额外法攻
    /// </summary>
    public float baseValue_6
    {
        get
        {
            return (float)(charAttribute.addSDMG * (1 + charAttribute.fvDB + charAttribute.finalSPBonus));
        }
    }
    /// <summary>
    /// 额外法攻/群攻
    /// </summary>
    public float baseValue_7
    {
        get
        {
            return (float)(charAttribute.addSDMG * (1 + charAttribute.fvDB + charAttribute.finalSPBonus) / 3);
        }
    }
    /// <summary>
    /// 额外真伤
    /// </summary>
    public float baseValue_8
    {
        get
        {
            return (float)(charAttribute.addTrueDMG * (1 + charAttribute.fvDB + charAttribute.finalAPBonus + charAttribute.finalSPBonus));
        }
    }
    /// <summary>
    /// 额外真伤/群攻
    /// </summary>
    public float baseValue_9
    {
        get
        {
            return (float)(charAttribute.addTrueDMG * (1 + charAttribute.fvDB + charAttribute.finalAPBonus + charAttribute.finalSPBonus) / 3);
        }
    }
    /// <summary>
    /// 额外血伤
    /// </summary>
    public float baseValue_10
    {
        get
        {
            return (float)(charAttribute.addHPDMG * (1 + charAttribute.fvDB + charAttribute.finalAPBonus + charAttribute.finalSPBonus));
        }
    }
    /// <summary>
    /// 额外血伤/群攻
    /// </summary>
    public float baseValue_11
    {
        get
        {
            return (float)(charAttribute.addHPDMG * (1 + charAttribute.fvDB + charAttribute.finalAPBonus + charAttribute.finalSPBonus) / 3);
        }
    }
    /// <summary>
    /// 爆发护甲_物攻无效-坦克-无回能
    /// </summary>
    public float baseValue_12
    {
        get
        {
            return (float)(((charAttribute.finalSkill * 11 / 6 * 3 / 3) * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalArmorBonus) + charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalArmorBonus)) * 0.75);
        }
    }
    /// <summary>
    /// 爆发群防-物攻有效-坦克-无回能
    /// </summary>
    public float baseValue_13
    {
        get
        {
            return (float)((charAttribute.finalSkill * 11 / 6 * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalArmorBonus + charAttribute.finalShieldBonus) + charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalArmorBonus + charAttribute.finalShieldBonus)) / 3 * 0.75);
        }
    }
    /// <summary>
    /// 持续回盾-法伤有效-辅助
    /// </summary>
    public float baseValue_14
    {
        get
        {
            return (float)(((charAttribute.finalSkill * 11 / 6 / 3) * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalSPBonus) + charAttribute.preSP * (1 + charAttribute.fvDB + charAttribute.finalSPBonus)) * 0.75);
        }
    }
    /// <summary>
    /// 次要法伤-辅助
    /// </summary>
    public float baseValue_15
    {
        get
        {
            return (float)(((charAttribute.finalSkill * 2 / 3) * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalSPBonus) + charAttribute.preSP * (1 + charAttribute.fvDB + charAttribute.finalSPBonus)) * 0.75);
        }
    }
    /// <summary>
    /// 爆发真伤-物攻有效-输出
    /// </summary>
    public float baseValue_16
    {
        get
        {
            return (float)((charAttribute.finalSkill * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalAPBonus + charAttribute.finalSPBonus) + charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalAPBonus + charAttribute.finalSPBonus)) * 0.75);
        }
    }
    /// <summary>
    /// 爆发群攻-物攻有效-输出
    /// </summary>
    public float baseValue_17
    {
        get
        {
            return (float)(((charAttribute.finalSkill * 4.5 / 3) * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalAPBonus + charAttribute.finalSPBonus) + charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalAPBonus + charAttribute.finalSPBonus)) * 0.75 / 3);
        }
    }
    /// <summary>
    /// 盾甲双伤-物攻有效-输出
    /// </summary>
    public float baseValue_18
    {
        get
        {
            return (float)((charAttribute.finalSkill * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalAPBonus) + charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalAPBonus)) * 0.75);
        }
    }
    /// <summary>
    /// 专血伤-物攻有效-输出
    /// </summary>
    public float baseValue_19
    {
        get
        {
            return (float)((charAttribute.finalSkill * 2 / 3 * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalAPBonus) + charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalAPBonus)) * 0.75);
        }
    }
    /// <summary>
    /// 怪物普攻
    /// </summary>
    public float baseValue_51
    {
        get
        {
            return (float)(charAttribute.finalAttack);
        }
    }
    /// <summary>
    /// 怪物群攻
    /// </summary>
    public float baseValue_52
    {
        get
        {
            return (float)(charAttribute.finalAE);
        }
    }
    /// <summary>
    /// 怪物主技能
    /// </summary>
    public float baseValue_53
    {
        get
        {
            return (float)(charAttribute.finalSkill);
        }
    }
    /// <summary>
    /// 怪物次技能
    /// </summary>
    public float baseValue_54
    {
        get
        {
            return (float)(charAttribute.finalMinorSkill);
        }
    }
    /// <summary>
    /// 怪物回盾
    /// </summary>
    public float baseValue_55
    {
        get
        {
            return (float)(charAttribute.finalShield);
        }
    }
    /// <summary>
    /// 怪物回甲
    /// </summary>
    public float baseValue_56
    {
        get
        {
            return (float)(charAttribute.finalArmor);
        }
    }
    /// <summary>
    /// 怪物狂暴
    /// </summary>
    public float baseValue_57
    {
        get
        {
            return (float)(state_template.stateCoeSet[0]);
        }
    }
    /// <summary>
    /// 保护
    /// </summary>
    public float baseValue_58
    {
        get
        {
            return (float)(state_template.stateCoeSet[0]);
        }
    }
    /// <summary>
    /// 公式枚举
    /// </summary>
    public int bvFormula;
    /// <summary>
    /// 基础值
    /// </summary>
    public float baseValue
    {
        get
        {

            switch (bvFormula)
            {
                case 1:
                    return baseValue_1;
                case 2:
                    return baseValue_2;
                case 3:
                    return baseValue_3;
                case 4:
                    return baseValue_4;
                case 5:
                    return baseValue_5;
                case 6:
                    return baseValue_6;
                case 7:
                    return baseValue_7;
                case 8:
                    return baseValue_8;
                case 9:
                    return baseValue_9;
                case 10:
                    return baseValue_10;
                case 11:
                    return baseValue_11;
                case 12:
                    return baseValue_12;
                case 13:
                    return baseValue_13;
                case 14:
                    return baseValue_14;
                case 15:
                    return baseValue_15;
                case 16:
                    return baseValue_16;
                case 17:
                    return baseValue_17;
                case 18:
                    return baseValue_18;
                case 19:
                    return baseValue_19;
                case 20:
                    return baseValue_51;
                case 21:
                    return baseValue_52;
                case 22:
                    return baseValue_53;
                case 23:
                    return baseValue_54;
                case 24:
                    return baseValue_55;
                case 25:
                    return baseValue_56;
                case 26:
                    return baseValue_57;
                case 27:
                    return baseValue_58;
                default:
                    return 0;
            }
        }
    }
    /// <summary>
    /// 最终值
    /// </summary>
    public float finalValue
    {
        get
        {

            switch (bvFormula)
            {
                case 1:
                    return baseValue * rndRate;
                case 2:
                    return baseValue * rndRate;
                case 3:
                    return baseValue * rndRate;
                case 4:
                    return baseValue * rndRate;
                case 5:
                    return baseValue * rndRate;
                case 6:
                    return baseValue * rndRate;
                case 7:
                    return baseValue * rndRate;
                case 8:
                    return baseValue * rndRate;
                case 9:
                    return baseValue * rndRate;
                case 10:
                    return baseValue * rndRate;
                case 11:
                    return baseValue * rndRate;
                case 12:
                    return baseValue;
                case 13:
                    return baseValue;
                case 14:
                    return baseValue;
                case 15:
                    return baseValue * rndRate;
                case 16:
                    return baseValue * rndRate;
                case 17:
                    return baseValue * rndRate;
                case 18:
                    return baseValue * rndRate;
                case 19:
                    return baseValue * rndRate;
                case 20:
                    return baseValue * rndRate;
                case 21:
                    return baseValue * rndRate;
                case 22:
                    return baseValue * rndRate;
                case 23:
                    return baseValue * rndRate;
                case 24:
                    return baseValue;
                case 25:
                    return baseValue;
                case 26:
                    return baseValue;
                case 27:
                    return baseValue;
                default:
                    return 0;
            }
        }
    }
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempDamageAbsorb;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPeriodShield;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempShield;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPeriodArmor;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempArmor;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPeriodHP;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempDamageBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPhysicalDB;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSpellDB;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempDamageTaken;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPhysicalTaken;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSpellTaken;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPhysicaReduction;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSpellReduction;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempShieldReflect;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempArmorReflect;
    /// <summary>
    /// 吸取值
    /// </summary>
    public float drainValue;

}
