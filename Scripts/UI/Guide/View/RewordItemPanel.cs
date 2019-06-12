
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/30/2019
//Note:     
//--------------------------------------------------------------

using Comomon.ItemList;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RewordItemPanel : UIPanelBehaviour
{
    public Action CloseCallBack;
    private GameObject m_prefab;
    private Transform m_parent;

    protected override void OnShow(List<object> parmers = null)
    {
        base.OnShow(parmers);
        InitComponent();
    }

    protected override void OnHide()
    {
        base.OnHide();
        PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.CoreGuideRewardItem,true);
    }

    private void InitComponent()
    {
        m_prefab = transform.Find("Parent/Item").gameObject;
        m_prefab.SetActive(false);
        m_parent = transform.Find("Parent");
        Utility.AddButtonListener(transform.Find("Btn"),Close);
    }

    public void Init(int id)
    {
        Dialog_template dia = Dialog_templateConfig.GetDialog_template(id);
        List<int> idList = new List<int>();
        List<float> levelList = new List<float>();
        for(int i = 0; i < dia.itemReward.Count; i++)
        {
            idList.Add(dia.itemReward[i][0]);
            levelList.Add(dia.itemReward[i][1]);
        }

        if(idList.Count == 0 || levelList.Count == 0)
            return;

        PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.CoreGuideRewardItem);

        ItemRewardInfo itemRewardInfo = new ItemRewardInfo(levelList,idList);
        List<ItemData> list = ItemSystem.Instance.Itemrewards_ItemDate(itemRewardInfo);

        for(int i = 0; i < list.Count; i++)
        {
            GameObject obj = PrefabPool.Instance.GetObjSync(StringDefine.ObjectPooItemKey.CoreGuideRewardItem,m_prefab);
            Utility.SetParent(obj,m_parent);
            NewItem item = Utility.RequireComponent<NewItem>(obj);
            item.InitInfo(new ItemAttribute(list[i]));
        }
    }

    private void Close()
    {
        if(CloseCallBack != null)
            CloseCallBack();
        UIPanelManager.Instance.Hide<RewordItemPanel>();
    }
}
