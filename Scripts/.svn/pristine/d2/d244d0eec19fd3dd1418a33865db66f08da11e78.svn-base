using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnchantAttrTip: MonoBehaviour
{
    private GameObject m_prefab;

    private Text m_name;
    //private Text m_spec;


    //private void OnDisable()
    //{
    //    Free();
    //}

    public void InitComponent()
    {
        m_prefab = transform.Find("Prefab").gameObject;
        m_prefab.SetActive(false);

        m_name = transform.parent.Find("EnchantTitle/Name").GetComponent<Text>();
        //m_spec = transform.Find("Special").GetComponent<Text>();
    }

    public void UpdateInfo(EquipAttribute attr)
    {
        Free();

        Enchant_template temp = Enchant_templateConfig.GetEnchant_Template(attr.enchantRnd.instanceID);
        m_name.text = "附魔："+temp.enchantName;

        string speDes = string.Empty;

        List<AtrDesInfo> list = EnchantAttriUtil.GetEquipAttr(attr,out speDes);
        for(int i = 0; i < list.Count; i++)
        {
            GameObject obj = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.QquipEnchantAttr,m_prefab);
            Utility.SetParent(obj,transform);
            obj.GetComponent<Text>().text = list[i].Des;
        }

        //m_spec.text = speDes;
    }

    public void Free()
    {
        GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.QquipEnchantAttr);
    }

}
