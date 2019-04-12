using System;
using UnityEngine;
using System.Collections;

public class ConfirmPanelUtil
{
    /// <summary>
    /// 现实确认框
    /// </summary>
    /// <param name="message">显示内容</param>
    /// <param name="title">标题</param>
    /// <param name="sureCallBack">确定回掉</param>
    /// <param name="cancalCallBack">取消回掉</param>
    /// <param name="showSureBtn">是否显示确认按钮</param>
    /// <param name="showCancelBtn">是否显示曲线按钮</param>
    public static void ShowConfirmPanel(string message,string title, 
        Action sureCallBack=null,Action cancalCallBack=null,
         bool showSureBtn = true,bool showCancelBtn = true)
    {
        UIPanelManager.Instance.Show<ConfirmPanel>(CavasType.PopUI);
        ConfirmPanel confirmPanel = (ConfirmPanel) UIPanelManager.Instance.GetUiPanelBehaviour<ConfirmPanel>();
        confirmPanel.SetInfo( sureCallBack, cancalCallBack,title,message,showSureBtn,showCancelBtn);
    }
}
