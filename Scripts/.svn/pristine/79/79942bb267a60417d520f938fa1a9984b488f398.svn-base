
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/9/2019
//Note:     
//--------------------------------------------------------------

using DG.Tweening;
using Guide;

/// <summary>
/// 流程引导界面
/// </summary>
public partial class GuidePanel: UIPanelBehaviour
{
    private Dia m_dia;

    private GuideStep m_step;
    private bool m_animPlaying;

    private Script_storyLine m_stroy;
    private DOTweenAnimation m_dg;

    protected override void OnAwake()
    {
        base.OnAwake();
        InitComponent();
    }

    private void InitComponent()
    {
        m_dia = Utility.RequireComponent<Dia>(transform.Find("Dia").gameObject);
        m_dia.InitComponent(ClickBtn);

        m_dg = transform.Find("QieHuan").GetComponent<DOTweenAnimation>();
        DoTweenPlayCallBack qiehuan = m_dg.GetComponent<DoTweenPlayCallBack>();
        qiehuan.ComplateCall = QieHuanAnimCall;

        Utility.AddButtonListener(transform.Find("Btn"),ClickBtn);
    }

    public void SetCurrentStep(GuideStep step)
    {
        m_animPlaying = false;
        m_step = step;
        m_stroy = Script_storyLineConfig.GetScByUid((int)step);
        m_dg.gameObject.transform.localPosition = new UnityEngine.Vector3(1500,0,0);
        if(step == GuideStep.EmptyCity)
        {
            EmptyShowDia();
        }
        else if(step == GuideStep.Core)
        {
            CoreShowDia();
        }
        else if(step == GuideStep.Hall)
        {
            HallShowDia();
        }
        else if(step == GuideStep.Barrack)
        {
            BarrackShowDia();
        }
    }

    private void ClickBtn()
    {
        if(!m_dia.DiaIsEnd())
        {
            m_dia.UpdateInfo();
        }
        else
        {
            if(!m_animPlaying)
                DiaShowEnd();
        }
    }

    private void DiaShowEnd()
    {
        if(m_step == GuideStep.EmptyCity)
        {
            EmpytyDiaEnd();
        }
        else if(m_step == GuideStep.Core)
        {
            CoreDiaEnd();
        }
        else if(m_step == GuideStep.Hall)
        {
            HallDiaEnd();
        }
        else if(m_step == GuideStep.Barrack)
        {
            BarrackDiaEnd();
        }
    }

    private void QieHuanAnimCall()
    {
        if(m_step == GuideStep.EmptyCity)
        {
            EmpyQieHuanEnd();
        }
        else if(m_step == GuideStep.Core)
        {
            CoreQieHuanEnd();
        }
        else if(m_step == GuideStep.Hall)
        {
            HallQieHuanEnd();
        }
        else if(m_step == GuideStep.Barrack)
        {
            //todo
        }
    }

    private void SetDiaInfo(int diaId)
    {
        m_dia.SetDiaInfo(diaId);
        m_dia.UpdateInfo();
    }

    private void HideNpc()
    {
        m_dia.HideAllNpc();
    }
}
