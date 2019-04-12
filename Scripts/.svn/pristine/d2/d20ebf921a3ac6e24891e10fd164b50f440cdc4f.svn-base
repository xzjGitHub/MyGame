using ProtoBuf;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// 装备存档数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class EquipmentData:ItemData
{
    /// <summary>
    /// 角色实例ID
    /// </summary>
    public int charID;
    /// <summary>
    /// 随机属性字段
    /// </summary>
    public List<string> randomField=new List<string>();
    /// <summary>
    /// 附魔
    /// </summary>
    public bool enchanted;
    /// <summary>
    /// 装备状态
    /// </summary>
    public int equipState;
    /// <summary>
    /// 物品赋值等级
    /// </summary>
    public int tempItemLevel;
    /// <summary>
    /// 物品等级加值
    /// </summary>
    public int addLevel;
    /// <summary>
    /// 角色需求等级
    /// </summary>
    public int charLevelReq;
    /// <summary>
    /// 附魔ID
    /// </summary>
    public int enchantID;
    /// <summary>
    /// 研究等级
    /// </summary>
    public float researchLevel;
    /// <summary>
    /// 角色随机强化_生命
    /// </summary>
    public float upgradeDef;
    /// <summary>
    /// 角色随机强化_伤害
    /// </summary>
    public float upgradeOff;
    /// <summary>
    /// 装备模板ID
    /// </summary>
    public int equipTemplateID;
    /// <summary>
    /// 最小升级
    /// </summary>
    public float minUpgrade;
    /// <summary>
    /// 最大升级
    /// </summary>
    public float maxUpgrade;
    /// <summary>
    /// 随机物品等级
    /// </summary>
    public int rndItemLevel;
    /// <summary>
    /// 装备位阶增幅
    /// </summary>
    public float equipRankBonus;
    //
    public float upgradeAll;
    public float upgradeAll2;
    public float upgradeRnd;
    public float upgradeRnd2;
    public List<FieldInfo> randomFieldInfos = new List<FieldInfo>();
    public EnchantRndData enchantRndData;
}

