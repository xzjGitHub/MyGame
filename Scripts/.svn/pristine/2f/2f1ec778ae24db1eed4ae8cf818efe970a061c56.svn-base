using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// EquipAttribute
/// </summary>
public partial class EquipAttribute
{
    /// <summary>
    /// 游戏配置
    /// </summary>
    public Game_config game_config;
    /// <summary>
    /// 人员管理配置
    /// </summary>
    public HR_config hr_config;
    /// <summary>
    /// 物品模板
    /// </summary>
    public Item_instance item_instance;
    /// <summary>
    /// 装备模板
    /// </summary>
    public Equip_template equip_template;
    /// <summary>
    /// 装备研究表
    /// </summary>
    public ER_template er_template;
    /// <summary>
    /// 装备随机实例
    /// </summary>
    public EquipRnd equipRnd;
    /// <summary>
    /// 附魔属性
    /// </summary>
    public EnchantRnd enchantRnd;
    /// <summary>
    /// 装备生命
    /// </summary>
    public float tempHP
    {
        get
        {
            return (float)(equipRnd.tempHP);
        }
    }
    /// <summary>
    /// 装备护盾
    /// </summary>
    public float tempShield
    {
        get
        {
            return (float)(equipRnd.tempShield + equipRnd.rndShield1);
        }
    }
    /// <summary>
    /// 装备护甲
    /// </summary>
    public float tempArmor
    {
        get
        {
            return (float)(equipRnd.tempArmor + equipRnd.addArmor + equipRnd.rndArmor1);
        }
    }
    /// <summary>
    /// 装备伤害
    /// </summary>
    public float tempAttack
    {
        get
        {
            return (float)(equipRnd.tempAttack);
        }
    }
    /// <summary>
    /// 装备技能
    /// </summary>
    public float tempSkill
    {
        get
        {
            return (float)(equipRnd.tempSkill);
        }
    }
    /// <summary>
    /// 装备物攻
    /// </summary>
    public float tempAP
    {
        get
        {
            return (float)(equipRnd.tempAP + equipRnd.rndAP1);
        }
    }
    /// <summary>
    /// 装备法伤
    /// </summary>
    public float tempSP
    {
        get
        {
            return (float)(equipRnd.tempSP + equipRnd.rndSP1);
        }
    }
    /// <summary>
    /// 装备回能
    /// </summary>
    public float tempEnergyReg
    {
        get
        {
            return (float)(equipRnd.tempEnergyReg + equipRnd.rndEnergyReg1);
        }
    }
    /// <summary>
    /// 装备回盾
    /// </summary>
    public float tempShieldReg
    {
        get
        {
            return (float)(equipRnd.rndShieldReg1);
        }
    }
    /// <summary>
    /// 装备回甲
    /// </summary>
    public float tempArmorReg
    {
        get
        {
            return (float)(equipRnd.rndArmorReg1);
        }
    }
    /// <summary>
    /// 护盾伤害比例
    /// </summary>
    public float tempShieldDB
    {
        get
        {
            return (float)(equipRnd.tempShieldDB);
        }
    }
    /// <summary>
    /// 护甲伤害比例
    /// </summary>
    public float tempArmorDB
    {
        get
        {
            return (float)(equipRnd.tempArmorDB);
        }
    }
    /// <summary>
    /// 生命伤害比例
    /// </summary>
    public float tempHPDB
    {
        get
        {
            return (float)(equipRnd.tempHPDB);
        }
    }
    /// <summary>
    /// 伤害增幅
    /// </summary>
    public float fvDB
    {
        get
        {
            return (float)(equipRnd.fvDB);
        }
    }
    /// <summary>
    /// 物攻增幅
    /// </summary>
    public float tempAPBonus
    {
        get
        {
            return (float)(0);
        }
    }
    /// <summary>
    /// 法伤增幅
    /// </summary>
    public float tempSPBonus
    {
        get
        {
            return (float)(0);
        }
    }
    /// <summary>
    /// 护盾增幅
    /// </summary>
    public float tempShieldBonus
    {
        get
        {
            return (float)(0);
        }
    }
    /// <summary>
    /// 护甲增幅
    /// </summary>
    public float tempArmorBonus
    {
        get
        {
            return (float)(0);
        }
    }
    /// <summary>
    /// 生命增幅
    /// </summary>
    public float tempHPBonus
    {
        get
        {
            return (float)(equipRnd.tempHPBonus);
        }
    }
    /// <summary>
    /// 复活强度
    /// </summary>
    public float revivePower
    {
        get
        {
            return (float)(equipRnd.revivePower);
        }
    }
    /// <summary>
    /// 装备额外物攻
    /// </summary>
    public float addPDMG
    {
        get
        {
            return (float)(equipRnd.rndPDMG1);
        }
    }
    /// <summary>
    /// 装备额外法伤
    /// </summary>
    public float addSDMG
    {
        get
        {
            return (float)(equipRnd.rndSDMG1);
        }
    }
    /// <summary>
    /// 装备额外血伤
    /// </summary>
    public float addHPDMG
    {
        get
        {
            return (float)(equipRnd.rndHPDMG1);
        }
    }
    /// <summary>
    /// 装备额外真伤
    /// </summary>
    public float addTrueDMG
    {
        get
        {
            return (float)(equipRnd.rndTrueDMG1);
        }
    }
    /// <summary>
    /// 附魔物攻增幅
    /// </summary>
    public float enchantAPBonus
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantAPBonus));
        }
    }
    /// <summary>
    /// 附魔法伤增幅
    /// </summary>
    public float enchantSPBonus
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantSPBonus));
        }
    }
    /// <summary>
    /// 附魔技能增幅
    /// </summary>
    public float enchantSkillPB
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantSkillPB));
        }
    }
    /// <summary>
    /// 附魔护盾增伤
    /// </summary>
    public float enchantShieldDB
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantShieldDB));
        }
    }
    /// <summary>
    /// 附魔护甲增伤
    /// </summary>
    public float enchantArmorDB
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantArmorDB));
        }
    }
    /// <summary>
    /// 附魔生命增伤
    /// </summary>
    public float enchantHPDB
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantHPDB));
        }
    }
    /// <summary>
    /// 附魔护盾增幅
    /// </summary>
    public float enchantShieldBonus
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantShieldBonus));
        }
    }
    /// <summary>
    /// 附魔护甲增幅
    /// </summary>
    public float enchantArmorBonus
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantArmorBonus));
        }
    }
    /// <summary>
    /// 附魔生命增幅
    /// </summary>
    public float enchantHPBonus
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantHPBonus));
        }
    }
    /// <summary>
    /// 参与装备研究的角色数
    /// </summary>
    public int ERChar ;
    /// <summary>
    /// 装备基础研究经验
    /// </summary>
    public float ERExpReward;
    /// <summary>
    /// 装备最终研究经验
    /// </summary>
    public float dailyERExp  
    {
        get
        {
            return (float)((ERExpReward / game_config.productionCycle) * RandomBuilder.RandomNum((1 + game_config.researchDeviation),(1 - game_config.researchDeviation)) * (1 + ERChar * hr_config.researchBonus));
        }
    }
    /// <summary>
    /// 装备购买价格
    /// </summary>
    public float finalPurchasePrice
    {
        get
        {
            return (float)(item_instance.basePurchasePrice);
        }
    }
    /// <summary>
    /// 物品出售价格
    /// </summary>
    public float finalSellPrice
    {
        get
        {
            return (float)(item_instance.baseSellPrice);
        }
    }

}
