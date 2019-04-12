using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class BuilidUtil: Singleton<BuilidUtil>
{
    private BuilidUtil() { }

    GameObject m_btn;

    public SkeletonGraphic InitEff(BuildingTypeIndex index,UINPC npc,GameObject btn,GameObject obj,bool playNpcAnim = true)
    {
        m_btn = btn;
        GameObject eff = obj;

        SkeletonGraphic m_skeletonGraphic = eff.GetComponent<SkeletonGraphic>();

        // m_skeletonGraphic.AnimationState.Complete -= PlayShowComplete;
        m_skeletonGraphic.AnimationState.Complete -= PlayBgComplete;

        if(!BuildUIController.Instance.HaveShowList.Contains(index))
        {
            //m_skeletonGraphic.AnimationState.Complete += PlayShowComplete;
            m_skeletonGraphic.AnimationState.SetAnimation(0,"Show",false);
            if(playNpcAnim)
                npc.PlayWalk();
            BuildUIController.Instance.HaveShowList.Add(index);
        }
        else
        {
            m_skeletonGraphic.AnimationState.Complete += PlayBgComplete;
            m_skeletonGraphic.AnimationState.SetAnimation(0,"Bg",false);
            npc.PlayIdle();
        }
        return m_skeletonGraphic;
    }

    private void PlayShowComplete(TrackEntry trackEntry)
    {
        // BuildUIController.Instance.CanClick = true;
        //  m_btn.SetActive(true);
    }

    private void PlayBgComplete(TrackEntry trackEntry)
    {
        //  BuildUIController.Instance.CanClick = true;
        // Game.Instance.StartCoroutine(UpdateClick());
        m_btn.SetActive(true);
    }

    private IEnumerator UpdateClick()
    {
        yield return new WaitForSeconds(1.5f);
        //  BuildUIController.Instance.CanClick = true;
    }

    public void RemoveEvent(SkeletonGraphic sk)
    {
        // sk.AnimationState.Complete -= PlayShowComplete;
        sk.AnimationState.Complete -= PlayBgComplete;
    }
}

