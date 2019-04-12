using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 播放效果操作
/// </summary>
public class PlayEffectTool
{
    /// <summary>
    /// 获得特效显示位置
    /// </summary>
    /// <param name="effectResult"></param>
    /// <returns></returns>
    public static int GetEffectShowPos(CREffectResult effectResult)
    {
        int pos = 0;
        if (effectResult.isArmorDefend || effectResult.isArmorMar)
        {
            pos++;
        }
        if (effectResult.isShieldDefend || effectResult.isShieldMar)
        {
            pos++;
        }
        return pos;
    }

    /// <summary>
    /// 普通创建
    /// </summary>
    public static EffectInfo CreateEffectInfo(string RP_Name, string effectName, Transform carrier, int sortingOrde, object startPos = null, bool isLoop = false)
    {
        return new EffectInfo(RP_Name, effectName, carrier, sortingOrde, startPos, isLoop);
    }
    /// <summary>
    /// 技能效果创建
    /// </summary>
    public static EffectInfo CreateEffectInfo(ActionEffect effect, UICharUnit atkUnit, int hitTeam)
    {
        if (effect == null)
        {
            return null;
        }
        string resStr = effect.skillEventEffect;
        if (effect.simpleDisplay == 1)
        {
            resStr += "_" + atkUnit.combatUnit.charAttribute.templateID;
        }
        Transform carrier;
        Vector3 startWorldPos = Vector3.zero;
        Vector3 endLocalPos = Vector3.zero;
        int sortingOrder = 100;
        bool isMove = false;
        switch (effect.SECase)
        {
            case 1: //特效施放在受击方队伍中心，且不移动；
                carrier = UICombatTool.Instance.GetTeamUI(atkUnit.teamID).TeamTrans;
                bool isOneself = atkUnit.teamID == hitTeam;
                startWorldPos = UICombatTool.Instance.GetTeamUI(atkUnit.teamID, isOneself).TeamTrans.position;
                endLocalPos = GameTools.WorldToLocalPoint(startWorldPos, carrier);
                break;
            case 2: //特效从施放者处出现，并向队伍中心移动（爆炸箭）
                carrier = UICombatTool.Instance.GetTeamUI(atkUnit.teamID).TeamTrans;
                isMove = true;
                startWorldPos = atkUnit.moveTrans.position;
                //  endLocalPos = GameTools.WorldToLocalPoint(carrier.position, carrier);
                break;
            case 3: //特效从我方队伍中心出现，并向敌方队伍中心移动
                carrier = UICombatTool.Instance.GetTeamUI(atkUnit.teamID).TeamTrans;
                isMove = true;
                startWorldPos = atkUnit.moveTrans.position;
                endLocalPos = GameTools.WorldToLocalPoint(UICombatTool.Instance.GetTeamUI(atkUnit.teamID, false).TeamTrans.position, carrier);
                break;
            default:
                carrier = atkUnit.moveTrans;
                startWorldPos = atkUnit.BonePos(effect.origin);
                sortingOrder = atkUnit.AtkSortingOrder;
                break;
        }
        //
        EffectInfo info = new EffectInfo(resStr, resStr, carrier, sortingOrder, startWorldPos)
        {
            isMove = isMove,
            endLocalPos = endLocalPos,
        };



        return info;
    }

