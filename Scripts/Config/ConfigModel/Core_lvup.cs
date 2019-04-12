using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Core_lvupConfig配置表
/// </summary>
public partial class Core_lvupConfig : IReader
{
    public List<Core_lvup> _Core_lvup = new List<Core_lvup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Core_lvup.Add(new Core_lvup(array[i]));
        }
    }
}



/// <summary>
/// Core_lvup配置表
/// </summary>
public partial class Core_lvup : IReader
{
    /// <summary>
    /// 当前等级
    /// </summary>
    public int coreLevel;
    /// <summary>
    /// 升级所需的累积值
    /// </summary>
    public int lvupCorePower;
    /// <summary>
    /// 战斗初始魔能
    /// </summary>
    public int initialEnergy;
    /// <summary>
    /// 资源产出增幅
    /// </summary>
    public float resourceOutPutBonus;
    /// <summary>
    /// 建筑效率
    /// </summary>
    public float buildingEfficiency;
    /// <summary>
    /// 修理花费金币
    /// </summary>
    public int repairCost;
    /// <summary>
    /// 修理花费时间
    /// </summary>
    public int repairTime;
    /// <summary>
    /// 建筑对话，index =0，大厅；index = 1，核心
    /// </summary>
    public List<int> buildingDialog;
    /// <summary>
    /// 最大能参与工作的角色数
    /// </summary>
    public int maxWorker;
    /// <summary>
    /// 学院生产线数
    /// </summary>
    public int academyLine;
    /// <summary>
    /// 工坊生产线数
    /// </summary>
    public int workshopLine;
    /// <summary>
    /// 最小研究等级
    /// </summary>
    public int minResearchLevel;
    /// <summary>
    /// 每日训练经验奖励
    /// </summary>
    public float dailyExpReward;
    /// <summary>
    /// 最大训练等级
    /// </summary>
    public int trainningMaxLevel;
    /// <summary>
    /// 最大角色数
    /// </summary>
    public int maxChar;
    /// <summary>
    /// 备选列表上限
    /// </summary>
    public int maxSummonList;
    /// <summary>
    /// 增加的物品等级
    /// </summary>
    public List<int> addItemLevel;
    /// <summary>
    /// 增加的附魔等级
    /// </summary>
    public List<int> addEnchantLevel;
    /// <summary>
    /// 
    /// </summary>
    public int summonCharLevel;
    /// <summary>
    /// 召唤角色的最小强化等级随机范围
    /// </summary>
    public List<int> minUpgrade;
    /// <summary>
    /// 召唤角色的最大强化等级
    /// </summary>
    public int maxUpgrade;
    /// <summary>
    /// 
    /// </summary>
    public float equipRankBonus;



    public Core_lvup() { }
    public Core_lvup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        coreLevel = int.Parse(array[0]);
        lvupCorePower = int.Parse(array[1]);
        initialEnergy = int.Parse(array[2]);
        resourceOutPutBonus = float.Parse(array[3]);
        buildingEfficiency = float.Parse(array[4]);
        repairCost = int.Parse(array[5]);
        repairTime = int.Parse(array[6]);
        //列表buildingDialog取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        buildingDialog = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { buildingDialog.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        maxWorker = int.Parse(array[8]);
        academyLine = int.Parse(array[9]);
        workshopLine = int.Parse(array[10]);
        minResearchLevel = int.Parse(array[11]);
        dailyExpReward = float.Parse(array[12]);
        trainningMaxLevel = int.Parse(array[13]);
        maxChar = int.Parse(array[14]);
        maxSummonList = int.Parse(array[15]);
        //列表addItemLevel取值
        array[16] = array[16].Replace("[", "").Replace("]", "").Replace(" ","");
        addItemLevel = new List<int>();
        foreach (var _str in array[16].Split(','))
        {
            try { addItemLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addEnchantLevel取值
        array[17] = array[17].Replace("[", "").Replace("]", "").Replace(" ","");
        addEnchantLevel = new List<int>();
        foreach (var _str in array[17].Split(','))
        {
            try { addEnchantLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        summonCharLevel = int.Parse(array[18]);
        //列表minUpgrade取值
        array[19] = array[19].Replace("[", "").Replace("]", "").Replace(" ","");
        minUpgrade = new List<int>();
        foreach (var _str in array[19].Split(','))
        {
            try { minUpgrade.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        maxUpgrade = int.Parse(array[20]);
        equipRankBonus = float.Parse(array[21]);
    }
}
