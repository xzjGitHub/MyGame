using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipDetailInfo: MonoBehaviour
{
    private GameObject m_enchanTag;
    private GameObject m_enchantTitle;
    private GameObject m_enchantTipObj;
    private Image m_icon;
    private Text m_name;
    private Text m_quility;

    private bool m_hasInit;
    private EquipTipAttrInfo m_attr;
    private EnchantAttrTip m_enchantInfo;

    private List<GameObject> m_starList = new List<GameObject>();

    private void OnDisable()
    {
        Free();
    }

    private void InitComponent()
    {
        m_enchanTag = transform.Find("EnchanTag").gameObject;
        m_enchantTitle = transform.Find("Scroll/Content/EnchantTitle").gameObject;
        m_enchantTipObj = transform.Find("Scroll/Content/EnchantAttr").gameObject;
        m_icon = transform.Find("Title/Icon").GetComponent<Image>();
        m_name = transform.Find("Title/Name").GetComponent<Text>();
        m_quility = transform.Find("Scroll/Content/Parent/Quli/Qui").GetComponent<Text>();
        Transform trans = transform.Find("Scroll/Content/Parent/Quli/Grid");
        for(int i = 0; i < trans.childCount; i++)
        {
            m_starList.Add(trans.GetChild(i).transform.Find("Have").gameObject);
        }
    }

    public void InitInfo(EquipAttribute equipAttribute)
    {
        if(!m_hasInit)
        {
            InitComponent();
            m_hasInit = true;
        }

        m_name.text = EquipTipEx.GetEquipName(equipAttribute);
        m_quility.text = "品质: " + equipAttribute.equipRnd.equipQuality;
        m_icon.sprite = ResourceLoadUtil.LoadItemIcon(equipAttribute); //LoadItemQuiltySprite
        InitStar(equipAttribute.equipRnd.equipRank);

        List<AtrDesInfo> list = EquipTipEx.GetEquipAttr(equipAttribute);
        m_attr = Utility.RequireComponent<EquipTipAttrInfo>(transform.Find("Scroll/Content/Attr").gameObject);
        m_attr.Init(equipAttribute,list);

        InitEnchantInfo(equipAttribute);
    }

    private void InitStar(int rank)
    {
        int temp = rank - 1;
       // Transform trans = transform.Find("Scroll/Content/Parent/Quli/Grid");
        for(int i = 0; i < m_starList.Count; i++)
        {
            m_starList[i].SetActive((i <= temp));
        }
    }

    private void InitEnchantInfo(EquipAttribute equipAttribute)
    {
        bool showObj = equipAttribute.enchantRnd != null
            && equipAttribute.enchantRnd.instanceID != 0;

        m_enchanTag.SetActive(showObj);
        m_enchantTitle.SetActive(showObj);
        m_enchantTipObj.SetActive(showObj);

        if(showObj)
        {
            if(m_enchantInfo == null)
            {
                m_enchantInfo = Utility.RequireComponent<EnchantAttrTip>(m_enchantTipObj);
                m_enchantInfo.InitComponent();
            }
            m_enchantInfo.UpdateInfo(equipAttribute);
        }
    }


    public void Free()
    {
        if(m_attr != null)
            m_attr.Free();
        if(m_enchantInfo != null)
            m_enchantInfo.Free();
    }
}
