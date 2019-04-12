public class ItemCreate
{
    /// <summary>
    /// 装备实例配置表ID
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 装备模板ID
    /// </summary>
    public int equipTemplateID;
    /// <summary>
    /// 物品id
    /// </summary>
    public int itemID;
    /// <summary>
    /// 物品默认数量=1
    /// </summary>
    public int sum = 1;
    /// <summary>
    /// 物品类型
    /// </summary>
    public int itemType;
    /// <summary>
    /// 创建类型
    /// </summary>
    public ItemCreateType createType;
    /// <summary>
    /// 物品等级
    /// </summary>
    public int itemLevel;
    /// <summary>
    /// 物品最大等级
    /// </summary>
    public int maxItemLevel;
    /// <summary>
    /// 最小升级
    /// </summary>
    public float minUpgrade;
    /// <summary>
    /// 最大升级
    /// </summary>
    public float maxUpgrade;

    /// <summary>
    /// 创建物品
    /// </summary>
    /// <param name="instanceID">实例ID</param>
    /// <param name="itemID">物品ID</param>
    /// <param name="createType">创建类型</param>
    /// <param name="itemLevel">物品等级</param>
    /// <param name="maxItemLevel">物品最大等级</param>
    /// <param name="minUpgrade">最小升级</param>
    /// <param name="maxUpgrade">最大升级</param>
    public ItemCreate(int instanceID, int itemID, ItemCreateType createType = ItemCreateType.Drop, int itemLevel = 0, int maxItemLevel = 0, float minUpgrade = 0, float maxUpgrade = 0)
    {
        this.instanceID = instanceID;
        this.itemID = itemID;
        this.createType = createType;
        InitInfo(instanceID);
        if (createType != ItemCreateType.Drop)
        {
            this.itemLevel = itemLevel;
            this.maxItemLevel = maxItemLevel;
            this.minUpgrade = minUpgrade;
            this.maxUpgrade = maxUpgrade;
        }

        if (itemLevel != 0)
        {
            this.itemLevel = itemLevel;
        }
    }

    /// <summary>
    /// 初始化信息
    /// </summary>
    /// <param name="instanceID">实例ID</param>
    private void InitInfo(int instanceID)
    {
        itemInstance = Item_instanceConfig.GetItemInstance(instanceID);
        if (!GameTools.IsEquip(itemInstance.itemType))
        {
            return;
        }
        equipTemplateID = RandomBuilder.RandomList(1, itemInstance.template)[0];
        equipTemplate = Equip_templateConfig.GetEquip_template(equipTemplateID);
        if (equipTemplate == null)
        {
            LogHelperLSK.LogError("equipTemplate is null, equipTemplateID is: " + equipTemplateID);
        }
        itemType = itemInstance.itemType;
        switch (createType)
        {
            case ItemCreateType.Drop:
                minUpgrade = equipTemplate.upgrade[0];
                maxUpgrade = equipTemplate.upgrade[1];
                itemLevel = (int)itemInstance.baseItemLevel;
                maxItemLevel = itemInstance.maxItemLevel;
                break;
            case ItemCreateType.Make:
                break;
            case ItemCreateType.Recoin:
                break;
        }
    }
    //
    private Item_instance itemInstance;
    private Equip_template equipTemplate;
}

/// <summary>
/// 物品创建类型
/// </summary>
public enum ItemCreateType
{
    /// <summary>
    /// 掉落
    /// </summary>
    Drop = 0,
    /// <summary>
    /// 制造
    /// </summary>
    Make = 1,
    /// <summary>
    /// 重铸
    /// </summary>
    Recoin = 2,
}

