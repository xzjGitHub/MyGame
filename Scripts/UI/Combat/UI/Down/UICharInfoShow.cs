using System.Collections.Generic;
using System.Linq;
using MCCombat;
using UnityEngine;

public class UICharInfoShow : MonoBehaviour
{
    public int IncentiveFromIndex { get { return _incentiveFromIndex; } }
    public bool IsIncentive { get { return _isIncentive; } }
    public UseManualSkillInfo UseSkillInfo { get { return _useSkillInfo; } }
    public int Index { get { return _combatUnit.initIndex; } }

    //
    public delegate int CallBack1(int charIndex);
    public delegate void CallBack2(int value);
    public delegate void CallBack3(int incentiveFromIndex, bool isIncentive);
    /// <summary>
    /// 使用技能
    /// </summary>
    public CallBack1 CallUseManualSkill;
    /// <summary>
    /// 使用技能
    /// </summary>
    public CallBack1 CallCancelManualSkill;
    /// <summary>
    /// 更新魔力
    /// </summary>
    public CallBack2 CallUpdateMp;
    /// <summary>
    /// 重置激励
    /// </summary>
    public CallBack2 CallResetIncentive;
    /// <summary>
    /// 全体激励生效
    /// </summary>
    public CallBack2 CallAllIncentiveValidity;
    /// <summary>
    /// 重置其他激励
    /// </summary>
    public CallBack2 CallResetOtherIncentive;
    /// <summary>
    /// 全部激励
    /// </summary>
    public CallBack3 CallAllIncentive;



    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(CombatUnit combatUnit, int nowMP = 0)
    {
        if (!_isFirst)
        {
            GetObj();
            _useSkillInfo = new UseManualSkillInfo();
        }
        _combatUnit = combatUnit;
        //初始化技能
        for (int i = 0; i < combatUnit.manualSkills.Count; i++)
        {
            if (i > 1)
            {
                break;
            }
            _manualSkills[i].Init(combatUnit, combatUnit.manualSkills[i], nowMP);
        }

        CommonSkillInfo info = GetCommonSkillInfo(combatUnit);
        if (info == null || info.ID == 0)
        {
            return;
        }
        _commonSkill.Init(info, combatUnit);
    }
    /// <summary>
    /// 创建回合更新状态
    /// </summary>
    public void CreateRoundUpdateInfo()
    {
        _isOneselfIncentiveUsed = false;
        _isIncentive = false;
        _incentiveFromIndex = -1;
        if (_useSkillInfo != null)
        {
            _useSkillInfo.ResetSkill();
        }

        //重置技能显示
        foreach (UIManualSkill item in _manualSkills)
        {
            item.UpdateUseCommonSkillShow(false);
        }

        if (_commonSkill == null)
        {
            return;
        }
        _commonSkill.UpdateShow(true);
    }

