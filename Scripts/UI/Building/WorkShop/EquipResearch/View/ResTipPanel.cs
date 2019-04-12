using System;
using UnityEngine;
using UnityEngine.UI;
using WorkShop.EquipResearch.Controller;

namespace WorkShop.EquipResearch.View
{
    public class ResTipPanel: MonoBehaviour
    {
        private GameObject m_sureBtn;
        private GameObject m_cancelBtn;

        private Text m_resType;
        private Text m_level;
        private Text m_exp;
        private Text m_num;

        private EquipDetailInfo m_detialInfo;

        private Action m_sureAction;
        private Action m_cancelAction;

    
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
            Utility.AddButtonListener(transform.Find("Back/Btn"),()=>gameObject.SetActive(false));
        }


        public void UpdateInfo(int equipId,
            Action sureAction=null,Action cancelAction=null)
        {
            m_sureAction = sureAction;
            m_cancelAction = cancelAction;

            m_sureBtn.SetActive(sureAction!=null);
            m_cancelBtn.SetActive(cancelAction!=null);

            if (m_detialInfo == null)
            {
                GameObject prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo);
                Utility.SetParent(prefab,transform.Find("Right/Parent"));
                m_detialInfo = Utility.RequireComponent<EquipDetailInfo>(prefab);
            }
            EquipAttribute attr = ItemSystem.Instance.GetEquipAttribute(equipId);
            m_detialInfo.Free();
            m_detialInfo.InitInfo(attr);

            Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
            Equip_template eq = Equip_templateConfig.GetEquip_template(attr.instanceID);

            ER_template eR_Template = ER_templateConfig.GetER_template(item.ERTemplate);

            m_resType.text = EquipResearchController.GetName(eq.REType);

            m_level.text = (eR_Template.activeRELevel.Count>0?eR_Template.activeRELevel[0]:0).ToString();
            int currentLevel = WorkshopSystem.Instance.GetResearchLevel(eq.REType);
            int REExpReward = 0;
            if(eR_Template.activeRELevel.Count >= 2 && eR_Template.REExpReward.Count >= 2)
            {
                if(currentLevel <= eR_Template.activeRELevel[0])
                {
                    REExpReward = eR_Template.REExpReward[1];
                }
                if(currentLevel > eR_Template.activeRELevel[0] && currentLevel <= eR_Template.activeRELevel[1])
                {
                    REExpReward = eR_Template.REExpReward[0];
                }
            }
            //athf.FloorToInt(exp)
            m_exp.text = Mathf.FloorToInt(REExpReward).ToString();
            UpdateCount();
        }

        private void UpdateCount()
        {
            int max = ControllerCenter.Instance.EquipResearchController.GetMaxWorkNum();
            int current = ControllerCenter.Instance.EquipResearchController.GetCorrentNum();
            m_num.text = string.Format("研究上限:   {0}/{1}",current,max);
        }

        private void ClickSure()
        {
            if (m_sureAction != null)
            {
                m_sureAction();
                gameObject.SetActive(false);
            }
        }

        private void ClickCancel()
        {
            if(m_cancelAction != null)
            {
                m_cancelAction();
                gameObject.SetActive(false);
            }
        }
    }
}
