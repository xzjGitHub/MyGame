using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 物品系统
/// </summary>
public partial class ItemSystem : ScriptBase
{
    private static ItemSystem instance;

    public static ItemSystem Instance
    {
        get { return instance; }
    }

    public Dictionary<int, ItemAttribute> ItemAttributes
    {
        get { return itemAttributes; }
    }

    public ItemSystem()
    {
        instance = this;
    }

    public override void Init()
    {
        itemAttributes.Clear();
        //
        if (itemSystemData == null)
        {
            itemSystemData = new ItemSystemData();
        }

        itemDatas = new Dictionary<int, ItemData>();
        //
        equipmentDatas = itemSystemData.equipmentDatas;
        materialDatas = itemSystemData.materialDatas;
        consumableDatas = itemSystemData.consumableDatas;
        runeDatas = itemSystemData.runeDatas;
        contractDatas = itemSystemData.contractDatas;
        //
        //整合itemDatas
        foreach (KeyValuePair<int, EquipmentData> item in equipmentDatas)
        {
            itemDatas.Add(item.Key, item.Value);
        }
        foreach (KeyValuePair<int, ItemData> item in materialDatas)
        {
            itemDatas.Add(item.Key, item.Value);
        }
        foreach (KeyValuePair<int, ItemData> item in consumableDatas)
        {
            itemDatas.Add(item.Key, item.Value);
        }
        foreach (KeyValuePair<int, ItemData> item in runeDatas)
        {
            itemDatas.Add(item.Key, item.Value);
        }
        foreach (KeyValuePair<int, ItemData> item in contractDatas)
        {
            itemDatas.Add(item.Key, item.Value);
        }
        //创建属性
        foreach (KeyValuePair<int, ItemData> item in itemDatas)
        {
            itemAttributes.Add(item.Key, CreateItemAttribute(item.Value));
        }
    }

    /// <summary>
    /// 读档
    /// </summary>
    /// <param name="parentPath"></param>
    public override void ReadData(string parentPath)
    {
        this.parentPath = parentPath;
        itemSystemData = GameDataManager.ReadData<ItemSystemData>(parentPath + ItemSystemPath) as ItemSystemData;
        //itemDatas = GameDataManager.ReadData<Dictionary<int, ItemData>>(parentPath + EquipPath) as Dictionary<int, ItemData>;
        //equipmentDatas = GameDataManager.ReadData<Dictionary<int, EquipmentData>>(parentPath + EquipmentPath) as Dictionary<int, EquipmentData>;
        //materialDatas = GameDataManager.ReadData<Dictionary<int, ItemData>>(parentPath + MaterialPath) as Dictionary<int, ItemData>;
        //consumableDatas = GameDataManager.ReadData<Dictionary<int, ItemData>>(parentPath + ConsumablePath) as Dictionary<int, ItemData>;
    }

    /// <summary>
    /// 存档
    /// </summary>
    public override void SaveData(string parentPath)
    {
        this.parentPath = parentPath;
        //  itemDatas.Clear();
        equipmentDatas.Clear();
        materialDatas.Clear();
        consumableDatas.Clear();
        runeDatas.Clear();
        contractDatas.Clear();
        //
        foreach (KeyValuePair<int, ItemAttribute> item in itemAttributes)
        {
            if (GameTools.IsEquip(item.Value.itemType))
            {
                equipmentDatas.Add(item.Key, item.Value.GetItemData() as EquipmentData);
            }
            else
            {
                materialDatas.Add(item.Key, item.Value.GetItemData());
            }
            //   itemDatas.Add(item.Key, item.Value.GetItemData());
        }
        //
        itemSystemData = new ItemSystemData
        {
            equipmentDatas = equipmentDatas,
            materialDatas = materialDatas,
            consumableDatas = consumableDatas,
            runeDatas = runeDatas,
            contractDatas = contractDatas,
        };

        GameDataManager.SaveData(parentPath, ItemSystemPath, itemSystemData);
        // GameDataManager.SaveData(parentPath, EquipPath, itemDatas);
    }

    /// <summary>
    /// 合并物品属性列表
    /// </summary>
    /// <param name="_sourceList">来源列表</param>
    /// <param name="_targetList">目标列表</param>
    public void CombineItemAttributeList(List<ItemAttribute> _sourceList, List<ItemAttribute> _targetList)
    {
        for (int i = 0; i < _sourceList.Count; i++)
        {
            ItemAttribute _attribute = _targetList.Find(a => a.instanceID == _sourceList[i].instanceID);
            if (_attribute != null)
            {
                _attribute.sum += _sourceList[i].sum;
                continue;
            }
            //
            _targetList.Add(_sourceList[i]);
        }
    }


