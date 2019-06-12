
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/30 8:48:08
//Note:     
//--------------------------------------------------------------

using Barrack.View;
using EventCenter;
using Guide;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public partial class GuidePanel
{
    private void BarrackShowDia()
    {
        SetDiaInfo(m_stroy.dialogID[0]);
    }

    private void BarrackDiaEnd()
    {
        HideNpc();

        GuideBountyPanel panel = UIPanelManager.Instance.Show<GuideBountyPanel>();
        panel.CloseCall = TaskColose;
        panel.SetInfo(m_stroy.dialogID[0]);
    }

    private void TaskColose(object obj)
    {
        HideNpc();

        EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.ShowMainBtn,
            new List<MainPanelDefine> { MainPanelDefine.AdvBtn });
        GuideSys.Instance.SetHaveEnd(GuideStep.Barrack);
        //播放切换动画
        // m_dg.DORestart();

        GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
        if(step != GuideStep.None)
        {
            //todo
            //BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Barracks);
            //BarrackPanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<BarrackPanel>();

            //panel.NpcWalkEnd = () =>
            //{
            UIPanelManager.Instance.Show<GuidePanel>();
            //m_cav.sortingOrder = 100;
            SetCurrentStep(step);
            //};
        }
        else
        {
            UIPanelManager.Instance.Hide<GuidePanel>(true);
            BarrackPanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<BarrackPanel>();
            panel.PlayNpcWalk();
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.ShowMainPanelBtn);
        }
    }
}

