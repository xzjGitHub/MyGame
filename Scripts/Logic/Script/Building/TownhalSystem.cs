using GameEventDispose;
using System.Collections.Generic;


/// <summary>
/// 大厅系统
/// </summary>
public class TownhalSystem : Building
{
    private static TownhalSystem instance;

    public static TownhalSystem Instance { get { return instance; } }

    public float CurrentHp { get { return currentHp; } }

    public Building_template BuildingTemplate
    {
        get { return Building_templateConfig.GetBuildingTemplate((int)BuildingType.TownHall); }
    }

    public Building_levelup BuildingLevelup
    {
        get { return Building_levelupConfig.GetBuildingLevelup(level); }
    }

    //
    private const string TownhalPath = "Townhal";
    //
    private int level;
    private float currentHp;//当前生命值
    private int availableGold;
    private List<ItemData> existingItemDatas;
    //
    private TownhalData townhalData;
    //
    private Building_template buildingTemplate;

    public TownhalSystem() { instance = this; }

    public override void Init()
    {
        if (townhalData == null)
        {
            townhalData = new TownhalData();
            currentHp = Game_configConfig.GetGame_Config().coreHP;
            return;
        }
        level = townhalData.level;
        existingItemDatas = townhalData.existingItemDatas;
        availableGold = townhalData.availableGold;
        currentHp = townhalData.currentHp;
    }

    /// <summary>
    /// 读取存档信息
    /// </summary>
    /// <param name="parentPath"></param>
    public override void ReadData(string parentPath)
    {
        this.parentPath = parentPath;
        townhalData = GameDataManager.ReadData<TownhalData>(parentPath + TownhalPath) as TownhalData;
    }

    /// <summary>
    /// 自动保存收益
    /// </summary>
    /// <param name="gold">金币</param>
    /// <param name="itemAttributes">物品列表</param>

    public void AutoSaveEarnings(int gold, List<ItemAttribute> itemAttributes)
    {
        availableGold += gold;
        foreach (var item in itemAttributes)
        {
            existingItemDatas.Add(item.GetItemData());
        }
        //  SaveData(parentPath);
    }

    /// <summary>
    /// 领取收益
    /// </summary>
    public void ReceiveEarnings()
    {
        ScriptSystem.Instance.AddGold(availableGold);
        ItemSystem.Instance.AddItem(existingItemDatas);
    }


    /// <summary>
    /// 损失生命值
    /// </summary>
    public void LossCurrentHp(int _value)
    {
        currentHp -= _value;
        if (!(currentHp <= 0)) return;
        LogHelperLSK.LogError("大厅生命小于0，游戏结束");
        EventDispatcher.Instance.InvasionEvent.DispatchEvent(EventId.SystemEvent, GameSystemEventType.GameOver, (object)null);
    }
    /// <summary>
    /// 恢复生命值
    /// </summary>
    public void RecoverCurrentHp()
    {
        currentHp = Game_configConfig.GetGame_Config().coreHP;
    }

    /// <summary>
    /// 获得最终奖励等级
    /// </summary>
    /// <param name="baseRewardLevel"></param>
    /// <returns></returns>
    public float GetFinalRewardLevel(float baseRewardLevel)
    {
        return BuildingAttribute.Building.finalRewardLevel( BuildingTemplate);
    }


    /// <summary>
    /// 存档
    /// </summary>
    /// <param name="parentPath"></param>
    public override void SaveData(string parentPath)
    {
        townhalData.level = level;
        townhalData.availableGold = availableGold;
        townhalData.existingItemDatas = existingItemDatas;
        townhalData.currentHp = currentHp;
        GameDataManager.SaveData(parentPath, TownhalPath, townhalData);
    }
}
