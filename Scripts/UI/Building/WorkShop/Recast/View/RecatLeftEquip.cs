﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorkShop.Recast.View
{
    public class RecatLeftEquip: MonoBehaviour
    {
        private GameObject m_title;
        private GameObject m_prefab;
        private Transform m_parent;

        private Dictionary<int,RecastLeftEquipItem> m_dict=new Dictionary<int, RecastLeftEquipItem>();

        public void InitComponent()
        {
            m_prefab = transform.Find("EquipList/Grid/Equip").gameObject;
            m_prefab.SetActive(false);
            m_title = transform.Find("Title").gameObject;
            m_title.SetActive(false);
            m_parent = transform.Find("EquipList/Grid");
        }

        public void UpdateInfo(int itemInstanceId,Action<EquipmentData,int> click)
        {
            Free();
            m_dict.Clear();
            Item_instance item = Item_instanceConfig.GetItemInstance(itemInstanceId);
            for(int i = 0; i < item.craftList.Count; i++)
            {
                GameObject obj = GameObjectPool.Instance.GetObject(
                      StringDefine.ObjectPooItemKey.EquipRecastEquip,m_prefab);
                Utility.SetParent(obj,m_parent);

                Craft_template craft = Craft_templateConfig.GetCraft_template(item.craftList[i]);
                EquipmentData data = ControllerCenter.Instance.
                    EquipRecastController.GetEquipmentData(craft.instanceID);
                RecastLeftEquipItem info = Utility.RequireComponent<RecastLeftEquipItem>(obj);
                int craftId = item.craftList[i];
                info.InitInfo(craftId,data,click);
                m_dict.Add(data.itemID,info);
            }
            m_title.SetActive(true);
        }

        public void UpdateSelect(int itemId, bool show)
        {
            if (m_dict.ContainsKey(itemId))
            {
                m_dict[itemId].UpdateSelectShow(show);
            }
        }

        public void Free()
        {
            m_title.SetActive(false);
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipRecastEquip);
        }
    }
}
