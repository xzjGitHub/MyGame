using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Comomon.ItemList
{
    public class NewItemList: MonoBehaviour
    {
        private Transform m_parent;

        private Action<ItemAttribute> m_clickCallBack;

        private Dictionary<int,NewItem> m_dict = new Dictionary<int,NewItem>();

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

        public void InitListList(List<ItemAttribute> list,Action<ItemAttribute> clickCallBack,Action showEndCallBack=null)
        {
            m_clickCallBack = clickCallBack;
            m_dict.Clear();

            Free();
            StopCortine();
            m_coroutine = new CoroutineUtil(InitListInfo(list,showEndCallBack));
        }

        private IEnumerator InitListInfo(List<ItemAttribute> list,Action showEndCallBack = null)
        {
            for(int i = 0; i < list.Count; i++)
            {
                NewItem newItem = AddItem(list[i]);
                m_dict[list[i].itemID] = newItem;

                if (i > 16)
                {
                    yield return null;
                }
            }

            if(showEndCallBack != null)
                showEndCallBack();
        }


        public NewItem AddItem(ItemAttribute attr)
        {
            GameObject temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.NewItem,
                ItemUtil.Item,attr.itemID.ToString());

            temp.name = attr.instanceID.ToString();

            Utility.SetParent(temp,m_parent);

            NewItem newItem = Utility.RequireComponent<NewItem>(temp);
            newItem.InitInfo(attr,ClickItem);


            return newItem;
        }

        private void ClickItem(ItemAttribute attr)
        {
            if(m_clickCallBack != null)
            {
                m_clickCallBack(attr);
            }
        }

        public void ClickFirst()
        {
            List<int> keys = new List<int>();
            keys.AddRange(m_dict.Keys);
            if(keys.Count > 0)
            {
                m_dict[keys[0]].Click();
            }
        }

        public void FreeItem(int itemId)
        {
            GameObjectPool.Instance.FreeGameObjectByName(StringDefine.ObjectPooItemKey.NewItem,itemId.ToString());
            m_dict.Remove(itemId);
        }

        public void UpdateNum(int itemId,int num)
        {
            if(m_dict.ContainsKey(itemId))
                m_dict[itemId].UpdateNum(num);
        }

        public bool HasIn(int itemId)
        {
            return m_dict.ContainsKey(itemId);
        }

        public void UpdateSelectShow(int itemId,bool show)
        {
            if(m_dict.ContainsKey(itemId))
                m_dict[itemId].UpdateSelectShow(show);
        }

        public void UpdateSelectShow(bool show)
        {
            List<int> keys = new List<int>();
            keys.AddRange(m_dict.Keys);

            for(int i = 0; i < keys.Count; i++)
            {
                UpdateSelectShow(keys[i],show);
            }
        }

        public void Free()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.NewItem);
        }
    }
}
