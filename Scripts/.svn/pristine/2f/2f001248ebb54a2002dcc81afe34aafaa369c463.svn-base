/// <summary>
/// 物品属性
/// </summary>
public class ItemAttribute
{
    public ItemType ItemType
    {
        get { return (ItemType) itemType; }
    }

    /// <summary>
    /// 物品id
    /// </summary>
    public int itemID;
    /// <summary>
    /// 物品实例id
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 物品数量
    /// </summary>
    public int sum;
    /// <summary>
    /// 物品种类
    /// </summary>
    public int itemCategory;
    /// <summary>
    /// 物品类型
    /// </summary>
    public int itemType;
    /// <summary>
    /// 物品品质
    /// </summary>
    public int itemQuality;
    /// <summary>
    /// 物品等级
    /// </summary>
    public int itemLevel;
    /// <summary>
    /// 装备模板
    /// </summary>
    public Item_instance item_instance;

    public ItemAttribute() { }

    /// <summary>
    /// 根据存档创建物品
    /// </summary>
    /// <param name="itemData"></param>
    public ItemAttribute(ItemData itemData)
    {
        instanceID = itemData.instanceID;
        itemID = itemData.itemID;
        sum = itemData.sum;
        itemType = itemData.itemType;
        itemQuality = itemData.itemQuality;
        itemLevel = itemData.itemLevel;
    }

    /// <summary>
    /// 新建物品
    /// </summary>
    /// <param name="itemCreate"></param>
    public ItemAttribute(ItemCreate itemCreate)
    {
        instanceID = itemCreate.instanceID;
        itemID = itemCreate.itemID;
        sum = itemCreate.sum;
        itemType = itemCreate.itemType;
        itemLevel = itemCreate.itemLevel;
        item_instance = Item_instanceConfig.GetItemInstance(instanceID);
       // itemQuality = item_instance.itemQuality;
    }

    /// <summary>
    /// 获得物品存档数据
    /// </summary>
    /// <returns></returns>
    public virtual ItemData GetItemData()
    {
        return new ItemData
        {
            itemID = itemID,
            instanceID = instanceID,
            sum = sum,
            itemType = itemType,
            itemQuality = itemQuality,
            itemLevel = itemLevel,
        };
    }

}

