using MCCombat;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CombatSystem
{

    #region 加载状态操作

    /// <summary>
    /// 战斗单元加载状态
    /// </summary>
    /// <param name="skillID">技能id</param>
    /// <param name="stateIds">状态id列表</param>
    /// <param name="hitResult">攻击类型</param>
    /// <param name="attackUnit">攻击方角色信息</param>
    /// <param name="hitUnit">受击方角色信息</param>
    private List<CRExtraUseSkill> CombatUnitLoadXiXueState(Targetset_template targetset, CSkillInfo skillInfo, HitResult hitResult, CombatUnit attackUnit, CombatUnit hitUnit, float returnResultValue, out CRSkillEffect skillEffect, out float tempResultValue)
    {
        int skillID = skillInfo.ID;
        tempResultValue = 0;
        //todo hitResult修改为命中
        hitResult = HitResult.Hit;
        List<CRExtraUseSkill> extraUseSkills = new List<CRExtraUseSkill>();
        //技能效果赋值
        skillEffect = new CRSkillEffect
        {
            hitTeamId = hitUnit.teamId,
            hitCharId = hitUnit.charAttribute.charID,
            hitIndex = hitUnit.initIndex,
            hitResult = hitResult,
            skillEffectType = SkillEffectType.Charge, //默认设置：冲锋
            skillEffectResults = new List<CRSkillEffectResult>(),
        };
        int stateID;
        float tempValue;
        for (int i = 0; i < targetset.stateList.Count - 1; i++)
        {
            HitResult tempHitResult = hitResult;
            stateID = targetset.stateList[i];
            extraUseSkills.AddRange(AddStateInfo(skillInfo, targetset.targetSetID, stateID, attackUnit, hitUnit, tempHitResult, skillEffect, returnResultValue, out tempValue));
            tempResultValue += tempValue;
        }
        //
        return extraUseSkills;
    }

    /// <summary>
    /// 添加状态信息
    /// </summary>
    /// <param name="skillID"></param>
    /// <param name="targetSetID"></param>
    /// <param name="stateID"></param>
    /// <param name="attackUnit"></param>
    /// <param name="hitUnit"></param>
    /// <param name="hitResult"></param>
    /// <param name="skillEffect"></param>
    /// <param name="returnResultValue"></param>
    /// <param name="tempResultValue"></param>
    /// <returns></returns>
    private List<CRExtraUseSkill> AddStateInfo(CSkillInfo skillInfo, int targetSetID, int stateID, CombatUnit attackUnit, CombatUnit hitUnit, HitResult hitResult, CRSkillEffect skillEffect, float returnResultValue, out float tempResultValue)
    {
        int skillID = skillInfo.ID;
        List<CRExtraUseSkill> extraUseSkills = new List<CRExtraUseSkill>();
        tempResultValue = 0;
        stateTemplate = State_templateConfig.GetState_template(stateID);
        if (stateTemplate == null)
        {
            return extraUseSkills;
        }
        CRSkillEffectResult skillEffectResult = new CRSkillEffectResult();
        //Todo 状态属性操作----返回效果信息
        StateAttribute stateAttribute = new StateAttribute(skillInfo, targetSetID, stateID, new CombatUnit(attackUnit, true), hitUnit, nowRound, GetCombatTeamInfo(hitUnit.teamId), returnResultValue);
        //不能加载该状态
        if (!StateSystem.IsLoadState(stateTemplate, hitResult, attackUnit, hitUnit, nowRound, hitUnit.nowActionCount + 1, stateAttribute.finalValue))
        {
            skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.Miss;
            skillEffect.skillEffectResults.Add(skillEffectResult);
            return extraUseSkills;
        }
        //设置治疗标签
        if (stateTemplate.HealingTag == 1)
        {
            GetTeamInfo(hitUnit.teamId).SetHealingTag(hitUnit.initIndex, nowRound);
        }
        CREffectResult effectResult = new CREffectResult();
        //加载状态效果
        switch ((StateType)stateTemplate.stateType)
        {
            case StateType.Constant:
                skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
                effectResult = stateAttribute.AddState();
                break;
            case StateType.Impulse:
                skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
                effectResult = stateAttribute.AddState();
                break;
            case StateType.Instant:
                skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.ExecEffect;
                effectResult = stateAttribute.ExecuteEffect();
                tempResultValue = stateAttribute.returnResultValue;
                break;
            case StateType.Aura:
                skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
                effectResult = stateAttribute.AddState();
                break;
            case StateType.Incentive:
                skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
                effectResult = stateAttribute.AddState();
                break;
            case StateType.HpTrigger:
                break;
            case StateType.HitTrigger:
                break;
            case StateType.SuperImpulse:
                skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
                effectResult = stateAttribute.AddState();
                break;
        }
        skillEffectResult.execState = new CRExecState(attackUnit, skillID, stateID, effectResult, stateAttribute);
        skillEffect.skillEffectResults.Add(skillEffectResult);
        //不是瞬间生效就添加状态
        if ((StateType)stateTemplate.stateType != StateType.Instant)
        {
            hitUnit.AddState(stateAttribute);
        }
        //检查是否暴击
        //if (stateAttribute.isCritical)
        //{
        //    hitResult = HitResult.Critical;
        //}

        #region 前置和后置效果---先不做

        //SkillAttribute skillAttribute = attackUnit.charAttribute.skillAttribute.Find(a => a.skillId == skillID);
        //if (skillAttribute != null)
        //{
        //    skillAttribute.charAttribute = attackUnit.charAttribute;
        //    skillAttribute.targetAttribute = hitUnit.charAttribute;
        //    skillAttribute.targetset_template = _targetset;
        //}
        // 
        //skillAttribute.skillEffect = stateTemplate.skillEffect;
        //float _finalvalue = GetStateFinalValue(skillAttribute);
        ////添加状态 Final保存，
        //float _resultValue = GetStateResultValue(stateTemplate, hitResult, hitUnit, skillAttribute);
        ////
        //int triggerType;
        //StateAttribute state;
        //bool _isTrigger;
        ////状态生命反应触发
        //_isTrigger = IsStateHpTrigger(hitUnit, _resultValue, out triggerType, out state);
        ////生命反应触发了
        //if (_isTrigger)
        //{
        //    switch (triggerType)
        //    {
        //        case 1: //前置
        //            StateFrontEffect(attackUnit, hitUnit, hitResult, currentStateID, _finalvalue, state, skillID, skillEffect);
        //            break;
        //        case 2: //后置
        //            StateBehindEffect(attackUnit, hitUnit, hitResult, currentStateID, _finalvalue, state, skillID, skillEffect, extraUseSkills);
        //            break;
        //    }
        //}
        ////状态受击反应触发 -只能在技能效果为伤害是检测
        //if ((SkillEffect)stateTemplate.skillEffect == SkillEffect.WuShang)
        //{
        //    List<State> states = new List<State>();
        //    _isTrigger = IsStateHitTrigger(hitUnit, out states);
        //    if (_isTrigger)
        //    {
        //        //所有状态生效
        //        for (int j = 0; j < states.Count; j++)
        //        {
        //            StateBehindEffect(attackUnit, hitUnit, hitResult, currentStateID, _finalvalue, states[j], skillID, skillEffect, extraUseSkills);
        //        }
        //    }
        //}

        #endregion

        //没有触发状态
        // if (!_isTrigger)
        //   {
        // AddSkillEffectResult(attackUnit, hitUnit, hitResult, stateAttribute.finalValue, skillID, currentStateID, skillEffect);
        //  }
        return extraUseSkills;
    }

    /// <summary>
    /// 战斗单元加载状态
    /// </summary>
    /// <param name="skillID">技能id</param>
    /// <param name="stateIds">状态id列表</param>
    /// <param name="hitResult">攻击类型</param>
    /// <param name="attackUnit">攻击方角色信息</param>
    /// <param name="hitUnit">受击方角色信息</param>
    private List<CRExtraUseSkill> CombatUnitLoadState(Targetset_template targetset, CSkillInfo skillInfo, List<int> stateIds, HitResult hitResult, CombatUnit attackUnit, CombatUnit hitUnit, out CRSkillEffect skillEffect, out float returnResultValue)
    {
        int skillID = skillInfo.ID;
        returnResultValue = 0;
        bool isXixue = (TargetType)targetset.targetType == TargetType.XiXue;
        //todo hitResult修改为命中
        hitResult = HitResult.Hit;
        List<CRExtraUseSkill> extraUseSkills = new List<CRExtraUseSkill>();
        //技能效果赋值
        skillEffect = new CRSkillEffect
        {
            hitTeamId = hitUnit.teamId,
            hitCharId = hitUnit.charAttribute.charID,
            hitIndex = hitUnit.initIndex,
            hitResult = hitResult,
            skillEffectType = SkillEffectType.Charge, //默认设置：冲锋
            skillEffectResults = new List<CRSkillEffectResult>(),
        };
        int count = isXixue ? stateIds.Count - 1 : stateIds.Count;
        //遍历状态列表
        for (int i = 0; i < count; i++)
        {
            int stateID = stateIds[i];
            stateTemplate = State_templateConfig.GetState_template(stateID);
            if (stateTemplate == null)
            {
                continue;
            }
            float temp;
            extraUseSkills.AddRange(AddStateInfo(skillInfo, targetset.targetSetID, stateID, attackUnit, hitUnit, hitResult, skillEffect, returnResultValue, out temp));
            returnResultValue += temp;
            //todo 临时测试状态属性
            #region 测试
            //CRSkillEffectResult skillEffectResult = new CRSkillEffectResult();

            //StateAttribute stateAttribute = new StateAttribute(skillInfo, targetset.targetSetID, stateID, new CombatUnit(attackUnit, true), hitUnit, nowRound, GetCombatTeamInfo(hitUnit.teamId), returnResultValue);
            ////不能加载该状态
            //if (!StateSystem.IsLoadState(stateTemplate, hitResult, attackUnit, hitUnit, nowRound, hitUnit.nowActionCount + 1, stateAttribute.finalValue))
            //{
            //    skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.Miss;
            //    skillEffect.skillEffectResults.Add(skillEffectResult);
            //    continue;
            //}
            //CREffectResult effectResult = new CREffectResult();
            ////加载状态效果
            //switch ((StateType)stateTemplate.stateType)
            //{
            //    case StateType.Constant:
            //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
            //        effectResult = stateAttribute.AddState();
            //        break;
            //    case StateType.Impulse:
            //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
            //        effectResult = stateAttribute.AddState();
            //        break;
            //    case StateType.Instant:
            //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.ExecEffect;
            //        effectResult = stateAttribute.ExecuteEffect();
            //        returnResultValue = stateAttribute.returnResultValue;
            //        break;
            //    case StateType.Aura:
            //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
            //        effectResult = stateAttribute.AddState();
            //        break;
            //    case StateType.Incentive:
            //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
            //        effectResult = stateAttribute.AddState();
            //        break;
            //    case StateType.HpTrigger:
            //        break;
            //    case StateType.HitTrigger:
            //        break;
            //    case StateType.SuperImpulse:
            //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
            //        effectResult = stateAttribute.AddState();
            //        break;
            //}
            //skillEffectResult.execState = new CRExecState(attackUnit, skillID, effectResult);
            //skillEffect.skillEffectResults.Add(skillEffectResult);
            //skillEffectResult.execState = new CRExecState(attackUnit, skillID, effectResult);
            //skillEffect.skillEffectResults.Add(skillEffectResult);
            ////不是瞬间生效就添加状态
            //if ((StateType)stateTemplate.stateType != StateType.Instant)
            //{
            //    hitUnit.AddState(stateAttribute);
            //}
            ////检查是否暴击
            //if (stateAttribute.isCritical)
            //{
            //    hitResult = HitResult.Critical;
            //}

            #region 前置和后置效果---先不做

            //SkillAttribute skillAttribute = attackUnit.charAttribute.skillAttribute.Find(a => a.skillId == skillID);
            //if (skillAttribute != null)
            //{
            //    skillAttribute.charAttribute = attackUnit.charAttribute;
            //    skillAttribute.targetAttribute = hitUnit.charAttribute;
            //    skillAttribute.targetset_template = _targetset;
            //}
            //skillAttribute.skillEffect = stateTemplate.skillEffect;
            //float _finalvalue = GetStateFinalValue(skillAttribute);
            ////添加状态 Final保存，
            //float _resultValue = GetStateResultValue(stateTemplate, hitResult, hitUnit, skillAttribute);
            ////
            //int triggerType;
            //StateAttribute state;
            //bool _isTrigger;
            ////状态生命反应触发
            //_isTrigger = IsStateHpTrigger(hitUnit, _resultValue, out triggerType, out state);
            ////生命反应触发了
            //if (_isTrigger)
            //{
            //    switch (triggerType)
            //    {
            //        case 1: //前置
            //            StateFrontEffect(attackUnit, hitUnit, hitResult, currentStateID, _finalvalue, state, skillID, skillEffect);
            //            break;
            //        case 2: //后置
            //            StateBehindEffect(attackUnit, hitUnit, hitResult, currentStateID, _finalvalue, state, skillID, skillEffect, extraUseSkills);
            //            break;
            //    }
            //}
            ////状态受击反应触发 -只能在技能效果为伤害是检测
            //if ((SkillEffect)stateTemplate.skillEffect == SkillEffect.WuShang)
            //{
            //    List<State> states = new List<State>();
            //    _isTrigger = IsStateHitTrigger(hitUnit, out states);
            //    if (_isTrigger)
            //    {
            //        //所有状态生效
            //        for (int j = 0; j < states.Count; j++)
            //        {
            //            StateBehindEffect(attackUnit, hitUnit, hitResult, currentStateID, _finalvalue, states[j], skillID, skillEffect, extraUseSkills);
            //        }
            //    }
            //}

            #endregion

            //没有触发状态
            // if (!_isTrigger)
            //   {
            // AddSkillEffectResult(attackUnit, hitUnit, hitResult, stateAttribute.finalValue, skillID, currentStateID, skillEffect);
            //  }
            #endregion
        }
        return extraUseSkills;
    }

    /// <summary>
    /// 添加技能效果结果
    /// </summary>
    private void AddSkillEffectResult(CombatUnit attackUnit, CombatUnit hitUnit, HitResult hitResult, float finalvalue, int skillID, int stateID, CRSkillEffect skillEffect)
    {
        //先计算
        State_template template = State_templateConfig.GetState_template(stateID);
        if (template == null)
        {
            return;
        }
        CRSkillEffectResult crSkillEffectResult = new CRSkillEffectResult();
        float _resultValue = finalvalue;
        //
        int _attackTeamId = attackUnit == null ? 0 : attackUnit.teamId;
        int _attackCharID = attackUnit == null ? 0 : attackUnit.charAttribute.charID;
        int _attackIndex = attackUnit == null ? 0 : attackUnit.initIndex;
        //新建状态
        State _state = new State(stateID, skillID, _attackTeamId, _attackCharID, _attackIndex, finalvalue, _resultValue, hitResult);
        GetCRSkillEffectResult(skillID, _state, attackUnit, hitUnit, crSkillEffectResult);
        skillEffect.skillEffectResults.Add(crSkillEffectResult);
    }

    /// <summary>
    /// 得到技能效果结果
    /// </summary>
    private void GetCRSkillEffectResult(int skillID, State state, CombatUnit attackUnit, CombatUnit hitUnit, CRSkillEffectResult effectResult)
    {
        State_template template = State_templateConfig.GetState_template(state.stateID);
        if (template == null)
        {
            return;
        }

        int attackTeamId = attackUnit == null ? 0 : attackUnit.teamId;
        int attackCharId = attackUnit == null ? 0 : attackUnit.charAttribute.charID;
        int attackIndex = attackUnit == null ? 0 : attackUnit.initIndex;
        //新增状态信息
        effectResult.execState = new CRExecState(attackTeamId, attackCharId, attackIndex, skillID, state.stateID)
        {
            effectResult = new CREffectResult(),
            stateInfo = new UnitStateInfo(state),
        };
        //状态效果修正
        StateEffectAmend(template, hitUnit, state, effectResult);
    }

    /// <summary>
    /// 状态效果修正
    /// </summary>
    private void StateEffectAmend(State_template template, CombatUnit combatUnit, State _state, CRSkillEffectResult skillEffectResult)
    {
        ////加载状态效果
        //switch ((StateType)template.stateType)
        //{
        //    case StateType.Constant:
        //        combatUnit.AddState(_state);
        //        //
        //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
        //        //
        //        AmendCombatUnitAttribute_State(combatUnit, _state, skillEffectResult.execState.effectResult);
        //        break;
        //    case StateType.Impulse:
        //        combatUnit.AddState(_state);
        //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.AddState;
        //        //添加状态
        //        skillEffectResult.execState.effectResult.hitTeamId = combatUnit.teamId;
        //        skillEffectResult.execState.effectResult.hitCharId = combatUnit.charAttribute.charID;
        //        skillEffectResult.execState.effectResult.hitIndex = combatUnit.initIndex;
        //        skillEffectResult.execState.effectResult.currentHp = combatUnit.hp;
        //        skillEffectResult.execState.effectResult.maxHp = combatUnit.maxHp;
        //        skillEffectResult.execState.effectResult.CrStateEffectType = CRStateEffectType.AddState;
        //        break;
        //    case StateType.Instant:
        //        skillEffectResult.CrSkillEffectResultType = CRSkillEffectResultType.ExecEffect;
        //        AmendCombatUnitAttribute_State(combatUnit, _state, skillEffectResult.execState.effectResult);
        //        break;
        //}
    }



    /// <summary>
    ///  得到攻击类型
    /// </summary>
    private HitResult GetHitResult(float _criticalHitChance, float _finalBlockChance)
    {
        //最终格挡率
        float resultBlockChance = _finalBlockChance;
        //最终命中率
        float resultHitChance = 1 - _criticalHitChance - resultBlockChance;
        //无命中率
        if (resultHitChance <= 0)
        {
            //修正的格挡率
            float tempResultBlockChance = resultBlockChance / (resultBlockChance + _criticalHitChance);
            //修正的暴击率
            float tempCriticalHitChance = _criticalHitChance / (resultBlockChance + _criticalHitChance);
            //得到随机数
            return
                RandomBuilder.RandomIndex_Chances(new List<float>
                {
                    tempResultBlockChance*RandomBuilder.RandomMaxFactor,
                    tempCriticalHitChance*RandomBuilder.RandomMaxFactor
                }) == 0
                    ? HitResult.Block
                    : HitResult.Critical;
        }
        //得到随机数
        switch (RandomBuilder.RandomIndex_Chances(new List<float>
        {
            resultHitChance * RandomBuilder.RandomMaxFactor,
            resultBlockChance * RandomBuilder.RandomMaxFactor, _criticalHitChance* RandomBuilder.RandomMaxFactor
        }))
        {
            case 0:
                return HitResult.Hit;
            case 1:
                return HitResult.Block;
            case 2:
                return HitResult.Critical;
            default:
                return HitResult.Hit;
        }
    }


    /// <summary>
    /// 战斗单元状态更新
    /// </summary>
    /// <param name="combatUnit">战斗单元</param>
    /// <param name="combatRound">战斗回合</param>
    private void CombatUnitStateUpdate(CombatUnit combatUnit, CombatRound combatRound)
    {
        //todo 执行SuperImpulse没有做演示效果
        //执行SuperImpulse
        List<StateAttribute> lists = combatUnit.States.FindAll(a => a.createRound == nowRound).ToList();
        if (lists.Count - 1 >= combatUnit.nowActionCount)
        {
            StateAttribute temp = lists[combatUnit.nowActionCount];
            if (temp.StateType == StateType.SuperImpulse)
            {
                lists[combatUnit.nowActionCount].ExecuteEffect();
                temp.duration--;
            }
        }
        //本回合已经更新过了
        if (combatUnit.IsStateUpdate)
        {
            return;
        }
        combatUnit.UpdateSateInfo(true);
        //没有可用状态
        if (combatUnit.States.Count <= 0)
        {
            return;
        }
        CombatRoundResult result = new CombatRoundResult(CommandResultType.ImpulseEffect)
        {
            index = combatUnit.initIndex,
            teamID = combatUnit.teamId,
            charID = combatUnit.charAttribute.charID,
            resultType = CommandResultType.ImpulseEffect,
            impulseEffect = new CRImpulseEffect(),
            targetIndexs = new List<int> { combatUnit.initIndex },
        };
        //脉冲效果
        CRImpulseEffect _impulseEffect = new CRImpulseEffect
        {
            index = combatUnit.initIndex,
            teamID = combatUnit.teamId,
            charID = combatUnit.charAttribute.charID,
            skillEffectResults = new List<CRSkillEffectResult>(),
        };
        //1、清除duration = 0的脉冲、常效状态
        bool isHaveState = ClearInvalidState(combatUnit, new List<StateType> { StateType.Impulse }, null, _impulseEffect);
        if (combatUnit.States.Count <= 0)
        {
            return;
        }
        //2、 脉冲状态生效
        StateType stateType;
        StateAttribute state;
        for (int i = 0; i < combatUnit.States.Count; i++)
        {
            state = combatUnit.States[i];
            stateType = (StateType)state.stateType;
            if (stateType != StateType.Impulse || state.duration <= 0)
            {
                continue;
            }

            isHaveState = true;
            //
            CRSkillEffectResult skillEffectResult = new CRSkillEffectResult { CrSkillEffectResultType = CRSkillEffectResultType.ExecEffect, };
            CRExecState execState = new CRExecState(state, 0);
            //   AmendCombatUnitAttribute_State(_combatUnit, state, execState.effectResult);
            //
            skillEffectResult.execState = execState;
            _impulseEffect.skillEffectResults.Add(skillEffectResult);
        }
        //
        if (isHaveState)
        {
            result.impulseEffect = _impulseEffect;
            combatRound.combatRoundResults.Add(result);
        }
        //3、更新脉冲、常效状态的持续时间，duration = duration -1
        UpdateCombatStateDuration(combatUnit, new List<StateType> { StateType.Impulse });
    }

    /// <summary>
    /// 更新战斗单元指定类型回合
    /// </summary>
    /// <param name="combatUnit"></param>
    /// <param name="types"></param>
    private void UpdateCombatStateDuration(CombatUnit combatUnit, List<StateType> types, CRRemoveState removeState = null)
    {
        //清理无效状态
        ClearInvalidState(combatUnit, types, removeState);
        //
        for (int i = 0; i < combatUnit.States.Count; i++)
        {
            StateAttribute state = combatUnit.States[i];
            StateType stateType = (StateType)state.stateType;
            if (!types.Contains(stateType))
            {
                continue;
            }
            state.duration--;
        }
    }

    /// <summary>
    /// 清理无效状态
    /// </summary>
    private bool ClearInvalidState(CombatUnit combatUnit, List<StateType> types, CRRemoveState removeState = null, CRImpulseEffect impulseEffect = null)
    {
        bool isHaveState = false;
        bool isRemove = false;
        while (true)
        {
            isRemove = false;
            StateType stateType;
            StateAttribute state;
            for (int i = 0; i < combatUnit.States.Count; i++)
            {
                state = combatUnit.States[i];
                stateType = (StateType)state.stateType;
                //瞬时状态直接忽略
                if (!types.Contains(stateType))
                {
                    continue;
                }
                //回合数归零了
                if (state.duration <= 0)
                {
                    isHaveState = true;
                    //临时生命卸载-----最大生命和当前生命修复
                    if (state.skillEffect == (int)SkillEffect.LinShiShengMing)
                    {
                        combatUnit.maxHp -= (int)state.tempValue;
                        combatUnit.hp = Math.Min(combatUnit.maxHp, combatUnit.hp);
                    }
                    //移除状态
                    RemoveUnitState(combatUnit, state, removeState, impulseEffect);
                    isRemove = true;
                    break;
                }
                //临时护甲、护盾卸载---剩余临时防御属性 = 0
                if (state.skillEffect == (int)SkillEffect.LinShiHuDun && state.tempPeriodShield <= 0)
                {
                    isHaveState = true;
                    //移除状态
                    RemoveUnitState(combatUnit, state, removeState, impulseEffect);
                    isRemove = true;
                }
                if (state.skillEffect == (int)SkillEffect.LinShiHuJia && state.tempPeriodArmor <= 0)
                {
                    isHaveState = true;
                    //移除状态
                    RemoveUnitState(combatUnit, state, removeState, impulseEffect);
                    isRemove = true;
                }
                //判断光环状态
                if (stateType == StateType.Aura)
                {
                    if (GetCombatUnitInfo(state.teamID, state.charIndex).hp > 0)
                    {
                        continue;
                    }
                    isHaveState = true;
                    //移除状态
                    RemoveUnitState(combatUnit, state, removeState, impulseEffect);
                    isRemove = true;
                }
                break;
            }
            if (!isRemove)
            {
                break;
            }
        }
        //
        return isHaveState;
    }

    /// <summary>
    /// 移除状态
    /// </summary>
    private void RemoveUnitState(CombatUnit combatUnit, StateAttribute state, CRRemoveState removeState, CRImpulseEffect impulseEffect)
    {
        state.duration = 0;
        //
        if (removeState != null)
        {
            removeState.stateID.Add(state.stateID);
        }
        //
        if (impulseEffect != null)
        {
            CRSkillEffectResult skillEffectResult = new CRSkillEffectResult { CrSkillEffectResultType = CRSkillEffectResultType.RemoveState, };
            CRExecState execState = new CRExecState(state, 0,false)
            {
                effectResult = new CREffectResult { CrStateEffectType = CRStateEffectType.RemoveState, stateID = state.stateID, },
            };
            skillEffectResult.execState = execState;
            impulseEffect.skillEffectResults.Add(skillEffectResult);
        }
        //移除状态
        combatUnit.RemoveState(state);
    }



    #region 前置和后置效果 ---先不做
    /// <summary>
    /// 状态前置效果
    /// </summary>
    private void StateFrontEffect(CombatUnit attackUnit, CombatUnit hitUnit, HitResult hitResult, int stateID, float finalvalue,
        State frontState, int skillID, CRSkillEffect skillEffect)
    {
        //前置生效
        State_template template = State_templateConfig.GetState_template(frontState.stateID);
        if (template != null)
        {
            CRSkillEffectResult extraEffect = new CRSkillEffectResult
            {
                CrSkillEffectResultType = CRSkillEffectResultType.Extra
            };
            GetCRSkillEffectResult(skillID, frontState, hitUnit, hitUnit, extraEffect);
            skillEffect.skillEffectResults.Add(extraEffect);
        }
        //继续计算
        AddSkillEffectResult(attackUnit, hitUnit, hitResult, finalvalue, skillID, stateID, skillEffect);
    }

    /// <summary>
    /// 状态后置效果
    /// </summary>
    private void StateBehindEffect(CombatUnit attackUnit, CombatUnit hitCUnit, HitResult hitResult, int stateID, float finalvalue,
        State behindState, int skillID, CRSkillEffect skillEffect, List<CRExtraUseSkill> extraUseSkills)
    {
        //先正常计算
        AddSkillEffectResult(attackUnit, hitCUnit, hitResult, finalvalue, skillID, stateID, skillEffect);
        //
        if (hitCUnit.hp <= 0)
        {
            return;
        }

        State_template template = State_templateConfig.GetState_template(behindState.stateID);
        if (template == null)
        {
            return;
        }

        extraUseSkills.Add(new CRExtraUseSkill
        {
            castTeamId = hitCUnit.teamId,
            castCharId = hitCUnit.charAttribute.charID,
            castIndex = hitCUnit.initIndex,
            castSkillId = template.triggerSkill,
        });
    }


    /// <summary>
    /// 状态生命反应触发
    /// </summary>
    private bool IsStateHpTrigger(CombatUnit combatUnit, float resultValue, out int triggerType, out StateAttribute state)
    {
        state = null;
        triggerType = 0;
        List<StateAttribute> states = combatUnit.charAttribute.stateAttribute.FindAll(a => (StateType)a.stateType == StateType.HpTrigger).ToList();
        if (states.Count == 0)
        {
            return false;
        }
        float HPProp = (combatUnit.hp - (int)resultValue) / (float)combatUnit.maxHp;
        foreach (StateAttribute item in states)
        {
            if (HPProp > item.HPPropReq)
            {
                continue;
            }
            //
            triggerType = item.triggerType;
            state = item;
            //卸载状态
            combatUnit.RemoveState(state);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 状态生命反应触发
    /// </summary>
    private bool IsStateHitTrigger(CombatUnit combatUnit, out List<StateAttribute> state)
    {
        state = combatUnit.charAttribute.stateAttribute.FindAll(a => (StateType)a.stateType == StateType.HitTrigger).ToList();
        return state.Count != 0;
    }




    #endregion

    #endregion
}
