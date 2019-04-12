public partial class EquipAttribute : ItemAttribute
{

    public int charID;
    /// <summary>
    /// 装备模板ID
    /// </summary>
    public int equipTemplateID;
    /// <summary>
    /// 附魔
    /// </summary>
    private bool enchanted;
    private EquipState equipState = EquipState.Idle;
    /// <summary>
    /// 角色等级需求
    /// </summary>
    public int charLevelReq;
    /// <summary>
    /// 物品名字
    /// </summary>
    public string itemName;
    /// <summary>
    /// 物品图标名字
    /// </summary>
    public string itemIconName;
    /// <summary>
    /// 最小升级
    /// </summary>
    public float minUpgrade;
    /// <summary>
    /// 最大升级
    /// </summary>
    public float maxUpgrade;
    /// <summary>
    /// 附魔
    /// </summary>
    public bool Enchanted { get { return enchanted; } }

    public EquipState EquipState { get { return equipState; } }

    public int AddLevel { get { return equipRnd.addLevel; } }

    public EquipAttribute(){ }

    /// <summary>
    /// 根据存档构建装备
    /// </summary>
    /// <param name="itemData"></param>
    public EquipAttribute(ItemData itemData) : base(itemData)
    {
        itemQuality = itemData.itemQuality;
        itemType = itemData.itemType;
        sum = itemData.sum;
        itemID = itemData.itemID;
        instanceID = itemData.instanceID;
        EquipmentData equipmentData = itemData as EquipmentData;
        if (equipmentData!=null)
        {
            equipTemplateID = equipmentData.equipTemplateID;
            InitInfo();
            enchantRnd = equipmentData.enchantRndData == null ? null : new EnchantRnd(equipmentData.enchantRndData);
            minUpgrade = equipmentData.minUpgrade;
            maxUpgrade = equipmentData.maxUpgrade;
            charID = equipmentData.charID;
            enchanted = equipmentData.enchanted;
            equipState = (EquipState)equipmentData.equipState;
            equipRnd = new EquipRnd(equipmentData)
            {
                finalItemLevel = itemData.itemLevel,
                equipRankBonus = equipmentData.equipRankBonus,
                rndItemLevel = equipmentData.rndItemLevel,
            };
            charLevelReq = equipmentData.charLevelReq;
            itemName = equipmentData.itemName;
            itemIconName = equipmentData.itemIconName;
        }
    }

    /// <summary>
    /// 直接构建装备
    /// </summary>
    /// <param name="create"></param>
    public EquipAttribute(ItemCreate create) : base(create)
    {
        sum = create.sum;
        itemID = create.itemID;
        instanceID = create.instanceID;
        equipTemplateID = create.equipTemplateID;
        InitInfo();
        EquimentCreate equimentCreate = create as EquimentCreate;
        if (equimentCreate!=null)
        {
            equipRnd = new EquipRnd(equimentCreate);
            minUpgrade = create.minUpgrade;
            maxUpgrade = create.maxUpgrade;
            charLevelReq = equimentCreate.charLevelReq;
            itemName = equimentCreate.itemName;
            itemIconName = equimentCreate.itemIconName;
        }
        
    }
    /// <summary>
    /// 获得物品存档
    /// </summary>
    /// <returns></returns>
    public override ItemData GetItemData()
    {
        return new EquipmentData
        {
            itemLevel = equipRnd.finalItemLevel,
            itemType = itemType,
            instanceID = instanceID,
            itemQuality = equipRnd.itemQuality,
            templateID = equipRnd.templateID,
            tempItemLevel = equipRnd.tempItemLevel,
            addLevel = equipRnd.addLevel,
            charID = charID,
            randomField = equipRnd.RandomField,
            itemID = itemID,
            sum = sum,
            enchanted = enchanted,
            equipState = (int)equipState,
            itemName = itemName,
            itemIconName = itemIconName,
            charLevelReq = charLevelReq,
            resultItemLevel = 0,
            upgradeOff = equipRnd.upgradeOff,
            upgradeDef = equipRnd.upgradeDef,
            upgradeAll = equipRnd.upgradeAll,
            upgradeAll2 = equipRnd.upgradeAll2,
            upgradeRnd = equipRnd.upgradeRnd,
            upgradeRnd2 = equipRnd.upgradeRnd2,
            maxUpgrade = maxUpgrade,
            minUpgrade = minUpgrade,
            equipRankBonus = equipRnd.equipRankBonus,
            rndItemLevel = equipRnd.rndItemLevel,
            equipTemplateID = equipTemplateID,
            enchantRndData = enchantRnd != null ? enchantRnd.GetData() : null,
        };
    }


    /// <summary>
    /// 附魔
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="maxItemLevel"></param>
    public void EquipEnchanted(int instanceID, int maxItemLevel = 0)
    {
        enchanted = true;
        enchantRnd = new EnchantRnd(instanceID, maxItemLevel);
    }
    /// <summary>
    /// 附魔
    /// </summary>
    /// <param name="enchantRnd"></param>
    public void EquipEnchanted(EnchantRnd enchantRnd)
    {
        enchanted = true;
        this.enchantRnd = new EnchantRnd(enchantRnd);
    }

    /// <summary>
    /// 装备状态
    /// </summary>
    public void SetEquipState(EquipState _state)
    {
        equipState = _state;
    }

    private void InitInfo()
    {
        equip_template = Equip_templateConfig.GetEquip_template(equipTemplateID);
        game_config = Game_configConfig.GetGame_Config();
        hr_config = HR_configConfig.GetHR_Config();
    }
}

/// <summary>
/// 装备状态
/// </summary>
public enum EquipState
{
    /// <summary>
    /// 空闲
    /// </summary>
    Idle = 0,
    /// <summary>
    /// 附魔
    /// </summary>
    Enchanting = 1,
    /// <summary>
    /// 穿戴
    /// </summary>
    Wear = 2,
    /// <summary>
    /// 锁定
    /// </summary>
    Lock = 3,
    /// <summary>
    /// 研究中
    /// </summary>
    Researching = 4,
}