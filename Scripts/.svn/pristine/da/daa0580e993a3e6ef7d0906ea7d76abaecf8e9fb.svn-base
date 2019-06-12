
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/29 17:36:38
//Note:     
//--------------------------------------------------------------

using Core.View;
using EventCenter;
using Guide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public partial class GuidePanel
{
    private bool m_isFirst;

    private void CoreShowDia()
    {
        m_isFirst = true;
        SetDiaInfo(m_stroy.dialogID[0]);
    }

    private void CoreDiaEnd()
    {
        HideNpc();
        if(m_isFirst)
        {
            //显示奖励
            RewordItemPanel reward = UIPanelManager.Instance.Show<RewordItemPanel>();
            reward.Init(m_stroy.dialogID[0]);
            reward.CloseCallBack = () =>
            {
                m_isFirst = false;
                SetDiaInfo(m_stroy.dialogID[1]);
            };
        }
        else
        {
            HideNpc();
            //显示按钮
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.ShowMainBtn,
                new List<MainPanelDefine> { MainPanelDefine.HallBtn });
            //播放切换动画
            m_animPlaying = true;
            m_dg.DORestart();
        }
    }

    private void CoreQieHuanEnd()
    {
        StartCoroutine(ShowHall());
    }

    private IEnumerator ShowHall()
    {
        yield return new WaitForSeconds(1f);
        UIPanelManager.Instance.Hide<GuidePanel>(false);
        UIPanelManager.Instance.Hide<CorePanel>();
        GuideSys.Instance.SetHaveEnd(GuideStep.Core);

        GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
        if(step != GuideStep.None)
        {
            BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.TownHall);
            //HallPanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<HallPanel>();

            //panel.NpcWalkEnd = () =>
            //{
            UIPanelManager.Instance.Show<GuidePanel>();
           // m_cav.sortingOrder = 100;
            SetCurrentStep(step);
            //  };
        }
        m_animPlaying = false;
    }
}

