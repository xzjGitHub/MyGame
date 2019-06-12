
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/30 8:32:46
//Note:     
//--------------------------------------------------------------

using Barrack.View;
using EventCenter;
using Guide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public partial class GuidePanel
{
    private void HallShowDia()
    {
        m_isFirst = true;
        SetDiaInfo(m_stroy.dialogID[0]);
    }

    private void HallDiaEnd()
    {
        HideNpc();
        if(m_isFirst)
        {
            RewordCharPanel reward = UIPanelManager.Instance.Show<RewordCharPanel>();
            reward.CloseCallBack = () =>
            {
                m_isFirst = false;
                SetDiaInfo(m_stroy.dialogID[1]);
            };
        }
        else
        {
            HideNpc();
            //显示核心按钮
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.ShowMainBtn,
                new List<MainPanelDefine> { MainPanelDefine.CharBtn,MainPanelDefine.BagBtn,MainPanelDefine.BarrackBtn });
            //播放切换动画
            m_dg.DORestart();
            m_animPlaying = true;
        }
    }

    private void HallQieHuanEnd()
    {
        StartCoroutine(ShowBarrack());
    }

    private IEnumerator ShowBarrack()
    {
        yield return new WaitForSeconds(1f);
        UIPanelManager.Instance.Hide<GuidePanel>(false);
        UIPanelManager.Instance.Hide<HallPanel>();
        GuideSys.Instance.SetHaveEnd(GuideStep.Hall);

        GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
        if(step != GuideStep.None)
        {
            BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Barracks);
            //BarrackPanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<BarrackPanel>();

            //panel.NpcWalkEnd = () =>
            //{
            UIPanelManager.Instance.Show<GuidePanel>();
           // m_cav.sortingOrder = 100;
            SetCurrentStep(step);
            // };
        }
        m_animPlaying = false;
    }
}

