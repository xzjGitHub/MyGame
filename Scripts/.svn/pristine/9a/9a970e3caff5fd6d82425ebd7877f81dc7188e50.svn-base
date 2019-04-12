using System.Collections.Generic;
using UnityEngine;

public class UIExploreAutoEvent : MonoBehaviour
{
    public delegate void CallBack(object pos);

    public CallBack OnCallBackItem;
    public CallBack OnCallBackHealingGlob;
    //
    private const float ScreenDefaultWidth = 1280f;
    private float autoWidth;
    private bool isStart;
    private readonly bool isReady;
    private float nowX;
    private bool isAuto;
    private int eventIndex;
    //   
    private List<Transform> items = new List<Transform>();
    private UIExploreItemMove exploreItemMove;
    //
    private Combat_config combatConfig;
    private EventAttribute eventAttribute;
    private WPVisitEventResult _visitEventResult;


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(EventAttribute _eventAttribute, int _index)
    {
        exploreItemMove = GameModules.popupSystem.GetPopupObj(ModuleName.itemMovePopup).GetComponent<UIExploreItemMove>();
        //
        autoWidth = -transform.GetComponent<RectTransform>().rect.width;
        //
        eventAttribute = _eventAttribute;
        eventIndex = _index;
    }

    private void Update()
    {
        CheckPos();
        AutoAccess();
    }

    private void CheckPos()
    {
        if (isStart)
        {
            return;
        }

        nowX = GameTools.WorldToScreenPoint(transform).x;
        if (nowX <= Screen.width)
        {
            //加载物品显示
            _visitEventResult = ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Pay);
            LoadResShow();
            //
            isStart = true;
            Destroy(gameObject);
            //    isAuto = true;
        }

    }

    private void AutoAccess()
    {
        if (!isAuto)
        {
            return;
        }

        nowX = GameTools.WorldToScreenPoint(transform).x;
        if (!(nowX < autoWidth * (Screen.width / ScreenDefaultWidth)))
        {
            return;
        }

        isAuto = false;
        Destroy(gameObject);
    }

    private void LoadResShow()
    {
        if (!_visitEventResult.isSucceed)
        {
            return;
        }

        items = exploreItemMove.LoadItemReward(_visitEventResult, OnClickItem, OnClickHealingGlob);
        Vector3 _tempVector3;
        foreach (Transform item in items)
        {
            item.position = transform.position;
            _tempVector3 = item.localPosition;
            item.localPosition = new Vector3(_tempVector3.x + 100, _tempVector3.y, 0);
            item.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 点击物品
    /// </summary>
    private void OnClickItem(int _index)
    {
        if (OnCallBackItem != null)
        {
            OnCallBackItem(items[_index].position);
           // ItemFactory.Instance.Release(items[_index].gameObject);
        }
    }
    /// <summary>
    /// 点击生命球
    /// </summary>
    private void OnClickHealingGlob(int _index)
    {
        if (combatConfig == null)
        {
            combatConfig = Combat_configConfig.GetCombat_config();
        }
        TeamSystem.Instance.UseGlobHealing(combatConfig.globHealing);
        //
        if (OnCallBackHealingGlob != null)
        {
            OnCallBackHealingGlob(items[_index].position);
        }

        DestroyImmediate(items[_index].gameObject);
    }

}