    /// <summary>
    /// 通用效果创建
    /// </summary>
    public static EffectInfo CreateEffectInfo(UICharUnit charUnit, string resName, string effectName, Vector3 startPos)
    {
        if (charUnit == null)
        {
            return null;
        }
        EffectInfo info = new EffectInfo(resName, effectName, charUnit.moveTrans, charUnit.HitSortingOrder, startPos, false);
        info.endLocalPos = info.PosAmend(startPos);
        return info;
    }
    /// <summary>
    /// 通用效果创建
    /// </summary>
    public static EffectInfo CreateEffectInfo(CommonEffectConfig effect, UICharUnit charUnit)
    {
        if (effect == null)
        {
            return null;
        }
        string resStr = effect.commonEffect;
        EffectInfo info = new EffectInfo(resStr, resStr, charUnit.moveTrans, charUnit.HitSortingOrder + effect.SOAmend,
            charUnit.BonePos(effect.origin), false, new List<float> { effect.CSYS_x, effect.CSYS_y });
        info.endLocalPos = info.PosAmend(info.endLocalPos);
        return info;
    }
    /// <summary>
    /// 状态效果创建
    /// </summary>
    public static EffectInfo CreateEffectInfo(StateEffectConfig effect, UICharUnit charUnit)
    {
        if (effect == null)
        {
            return null;
        }
        string originName = effect.origin != " " ? effect.origin : SkeletonTool.RootName;
        Vector3 stratPos = charUnit.BonePos(originName);
        EffectInfo info = new EffectInfo(effect.stateEffect, effect.stateEffect, charUnit.moveTrans,
            charUnit.HitSortingOrder + effect.SOAmend, stratPos, true, new List<float> { effect.CSYS_x, effect.CSYS_y });
        info.endLocalPos = info.PosAmend(stratPos);
        return info;
    }
    /// <summary>
    ///EO效果创建
    /// </summary>
    public static EffectInfo CreateEffectInfo(ObjectEffect effect, int sourceIndex, Transform carrier, Vector3 startPos, Vector3 endPos, int sortingOrde, bool isMove)
    {
        if (effect == null)
        {
            return null;
        }
        //  isMove = true;
        string resStr = effect.EOEventEffect;
        string effectName = effect.EOEventEffect;
        switch (effect.SECase)
        {
            case 1:
                effectName = sourceIndex.ToString();
                break;
        }
        EffectInfo effectInfo = new EffectInfo(resStr, effectName, carrier, sortingOrde, startPos)
        {
            isLoop = effect.loop == 1,
            isMove = isMove,
            endLocalPos = endPos,
        };
        effectInfo.waitFPS = effect.waitFPS != 0 ? effect.waitFPS : effectInfo.DefaultWaitFPS;
        if (effect.moveSpeed != 0)
        {
            effectInfo.moveSpeed = effect.moveSpeed;
        }
        if (effectInfo.isMove)
        {
            effectInfo.moveType = EffectMoveType.Line;
        }
        return effectInfo;
    }


    /// <summary>
    /// 播放Object特效
    /// </summary>
    /// <returns></returns>
    public static UIPlayEffect PlayObjectEffect(CRSkillEffectResult effectResult, ObjectEffect effect, Transform carrier, UICharUnit hitChar, int sourceIndex, UICharUnit atkChar, Action<UnityEngine.Object> onPlayEnd, List<float> erCSYS)
    {
        float temp = carrier.localScale.x;
        int sortingOrder;
        Vector3 endPos;
        string hitBoneName = effect.hitBone == " " ? SkeletonTool.HitBoneName : effect.hitBone;
        bool isMove = SkeletonTool.IsCheckEffectMove(carrier, carrier.parent.parent.name == LeftTansformName,
            (SkillAnimationPosType)effect.targetType, effect.CSYS_x, effect.CSYS_y, hitChar, atkChar, hitBoneName, temp, out endPos, out sortingOrder, erCSYS);
        Vector3 startPos;
        switch (effect.castType)
        {
            case 0:
                startPos = atkChar.BonePos(effect.origin);
                break;
            case 1:
                startPos = hitChar.BonePos(effect.origin);
                endPos = GameTools.WorldToLocalPoint(startPos, carrier);
                sortingOrder = hitChar.HitSortingOrder;
                break;
            default:
                startPos = atkChar.BonePos(effect.origin);
                break;
        }
        switch (effect.SECase)
        {
            case 1: //吸血
                isMove = false;
                endPos = startPos;
                endPos = carrier.InverseTransformPoint(endPos);
                endPos = GetPosAmend(endPos, temp, effect.CSYS_x, effect.CSYS_y);
                endPos = GetPosAmend(endPos, erCSYS, temp);
                sortingOrder = UICombatTool.Instance.GetTeamUI(atkChar.teamID, false).GetChar(sourceIndex).HitSortingOrder;
                break;
            default:
                // isMove = true;
                break;
        }
        //
        switch ((SkillAnimationPosType)effect.targetType)
        {
            case SkillAnimationPosType.NearEnemy:
                if (effectResult != null && effectResult.execState != null)
                {
                    endPos += Vector3.right * GetDefenseAmendX(effectResult.execState.effectResult, hitChar, hitBoneName, true);
                }
                break;
        }
        //
        EffectInfo effectInfo = CreateEffectInfo(effect, sourceIndex, carrier, startPos, endPos, sortingOrder, isMove);
        return PlayEffectInfo(effectInfo, onPlayEnd);
    }

