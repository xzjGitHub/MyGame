using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comomon.EquipList
{
    public class EquipList: MonoBehaviour
    {
        private Transform m_parent;

        private Dictionary<int,EquipItem> m_equipList = new Dictionary<int,EquipItem>();

        private Action<ItemAttribute> m_clickCallBack;


        private CoroutineUtil m_coroutine;

        private void OnDisable()
        {
            StopCortine();
        }

        private void StopCortine()
        {
            if(m_coroutine != null)
            {
                if(m_coroutine.Running)
                {
                    m_coroutine.Stop();
                }
            }
        }

        public void InitComponent()
        {
            m_parent = transform.Find("Scroll/Grid");
        }

        public void InitEquipList(List<ItemAttribute> list,Action<ItemAttribute> clickCallBack,Action showEndCallBack=null)
        {
            m_clickCallBack = clickCallBack;
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipItem);

            StopCortine();
            m_coroutine = new CoroutineUtil(InitEquipListInfo(list,showEndCallBack));
        }

        public IEnumerator InitEquipListInfo(List<ItemAttribute> list,Action showEndCallBack = null)
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipItem);

            for(int i = 0; i < list.Count; i++)
            {
                GameObject temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.EquipItem,
                    ItemUtil.EquipPrefab,list[i].itemID.ToString());
                Utility.SetParent(temp,m_parent);

                EquipItem equipItem = Utility.RequireComponent<EquipItem>(temp);
                equipItem.InitInfo(list[i],ClickEquip);

                m_equipList[list[i].itemID] = equipItem;

                if(i > 16)
                    yield return null;
            }

            if(showEndCallBack != null)
                showEndCallBack();
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
            GameObjectPool.Instance.FreeGameObjectByName(StringDefine.ObjectPooItemKey.EquipItem,equipId.ToString());
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
}
