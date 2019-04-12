using Spine;
using Spine.Unity;
using UnityEngine;

public class GetCharPanel: UIPanelBehaviour
{
    private const string IdleName = "Idle";
    private const string CelebrateName = "Celebrate";

    private SkeletonGraphic m_skeletonGraphic;
    private CharAttribute m_attr;

    protected override void OnAwake()
    {
        Utility.AddButtonListener(transform.Find("Mask"),Close);
    }

    public void UpdateInfo(CharAttribute attr)
    {
        m_attr = attr;

        GameObject obj = PlayerPool.Instance.GetPlayer(m_attr);
        Utility.SetParent(obj,transform.Find("CharPos"),true,new Vector3(0.7f,0.7f,1f));
        m_skeletonGraphic = obj.GetComponent<SkeletonGraphic>();
        if(m_skeletonGraphic == null)
            Debug.LogError("m_skeletonGraphic is null,id: " + m_attr.templateID);
        if(m_skeletonGraphic.AnimationState == null)
            Debug.LogError("m_skeletonGraphic.AnimationState is null,id: " + m_attr.templateID);

        if(m_skeletonGraphic.AnimationState != null)
        {
            m_skeletonGraphic.AnimationState.SetAnimation(0,CelebrateName,false);
            m_skeletonGraphic.AnimationState.Complete += PlayCeleBrateComplete;
        }
    }


    //public void PlayIdleComplete(TrackEntry trackEntry)
    //{
    //    m_skeletonGraphic.AnimationState.Complete -= PlayIdleComplete;

    //    m_skeletonGraphic.AnimationState.SetAnimation(0,CelebrateName,false);
    //    m_skeletonGraphic.AnimationState.Complete += PlayCeleBrateComplete;
    //}

    public void PlayCeleBrateComplete(TrackEntry trackEntry)
    {
        m_skeletonGraphic.AnimationState.Complete -= PlayCeleBrateComplete;
        m_skeletonGraphic.AnimationState.SetAnimation(0,IdleName,true);
    }

    private void Close()
    {
        PlayerPool.Instance.Free(m_attr.charID);
        UIPanelManager.Instance.Hide<GetCharPanel>();
    }
}

