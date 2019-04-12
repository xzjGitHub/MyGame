using System;
using UnityEngine;
using UnityEngine.UI;

namespace Barrack.View
{
    public class SelectWorkType: MonoBehaviour
    {
        private GameObject m_current;
        private GameObject m_select;
        private GameObject m_select1;
        private GameObject m_select2;
        private GameObject m_select3;

        private GameObject m_desObj;
        private GameObject m_costObj;
        private GameObject m_emptyObj;

        private Image m_icon;
        private Text m_level;
        private Text m_des;
        private Text m_num;
        private Text m_manaCost;

        private CharStatus m_status;
        private Action<CharStatus> m_closeAction;

        private string m_gold = "金币产出：";
        private string m_reaearch = "制造研究效率提升：";
        private string m_enchant = "附魔研究效率提升：";


        //BuildingAttribute.Building.GetUnitManaCost(hR_Config)
        public void InitComponent()
        {
            m_icon = transform.Find("Bottom/CharInfo/Icon").GetComponent<Image>();
            m_level = transform.Find("Bottom/CharInfo/Level").GetComponent<Text>();
            m_des = transform.Find("Bottom/Des/Text").GetComponent<Text>();
            m_num = transform.Find("Bottom/Des/Text/Num").GetComponent<Text>();
            m_manaCost = transform.Find("Bottom/ManaCost/Num").GetComponent<Text>();

            m_desObj = transform.Find("Bottom/Des").gameObject;
            m_costObj = transform.Find("Bottom/ManaCost").gameObject;
            m_emptyObj = transform.Find("Bottom/Empty").gameObject;

            Transform btn = transform.Find("WorkType/Gold/Bg");
            m_select = transform.Find("WorkType/Gold/Select").gameObject;
            m_select.SetActive(false);
            Utility.AddButtonListener(btn,() => ClickWorkType(m_select,CharStatus.GoldProduce));

            Transform btn1 = transform.Find("WorkType/Fomo/Bg");
            m_select1 = transform.Find("WorkType/Fomo/Select").gameObject;
            m_select1.SetActive(false);
            Utility.AddButtonListener(btn1,() => ClickWorkType(m_select1,CharStatus.EnchantResearch));

            Transform btn2 = transform.Find("WorkType/Res/Bg");
            m_select2 = transform.Find("WorkType/Res/Select").gameObject;
            m_select2.SetActive(false);
            Utility.AddButtonListener(btn2,() => ClickWorkType(m_select2,CharStatus.EquipResearch));

            Transform btn3 = transform.Find("WorkType/Idle/Bg");
            m_select3 = transform.Find("WorkType/Idle/Select").gameObject;
            m_select3.SetActive(false);
            Utility.AddButtonListener(btn3,() => ClickWorkType(m_select3,CharStatus.Idle));

            Utility.AddButtonListener(transform.Find("Bottom/Sure"),
                () =>
                {
                    gameObject.SetActive(false);
                    if(m_closeAction != null)
                    {
                        m_closeAction(m_status);
                    }
                });
        }

        public void InitInfo(CharStatus status,int level,string iconName,Action<CharStatus> action)
        {
            m_closeAction = action;
            InitCharInfo(level,iconName);
            ClickWorkType(GetObj(status),status);

            float cost = BuildingAttribute.Building.GetUnitManaCost(hR_Config);
            m_manaCost.text = cost.ToString();
        }

        private void InitCharInfo(int charLevel,string headIconName)
        {
            m_level.text = "Lv." + charLevel;
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon,headIconName);
        }

        private void ClickWorkType(GameObject obj,CharStatus status)
        {
            if(m_current != null)
            {
                m_current.SetActive(false);
            }
            m_current = obj;
            m_current.SetActive(true);
            m_status = status;

            UpdateDes();
        }

        private HR_config hR_Config = HR_configConfig.GetHR_Config();
        private void UpdateDes()
        {
            switch(m_status)
            {
                case CharStatus.GoldProduce:
                    int get = ControllerCenter.Instance.BarrackController.GetGold(1);
                    m_des.text = m_gold;
                    m_num.text = get.ToString();
                    break;
                case CharStatus.EquipResearch:
                    m_des.text = m_reaearch;
                    m_num.text = hR_Config.researchBonus * 100+"%";
                    break;
                case CharStatus.EnchantResearch:
                    m_des.text = m_enchant;
                    m_num.text = hR_Config.researchBonus * 100 + "%";
                    break;
            }

            UpdateShow(m_status==CharStatus.Idle);
        }


        private void UpdateShow(bool clickEmpty)
        {
            m_emptyObj.SetActive(clickEmpty);
            m_desObj.SetActive(!clickEmpty);
            m_costObj.SetActive(!clickEmpty);
        }

        private GameObject GetObj(CharStatus status)
        {
            if(status == CharStatus.GoldProduce)
            {
                return m_select;
            }
            if(status == CharStatus.EnchantResearch)
            {
                return m_select1;
            }
            if(status == CharStatus.EquipResearch)
            {
                return m_select2;
            }
            if(status == CharStatus.Idle)
            {
                return m_select3;
            }
            return null;
        }
    }
}
