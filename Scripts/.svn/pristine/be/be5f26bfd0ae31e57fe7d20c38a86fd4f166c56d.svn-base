using Comomon.EquipList;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEquipList: MonoBehaviour
{
    private Transform m_parent;

    private LoopScroller m_loopScroller;
    private Dictionary<int,EquipItem> m_equipList = new Dictionary<int,EquipItem>();
    private List<ItemAttribute> m_list = new List<ItemAttribute>();

    private Action<ItemAttribute> m_clickCallBack;

    public void InitComponent()
    {
        m_loopScroller = transform.Find("Scroll").GetComponent<LoopScroller>();
        m_loopScroller.InitInfo(ItemChangeAction,CreateItemAction,ItemDestroyAction,
            null,null);
        m_parent = transform.Find("Scroll/Grid");
    }

    private GameObject CreateItemAction(int index)
    {
        GameObject temp = null;
        ItemAttribute attr = m_list[index];
        EquipAttribute equip = attr as EquipAttribute;
        temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.EquipItem,
             ItemUtil.EquipPrefab);
        Utility.SetParent(temp,m_parent);
        Utility.RequireComponent<EquipItem>(temp);
        return temp;
    }

    private void ItemDestroyAction(int index,GameObject obj) { }

    private void ItemChangeAction(int index,GameObject obj)
    {
        ItemAttribute attr = m_list[index];
        EquipAttribute equip = attr as EquipAttribute;

        EquipItem equipItem = obj.GetComponent<EquipItem>();
        equipItem.InitInfo(equip,ClickEquip);

        if(!m_equipList.ContainsKey(equip.itemID))
            m_equipList.Add(equip.itemID,equipItem);
    }

    public void InitEquipList(List<ItemAttribute> list,Action<ItemAttribute> clickCallBack,Action showEndCallBack = null)
    {
        GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipItem);
        m_clickCallBack = clickCallBack;

        m_equipList.Clear();
        m_list.Clear();
        m_list.AddRange(list);
        Debug.Log(list.Count);
        m_loopScroller.UpdateInfo(list.Count);
    }


    private void ClickEquip(ItemAttribute attr)
    {
        if(m_clickCallBack != null)
        {
            m_clickCallBack(attr);
        }
    }

    public void UpdateEuipStatus(int equipId,EquipState equipState)
    {
        if(m_equipList.ContainsKey(equipId))
            m_equipList[equipId].UpdateStatusShow(equipState);
    }

    public void UpdateBelongCharInfo(int equipId)
    {
        if(m_equipList.ContainsKey(equipId))
            m_equipList[equipId].UpdateCharInfo();
    }

    public void FreeObj(int equipId)
    {
        if(m_equipList.ContainsKey(equipId)) {
            GameObjectPool.Instance.FreeGameObjectByName(StringDefine.ObjectPooItemKey.EquipItem,equipId.ToString());
            m_loopScroller.DelItem(m_equipList[equipId].GetComponent<LoopScrollItem>().Index);
        }
    }

    public void FreePool()
    {
        GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipItem);
    }

    public void UpdateSelectShow(int equipId,bool show)
    {
        if(m_equipList.ContainsKey(equipId))
            m_equipList[equipId].UpdateSelectShow(show);
    }
}

