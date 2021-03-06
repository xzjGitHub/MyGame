﻿using Spine.Unity;
using UnityEngine;

public class UIExploreCombatEvent : UIExploreEventBase
{
    public void CombatInit(EventAttribute eventAttribute)
    {
        InitUI(eventAttribute);
    }

    private void InitUI(EventAttribute eventAttribute)
    {
        if (_isFirst)
        {
            return;
        }
        //更新大小
        _charList = ShowTranS.Find("Char");
        //
        CharRPack charShow = CharRPackConfig.GeCharShowTemplate(eventAttribute.event_template.mobModel);

        if (charShow != null)
        {
            Transform parent = _charList.GetChild(0);
            parent.localScale = Vector3.one * sizeValue;
            LoadSkeletonRes(charShow.charRP, parent, 21);
        }
        else
        {
            LogHelper_MC.LogError("  mobModel=" + eventAttribute.event_template.mobModel + "null");
        }
        _charList.gameObject.SetActive(true);
        _isFirst = true;
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    private SkeletonAnimation LoadSkeletonRes(string _RP_Name, Transform _transform, int _sortingOrder)
    {
        SkeletonAnimation _obj = ResourceLoadUtil.LoadCharModel(_RP_Name, _transform).GetComponent<SkeletonAnimation>();
        if (_obj != null)
        {
            SkeletonTool.SetSkeletonLayer(_obj, _sortingOrder);
        }
        if (_obj != null)
        {
            SkeletonTool.PlayCharAnimation(_obj, CharModuleAction.Idle, null);
        }
        //  _obj.transform.localPosition = Vector3.left * 0.61f;
        return _obj;
    }

    //
    private Transform _charList;
    private bool _isFirst;
}
