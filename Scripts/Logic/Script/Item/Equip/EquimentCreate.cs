using System.Collections.Generic;

/// <summary>
/// 装备制造
/// </summary>
public class EquimentCreate : ItemCreate
{
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
    /// 随机字段
    /// </summary>
    public List<string> randomFields;
    /// <summary>
    /// 随机物品等级
    /// </summary>
    public int rndItemLevel;
    /// <summary>
    /// 装备位阶增幅
    /// </summary>
    public float equipRankBonus;


    /// <summary>
    /// 制造装备构造函数
    /// </summary>
    /// <param name="instanceId">实例ID</param>
    /// <param name="charLevelReq">角色等级需求</param>
    /// <param name="itemName">物品名字</param>
    /// <param name="itemIconName">物品图标名字</param>
    /// <param name="randomFields">随机字段</param>
    /// <param name="createType">物品创建类型</param>
    /// <param name="itemLevel">物品等级</param>
    /// <param name="maxItemLevel">物品最大等级</param>
    /// <param name="minUpgrade">最小升级</param>
    /// <param name="maxUpgrade">最大升级</param>
    /// <param name="rndItemLevel">物品随机等级</param>
    /// <param name="equipRankBonus">装备位阶增幅</param>
    public EquimentCreate(int instanceId, int charLevelReq, string itemName, string itemIconName,
        List<string> randomFields, ItemCreateType createType, int itemLevel, int maxItemLevel, 
        float minUpgrade, float maxUpgrade, int rndItemLevel, float equipRankBonus)
        : base(instanceId, -1, createType, itemLevel, maxItemLevel, minUpgrade, maxUpgrade)
    {
        this.charLevelReq = charLevelReq;
        this.itemName = itemName;
        this.itemIconName = itemIconName;
        this.randomFields = randomFields;
        this.rndItemLevel = rndItemLevel;
        this.equipRankBonus = equipRankBonus;
    }

    /// <summary>
    /// 非制造装备构造函数
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="itemID"></param>
    /// <param name="createType"></param>
    /// <param name="itemLevel"></param>
    /// <param name="maxItemLevel"></param>
    /// <param name="minUpgrade"></param>
    /// <param name="maxUpgrade"></param>
    public EquimentCreate(int instanceID, int itemID, ItemCreateType createType = ItemCreateType.Drop, int itemLevel = 0, int maxItemLevel = 0, float minUpgrade = 0, float maxUpgrade = 0)
        : base(instanceID, itemID, createType, itemLevel, maxItemLevel, minUpgrade, maxUpgrade)
    {
    }


}


