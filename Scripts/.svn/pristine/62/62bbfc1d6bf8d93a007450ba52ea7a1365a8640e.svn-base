using System;
using System.Collections.Generic;
using System.Reflection;


public partial class EquipRnd
{
    /// <summary>
    /// 角色升级
    /// </summary>
    public Char_lvup char_lvup { get { return Char_lvupConfig.GetChar_Lvup((int)finalItemLevel); } }

    public Equip_qualityup equip_qualityup;
    public int finalItemLevel;
    /// <summary>
    /// 物品赋值等级
    /// </summary>
    public int tempItemLevel;
    /// <summary>
    /// 物品等级加值
    /// </summary>
    public int addLevel;
    /// <summary>
    /// 模板ID
    /// </summary>
    public int templateID;
    /// <summary>
    /// 物品品质
    /// </summary>
    public int itemQuality;

    public List<FieldInfo> randomFieldInfos = new List<FieldInfo>();
    /// <summary>
    /// 角色随机强化_生命
    /// </summary>
    public float upgradeDef;
    /// <summary>
    /// 角色随机强化_伤害
    /// </summary>
    public float upgradeOff;

    public float upgradeAll;
    public float upgradeAll2;
    public float upgradeRnd;
    public float upgradeRnd2;

    /// <summary>
    /// 装备位阶增幅
    /// </summary>
    public float equipRankBonus;
    /// <summary>
    /// 随机等级
    /// </summary>
    public int rndItemLevel;


    /// <summary>
    /// 根据存档计算装备属性
    /// </summary>
    /// <param name="data">装备存档</param>
    public EquipRnd(EquipmentData data)
    {
        item_instance = Item_instanceConfig.GetItemInstance(data.instanceID);
        //
        templateID = data.equipTemplateID;
        itemQuality = data.itemQuality;
        addLevel = data.addLevel;
        finalItemLevel = data.itemLevel;
        tempItemLevel = data.tempItemLevel;
        _randomField = data.randomField;
        upgradeOff = data.upgradeOff;
        upgradeDef = data.upgradeDef;
        upgradeAll = data.upgradeAll;
        upgradeAll2 = data.upgradeAll2;
        upgradeRnd = data.upgradeRnd;
        upgradeRnd2 = data.upgradeRnd2;
        //
        equip_template = Equip_templateConfig.GetEquip_template(templateID);
        //
        InitInfo(itemQuality);
        //
        if (_randomField.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < _randomField.Count; i++)
        {
            if (_randomField[i] == "")
            {
                continue;
            }

            FieldInfo memberInfo = CombatSystemTools.GetField(this, "_is" + _randomField[i]);
            if (memberInfo == null)
            {
                continue;
            }

            memberInfo.SetValue(this, true);
        }
    }

    /// <summary>
    ///制造装备
    /// </summary>
    /// <param name="create"></param>
    public EquipRnd(EquimentCreate create)
    {
        upgradeAll = RandomBuilder.Random_Normal(create.maxUpgrade, create.minUpgrade);
        upgradeAll2 = RandomBuilder.Random_Normal(create.maxUpgrade, create.minUpgrade);
        upgradeRnd = RandomBuilder.Random_Normal(create.maxUpgrade, create.minUpgrade);
        upgradeRnd2 = RandomBuilder.Random_Normal(create.maxUpgrade, create.minUpgrade);
        finalItemLevel = create.itemLevel;
        //
        item_instance = Item_instanceConfig.GetItemInstance(create.instanceID);
        templateID = create.equipTemplateID;
        equip_template = Equip_templateConfig.GetEquip_template(templateID);
       // itemQuality = item_instance.itemQuality;
        if (itemQuality == -1)
        {
            itemQuality = RandomBuilder.RandomIndex_Chances(equip_template.qualitySelectChance) + 1;
        }
        //
        InitInfo(itemQuality);
        //
        int maxItemLevel = create.maxItemLevel + item_instance.maxItemLevel;
        //
        tempItemLevel = Math.Min(create.itemLevel, maxItemLevel);
        //
        switch (create.createType)
        {
            case ItemCreateType.Drop:
            case ItemCreateType.Recoin:
                equipRankBonus = char_config.charRankBonus * equipRank;
                rndItemLevel = finalItemLevel;
                break;
            case ItemCreateType.Make:
                equipRankBonus = create.equipRankBonus;
                rndItemLevel = create.rndItemLevel;
                break;

        }
        //TODO  upgradeOff 、upgradeDef为赋值计算
        upgradeDef = GetRandom_Normal(equip_template.upgrade[0], equip_template.upgrade[1]);
        upgradeOff = GetRandom_Normal(equip_template.upgrade[0], equip_template.upgrade[1]);
        //
        addLevel = 0;
        //addLevel = itemQuality == 1 ? 0 : GetAddLevel(equip_config.addLevelChance, tempItemLevel, maxItemLevel, item_instance.maxAddLevel);
        if (create.randomFields != null && create.randomFields.Count > 0)
        {         
            //随机字段操作
            RandomFieldOperation(create.randomFields);
            return;
        }
        //随机字段操作
        RandomFieldOperation(equip_template.rndAttribute);
    }

    /// <summary>
    /// 初始化信息
    /// </summary>
    private void InitInfo(int itemQuality)
    {
        char_config = Char_configConfig.GetConfig();
        equip_config = Equip_configConfig.GetConfig();
        equip_qualityup = Equip_qualityupConfig.GetQualityup(itemQuality);
        combat_config = Combat_configConfig.GetCombat_config();
}

    /// <summary>
    /// 获得添加等级
    /// </summary>
    private int GetAddLevel(List<float> chance, int tempLevel, int maxLevel, int maxSum)
    {
        int addLevel = 0;
        int sum = 0;
        //第一种检测
        while ((addLevel + tempLevel) < maxLevel && sum <= maxSum)
        {
            if (RandomBuilder.RandomIndex_Chances(new List<float> { chance[0] }) != 0)
            {
                return addLevel;
            }

            addLevel++;
            sum++;
        }
        //第二种检测
        while (sum <= maxSum)
        {
            if (RandomBuilder.RandomIndex_Chances(new List<float> { chance[1] }) != 0)
            {
                return addLevel;
            }

            addLevel++;
            sum++;
        }
        return addLevel;
    }

    /// <summary>
    /// 获得正态分布
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    private float GetRandom_Normal(float max, float min)
    {
        return (int)(RandomBuilder.Random_Normal(max, min) * 100) / 100f;
    }

}

