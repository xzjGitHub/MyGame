using System.Collections.Generic;
using College.Research.Data;
using Comomon.ItemList;
using UnityEngine;
using UnityEngine.UI;


namespace College.Research.View
{
    public class EnchanteResearch: MonoBehaviour
    {
        private GameObject m_resTipPanelObj;
        private GameObject m_endTipObj;
        private GameObject m_viewObj;

        private Text m_researchCount;
        private Text m_researchEff;
        private Text m_manaCost;
        private Text m_charNum;

        private NewItemList m_newItemList;
        private EnchanteResearchList m_enchanteResearchList;
        private EnchantResTipPanel m_resTipPanel;
        private EnchantEndTipPanel m_endTip;
        private EnchantViewInfo m_viewInfo;

        private ItemAttribute m_attr;

        private bool m_hasInit;
        private string m_id = string.Empty;

        private void OnEnable()
        {
            ControllerCenter.Instance.EnchanteResearchController.CountDownAction += UpdateSlider;
            ControllerCenter.Instance.EnchanteResearchController.ResearchChange += OnResearchChange;
        }

        private void OnDisable()
        {
            Free();
            ControllerCenter.Instance.EnchanteResearchController.CountDownAction -= UpdateSlider;
            ControllerCenter.Instance.EnchanteResearchController.ResearchChange -= OnResearchChange;
        }

        private void InitComponent()
        {
  
            m_enchanteResearchList = Utility.RequireComponent<EnchanteResearchList>(transform.Find("Left/ResList").gameObject);
            m_enchanteResearchList.InitComponent();

            m_resTipPanelObj = transform.Find("ResTipPanel").gameObject;
            m_resTipPanelObj.SetActive(false);
            m_resTipPanel = Utility.RequireComponent<EnchantResTipPanel>(m_resTipPanelObj);
            m_resTipPanel.InitComponent();

            m_endTipObj = transform.Find("ResEndTipPanel").gameObject;
            m_endTipObj.SetActive(false);
            m_endTip = Utility.RequireComponent<EnchantEndTipPanel>(m_endTipObj);
            m_endTip.InitComponent();

            m_viewObj = transform.Find("ViewInfo").gameObject;
            m_viewObj.SetActive(false);
            m_viewInfo = Utility.RequireComponent<EnchantViewInfo>(m_viewObj);
            m_viewInfo.InitComponent();


            m_researchCount = transform.Find("Left/MaxRes/Num").GetComponent<Text>();
            m_researchCount = transform.Find("Left/MaxRes/Num").GetComponent<Text>();
            m_researchEff = transform.Find("Left/Eff/Num").GetComponent<Text>();
            m_manaCost = transform.Find("Left/Mana/Num").GetComponent<Text>();
            m_charNum = transform.Find("Left/CharNum/Text").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Left/ViewBtn/Btn"),ClickViewBtn);
            Utility.AddButtonListener(transform.Find("Left/CharNum/Image"),ClickChar);

            UpdateCount();
        }

        public void InitInfo()
        {
            if(!m_hasInit)
            {
                InitComponent();
                m_hasInit = true;
            }

            BuildUIController.Instance.LoadItemList(transform.Find("Right/Parent"),out m_newItemList);
            m_newItemList.InitComponent();
            List<ItemAttribute> list = ItemSystem.Instance.GetItemListByItemType(ItemType.XiSu);
            m_newItemList.InitListList(list,ClickItem);
            m_enchanteResearchList.InitList(ResearchLabSystem.Instance.GetResearchingInfoList(),ClickResItem);

            UpdateCount();
            UpdateManaCost();
            UpdateEff();
            UpdateCharNum();
        }


