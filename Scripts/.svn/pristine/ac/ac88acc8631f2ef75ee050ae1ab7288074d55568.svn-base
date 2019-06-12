
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/31/2019
//Note:     
//--------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ClickScreen: MonoBehaviour
{
    private ClickEffect m_eff;
    private RectTransform m_rect;
    private Camera m_camera;

    private Vector2 m_localPos;

    private void Awake()
    {
        m_eff = transform.Find("ClickScreenEffect").GetComponent<ClickEffect>();
        m_rect = gameObject.GetComponent<RectTransform>();
        m_camera = transform.parent.GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_rect,
                Input.mousePosition,m_camera,out m_localPos);
            m_eff.transform.localPosition = m_localPos;
            m_eff.Play();
        }
    }
}
