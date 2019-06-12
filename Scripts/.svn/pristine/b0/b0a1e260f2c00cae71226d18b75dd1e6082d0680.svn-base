
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/30/2019
//Note:     
//--------------------------------------------------------------

using System;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GuideBountyPanel: UIPanelBehaviour
{
    public Action<object> CloseCall;

    protected override void OnShow(List<object> parmers = null)
    {
        base.OnShow(parmers);
    }

    public void SetInfo(int id)
    {
        UIBountyInfoPopup reward = Utility.RequireComponent<UIBountyInfoPopup>(
            transform.Find("BountyInfoPopup").gameObject);
        reward.OpenUI(id,BuountyUIState.Accepted); 
        reward.OnClose = TaskColose;
    }

    private void TaskColose(object obj)
    {
        if(CloseCall != null)
            CloseCall(obj);
        UIPanelManager.Instance.Hide<GuideBountyPanel>();
    }
}
