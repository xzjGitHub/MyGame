using System;
using System.Linq;



/// <summary>
/// CharAttribute
/// </summary>
public partial class CharAttribute
{
    /// <summary>
    /// 角色位阶增幅
    /// </summary>
    public virtual float charRankBonus
    {
        get
        {
            return char_config.charRankBonus * charRank;
        }
    }
    /// <summary>
    /// 强化中位数
    /// </summary>
    public virtual float midUP
    {
        get
        {
            return char_template.midUP;
        }
    }
    /// <summary>
    /// 附加生存成长
    /// </summary>
    public virtual float finalUpgradeDef
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate, upgradeDef)));
        }
    }
    /// <summary>
    /// 附加伤害成长
    /// </summary>
    public virtual float finalUpgradeOff
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate, upgradeOff)));
        }
    }
    /// <summary>
    /// 技能成长
    /// </summary>
    public virtual float finalMidUP
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate, midUP)));
        }
    }
    /// <summary>
    /// 伤害成长
    /// </summary>
    public virtual float finalDMGRate
    {
        get
        {
            return (float)((Math.Pow((11 / 10), (charLevel / 3)) + char_config.lvTEHP * charLevel / char_config.TankEHP));
        }
    }
    /// <summary>
    /// 角色基本生命
    /// </summary>
    public virtual float baseCharHP
    {
        get
        {
            return char_template.baseHP * finalUpgradeDef * (1 + charRankBonus);
        }
    }
    /// <summary>
    /// 角色基本伤害
    /// </summary>
    public virtual float baseCharAttack
    {
        get
        {
            return char_template.baseAttack * finalUpgradeOff * (1 + charRankBonus);
        }
    }
    /// <summary>
    /// 角色基本技能
    /// </summary>
    public virtual float baseCharSkill
    {
        get
        {
            return char_template.baseSkill * finalMidUP * (1 + charRankBonus);
        }
    }
    /// <summary>
    /// 角色技能（不随机）
    /// </summary>
    public virtual float charSkill
    {
        get
        {
            return char_template.baseSkill * finalDMGRate * finalMidUP * (1 + charRankBonus);
        }
    }
    /// <summary>
    /// 角色回能
    /// </summary>
    public virtual float charEnergyReg
    {
        get
        {
            return char_template.baseEnergyReg;
        }
    }
    /// <summary>
    /// 临时生命
    /// </summary>
    public virtual float accumHP
    {
        get
        {
            return equipAttribute.Sum(a => a.tempHP);
        }
    }
    /// <summary>
    /// 临时伤害
    /// </summary>
    public virtual float accumAttack
    {
        get
        {
            return equipAttribute.Sum(a => a.tempAttack);
        }
    }
    /// <summary>
    /// 临时物伤
    /// </summary>
    public virtual float accumAP
    {
        get
        {
            return equipAttribute.Sum(a => a.tempAP);
        }
    }
    /// <summary>
    /// 临时法伤
    /// </summary>
    public virtual float accumSP
    {
        get
        {
            return equipAttribute.Sum(a => a.tempSP);
        }
    }
    /// <summary>
    /// 临时技能
    /// </summary>
    public virtual float accumSkill
    {
        get
        {
            return equipAttribute.Sum(a => a.tempSkill);
        }
    }
    /// <summary>
    /// 临时回能
    /// </summary>
    public virtual float accumEnergyReg
    {
        get
        {
            return equipAttribute.Sum(a => a.tempEnergyReg);
        }
    }
    /// <summary>
    /// 修正前生命
    /// </summary>
    public virtual float preHP
    {
        get
        {
            return (charHP + accumHP);
        }
    }
    /// <summary>
    /// 修正前物攻
    /// </summary>
    public virtual float preAP
    {
        get
        {
            return (charAttack + accumAttack + accumAP);
        }
    }
    /// <summary>
    /// 修正前法攻
    /// </summary>
    public virtual float preSP
    {
        get
        {
            return (charAttack + accumAttack + accumSP);
        }
    }
    /// <summary>
    /// 修正前技能
    /// </summary>
    public virtual float preSkill
    {
        get
        {
            return (charSkill + accumSkill);
        }
    }
    /// <summary>
    /// 最终生命
    /// </summary>
    public virtual float finalHP
    {
        get
        {
            return (charHP + accumHP) * (1 + finalHPBonus);
        }
    }
    /// <summary>
    /// 最终物伤
    /// </summary>
    public virtual float finalAP
    {
        get
        {
            return (charAttack + accumAttack + accumAP) * (1 + finalAPBonus);
        }
    }
    /// <summary>
    /// 最终法伤
    /// </summary>
    public virtual float finalSP
    {
        get
        {
            return (charAttack + accumAttack + accumSP) * (1 + finalSPBonus);
        }
    }
    /// <summary>
    /// 最终技能
    /// </summary>
    public virtual float finalSkill
    {
        get
        {
            return (charSkill + accumSkill);
        }
    }
    /// <summary>
    /// 最终回能
    /// </summary>
    public virtual float finalEnergyReg
    {
        get
        {
            return charEnergyReg + accumEnergyReg;
        }
    }
    /// <summary>
    /// 角色护盾
    /// </summary>
    public virtual float charShield
    {
        get
        {
            return 0;
        }
    }
    /// <summary>
    /// 角色护甲
    /// </summary>
    public virtual float charArmor
    {
        get
        {
            return 0;
        }
    }
    /// <summary>
    /// 临时护盾
    /// </summary>
    public virtual float accumShield
    {
        get
        {
            return equipAttribute.Sum(a => a.tempShield);
        }
    }
    /// <summary>
    /// 临时护甲
    /// </summary>
    public virtual float accumArmor
    {
        get
        {
            return equipAttribute.Sum(a => a.tempArmor);
        }
    }
    /// <summary>
    /// 修正前护盾
    /// </summary>
    public virtual float preShield
    {
        get
        {
            return accumShield;
        }
    }
    /// <summary>
    /// 修正前护甲
    /// </summary>
    public virtual float preArmor
    {
        get
        {
            return accumArmor;
        }
    }
    /// <summary>
    /// 最终护盾
    /// </summary>
    public virtual float finalShield
    {
        get
        {
            return accumShield * (1 + finalShieldBonus);
        }
    }
    /// <summary>
    /// 最终护甲
    /// </summary>
    public virtual float finalArmor
    {
        get
        {
            return accumArmor * (1 + finalArmorBonus);
        }
    }
    /// <summary>
    /// 最终回盾
    /// </summary>
    public virtual float finalShieldReg
    {
        get
        {
            return equipAttribute.Sum(a => a.tempShieldReg) * (1 + finalShieldBonus);
        }
    }
    /// <summary>
    /// 最终回甲
    /// </summary>
    public virtual float finalArmorReg
    {
        get
        {
            return equipAttribute.Sum(a => a.tempArmorReg) * (1 + finalArmorBonus);
        }
    }
    /// <summary>
    /// 额外物攻
    /// </summary>
    public virtual float addPDMG
    {
        get
        {
            return equipAttribute.Sum(a => a.addPDMG);
        }
    }
    /// <summary>
    /// 额外法伤
    /// </summary>
    public virtual float addSDMG
    {
        get
        {
            return equipAttribute.Sum(a => a.addSDMG);
        }
    }
    /// <summary>
    /// 额外血伤
    /// </summary>
    public virtual float addHPDMG
    {
        get
        {
            return equipAttribute.Sum(a => a.addHPDMG);
        }
    }
    /// <summary>
    /// 额外真伤
    /// </summary>
    public virtual float addTrueDMG
    {
        get
        {
            return equipAttribute.Sum(a => a.addTrueDMG);
        }
    }
    /// <summary>
    /// 攻击增幅
    /// </summary>
    public virtual float fvDB
    {
        get
        {
            return equipAttribute.Sum(a => a.fvDB) + selectionFvDB;
        }
    }

    /// <summary>
    /// 复活后的生命值
    /// </summary>
    public virtual float HPRevive
    {
        get
        {
            return char_config.reviveProp * finalHP;
        }
    }
    /// <summary>
    /// 物攻强化
    /// </summary>
    public virtual float finalAPBonus
    {
        get
        {
            return equipAttribute.Sum(a => a.tempAPBonus) + equipAttribute.Sum(a => a.enchantAPBonus);
        }
    }
    /// <summary>
    /// 物攻法伤强化
    /// </summary>
    public virtual float finalSPBonus
    {
        get
        {
            return equipAttribute.Sum(a => a.tempSPBonus) + equipAttribute.Sum(a => a.enchantSPBonus);
        }
    }
    /// <summary>
    /// 技能强化
    /// </summary>
    public virtual float finalSkillPB
    {
        get
        {
            return equipAttribute.Sum(a => a.enchantSkillPB);
        }
    }
    /// <summary>
    /// 护盾伤害
    /// </summary>
    public virtual float finalShieldDB
    {
        get
        {
            return equipAttribute.Sum(a => a.tempShieldDB) + equipAttribute.Sum(a => a.enchantShieldDB);
        }
    }
    /// <summary>
    /// 护甲伤害
    /// </summary>
    public virtual float finalArmorDB
    {
        get
        {
            return equipAttribute.Sum(a => a.tempArmorDB) + equipAttribute.Sum(a => a.enchantArmorDB);
        }
    }
    /// <summary>
    /// 生命伤害
    /// </summary>
    public virtual float finalHPDB
    {
        get
        {
            return equipAttribute.Sum(a => a.tempHPDB) + equipAttribute.Sum(a => a.enchantHPDB);
        }
    }
    /// <summary>
    /// 护盾增幅
    /// </summary>
    public virtual float finalShieldBonus
    {
        get
        {
            return equipAttribute.Sum(a => a.tempShieldBonus) + equipAttribute.Sum(a => a.enchantShieldBonus);
        }
    }
    /// <summary>
    /// 护甲增幅
    /// </summary>
    public virtual float finalArmorBonus
    {
        get
        {
            return equipAttribute.Sum(a => a.tempArmorBonus) + equipAttribute.Sum(a => a.enchantArmorBonus);
        }
    }
    /// <summary>
    /// 生命增幅
    /// </summary>
    public virtual float finalHPBonus
    {
        get
        {
            return equipAttribute.Sum(a => a.tempHPBonus) + equipAttribute.Sum(a => a.enchantHPBonus);
        }
    }
    /// <summary>
    /// 怪物平击
    /// </summary>
    public virtual float finalAttack
    {
        get
        {
            return 0;
        }
    }
    /// <summary>
    /// 怪物群攻
    /// </summary>
    public virtual float finalAE
    {
        get
        {
            return 0;
        }
    }
    /// <summary>
    /// 怪物次技能
    /// </summary>
    public virtual float finalMinorSkill
    {
        get
        {
            return 0;
        }
    }

}
