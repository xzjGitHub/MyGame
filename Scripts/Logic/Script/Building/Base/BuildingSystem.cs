using System.Collections.Generic;
using Altar.Data;
using College.Research.Data;
using Core.Data;
using Shop.Data;

/// <summary>
/// 建筑系统
/// </summary>
public class BuildingSystem : ScriptBase
{
    //
    private List<Building> buildingBases;

    private static BuildingSystem instance;

    public static BuildingSystem Instance { get { return instance; } }

    /// <summary>
    /// 构造建筑系统
    /// </summary>
    public BuildingSystem()
    {
        instance = this;
        //初始建筑
        InitBuilding();
    }

    /// <summary>
    /// 存档
    /// </summary>
    public override void SaveData(string parentPath)
    {
        foreach (var item in buildingBases)
        {
            item.parentPath = parentPath;
            item.SaveData(parentPath);
        }
    }



    /// <summary>
    /// 初始建筑
    /// </summary>
    private void InitBuilding()
    {
        buildingBases = new List<Building>
        {
            new AltarSystem(),
            new Barrack.Data.BarrackSystem(),
            new MerchantSystem(),
            new ResearchLabSystem(),
            new TownhalSystem(),
            new WorkshopSystem(),
            new CoreSystem()
        };
    }

    /// <summary>
    /// 读档
    /// </summary>
    public override void ReadData(string parentPath)
    {
        foreach (var item in buildingBases)
        {
            item.parentPath = parentPath;
            item.ReadData(parentPath);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        foreach (var item in buildingBases)
        {
            item.Init();
        }
        //   buildingBases = null;
    }

}

