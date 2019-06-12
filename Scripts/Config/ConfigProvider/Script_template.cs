using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Script_templateConfig配置表
/// </summary>
public partial class Script_templateConfig: TxtConfig<Script_templateConfig>
{
    protected override void Init()
    {
        base.Init();
        Info.Name = "Script_template";
    }

    public static List<Script_template> GetAll()
    {
        return Config._Script_template;
    }

    public static Script_template GetScript_template(int levelId)
    {
        Script_template scri = Config._Script_template.Find(a => a.templateID == levelId);
        if(scri != null)
        {
            return scri;
        }
        else
        {
            LogHelper_MC.Log("获取Script_template对象出错，levelID: " + levelId);
            return null;
        }
    }

    public static Script_template GetFirstScript_template()
    {
        return GetScript_templateIndex(0);
    }
    public static Script_template GetScript_templateIndex(int _index)
    {
        return Config._Script_template[_index];
    }

    public static List<int> GetInitTime(int levelID)
    {
        return GetScript_template(levelID).initialDate;
    }

    public static long GetInitSecod(int levelID)
    {
        List<int> times = GetScript_template(levelID).initialDate;

        if(times[0] == 0 && times[1] == 0 && times[2] == 0)
        {
            return 0;
        }

        long allSecod = 1;
        if(times[2] != 0)
            allSecod = times[2] * TimeUtil.DaySeconds;
        if(times[1] != 0)
            allSecod *= times[1] * TimeUtil.MonthSceonds;
        if(times[0] != 0)
            allSecod *= times[0] * TimeUtil.YearSceonds;

        return allSecod;
    }

    /// <summary>
    /// 获取关卡初始化角色列表
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public static List<List<int>> GetInitCharaterList(int levelId)
    {
        return GetScript_template(levelId).initialCharList;
    }

    /// <summary>
    /// 获取关卡初始化建筑列表
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public static List<List<int>> GetIniArchList(int levelId)
    {
        return null;
    }

    /// <summary>
    /// 获取初始装备
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public static List<List<int>> GetIniEquipList(int levelId)
    {
        return GetScript_template(levelId).initialEquipment;
    }

    /// <summary>
    /// 获取初始化物品
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public static List<List<int>> GetIniItemst(int levelId)
    {
        return GetScript_template(levelId).initialItemList;
    }


    /// <summary>
    /// 获取关卡初始化金币
    /// </summary>
    /// <param name="levelID"></param>
    /// <returns></returns>
    public static float GetInitGold(int levelID)
    {
        return GetScript_template(levelID).initialGold;
    }

    /// <summary>
    /// 获取关卡初始化魔力
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public static float GetInitMana(int levelId)
    {
        return GetScript_template(levelId).initialMana;
    }

    /// <summary>
    /// 获取玩家和对应的装备 玩家id 装备列表
    /// </summary>
    /// <param name="levelID"></param>
    /// <returns></returns>
    public static Dictionary<int,List<int>> GetPlayerAndEquipList(int levelID)
    {
        Dictionary<int,List<int>> eq = new Dictionary<int,List<int>>();
        List<List<int>> playerList = GetInitCharaterList(levelID);
        List<List<int>> equipList = GetIniEquipList(levelID);
        for(int i = 0; i < playerList.Count; i++)
        {
            if(equipList.Count > i)
                eq[playerList[i][0]] = equipList[i];
            else
                eq[playerList[i][0]] = new List<int>();
        }
        return eq;
    }
}
