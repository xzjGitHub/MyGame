using System;
using System.Collections.Generic;
using System.Linq;
using GameEventDispose;

/// <summary>
/// 远征系统
/// </summary>
public class ExpeditionSystem : ScriptBase
{

    public static ExpeditionSystem Instance { get { return instance; } }

    /// <summary>
    /// 新建远征
    /// </summary>
    public ExpeditionSystem()
    {
        instance = this;
    }

    /// <summary>
    /// 创建远征
    /// </summary>
    /// <param name="_chars"></param>
    public void CreateExpedition(List<int> _chars)
    {
        isExpeditioning = true;
        //
        EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
        //
        expeditionData = new ExpeditionData
        {
            siegeStartTime = TimeUtil.GetPlayDays(),
            expeditionStartTime = expeditionData.siegeStartTime,
            expeditionTime = Game_configConfig.GetGame_Config().expeditionTime,
            siegeTime = Game_configConfig.GetGame_Config().siegeTime,
        };
        expeditionData.charGroups.AddRange(_chars);
        CharAttribute _attribute;
        //新建战斗单元组
        for (int i = 0; i < _chars.Count; i++)
        {
            _attribute = CharSystem.Instance.GetCharAttribute(_chars[i]);
            _attribute.SetCharPos(CharPos.Expedition);
            expeditionData.combatUnitGroups.Add(_chars[i], new CombatUnit(_attribute, i));
        }
    }
    /// <summary>
    /// 选择要塞
    /// </summary>
    /// <param name="_fortId"></param>
    /// <returns></returns>
    public bool SelectFort(int _fortId)
    {
        expeditionData.guardMobTeams.Clear();
        fortTemplate = Fort_templateConfig.GetFort_template(_fortId);
        if (fortTemplate == null) return false;
        //
        CreateGuardMobTeam();
        //
        return true;
    }
    /// <summary>
    /// 领取周期奖励
    /// </summary>
    public void ReceiveCycleReward()
    {
        ExpeditionReward _reward = new ExpeditionReward
        {
            gold = expeditionCycleRewards.Sum(a => a.gold),
            token = expeditionCycleRewards.Sum(a => a.token)
        };
        //物品列表
        foreach (var item in expeditionCycleRewards)
        {
            ItemSystem.Instance.CombineItemAttributeList(item.items, _reward.items);
        }
        //添加金币
        ScriptSystem.Instance.AddGold((int)_reward.gold);
        //添加代币
        PlayerSystem.Instance.AddToken((int)_reward.token);
        //添加物品
        foreach (var item in _reward.items)
        {
            ItemSystem.Instance.AddItem(item.GetItemData());
        }

    }
    /// <summary>
    /// 创建战斗队伍
    /// </summary>
    public void CreatCombatTeam(List<int> _list)
    {
        List<CombatUnit> _combatUnits = _list.Select(item => expeditionData.combatUnitGroups[item]).ToList();
        //
        TeamSystem.Instance.SetCharList(_combatUnits);
    }
    /// <summary>
    /// 选择Npc队伍
    /// </summary>
    /// <param name="_teamId"></param>
    public void SelectNpcTeam(int _teamId)
    {
        expeditionData.selectNpcTeamId = _teamId;
    }
    /// <summary>
    /// 开始主动攻击
    /// </summary>
    public void StartActiveAttack()
    {
        EventDispatcher.Instance.CombatEvent.AddEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
        combatSystem = new CombatSystem();
        var _playInfo = new CombatTeamInfo(0, TeamType.Player, TeamSystem.Instance.TeamAttribute.combatUnits);
        var _enemyInfo = new CombatTeamInfo(1, TeamType.Enemy, expeditionData.guardMobTeams.Find(a => a.teamId == expeditionData.selectNpcTeamId).combatUnits);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateCombat, (object)new CombatTeamInfo[] { _playInfo, _enemyInfo });
    }


    /// <summary>
    /// 恢复远征
    /// </summary>
    private void RecoverExpedition(ExpeditionData _expeditionData)
    {
        expeditionData = _expeditionData;
        EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
    }
    /// <summary>
    /// 创建守备队伍
    /// </summary>
    private void CreateGuardMobTeam()
    {
        foreach (var item in fortTemplate.mobTeamList)
        {
            mobMobteam = Mob_mobteamConfig.GetMobMobteam(item);
            if (mobMobteam == null) continue;
            expeditionData.guardMobTeams.Add(new GuardMobTeam(item));
        }
    }
    /// <summary>
    /// 完成远征
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    private ExpeditionReward FinishExpedition(int _id)
    {
        fortTemplate = Fort_templateConfig.GetFort_template(_id);
        return fortTemplate == null ? null : GetExpeditionReward(fortTemplate, false);
    }
    /// <summary>
    /// 周期结算
    /// </summary>
    private void CycleClearing()
    {
        //已占领的结算
        foreach (var item in occupyExpeditions)
        {
            fortTemplate = Fort_templateConfig.GetFort_template(item);
            if (fortTemplate == null) continue;
            ExpeditionReward _expedition = GetExpeditionReward(fortTemplate);
            ExpeditionReward _tempExpeditionReward = expeditionCycleRewards.Find(a => a.fortId == item);
            if (_tempExpeditionReward != null)
            {
                _tempExpeditionReward.gold += _expedition.gold;
                _tempExpeditionReward.token += _expedition.token;
                //合并物品属性列表
                ItemSystem.Instance.CombineItemAttributeList(_expedition.items, _tempExpeditionReward.items);
                continue;
            }
            expeditionCycleRewards.Add(_expedition);
        }
        //远征队伍结算
        SiegeClearing();
    }
    /// <summary>
    /// 围攻结算
    /// </summary>
    private void SiegeClearing()
    {
        //清除角色战斗休息状态
        foreach (var item in expeditionData.combatUnitGroups)
        {
            item.Value.ClearCombatRest();
        }
        //Npc恢复生命值
        foreach (var item in expeditionData.guardMobTeams)
        {
            item.RecoveryHp();
        }
    }
    /// <summary>
    /// 获得远征奖励
    /// </summary>
    /// <param name="_fortTemplate"></param>
    /// <param name="_isCycle">是否为周期奖励</param>
    /// <returns></returns>
    private ExpeditionReward GetExpeditionReward(Fort_template _fortTemplate, bool _isCycle = true)
    {
        ExpeditionReward _reward = new ExpeditionReward
        {
            fortId = _fortTemplate.fortID,
            gold = _isCycle ? _fortTemplate.cycleGoldReward : _fortTemplate.baseGoldReward,
            token = _isCycle ? _fortTemplate.cycleTokenReward : _fortTemplate.baseTokenReward,
        };
        List<ItemAttribute> _items = new List<ItemAttribute>();
        //创建奖励
        List<int> _list = _isCycle ? _fortTemplate.cycleRewardSet : _fortTemplate.itemRewardSet;
        //奖励等级
        List<float> finalLevels =
            (_isCycle ? _fortTemplate.baseRewardLevel : _fortTemplate.cycleRewardSet).Select(
                t => TownhalSystem.Instance.GetFinalRewardLevel(t)).ToList();
        List<ItemAttribute> _tempAttributes = ItemSystem.Instance.CreateItemreward(new ItemRewardInfo(finalLevels, _list), false);
        //合并物品属性列表
        ItemSystem.Instance.CombineItemAttributeList(_tempAttributes, _items);
        //添加奖励
        _reward.items.AddRange(_items);
        //
        return _reward;
    }
    /// <summary>
    /// 是否敌方全部死亡
    /// </summary>
    /// <returns></returns>
    private bool IsNpcAllDie()
    {
        return !expeditionData.guardMobTeams.Any(a => a.combatUnits.Any(_info => _info.hp > 0));
    }
    /// <summary>
    /// 是否敌方全部死亡
    /// </summary>
    /// <returns></returns>
    private bool IsCharAllDie()
    {
        return !expeditionData.combatUnitGroups.Any(a => a.Value.hp > 0);
    }
    /// <summary>
    /// 战斗事件
    /// </summary>
    private void OnCombatEvent(PlayCombatStage arg1, object arg2)
    {
        switch (arg1)
        {
            case PlayCombatStage.CombatEnd:
                EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
                //检查战斗单元组的状态
                foreach (var item in expeditionData.combatUnitGroups)
                {
                    if (item.Value.charAttribute.Status == CharStatus.Idle) continue;
                    item.Value.SetCharSate(item.Value.hp == 0 ? CharStatus.Die : CharStatus.CombatRest);
                }
                //判断远征状态
                if ((bool)arg2)
                {
                    //更新敌方当前状态
                    expeditionData.guardMobTeams.Find(a => a.teamId == expeditionData.selectNpcTeamId).SetAllDie();
                    //检查敌方角色列表是否全部死完
                    if (IsNpcAllDie())
                    {
                        //完成远征
                        CharSystem.Instance.ClearExpeditionPos();
                    }
                }
                else
                {
                    //检查我方角色列表是否全部死完
                    if (IsCharAllDie())
                    {
                        //完成远征
                        CharSystem.Instance.ClearExpeditionPos();
                    }
                }
                break;
        }
    }
    /// <summary>
    /// 剧本时间更新
    /// </summary>
    private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1, object arg2)
    {
        if (arg1 != ScriptTimeUpdateType.Day) return;
        //远征结束
        if (expeditionData.expeditionStartTime + expeditionData.expeditionTime >= (float)arg2)
        {
            //
            EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
            return;
        }
        //开始围攻
        if (arg2 != null && Math.Abs((float)arg2 - expeditionData.siegeStartTime - expeditionData.siegeTime) < 0.1f)
        {
            expeditionData.siegeStartTime = (float)arg2;
            SiegeClearing();
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        resurrectProp = Combat_configConfig.GetCombat_config().resurrectProp;
        if (expeditionSystemData == null)
        {
            expeditionSystemData = new ExpeditionSystemData();
            isExpeditioning = false;
            return;
        }
        //
        isExpeditioning = expeditionSystemData.isExpeditioning;
        unlockExpeditions = expeditionSystemData.unlockExpeditions;
        occupyExpeditions = expeditionSystemData.occupyExpeditions;
        expeditionCycleRewards = expeditionSystemData.expeditionCycleRewards;
        if (!isExpeditioning) return;
        if (expeditionSystemData.expeditionData == null) return;
        //恢复远征数据
        RecoverExpedition(expeditionSystemData.expeditionData);
    }
    /// <summary>
    /// 存档
    /// </summary>
    /// <param name="parentPath"></param>
    public override void SaveData(string parentPath)
    {
        expeditionSystemData.unlockExpeditions = unlockExpeditions;
        expeditionSystemData.occupyExpeditions = occupyExpeditions;
        expeditionSystemData.expeditionCycleRewards = expeditionCycleRewards;
        expeditionSystemData.isExpeditioning = isExpeditioning;
        expeditionSystemData.expeditionData = expeditionData;
        //
        GameDataManager.SaveData(parentPath, ExpeditionFilePath, expeditionSystemData);
    }
    /// <summary>
    /// 读档
    /// </summary>
    /// <param name="parentPath"></param>
    public override void ReadData(string parentPath)
    {
        this.parentPath = parentPath;
        //剧本存档
        expeditionSystemData = GameDataManager.ReadData<BountySystemData>(parentPath + ExpeditionFilePath) as ExpeditionSystemData;
    }

    private static ExpeditionSystem instance;
    //
    private string parentPath;
    private const string ExpeditionFilePath = "ExpeditionSystemData";
    private ExpeditionSystemData expeditionSystemData;
    private ExpeditionData expeditionData;
    //
    /// <summary>
    /// 是否远征中
    /// </summary>
    private bool isExpeditioning;
    //
    private float resurrectProp;
    /// <summary>
    /// 远征ID总列表
    /// </summary>
    private List<int> expeditionIds = new List<int>();
    /// <summary>
    /// 已解锁的列表
    /// </summary>
    private List<int> unlockExpeditions = new List<int>();
    /// <summary>
    /// 已经占领的列表
    /// </summary>
    private List<int> occupyExpeditions = new List<int>();
    /// <summary>
    /// 远征周期奖励
    /// </summary>
    private List<ExpeditionReward> expeditionCycleRewards = new List<ExpeditionReward>();
    //
    private Fort_template fortTemplate;
    private Mob_mobteam mobMobteam;
    private CombatSystem combatSystem;
}


