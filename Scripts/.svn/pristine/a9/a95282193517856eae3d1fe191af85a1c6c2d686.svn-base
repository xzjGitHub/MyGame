using System.Collections.Generic;
using System.Linq;
using MCCombat;

public partial class CombatSystem
{
    /// <summary>
    /// 预选目标
    /// </summary>
    private void PrimaryTargets()
    {
        foreach (CombatUnit item in playerTeamInfo.combatUnits)
        {
            PrimaryTarget(nowRound, item);
        }
        foreach (CombatUnit item in enemyTeamInfo.combatUnits)
        {
            PrimaryTarget(nowRound, item);
        }
    }

    /// <summary>
    /// 预选目标
    /// </summary>
    /// <param name="nowRound"></param>
    private void PrimaryTarget(int nowRound, CombatUnit combatUnit)
    {
        combatUnit.preselectedTargetInfos.Clear();
        if (!combatUnit.isMob)
        {
            return;
        }
        for (int i = 1; i < 5; i++)
        {
            CSkillInfo info = combatUnit.GetAutoUseSkillInfo(i, nowRound);
            if (info != null)
            {
                Targetset_template template;
                Dictionary<int, List<CombatUnit>> preselectedTargets = new Dictionary<int, List<CombatUnit>>();
                TargetType targetType;
                int skillID = info.ID;

                for (int j = 0; j < info.Combatskill.targetSetList.Count; j++)
                {
                    //添加这个Targetset
                    if (!preselectedTargets.ContainsKey(j))
                    {
                        preselectedTargets.Add(j, null);
                    }
                    template = Targetset_templateConfig.GetTargetset_template(info.Combatskill.targetSetList[j]);
                    targetType = (TargetType)template.targetType;
                    if (info is BossSkillInfo)
                    {
                        targetType = (TargetType)((BossSkillInfo)info).bossSkill.altTargeType[j];
                    }
                    preselectedTargets[j] = GetCombatUnits_Targetset(combatUnit, template, targetType);
                }
                combatUnit.preselectedTargetInfos.Add(new PreselectedTargetInfo(info, preselectedTargets));
            }
        }
    }

    #region 技能目标操作
    /// <summary>
    /// 根据技能目标得到需要操作的战斗角色列表
    /// </summary>
    /// <param name="atkUnit">攻击方角色信息</param>
    /// <param name="targetset">技能目标</param> 
    /// <returns></returns>
    private List<CombatUnit> GetCombatUnits_Targetset(CombatUnit atkUnit, Targetset_template targetset, TargetType targetType)
    {
        List<CombatUnit> list = new List<CombatUnit>();
        switch (targetType)
        {
            case TargetType.OtherPrimary:
                list = GetPrimaryCharList(atkUnit.teamId, targetset.targetCount, false);
                break;
            case TargetType.OtherSneak:
                list = GetSneakCharList(atkUnit.teamId, targetset.targetCount, false);
                break;
            case TargetType.OtherRandom:
                list = GetRandomCharList(atkUnit.teamId, targetset.targetCount, false);
                break;
            case TargetType.OtherPreviousAction:
                list = GetPreviousActionChar(atkUnit.teamId, atkUnit.actionId, false);
                break;
            case TargetType.OtherNextAction:
                list = GetNextActionChar(atkUnit.teamId, atkUnit.actionId, false);
                break;
            case TargetType.OtherHpMin:
                list = GetHpMinChar(atkUnit.teamId, false);
                break;
            case TargetType.OtherHpMax:
                list = GetOtherHpMax(atkUnit.teamId);
                break;
            case TargetType.OtherSecondStart:
                list = GetOtherSecondStart(atkUnit.teamId, targetset.targetCount);
                break;
            case TargetType.OtherFirstAfterRandom:
                list = GetOtherFirstAfterRandom(atkUnit.teamId);
                break;
            case TargetType.OtherSameIndex:
                list = GetOtherSameIndex(atkUnit.teamId, atkUnit.initIndex);
                break;
            case TargetType.Oneself:
                list = new List<CombatUnit> { atkUnit };
                break;
            case TargetType.SamePrimary:
                list = GetPrimaryCharList(atkUnit.teamId, targetset.targetCount);
                break;
            case TargetType.SameSneak:
                list = GetSneakCharList(atkUnit.teamId, targetset.targetCount);
                break;
            case TargetType.SameRandom:
                list = GetRandomCharList(atkUnit.teamId, targetset.targetCount);
                break;
            case TargetType.SamePreviousAction:
                list = GetPreviousActionChar(atkUnit.teamId, atkUnit.actionId);
                break;
            case TargetType.SameNextAction:
                list = GetNextActionChar(atkUnit.teamId, atkUnit.actionId);
                break;
            case TargetType.SameHpMin:
                list = GetHpMinChar(atkUnit.teamId);
                break;
            case TargetType.SameDie:
                list = GetSameDieCharList(atkUnit.teamId, targetset.targetCount);
                break;
            case TargetType.SameTeam:
                break;
            case TargetType.HealingTag:
                list = GetHealingTag(atkUnit.teamId, targetset.targetCount);
                break;
            case TargetType.HighThreat:
                list = GetHighThreat();
                break;
            case TargetType.XiXue:
                list = GetXiXue(atkUnit, targetset.targetCount);
                break;
            case TargetType.SameState:
                list = GetSameState(atkUnit, targetset);
                break;
            case TargetType.BeingHitMin:
                list = GetBeingHitMin(atkUnit);
                break;
            case TargetType.BeingHitAllMax:
                list = GetBeingHitAllMax(atkUnit);
                break;
            case TargetType.BeingHitMax:
                list = GetBeingHitMax(atkUnit);
                break;
        }
        //
        list = AmendList(list, targetset.targetCount);
        return list;
    }

