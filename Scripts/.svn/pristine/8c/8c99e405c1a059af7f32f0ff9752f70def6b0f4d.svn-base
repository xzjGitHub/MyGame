using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


/// <summary>
/// 弹窗系统
/// </summary>
public class PopupSystem : IGameModule
{
    //
    Dictionary<string ,GameObject>popupGameObjects=new Dictionary<string, GameObject>();


    /// <summary>
    /// 注册弹窗组件
    /// </summary>
    /// <param name="_objName"></param>
    /// <param name="_obj"></param>
    public void RegisterPopupObj(string _objName,GameObject _obj)
    {
        if (popupGameObjects.ContainsKey(_objName))
        {
            popupGameObjects[_objName] = _obj;
            return;
        }
        popupGameObjects.Add(_objName,_obj);
    }

    /// <summary>
    /// 得到弹窗组件
    /// </summary>
    /// <param name="_objName"></param>
    /// <returns></returns>
    public GameObject GetPopupObj(string _objName)
    {
        return !popupGameObjects.ContainsKey(_objName) ? null : popupGameObjects[_objName];
    }


    /// <summary>
    /// 关闭组件
    /// </summary>
    /// <param name="param"></param>
    void OnClosePopupObj(object param)
    {
        if (param==null)
        {
            return;
        }
        if (popupGameObjects.ContainsKey((string)param))
        {
            popupGameObjects[(string)param].SetActive(false);
        }
    }    
    /// <summary>
    /// 关闭组件
    /// </summary>
    /// <param name="param"></param>
    void OnCloseAllPopupObj(object param)
    {
        foreach (var item in popupGameObjects)
        {
            if (item.Value.activeSelf)
            {
                item.Value.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 打开组件
    /// </summary>
    /// <param name="param"></param>
    void OnOpenPopupObj(object param)
    {
        if (param == null)
        {
            return;
        }
        if (popupGameObjects.ContainsKey((string)param))
        {
            popupGameObjects[(string)param].SetActive(true);
        }
    }



    #region 重写接口

    public void AfterStartModule()
    {

    }

    public void AfterStopModule()
    {

    }

    public void AfterUpdateModule()
    {

    }

    public void BeforeStartModule()
    {

    }

    public void BeforeStopModule()
    {

    }

    public void BeforeUpdateModule()
    {

    }

    public void OnFreeScene()
    {

    }

    public void StartModule()
    {

    }

    public void StopModule()
    {

    }

    public void UpdateModule()
    {

    }

    #endregion

}
