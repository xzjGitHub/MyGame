using System.Collections.Generic;
using GameEventDispose;
using UnityEngine;

public class UICharUnit : UICharBase
{
    public bool IsPlayCharUpgradeShowOk { get { return upgradeShow.IsOk; } }

    public bool IsFade { get { return actionOperation.IsFade; } }

    public UICharActionOperation ActionOperation { get { return actionOperation; } }

    public CameraShake CameraShake { get { return cameraShake; } }

    public List<float> erCSYS;

    public int HitSortingOrder { get { return ActionOperation.HitSortingOrder; } }

    public int AtkSortingOrder { get { return ActionOperation.AtkSortingOrder; } }

    public UICharStateManager StateManager { get { return stateManager; } }

    public Vector3 BonePos(string boneStr)
    {
        return SkeletonTool.GetCharBonePos(actionOperation, boneStr);
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(CombatUnit combatUnit, Transform teamTrans)
    {
        this.combatUnit = combatUnit;
        base.InitInfo(combatUnit);
        //
        this.teamTrans = teamTrans;
        unitTrans = teamTrans.GetChild(combatUnit.initIndex);
        logicTrans = transform.GetChild(combatUnit.initIndex);
        if (combatUnit.teamType == TeamType.Thirdparty)
        {
            return;
        }
        //
        GetObj(unitTrans, logicTrans);
        //
        actionOperation.Init(this);
        actionEventEffect.Init(this, actionOperation);
        hpShow.Init(this, actionOperation.SortingOrder);
        tagShow.Init(this, actionOperation.SortingOrder);
        //
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, UICharInfo, object>(EventId.CharEvent,
            OnCharEvent);
    }

    /// <summary>
    /// 销毁资源
    /// </summary>
    public void DestroyRes()
    {
        ResourceLoadUtil.DeleteChildObj(moveTrans);
        ResourceLoadUtil.DeleteChildObj(fixedTrans);
        //
        DestroyImmediate(actionOperation);
        DestroyImmediate(hpShow.gameObject);
        DestroyImmediate(tagShow.gameObject);
        DestroyImmediate(upgradeShow);
        DestroyImmediate(actionEventEffect);
        //
        EventDispatcher.Instance.CharEvent.RemoveEventListener<CharActionOperation, UICharInfo, object>(
            EventId.CharEvent, OnCharEvent);
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj(Transform unitTrans, Transform logicTrans)
    {
        if (isFirst)
        {
            return;
        }

        moveTrans = unitTrans.Find("Move");
        fixedTrans = unitTrans.Find("Fixed");
        //
        GameObject _obj = ResourceLoadUtil.InstantiateRes(ResourceLoadUtil.LoadHpShow(), unitTrans, teamTrans.localScale);
        hpShow = _obj.AddComponent<UICharHpShow>();
        _obj.SetActive(false);
        //
        _obj = ResourceLoadUtil.InstantiateRes(ResourceLoadUtil.LoadTagShow(), unitTrans, teamTrans.localScale);
        tagShow = _obj.AddComponent<UICharTagShow>();
        _obj.SetActive(false);
        //
        actionOperation = logicTrans.gameObject.AddComponent<UICharActionOperation>();
        actionEventEffect = logicTrans.gameObject.AddComponent<UICharActionEventEffectInfo>();
        //
        if (teamTrans.name == "Left")
        {
            upgradeShow = unitTrans.Find("CharUpgradeRewards").gameObject.AddComponent<UICharUpgradeShow>();
        }
        //
        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            cameraShake = Camera.main.gameObject.AddComponent<CameraShake>();
        }
        //
        isFirst = true;
    }

    /// <summary>
    /// 角色事件
    /// </summary>
    private void OnCharEvent(CharActionOperation arg1, UICharInfo charInfo, object arg2)
    {
        if (charInfo.teamID != base.teamID || charInfo.charID != base.charID || charInfo.charIndex != base.charIndex)
        {
            return;
        }

        switch (arg1)
        {
            case CharActionOperation.Init:
                break;
            case CharActionOperation.Fade:
                actionOperation.Fade();
                break;
            case CharActionOperation.FadeOut:
                actionOperation.FadeOut();
                break;
            case CharActionOperation.Action:
                object[] obj1 = arg2 as object[];
                CharModuleAction charModule;
                bool isIdle = false;
                if (obj1[0] is string)
                {
                    charModule = SkeletonTool.GetCharModuleState((string)obj1[0]);
                    if (charModule == CharModuleAction.Default)
                    {
                        break;
                    }
                    isIdle = (bool)obj1[1];
                }
                else
                {
                    charModule = (CharModuleAction)obj1[0];
                    isIdle = (bool)obj1[1];
                }
                actionOperation.PlayAction(charModule, isIdle);
                break;
            case CharActionOperation.ResetPos:
                LogHelperLSK.LogWarning("CharActionOperation.ResetPos");
                actionOperation.ResetPos();
                break;
            case CharActionOperation.ActionEffect:
                break;
            case CharActionOperation.ActonEventEffect:
            case CharActionOperation.PhaseStartEffect:
                break;
            case CharActionOperation.PhaseEndEffect:
                break;
            case CharActionOperation.HPCost:
                //执行状态效果
                if (arg2 is CRExecState)
                {
                    EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Hit, teamID, charIndex,
                        (object)new object[] { arg2, actionOperation.HpPos, actionOperation.SortingOrder });
                    return;
                }
                //
                if (arg2 is CREffectResult)
                {
                    EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Hit, teamID, charIndex,
                        (object)new object[] { arg2, actionOperation.HpPos, actionOperation.SortingOrder });
                    return;
                }
                //
                object[] obj3 = arg2 as object[];
                if (obj3[0] is int)
                {
                    actionOperation.PlayAction(CharModuleAction.Hurt, true);
                    EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Hit, teamID, charIndex,
                        (object)new object[] { obj3[0], obj3[1], obj3[2], actionOperation.HpPos, actionOperation.SortingOrder });
                    actionOperation.PlayEffect("xue", "xue");
                    return;
                }

                break;
            case CharActionOperation.TestShow:
                if (test == null)
                {
                    return;
                }

                test.SetActive(!test.activeInHierarchy);
                break;
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CharEvent.RemoveEventListener<CharActionOperation, UICharInfo, object>(EventId.CharEvent, OnCharEvent);
    }


    //
    private UICharActionOperation actionOperation;
    private UICharHpShow hpShow;
    private UICharTagShow tagShow;
    private UICharUpgradeShow upgradeShow;
    private UICharActionEventEffectInfo actionEventEffect;
    private readonly UICharStateManager stateManager = new UICharStateManager();
    private CameraShake cameraShake;
    //
    private Transform logicTrans;
    private Transform teamTrans;
    private Transform unitTrans;
    private bool isFirst;

    private GameObject test;
}
