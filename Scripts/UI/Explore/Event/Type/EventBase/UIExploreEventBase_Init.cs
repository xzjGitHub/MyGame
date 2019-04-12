using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public partial class UIExploreEventBase
{
    public delegate void CallBack(object param);

    public CallBack OnExitEvent;
    public CallBack OnStartOpen;
    public CallBack OnVisiting;
    public CallBack OnCallBackItem;
    public CallBack OnCallBackHealingGlob;
    public CallBack OnAddEvent;

    protected CallBack OnOpenUI;
    protected CallBack OnProgressing;
    protected CallBack OnProgressAchieve;
    protected CallBack OnFade;
    protected CallBack OnFadeOk;
    protected CallBack OnOpenShow;
    protected CallBack OnOpenShow1;
    protected CallBack OnOpenPrepare;

    //
    public float xValue = 50;
    public float x1Value = 400;
    public float sizeValue = 36;
    //
    public bool isAutoVisit;

    public EventAttribute EventAttribute { get { return eventAttribute; } }



    public void OpenUI(EventAttribute _eventAttribute)
    {
        if (OnOpenUI != null)
        {
            OnOpenUI(_eventAttribute);
        }
    }


    /// <summary>
    /// 初始化基类
    /// </summary>
    /// <param name="_eventAttribute"></param>
    protected void BaseInit(EventAttribute _eventAttribute)
    {
        //
        InitInfo(_eventAttribute);
        SceneIndex = _eventAttribute.SceneIndex;
        ScenePos = _eventAttribute.ScenePos;
        //
        isHaveSmoke = _eventAttribute.isHaveSmoke;
        aspd = 5;
        //
        uiCanvas = gameObject.AddComponent<Canvas>();
        gameObject.AddComponent<GraphicRaycaster>();
        //
        exploreEventPopup = GameModules.popupSystem.GetPopupObj(ModuleName.exploreEventPopup).GetComponent<UIExploreEventPopup>();
        exploreItemMove = GameModules.popupSystem.GetPopupObj(ModuleName.itemMovePopup).GetComponent<UIExploreItemMove>();
        //
        Transform obj = ShowTranS.Find("Icon");
        if (obj != null)
        {
            iconSkeleton = ShowTranS.Find("Icon/Effect").GetComponent<SkeletonAnimation>();
            if (iconSkeleton != null)
            {
                renderers.Add(GameTools.GetObjRenderer(iconSkeleton.gameObject));
                iconSkeleton.AnimationState.Event += EventList;
            }
            //
            obj = ShowTranS.Find("Hei");
            if (obj != null)
            {
                heiObj = obj.gameObject;
                heiButton = heiObj.GetComponent<Button>();
                heiButton.onClick.AddListener(OnClickHei);
                heiObj.SetActive(false);
            }
            //
            obj = ShowTranS.Find("Shot");
            if (obj != null)
            {
                shotObj = obj.gameObject;
            }
            //
            button = ShowTranS.Find("Button").GetComponent<Button>();
            buttonImage = button.GetComponent<Image>();
            button.onClick.AddListener(OnClickButton);
            buttonImage.raycastTarget = true;
            // 
            obj = ShowTranS.Find("ExplodeEffect");
            if (obj != null)
            {
                explodeGameObject = obj.gameObject;
                //添加脚本
                alterParticles.Add(explodeGameObject.AddComponent<UIAlterParticleSystemLayer>());
            }
            obj = ShowTranS.Find("TopEffect");
            if (obj != null)
            {
                topEffectGameObject = obj.gameObject;
                //添加脚本
                alterParticles.Add(topEffectGameObject.AddComponent<UIAlterParticleSystemLayer>());
            }
            //
            if (eventType != WPEventType.Combat || eventType != WPEventType.Boss)
            {
                obj = ShowTranS.Find("Text");
                if (obj != null)
                {
                    Text = obj.gameObject;
                    Text.GetComponent<Text>().text = eventAttribute.event_template.eventName;
                    Text.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                obj = ShowTranS.Find("HeightLightEffect");
                if (obj != null)
                {
                    heightLightEffect = obj.gameObject;
                    alterParticles.Add(heightLightEffect.AddComponent<UIAlterParticleSystemLayer>());
                    particleSystemAlpha = heightLightEffect.AddComponent<UIAlterParticleSystemAlpha>();
                }

                obj = ShowTranS.Find("Progress");
                if (obj != null)
                {
                    eventProgress = obj.gameObject.AddComponent<UIEventProgress>();
                }
                //
                obj = ShowTranS.Find("ItemLaunch");
                if (obj != null)
                {
                    itemLaunchPosition = obj.gameObject.AddComponent<UIItemLaunchPosition>();
                }
                //
                if (alterParticles.Count > 0)
                {
                    alterParticles[alterParticles.Count - 1].Init(sortingLayerName, sortingOrder);
                }
            }
        }
        //
        UpdateShow(sortingLayerName, sortingOrder);
        //
        if (isAutoVisit)
        {
            OnCallEventShow();
            OnCallEventCanVisit();
            OnCallEventAutoVisit();
            return;
        }
        eventPosDetection = gameObject.AddComponent<UIExploreEventPosDetection>();
        //eventPosDetection.OnEnterScreen = OnCallEventShow;
        //eventPosDetection.OnShow1 = OnCallEventShow1;
        //eventPosDetection.OnCanVisit = OnCallEventCanVisit;
        //eventPosDetection.OnAutoVisit = OnCallEventAutoVisit;
        //eventPosDetection.OnAutoAbandonVisit1 = OnCallEventAutoAbandonVisit1;
        // 
        eventPosDetection.OnAutoAbandonVisit2 = OnCallEventAutoAbandonVisit2;
        eventPosDetection.offset1 = xValue;
        eventPosDetection.offset2 = x1Value;
        //
        if (particleSystemAlpha != null)
        {
            particleSystemAlpha.SetAlpha(0);
            particleSystemAlpha.gameObject.SetActive(true);
        }
        // if (ShowTranS != null) ShowTranS.gameObject.SetActive(false);
        //
        // if (eventType == WPEventType.Grass)
        //   {
        //        heiObj.SetActive(false);
        //        buttonImage.raycastTarget = true;
        //      return;
        //    }
        //  if (heiButton != null) heiButton.GetComponent<Image>().raycastTarget = false;
        //  if (isHaveSmoke) return;
        //    if (heiObj != null) heiObj.SetActive(false);
    }


    /// <summary>
    /// 初始化信息
    /// </summary>
    private void InitInfo(EventAttribute _eventAttribute)
    {
        event_Info = Event_infoConfig.GetEvent_Info(_eventAttribute.event_template.eventType);
        sortingLayerName = event_Info.sortingLayer;
        sortingOrder = event_Info.sortingOrder;
        switch (_eventAttribute.EventPosType)
        {
            case 1:
                sortingOrder = 20;
                break;
            case 2:
                sortingOrder = 20;
                break;
            case 3:
                sortingOrder = 10;
                break;
            case 4:
                sortingOrder = 30;
                break;
            default:
                sortingOrder = 20;
                break;
        }
        //
        eventAttribute = _eventAttribute;
        eventIndex = _eventAttribute.EventIndex;
        eventID = _eventAttribute.eventId;
        eventPosIndex = _eventAttribute.EventPos;
        //
        eventType = (WPEventType)eventAttribute.event_template.eventType;
        //
        rectTransform = ShowTranS.GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Vector2.up * 0 /*event_Info.YValue*/;
    }


    //
    private int sum;
    private int sum1;
    //
    public float aspd = 1f;
    public string sortingLayerName = "char";
    public int sortingOrder = 21;
    //
    private const string shotStr = "normalHit_6001";
    protected SkeletonAnimation shot;
    protected GameObject shotObj;
    protected GameObject tansuo;
    protected GameObject explodeGameObject;
    protected GameObject topEffectGameObject;
    //
    protected Button heiButton;
    protected UIAlterParticleSystemAlpha particleSystemAlpha;
    protected GameObject heiObj;
    protected GameObject Text;
    protected Canvas uiCanvas;
    protected List<UIAlterParticleSystemLayer> alterParticles = new List<UIAlterParticleSystemLayer>();
    protected List<Renderer> renderers = new List<Renderer>();
    protected List<Canvas> canvas = new List<Canvas>();
    protected UIItemLaunchPosition itemLaunchPosition;
    protected List<int> selectindexs = new List<int>();
    protected List<Transform> items = new List<Transform>();
    protected Combat_config combatConfig;
    protected WPEventType eventType;
    //
    protected UIExploreEventPopup exploreEventPopup;
    protected EventAttribute eventAttribute;
    protected int eventIndex;
    protected int eventPosIndex;
    protected int eventID;
    protected Button button;
    protected Image buttonImage;
    protected GameObject heightLightEffect;
    protected WPVisitEventResult wpVisitEventResult;
    protected UIEventProgress eventProgress;
    protected SkeletonAnimation iconSkeleton;
    //
    protected UIExploreItemMove exploreItemMove;
    protected UIExploreEventPosDetection eventPosDetection;
    protected Event_info event_Info;
    //
    protected RectTransform rectTransform;
    private Transform showTranS;
    private bool isHaveSmoke;

    //
    protected const string bxName1Str = "Idle";
    protected const string bxName2Str = "Open";
    protected const string bxName3Str = "Failed";

    protected Transform ShowTranS
    {
        get
        {
            if (showTranS == null)
            {
                showTranS = transform.Find("Show");
            }

            if (showTranS == null)
            {
                showTranS = transform;
            }

            return showTranS;
        }
    }


}
