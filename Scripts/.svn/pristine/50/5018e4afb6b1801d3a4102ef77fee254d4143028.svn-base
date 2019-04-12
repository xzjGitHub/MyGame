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
            return (float)(((charAttribute.finalSkill * 11 / 6 * 4 / 3) * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalArmorBonus) + charAttribute.preAP * (1 + charAttribute.fvDB + charAttribute.finalArmorBonus)) * 0.75);
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
            return (float)(((charAttribute.finalSkill * 11 / 6) * (1 + charAttribute.fvDB + charAttribute.finalSkillPB + charAttribute.finalSPBonus) + charAttribute.preSP * (1 + charAttribute.fvDB + charAttribute.finalSPBonus)) * 0.75);
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
