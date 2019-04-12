using System.Collections;
using System.Collections.Generic;
using GameEventDispose;
using UnityEngine;
using UnityEngine.UI;

public class UIExploreEventPopup : MonoBehaviour
{
    public delegate void CallBack(object param);

    public CallBack OnOpen;
    public CallBack OnExit;
    public CallBack OnRandom;
    public CallBack OnOpenPrepare;

    public float time = 0f;

    /// <summary>
    /// 打开显示
    /// </summary>
    /// <param name="_eventAttribute"></param>
    public void OpenUI(EventAttribute _eventAttribute)
    {
        eventAttribute = _eventAttribute;
        eventIndex = _eventAttribute.EventIndex;
        //
        //  eventIntroPopup.UpdateShow(string.Format(event_Info.interact, eventAttribute.event_template.eventID));
        EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.TestShow, 0, 0, (object)null);
        //立即访问
        if (ImmediateVisit(eventAttribute))
        {
            StartVisit(WPEventVisitType.Normal);
            return;
        }
        //
        eventAttribute.SetPhase(1);
        //初始化
        Init();
        bgCanvas.alpha = 0;
        //
        if (event_Config == null)
        {
            event_Config = Event_configConfig.GetConfig();
        }

        nameT.text = eventAttribute.event_template.eventName;
        event_Info = Event_infoConfig.GetEvent_Info(_eventAttribute.event_template.eventType);
        //更新选项显示
        ResourceLoadUtil.DeleteChildObj(buttonList);
        CreateSeletion(_eventAttribute);
        //
        bgCanvas.alpha = 1;
        showTransform.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 取消访问
    /// </summary>
    public void CancelVisit()
    {
        if (showTransform == null)
        {
            return;
        }

        showTransform.gameObject.SetActive(false);
    }

    /// <summary>
    /// 立即访问
    /// </summary>
    private bool ImmediateVisit(EventAttribute eventAttribute)
    {
        //选项为默认选项
        return eventAttribute.PhaseSum == 0;
    }

    /// <summary>
    /// 开始访问
    /// </summary>
    private void StartVisit(WPEventVisitType type)
    {
        gameObject.SetActive(false);
        //
        //  if (type != WPEventVisitType.Pay) eventIntroPopup.UpdateShow(string.Format(event_Info.visite2, eventAttribute.event_template.eventID));
        //
        new CoroutineUtil(IEStartVisit(type));
    }

    private IEnumerator IEStartVisit(WPEventVisitType type)
    {
        int random = RandomBuilder.RandomNum(100, 1);
        if (OnRandom != null)
        {
            OnRandom(random);
        }
        //
        WPVisitEventResult result = ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, type, random);
        //访问时需要消耗生命 发送损失生命
        if (type != WPEventVisitType.Pay)
        {
            //switch (eventAttribute.EventType)
            //{
            //    case WPEventType.Altar:
            //    case WPEventType.Challenge:
            //        TeamSystem.Instance.CharCostHP_UI(result.charCostHP);
            //        yield return null;
            //        break;
            //}
            yield return null;
            if (OnOpenPrepare != null)
            {
                OnOpenPrepare(null);
            }
            //  yield return new WaitForSeconds(eventAttribute.EventType == WPEventType.Choice ? 0 : time);

            //if (eventIntroPopup != null)
            //{
            //    eventIntroPopup.UpdateShow(GetRollStr(random, result.visitResult));
            //}
        }
        //
        if (OnOpen != null)
        {
            OnOpen(result);
        }

