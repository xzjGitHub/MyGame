
using UnityEngine;
using UnityEngine.UI;

public class CommonTip1: UIPanelBehaviour
{

    private Text m_name;
    private Text m_des;

   // private int m_selfWidth = 394;  //自身的宽度
   // private int m_interval = 30;    //间隔

   // private int m_bottomY = -360;   //最下边的坐标
   // private int m_topY = 360;       //最上边的坐标
    //private int m_distanceToBottomOrTop = 20;   //距离最上或者最小的最小距离

   // private RectTransform m_rect;
    private void Awake()
    {
        m_name = transform.Find("Name").GetComponent<Text>();
        m_des = transform.Find("Des").GetComponent<Text>();

      //  m_rect = transform.GetComponent<RectTransform>();
        Utility.AddButtonListener(transform.Find("Image"),Close);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos">当前的世界坐标</param>
    /// <param name="width">宽度</param>
    /// <param name="height">高度</param>
    public void UpdatePos(Vector3 pos,float width = 0,float height = 0)
    {
        //Camera camera = UIPanelManager.Instance.UICamera;
        //Vector3 screenPos = camera.WorldToScreenPoint(pos);

        //Vector3 finaPos;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rect,
        //    new Vector2(screenPos.x,screenPos.y),camera,out finaPos);
        //transform.position += finaPos;

        //float localPosX = transform.localPosition.x - width / 2 - m_interval - m_selfWidth / 2;
        //float localPosY = 0;
        //if(transform.localPosition.y <= m_bottomY + m_distanceToBottomOrTop + height / 2)
        //{
        //    localPosY = m_bottomY + m_distanceToBottomOrTop + height / 2;
        //}
        //else if(transform.localPosition.y >= m_topY - m_distanceToBottomOrTop - height / 2)
        //{
        //    localPosY = m_topY - m_distanceToBottomOrTop - height / 2;
        //}
        //else
        //{
        //    localPosY = transform.localPosition.y;
        //}

        //transform.localPosition = new Vector3(localPosX,localPosY);

        //Vector3 tempPos = TipPanelPosUtil.UpdatePos(transform, m_rect, pos, width, height);
        //transform.localPosition = tempPos;
    }

    public void SetShowInfo(string name,string des)
    {
        m_name.text = name;
        m_des.text = des;
    }

    private void Close()
    {
        UIPanelManager.Instance.Hide<CommonTip1>();
    }
}


