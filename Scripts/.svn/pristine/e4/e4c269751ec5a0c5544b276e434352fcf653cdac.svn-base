using System;
using Spine.Unity;
using UnityEngine;
using Spine;

public class UINPC: MonoBehaviour
{
    private GameObject m_npcObj;
    private SkeletonGraphic m_npcGraphic;

    private Action m_playComplateAction;

    private float m_randomTime;
    private float m_timer;
    private bool m_startCountDown;
    private bool m_playRandomAnim;

    private string WalkAnimName = "ruchang";
    public string IdleAnimName = "Idle1";
    public string LaughAnimName = "Xiao1";

    public void Init(Action action,string npcName = "Npc1_101")
    {
        m_playComplateAction = action;
        m_npcGraphic = transform.GetComponentInChildren<SkeletonGraphic>();
        //m_npcObj = NpcUtil.Instance.GetNpc(NpcType.Npc1,npcName);
        //Utility.SetParent(m_npcObj,transform);
        //m_npcObj.transform.localScale =new Vector3(0.75f,0.75f,0);
        //m_npcObj.transform.localPosition = new Vector3(435,0,0);
        //m_npcGraphic = m_npcObj.GetComponent<SkeletonGraphic>();
        //Debug.LogError(m_npcObj.transform.localPosition);
    }

    public void PlayWalk()
    {
        gameObject.SetActive(true);
        m_npcGraphic.AnimationState.Complete += PlayWalkComplete;
        m_npcGraphic.AnimationState.SetAnimation(0,WalkAnimName,false);
    }

    private void PlayWalkComplete(TrackEntry trackEntry)
    {
        m_npcGraphic.AnimationState.Complete -= PlayWalkComplete;
        m_playComplateAction();
        PlayIdle();
    }


    public void PlayIdle()
    {
        gameObject.SetActive(true);

        m_npcGraphic.AnimationState.SetAnimation(0,IdleAnimName,true);

        m_randomTime = UnityEngine.Random.Range(15,30);
        m_timer = 0;
        m_startCountDown = true;
        // Debug.LogError("随机时间: " + m_randomTime);
    }

    private void Update()
    {
        if(m_startCountDown)
        {
            if(m_timer <= m_randomTime)
            {
                m_timer += Time.deltaTime;
                //  Debug.LogError("m_timer: " +m_timer);
            }
            else
            {
                m_playRandomAnim = true;
                m_startCountDown = false;
                // Debug.LogError("可以播放笑2动画: ");
            }
        }
        if(m_playRandomAnim)
        {
            // Debug.LogError("开始播放笑2动画: ");
            m_npcGraphic.AnimationState.Complete += PlayXiao2Complete;
            m_npcGraphic.AnimationState.SetAnimation(0,LaughAnimName,false);
            m_playRandomAnim = false;
        }
    }

    private void PlayXiao2Complete(TrackEntry trackEntry)
    {
        m_npcGraphic.AnimationState.Complete -= PlayXiao2Complete;
        //  Debug.LogError("笑2动画播放完毕 ");
        PlayIdle();
    }
}

