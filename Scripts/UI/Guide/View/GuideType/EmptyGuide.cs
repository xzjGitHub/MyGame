
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/29 15:21:34
//Note:     
//--------------------------------------------------------------

using System.Collections.Generic;
using Spine;
using EventCenter;
using Guide;
using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public partial class GuidePanel
{
    private void EmptyShowDia()
    {
        SetDiaInfo(m_stroy.dialogID[0]);
    }

    private void EmpytyDiaEnd()
    {
        HideNpc();
        //显示核心按钮
        EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.ShowMainBtn,
            new List<MainPanelDefine> { MainPanelDefine.CoreBtn });
        //播放切换动画
        m_dg.DOPlay();
        m_animPlaying = true;
    }

    private void EmpyQieHuanEnd()
    {
        StartCoroutine(ShowCore());
    }

    private IEnumerator ShowCore()
    {
        yield return new WaitForSeconds(1f);
        UIPanelManager.Instance.Hide<EmptyPanel>();
        UIPanelManager.Instance.Hide<GuidePanel>(false);
        GuideSys.Instance.SetHaveEnd(GuideStep.EmptyCity);

        GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
        if(step != GuideStep.None)
        {
            BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Core);
            //CorePanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<CorePanel>();
            //if(panel != null)
            //    panel.PlayNpcWalk();
            //panel.NpcWalkEnd = () =>
            //  {
            UIPanelManager.Instance.Show<GuidePanel>();
            // m_cav.sortingOrder = 100;
            SetCurrentStep(step);
            //  };
        }
        m_animPlaying = false;
    }
}