    /// <summary>
    /// 回合更新显示
    /// </summary>
    public void RoundUpdateShow(int nowMP = 0)
    {
        if (!_isFirst)
        {
            return;
        }
        //
        if (UICombatTool.Instance.CombatSystem.GetCombatUnitInfo(_combatUnit).hp <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        //
        foreach (UIManualSkill item in _manualSkills)
        {
            item.ResetShow(nowMP);
        }

        _commonSkill.UpdateShow(true);

    }

    /// <summary>
    /// 魔力更新显示
    /// </summary>
    /// <param name="mpValue"></param>
    public void MPUpdateShow(int mpValue)
    {
        foreach (UIManualSkill item in _manualSkills)
        {
            item.MPUpdateShow(mpValue);
        }
    }
    /// <summary>
    /// 更新激励技能显示
    /// </summary>
    public void UpdateAllIncentiveShow(int incentiveIndex, bool isShow = true)
    {
        if (_useSkillInfo == null || _useSkillInfo.IsSelect)
        {
            return;
        }
        if (_isIncentive)
        {
            UpdateIncentiveManualSkillShow(-1, false, 2);
            if (!_isOneselfIncentiveUsed)
            {
                _commonSkill.UpdateShow(true);
            }

        }
        _incentiveFromIndex = isShow ? incentiveIndex : -1;
        _isIncentive = isShow;
        _incentiveFromIndex = isShow ? incentiveIndex : -1;
        foreach (UIManualSkill item in _manualSkills)
        {
            item.UpdateUseCommonSkillShow(isShow, 2);
        }
    }
    /// <summary>
    /// 重置激励
    /// </summary>
    /// <param name="incentiveIndex"></param>
    public void ResetIncentive(int incentiveIndex)
    {
        ////选择了技能
        //if (_useSkillInfo.IsSelect)
        //{
        //    //没有激励
        //    if (!_useSkillInfo.isIncentive)
        //    {
        //        return;
        //    }
        //    //不是重置的激励
        //    if (_useSkillInfo.incentiveFromIndex != incentiveIndex)
        //    {
        //        return;
        //    }
        //    //重置的是自己的激励
        //    if (incentiveIndex == _combatUnit.initIndex)
        //    {
        //        //如果激励已经生效
        //        if (_isOneselfIncentiveUsed)
        //        {
        //            _isOneselfIncentiveUsed = false;
        //            _commonSkill.UpdateButtonShow(true);
        //        }
        //        _commonSkill.UpdateShow(false);
        //        UpdateIncentiveManualSkillShow(-1, false, (int)_commonSkill.CSkillInfo.CommonType);
        //        return;
        //    }
        //    //此时这个激励是全体
        //    LogHelperLSK.LogError("重置全体激励：" + incentiveIndex + "  charIndex=" + _combatUnit.initIndex + "有误");
        //    return;
        //}
        ////没有选择技能
        ////自己没有激励
        //if (!_isIncentive)
        //{
        //    return;
        //}
        ////自己有激励、但不是自己的激励
        //if (_combatUnit.initIndex != incentiveIndex)
        //{
        //    if (_incentiveFromIndex == incentiveIndex)
        //    {
        //        UpdateIncentiveManualSkillShow(-1, false, 2);
        //    }
        //    return;
        //}
        //return;
        //重置的是自己的激励
        if (incentiveIndex == _combatUnit.initIndex)
        {
            //如果激励已经生效
            if (_isOneselfIncentiveUsed)
            {
                _isOneselfIncentiveUsed = false;
                if (_useSkillInfo.IsSelect)
                {
                    _commonSkill.UpdateShow(false);
                    return;
                }
                _commonSkill.UpdateShow(true);
            }
            else
            {
                _commonSkill.UpdateShow(true);
            }
            // _commonSkill.UpdateShow(false);
            UpdateIncentiveManualSkillShow(-1, false, (int)_commonSkill.SkillInfo.CommonType);
            return;
        }
        //自己没有激励
        if (!_isIncentive)
        {
            return;
        }
        //自己有激励、但不是自己的激励
        if (_combatUnit.initIndex != incentiveIndex)
        {
            if (_incentiveFromIndex == incentiveIndex)
            {
                UpdateIncentiveManualSkillShow(-1, false, 2);
            }
            return;
        }
    }
    /// <summary>
    /// 全体激励生效
    /// </summary>
    /// <param name="incentiveIndex"></param>
    public void AllIncentiveValidity(int incentiveIndex)
    {
        //自己的激励已经生效
        if (_combatUnit.initIndex == incentiveIndex)
        {
            _commonSkill.UpdateButtonShow(false);
            if (!_useSkillInfo.IsSelect)
            {
                _isOneselfIncentiveUsed = true;
                UpdateIncentiveManualSkillShow(-1, false, 2);
            }
            return;
        }
        if (!_useSkillInfo.IsSelect)
        {
            UpdateIncentiveManualSkillShow(-1, false, 2);
        }

    }
    /// <summary>
    /// 重置其他激励
    /// </summary>
    /// <param name="index"></param>
    public void ResetOtherIncentive(int index)
    {
        //是否选择了技能
        if (_useSkillInfo.IsSelect)
        {
            return;
        }
        if (!_isIncentive)
        {
            return;
        }
        if (_incentiveFromIndex == index)
        {
            return;
        }
        _commonSkill.UpdateShow(true);

        UpdateIncentiveManualSkillShow(-1, false);
    }
    /// <summary>
    /// 更新激励显示
    /// </summary>
    private void UpdateIncentiveManualSkillShow(int incentiveIndex, bool isShow = true, int commonType = 1)
    {
        _incentiveFromIndex = isShow ? incentiveIndex : -1;
        _isIncentive = isShow;
        foreach (UIManualSkill item in _manualSkills)
        {
            item.UpdateUseCommonSkillShow(isShow, commonType);
        }
    }

    private CommonSkillInfo GetCommonSkillInfo(CombatUnit combatUnit)
    {
        if (combatUnit.initIndex != 0)
        {
            return (CommonSkillInfo)combatUnit.commonSkills[0];
        }

        if (combatUnit.charAttribute.IsCommander)
        {
            return (CommonSkillInfo)combatUnit.commonSkills.Find(a => (a as CommonSkillInfo).CommonType == CommonSkillType.All);
        }
        return (CommonSkillInfo)combatUnit.commonSkills[0];


    }

    /// <summary>
    /// 点击技能回调
    /// </summary>
    private bool CallBackClickSkill(UIManualSkill manualSkill)
    {
        //是否已经选择了技能
        if (_useSkillInfo.IsSelect)
        {
            //取消自己
            if (manualSkill.SkillInfo.ID == _useSkillInfo.manualSkillID)
            {
                if (_isIncentive)
                {
                    //重置激励
                    if (CallResetIncentive != null)
                    {
                        CallResetIncentive(_incentiveFromIndex);
                    }
                }
                if (!_isOneselfIncentiveUsed)
                {
                    //激励可以使用
                    _commonSkill.UpdateButtonShow(true);
                }
                return false;
            }
            //取消以前的技能
            UIManualSkill info = _manualSkills.Find(a => a.SkillID == _useSkillInfo.manualSkillID);
            if (info != null)
            {
                info.CancelClick();
            }
            //自身不能激励、但是现在处于激励状态
            if (!manualSkill.IsCanAlternative && _isIncentive)
            {
                //重置激励
                if (CallResetIncentive != null)
                {
                    CallResetIncentive(_incentiveFromIndex);
                }
            }
            UseManualSkill(manualSkill);
            return true;
        }
        //没有选择技能
        if (_isIncentive && !manualSkill.IsCanAlternative)
        {
            //重置激励
            if (CallResetIncentive != null)
            {
                CallResetIncentive(_incentiveFromIndex);
            }
        }
        UseManualSkill(manualSkill);
        return true;
    }
    /// <summary>
    /// 使用技能
    /// </summary>
    /// <param name="manualSkill"></param>
    private void UseManualSkill(UIManualSkill manualSkill)
    {
        //使用技能
        _useSkillInfo.UseSkill(_isIncentive && manualSkill.IsCanAlternative, _incentiveFromIndex, _combatUnit.initIndex, manualSkill.SkillInfo.ID, 0);
        //更新自己激励按钮显示
        _commonSkill.UpdateButtonShow(false);
        //更新魔力
        if (CallUpdateMp != null)
        {
            CallUpdateMp(-manualSkill.EnergyCost);
        }
        //更新使用技能后得到索引
        if (CallUseManualSkill != null)
        {
            _useSkillInfo.selectIndex = CallUseManualSkill(_combatUnit.initIndex);
        }
        //更新手动技能高亮显示
        foreach (UIManualSkill item in _manualSkills)
        {
            if (item == manualSkill)
            {
                if (_isIncentive)
                {
                    if (manualSkill.IsCanAlternative)
                    {
                        CommonSkillType type = _useSkillInfo.incentiveFromIndex == _combatUnit.initIndex ? _commonSkill.SkillInfo.CommonType : CommonSkillType.All;
                        //显示高亮
                        item.UpdateUseCommonSkillShow(true, (int)type);
                    }
                }
                continue;
            }
            item.UpdateUseCommonSkillShow(false);
        }
        //

        //全体激励生效
        if (_useSkillInfo.isIncentive)
        {
            if (_useSkillInfo.incentiveFromIndex != _combatUnit.initIndex || _commonSkill.SkillInfo.CommonType == CommonSkillType.All)
            {
                if (CallAllIncentiveValidity != null)
                {
                    CallAllIncentiveValidity(_useSkillInfo.incentiveFromIndex);
                }
            }

        }
        LogHelperLSK.LogWarning("isIncentive=" + _useSkillInfo.isIncentive
    + "  incentiveIndex=" + _useSkillInfo.incentiveFromIndex
    + "  charIndex=" + _useSkillInfo.charIndex
    + "  selectIndex=" + _useSkillInfo.selectIndex
    + "  manualSkillID=" + _useSkillInfo.manualSkillID
    + "  isSelect=" + _useSkillInfo.IsSelect);
    }
    /// <summary>
    /// 取消技能回调
    /// </summary>
    private bool CallBackCancelSkill(UIManualSkill manualSkill)
    {
        _useSkillInfo.ResetSkill();
        //更新魔力
        if (CallUpdateMp != null)
        {
            CallUpdateMp(manualSkill.EnergyCost);
        }
        if (CallCancelManualSkill != null)
        {
            CallCancelManualSkill(_combatUnit.initIndex);
        }
        return true;
    }
    /// <summary>
    /// 点击通用技能回调
    /// </summary>
    private bool CallBackCommonSkillClick(CommonSkillInfo info)
    {
        //if (_isOneselfIncentiveUsed)
        //{
        //    return true;
        //}
        //相同的激励
        if (_incentiveFromIndex == _combatUnit.initIndex)
        {
            UpdateIncentiveManualSkillShow(_combatUnit.initIndex, false, (int)info.CommonType);
            if (CallResetOtherIncentive != null)
            {
                CallResetOtherIncentive(_combatUnit.initIndex);
            }

            return false;
        }
        switch (info.CommonType)
        {
            case CommonSkillType.All:
                //指挥官激励
                if (CallAllIncentive != null)
                {
                    CallAllIncentive(_combatUnit.initIndex, true);
                }
                break;
            default:
                UpdateIncentiveManualSkillShow(_combatUnit.initIndex, true, (int)info.CommonType);
                if (CallResetOtherIncentive != null)
                {
                    CallResetOtherIncentive(_combatUnit.initIndex);
                }

                break;
        }
        return true;
        bool isUse = _incentiveFromIndex != _combatUnit.initIndex;
        if (true)
        {

        }

        if (!isUse)
        {
            if (_useSkillInfo.IsSelect)
            {
                LogHelperLSK.LogError("已经选择了技能,先取消技能");
                return true;
            }
        }
        else
        {
            //已经选择了技能
            if (_useSkillInfo.IsSelect)
            {
                LogHelperLSK.LogError("已经选择了技能,先取消技能");
                return false;
            }
            //重置以前的激励
            if (CallResetIncentive != null)
            {
                CallResetIncentive(_incentiveFromIndex);
            }
        }
        //更新显示
        switch (info.CommonType)
        {
            case CommonSkillType.All:
                //指挥官激励
                if (CallAllIncentive != null)
                {
                    CallAllIncentive(_combatUnit.initIndex, isUse);
                }
                break;
            default:
                UpdateIncentiveManualSkillShow(_combatUnit.initIndex, isUse, 1);
                if (CallResetOtherIncentive != null)
                {
                    CallResetOtherIncentive(_combatUnit.initIndex);
                }
                break;
        }

        return isUse;
    }

    //获得组件
    private void GetObj()
    {
        //添加手动技能
        foreach (Transform item in transform.Find("ManualSkill"))
        {
            _manualSkills.Add(item.gameObject.AddComponent<UIManualSkill>());
            _manualSkills.Last().CallClickSkill = CallBackClickSkill;
            _manualSkills.Last().CallCancelSkill = CallBackCancelSkill;
            _manualSkills.Last().gameObject.SetActive(false);
        }
        //添加通用技能
        foreach (Transform item in transform.Find("CommonSkill"))
        {
            _commonSkill = item.gameObject.AddComponent<UICommonSkill>();
            _commonSkill.CallClickButton = CallBackCommonSkillClick;
            _commonSkill.gameObject.SetActive(false);
        }
        _isFirst = true;
    }

    //
    private bool _isFirst;
    //
    private bool _isOneselfIncentiveUsed;
    private bool _isIncentive;
    private int _incentiveFromIndex = -1;
    //
    private List<UIManualSkill> _manualSkills = new List<UIManualSkill>();
    private UICommonSkill _commonSkill;
    private CombatUnit _combatUnit;
    private UseManualSkillInfo _useSkillInfo;
}
