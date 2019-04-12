using System.Collections.Generic;
using Comomon.ItemList;
using Core.Controller;
using Core.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class CoreResearchPanel: MonoBehaviour
    {
        private GameObject m_scrollBar;

        private Text m_level;
        private Text m_power;
        private Text m_manaCC;
        private Image m_nowImage;
        private Image m_addImage;

        private bool m_isAll;

        private PowerList m_powerList;
        private NewItemList m_newItemList;

        private List<ItemAttribute> m_selectList = new List<ItemAttribute>();
        private float m_all;

        private SliderPos m_add;
        private SliderPos m_now;

        public void InitComponent()
        {
            m_scrollBar = transform.Find("Left/ScrollBar").gameObject;
            m_scrollBar.SetActive(false);

            m_level = transform.Find("Left/Level/Level").GetComponent<Text>();
            m_manaCC = transform.Find("Left/ManaCC/Get").GetComponent<Text>();
            m_power = transform.Find("Left/Slider/Text").GetComponent<Text>();
            m_nowImage = transform.Find("Left/Slider/Now").GetComponent<Image>();
            m_addImage = transform.Find("Left/Slider/Add").GetComponent<Image>();

            m_add = transform.Find("Left/Slider/AddMask/Eff").GetComponent<SliderPos>();
            m_now = transform.Find("Left/Slider/NowMask/Eff").GetComponent<SliderPos>();

            Utility.AddButtonListener(transform.Find("Bottom/CNBtn"),ClickCN);
            Utility.AddButtonListener(transform.Find("Bottom/AllBtn"),ClickAll);
            Utility.AddButtonListener(transform.Find("Left/View"),() => CoreEventCenter.Instance.EmitShowCoreInfo());

            m_powerList = Utility.RequireComponent<PowerList>(transform.Find("Left/Scroll").gameObject);
            m_powerList.InitComponent();

            m_all = ControllerCenter.Instance.CoreController.GetMaxPower();
        }

        public void InitInfo()
        {
            BuildUIController.Instance.LoadItemList(transform.Find("Right/Parent"),out m_newItemList);
            m_newItemList.InitComponent();
            List<ItemAttribute> list = ItemSystem.Instance.GetItemListByItemType(ItemType.MoJing);
            m_newItemList.InitListList(list,ClickItem);

            UpdateNowImage();
            UpdateAddImage();
            UpdateLevel();
            UpdateManaCC();

            m_scrollBar.SetActive(m_selectList.Count > 0);
        }


        private void ClickItem(ItemAttribute itemAttribute)
        {
            if(m_selectList.Contains(itemAttribute))
            {
                m_selectList.Remove(itemAttribute);
                m_powerList.RemoveItem(itemAttribute.itemID);
                m_newItemList.UpdateSelectShow(itemAttribute.itemID,false);
            }
            else
            {
                m_selectList.Add(itemAttribute);
                m_powerList.AddItem(itemAttribute);
                m_newItemList.UpdateSelectShow(itemAttribute.itemID,true);
            }

            m_scrollBar.SetActive(m_selectList.Count > 0);
            UpdateAddImage();
        }


        public void Free()
        {
            if(m_newItemList != null)
                m_newItemList.Free();
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.CorePowerItem);
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.NewItemList);
            ClearInfo();
        }

        private void ClickCN()
        {
            if(m_selectList.Count == 0)
            {
                return;
            }
            Research();
        }

        private void ClickAll()
        {
            m_isAll = !m_isAll;

            for(int i = 0; i < m_selectList.Count; i++)
            {
                m_powerList.RemoveItem(m_selectList[i].itemID);
            }
            m_selectList.Clear();

            if(m_isAll)
            {
                m_selectList = ItemSystem.Instance.GetItemListByItemType(ItemType.MoJing);
                for(int i = 0; i < m_selectList.Count; i++)
                {
                    m_powerList.AddItem(m_selectList[i]);
                }
            }
            m_newItemList.UpdateSelectShow(m_isAll);

            UpdateAddImage();
            m_scrollBar.SetActive(m_selectList.Count > 0);
        }

        private void Research()
        {
          //  ControllerCenter.Instance.CoreController.Research(m_selectList);

            for(int i = 0; i < m_selectList.Count; i++)
            {
                m_powerList.RemoveItem(m_selectList[i].itemID);
                int remianNum = ItemSystem.Instance.GetItemNum(m_selectList[i].itemID);
                if(remianNum == 0)
                {
                    m_newItemList.FreeItem(m_selectList[i].itemID);
                }
                else
                {
                    m_newItemList.UpdateNum(m_selectList[i].itemID,remianNum);
                }
            }

            ClearInfo();
            UpdateNowImage();
            UpdateAddImage();
            UpdateLevel();
            UpdateManaCC();
            m_scrollBar.SetActive(m_selectList.Count > 0);
        }

        private void ClearInfo()
        {
            m_selectList.Clear();
            m_isAll = false;
        }


        private void UpdateAddImage()
        {
            float value = 0;
            if(m_selectList.Count > 0)
            {
                float now = ControllerCenter.Instance.CoreController.GetNowPower();
                //float add = ControllerCenter.Instance.CoreController.GetAddPower(m_selectList);
                //value = (now + add) / m_all;
            }

            m_addImage.fillAmount = value;
            m_add.UpdatePos();
            UpdatePowerText();
        }

        private void UpdateNowImage()
        {
            float now = ControllerCenter.Instance.CoreController.GetNowPower();
            float value = now / m_all;
            m_nowImage.fillAmount = value;
            m_now.UpdatePos();
        }

        private void UpdateLevel()
        {
            int level = CoreSystem.Instance.GetLevel();
            m_level.text = level.ToString();
        }

        private void UpdateManaCC()
        {
            float currentPower = CoreSystem.Instance.GetPower();
            float cc = BuildingAttribute.Building.GetbaseRewardValue(
                Building_templateConfig.GetBuildingTemplate((int)BuildingType.Core),currentPower);
            m_manaCC.text = "+" + cc;
        }

        private string m_s1 = "功率:<color=#FAFAD2FF>{0}</color>";
        private string m_s2 = string.Empty;
        private string m_s3 = "/<color=#FAFAD2FF>{0}</color>";

        private void UpdatePowerText()
        {
            float now = ControllerCenter.Instance.CoreController.GetNowPower();
            //float add = m_selectList.Count > 0 ?
            //        ControllerCenter.Instance.CoreController.GetAddPower(m_selectList)
            //        :
            //        0;
            float add = 0;
  
            m_s1 = string.Format(m_s1,(int)(now * 100) / 100f);
            if(add != 0)
            {
                m_s2 = "<color=#17CAE3FF>(+" + (int)(add * 100) / 100f + ")</color>";
            }
            m_s3 = string.Format(m_s3,(int)(m_all * 100) / 100f);
            m_power.text = m_s1 + m_s2 + m_s3;
        }
    }
}
