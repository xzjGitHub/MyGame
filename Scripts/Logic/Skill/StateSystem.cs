using System.Collections.Generic;
using System.Linq;
using MCCombat;

/// <summary>
/// 状态系统
/// </summary>
public class StateSystem
{

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="state"></param>
    public static void AddState(List<StateAttribute> sources, StateAttribute stateAttribute)
    {
        switch (stateAttribute.stackType)
        {
            case 1:
                sources.Add(stateAttribute);
                break;
            case 2:
                StateAttribute temp = sources.Find(a => a.stateID == stateAttribute.stateID);
                if (temp != null)
                {
                    RemoveState(sources, temp);
                }
                sources.Add(stateAttribute);
                break;
        }
    }

    /// <summary>
    /// 移除状态
    /// </summary>
    /// <param name="stateType"></param>
    public static int RemoveState(List<StateAttribute> sources, int stateType)
    {
        return RemoveState(sources, stateType, sources.Count);
    }

    /// <summary>
    /// 移除状态
    /// </summary>
    public static int RemoveState(List<StateAttribute> sources, StateType stateType, int removeSum)
    {
        return RemoveState(sources, (int)stateType, removeSum);
    }
    /// <summary>
    /// 移除状态
    /// </summary>
    public static int RemoveState(List<StateAttribute> sources, int stateType, int removeSum)
    {
        if (sources == null)
        {
            return 0;
        }
        sources = sources.OrderBy(a => a.createRound).ToList();
        List<StateAttribute> lsit = sources.FindAll(a => a.StateType == (StateType)stateType);
        int count = lsit.Count;
        count = removeSum > count ? count : removeSum;
        for (int i = 0; i < count; i++)
        {
            RemoveState(sources, lsit[i]);
        }

        return count;
    }

    /// <summary>
    /// 移除状态
    /// </summary>
    public static void RemoveState(List<StateAttribute> sources, StateAttribute state)
    {
        if (sources == null)
        {
            return;
        }
        sources.Remove(state);
    }

    public static void RemoveState(List<StateAttribute> sources, List<StateAttribute> states)
    {
        //卸载为0的状态
        while (states.Count > 0)
        {
            RemoveState(sources, states[0]);
        }
    }


    /// <summary>
    /// 状态属性操作
    /// </summary>
    /// <param name="stateAttribute"></param>
    public static CREffectResult StateOperation(StateAttribute stateAttribute)
    {
        return stateAttribute.ExecuteEffect();
    }


 
  


    /// <summary>
    /// 是否加载状态
    /// </summary>
    public static bool IsLoadState(State_template template, HitResult hitResult, CombatUnit atkUnit, CombatUnit targetUnit, int nowRound, int stunnedIndex = -1, float finalValue = 0)
    {
        if (hitResult == HitResult.Miss)
        {
            return false;
        }

        if (template.skillEffect == (int)SkillEffect.JiYun)
        {
            float value = GetStunnedValue(targetUnit, nowRound, stunnedIndex);
            value = finalValue / (finalValue + value);
            if (RandomBuilder.RandomIndex_Chances(value) != 0)
            {
                return false;
            }
        }
        switch ((StateLoadType)template.loadType)
        {
            case StateLoadType.Any:
                return RandomBuilder.RandomIndex_Chances(new List<float> { template.loadChance * 10000 }) == 0;
            case StateLoadType.Frequency:
                List<FrequencyState> temps = atkUnit.FrequencyStates.FindAll(a => a.stateID == template.stateID);
                bool isCanLoad;
                if (temps.Count == 0)
                {
                    isCanLoad = true;
                }
                else
                {
                    //逆向排序
                    temps = temps.OrderByDescending(a => a.loadRound).ToList();
                    int quotient = temps.First().loadRound / template.loadFrequency;
                    isCanLoad = template.loadFrequency / nowRound != quotient;
                }
                //
                if (isCanLoad)
                {
                    atkUnit.AddFrequencyState(template, nowRound);
                }
                return isCanLoad;
            default:
                return RandomBuilder.RandomIndex_Chances(new List<float> { template.loadChance * 10000 }) == 0;
        }
    }


    private static float GetStunnedValue(CombatUnit targetUnit, int nowRound, int stunnedIndex)
    {
        if (!targetUnit.isMob || stunnedIndex < 0)
        {
            return 10000f;
        }
        return targetUnit.GetAutoUseSkillInfo(stunnedIndex + 1, nowRound).Combatskill.stunValue;
    }
}





/// <summary>
/// 状态
/// </summary>
public class State
{

    public State(int id, int skillID, CombatUnit combatUnit)
    {
        Init(id, skillID);
        teamID = combatUnit.teamId;
        charID = combatUnit.charAttribute.charID;
        charIndex = combatUnit.initIndex;
    }

    public State(int id, int skillID, int _teamID, int _charID, int _charIndex, float _finalValue, float _resultValue, HitResult _hitResult)
    {
        Init(id, skillID);
        fromSkillId = skillID;
        teamID = _teamID;
        charID = _charID;
        charIndex = _charIndex;
        finalValue = _finalValue;
        effectValue = _resultValue;
        hitResult = _hitResult;
    }



    /// <summary>
    /// 状态的ID
    /// </summary>
    public int stateID;
    /// <summary>
    /// 状态的持续周期计算参数
    /// </summary>
    public int duration;
    /// <summary>
    /// 来自的技能
    /// </summary>
    public int fromSkillId;
    /// <summary>
    /// 使用者队伍
    /// </summary>
    public int teamID;
    /// <summary>
    /// 使用角色
    /// </summary>
    public int charID;
    /// <summary>
    /// 使用角色索引
    /// </summary>
    public int charIndex;
    /// <summary>
    /// 最终值
    /// </summary>
    public float finalValue;
    /// <summary>
    /// 结果值
    /// </summary>
    public float effectValue;
    /// <summary>
    /// 技能效果
    /// </summary>
    public int skillEffect;
    /// <summary>
    /// 状态类型
    /// </summary>
    public int stateType;
    /// <summary>
    /// 共存类型
    /// </summary>
    public int stackType;
    /// <summary>
    /// 攻击类型
    /// </summary>
    public HitResult hitResult;
    /// <summary>
    /// 剩余值
    /// </summary>
    public int remainValue;

    public float HPPropReq;

    public int triggerType;

    public int canBeDispelled;

    private void Init(int id, int skillID)
    {
        State_template stateTemplate = State_templateConfig.GetState_template(id);
        stateID = id;
        fromSkillId = skillID;
        duration = stateTemplate.duration;
        skillEffect = stateTemplate.skillEffect;
        stateType = stateTemplate.stateType;
        HPPropReq = stateTemplate.HPPropReq;
        triggerType = stateTemplate.triggerType;
        canBeDispelled = stateTemplate.canBeDispelled;
        stackType = stateTemplate.stackType;
    }


    public CharAttribute atkAttribute;
    public CharAttribute oneselfAttribute;

    /// <summary>
    /// 真伤盾
    /// </summary>
    public float RDShield;
    /// <summary>
    /// 临时护盾
    /// </summary>
    public float tempShield;
    /// <summary>
    /// 护盾
    /// </summary>
    public float shield;
    /// <summary>
    /// 临时护甲
    /// </summary>
    public float tempArmor;
    /// <summary>
    /// 护甲
    /// </summary>
    public float armor;
    /// <summary>
    /// 生命
    /// </summary>
    public float hp;

    /*
        1、状态加载时记录这6个效果值存档： 真伤盾、临时护盾、护盾、临时护甲、护甲、生命
        2、通过存档效果值修正角色当前对应属性值


    

     */
}
