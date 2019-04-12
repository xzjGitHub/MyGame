using System;
using System.Collections.Generic;
using System.Linq;
using MCCombat;

/// <summary>
/// 战斗队伍信息
/// </summary>
public class CombatTeamInfo
{
    public CombatTeamInfo(int _teamId, TeamType _teamType, List<CombatUnit> _combatUnits = null)
    {
        teamId = _teamId;
        teamType = _teamType;
        maxPower = Combat_configConfig.GetCombat_config().maxEnergy;
        // currentPower = (int)(Core_lvupConfig.GetCore_lvup(CoreSystem.Instance.GetLevel()).initialPower + combatUnits.Sum(a => a.charAttribute.initialPower));
        if (_combatUnits == null)
        {
            return;
        }

        while (_combatUnits.Any(a => a == null))
        {
            for (int i = 0; i < _combatUnits.Count; i++)
            {
                if (_combatUnits[i] == null)
                {
                    _combatUnits.RemoveAt(i);
                    break;
                }
            }
        }
        //
        combatUnits = _combatUnits;
        for (int i = 0; i < combatUnits.Count; i++)
        {
            combatUnits[i].teamId = _teamId;
            combatUnits[i].teamType = _teamType;
        }
        //
        //TODO PersonalityTake();
    }

    /// <summary>
    /// 设置治疗标签
    /// </summary>
    /// <param name="index"></param>
    /// <param name="nowRound"></param>
    public void SetHealingTag(int index, int nowRound)
    {
        if (combatHealingTag == null)
        {
            combatHealingTag = new CombatHealingTag(index, nowRound);
            return;
        }
        combatHealingTag.SetIndex(index, nowRound);
    }

    /// <summary>
    /// 队伍id
    /// </summary>
    public int teamId;
    /// <summary>
    /// 队伍类型
    /// </summary>
    public TeamType teamType;
    /// <summary>
    /// 角色列表
    /// </summary>
    public List<CombatUnit> combatUnits;
    /// <summary>
    /// 队伍状态
    /// </summary>
    public List<StateAttribute> states = new List<StateAttribute>();

    /// <summary>
    /// 治疗标签
    /// </summary>
    public CombatHealingTag combatHealingTag;

    public float CurrentEnergy { get { return _currentEnergy; } }

    public float MaxPower { get { return maxPower; } }


    /// <summary>
    /// 添加能量
    /// </summary>
    /// <param name="value"></param>
    public void AddEnergy(float value)
    {
        _currentEnergy = Math.Min(_currentEnergy + value, maxPower);
    }
    /// <summary>
    /// 使用能量
    /// </summary>
    /// <param name="value"></param>
    public void UseEnergy(int value)
    {
        _currentEnergy = Math.Max(0, _currentEnergy - value);
    }
    /// <summary>
    /// 消耗能量
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int ConsumeEnergy(int value)
    {
        int temp = (int)_currentEnergy;
        UseEnergy(value);
        temp = temp - (int)_currentEnergy;
        return temp;
    }

    public void AddState(StateAttribute state)
    {
        StateSystem.AddState(states, state);
    }
    public void RemoveState(StateAttribute state)
    {
        StateSystem.RemoveState(states, state);
    }


    /// <summary>
    /// 移除状态
    /// </summary>
    public int RemoveState(StateType stateType, int removeSum)
    {
        return StateSystem.RemoveState(states, stateType, removeSum);
    }


    /// <summary>
    /// 重置性格效果
    /// </summary>
    public void RestPersonalityTake()
    {
        foreach (CombatUnit item in combatUnits)
        {
            item.ResetPersonalityEffect();
        }
        PersonalityTake();
    }

