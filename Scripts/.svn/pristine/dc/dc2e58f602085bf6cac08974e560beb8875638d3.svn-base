using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchCon: MonoBehaviour
{
    /// <summary>
    /// 拖拽的滑动列表
    /// </summary>
    private ScrollRect m_rect;

    /// <summary>
    /// 得到图片
    /// </summary>
    private RectTransform m_bg;

    /// <summary>
    /// 上一帧两指间距离
    /// </summary>
    private float m_lastDistance = 0;

    /// <summary>
    /// 当前两个手指之间的距离
    /// </summary>
    private float m_twoTouchDistance = 0;


    /// <summary>
    /// 第一根手指按下的坐标
    /// </summary>
    private Vector2 m_firstTouch = Vector3.zero;

    /// <summary>
    /// 第二根手指按下的坐标
    /// </summary>
    private Vector2 m_secondTouch = Vector3.zero;

    /// <summary>
    /// 是否有两只手指按下
    /// </summary>
    private bool m_isTwoTouch = false;

    private void Awake()
    {
        m_rect = transform.Find("Scroll").GetComponent<ScrollRect>();
        m_bg = transform.Find("Scroll/Bg").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果有两个及以上的手指按下
        if(Input.touchCount > 1)
        {
            m_rect.enabled = false;

            //当第二根手指按下的时候
            if(Input.GetTouch(1).phase == TouchPhase.Began)
            {
                m_isTwoTouch = true;
                //获取第一根手指的位置
                m_firstTouch = Input.touches[0].position;
                //获取第二根手指的位置
                m_secondTouch = Input.touches[1].position;

                m_lastDistance = Vector2.Distance(m_firstTouch,m_secondTouch);
            }

            //如果有两根手指按下
            if(m_isTwoTouch)
            {
                //每一帧都得到两个手指的坐标以及距离
                m_firstTouch = Input.touches[0].position;
                m_secondTouch = Input.touches[1].position;
                m_twoTouchDistance = Vector2.Distance(m_firstTouch,m_secondTouch);

                //当前图片的缩放
                Vector3 curImageScale = new Vector3(m_bg.localScale.x,m_bg.localScale.y,1);
                //两根手指上一帧和这帧之间的距离差
                //因为100个像素代表单位1，把距离差除以100看缩放几倍
                float changeScaleDistance = (m_twoTouchDistance - m_lastDistance) / 500;
                //因为缩放 Scale 是一个Vector3，所以这个代表缩放的Vector3的值就是缩放的倍数
                Vector3 changeScale = new Vector3(changeScaleDistance,changeScaleDistance,0);
                //图片的缩放等于当前的缩放加上 修改的缩放
                m_bg.localScale = curImageScale + changeScale;
                //控制缩放级别
                m_bg.localScale = new Vector3(Mathf.Clamp(m_bg.localScale.x,0.5f,10f),Mathf.Clamp(m_bg.localScale.y,0.5f,10f),1);
                //这一帧结束后，当前的距离就会变成上一帧的距离了
                m_lastDistance = m_twoTouchDistance;
                //if((twoTouchDistance - lastDistance) < 0)
                //{
                //    image.transform.localPosition = Vector3.zero;
                //}
                m_bg.transform.localPosition = Vector3.zero;

                //if (m_bg.transform.localScale.x < 0.95f)
                //{
                //    MainPanelEventCenter.Instance.EmitClearSelectEvent();
                //}
            }

            //当第二根手指结束时（抬起）
            if(Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                m_isTwoTouch = false;
                m_firstTouch = Vector3.zero;
                m_secondTouch = Vector3.zero;
            }
        }
        else
        {
            m_rect.enabled = true;
        }
    }
}
