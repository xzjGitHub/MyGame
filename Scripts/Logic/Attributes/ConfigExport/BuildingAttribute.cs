using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// BuildingAttribute
/// </summary>
public partial class BuildingAttribute
{
    /// <summary>
    /// 建筑模板
    /// </summary>
    public Building_template building_template;
    /// <summary>
    /// 兵营参数
    /// </summary>
    public HR_config hr_config;
    /// <summary>
    /// 核心升级
    /// </summary>
    public Core_lvup core_lvup;
    /// <summary>
    /// 市场配置
    /// </summary>
    public Shop_config shop_config;
    /// <summary>
    /// 制造模板
    /// </summary>
    public Forge_template forge_template;
    /// <summary>
    /// 声望奖励模板
    /// </summary>
    public RR_template rr_template;
    /// <summary>
    /// 
    /// </summary>
    public Blackmarket_template blackmarket_template;
    /// <summary>
    /// 单位魔力消耗
    /// </summary>
    public float unitManaCost
    {
        get
        {
            return (float)(hr_config.unitManaCost);
        }
    }
    /// <summary>
    /// 金币消耗
    /// </summary>
    public float goldCost
    {
        get
        {
            return (float)(100);
        }
    }
    /// <summary>
    /// 魔力消耗(存量)
    /// </summary>
    public float manaCost
    {
        get
        {
            return (float)(100);
        }
    }
    /// <summary>
    /// 单位产量
    /// </summary>
    public float finalRewardVaue
    {
        get
        {

            switch (building_template.buildingType)
            {
                case 1:
                    return 0;
                case 2:
                    return baseRewardValue * (1 + core_lvup.resourceOutPutBonus) * coreCapacity;
                case 3:
                    return hr_config.baseGoldOutput * (1 + core_lvup.resourceOutPutBonus);
                case 4:
                    return 0;
                case 5:
                    return 0;
                case 6:
                    return 0;
                default:
                    return 0;
            }
        }
    }
    /// <summary>
    /// 基本产量
    /// </summary>
    public float baseRewardValue
    {
        get
        {

            switch (building_template.buildingType)
            {
                case 1:
                    return 0;
                case 2:
                    return currentCorePower / 3;
                case 3:
                    return 0;
                case 4:
                    return 0;
                case 5:
                    return 0;
                case 6:
                    return 0;
                default:
                    return 0;
            }
        }
    }
    /// <summary>
    /// 核心效率
    /// </summary>
    public float coreCapacity;
    /// <summary>
    /// 战力增幅
    /// </summary>
    public float skillBonus;
    /// <summary>
    /// 当前核心功率
    /// </summary>
    public float currentCorePower;
    /// <summary>
    /// 最小研究等级
    /// </summary>
    public float minResearchLevel
    {
        get
        {
            return (float)(core_lvup.minResearchLevel);
        }
    }
    /// <summary>
    /// 金币销售
    /// </summary>
    public float finalGoldSales
    {
        get
        {
            return (float)(shop_config.baseGoldSales * (1 + core_lvup.resourceOutPutBonus));
        }
    }
    /// <summary>
    /// 装备制造-金币消耗
    /// </summary>
    public float forgeGoldCost
    {
        get
        {
            return (float)(forge_template.goldCost);
        }
    }
    /// <summary>
    /// 装备制造-魔力消耗
    /// </summary>
    public float forgeManaCost
    {
        get
        {
            return (float)(forge_template.manaCost);
        }
    }

}
