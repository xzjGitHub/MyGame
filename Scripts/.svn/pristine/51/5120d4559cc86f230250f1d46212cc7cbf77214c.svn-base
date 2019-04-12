using System;
using LskConfig;
using System.Collections.Generic;



/// <summary>
/// Building_levelupConfig配置表
/// </summary>
public partial class Building_levelupConfig : TxtConfig<Building_levelupConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Building_levelup";
    }


    private static int GetLevelUpID(int arcId,int level)
    {
        string id = "";
        if (level<10)
        {
            id = "00" + level;
        }

        if(level>=10 && level<100)
        {
            id = "0" + level;
        }

        if(level >= 100)
        {
            id =""+level;
        }
        return Int32.Parse(arcId + id);
    }

    public static Building_levelup GetBuildingLevelup(int _level)
    {
        return Config._Building_levelup.Find(a => a.leveupID == _level);
    }

    public static Building_levelup GetBuiLevelUp(int arcId,int level)
    {
        int id = GetLevelUpID(arcId,level);
        Building_levelup levelup = Config._Building_levelup.Find(a => a.leveupID == id);
        if(levelup != null)
        {
            return levelup;
        }
        else
        {
            return null;
        }
    }

    public static float GetUpLevelNeddCoin(int arcId,int level)
    {
        return GetBuiLevelUp(arcId, level).goldCost;
    }

    public static float GetUpLevelNeddMana(int arcId,int level)
    {
        return GetBuiLevelUp(arcId,level).manaCost;
    }

    public static int GetMaxCharaterCount(int arcId,int level)
    {
        return GetBuiLevelUp(arcId,level).maxCharCount;
    }

    public static List<int> GetRewordSet(int arcId,int level)
    {
        return GetBuiLevelUp(arcId,level).itemRewardSet;
    }

    public static List<List<int>> GetItemCost(int arcId,int level)
    {
        return GetBuiLevelUp(arcId,level).itemCost;
    }
}
