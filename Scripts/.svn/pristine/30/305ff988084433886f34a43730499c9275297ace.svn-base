using GameEventDispose;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class UIEventSelectionPopup : MonoBehaviour
{

    public delegate void CallBack(object param);

    /// <summary>
    /// 离开
    /// </summary>
    public CallBack OnExit;
    /// <summary>
    /// 返回
    /// </summary>
    public CallBack OnBack;
    /// <summary>
    /// 事件结束
    /// </summary>
    public CallBack OnEventEnd;
    /// <summary>
    /// 复活
    /// </summary>
    public CallBack OnResurrection;
    /// <summary>
    /// 撤退
    /// </summary>
    public CallBack OnPullout;
    /// <summary>
    /// 修正属性结束
    /// </summary>
    public CallBack OnAmendAttributeEnd;
    /// <summary>
    /// 选项对话结束
    /// </summary>
    public CallBack OnSelectionDialogEnd;
    /// <summary>
    /// 战斗结束
    /// </summary>
    public CallBack OnCombatEnd;
    /// <summary>
    /// 后续选项
    /// </summary>
    public CallBack OnAfterDialogEnd;
    /// <summary>
    /// 奖励结束
    /// </summary>
    public CallBack OnRwardEnd;


    /// <summary>
    /// 打开显示
    /// </summary>
    public void OpenUI(UIExploreEventBase eventBase, Transform charObj)
    {
        _charTransform = charObj;
        //todo 打开选项停止计时
        ScriptTimeSystem.Instance.StopTiming();
        _nowLayer = 0;
        _eventBase = eventBase;
        _eventAttribute = eventBase.EventAttribute;
        //
        Init();
        if (_eventConfig == null)
        {
            _eventConfig = Event_configConfig.GetConfig();
        }
        _visitTime = _eventConfig.baseTimeCost;
        //
        _bgCanvas.alpha = 0;
        _isShowSelection = _eventAttribute.EventType != WPEventType.Trap;
        _selectionAttribute = null;
        if (_isShowSelection)
        {
            AddBackButtonShow();
            UpdateSelection(_eventAttribute.SelectionAttributes);
        }
        //
        _bgCanvas.alpha = 1;
        UpdateUIShow(_isShowSelection);
    }

    /// <summary>
    /// 陷阱事件选择失败
    /// </summary>
    public void TarpEventSelectFailure()
    {
        OnCallBackClickSelection(_eventAttribute.DefultFailureSelection);
    }
    /// <summary>
    /// 陷阱事件选择成功
    /// </summary>
    public void TarpEventSelectSuccess()
    {
        OnCallBackClickSelection(_eventAttribute.DefultSuccessSelection);
    }

    /// <summary>
    /// 取消访问
    /// </summary>
    public void CancelVisit()
    {
        if (_showTransform == null)
        {
            return;
        }

        _showTransform.gameObject.SetActive(false);
    }


    #region 流程
    /// <summary>
    /// 属性修正结束
    /// </summary>
    /// <param name="param"></param>
    private void AmendAttributeEnd(object param)
    {
        //2、对话
        OnSelectionDialogEnd = SelectionDialogEnd;
        _selectionAttribute = param as SelectionAttribute;
        if (_dialogPopup == null)
        {
            _dialogPopup = GameModules.popupSystem.GetPopupObj(ModuleName.dialogPopup).GetComponent<UIDialogPopup>();
        }

        _dialogPopup.Param = param;
        _dialogPopup.OnEnd = OnCallSelectionDialogEnd;
        _dialogPopup.OpenUI(_selectionAttribute.event_selection.selectionDialog);
    }

    /// <summary>
    /// 对话框结束
    /// </summary>
    /// <param name="param"></param>
    private void SelectionDialogEnd(object param)
    {
        _selectionAttribute = param as SelectionAttribute;
        OnCombatEnd = CombatEnd;
        //3、是否战斗
        _isCombatSelection = _selectionAttribute.SelectionType == SelectionType.Combat;
        _isCombatWin = false;
        if (_isCombatSelection)
        {
            CreateCombat(_selectionAttribute);
            return;
        }
        //战斗结束
        OnCallCombatEnd(param);
    }
    /// <summary>
    /// 战斗结束
    /// </summary>
    /// <param name="param"></param>
    private void CombatEnd(object param)
    {
        //todo 后续对话 
        OnAfterDialogEnd = AfterDialogEnd;
        _selectionAttribute = param as SelectionAttribute;
        if (_dialogPopup == null)
        {
            _dialogPopup = GameModules.popupSystem.GetPopupObj(ModuleName.dialogPopup).GetComponent<UIDialogPopup>();
        }

        _dialogPopup.Param = param;
        _dialogPopup.OnEnd = OnCallAfterDialogEnd;
        _dialogPopup.OpenUI(_selectionAttribute.event_selection.afterDialog);
    }

    /// <summary>
    /// 后续对话结束
    /// </summary>
    /// <param name="param"></param>
    private void AfterDialogEnd(object param)
    {
        //奖励
        OnRwardEnd = RwardEnd;
        if (_rwardPopup == null)
        {
            _rwardPopup = GameModules.popupSystem.GetPopupObj(ModuleName.rwardPopup).GetComponent<UIRwardPopup>();
        }
        //
        _rwardPopup.Param = param;
        _rwardPopup.OnClose = OnCallRwardEnd;
        _rwardPopup.OpenUI((param as SelectionAttribute).GetRwardInfo());
    }

    /// <summary>
    /// 奖励结束
    /// </summary>
    /// <param name="param"></param>
    private void RwardEnd(object param)
    {
        _selectionAttribute = param as SelectionAttribute;
        //后续选项、事件
        List<SelectionAttribute> selectionAttributes = new List<SelectionAttribute>();
        //判断是否是战斗事件、输赢
        if (_isCombatSelection && !_isCombatWin)
        {
            //添加撤退、复活
            selectionAttributes.Add(new SelectionAttribute(_eventConfig.defaultSelection[3], 0, _eventAttribute));
            selectionAttributes.Add(new SelectionAttribute(_eventConfig.defaultSelection[2], 0, _eventAttribute));
        }
        else
        {
            selectionAttributes = _selectionAttribute.GetNextLayerSelections();
            //添加正常选择
            if (selectionAttributes == null || selectionAttributes.Count == 0)
            {
                //没有选项结束事件
                UpdateUIShow();
                //todo 更新剧本时间
                ScriptTimeSystem.Instance.AddTime(_visitTime);
                ScriptTimeSystem.Instance.StartTiming();
                if (OnEventEnd != null)
                {
                    OnEventEnd(param);
                }
                OnEventEnd = null;
                return;
            }
        }

        //重新添加选项
        _nowLayer++;
        UpdateSelection(selectionAttributes);
        _exitObj.SetActive(false);
    }

    #endregion

    #region 回调

    /// <summary>
    /// 点击选项回调
    /// </summary>
    /// <param name="selectionAttribute"></param>
    private void OnCallBackClickSelection(SelectionAttribute selectionAttribute)
    {
        CloseSelectionShow();
        ScriptSystem.Instance.AddFlag(selectionAttribute.event_selection.addFlag);
        ScriptSystem.Instance.RemoveFlag(selectionAttribute.event_selection.remvFlag);
        //
        _visitTime += selectionAttribute.event_selection.addTeamInitiative;
        selectionAttribute.SelectionCost();
        switch (selectionAttribute.SelectionType)
        {
            case SelectionType.Default:
            case SelectionType.Base:
            case SelectionType.Page:
            case SelectionType.Combat:
                break;
            case SelectionType.Ignore:
                UpdateUIShow();
                if (OnExit != null)
                {
                    OnExit(_eventBase);
                }
                OnExit = null;
                return;
            case SelectionType.Back:
                UpdateUIShow();
                if (OnBack != null)
                {
                    OnBack(_eventBase);
                }
                OnBack = null;
                return;
            case SelectionType.Resurrection:
                UpdateUIShow();
                if (OnResurrection != null)
                {
                    OnResurrection(_eventBase);
                }
                OnResurrection = null;
                return;
            case SelectionType.Pullout:
                UpdateUIShow();
                if (OnPullout != null)
                {
                    OnPullout(_eventBase);
                }
                OnPullout = null;
                return;
        }
        //1、修正属性
        OnAmendAttributeEnd = AmendAttributeEnd;
        List<float> amendValues = selectionAttribute.event_selection.attributeCost;
        if (selectionAttribute.ExpRward == 0 && amendValues.Count == 0)
        {
            OnCallAmendAttributeEnd(selectionAttribute);
            return;
        }
        List<float> values = new List<float>() { selectionAttribute.ExpRward };
        values.AddRange(AmendAttribute(amendValues));
        if (_amendAttribute == null)
        {
            _amendAttribute = GameModules.popupSystem.GetPopupObj(ModuleName.amendAttributePopup).GetComponent<UIAmendAttributePopup>();
        }
        _amendAttribute.Param = selectionAttribute;
        _amendAttribute.OnPlayEnd = OnCallAmendAttributeEnd;
        _amendAttribute.OpenUI(values, _charTransform.position);
    }

    /// <summary>
    /// 修正属性结束回调
    /// </summary>
    /// <param name="param"></param>
    private void OnCallAmendAttributeEnd(object param)
    {
        if (OnAmendAttributeEnd != null)
        {
            OnAmendAttributeEnd(param);
        }

        OnAmendAttributeEnd = null;
    }

    /// <summary>
    /// 对话框结束回调
    /// </summary>
    /// <param name="param"></param>
    private void OnCallSelectionDialogEnd(object param)
    {
        if (OnSelectionDialogEnd != null)
        {
            OnSelectionDialogEnd(param);
        }

        OnSelectionDialogEnd = null;
    }
    /// <summary>
    /// 战斗结束回调
    /// </summary>
    /// <param name="param"></param>
    private void OnCallCombatEnd(object param)
    {
        if (OnCombatEnd != null)
        {
            OnCombatEnd(param);
        }

        OnCombatEnd = null;
    }
    /// <summary>
    /// 后续对话框结束回调
    /// </summary>
    /// <param name="param"></param>
    private void OnCallAfterDialogEnd(object param)
    {
        if (OnAfterDialogEnd != null)
        {
            OnAfterDialogEnd(param);
        }

        OnAfterDialogEnd = null;
    }


    /// <summary>
    /// 奖励结束回调
    /// </summary>
    /// <param name="param"></param>
    private void OnCallRwardEnd(object param)
    {
        if (OnRwardEnd != null)
        {
            OnRwardEnd(param);
        }

        OnRwardEnd = null;
    }
    //战斗事件
    private void OnCombatEvent(CombatEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case CombatEventType.StartCombat:
                break;
            case CombatEventType.ReadyCombat:
                break;
            case CombatEventType.ReadyCombatOk:
                break;
            case CombatEventType.CombatResult:
                EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
                _isCombatWin = (bool)arg2;
                OnCallCombatEnd(_selectionAttribute);
                if (_isCombatWin)
                {
                    //角色复活
                    TeamSystem.Instance.CombatRevivableChar(Combat_configConfig.GetCombat_config().resurrectProp);
                }
                break;
        }
    }


    #endregion

    #region 基本操作

    /// <summary>
    /// 修正属性
    /// </summary>
    private List<float> AmendAttribute(List<float> amendValues)
    {
        float value;
        for (int i = 0; i < amendValues.Count; i++)
        {
            foreach (CombatUnit combat in _eventAttribute.teamAttribute.combatUnits)
            {
                switch (i)
                {
                    case 0://护盾
                        if (amendValues[i] < 0)
                        {
                            combat.maxShield -= (int)(combat.maxShield * amendValues[i]);
                        }
                        value = (int)Math.Max(0, combat.shield - combat.maxShield * amendValues[i]);
                        amendValues[i] = (value - combat.shield) == 0 ? 0 : -amendValues[i];
                        combat.shield = (int)value;
                        break;
                    case 1: //护甲
                        if (amendValues[i] < 0)
                        {
                            combat.maxArmor -= (int)(combat.maxArmor * amendValues[i]);
                        }
                        value = (int)Math.Max(0, combat.armor - combat.maxArmor * amendValues[i]);
                        amendValues[i] = (value - combat.armor) == 0 ? 0 : -amendValues[i];
                        combat.armor = (int)value;
                        break;
                    case 2: //生命
                        if (amendValues[i] < 0)
                        {
                            combat.maxHp -= (int)(combat.maxHp * amendValues[i]);
                        }
                        value = (int)Math.Max(0, combat.hp - combat.maxHp * amendValues[i]);
                        amendValues[i] = (value - combat.hp) == 0 ? 0 : -amendValues[i];
                        combat.hp = (int)value;
                        break;
                    case 3: //攻击
                        combat.charAttribute.selectionFvDB += amendValues[i];
                        amendValues[i] = -amendValues[i];
                        break;
                }
            }
        }

        return amendValues;
    }

    /// <summary>
    /// 修正属性
    /// </summary>
    private void AmendAttribute(int index, float value)
    {
        if (value == 0)
        {
            return;
        }
        foreach (CombatUnit combat in _eventAttribute.teamAttribute.combatUnits)
        {
            switch (index)
            {
                case 0://护盾
                    combat.shield = (int)Math.Max(0, combat.shield - combat.maxShield * value);
                    break;
                case 1: //护甲
                    combat.armor = (int)Math.Max(0, combat.armor - combat.maxArmor * value);
                    break;
                case 2: //生命
                    combat.hp = (int)Math.Max(0, combat.hp - combat.maxHp * value);
                    break;
                case 3: //攻击
                    combat.charAttribute.selectionFvDB += value;
                    break;
            }
        }

    }
    /// <summary>
    /// 创建战斗
    /// </summary>
    /// <param name="selectionAttribute"></param>
    private void CreateCombat(SelectionAttribute selectionAttribute)
    {
        int mobTeamID;
        bool isCreateOk = TeamSystem.Instance.CreateCombat(selectionAttribute.GetMobTeamInfo(out mobTeamID), mobTeamID);
        if (isCreateOk)
        {
            EventDispatcher.Instance.CombatEvent.AddEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
            EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.ReadyCombat, (object)null);
        }
    }
    /// <summary>
    /// 添加选项
    /// </summary>
    private void AddSelection(List<SelectionAttribute> selectionAttributes)
    {
        ResourceLoadUtil.DeleteChildObj(_buttonList);
        _isHaveSelectionAside = false;
        _selectionAsideID = -1;
        foreach (SelectionAttribute item in selectionAttributes)
        {
            if (!_isHaveSelectionAside)
            {
                _selectionAsideID = item.event_selection.selectionAside;
                _isHaveSelectionAside = _selectionAsideID != 0;
            }
            switch (item.SelectionType)
            {
                case SelectionType.Ignore:
                case SelectionType.Back:
                    CreateSelectionObj(item, false);
                    break;
                default:
                    CreateSelectionObj(item);
                    break;
            }
        }
    }
    /// <summary>
    /// 创建选项组件
    /// </summary>
    /// <param name="selectionAttribute"></param>
    /// <returns></returns>
    private UIEventSelection CreateSelectionObj(SelectionAttribute selectionAttribute, bool isNew = true)
    {
        GameObject obj = !isNew
            ? _exitObj
            : ResourceLoadUtil.InstantiateRes(
                selectionAttribute.SelectionType == SelectionType.Page ? _selectionPageObj : _selectionBaseObj,
                _buttonList);

        UIEventSelection eventSelection = obj.AddComponent<UIEventSelection>();
        eventSelection.CreateSelection(_eventAttribute, selectionAttribute);
        eventSelection.OnClickSelection += OnCallBackClickSelection;
        return eventSelection;
    }

    /// <summary>
    /// 添加选项旁白
    /// </summary>
    private void AddSelectionAside()
    {
        _selectionAsideObj.SetActive(false);
        if (!_isHaveSelectionAside)
        {
            return;
        }
        Text_template textTemplate = Text_templateConfig.GetText_config(_selectionAsideID);
        if (textTemplate == null)
        {
            return;
        }
        //GameObject obj = _selectionAsideObj/* ResourceLoadUtil.InstantiateRes(_selectionAsideObj, _buttonList)*/;
        _selectionAsideObj.transform.Find("Text").GetComponent<Text>().text = textTemplate.text;
        _selectionAsideObj.SetActive(true);
        //obj.transform.SetAsFirstSibling();
    }
    /// <summary>
    /// 添加返回按钮显示
    /// </summary>
    private void AddBackButtonShow()
    {
        if (_backSelection != null)
        {
            DestroyImmediate(_backSelection);
        }
        //添加撤退、复活
        if (_eventConfig == null)
        {
            _eventConfig = Event_configConfig.GetConfig();
        }
        //
        switch (_eventAttribute.EventType)
        {
            case WPEventType.Trap:
                break;
            case WPEventType.Combat:
                //返回
                _backSelection = CreateSelectionObj(new SelectionAttribute(_eventConfig.defaultSelection[1], 0, _eventAttribute), false);
                break;
            default:
                //返回
                _backSelection = CreateSelectionObj(new SelectionAttribute(_eventConfig.defaultSelection[1], 0, _eventAttribute), false);
                ////无视
                //_backSelection = CreateSelectionObj(new SelectionAttribute(_eventConfig.defaultSelection[0], 0, _eventAttribute), false);
                break;
        }
        _exitObj.SetActive(true);
    }

    /// <summary>
    /// 更新选项
    /// </summary>
    /// <param name="selectionAttributes"></param>
    private void UpdateSelection(List<SelectionAttribute> selectionAttributes)
    {
        ResourceLoadUtil.DeleteChildObj(_buttonList);
        AddSelection(selectionAttributes);
        AddSelectionAside();
        OpenSelectionShow();
        UpdateUIShow(true);
    }

    /// <summary>
    /// 打开选项显示
    /// </summary>
    private void OpenSelectionShow()
    {
        _showTransform.gameObject.SetActive(true);
    }

    /// <summary>
    /// 关闭选项显示
    /// </summary>
    private void CloseSelectionShow()
    {
        _showTransform.gameObject.SetActive(false);
    }
    /// <summary>
    /// 更新UI显示
    /// </summary>
    private void UpdateUIShow(bool isShow = false)
    {
        gameObject.SetActive(isShow);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (_isFirst)
        {
            return;
        }
        //
        _bgCanvas = transform.GetComponent<CanvasGroup>();
        _showTransform = transform.Find("Show");
        _temp = transform.Find("Temp");
        _selectionBaseObj = _temp.Find("Base").gameObject;
        _selectionAsideObj = _showTransform.Find("Aside").gameObject;
        _selectionPageObj = _temp.Find("Page").gameObject;
        //
        _exitObj = _showTransform.Find("Exit").gameObject;
        _buttonList = _showTransform.Find("ButtonList");
        //
        _isFirst = true;
    }

    #endregion

    //
    private float _visitTime;
    private bool _isCombatSelection;
    private bool _isCombatWin;
    private bool _isFirst;
    private int _nowLayer;
    private bool _isShowSelection;
    private bool _isHaveSelectionAside;
    private long _selectionAsideID;
    //组件
    private CanvasGroup _bgCanvas;
    private Transform _showTransform;
    private Transform _buttonList;
    private Transform _temp;
    private GameObject _exitObj;
    private GameObject _selectionBaseObj;
    private GameObject _selectionAsideObj;
    private GameObject _selectionPageObj;
    //
    private Transform _charTransform;
    //弹窗
    private UIDialogPopup _dialogPopup;
    private UIBountyInfoPopup _bountyInfoPopup;
    private UIRwardPopup _rwardPopup;
    private UIAmendAttributePopup _amendAttribute;
    //
    private EventAttribute _eventAttribute;
    private SelectionAttribute _selectionAttribute;
    private UIExploreEventBase _eventBase;
    private UIEventSelection _backSelection;
    //
    private Event_config _eventConfig;
}