    /// <summary>
    /// 播放公共特效
    /// </summary>
    /// <param name="commonEffectID">公共特效ID</param>
    /// <param name="hitChar">命中角色</param>
    /// <param name="trackTime">开始第几帧</param>
    /// <returns></returns>
    public static UIPlayEffect PlayCommonEffect(int commonEffectID, UICharUnit hitChar, float trackTime = 1f)
    {
        CommonEffectConfig effectConfig = CommonEffectConfigConfig.GetCommonEffectConfig(commonEffectID);
        if (effectConfig == null)
        {
            LogHelperLSK.Log("播放CommonEffect" + commonEffectID + "为空");
            return null;
        }
        EffectInfo effectInfo = CreateEffectInfo(effectConfig, hitChar);
        return PlayCommonEffectInfo(effectInfo.carrier.gameObject, effectInfo, effectConfig, hitChar.ActionOperation, null, trackTime);
    }

    /// <summary>
    /// 播放状态特效
    /// </summary>
    /// <param name="stateID">状态id</param>
    /// <param name="hitChar">命中角色</param>
    /// <returns></returns>
    public static UIPlayEffect PlayStateEffect(int stateID, UICharUnit hitChar)
    {
        State_template _stateTemplate = State_templateConfig.GetState_template(stateID);
        if (_stateTemplate == null)
        {
            return null;
        }
        //
        StateEffectConfig stateEffect = StateEffectConfigConfig.GetStateEffectConfig(_stateTemplate.stateEffect);
        //
        if (stateEffect == null)
        {
            return null;
        }
        //TODO 暂时没有播放角色动作
        return PlayEffectInfo(CreateEffectInfo(stateEffect, hitChar), null, 1, false);
    }

    /// <summary>
    /// 播放动作特效
    /// </summary>
    /// <param name="carrier">载体</param>
    /// <param name="onPlayEnd">播放完成事件</param>
    /// <param name="effectInfo">特效数据</param>
    /// <returns></returns>
    public static UIPlayEffect PlayActionEffect(GameObject carrier, Action<UnityEngine.Object> onPlayEnd, ActionEffect effect, UICharUnit atkUnit, int hitTeam, float trackTime = 1f)
    {
        return PlayEffectInfo(CreateEffectInfo(effect, atkUnit, hitTeam), onPlayEnd, trackTime);
    }

    /// <summary>
    /// 播放效果
    /// </summary>
    public static UIPlayEffect PlayEffect(UICharUnit charUnit, string resName, string effectName, Vector3 startPos, Action<UnityEngine.Object> onPlayEnd = null, float trackTime = 1f)
    {
        return PlayEffect(onPlayEnd, CreateEffectInfo(charUnit, resName, effectName, startPos), trackTime);
    }

    /// <summary>
    /// 播放效果
    /// </summary>
    public static UIPlayEffect PlayEffect(UICharUnit charUnit, string resName, Vector3 startPos, Action<UnityEngine.Object> onPlayEnd = null, float trackTime = 1f)
    {
        return PlayEffect(onPlayEnd, CreateEffectInfo(charUnit, resName, resName, startPos), trackTime);
    }

