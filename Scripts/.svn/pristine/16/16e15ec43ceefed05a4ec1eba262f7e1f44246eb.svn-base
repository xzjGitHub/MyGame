using EventCenter;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharAttrTip: MonoBehaviour
{
    private Transform m_parent;
    private GameObject m_prefab;

    private bool m_hasInit;

    private void OnEnable()
    {
        EventManager.Instance.RegEventListener(EventSystemType.UI,EventTypeNameDefine.HideCharAttrTip,HideObj);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnRegEventListener(EventSystemType.UI,EventTypeNameDefine.HideCharAttrTip,HideObj);
        Free();
    }

    public void InitComponent()
    {
        if(!m_hasInit)
        {
            m_parent = transform.Find("Parent/Grid");
            m_prefab = transform.Find("Parent/Grid/Prefab").gameObject;
            m_prefab.SetActive(false);
            Utility.AddButtonListener(transform.Find("Mask"),() => gameObject.SetActive(false));
            m_hasInit = true;
        }
    }

    private void HideObj()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public void UpdateInfo(int id,CharAttribute attr)
    {
        Free();

        Char_display dis = Char_displayConfig.GetChar_display(id); ;

        for(int i = 0; i < dis.details.Count; i++)
        {
            GameObject obj = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.CharAttrTipItem,m_prefab);
            Utility.SetParent(obj,m_parent);
            Char_display temp = Char_displayConfig.GetChar_display(dis.details[i]);
            double value = Utility.GetNumberPoint(CharAttributeUtil.GetAttrValue(attr,temp.attributeName),2);
            if(temp.isPercentage == 1)
            {
                obj.GetComponent<Text>().text = temp.attributeNameCN + "：" + Utility.GetPercent((float)value,2);
            }
            else
            {
                obj.GetComponent<Text>().text = temp.attributeNameCN + "：" + value;
            }
        }
    }

    private void Free()
    {
        GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.CharAttrTipItem);
    }
}