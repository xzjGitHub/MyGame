﻿using Spine;
using Spine.Unity;
using UnityEngine;

public class GetCharPanel: UIPanelBehaviour
{
    private const string IdleName = "Idle";
    private const string CelebrateName = "Celebrate";

    private SkeletonGraphic m_skeletonGraphic;
    private CharAttribute m_attr;
    private GameObject m_charObj;
    private string m_charObjAssetName;

    protected override void OnAwake()
    {
        Utility.AddButtonListener(transform.Find("Mask"),Close);
    }

    public void UpdateInfo(CharAttribute attr)
    {
        m_attr = attr;

        CharRPack info = CharRPackConfig.GeCharShowTemplate(m_attr.char_template.templateID);
        m_charObjAssetName = info.charRP;
        m_charObj = PrefabPool.Instance.GetObjSync(m_charObjAssetName,Res.AssetType.UIChar);

        Utility.SetParent(m_charObj,transform.Find("CharPos"),true,new Vector3(0.7f,0.7f,1f));
        m_skeletonGraphic = m_charObj.GetComponent<SkeletonGraphic>();
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

    public void PlayCeleBrateComplete(TrackEntry trackEntry)
    {
        m_skeletonGraphic.AnimationState.Complete -= PlayCeleBrateComplete;
        m_skeletonGraphic.AnimationState.SetAnimation(0,IdleName,true);
    }

    private void Close()
    {
        // PlayerPool.Instance.Free(m_attr.charID);
        PrefabPool.Instance.Free(m_charObjAssetName,m_charObj);
        UIPanelManager.Instance.Hide<GetCharPanel>();
    }
}

