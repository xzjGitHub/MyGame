using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// MobAttribute
/// </summary>
public partial class MobAttribute
{
    /// <summary>
    /// 附加生存成长
    /// </summary>
    public override float finalUpgradeDef
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,upgradeDef)));
        }
    }
    /// <summary>
    /// 附加伤害成长
    /// </summary>
    public override float finalUpgradeOff
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,upgradeOff)));
        }
    }
    /// <summary>
    /// 角色基本生命
    /// </summary>
    public override float baseCharHP
    {
        get
        {
            return (float)(mob_template.baseHP * finalUpgradeDef);
        }
    }
    /// <summary>
    /// 角色基本伤害
    /// </summary>
    public override float baseCharAttack
    {
        get
        {
            return (float)(mob_template.baseAttack * finalUpgradeOff);
        }
    }
    /// <summary>
    /// 角色基本技能
    /// </summary>
    public override float baseCharSkill
    {
        get
        {
            return (float)(0);
        }
    }

    /// <summary>
    /// 最终生命
    /// </summary>
    public override float finalHP
    {
        get
        {
            return (float)(charHP);
        }
    }
    /// <summary>
    /// 最终物伤
    /// </summary>
    public override float finalAP
    {
        get
        {
            return (float)(charAttack);
        }
    }
    /// <summary>
    /// 最终法伤
    /// </summary>
    public override float finalSP
    {
        get
        {
            return (float)(charAttack);
        }
    }
    /// <summary>
    /// 最终技能
    /// </summary>
    public override float finalSkill
    {
        get
        {
            return (float)(charSkill);
        }
    }
    /// <summary>
    /// 最终回能
    /// </summary>
    public override float finalEnergyReg
    {
        get
        {
            return (float)(charEnergyReg);
        }
    }
    /// <summary>
    /// 角色护盾
    /// </summary>
    public override float charShield
    {
        get
        {
            return (float)(mob_template.baseShield * finalUpgradeDef);
        }
    }
    /// <summary>
    /// 角色护甲
    /// </summary>
    public override float charArmor
    {
        get
        {
            return (float)(mob_template.baseArmor * finalUpgradeDef);
        }
    }
    /// <summary>
    /// 最终护盾
    /// </summary>
    public override float finalShield
    {
        get
        {
            return (float)(charShield);
        }
    }
    /// <summary>
    /// 最终护甲
    /// </summary>
    public override float finalArmor
    {
        get
        {
            return (float)(charArmor);
        }
    }
    /// <summary>
    /// 最终回盾
    /// </summary>
    public override float finalShieldReg
    {
        get
        {
            return (float)(mob_template.baseShieldReg);
        }
    }
    /// <summary>
    /// 最终回甲
    /// </summary>
    public override float finalArmorReg
    {
        get
        {
            return (float)(mob_template.baseArmorReg);
        }
    }
    /// <summary>
    /// 复活后的生命值
    /// </summary>
    public override float HPRevive
    {
        get
        {
            return (float)(char_config.reviveProp * finalHP);
        }
    }
}
