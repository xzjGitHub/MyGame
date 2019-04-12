using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// EquipRnd
/// </summary>
public partial class EquipRnd
{
    /// <summary>
    /// 角色设置
    /// </summary>
    public Char_config char_config;
    /// <summary>
    /// 装备设置
    /// </summary>
    public Equip_config equip_config;
    /// <summary>
    /// 战斗设置
    /// </summary>
    public Combat_config combat_config;
    /// <summary>
    /// 物品模板
    /// </summary>
    public Item_instance item_instance;
    /// <summary>
    /// 装备模板
    /// </summary>
    public Equip_template equip_template;
    /// <summary>
    /// 附魔属性
    /// </summary>
    public EnchantRnd enchantRnd;
    /// <summary>
    /// 附魔物品等级加值
    /// </summary>
    public float enchantItemLevel
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.enchantItemLevel));
        }
    }
    /// <summary>
    /// 装备等级成长阶乘
    /// </summary>
    public float LvRatePower
    {
        get
        {
            return (float)(((finalItemLevel + enchantItemLevel) / 2f));
        }
    }
    /// <summary>
    /// 装备等级成长
    /// </summary>
    public float finalLvRate
    {
        get
        {
            return (float)((Math.Pow(char_config.lvRate,LvRatePower)));
        }
    }
    /// <summary>
    /// 装备攻击成长阶乘
    /// </summary>
    public float DMGRatePower
    {
        get
        {
            return (float)(((finalItemLevel + enchantItemLevel) / 2f / 3f));
        }
    }
    /// <summary>
    /// 伤害成长，使用(finalItemLevel+enchantItemLevel)/2
    /// </summary>
    public float finalDMGRate
    {
        get
        {
            return (float)((Math.Pow((11 / 10f),DMGRatePower) + char_config.lvTEHP * (finalItemLevel + enchantItemLevel) / 2f / char_config.TankEHP));
        }
    }
    /// <summary>
    /// 附魔强化_全局
    /// </summary>
    public float enchantUpgradeAll
    {
        get
        {
            return (float)(enchantRnd==null?0:(enchantRnd.upgradeAll));
        }
    }
    /// <summary>
    /// 强化中位数
    /// </summary>
    public float midUP
    {
        get
        {
            return (float)(equip_template.midUP);
        }
    }
    /// <summary>
    /// 附加全局成长
    /// </summary>
    public float finalUpgradeAll
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,upgradeAll)));
        }
    }
    /// <summary>
    /// 附加全局成长2
    /// </summary>
    public float finalUpgradeAll2
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,upgradeAll2)));
        }
    }
    /// <summary>
    /// 附加随机成长
    /// </summary>
    public float finalUpgradeRnd
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,upgradeRnd)));
        }
    }
    /// <summary>
    /// 附加随机成长2
    /// </summary>
    public float finalUpgradeRnd2
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,upgradeRnd2)));
        }
    }
    /// <summary>
    /// 附魔成长
    /// </summary>
    public float finalEnchantUPG
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,enchantUpgradeAll)));
        }
    }
    /// <summary>
    /// 中位数成长
    /// </summary>
    public float finalMidUP
    {
        get
        {
            return (float)((Math.Pow(char_config.upgradeRate,midUP)));
        }
    }
    /// <summary>
    /// 装备品质
    /// </summary>
    public float equipQuality
    {
        get
        {
            return (float)(upgradeAll * equip_template.UGProp + upgradeRnd * equip_template.UGProp1 + upgradeRnd * equip_template.UGProp2);
        }
    }
    /// <summary>
    /// 装备位阶
    /// </summary>
    public int equipRank
    {
        get
        {
            return (int)((finalItemLevel / 5f) * equip_template.baseEquipRank[1] + equip_template.baseEquipRank[0]);
        }
    }
    /// <summary>
    /// 装备生命
    /// </summary>
    public float tempHP
    {
        get
        {
            return (float)((equip_template.tempHP * finalLvRate + (equip_template.tempHP>0?((finalItemLevel + enchantItemLevel) / 2) * equip_template.rndLvHP:0)) * (1 + equipRankBonus) / 2 * (finalUpgradeRnd + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 装备护盾
    /// </summary>
    public float tempShield
    {
        get
        {
            return (float)((equip_template.tempShield * finalLvRate + (equip_template.tempShield>0?((finalItemLevel + enchantItemLevel) / 2) * equip_template.addLvDef:0)) * (1 + equipRankBonus) / 2 * (finalUpgradeAll + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 装备护甲
    /// </summary>
    public float tempArmor
    {
        get
        {
            return (float)((equip_template.tempArmor * finalLvRate + (equip_template.tempArmor>0?((finalItemLevel + enchantItemLevel) / 2) * equip_template.addLvDef:0)) * (1 + equipRankBonus) / 2 * (finalUpgradeAll + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 额外护甲_半甲
    /// </summary>
    public float addArmor
    {
        get
        {
            return (float)((equip_template.addArmor * finalLvRate + (equip_template.addArmor>0?((finalItemLevel + enchantItemLevel) / 2) * equip_template.addLvDef:0)) * (1 + equipRankBonus) / 2 * (finalUpgradeAll2 + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 装备伤害
    /// </summary>
    public float tempAttack
    {
        get
        {
            return (float)((equip_template.tempAttack * finalDMGRate) * (1 + equipRankBonus) / 2f * (finalUpgradeAll + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 装备物攻
    /// </summary>
    public float tempAP
    {
        get
        {
            return (float)((equip_template.tempAP * finalDMGRate) * (1 + equipRankBonus) / 2f * (finalUpgradeAll + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 装备法攻
    /// </summary>
    public float tempSP
    {
        get
        {
            return (float)((equip_template.tempSP * finalDMGRate) * (1 + equipRankBonus) / 2f * (finalUpgradeAll + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 装备技能
    /// </summary>
    public float tempSkill
    {
        get
        {
            return (float)((equip_template.tempSkill * finalDMGRate) * (1 + equipRankBonus) / 2f * (finalUpgradeRnd + finalEnchantUPG));
        }
    }
    /// <summary>
    /// 装备回能
    /// </summary>
    public float tempEnergyReg
    {
        get
        {
            return (float)(equip_template.tempEnergyReg);
        }
    }
    /// <summary>
    /// 武器最低伤害
    /// </summary>
    public float minDMG
    {
        get
        {
            return (float)(tempAttack * (1 - combat_config.DMGDev));
        }
    }
    /// <summary>
    /// 武器最高伤害
    /// </summary>
    public float maxDMG
    {
        get
        {
            return (float)(tempAttack * (1 + combat_config.DMGDev));
        }
    }
    /// <summary>
    /// 护盾伤害比例
    /// </summary>
    public float tempShieldDB
    {
        get
        {
            return (float)(equip_template.shieldDB);
        }
    }
    /// <summary>
    /// 护甲伤害比例
    /// </summary>
    public float tempArmorDB
    {
        get
        {
            return (float)(equip_template.armorDB);
        }
    }
    /// <summary>
    /// 生命伤害比例
    /// </summary>
    public float tempHPDB
    {
        get
        {
            return (float)(equip_template.HPDB);
        }
    }
    /// <summary>
    /// 伤害增幅
    /// </summary>
    public float fvDB
    {
        get
        {
            return (float)(equip_template.fvDB);
        }
    }
    /// <summary>
    /// 生命增幅
    /// </summary>
    public float tempHPBonus
    {
        get
        {
            return (float)(equip_template.HPBonus);
        }
    }
    /// <summary>
    /// 复活强度
    /// </summary>
    public float revivePower
    {
        get
        {
            return (float)((equip_template.revivePower * finalLvRate + (equip_template.revivePower>0?finalItemLevel * equip_template.rndLvHP:0)) * finalMidUP * (1 + equipRankBonus));
        }
    }
    /// <summary>
    /// 随机等级成长
    /// </summary>
    public float rndLvRate
    {
        get
        {
            return (float)((Math.Pow(char_config.lvRate,rndItemLevel)));
        }
    }
    /// <summary>
    /// 伤害成长，随机，使用finalItemLevel
    /// </summary>
    public float rndDMGRate
    {
        get
        {
            return (float)((Math.Pow((11 / 10f),(finalItemLevel / 3f)) + char_config.lvTEHP * finalItemLevel / char_config.TankEHP));
        }
    }
    /// <summary>
    /// 随机护甲
    /// </summary>
    public float rndShield1
    {
        get
        {
            if (!_isrndShield1) return 0;
            return (float)((equip_template.rndDef * rndLvRate + finalItemLevel * equip_template.rndLvDef) * finalUpgradeRnd2 * (1 + equipRankBonus));
        }
    }
    /// <summary>
    /// 随机护甲是否使用
    /// </summary>
    public bool isrndShield1 { get { return _isrndShield1; } }
    private bool _isrndShield1;
    /// <summary>
    /// 随机护盾
    /// </summary>
    public float rndArmor1
    {
        get
        {
            if (!_isrndArmor1) return 0;
            return (float)((equip_template.rndDef * rndLvRate + finalItemLevel * equip_template.rndLvDef) * finalUpgradeRnd2 * (1 + equipRankBonus));
        }
    }
    /// <summary>
    /// 随机护盾是否使用
    /// </summary>
    public bool isrndArmor1 { get { return _isrndArmor1; } }
    private bool _isrndArmor1;
    /// <summary>
    /// 随机物攻
    /// </summary>
    public float rndAP1
    {
        get
        {
            if (!_isrndAP1) return 0;
            return (float)((equip_template.rndOff * rndDMGRate) * finalUpgradeRnd2 * (1 + equipRankBonus));
        }
    }
    /// <summary>
    /// 随机物攻是否使用
    /// </summary>
    public bool isrndAP1 { get { return _isrndAP1; } }
    private bool _isrndAP1;
    /// <summary>
    /// 随机法伤
    /// </summary>
    public float rndSP1
    {
        get
        {
            if (!_isrndSP1) return 0;
            return (float)((equip_template.rndOff * rndDMGRate) * finalUpgradeRnd2 * (1 + equipRankBonus));
        }
    }
    /// <summary>
    /// 随机法伤是否使用
    /// </summary>
    public bool isrndSP1 { get { return _isrndSP1; } }
    private bool _isrndSP1;
    /// <summary>
    /// 随机回能
    /// </summary>
    public float rndEnergyReg1
    {
        get
        {
            if (!_isrndEnergyReg1) return 0;
            return (float)(equip_template.rndEnergyReg);
        }
    }
    /// <summary>
    /// 随机回能是否使用
    /// </summary>
    public bool isrndEnergyReg1 { get { return _isrndEnergyReg1; } }
    private bool _isrndEnergyReg1;
    /// <summary>
    /// 随机护盾恢复
    /// </summary>
    public float rndShieldReg1
    {
        get
        {
            if (!_isrndShieldReg1) return 0;
            return (float)((equip_template.rndDef * rndLvRate + finalItemLevel * equip_template.rndLvDef) * finalUpgradeRnd2 * (1 + equipRankBonus) / 4f);
        }
    }
    /// <summary>
    /// 随机护盾恢复是否使用
    /// </summary>
    public bool isrndShieldReg1 { get { return _isrndShieldReg1; } }
    private bool _isrndShieldReg1;
    /// <summary>
    /// 随机护甲恢复
    /// </summary>
    public float rndArmorReg1
    {
        get
        {
            if (!_isrndArmorReg1) return 0;
            return (float)((equip_template.rndDef * rndLvRate + finalItemLevel * equip_template.rndLvDef) * finalUpgradeRnd2 * (1 + equipRankBonus) / 4f);
        }
    }
    /// <summary>
    /// 随机护甲恢复是否使用
    /// </summary>
    public bool isrndArmorReg1 { get { return _isrndArmorReg1; } }
    private bool _isrndArmorReg1;
    /// <summary>
    /// 装备额外物攻
    /// </summary>
    public float rndPDMG1;
    /// <summary>
    /// 装备额外法伤
    /// </summary>
    public float rndSDMG1;
    /// <summary>
    /// 装备额外血伤
    /// </summary>
    public float rndHPDMG1;
    /// <summary>
    /// 装备额外真伤
    /// </summary>
    public float rndTrueDMG1
    {
        get
        {
            if (!_isrndTrueDMG1) return 0;
            return (float)((equip_template.rndOff * rndDMGRate) * finalUpgradeRnd2 * (1 + equipRankBonus) * 0.75);
        }
    }
    /// <summary>
    /// 装备额外真伤是否使用
    /// </summary>
    public bool isrndTrueDMG1 { get { return _isrndTrueDMG1; } }
    private bool _isrndTrueDMG1;
    private List<string> _randomField = new List<string>();
    /// <summary>
    /// 随机属性字段
    /// </summary>
    public List<string> RandomField { get { return _randomField; } }

    /// <summary>
    /// 随机字段
    /// </summary>
    private void RandomFieldOperation(List<string> _list)
    {
        Type t = this.GetType();
        int sum = RandomBuilder.RandomNum(_list.Count - 1);
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i] == null) continue;
            var memberInfo = t.GetField("_is" + _list[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (memberInfo != null) memberInfo.SetValue(this, i == sum);
            if (i == sum) { _randomField.Add(_list[i]); }
        }
    }
    public EquipRnd()
    {
        //随机字段操作
        RandomFieldOperation(equip_template.rndAttribute);
    }
}
