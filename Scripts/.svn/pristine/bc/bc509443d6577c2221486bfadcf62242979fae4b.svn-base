using UnityEngine;
using UnityEngine.UI;

public class Tip: MonoBehaviour
{
    private bool m_notFree;

    public void SetInfo(string message)
    {
        m_notFree = true;
        gameObject.GetComponent<Text>().text = message;
    }


    private void Update()
    {
        if(m_notFree)
        {
            if(transform.localPosition.y >= 420)
            {
                TipManager.Instance.FreeTip(this.gameObject);
                m_notFree = false;
            }
        }
    }
}