        public void Free()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.RareResearchItem);
            if(m_newItemList != null)
                m_newItemList.Free();
            if(m_resTipPanel != null)
                m_resTipPanel.Free();

            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.NewItemList);

            if(!string.IsNullOrEmpty(m_id))
            {
                m_enchanteResearchList.UpdateSelectShow(m_id,false);
            }
            m_id = string.Empty;
        }

        private void ClickItem(ItemAttribute attr)
        {
            m_attr = attr;
            m_resTipPanelObj.SetActive(true);
            m_resTipPanel.UpdateInfo(attr.GetItemData(),
                () =>
                {
                    bool canResearch = ControllerCenter.Instance.EnchanteResearchController.CanReseach(m_attr);
                    if(canResearch)
                    {
                        ControllerCenter.Instance.EnchanteResearchController.Research(m_attr);
                    }
                });
        }

        private void ClickResItem(string id)
        {
            if(!string.IsNullOrEmpty(m_id))
            {
                m_enchanteResearchList.UpdateSelectShow(m_id,false);
            }
            m_id = id;
            m_enchanteResearchList.UpdateSelectShow(m_id,true);
            ResearchingInfo info = ResearchLabSystem.Instance.GetResearchingInfo(id);
            if(info.HaveUseTime >= info.NeedTime)
            {
                m_endTipObj.SetActive(true);
                m_endTip.UpdateInfo(info);
            }
            else
            {
                m_resTipPanelObj.SetActive(true);
                m_resTipPanel.UpdateInfo(info.Data,() => m_resTipPanelObj.SetActive(false),
                    () =>
                    {
                        ControllerCenter.Instance.EnchanteResearchController.CancelReseach(info);
                    });
            }
        }

        private void UpdateSlider(string id,float allTime,int haveUseTime,int exp)
        {
            m_enchanteResearchList.UpdateSlider(id,allTime,haveUseTime,exp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="status">状态（1：添加 2：时间到了 3：取消 4结束</param>
        private void OnResearchChange(ResearchingInfo info,int status)
        {
            switch(status)
            {
                case 1:
                    OnAddResearch(info);
                    break;
                case 2:
                    m_enchanteResearchList.UpdateWhenResEnd(info.Id);
                    break;
                case 3:
                    m_enchanteResearchList.RemoveResearch(info.Id);
                    OnResearchCancel(info);
                    break;
                case 4:
                    m_enchanteResearchList.RemoveResearch(info.Id);
                    break;
            }
            UpdateCount();
            UpdateManaCost();
        }

        private void OnAddResearch(ResearchingInfo info)
        {
            MR_template rare = MR_templateConfig.GetTemplate(info.Data.instanceID);
            int temp = ItemSystem.Instance.GetItemNum(info.Data.itemID);
            if(temp > 0)
            {
                int remainNum = temp - rare.researchCost;
                if(remainNum == 0)
                {
                    m_newItemList.FreeItem(info.Data.itemID);
                }
                else
                {
                    m_newItemList.UpdateNum(info.Data.itemID,remainNum);
                }
            }
            //添加到列表
            m_enchanteResearchList.AddResearch(info,ClickResItem);
        }


        private void OnResearchCancel(ResearchingInfo info)
        {
            if(m_newItemList.HasIn(info.Data.itemID))
            {
                int nowNum = ItemSystem.Instance.GetItemNum(info.Data.itemID);
                m_newItemList.UpdateNum(info.Data.itemID,nowNum);
            }
            else
            {
                ItemAttribute attr = ItemSystem.Instance.GetItemAttributeByTemplateId(info.Data.instanceID);
                m_newItemList.AddItem(attr);
            }
        }

        private void UpdateCount()
        {
            int max = ControllerCenter.Instance.EnchanteResearchController.GetMaxWorkNum();
            int current = ControllerCenter.Instance.EnchanteResearchController.GetCorrentNum();
            m_researchCount.text = current + "/" + max;
        }

        private void UpdateManaCost()
        {
            float singleCost = ControllerCenter.Instance.EnchanteResearchController.GetManaCost();
            int trainNum = ControllerCenter.Instance.EnchanteResearchController.GetCorrentNum(); ;
            m_manaCost.text = ((int)singleCost * trainNum).ToString();
        }

        private void UpdateEff()
        {
            //1 + ERChar * hr_config. researchBonus 
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            int num = Barrack.Data.BarrackSystem.Instance.GetNowUserTypeCharCount(CharStatus.EnchantResearch);
            float eff = 1 + num * hR_Config.researchBonus;
            m_researchEff.text = (eff * 100).ToString();
        }

        private void UpdateCharNum()
        {
            int num = Barrack.Data.BarrackSystem.Instance.GetNowUserTypeCharCount(CharStatus.EnchantResearch);
            m_charNum.text = num.ToString();
        }


        private void ClickViewBtn()
        {
            m_viewObj.SetActive(true);
            m_viewInfo.UpdateInfo();
        }

        private void ClickChar()
        {
            return;
          //  GameEventrCenter.Instance.EmitClickBuilidFucEvent(false);
           // GameEventrCenter.Instance.EmitUpdateRoomNameEvent(false,"");
          //  BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Barracks);
        }
    }
}