    /// <summary>
    /// 性格生效
    /// </summary>
    public void PersonalityTake()
    {
        //先按类型排序
        combatUnits = combatUnits.OrderBy(a => a.charAttribute.PersonalityType).ToList();
        //性格生效
        foreach (CombatUnit item in combatUnits)
        {
            PersonalityTakeEffect(item, combatUnits);
        }
        //按索引排序
        combatUnits = combatUnits.OrderBy(a => a.initIndex).ToList();
        //最后添加激励
        List<int> relational1 = new List<int> { 1, 2, 3 };
        List<int> relational2 = new List<int> { 4, 5, 6 };
        //先禁用产出   //如果1名角色上，有2个相关联的disableTag，则所有获得或产出会为0；
        foreach (CombatUnit unit in combatUnits)
        {
            //产出
            int sum = GetRelationalSum(unit, relational2);
            switch (sum)
            {
                case 0:
                    break;
                case 1:
                    foreach (CombatUnit item in combatUnits)
                    {
                        foreach (PersonalityAddEncourage info in item.personalityAddEncourages)
                        {
                            if (info.formeCharIndex == unit.initIndex && info.disableTag != 4)
                            {
                                info.addEncourage = 0;
                            }
                        }
                    }
                    break;
                default:
                    foreach (CombatUnit item in combatUnits)
                    {
                        foreach (PersonalityAddEncourage info in item.personalityAddEncourages)
                        {
                            if (info.formeCharIndex == unit.initIndex)
                            {
                                info.addEncourage = 0;
                            }
                        }
                    }
                    break;
            }
        }
        //再加获得
        foreach (CombatUnit unit in combatUnits)
        {
            //获得
            int sum = GetRelationalSum(unit, relational1);
            switch (sum)
            {
                case 0:
                    foreach (PersonalityAddEncourage info in unit.personalityAddEncourages)
                    {
                        unit.charAttribute.SetFinalEncourage(info.addEncourage, info.formeCharIndex);
                    }
                    break;
                case 1:
                    foreach (PersonalityAddEncourage info in unit.personalityAddEncourages)
                    {
                        if (info.disableTag == 1 || info.disableTag == 2 || info.disableTag == 3)
                        {
                            unit.charAttribute.SetFinalEncourage(info.addEncourage, info.formeCharIndex);
                        }
                    }
                    break;
                default:
                    unit.charAttribute.SetFinalEncourage(0);
                    break;
            }
        }
    }

    public void PowerUpTake()
    {
        //强化生效
        foreach (CombatUnit item in combatUnits)
        {
            PowerUpTakeEffect(item, combatUnits);
        }
    }


    private void PowerUpTakeEffect(CombatUnit oneself, List<CombatUnit> combatUnits)
    {

        foreach (int powerUpID in oneself.charAttribute.PowerUpIDs)
        {
            PowerUp_template powerUp = PowerUp_templateConfig.GeTemplate(powerUpID);
            if (powerUp == null)
            {
                continue;
            }

            CombatUnit targetUnit;
            switch (powerUp.reqType)
            {
                case 1: //以相邻的指定性格的角色为目标，优先右侧
                    targetUnit = GetCharRightPos(oneself, combatUnits);
                    if (targetUnit != null)
                    {
                        if (targetUnit.charAttribute.AttitudeID == powerUp.personality)
                        {
                            targetUnit.charAttribute.SetFinalEncourage(powerUp.addEncourage, oneself.initIndex);
                            break;
                        }
                    }
                    targetUnit = GetCharLeftPos(oneself, combatUnits);
                    if (targetUnit != null)
                    {
                        if (targetUnit.charAttribute.AttitudeID == powerUp.personality)
                        {
                            targetUnit.charAttribute.SetFinalEncourage(powerUp.addEncourage, oneself.initIndex);
                        }
                    }
                    break;
                case 2: //以角色右侧的角色为目标
                    targetUnit = GetCharRightPos(oneself, combatUnits);
                    if (targetUnit != null)
                    {
                        targetUnit.charAttribute.SetFinalEncourage(powerUp.addEncourage, oneself.initIndex);
                    }
                    break;
                case 3: //以角色左侧的角色为目标
                    targetUnit = GetCharLeftPos(oneself, combatUnits);
                    if (targetUnit != null)
                    {
                        targetUnit.charAttribute.SetFinalEncourage(powerUp.addEncourage, oneself.initIndex);
                    }
                    break;
                case 4: //以队伍中最右侧的指挥官为目标
                    combatUnits = combatUnits.OrderBy(a => a.initIndex).ToList();
                    foreach (CombatUnit item in combatUnits)
                    {
                        if (!item.charAttribute.IsCommander)
                        {
                            continue;
                        }

                        item.charAttribute.SetFinalEncourage(powerUp.addEncourage, oneself.initIndex);
                        break;
                    }
                    break;
                case 5: //以自身为目标
                    oneself.charAttribute.SetFinalEncourage(powerUp.addEncourage, oneself.initIndex);
                    break;
            }
        }

    }



