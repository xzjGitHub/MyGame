using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderPos:MonoBehaviour
{
    [SerializeField]
    public float Offset;
    [SerializeField]
    public Image TargetImage;
    [SerializeField]
    public float ParentWidth;

    private RectTransform m_rect;

    private void OnEnable()
    {
        UpdatePos();
    }

    public void UpdatePos()
    {
        if (m_rect == null)
        {
            m_rect = gameObject.GetComponent<RectTransform>();
        }
        m_rect.anchoredPosition = new Vector3(ParentWidth * TargetImage.fillAmount - Offset,0,0);
    }
}

