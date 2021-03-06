﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorkShop.EquipMake.View
{
    public class EquipMake: MonoBehaviour
    {
        private GameObject m_select;
        private GameObject m_equipDetialObj;
        private GameObject m_cost;
        private GameObject m_bottom;
        private GameObject m_zcGray;
        private GameObject m_fcGray;

        private Text m_goldCost;
        private Text m_manaCost;

        private MakeTypeInfo m_makeTypeInfo;
        private ZcInfo m_zcInfo;
        private FcInfo m_fcInfo;
        private EquipDetailInfo m_detialInfo;

        private MakeType m_makeType = MakeType.Wuqi;
        private int m_forgeId = -1;
        private ItemAttribute m_zcAttr;
        private ItemAttribute m_fcAttr;
        private EquipAttribute m_equip;

        private bool m_hasInit;

        private void InitComponent()
        {
            m_cost = transform.Find("Cost").gameObject;
            m_cost.SetActive(false);
            m_bottom = transform.Find("Bottom").gameObject;
            m_bottom.SetActive(false);
            m_zcGray = transform.Find("LeftInfo/Zc/Btn/Gray").gameObject;
            m_zcGray.SetActive(true);
            m_fcGray = transform.Find("LeftInfo/Fc/Btn/Gray").gameObject;
            m_fcGray.SetActive(true);

            m_goldCost = transform.Find("Cost/GoldCost/Num").GetComponent<Text>();
            m_manaCost = transform.Find("Cost/ManaCost/Num").GetComponent<Text>();

            m_makeTypeInfo = Utility.RequireComponent<MakeTypeInfo>(transform.Find("LeftInfo/Type").gameObject);
            m_makeTypeInfo.InitComponent(ClickMakeType);

            m_zcInfo = Utility.RequireComponent<ZcInfo>(transform.Find("LeftInfo/Zc").gameObject);
            m_zcInfo.InitComponent(ClickZcInfo);

            m_fcInfo = Utility.RequireComponent<FcInfo>(transform.Find("LeftInfo/Fc").gameObject);
            m_fcInfo.InitComponent(ClickFcInfo);

            //m_equipDetialObj = PrefabPool.Instance.GetObjSync(StringDefine.PrefabNameDefine.EquipDetialInfo,Res.AssetType.Prefab);
            //Utility.SetParent(m_equipDetialObj,transform.Find("DetialInfo"));
            //m_detialInfo = Utility.RequireComponent<EquipDetailInfo>(m_equipDetialObj);
            //m_equipDetialObj.SetActive(false);

            Utility.AddButtonListener(transform.Find("Bottom/CallBtn/Btn"),ClickMake);
        }

        private void OnDisable()
        {
            Reset();
        }


        private void UpdateSlect(GameObject obj)
        {
            if(m_select != null)
            {
                m_select.SetActive(false);
            }
            m_select = obj;
            m_select.SetActive(true);
        }

        public void InitInfo()
        {
            if(!m_hasInit)
            {
                InitComponent();
                m_hasInit = true;
            }
            if(m_equipDetialObj == null)
            {
                m_equipDetialObj = PrefabPool.Instance.GetObjSync(StringDefine.PrefabNameDefine.EquipDetialInfo,Res.AssetType.Prefab);
                Utility.SetParent(m_equipDetialObj,transform.Find("DetialInfo"));
                m_detialInfo = Utility.RequireComponent<EquipDetailInfo>(m_equipDetialObj);
                m_equipDetialObj.SetActive(false);
            }

            m_makeTypeInfo.UpdateInfo(-1);
            m_zcInfo.UpdateInfo(null,0);
            m_fcInfo.UpdateInfo(null,0);
        }

        private void ClickMakeType(GameObject select)
        {
            SelectEquipMakeTypePanel panel = UIPanelManager.Instance.Show<SelectEquipMakeTypePanel>();
            if(m_forgeId == -1)
                m_forgeId = 1001;
            panel.UpdateInfo(m_makeType,m_forgeId,OnMakeTypeAndForgeChange);
            UpdateSlect(select);
        }

        private void ClickZcInfo(GameObject select)
        {
            if(m_forgeId == -1)
                return;
            SelectMatPanel panel = UIPanelManager.Instance.Show<SelectMatPanel>();
            panel.InitComponent(OnZcChane);
            // panel.UpdateInfo(m_zcAttr,ItemType.ZhuCai,ItemSystem.Instance.GetItemListByItemType(ItemType.ZhuCai));
            panel.UpdateInfo(m_zcAttr,ItemType.ZhuCai,ItemSystem.Instance.GetEquipMakeZc(ItemType.ZhuCai,m_forgeId));
            UpdateSlect(select);
        }

        private void ClickFcInfo(GameObject select)
        {
            if(m_zcAttr == null)
                return;

            SelectMatPanel panel = UIPanelManager.Instance.Show<SelectMatPanel>();
            panel.InitComponent(OnFcChane);
            //panel.UpdateInfo(m_fcAttr,ItemType.FuCai,ItemSystem.Instance.GetItemListByItemType(ItemType.FuCai));
            panel.UpdateInfo(m_fcAttr,ItemType.FuCai,ItemSystem.Instance.GetEquipMakeFc(ItemType.FuCai,m_forgeId));

            UpdateSlect(select);
        }

        private void OnMakeTypeAndForgeChange(MakeType makeType,int forgeId)
        {
            if(makeType != m_makeType || m_forgeId != forgeId)
            {
                m_zcAttr = null;
                m_fcAttr = null;
                m_equip = null;
            }

            m_makeType = makeType;
            m_forgeId = forgeId;

            m_makeTypeInfo.UpdateInfo(forgeId);
            m_zcInfo.UpdateInfo(m_zcAttr,m_forgeId);
            m_fcInfo.UpdateInfo(null,0);

            m_zcGray.SetActive(m_forgeId == -1);
            m_fcGray.SetActive(m_zcAttr == null);
            m_equipDetialObj.SetActive(m_equip != null);
        }

        private void OnZcChane(ItemAttribute attr)
        {
            m_zcAttr = attr;
            m_fcAttr = null;
            m_zcInfo.UpdateInfo(attr,m_forgeId);
            m_fcInfo.UpdateInfo(m_fcAttr,m_forgeId);

            m_makeTypeInfo.UpdateLevel((int)m_makeType + 1,m_zcAttr == null ? 0 : m_zcAttr.instanceID);

            if(attr != null)
            {
                m_equip = ControllerCenter.Instance.EquipMakeController.CreatEquip(
                   (int)m_makeType + 1,m_forgeId,m_zcAttr,m_fcAttr);
                m_detialInfo.Free();
                m_detialInfo.InitInfo(m_equip);
            }

            m_fcGray.SetActive(m_zcAttr == null);
            m_equipDetialObj.SetActive(m_equip != null);
            m_cost.SetActive(m_zcAttr != null);
            m_bottom.SetActive(m_zcAttr != null);

            if(m_zcAttr != null)
            {
                UpdateCost();
            }
        }


        private void OnFcChane(ItemAttribute attr)
        {
            m_fcAttr = attr;
            m_fcInfo.UpdateInfo(m_fcAttr,m_forgeId);

            List<ItemData> newList = new List<ItemData>();
            newList.Add(m_zcAttr.GetItemData());
            if(m_fcAttr != null)
                newList.Add(m_fcAttr.GetItemData());
            m_equip = ControllerCenter.Instance.EquipMakeController.CreatEquip(
               (int)m_makeType + 1,m_forgeId,m_zcAttr,m_fcAttr);
            m_detialInfo.Free();
            m_detialInfo.InitInfo(m_equip);
            m_equipDetialObj.SetActive(true);

        }

        private void UpdateCost()
        {
            Forge_template forgeTem = ControllerCenter.
                Instance.EquipMakeController.GetForge_Template(m_forgeId);
            m_goldCost.text = forgeTem.goldCost.ToString();
            m_manaCost.text = forgeTem.manaCost.ToString();
        }

        private void ClickMake()
        {
            List<ItemData> newList = new List<ItemData>();
            newList.Add(m_zcAttr.GetItemData());
            if(m_fcAttr != null)
                newList.Add(m_fcAttr.GetItemData());

            if(ControllerCenter.Instance.EquipMakeController.CanMake(m_forgeId,m_zcAttr,m_fcAttr))
            {
                ControllerCenter.Instance.EquipMakeController.MakeEquip(m_forgeId,m_zcAttr,m_fcAttr,m_equip);
                Reset();
            }
        }

        private void Reset()
        {
            m_forgeId = -1;
            m_makeType = MakeType.Wuqi;
            m_zcAttr = null;
            m_equip = null;
            m_fcAttr = null;

            m_makeTypeInfo.UpdateInfo(m_forgeId);
            m_zcInfo.UpdateInfo(m_zcAttr,(int)m_makeType + 1);
            m_fcInfo.UpdateInfo(m_fcAttr,(int)m_makeType + 1);
            m_makeTypeInfo.UpdateLevel((int)m_makeType + 1,m_zcAttr == null ? 0 : m_zcAttr.instanceID);

            m_cost.SetActive(false);
            m_equipDetialObj.SetActive(false);
            m_bottom.SetActive(false);
            m_zcGray.SetActive(true);
            m_fcGray.SetActive(true);
            if(m_select != null)
            {
                m_select.SetActive(false);
            }
        }

        public void Free()
        {
            SelectEquipMakeTypePanel typePanel = UIPanelManager.Instance.GetUiPanelBehaviour<SelectEquipMakeTypePanel>();
            if(typePanel != null)
                typePanel.Close();
            if(m_equipDetialObj != null)
            {
                PrefabPool.Instance.Free(StringDefine.PrefabNameDefine.EquipDetialInfo,m_equipDetialObj);
                m_equipDetialObj = null;
            }
        }
    }
}