    /// <summary>
    /// 播放效果
    /// </summary>
    /// <param name="carrier"></param>
    /// <param name="effectInfo"></param>
    /// <param name="onPlayEnd"></param>
    /// <param name="trackTime"></param>
    /// <returns></returns>
    public static UIPlayEffect PlayEffect(EffectInfo effectInfo, Action<UnityEngine.Object> onPlayEnd = null, float trackTime = 1f)
    {
        return PlayEffect(onPlayEnd, effectInfo, trackTime);
    }
    /// <summary>
    /// 获得防御修正X轴
    /// </summary>
    /// <param name="effectResult"></param>
    /// <param name="charUnit"></param>
    /// <returns></returns>
    public static float GetDefenseAmendX(CREffectResult effectResult, UICharUnit charUnit, string hitBoneName = SkeletonTool.RootName, bool isIgnoreScale = false)
    {
        if (effectResult == null)
        {
            return 0;
        }
        int posType = GetEffectShowPos(effectResult);
        if (posType != 0)
        {
            Vector3 temp = GameTools.WorldToLocalPoint(charUnit.BonePos(hitBoneName), charUnit.moveTrans);
            Vector3 temp1 = temp;
            switch (posType)
            {
                case 1:
                    temp1 = GameTools.WorldToLocalPoint(charUnit.BonePos(SkeletonTool.OnHit1Name), charUnit.moveTrans);
                    break;
                case 2:
                    temp1 = GameTools.WorldToLocalPoint(charUnit.BonePos(SkeletonTool.OnHit2Name), charUnit.moveTrans);
                    break;
            }
            return (temp - temp1).x * (isIgnoreScale ? 1 : charUnit.moveTrans.localScale.x);
        }
        return 0;
    }

    /// <summary>
    /// 播放特效
    /// </summary>
    /// <param name="carrier">载体</param>
    /// <param name="onPlayEnd">播放完成事件</param>
    /// <param name="effectInfo">特效数据</param>
    /// <returns></returns>
    private static UIPlayEffect PlayEffect(Action<UnityEngine.Object> onPlayEnd, EffectInfo effectInfo, float trackTime = 1f)
    {
        return PlayEffectInfo(effectInfo, onPlayEnd, trackTime);
    }

    private static UIPlayEffect PlayEffectInfo(EffectInfo effectInfo, Action<UnityEngine.Object> onPlayEnd = null, float trackTime = 1f, bool isAutoDestory = true)
    {
        UIPlayEffect playEffect = effectInfo.carrier.gameObject.AddComponent<UIPlayEffect>();
        if (onPlayEnd != null)
        {
            playEffect.OnPlayEnd = onPlayEnd;
        }
        playEffect.PlayEffect(effectInfo, trackTime, isAutoDestory);

        return playEffect;
    }

    private static UIPlayEffect PlayCommonEffectInfo(GameObject carrier, EffectInfo effectInfo, CommonEffectConfig commonEffect, UICharActionOperation actionOperation, Action<UnityEngine.Object> onPlayEnd = null, float trackTime = 1f)
    {
        UIPlayEffect playEffect = carrier.AddComponent<UIPlayEffect>();
        if (onPlayEnd != null)
        {
            playEffect.OnPlayEnd = onPlayEnd;
        }
        playEffect.PlayCommonEffect(effectInfo, commonEffect, actionOperation, trackTime);

        return playEffect;
    }


    private static Vector3 GetPosAmend(Vector3 pos, float localScale, float x, float y)
    {
        return GameTools.AmendVector(pos, x / localScale, y / localScale);
    }

    public static Vector3 GetPosAmend(Vector3 vector3, List<float> CSYS, float localScale)
    {
        return GameTools.AmendVector(vector3, CSYS, localScale);
    }
    //
    private const string LeftTansformName = "Left";

}