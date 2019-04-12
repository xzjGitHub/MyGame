using GameEventDispose;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public float leftSize = 36;

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(bool isTest, float size, Transform ui, Transform effect, Transform logic)
    {
        leftSize = size;
        this.isTest = isTest;
        //
        GetObj(ui, effect, logic);
        combatUiInfo.gameObject.SetActive(false);
        //销毁事件
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatStageEvent);
        //添加事件
        EventDispatcher.Instance.CombatEvent.AddEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatStageEvent);
        //注册播放Effect事件
        EventDispatcher.Instance.CombatEffect.AddEventListener<PlayCombatEffect, object>(EventId.CombatEffect, OnPlayCombatEffect);
    }

    /// <summary>
    /// 战斗事件
    /// </summary>
    private void OnCombatEvent(CombatEventType _type, object param)
    {
        switch (_type)
        {
            case CombatEventType.CastSkillOk:
                break;
            case CombatEventType.ImpulseEffectOk:
                break;
            case CombatEventType.CombatWin:
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.ResetRes, TeamType.Enemy, (object)null);
                break;
            case CombatEventType.CombatFail:
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.ResetRes, TeamType.Enemy, (object)null);
                break;
            case CombatEventType.CombatResult:
                //
                EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
                combatResultInfo.Reset();
                //发送待机
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.TeamAction, TeamType.Player,
                    (object)new object[] { CharModuleAction.Idle, false });
                //关闭敌人显示
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.SetActive, TeamType.Enemy, (object)false);
                //
                break;
        }
    }

    /// <summary>
    /// 战斗阶段事件
    /// </summary>
    private void OnCombatStageEvent(PlayCombatStage arg1, object arg2)
    {
        switch (arg1)
        {
            case PlayCombatStage.CreateCombat:
            case PlayCombatStage.Opening:
                break;
            case PlayCombatStage.CombatPrepare:
                //添加UICombatTeam
                combatSystem = arg2 as CombatSystem;
                //初始化自己
                UICombatTeam combatTeam = leftTrans.gameObject.AddComponent<UICombatTeam>();
                combatTeam.Init(combatSystem.PlayerTeamInfo, combatTrans);
                combatTeams.Add(combatTeam);
                //初始化敌方
                combatTeam = rightTrans.gameObject.AddComponent<UICombatTeam>();
                combatTeam.Init(combatSystem.EnemyTeamInfo, combatTrans);
                combatTeams.Add(combatTeam);
                //初始化第三方
                combatTeam = thirdpartyTrans.gameObject.AddComponent<UICombatTeam>();
                combatTeam.Init(
                    new CombatTeamInfo(-1, TeamType.Thirdparty),
            /*        new List<CombatUnit>{ new CombatUnit(TeamSystem.Instance.TeamAttribute.charAttribute[0],0)}) , */combatTrans);
                combatTeams.Add(combatTeam);
                //初始化战斗UI
                combatUiInfo.Init(isTest);
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateRound, (object)null);
                break;
            case PlayCombatStage.CreateRound:
            case PlayCombatStage.ChooseSkill:
                break;
            case PlayCombatStage.ImmediateSkillEffect:
                EventDispatcher.Instance.CombatEffect.DispatchEvent(EventId.CombatEffect, PlayCombatEffect.ImmediateSkillEffect, arg2);
                break;
            case PlayCombatStage.RoundInfo:
                IE_PlayCombatRoundInfo = new CoroutineUtil(NewPlayCombatRoundInfo((CombatRound)arg2));
                break;
            case PlayCombatStage.CombatEnd:
            case PlayCombatStage.InitLeft:
            case PlayCombatStage.InitRight:
                break;
            case PlayCombatStage.InitRes:
                break;
            case PlayCombatStage.PlayRoundInfoEnd:
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.RoundEnd, arg2);
                break;
            case PlayCombatStage.RoundEnd:
                CombatRound _combatRound = arg2 as CombatRound;
                if (!_combatRound.combatResult.isEnd || combatSystem.isTestSkill)
                {
                    EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateRound, (object)null);
                    return;
                }
                //
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CombatEnd, (object)null);
                //
                IE_PlayCombatEnd = new CoroutineUtil(PlayCombatEnd(_combatRound.combatResult));
                break;
            case PlayCombatStage.ImmediateShow:
                OnImmediateShowEffect(arg2 as List<CRImmediateShowEffect>);
                break;
        }
    }

    /// <summary>
    /// 立即显示效果
    /// </summary>
    /// <param name="effects"></param>
    private void OnImmediateShowEffect(List<CRImmediateShowEffect> effects)
    {
        if (effects == null)
        {
            return;
        }
        //
        bool isShield = effects.Any(a => a.effectType == ImmediateShowEffectType.ShieldRecover);
        bool isArmor = effects.Any(a => a.effectType == ImmediateShowEffectType.ArmorRecover);
        foreach (CRImmediateShowEffect item in effects)
        {
            EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.HPCost, new UICharInfo(item.effectResult),
                (object)item.effectResult);

            if (isShield && isArmor)
            {
                if (item.effectType == ImmediateShowEffectType.ArmorRecover)
                {
                    continue;
                }
            }

            playImmediateShowEffect = playEffectTrans.gameObject.AddComponent<UIPlayImmediateShowEffect>();
            playImmediateShowEffect.StartPlay(item);
        }
    }

    /// <summary>
    /// 播放战斗结束
    /// </summary>
    private IEnumerator PlayCombatEnd(CombatResult combatResult)
    {
        if ((GameModules.Find(ModuleName.combatSystem) as CombatSystem).isTestSkill)
        {
            yield break;
        }

        combatUiInfo.gameObject.SetActive(false);
        if (combatResult.victoryTeam == 0)
        {
            combatResultInfo.PlayWin();
            //播放胜利动画
            EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.Celebrate, combatResult.victoryTeam, (object)null);
            //
            ComabatTeamTest teamTest = new ComabatTeamTest();
            EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.OpenUpgradeShow, TeamType.Player, (object)teamTest);
            while (teamTest.combatTeam == null)
            {
                yield return null;
            }
            //
            while (!teamTest.combatTeam.IsPlayCharUpgradeShowOk)
            {
                yield return null;
            }
            //
            //  leftCombatManager.RemoveActionLoop();
            yield return new WaitForSeconds(0.5f);
            EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.CloseUpgradeShow, TeamType.Player, (object)null);
        }
        else
        {
            combatResultInfo.PlayLose();
        }
        combatEndPopup.param = combatResult.victoryTeam == 0;
        combatEndPopup.OnClose = OnCombatEndPopupClose;
        combatEndPopup.Show();
        //播放战斗结果
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.CombatEnd, (object)(combatResult.victoryTeam == 0));
        //关闭敌人显示
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.SetActive, TeamType.Enemy, (object)false);
    }

    /// <summary>
    /// 结束战斗弹窗关闭
    /// </summary>
    private void OnCombatEndPopupClose(object _isWin)
    {
        gameObject.SetActive(false);
        combatResultInfo.gameObject.SetActive(false);
        //播放战斗结果
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.CombatResult, _isWin);
        Destroy(transform.parent.gameObject);
    }

    /// <summary>
    /// 播放战斗回合信息
    /// </summary>
    private IEnumerator NewPlayCombatRoundInfo(CombatRound _combatRound)
    {
        //初始化位置
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.ResetCharPos, TeamType.Player, (object)null);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatTeamEvent.ResetCharPos, TeamType.Enemy, (object)null);
        yield return null;
        //
        if (immediateSkillEffect != null)
        {
            while (!immediateSkillEffect.IsPlayImmediateSkillEnd)
            {
                yield return null;
            }
        }
        if (playCastSkillManager != null)
        {
            DestroyImmediate(playCastSkillManager);
        }
        if (playCheckBuff != null)
        {
            DestroyImmediate(playCheckBuff);
        }
        foreach (CombatRoundResult combatRoundResult in _combatRound.combatRoundResults)
        {
            if (playCastSkillManager != null)
            {
                DestroyImmediate(playCastSkillManager);
            }
            //播放行动
            switch (combatRoundResult.resultType)
            {
                case CommandResultType.CastSkill:
                    playCastSkillManager = playEffectTrans.gameObject.AddComponent<UIPlayCastSkillManager>();
                    playCastSkillManager.PlayCastSkill(combatRoundResult.castSkill, combatTeams);
                    while (!playCastSkillManager.IsPlayCastSkillEnd)
                    {
                        yield return null;
                    }
                    DestroyImmediate(playCastSkillManager);
                    break;
                case CommandResultType.ImpulseEffect:
                    playImpulseEffect = playEffectTrans.gameObject.AddComponent<UIPlayImpulseEffect>();
                    playImpulseEffect.PlayImpulseEffect(combatRoundResult.impulseEffect, combatTeams);
                    while (!playImpulseEffect.IsPlayImpulseEffectEnd)
                    {
                        yield return null;
                    }
                    break;
                case CommandResultType.CheckBuff:
                    //检查Buff
                    playCheckBuff = playEffectTrans.gameObject.AddComponent<UIPlayCheckBuff>();
                    playCheckBuff.StartPlay(combatRoundResult.removeStates, combatTeams);
                    while (!playCheckBuff.IsPlayOk)
                    {
                        yield return null;
                    }
                    DestroyImmediate(playCheckBuff);
                    break;
            }
        }
        //回合结束
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, PlayCombatStage.PlayRoundInfoEnd, (object)_combatRound);
    }

    private void OnPlayCombatEffect(PlayCombatEffect arg1, object arg2)
    {
        switch (arg1)
        {
            case PlayCombatEffect.ImpulseEffect:
                break;
            case PlayCombatEffect.Targetset:
                break;
            case PlayCombatEffect.SkillEffect:
                break;
            case PlayCombatEffect.EndEvent:
                break;
            case PlayCombatEffect.ResultEnd:
                break;
            case PlayCombatEffect.CharDie:
                break;
            case PlayCombatEffect.ImmediateSkillEffect:
                immediateSkillEffect = playEffectTrans.gameObject.AddComponent<UIPlayImmediateSkillEffect>();
                immediateSkillEffect.PlayPlayImmediateSkill(arg2 as List<CRTargetInfo>, combatTeams);
                break;
        }
    }


    private void GetObj(Transform ui, Transform effect, Transform logic)
    {
        if (isFirst)
        {
            return;
        }
        //
        combatTrans = effect;
        leftTrans = logic.Find("Left");
        rightTrans = logic.Find("Right");
        thirdpartyTrans = logic.Find("Thirdparty");
        playEffectTrans = logic.Find("PlayEffect");
        //
        combatUiInfo = ui.GetComponent<UICombatUIInfo>();
        combatResultInfo = effect.Find("CombatResult").gameObject.AddComponent<UICombatResult>();
        combatEndPopup = effect.Find("CombatEndMask").gameObject.AddComponent<UICombatEndPopup>();
        isFirst = true;
    }

    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_PlayCombatEnd != null)
        {
            IE_PlayCombatEnd.Stop();
        }

        if (IE_PlayCombatRoundInfo != null)
        {
            IE_PlayCombatRoundInfo.Stop();
        }

        if (IE_StartPlayRoundInfo != null)
        {
            IE_StartPlayRoundInfo.Stop();
        }
        //
        IE_StartPlayRoundInfo = null;
        IE_PlayCombatEnd = null;
        IE_PlayCombatRoundInfo = null;
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatStageEvent);
        //注册播放Effect事件
        EventDispatcher.Instance.CombatEffect.RemoveEventListener<PlayCombatEffect, object>(EventId.CombatEffect, OnPlayCombatEffect);
        //
        StopAllCoroutine();
    }

    //
    private CombatSystem combatSystem;
    //
    private Transform combatTrans;
    //
    private UICombatUIInfo combatUiInfo;
    private UICombatResult combatResultInfo;
    private UICombatEndPopup combatEndPopup;
    //
    private bool isTest;
    private bool isFirst;
    private Transform leftTrans;
    private Transform rightTrans;
    private Transform thirdpartyTrans;
    private Transform playEffectTrans;
    //
    private CoroutineUtil IE_PlayCombatEnd;
    private CoroutineUtil IE_PlayCombatRoundInfo;
    private CoroutineUtil IE_StartPlayRoundInfo;
    //
    private UIPlayCheckBuff playCheckBuff;
    private UIPlayCastSkillManager playCastSkillManager;
    private UIPlayImpulseEffect playImpulseEffect;
    private UIPlayImmediateSkillEffect immediateSkillEffect;
    private UIPlayImmediateShowEffect playImmediateShowEffect;


    private List<UICombatTeam> combatTeams = new List<UICombatTeam>();

    public List<UICombatTeam> CombatTeams { get { return combatTeams; } }
}

