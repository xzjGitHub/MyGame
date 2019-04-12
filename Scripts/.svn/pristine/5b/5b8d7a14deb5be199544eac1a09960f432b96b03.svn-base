using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem: MonoBehaviour
{
    // public int ItemId;

    protected GameObject m_select;

    protected Image m_icon;
    //protected Image m_quility;
    protected Text m_num;

    protected ItemAttribute m_attr;
    public ItemAttribute Attr
    {
        get { return m_attr; }
    }

    protected Action<ItemAttribute> m_clickCallBack;
    protected Action<ItemAttribute,GameObject> m_clickCallBackObj;

    protected virtual void InitSelfComponent() { }

    protected void InitComponent()
    {
        m_select = transform.Find("Select").gameObject;
        m_select.SetActive(false);

        m_icon = transform.Find("Icon").GetComponent<Image>();
        //   m_quility = transform.Find("Quility").GetComponent<Image>();
        //  m_quility.gameObject.SetActive(false);
        m_num = transform.Find("Num").GetComponent<Text>();

        Utility.AddButtonListener(transform.Find("Btn"),Click);

        InitSelfComponent();
    }

    public void Click()
    {
        if(m_clickCallBack != null)
        {
            m_clickCallBack(m_attr);
        }
        if(m_clickCallBackObj != null)
        {
            m_clickCallBackObj(m_attr,gameObject);
        }
    }

    public void UpdateSelectShow(bool show)
    {
        m_select.SetActive(show);
    }

    public void UpdateNum(int remainNum)
    {
        m_attr.sum = remainNum;
        m_num.text = remainNum.ToString();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

