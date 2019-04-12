using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// SkillAttribute
/// </summary>
public partial class SkillAttribute
{
    /// <summary>
    /// 角色属性
    /// </summary>
    public CharAttribute charAttribute;
    /// <summary>
    /// 命中模板
    /// </summary>
    public Targetset_template targetset_template;
    /// <summary>
    /// 目标属性
    /// </summary>
    public CharAttribute targetAttribute;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempEND;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempHPBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempHPRcy;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempHPRcyBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempShield;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempShieldBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSHI;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempShieldRcy;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempShieldRcyBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempARM;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempDRBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempBLO;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempBVBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempAP;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempDMGBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempHLBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPRE;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempPrecisionBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempCRT;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempCVBonus;
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSkillDB1
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSkillDB2
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSkillHB1
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSkillHB2
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSkillDR1
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 目标属性
    /// </summary>
    public float tempSkillDR2
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 技能护盾
    /// </summary>
    public float fv_shield
    {
        get
        {
            return (float)10;
        }
    }
    /// <summary>
    /// 技能伤害_fv
    /// </summary>
    public float fv_damage
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 技能治疗_fv
    /// </summary>
    public float fv_healing
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 技能伤害_rv
    /// </summary>
    public float rv_damage
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 技能治疗_rv
    /// </summary>
    public float rv_healing
    {
        get
        {
            return (float)0;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public float combatBonus
    {
        get
        {
            return (float)0;
        }
    }

}
