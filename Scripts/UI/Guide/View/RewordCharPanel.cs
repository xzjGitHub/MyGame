
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/30/2019
//Note:     
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class RewordCharPanel : UIPanelBehaviour
{
    public Action CloseCallBack;
    private GameObject m_prefab;
    private Transform m_parent;

    protected override void OnShow(List<object> parmers = null)
    {
        base.OnShow(parmers);
        InitComponent();
    }

    private void InitComponent()
    {
        m_prefab = transform.Find("Parent/Char").gameObject;
        m_prefab.SetActive(false);
        m_parent = transform.Find("Parent");
        Utility.AddButtonListener(transform.Find("Btn"),Close);
    }

    protected override void OnHide()
    {
        base.OnHide();
        PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.CoreGuideRewardChar,true);
    }

    public void Init(int id)
    {
        PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.CoreGuideRewardChar);

        Dialog_template dia = Dialog_templateConfig.GetDialog_template(id);
        for(int i = 0; i < dia.itemReward.Count; i++)
        {
            GameObject obj = PrefabPool.Instance.GetObjSync(
              StringDefine.ObjectPooItemKey.CoreGuideRewardChar,m_prefab);
            Utility.SetParent(obj,m_parent);
            CharCreate charCreate = new CharCreate(dia.charReward[i][0]);
            charCreate.charLevel = dia.charReward[i][1];
            CharAttribute attr = CharSystem.Instance.CreateChar(charCreate);
            obj.transform.Find("").GetComponent<Text>().text = "Lv." + attr.charLevel;
            obj.transform.Find("").GetComponent<Image>().sprite=
              ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon,attr.char_template.HeadIcon);
        }
    }

    private void Close()
    {
        if(CloseCallBack != null)
            CloseCallBack();
        UIPanelManager.Instance.Hide<RewordCharPanel>();
    }
}
