
using UnityEngine;
using UnityEngine.UI;


public class MinTipPanel: UIPanelBehaviour
{

    private Text m_des;
   // private RectTransform m_rect;
    private void Awake()
    {
        m_des = transform.Find("Des").GetComponent<Text>();
      //  m_rect = transform.GetComponent<RectTransform>();

        Utility.AddButtonListener(transform.Find("Image"),Close);
    }

    public void SetShowInfo(string des)
    {
        m_des.text = des;
    }

    public void UpdatePos(Vector3 pos, float height = 0)
    {
        //Vector3 tempPos = TipPanelPosUtil.UpdatePos(transform,m_rect,pos,height);
        //transform.localPosition = tempPos;
    }

    private void Close()
    {
        UIPanelManager.Instance.Hide<MinTipPanel>();
    }
}