/// <summary>
/// 守备队伍
/// </summary>
public class GuardMobTeam
{
    /// <summary>
    /// 队伍ID
    /// </summary>
    public int teamId;
    /// <summary>
    /// 角色属性列表
    /// </summary>
    public List<CombatUnit> combatUnits = new List<CombatUnit>();
    /// <summary>
    /// 是否击败
    /// </summary>
    public bool isDefeat;
    /// <summary>
    /// 是否可以复活
    /// </summary>
    private bool isRevivable;
    /// <summary>
    /// 恢复比例
    /// </summary>
    private float recoveryProb;
    /// <summary>
    /// 是否全部死亡
    /// </summary>
    private bool IsAllDie { get { return !combatUnits.Any(a => a.hp > 0); } }

    public GuardMobTeam(int _id)
    {
        teamId = _id;
        Mob_mobteam mobteam = Mob_mobteamConfig.GetMobMobteam(_id);
        //
        isRevivable = mobteam.isRevivable == 1;
        recoveryProb = mobteam.recoveryProb;
        for (int i = 0; i < mobteam.mobList.Count; i++)
        {
            combatUnits.Add(new CombatUnit(new CharAttribute(new CharCreate(mobteam.mobList[i])), i));
        }
    }
    /// <summary>
    /// 恢复生命值
    /// </summary>
    public void RecoveryHp()
    {
        if (!isRevivable && IsAllDie) return;
        foreach (var _combat in combatUnits)
        {
            _combat.hp = Math.Min((int)(_combat.hp + _combat.maxHp * recoveryProb), _combat.maxHp);
        }
    }

    /// <summary>
    /// 设置阵亡
    /// </summary>
    public void SetAllDie()
    {
        isDefeat = true;
        foreach (var item in combatUnits)
        {
            item.hp = 0;
        }
    }
}


/// <summary>
/// 远征奖励
/// </summary>
public class ExpeditionReward
{
    /// <summary>
    /// ID
    /// </summary>
    public int fortId;
    /// <summary>
    /// 金币
    /// </summary>
    public float gold;
    /// <summary>
    /// 代币
    /// </summary>
    public float token;
    /// <summary>
    /// 物品列表
    /// </summary>
    public List<ItemAttribute> items = new List<ItemAttribute>();
}