using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    TopBottom,
    LeftRight
}

public class UpdatePos: MonoBehaviour
{
    [SerializeField]
    private ScrollRect m_scrollRect;
    [SerializeField]
    private RectTransform m_targetRect;

    [SerializeField]
    private Direction m_Direction = Direction.TopBottom;

    [SerializeField]
    private float Min;
    [SerializeField]
    private float Max;

    private float m_target;


    private void Start()
    {
        m_scrollRect.onValueChanged.AddListener(ScrollRectChange);
    }

    private void OnEnable()
    {
        if (m_targetRect != null)
        {
            if (m_Direction == Direction.TopBottom)
            {
                m_targetRect.transform.localPosition = new Vector3(0,Min,0);
            }
        }
    }

    private void ScrollRectChange(Vector2 arg0)
    {
        if(m_Direction == Direction.TopBottom)
        {
            if(m_scrollRect.verticalNormalizedPosition <= 0)
            {
                m_target = Max;
            }
            else if(m_scrollRect.verticalNormalizedPosition >= 1)
            {
                m_target = Min;
            }
            else
            {
                m_target = Min - Mathf.Abs(Max) * (1 - m_scrollRect.verticalNormalizedPosition);
            }
            m_targetRect.transform.localPosition = new Vector3(0,m_target,0);

        }
        else
        {
            if(m_scrollRect.horizontalNormalizedPosition < 0)
            {
                m_target = Max;
            }
            else if(m_scrollRect.horizontalNormalizedPosition > 1)
            {
                m_target = 0;
            }
            else
            {
                m_target = Max * (1 - m_scrollRect.horizontalNormalizedPosition);
            }
            m_targetRect.transform.localPosition = new Vector3(m_target,0,0);
        }
    }
}
