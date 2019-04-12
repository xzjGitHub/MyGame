using System;
using Char.View;
using Comomon.ItemList;
using UnityEngine;

namespace College.Enchant.View
{
    public class SelectItemPanel: MonoBehaviour
    {
        private GameObject m_equipObj;
        private GameObject m_itemObj;

        private GameObject m_sureBtn;

        private EquipListViewPanel m_equipList;
        private NewItemList m_itemList;
        private SelectLeftInfo m_leftInfo;

        private Action<ItemAttribute,bool> m_closeAction;

        private ItemAttribute m_attr;
        private bool m_isEquip;

        private void OnDisable()
        {
            //m_attr = null;
            //m_leftInfo.UpdateInfo(m_attr);
            //m_sureBtn.SetActive(m_attr != null);
        }

        public void InitComponent()
        {
            m_equipObj = transform.Find("EquipListViewPanel ").gameObject;
            m_itemObj = transform.Find("ItemListView").gameObject;

            m_leftInfo = Utility.RequireComponent<SelectLeftInfo>(transform.Find("Left").gameObject);
            m_leftInfo.InitComponent();

            m_equipList = Utility.RequireComponent<EquipListViewPanel>(m_equipObj);
            m_equipList.InitComponent(ClickItem,ShowEndCallBack);

            m_itemList = Utility.RequireComponent<NewItemList>(transform.Find("ItemListView/ListParent/NewItemList").gameObject);
            m_itemList.InitComponent();

            m_sureBtn = transform.Find("SureBtn").gameObject;
            m_sureBtn.SetActive(false);

            Utility.AddButtonListener(transform.Find("SureBtn/Btn"),ClickSure);
            Utility.AddButtonListener(transform.Find("Back/Btn"),ClickSure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attr">当前的</param>
        /// <param name="isEquip">是否点击的装备</param>
        /// <param name="closeAction">关闭回掉</param>
        public void UpdateInfo(ItemAttribute attr,bool isEquip,
            Action<ItemAttribute,bool> closeAction)
        {

            m_attr = attr;
            m_isEquip = isEquip;
            m_closeAction = closeAction;

            m_equipObj.SetActive(isEquip);
            m_itemObj.SetActive(!isEquip);

            m_equipList.m_currenType = EquipPanelType.None;

            if(isEquip)
            {
                m_equipList.ClickTag((int)EquipPanelType.All);
            }
            else
            {
                m_itemList.InitListList(ItemSystem.Instance.GetItemListByItemType(ItemType.XiSu),ClickItem,ShowEndCallBack);
            }

            m_leftInfo.UpdateInfo(m_attr);
            m_sureBtn.SetActive(m_attr != null);
        }

        private void ClickItem(ItemAttribute attr)
        {
            if(m_attr != null)
            {
                if(m_attr.itemID == attr.itemID)
                {
                    UpdateInfo(attr.itemID,false);
                    m_attr = null;
                }
                else
                {
                    UpdateInfo(m_attr.itemID,false);
                    m_attr = attr;
                    UpdateInfo(m_attr.itemID,true);
                }
            }
            else
            {
                m_attr = attr;
                UpdateInfo(m_attr.itemID,true);
            }
            m_leftInfo.UpdateInfo(m_attr);
            m_sureBtn.SetActive(m_attr != null);
        }

        private void UpdateInfo(int id,bool show)
        {
            if(m_isEquip)
            {
                m_equipList.UpdateSelectShow(id,show);
            }
            else
            {
                m_itemList.UpdateSelectShow(id,show);
            }
        }

        private void ShowEndCallBack()
        {
            if(m_attr != null)
            {
                UpdateInfo(m_attr.itemID,true);
            }
           // m_leftInfo.UpdateInfo(m_attr);
        }

        public void ClickSure()
        {
            gameObject.SetActive(false);
            if(m_closeAction != null)
            {
                m_closeAction(m_attr,m_isEquip);
            }
        }


        public void Free()
        {
            if(m_equipList != null)
                m_equipList.Free();
            if(m_itemList != null)
                m_itemList.Free();
            m_leftInfo.Free();

            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.NewItemList);
        }
    }
}