    /// <summary>
    /// 性格生效
    /// </summary>
    private void PersonalityTakeEffect(CombatUnit oneself, List<CombatUnit> combatUnits)
    {
        PassiveSkill_template passiveSkill = PassiveSkill_templateConfig.GetPassiveSkill_Template(oneself.charAttribute.PersonalityAddPassiveSkill);
        if (passiveSkill == null)
        {
            return;
        }
        CombatUnit targetUnit;
        int personalityID;
        //性格需求表
        List<int> personalityReqIDs = new List<int>();
        for (int i = 0; i < passiveSkill.personalityList.Count; i++)
        {
            personalityID = passiveSkill.personalityList[i];
            if (personalityID == 0)
            {
                continue;
            }
            personalityReqIDs.Add(personalityID);
        }
        //检查需求
        switch (passiveSkill.reqType)
        {
            case 0: //无条件，必定生效
                AddTagAndReward(passiveSkill, oneself, combatUnits);
                break;
            case 1: //与指定性格（列表）的位置关系——相邻
                //先检查右边
                targetUnit = GetCharRightPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.charAttribute.AttitudeID))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                        break;
                    }
                }
                //检查左边
                targetUnit = GetCharLeftPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.charAttribute.AttitudeID))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                    }
                }
                break;
            case 2: //与指定性格（列表）的位置关系——相邻且在其后
                //检查右边
                targetUnit = GetCharRightPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.charAttribute.AttitudeID))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                    }
                }
                break;
            case 3: //与指定性格（列表）的位置关系——相邻且在其前
                //检查左边
                targetUnit = GetCharLeftPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    if (personalityReqIDs.Contains(targetUnit.charAttribute.AttitudeID))
                    {
                        AddTagAndReward(passiveSkill, oneself, combatUnits, targetUnit);
                    }
                }
                break;
            case 4: //队友不包含指定性格（列表）
                List<int> temp = new List<int>();
                foreach (CombatUnit item in combatUnits)
                {
                    if (item.initIndex == oneself.initIndex)
                    {
                        continue;
                    }
                    temp.Add(item.charAttribute.AttitudeID);
                }
                bool isHaveAttitudeID = false;
                foreach (int item in personalityReqIDs)
                {
                    if (temp.Contains(item))
                    {
                        isHaveAttitudeID = true;
                        break;
                    }
                }
                //没有满足的性格
                if (!isHaveAttitudeID)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 5: //位于首位
                if (oneself.initIndex == 0)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 6: //不位于首位
                if (oneself.initIndex != 0)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 7: //位于最后一位
                if (oneself.initIndex == 3)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 8: //不位于最后一位
                if (oneself.initIndex != 3)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 9: //受到激励
                bool isHaveEncourage = false;
                foreach (PersonalityAddEncourage item in oneself.personalityAddEncourages)
                {
                    if (item.addEncourage > 0)
                    {
                        isHaveEncourage = true;
                        break;
                    }
                }
                if (isHaveEncourage)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 10:  //未受到激励  
                bool isNoHaveEncourage = false;
                foreach (PersonalityAddEncourage item in oneself.personalityAddEncourages)
                {
                    if (item.addEncourage > 0)
                    {
                        isNoHaveEncourage = true;
                        break;
                    }
                }
                if (!isNoHaveEncourage)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;
            case 11:  //队伍中至少有2人  
                int count = 0;
                foreach (CombatUnit item in combatUnits)
                {
                    if (item != null)
                    {
                        count++;
                    }
                }
                if (count > 2)
                {
                    AddTagAndReward(passiveSkill, oneself, combatUnits);
                }
                break;

        }
    }

    /// <summary>
    /// 添加标签和奖励
    /// </summary>
    /// <param name="passiveSkill"></param>
    /// <param name="oneself"></param>
    /// <param name="combatUnits"></param>
    /// <param name="targetUnit"></param>
    private void AddTagAndReward(PassiveSkill_template passiveSkill, CombatUnit oneself, List<CombatUnit> combatUnits, CombatUnit targetUnit = null)
    {
        bool isAddReward = false;
        PersonalityAddEncourage addEncourageInfo;
        //添加标签
        List<int> tags = new List<int>();
        foreach (int item in passiveSkill.addTag)
        {
            if (item != 0)
            {
                tags.Add(item);
            }
        }
        //有标签和奖励
        if (tags.Count > 0)
        {
            foreach (int tag in tags)
            {
                switch (tag)
                {
                    case 1:
                        //添加目标奖励
                        if (!isAddReward && passiveSkill.rewardType == 2)
                        {
                            if (targetUnit == null)
                            {
                                targetUnit = GetCharRightPos(oneself, combatUnits);
                            }
                            if (targetUnit != null)
                            {
                                targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                                {
                                    addEncourage = passiveSkill.addEncourage,
                                });
                            }
                        }
                        //添加自身标签和奖励
                        addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                        {
                            disableTag = tag,
                        };
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                        }
                        oneself.personalityAddEncourages.Add(addEncourageInfo);
                        break;
                    case 4://自己加
                           //添加目标奖励
                        if (!isAddReward && passiveSkill.rewardType == 2)
                        {
                            if (targetUnit == null)
                            {
                                targetUnit = GetCharRightPos(oneself, combatUnits);
                            }
                            if (targetUnit != null)
                            {
                                targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                                {
                                    disableTag = tag,
                                    addEncourage = passiveSkill.addEncourage,
                                });
                            }
                        }
                        //添加自身标签和奖励
                        addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                        {
                            disableTag = tag,
                        };
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                        }
                        oneself.personalityAddEncourages.Add(addEncourageInfo);
                        break;
                    case 2://目标加
                           //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        if (targetUnit == null)
                        {
                            targetUnit = GetCharRightPos(oneself, combatUnits);
                        }
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                    case 5://目标加
                           //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        if (targetUnit == null)
                        {
                            targetUnit = GetCharRightPos(oneself, combatUnits);
                        }
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                    case 3://右侧和左侧的角色
                           //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        targetUnit = GetCharRightPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                                isAddReward = true;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        targetUnit = GetCharLeftPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                                isAddReward = true;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                    case 6: //右侧和左侧的角色
                            //添加自身奖励
                        if (!isAddReward && passiveSkill.rewardType == 1)
                        {
                            oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                            {
                                addEncourage = passiveSkill.addEncourage,
                            });
                        }
                        //添加目标标签和奖励
                        targetUnit = GetCharRightPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        targetUnit = GetCharLeftPos(oneself, combatUnits);
                        if (targetUnit != null)
                        {
                            addEncourageInfo = new PersonalityAddEncourage(oneself.initIndex)
                            {
                                disableTag = tag,
                            };
                            if (!isAddReward && passiveSkill.rewardType == 2)
                            {
                                addEncourageInfo.addEncourage = passiveSkill.addEncourage;
                            }
                            targetUnit.personalityAddEncourages.Add(addEncourageInfo);
                        }
                        break;
                }
                isAddReward = true;
            }
            return;
        }
        //没有标签只有奖励
        switch (passiveSkill.rewardType)
        {
            case 1:
                oneself.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                {
                    addEncourage = passiveSkill.addEncourage,
                });
                break;
            case 2:
                //如果没有就优先加给右边的角色——有角色就直接加指定人
                if (targetUnit == null)
                {
                    targetUnit = GetCharRightPos(oneself, combatUnits);
                }
                if (targetUnit != null)
                {
                    targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                    {
                        addEncourage = passiveSkill.addEncourage,
                    });
                    break;
                }
                //右边没有角色加给左边的角色
                targetUnit = GetCharLeftPos(oneself, combatUnits);
                if (targetUnit != null)
                {
                    targetUnit.personalityAddEncourages.Add(new PersonalityAddEncourage(oneself.initIndex)
                    {
                        addEncourage = passiveSkill.addEncourage,
                    });
                }
                break;
        }
    }
    /// <summary>
    /// 获得关系数量
    /// </summary>
    /// <param name="combatUnit"></param>
    /// <param name="relationals"></param>
    /// <returns></returns>
    private int GetRelationalSum(CombatUnit combatUnit, List<int> relationals)
    {
        int sum = 0;
        foreach (PersonalityAddEncourage info in combatUnit.personalityAddEncourages)
        {
            if (info.disableTag == 4 && info.formeCharIndex != combatUnit.initIndex && relationals.Contains(4))
            {
                continue;
            }
            if (relationals.Contains(info.disableTag))
            {
                sum++;
            }
        }

        return sum;
    }

    private CombatUnit GetCharLeftPos(CombatUnit onself, List<CombatUnit> combatUnits)
    {
        return onself.initIndex == 3 ? null : combatUnits.Find(a => a.initIndex == onself.initIndex + 1);
    }
    private CombatUnit GetCharRightPos(CombatUnit onself, List<CombatUnit> combatUnits)
    {
        return onself.initIndex == 0 ? null : combatUnits.Find(a => a.initIndex == onself.initIndex - 1);
    }
    private int GetCharPos(CombatUnit onself, CombatUnit target)
    {
        if (onself.initIndex - target.initIndex == -1)
        {
            return 1;
        }
        if (onself.initIndex - target.initIndex == 1)
        {
            return 2;
        }
        return -1;
    }

    /// <summary>
    /// 当前能量
    /// </summary>
    private float _currentEnergy;
    /// <summary>
    /// 最大能量
    /// </summary>
    private readonly float maxPower;
}