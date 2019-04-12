using System;
using College.Research.Controller;
using College.Research.Data;
using UnityEngine;
using UnityEngine.UI;

namespace College.Research.View
{
    public class EnchantResTipPanel: MonoBehaviour
    {
        private GameObject m_effObj;
        private GameObject m_detialObj;
        private GameObject m_sureBtn;
        private GameObject m_cancelBtn;

        private Text m_resType;
        private Text m_level;
        private Text m_exp;
        private Text m_num;

        private Action m_sureAction;
        private Action m_cancelAction;

        private EnchantEffList m_effList;
        private ItemTipInfo m_itemInfo;

        private bool m_showEff;

        public void InitComponent()
        {
            m_sureBtn = transform.Find("SureBtn").gameObject;
            m_cancelBtn = transform.Find("CancelBtn").gameObject;

            m_resType = transform.Find("Left/ResType/Type").GetComponent<Text>();
            m_level = transform.Find("Left/ResLevel/Level").GetComponent<Text>();
            m_exp = transform.Find("Left/Exp/Exp").GetComponent<Text>();
            m_num = transform.Find("SureBtn/Num").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("SureBtn/Btn"),ClickSure);
            Utility.AddButtonListener(transform.Find("CancelBtn/Btn"),ClickCancel);
            Utility.AddButtonListener(transform.Find("ShowEffBtn/Btn"),ClickShowEff);

            m_effObj = transform.Find("EffList").gameObject;
            m_effList = Utility.RequireComponent<EnchantEffList>(m_effObj);
            m_effList.InitComponent();
        }


        public void UpdateInfo(ItemData data,
            Action sureAction = null,Action cancelAction = null)
        {
            m_sureAction = sureAction;
            m_cancelAction = cancelAction;

            m_sureBtn.SetActive(sureAction != null);
            m_cancelBtn.SetActive(cancelAction != null);

            MR_template rare = MR_templateConfig.GetTemplate(data.instanceID);
            m_resType.text = EnchanteResearchController.GetName(rare.enchantType);

            m_level.text = (rare.activeEnchantLevel.Count > 0 ? rare.activeEnchantLevel[0] : 0).ToString();
            int currentLevel = (int)ResearchLabSystem.Instance.GetReseachLvel(rare.enchantType);
            int enchantExpReward = 0;
            if(currentLevel <= rare.activeEnchantLevel[0])
            {
                enchantExpReward = rare.enchantExpReward[1];
            }
            else if(currentLevel > rare.activeEnchantLevel[0]
                     && currentLevel < rare.activeEnchantLevel[1])
            {
                enchantExpReward = rare.enchantExpReward[0];
            }
            else
            {
                enchantExpReward = 0;
            }
            m_exp.text = enchantExpReward.ToString();

            if (m_itemInfo == null)
            {
                m_detialObj = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.ItemDetialInfo);
                Utility.SetParent(m_detialObj,transform.Find("Parent"));
                m_itemInfo = Utility.RequireComponent<ItemTipInfo>(m_detialObj);
            }

            m_itemInfo.UpdateInfo(data);
            m_showEff = false;
            UpdateShow();
            UpdateCount();
            m_effList.InitInfo(data.instanceID);
        }

        private void UpdateCount()
        {
            int max = ControllerCenter.Instance.EnchanteResearchController.GetMaxWorkNum();
            int current = ControllerCenter.Instance.EnchanteResearchController.GetCorrentNum();
            m_num.text = string.Format("研究上限:   {0}/{1}",current,max);
        }

        private void ClickSure()
        {
            if(m_sureAction != null)
            {
                m_sureAction();
            }
            gameObject.SetActive(false);
        }

        private void ClickCancel()
        {
            if(m_cancelAction != null)
            {
                m_cancelAction();
            }
            gameObject.SetActive(false);
        }

        private void ClickShowEff()
        {
            m_showEff = !m_showEff;
            UpdateShow();
        }

        private void UpdateShow()
        {
            m_detialObj.SetActive(!m_showEff);
            m_effObj.SetActive(m_showEff);          
        }

        public void Free()
        {
            if (m_effList != null)
                m_effList.Free();
        }
    }
}