        OnOpen = null;
    }

    private void OnClickLiKai()
    {
        gameObject.SetActive(false);
        if (OnExit != null)
        {
            OnExit(eventAttribute);
        }
        OnExit = null;
    }

    /// <summary>
    /// 得到点数描述
    /// </summary>
    private string GetRollStr(int value, WPEventVisitResult result)
    {
        string str = string.Empty;
        switch (result)
        {
            case WPEventVisitResult.Jckpot:
                str = "完美成功！";
                break;
            case WPEventVisitResult.Success:
                str = "成功！";
                break;
            case WPEventVisitResult.Failure:
                str = "失败！";
                break;
            case WPEventVisitResult.Ambush:
                str = "严重失败！";
                break;
            case WPEventVisitResult.Trap:
                str = "严重失败！";
                break;
            default:
                str = "成功！";
                break;
        }
        return string.Format("你的投点 = {0}，{1}", value, str);
    }

    /// <summary>
    /// 创建选项
    /// </summary>
    /// <param name="eventAttribute"></param>
    private void CreateSeletion(EventAttribute eventAttribute)
    {
        ResourceLoadUtil.DeleteChildObj(buttonList);
        foreach (SelectionAttribute item in eventAttribute.Selections[eventAttribute.NowPhase])
        {
            _eventSelection = ResourceLoadUtil.InstantiateRes(selectionObj, buttonList).AddComponent<UIExploreEventSelection>();
            _eventSelection.NewCreate(eventAttribute, eventAttribute.NowPhase, item);
            _eventSelection.OnVisit += OnCallVisit;
        }
    }



    /// <summary>
    /// 访问
    /// </summary>
    private void OnCallVisit(SelectionAttribute selectionAttribute)
    {
        bool isVisitEnd = false;
        switch ((WPEventSelectionType)selectionAttribute.SelectionType)
        {
            case WPEventSelectionType.Next:
            case WPEventSelectionType.AdvancedNext:
                int result = selectionAttribute.GetResult();
                switch (result)
                {
                    case 0:
                        eventAttribute.OnClickNextOption(selectionAttribute.SelectionID);
                        CreateSeletion(eventAttribute);
                        return;
                    case 1: //失败 直接消失
                        OnClickLiKai();
                        return;
                    case 2: //陷阱 消失并且扣血
                        OnClickLiKai();
                        return;
                    case 3: //战斗 消失并开战
                        OnClickLiKai();
                        return;
                }
                break;
            case WPEventSelectionType.Award:
            case WPEventSelectionType.SuperAward:
            case WPEventSelectionType.Herb:
                isVisitEnd = true;
                break;
            case WPEventSelectionType.Bribery:
                isVisitEnd = true;
                // todo 成功 并使用相应的数据
                break;
            case WPEventSelectionType.Combat:
                //开战 
                break;
            case WPEventSelectionType.Summon:
                //直接开站 如果是失败不给奖励，继续探索
                selectionAttribute.SetMobTeamID();
                isVisitEnd = true;
                break;
        }
        //结算完有失败的清况  事件类型:战斗 boss失败不消失
        gameObject.SetActive(false);
        bgCanvas.alpha = 0;
        //访问成功
        if (OnOpen != null)
        {
            WPVisitEventResult result = ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Advanced);
            result.isVisitEnd = isVisitEnd;
            OnOpen(result);
        }
        OnOpen = null;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (isFirst)
        {
            return;
        }
        //
        eventIntroPopup = GameModules.popupSystem.GetPopupObj(ModuleName.eventIntroPopup).GetComponent<UIEventIntroPopup>();
        //
        bgCanvas = transform.GetComponent<CanvasGroup>();
        showTransform = transform.Find("Show");
        temp = transform.Find("Temp");
        selectionObj = temp.Find("Selection").gameObject;
        //
        exit = showTransform.Find("Exit").GetComponent<Button>();
        nameT = showTransform.Find("Name").GetComponent<Text>();
        buttonList = showTransform.Find("ButtonList");
        //
        exit.onClick.AddListener(OnClickLiKai);
        //
        isFirst = true;
    }
    //
    private Transform buttonList;
    private Transform temp;
    private Text nameT;
    private Button exit;
    private GameObject selectionObj;
    //
    private CanvasGroup bgCanvas;
    private readonly ContentSizeFitter optionsSize;
    private readonly ContentSizeFitter introSize;
    //
    private readonly Transform bg1;
    private Transform showTransform;
    private readonly Text eventName;
    private readonly Text describeText;
    private readonly Text failText;
    private readonly Text awardText;
    private readonly Text succeedText;
    //
    private bool isFirst;
    //
    private readonly WPEventVisitType visitType;
    private int eventIndex;
    private EventAttribute eventAttribute;
    private readonly EventOptionsIntro eventOptionsIntro;
    private readonly List<ExploreEventPopIntro> eventPopIntros = new List<ExploreEventPopIntro>();
    //
    private readonly string sortingLayerName;
    private readonly int sortingOrder;
    private readonly Canvas uiCanvas;
    //
    private UIEventIntroPopup eventIntroPopup;
    private Event_info event_Info;
    private readonly UIExploreEventBase eventBase;
    private Event_config event_Config;
    private readonly ItemAttribute costItemAttribute;
    private UIExploreEventSelection _eventSelection;
}

/// <summary>
/// 探索事件弹窗描述
/// </summary>

public class ExploreEventPopIntro
{
    public bool isCancel;
    public int optionIndex;
    public string intro;
    public WPEventVisitType visitType;
}