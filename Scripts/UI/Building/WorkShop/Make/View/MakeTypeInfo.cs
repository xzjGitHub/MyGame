using System;
using UnityEngine;
using UnityEngine.UI;

namespace WorkShop.EquipMake.View
{
    public class MakeTypeInfo:MonoBehaviour
    {
        private GameObject m_info;
        private GameObject m_select;
        private GameObject m_empty;

        private Image m_icon;
        private Text m_type;
        private Text m_resLevel;
        private Text m_itemLevel;

        private Action<GameObject> m_clickAction;

        public void InitComponent(Action<GameObject> click)
        {
            m_clickAction = click;

            m_select = transform.Find("Select").gameObject;
            m_select.SetActive(false);
            m_empty = transform.Find("Empty").gameObject;
            m_empty.SetActive(false);

            m_info = transform.Find("Info").gameObject;

            m_icon = transform.Find("Info/Icon/Icon").GetComponent<Image>();
            m_type = transform.Find("Info/Type").GetComponent<Text>();
            m_resLevel = transform.Find("Info/ResLevel/Level").GetComponent<Text>();
            m_itemLevel = transform.Find("Info/ItemLevel/Level").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Btn/Btn"),Click);
        }

        public void UpdateInfo(int id)
        {
            if (id != -1)
            {
                Forge_config forge = Forge_configConfig.GetForge_config(id);
                m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.EquipTypeIcon,forge.typeIcon);
                m_icon.SetNativeSize();
                m_type.text = forge.typeName;
                m_resLevel.text = WorkshopSystem.Instance.GetResearchLevel(forge.ERType).ToString();
            }
            m_info.gameObject.SetActive(id != -1);
            m_empty.SetActive(id == -1);
            m_itemLevel.transform.parent.gameObject.SetActive(false);
        }

        public void UpdateLevel(int type,int materialId)
        {
            m_itemLevel.transform.parent.gameObject.SetActive(materialId!=0);
            if (materialId != 0)
            {
                int min = ControllerCenter.Instance.EquipMakeController.GetMinItemLevel(type);
                int max = ControllerCenter.Instance.EquipMakeController.GetMaxItemLevel(materialId,type);
                m_itemLevel.text = min + "-" + max;
            }
        }


        public void UpdateSelectShow(bool show)
        {
            m_select.SetActive(show);
        }

        private void Click()
        {
            if (m_clickAction != null)
            {
                m_clickAction(m_select);
            }
        }
    }
}