/// <summary>
/// 探索自己移动信息
/// </summary>
public class OnExploreOneselfMove
{
    public Vector3 endVector3;
    public float aspd;
}

public class ComabatTeamTest
{
    public UICombatTeamManager teamManager;
    public UICombatTeam combatTeam;
}

public enum CombatUIOperationType
{
    /// <summary>
    /// 左边播放动作
    /// </summary>
    PlayLeftAction,
    /// <summary>
    /// 设置左边位置
    /// </summary>
    SetLeftPos,
    /// <summary>
    /// 设置右边位置
    /// </summary>
    SetRightPos,
    /// <summary>
    /// 更新右边移动
    /// </summary>
    UpdateRightMove,
}

namespace CombatEffect
{
    public class TargetsetInfo
    {
        public int castSkillID;
        public int castTeamID;
        public int castIndex;
        public int targetsetIndex;
        public int actionIndex;
        public List<CRTargetInfo> targetInfos;
        public UICombatTeamManager teamManager;
    }

    public class SkillEffectInfo
    {
        public int hitTeamID;
        public int hitCharID;
        public int hitCharIndex;
        public int sortingOrder;
        public List<CRSkillEffectResult> skillEffectResults;
        public List<PlaySkillResultEffectsObj> skillResultEffect = new List<PlaySkillResultEffectsObj>();
        public UICombatTeamManager teamManager;
    }


}