    #region 技能目标得到需要操作的战斗角色列表
    /// <summary>
    /// 得到下一个行动的角色
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="actionId"></param>
    /// <returns></returns>
    private List<CombatUnit> GetNextActionChar(int teamId, int actionId, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, isOneself).combatUnits;
        for (int i = 0; i < _tempCombatUnits.Count; i++)
        {
            if (_tempCombatUnits[i].actionId <= actionId || _tempCombatUnits[i].hp == 0)
            {
                continue;
            }

            _combatUnits.Add(_tempCombatUnits[i]);
            break;
        }
        return _combatUnits;
    }
    /// <summary>
    /// 得到前一个行动的角色
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="actionId"></param>
    /// <returns></returns>
    private List<CombatUnit> GetPreviousActionChar(int teamId, int actionId, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, isOneself).combatUnits;
        for (int i = _tempCombatUnits.Count; i >= 0; i--)
        {
            if (_tempCombatUnits[i].actionId < actionId && _tempCombatUnits[i].hp != 0)
            {
                _combatUnits.Add(_tempCombatUnits[i]);
                break;
            }
        }
        return _combatUnits;
    }
    /// <summary>
    /// 得到对方生命值最小的角色
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    private List<CombatUnit> GetHpMinChar(int teamId, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, isOneself).combatUnits;
        Dictionary<int, float> lossHPs = new Dictionary<int, float>();
        for (int i = 0; i < _tempCombatUnits.Count; i++)
        {
            if (_tempCombatUnits[i].hp == 0)
            {
                continue;
            }

            lossHPs.Add(i, (_tempCombatUnits[i].maxHp - _tempCombatUnits[i].hp) / (float)_tempCombatUnits[i].maxHp);
        }
        if (lossHPs.Count <= 0)
        {
            return _combatUnits;
        }

        lossHPs = lossHPs.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, o => o.Value);
        _combatUnits.Add(_tempCombatUnits[lossHPs.First().Key]);
        return _combatUnits;
    }
    /// <summary>
    /// 得到对方生命值最大的角色
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    private List<CombatUnit> GetOtherHpMax(int teamId)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, false).combatUnits;
        Dictionary<int, float> lossHPs = new Dictionary<int, float>();
        for (int i = 0; i < _tempCombatUnits.Count; i++)
        {
            if (_tempCombatUnits[i].hp == 0)
            {
                continue;
            }

            lossHPs.Add(i, _tempCombatUnits[i].maxHp - _tempCombatUnits[i].hp);
        }
        if (lossHPs.Count <= 0)
        {
            return _combatUnits;
        }

        lossHPs = lossHPs.OrderBy(p => p.Value).ToDictionary(p => p.Key, o => o.Value);
        _combatUnits.Add(_tempCombatUnits[lossHPs.First().Key]);
        return _combatUnits;
    }
    /// <summary>
    /// 得到首要的角色列表
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="targetCount"></param>
    /// <returns></returns>
    private List<CombatUnit> GetPrimaryCharList(int teamId, int targetCount, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, isOneself).combatUnits;
        //排序
        _tempCombatUnits = _tempCombatUnits.OrderBy(a => a.initIndex).ToList();
        List<int> _indexs = GetUsableCharIds(_tempCombatUnits);
        //
        int _sum = _indexs.Count > targetCount ? targetCount : _indexs.Count;

        for (int i = 0; i < _sum; i++)
        {
            _combatUnits.Add(_tempCombatUnits[_indexs[i]]);
        }
        //
        return _combatUnits;
    }
    /// <summary>
    /// 得到随机的角色
    /// </summary>
    /// <param name="_teamID"></param>
    /// <param name="_targetCount"></param>
    /// <returns></returns>
    private List<CombatUnit> GetRandomCharList(int _teamID, int _targetCount, bool _isOneself = true)
    {
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(_teamID, _isOneself).combatUnits;
        List<int> _indexs = GetUsableCharIds(_tempCombatUnits);
        //
        _indexs = RandomBuilder.RandomList(_targetCount, _indexs);
        //
        return GetUsableCombatUnits(_tempCombatUnits, _indexs);
    }
    /// <summary>
    /// 得到偷袭的角色列表
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="targetCount"></param>
    /// <param name="isOneself"></param>
    /// <returns></returns>
    private List<CombatUnit> GetSneakCharList(int teamId, int targetCount, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, isOneself).combatUnits;
        List<int> _indexs = GetUsableCharIds(_tempCombatUnits);
        //
        int _sum = _indexs.Count > targetCount ? targetCount : _indexs.Count;

        for (int i = 0; i < _sum; i++)
        {
            _combatUnits.Add(_tempCombatUnits[_indexs[i]]);
        }
        //
        return _combatUnits;
    }
    /// <summary>
    /// 得到己方死亡的角色列表
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="targetCount"></param>
    /// <param name="isOneself"></param>
    /// <returns></returns>
    private List<CombatUnit> GetSameDieCharList(int teamId, int targetCount, bool isOneself = true)
    {
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, isOneself).combatUnits;
        List<int> _indexs = new List<int>();
        //
        for (int i = 0; i < _tempCombatUnits.Count; i++)
        {
            if (_tempCombatUnits[i].hp != 0)
            {
                continue;
            }

            _indexs.Add(i);
        }
        _indexs = RandomBuilder.RandomList(targetCount, _indexs);

        //
        return GetUsableCombatUnits(_tempCombatUnits, _indexs);
    }
    /// <summary>
    /// 从敌第2位开始向后计
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="targetCount"></param>
    /// <returns></returns>
    private List<CombatUnit> GetOtherSecondStart(int teamId, int targetCount)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId).combatUnits;
        //排序
        _tempCombatUnits = _tempCombatUnits.OrderBy(a => a.initIndex).ToList();
        for (int i = 1; i < _tempCombatUnits.Count; i++)
        {
            if (_combatUnits.Count >= targetCount)
            {
                break;
            }

            if (_tempCombatUnits[i].hp == 0)
            {
                break;
            }

            _combatUnits.Add(_tempCombatUnits[i]);
        }
        if (_combatUnits.Count != 0)
        {
            return _combatUnits;
        }

        if (_tempCombatUnits[0].hp > 0)
        {
            _combatUnits.Add(_tempCombatUnits[0]);
        }

        return _combatUnits;
    }
    /// <summary>
    /// 从敌第1位以后随机取一个
    /// </summary>
    /// <param name="teamId"></param>
    /// <returns></returns>
    private List<CombatUnit> GetOtherFirstAfterRandom(int teamId)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId).combatUnits;
        List<int> _indexs = new List<int>();
        //
        for (int i = 0; i < _tempCombatUnits.Count; i++)
        {
            if (_tempCombatUnits[i].hp != 0)
            {
                continue;
            }

            _indexs.Add(i);
        }
        int _index = RandomBuilder.RandomList(1, _indexs)[0];
        if (_index != -1)
        {
            _combatUnits.Add(_tempCombatUnits[_index]);
        }

        if (_combatUnits.Count != 0)
        {
            return _combatUnits;
        }

        if (_tempCombatUnits[0].hp > 0)
        {
            _combatUnits.Add(_tempCombatUnits[0]);
        }

        return _combatUnits;
    }
    /// <summary>
    /// 攻击与施法者index相同的敌方队伍目标
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private List<CombatUnit> GetOtherSameIndex(int teamId, int index)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        List<CombatUnit> _tempCombatUnits = GetCombatTeamInfo(teamId, false).combatUnits;
        CombatUnit combatUnit;
        int temp = index;
        switch (index)
        {
            case 0:
                while (temp < 4)
                {
                    combatUnit = GetUsableCombatUnit(_tempCombatUnits, temp);
                    if (combatUnit != null)
                    {
                        _combatUnits.Add(combatUnit);
                        break;
                    }
                    temp++;
                }
                break;
            case 1:
                while (temp < 4)
                {
                    combatUnit = GetUsableCombatUnit(_tempCombatUnits, temp);
                    if (combatUnit != null)
                    {
                        _combatUnits.Add(combatUnit);
                        break;
                    }
                    temp++;
                }
                if (_combatUnits.Count > 0)
                {
                    break;
                }

                combatUnit = GetUsableCombatUnit(_tempCombatUnits, 0);
                if (combatUnit != null)
                {
                    _combatUnits.Add(combatUnit);
                }

                break;
            case 2:
                while (temp > 4)
                {
                    combatUnit = GetUsableCombatUnit(_tempCombatUnits, temp);
                    if (combatUnit != null)
                    {
                        _combatUnits.Add(combatUnit);
                        break;
                    }
                    temp++;
                }
                if (_combatUnits.Count > 0)
                {
                    break;
                }

                temp = 1;
                while (temp > 0)
                {
                    combatUnit = GetUsableCombatUnit(_tempCombatUnits, temp);
                    if (combatUnit != null)
                    {
                        _combatUnits.Add(combatUnit);
                        break;
                    }
                    temp--;
                }
                break;
            case 3:
                while (temp > 0)
                {
                    combatUnit = GetUsableCombatUnit(_tempCombatUnits, temp);
                    if (combatUnit != null)
                    {
                        _combatUnits.Add(combatUnit);
                        break;
                    }
                    temp--;
                }
                break;
        }

        combatUnit = _tempCombatUnits.Find(a => a.initIndex == index);
        if (combatUnit != null && combatUnit.hp > 0)
        {
            _combatUnits.Add(combatUnit);
        }

        if (_combatUnits.Count != 0)
        {
            return _combatUnits;
        }

        if (_tempCombatUnits[0].hp > 0)
        {
            _combatUnits.Add(_tempCombatUnits[0]);
        }

        return _combatUnits;
    }

    /// <summary>
    /// 获得可用战斗单元
    /// </summary>
    /// <param name="units"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    private CombatUnit GetUsableCombatUnit(List<CombatUnit> units, int index)
    {
        CombatUnit combatUnit = units.Find(a => a.initIndex == index);
        return combatUnit != null && combatUnit.hp > 0 ? combatUnit : null;
    }

    /// <summary>
    /// 获得可用的角色id列表
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private List<int> GetUsableCharIds(List<CombatUnit> source)
    {
        List<int> _usables = new List<int>();
        for (int i = 0; i < source.Count; i++)
        {
            if (source[i].hp == 0)
            {
                continue;
            }

            _usables.Add(i);
        }
        return _usables;
    }
    /// <summary>
    /// 获得可用的战斗单元
    /// </summary>
    /// <param name="source"></param>
    /// <param name="_usables"></param>
    /// <returns></returns>
    private List<CombatUnit> GetUsableCombatUnits(List<CombatUnit> source, List<int> _usables)
    {
        if (_usables == null || _usables.Count == 0)
        {
            return null;
        }
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        for (int i = 0; i < _usables.Count - 1; i++)
        {
            _combatUnits.Add(source[_usables[i]]);
        }
        return _combatUnits;
    }

    /// <summary>
    /// 得到战斗队伍信息
    /// </summary>
    private CombatTeamInfo GetCombatTeamInfo(int _teamId, bool _isOneself = true)
    {
        if (_isOneself)
        {
            return playerTeamInfo.teamId == _teamId ? playerTeamInfo : enemyTeamInfo;
        }

        return playerTeamInfo.teamId != _teamId ? playerTeamInfo : enemyTeamInfo;
    }
    /// <summary>
    /// 获得治疗标签
    /// </summary>
    /// <param name="teamID"></param>
    /// <param name="targetCount"></param>
    /// <param name="isOneself"></param>
    /// <returns></returns>
    private List<CombatUnit> GetHealingTag(int teamID, int targetCount, bool isOneself = false)
    {
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        CombatTeamInfo combatTeam = GetCombatTeamInfo(teamID, isOneself);
        foreach (CombatUnit item in combatTeam.combatUnits)
        {
            if (item.initIndex != combatTeam.combatHealingTag.NowIndex)
            {
                continue;
            }

            _combatUnits.Add(item);
            break;
        }

        return _combatUnits;
    }
    /// <summary>
    /// 获得高威胁
    /// </summary>
    /// <returns></returns>
    private List<CombatUnit> GetHighThreat()
    {
        return new List<CombatUnit>
        {
            enemyTeamInfo.combatUnits.OrderByDescending(a => a.highThreat)
                .OrderBy(b => b.initIndex).ToList()[0]
        };
    }
    /// <summary>
    /// 获得吸血
    /// </summary>
    /// <param name="atkUnit"></param>
    /// <param name="targetCount"></param>
    /// <returns></returns>
    private List<CombatUnit> GetXiXue(CombatUnit atkUnit, int targetCount)
    {
        return GetPrimaryCharList(atkUnit.teamId, targetCount, false); ;
    }
    /// <summary>
    /// 获得相同状态
    /// </summary>
    private List<CombatUnit> GetSameState(CombatUnit atkUnit, Targetset_template targetset)
    {
        if (targetset.stateList.Count == 0)
        {
            return null;
        }
        int tempStateID = targetset.stateList[0];
        List<CombatUnit> _combatUnits = GetCombatTeamInfo(atkUnit.teamId, false).combatUnits;
        _combatUnits = _combatUnits.OrderBy(a => a.initIndex).ToList();
        foreach (CombatUnit item in _combatUnits)
        {
            if (item.States.All(a => a.stateID != tempStateID))
            {
                continue;
            }
            return new List<CombatUnit>() { item };
        }
        return new List<CombatUnit>() { _combatUnits[0] };
    }
    /// <summary>
    /// beingHit最低的后排
    /// </summary>
    /// <param name="atkUnit"></param>
    /// <param name="isOneself"></param>
    /// <returns></returns>
    private List<CombatUnit> GetBeingHitMin(CombatUnit atkUnit, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = GetCombatTeamInfo(atkUnit.teamId, isOneself).combatUnits;
        _combatUnits = _combatUnits.OrderBy(a => a.beingHit).OrderBy(b => b.initIndex).ToList();
        if (_combatUnits.Count == 1)
        {
            return _combatUnits;
        }
        //移出第一位
        _combatUnits.RemoveAt(0);
        int temp = 0;
        switch (_combatUnits.Count)
        {
            case 1:
                temp = 0;
                break;
            case 2:
                if (_combatUnits[0].beingHit != _combatUnits[1].beingHit)
                {
                    temp = 0;
                    break;
                }
                temp = RandomBuilder.RandomList(1, new List<int> { 0, 1 })[0];
                break;
            case 3:
                if (_combatUnits[0].beingHit != _combatUnits[1].beingHit)
                {
                    temp = 0;
                    break;
                }
                if (_combatUnits[0].beingHit != _combatUnits[2].beingHit)
                {
                    temp = RandomBuilder.RandomList(1, new List<int> { 0, 1 })[0];
                    break;
                }
                temp = RandomBuilder.RandomList(1, new List<int> { 0, 1, 2 })[0];
                break;
        }
        return new List<CombatUnit> { _combatUnits[0] };
    }
    /// <summary>
    /// beingHit最高的目标
    /// </summary>
    /// <param name="atkUnit"></param>
    /// <param name="isOneself"></param>
    /// <returns></returns>
    private List<CombatUnit> GetBeingHitAllMax(CombatUnit atkUnit, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = GetCombatTeamInfo(atkUnit.teamId, isOneself).combatUnits;
        _combatUnits = _combatUnits.OrderByDescending(a => a.beingHit).OrderBy(b => b.initIndex).ToList();
        if (_combatUnits.Count == 1)
        {
            return _combatUnits;
        }
        int temp = 0;
        switch (_combatUnits.Count)
        {
            case 2:
                if (_combatUnits[0].beingHit != _combatUnits[1].beingHit)
                {
                    temp = 0;
                    break;
                }
                temp = RandomBuilder.RandomList(1, new List<int> { 0, 1 })[0];
                break;
            case 3:
                if (_combatUnits[0].beingHit != _combatUnits[1].beingHit)
                {
                    temp = 0;
                    break;
                }
                if (_combatUnits[0].beingHit != _combatUnits[2].beingHit)
                {
                    temp = RandomBuilder.RandomList(1, new List<int> { 0, 1 })[0];
                    break;
                }
                temp = RandomBuilder.RandomList(1, new List<int> { 0, 1, 2 })[0];
                break;
            case 4:
                if (_combatUnits[0].beingHit != _combatUnits[1].beingHit)
                {
                    temp = 0;
                    break;
                }
                if (_combatUnits[0].beingHit != _combatUnits[2].beingHit)
                {
                    temp = RandomBuilder.RandomList(1, new List<int> { 0, 1 })[0];
                    break;
                }
                if (_combatUnits[0].beingHit != _combatUnits[3].beingHit)
                {
                    temp = RandomBuilder.RandomList(1, new List<int> { 0, 1, 2, })[0];
                    break;
                }
                temp = RandomBuilder.RandomList(1, new List<int> { 0, 1, 2, 3 })[0];
                break;
        }
        return new List<CombatUnit> { _combatUnits[0] };
    }
    /// <summary>
    /// 2、3、4号位置中beingHit最高的目标
    /// </summary>
    /// <param name="atkUnit"></param>
    /// <param name="isOneself"></param>
    /// <returns></returns>
    private List<CombatUnit> GetBeingHitMax(CombatUnit atkUnit, bool isOneself = true)
    {
        List<CombatUnit> _combatUnits = GetCombatTeamInfo(atkUnit.teamId, isOneself).combatUnits;
        _combatUnits = _combatUnits.OrderByDescending(a => a.beingHit).OrderBy(b => b.initIndex).ToList();
        if (_combatUnits.Count == 1)
        {
            return _combatUnits;
        }
        //移出第一位
        _combatUnits.RemoveAt(0);
        int temp = 0;
        switch (_combatUnits.Count)
        {
            case 1:
                temp = 0;
                break;
            case 2:
                if (_combatUnits[0].beingHit != _combatUnits[1].beingHit)
                {
                    temp = 0;
                    break;
                }
                temp = RandomBuilder.RandomList(1, new List<int> { 0, 1 })[0];
                break;
            case 3:
                if (_combatUnits[0].beingHit != _combatUnits[1].beingHit)
                {
                    temp = 0;
                    break;
                }
                if (_combatUnits[0].beingHit != _combatUnits[2].beingHit)
                {
                    temp = RandomBuilder.RandomList(1, new List<int> { 0, 1 })[0];
                    break;
                }
                temp = RandomBuilder.RandomList(1, new List<int> { 0, 1, 2 })[0];
                break;
        }
        return new List<CombatUnit> { _combatUnits[0] };
    }

    /// <summary>
    /// 修正列表
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private List<CombatUnit> AmendList(List<CombatUnit> list, int targetCount)
    {
        //检查消失
        if (targetCount == 4)
        {
            while (list.Any(a => a.IsHidden))
            {
                list.Remove(list.Find(a => a.IsHidden));
            }
        }
        return list;
    }
    #endregion
    #endregion

}