    /// <summary>
    /// 穿戴装备
    /// </summary>
    public void WearEquipment(int itemId, int charId)
    {
        if (itemAttributes.ContainsKey(itemId))
        {
            (itemAttributes[itemId] as EquipAttribute).charID = charId;
            (itemAttributes[itemId] as EquipAttribute).SetEquipState(EquipState.Wear);
        }
    }
    /// <summary>
    /// 脱下装备
    /// </summary>
    public void RemoveEquipment(int itemId)
    {
        if (itemAttributes.ContainsKey(itemId))
        {
            (itemAttributes[itemId] as EquipAttribute).charID = 0;
            (itemAttributes[itemId] as EquipAttribute).SetEquipState(EquipState.Idle);
        }
    }
    /// <summary>
    /// 得到物品属性
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    public ItemAttribute GetItemAttribute(int _id)
    {
        return itemAttributes.ContainsKey(_id) ? itemAttributes[_id] : null;
    }

    /// <summary>
    /// 得到装备数据
    /// </summary>
    /// <param name="equipIDs"></param>
    /// <returns></returns>
    public List<ItemData> GetItemDatas(List<int> equipIDs)
    {
        List<ItemData> list = new List<ItemData>();

        foreach (int equipId in equipIDs)
        {
            list.AddRange(from item in itemAttributes where item.Key == equipId select item.Value.GetItemData());
        }

        return list;
    }
    /// <summary>
    /// 获取某个物品的数量
    /// </summary>
    /// <param name="itemID">唯一ID</param>
    /// <returns></returns>
    public int GetItemNum(int itemID)
    {
        if (!itemAttributes.ContainsKey(itemID))
        {
            return 0;
        }

        return itemAttributes[itemID].sum;
    }
    /// <summary>
    /// 获取某个物品的数量
    /// </summary>
    /// <param name="itemID">唯一ID</param>
    /// <returns></returns>
    public int GetItemNumByTemplateID(int itemID)
    {
        foreach (KeyValuePair<int, ItemAttribute> values in itemAttributes)
        {
            if (values.Value.instanceID == itemID)
            {
                return values.Value.sum;
            }
        }
        return 0;
    }


    /// <summary>
    /// 得到创建物品id
    /// </summary>
    private int GetCerateItemId(int id, bool isSave = true)
    {
        //临时物品Id
        if (!isSave)
        {
            id = tempItemIdList.Count + 1;
            tempItemIdList.Add(id);
            return id;
        }
        //清除为0的id
        if (id == 0)
        {
            id++;
        }

        if (!itemAttributes.ContainsKey(id))
        {
            return id;
        }

        id++;
        return GetCerateItemId(id);
    }
    /// <summary>
    /// 创建物品属性_直接
    /// </summary>
    private ItemAttribute CreateItemAttribute(ItemData itemData)
    {
        return GameTools.IsEquip(itemData.itemType) ? new EquipAttribute(itemData) : new ItemAttribute(itemData);
    }
    /// <summary>
    /// 创建物品属性_存档
    /// </summary>
    private ItemAttribute CreateItemAttribute(ItemCreate itemCreate)
    {
        return GameTools.IsEquip(itemCreate.itemType) ? new EquipAttribute(itemCreate) : new ItemAttribute(itemCreate);
    }
    ///// <summary>
    ///// 创建物品属性_存档
    ///// </summary>
    //private EquipAttribute CreateItemAttribute(EquimentCreate equimentCreate)
    //{
    //    return equimentCreate == null ? null : new EquipAttribute(equimentCreate);
    //}

    //
    private string parentPath;
    //
    private const string ItemSystemPath = "ItemSystemData";
    private const string EquipPath = "EquipDataList";
    private const string EquipmentPath = "EquipmentDataList";//装备列表
    private const string MaterialPath = "MaterialDataList";//素材列表
    private const string ConsumablePath = "ConsumableDataList";//消耗品列表
    //
    private ItemSystemData itemSystemData;
    //
    private Dictionary<int, ItemData> itemDatas = new Dictionary<int, ItemData>();
    private Dictionary<int, EquipmentData> equipmentDatas = new Dictionary<int, EquipmentData>();
    private Dictionary<int, ItemData> materialDatas = new Dictionary<int, ItemData>();
    private Dictionary<int, ItemData> consumableDatas = new Dictionary<int, ItemData>();
    private Dictionary<int, ItemData> runeDatas = new Dictionary<int, ItemData>();
    private Dictionary<int, ItemData> contractDatas = new Dictionary<int, ItemData>();
    //key:itemID,
    private Dictionary<int, ItemAttribute> itemAttributes = new Dictionary<int, ItemAttribute>();
    private List<int> tempItemIdList = new List<int>();
    //
    private Item_instance itemInstance;
}