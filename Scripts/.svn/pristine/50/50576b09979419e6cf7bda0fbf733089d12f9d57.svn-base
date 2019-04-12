using College.Research.Data;
using UnityEngine;
using UnityEngine.UI;

namespace College.Research.View
{
    public class EnchantEffItem: MonoBehaviour
    {
        private GameObject m_lock;

        private Text m_name;
        private Text m_des;

        private bool m_hasInit;

        public void InitComponent()
        {
            m_lock = transform.Find("Lock").gameObject;
            m_name = transform.Find("Name").GetComponent<Text>();
            m_des = transform.Find("Des").GetComponent<Text>();
        }

        public void UpdateInfo(int rareId,int effId)
        {
            if(!m_hasInit)
            {
                InitComponent();
                m_hasInit = true;
            }
            //bool hasIn = ResearchLabSystem.Instance.HasInEffect(rareId,effId);
            //todo
            bool hasIn = false;
            m_lock.SetActive(!hasIn);

            Enchant_template enchant = Enchant_templateConfig.GetEnchant_Template(effId);
            m_name.text = "";
            //todo
            m_des.text = "";
        }
    }
}
