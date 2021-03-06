using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// EnchantRnd
/// </summary>
public partial class EnchantRnd
{
    /// <summary>
    /// 附魔模板
    /// </summary>
    public Enchant_template enchant_template;
    /// <summary>
    /// 附魔品质
    /// </summary>
    public float equipQuality
    {
        get
        {
            return (float)(upgradeAll);
        }
    }
    /// <summary>
    /// 附魔等级加值
    /// </summary>
    public float enchantItemLevel
    {
        get
        {
            return (float)(finalItemLevel);
        }
    }
    /// <summary>
    /// 附魔位阶
    /// </summary>
    public int enchantRank
    {
        get
        {
            return (int)((finalItemLevel / 5) * enchant_template.baseEquipRank[1] + enchant_template.baseEquipRank[0]);
        }
    }
    /// <summary>
    /// 附魔物攻增幅
    /// </summary>
    public float enchantAPBonus
    {
        get
        {
            return (float)(enchant_template.APBonus);
        }
    }
    /// <summary>
    /// 附魔法伤增幅
    /// </summary>
    public float enchantSPBonus
    {
        get
        {
            return (float)(enchant_template.SPBonus);
        }
    }
    /// <summary>
    /// 附魔技能增幅
    /// </summary>
    public float enchantSkillPB
    {
        get
        {
            return (float)(enchant_template.SkillPB);
        }
    }
    /// <summary>
    /// 附魔护盾增伤
    /// </summary>
    public float enchantShieldDB
    {
        get
        {
            return (float)(enchant_template.ShieldDB);
        }
    }
    /// <summary>
    /// 附魔护甲增伤
    /// </summary>
    public float enchantArmorDB
    {
        get
        {
            return (float)(enchant_template.ArmorDB);
        }
    }
    /// <summary>
    /// 附魔生命增伤
    /// </summary>
    public float enchantHPDB
    {
        get
        {
            return (float)(enchant_template.HPDB);
        }
    }
    /// <summary>
    /// 附魔护盾增幅
    /// </summary>
    public float enchantShieldBonus
    {
        get
        {
            return (float)(enchant_template.ShieldBonus);
        }
    }
    /// <summary>
    /// 附魔护甲增幅
    /// </summary>
    public float enchantArmorBonus
    {
        get
        {
            return (float)(enchant_template.ArmorBonus);
        }
    }
    /// <summary>
    /// 附魔生命增幅
    /// </summary>
    public float enchantHPBonus
    {
        get
        {
            return (float)(enchant_template.HPBonus);
        }
    }

}
