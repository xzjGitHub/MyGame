using UnityEngine;
using UnityEngine.UI;

public partial class UIExplore : MonoBehaviour
{
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        GetObj();
        OpenMask();
        OnClickStartTiming();
        UpdateGoButtonShow(false);
        _muBuObj.localScale = Vector3.one * 1.15f + Vector3.right * (GameTools.WidthActualRatio - 1);
        _isFront = ScriptSystem.Instance.ScriptPhase == ScriptPhase.Front;
        //前置探索不能退出
        _backButton.gameObject.SetActive(!_isFront);
    }


    #region 按钮点击
    /// <summary>
    /// 点击了返回
    /// </summary>
    private void OnClickBack()
    {
        OnClickStartTiming();
        if (OnBack != null)
        {
            OnBack(_isFront);
        }
    }
    /// <summary>
    /// 按下暂停
    /// </summary>
    private void OnClickPause()
    {
        UpdateGoButtonShow(false);
        _startTimingButton.gameObject.SetActive(true);
        _pauseTimingButton.gameObject.SetActive(false);
        ScriptTimeSystem.Instance.StopTiming();
        _gamePause.SetActive(true);
    }
    /// <summary>
    /// 关闭暂停
    /// </summary>
    private void OnClickStartTiming()
    {
        _startTimingButton.gameObject.SetActive(false);
        _pauseTimingButton.gameObject.SetActive(true);
        ScriptTimeSystem.Instance.StartTiming();
        _gamePause.SetActive(false);
    }

    /// <summary>
    /// 点击了GO
    /// </summary>
    private void OnClickGo()
    {
        OpenMask();
        UpdateGoButtonShow(false);
        //
        LogHelper_MC.Log("重置场景");
        if (_IEResetScene != null)
        {
            _IEResetScene.Stop();
        }

        _IEResetScene = new CoroutineUtil(IResetScene(3, 1));
    }
    /// <summary>
    /// 点击任务
    /// </summary>
    private void OnClickTask()
    {
        _bountyPopup.OpenUI();
    }
    /// <summary>
    /// 点击地图
    /// </summary>
    private void OnClickMap()
    {
        _bigMapPopup.OpenUI(false, false);
    }
    /// <summary>
    /// 点击背包
    /// </summary>
    private void OnClickBag()
    {
        _bagPopup.OpenUI();
    }
    #endregion
    /// <summary>
    /// 点击地图探索完成确定
    /// </summary>
    private void OnCallMapExploreFinishOk()
    {
        OnClickBack();
    }

    private void UpdateDoorShow(bool isShow)
    {
        _doorEffect.SetActive(isShow);
    }

    /// <summary>
    /// 打开遮罩
    /// </summary>
    private void OpenMask()
    {
        _maskObj.SetActive(true);
    }
    /// <summary>
    /// 关闭遮罩
    /// </summary>
    private void CloseMask()
    {
        _maskObj.SetActive(false);
    }
    /// <summary>
    /// 更新go按钮显示
    /// </summary>
    /// <param name="isShow"></param>
    private void UpdateGoButtonShow(bool isShow = true)
    {
        _go.SetActive(isShow);
    }
    /// <summary>
    /// 打开探索显示
    /// </summary>
    private void OpenExploreShow()
    {
        _exploreChar.UpdateShow();
        _topTransform.gameObject.SetActive(true);
        _exploreEvent.gameObject.SetActive(true);
    }
    /// <summary>
    /// 关闭探索显示
    /// </summary>
    private void CloseExploreShow()
    {
        UpdateGoButtonShow(false);
        _topTransform.gameObject.SetActive(false);
        _exploreEvent.gameObject.SetActive(false);
        _exploreChar.UpdateShow(false);
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        GetOtherObj();
        //
        _moveHintObj = transform.Find("MoveHint").gameObject;
        _leftBanMove = transform.Find("BanMoveHint/Left").gameObject;
        _rightBanMove = transform.Find("BanMoveHint/Right").gameObject;
        _muBuObj = transform.Find("MuBu");
        _maskBgObj = transform.Find("MaskBg").gameObject;
        _topTransform = transform.Find("Top");
        _visitFailTrans = transform.Find("VisitFail");
        _visitFailObj = _visitFailTrans.Find("Text").gameObject;
        _maskObj = transform.Find("Mask").gameObject;
        _gamePause = transform.Find("GamePause").gameObject;
        _gameStart = _gamePause.transform.Find("Back").GetComponent<Button>();
        //
        _go = transform.Find("Go").gameObject;
        _go.AddComponent<Button>().onClick.AddListener(OnClickGo);
        //添加组件
        _exploreChar = transform.Find("ExploreChar").gameObject.AddComponent<UIExploreChar>();
        _exploreChar.OnClickButtonMovEnd = OnCallClickButtonMoveEnd;
        _exploreMap = transform.Find("Map").gameObject.AddComponent<UIExploreMap>();
        _doorEffect = transform.Find("DoorEffect").gameObject;
        _doorEffect.AddComponent<UIAlterParticleSystemLayer>().UpdateShow();
        _exploreEvent = transform.Find("EventList").gameObject.AddComponent<UIExploreEvent>();
        _exploreEvent.OnInitOK = OnCallEventInitOK;
        _exploreEvent.OnVisitReady = OnCallBackVisitReady;
        _exploreEvent.OnExitEvent = OnCallExitEvent;
        _exploreEvent.OnAutoMoveEnd = OnCallEventAutoMoveEnd;
        _exploreEvent.OnAutoVisit = OnCallAutoVisit;
        _exploreEvent.OnBlock = OnCallBlock;
        //
        _bagButton = _topTransform.Find("Bag").GetComponent<Button>();
        _mapButton = _topTransform.Find("Map").GetComponent<Button>();
        _taskButton = _topTransform.Find("Task").GetComponent<Button>();
        _bagButton.onClick.AddListener(OnClickBag);
        _mapButton.onClick.AddListener(OnClickMap);
        _taskButton.onClick.AddListener(OnClickTask);
        //
        _timeText = _topTransform.Find("Time/Time").GetComponent<Text>();
        _goldText = _topTransform.Find("Gold/Num").GetComponent<Text>();
        _manaText = _topTransform.Find("Mana/Num").GetComponent<Text>();
        _startTimingButton = _topTransform.Find("Time/Start").GetComponent<Button>();
        _pauseTimingButton = _topTransform.Find("Time/Pause").GetComponent<Button>();
        _gameStart.onClick.AddListener(OnClickStartTiming);
        _pauseTimingButton.onClick.AddListener(OnClickPause);
        //
        _backButton = _topTransform.Find("Back").GetComponent<Button>();
        _backButton.onClick.AddListener(OnClickBack);
        //
        _mapNameText = _topTransform.Find("Name/Name").GetComponent<Text>();
    }


    /// <summary>
    /// 获得其他组件
    /// </summary>
    private void GetOtherObj()
    {
        _eventIntroPopup = GameModules.popupSystem.GetPopupObj(ModuleName.eventIntroPopup).GetComponent<UIEventIntroPopup>();
        _wayponitStart = GameModules.popupSystem.GetPopupObj(ModuleName.exploreWayponitStart).GetComponent<UIWayponitStart>();
        _combatEventPopup = GameModules.popupSystem.GetPopupObj(ModuleName.combatEventPopup).GetComponent<UICombatEventPopup>();
        _bagPopup = GameModules.popupSystem.GetPopupObj(ModuleName.bagPopup).GetComponent<UIExploreBagPopup>();
        _dialogPopup = GameModules.popupSystem.GetPopupObj(ModuleName.dialogPopup).GetComponent<UIDialogPopup>();
        _bountyInfoPopup = GameModules.popupSystem.GetPopupObj(ModuleName.bountyInfoPopup).GetComponent<UIBountyInfoPopup>();
        _mapExploreFinish = GameModules.popupSystem.GetPopupObj(ModuleName.mapExploreFinish).GetComponent<UIMapExploreFinish>();
        _bigMapPopup = GameModules.popupSystem.GetPopupObj(ModuleName.bigMapPopup).GetComponent<UIBigMapPopup>();
        _bountyPopup = GameModules.popupSystem.GetPopupObj(ModuleName.bountyPopup).GetComponent<UIBountyPopup>();
        //
        _mapExploreFinish.OnConfirm = OnCallMapExploreFinishOk;
    }
    //
    private bool _isFirstHint = true;
    private GameObject _moveHintObj;
    private GameObject _leftBanMove;
    private GameObject _rightBanMove;
    private Transform _muBuObj;
    private Button _backButton;
    private Button _taskButton;
    private Button _bagButton;
    private Button _mapButton;
    private Transform _topTransform;
    private GameObject _go;
    private GameObject _doorEffect;
    private GameObject _gamePause;
    private Button _gameStart;
    private Text _timeText;
    private Text _mapNameText;
    private Text _goldText;
    private Text _manaText;
    private GameObject _maskObj;
    private Transform _visitFailTrans;
    private GameObject _visitFailObj;
    private GameObject _maskBgObj;
    //
    private readonly Button startButton;
    private Button _startTimingButton;
    private Button _pauseTimingButton;
    //
    private UIExploreChar _exploreChar;
    private UIExploreMap _exploreMap;
    private UIExploreEvent _exploreEvent;
    //
    private UIWayponitStart _wayponitStart;
    private UIMapExploreFinish _mapExploreFinish;
    private UICombatEventPopup _combatEventPopup;
    private UIEventIntroPopup _eventIntroPopup;
    private UIExploreBagPopup _bagPopup;
    private UIDialogPopup _dialogPopup;
    private UIBountyInfoPopup _bountyInfoPopup;
    private UIBigMapPopup _bigMapPopup;
    private UIBountyPopup _bountyPopup;
}