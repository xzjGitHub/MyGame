using System;
using UnityEngine;
using UnityEngine.UI;


public class ConfirmPanel : UIPanelBehaviour {

    private GameObject m_sureBtn;
    private GameObject m_cancelBtn;

    private Text m_title;
    private Text m_info;

    private Action m_sureCallBack;
    private Action m_cancelCallBack;

    protected override void OnAwake()
    {
        m_sureBtn = transform.Find("Parent/BtnParent/SureBtn").gameObject;
        m_cancelBtn = transform.Find("Parent/BtnParent/CancelBtn").gameObject;

        m_title = transform.Find("Parent/Title").GetComponent<Text>();
        m_info = transform.Find("Parent/Des").GetComponent<Text>();

        Utility.AddButtonListener(transform.Find("Mask"), Close);
        Utility.AddButtonListener(transform.Find("Parent/BtnParent/SureBtn/Btn"),ClickSure);
        Utility.AddButtonListener(transform.Find("Parent/BtnParent/CancelBtn/Btn"),ClickCancel);
    }

    public void SetInfo(Action sure,Action cancel,string title,string info,
        bool showSure=true,bool showCancel=true)
    {
        m_title.text = title;
        m_info.text = info;
        m_sureCallBack = sure;
        m_cancelCallBack = cancel;

        m_sureBtn.SetActive(showSure);
        m_cancelBtn.SetActive(showCancel);
    }

    private void ClickSure()
    {
        if (m_sureCallBack!=null)
        {
            m_sureCallBack();
        }
        Close();
    }

    private void ClickCancel()
    {
        if(m_cancelCallBack != null)
        {
            m_cancelCallBack();
        }
        Close();
    }

    private void Close()
    {
        UIPanelManager.Instance.Hide<ConfirmPanel>(false);
    }
}
