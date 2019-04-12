using UnityEngine;
using System.Collections.Generic;
using GameEventDispose;
using System.Linq;

/// <summary>
/// 战斗队伍基类
/// </summary>
public class UICombatTeamBase : MonoBehaviour
{
    public bool IsMove { get { return isMove; } }
    public int teamID;
    public TeamType teamType = TeamType.Thirdparty;

    protected void InitInfo(CombatTeamInfo teamInfo)
    {
        teamID = teamInfo.teamId;
        teamType = teamInfo.teamType;
    }

    /// <summary>
    /// 开启渐隐
    /// </summary>
    public void FadeOut()
    {
        isFade = true;
        foreach (var item in charUnits)
        {
            EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.FadeOut, item.teamID, item.charID, (object)null);
        }
    }

    /// <summary>
    /// 更新Npc移动
    /// </summary>
    protected void UpdateNpcMove(float aspd)
    {
        if (rectTransform == null)
        {
            rectTransform = transform.GetComponent<RectTransform>();
        }
        if (rectTransform.anchoredPosition.x <= 0) return;
        rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, Vector3.zero, aspd);
    }

    /// <summary>
    /// 设置召唤位置
    /// </summary>
    protected void SetNpcSummonPos()
    {
        if (rectTransform == null)
        {
            rectTransform = transform.GetComponent<RectTransform>();
        }
        rectTransform.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// 重置资源
    /// </summary>
    protected void ResetRes()
    {
        for (int i = 0; i < combatUnitManagers.Count; i++)
        {
            combatUnitManagers[i].ResetRes();
        }
    }
    /// <summary>
    /// 渐显
    /// </summary>
    protected void Fade()
    {
        foreach (var item in charUnits)
        {
            EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Fade, item.teamID, item.charID, (object)null);
        }
    }

    /// <summary>
    /// 更新移动
    /// </summary>
    private void UpdateMove()
    {
        if (!isMove) return;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, leftEndVector3, aspd * (1 / Time.fixedDeltaTime) * Time.deltaTime);
        if (transform.localPosition.x < leftEndVector3.x / 2) return;
        if (!isFade)
        {
            FadeOut();
            return;
        }
        //
        if (charUnits.Last().IsFade)
        {
            isMove = false;
            isFade = false;
            EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.OneselfMoveFinish, (object)null);
        }
    }

    /// <summary>
    /// 重置角色位置
    /// </summary>
    protected void ResetCharPos()
    {
        foreach (var item in charUnits)
        {
            EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.ResetPos, item.teamID, item.charID, (object)null);
        }
    }
    /// <summary>
    /// 关闭升级显示
    /// </summary>
    protected void CloseUpgradShow()
    {
        foreach (var item in combatUnitManagers)
        {
            item.CloseUpgradShow();
        }
    }

    /// <summary>
    /// Npc设置开始位置
    /// </summary>
    protected void NpcSetStartPos()
    {
        if (rectTransform == null) rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = StartPos;
    }

    private void Update()
    {
        UpdateMove();
    }

    //
    protected RectTransform rectTransform;
    protected bool isMove;
    protected Vector3 leftEndVector3;
    protected float aspd;
    protected bool isFade;
    protected bool isPlayEndEventOk;
    protected bool isPlayResultEffectOk;
    //
    protected Vector2 StartPos = new Vector2(533, 0);
    //
    protected List<UICombatUnitManager> combatUnitManagers = new List<UICombatUnitManager>();
    public List<UICharUnit> charUnits = new List<UICharUnit>();
}


public enum CombatTeamEvent
{
    Init,
    ResetRes,
    CombatEnd,
    Move,
    TeamAction,
    SetActive,
    SetStartPos,
    SetPos,
    SetSummonPos,
    UpdateNpcMove,
    ResetCharPos,
    OpenUpgradeShow,
    CloseUpgradeShow,

}

