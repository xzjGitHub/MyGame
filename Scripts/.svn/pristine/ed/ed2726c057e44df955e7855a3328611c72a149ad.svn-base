using UnityEngine;


public class LoopScrollItem : MonoBehaviour
{
    private LoopScroller m_scroller;
    private int m_index;

    public int Index
    {
        get { return m_index; }
        set
        {
            m_index = value;
            transform.localPosition = m_scroller.GetPosition(m_index);
        }
    }

    public LoopScroller Scroller
    {
        set { m_scroller = value; }
    }
}
