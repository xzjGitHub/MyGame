using UnityEngine;
using UnityEngine.UI;

public class QuitListen:MonoBehaviour
{
    private Text m_quitTipText;

    private bool m_canQuit;
    private float m_tipDisappearTime = 0.5f;
    private float m_clickTime;

    private Color m_startClor = new Color(255 / 255f,255 / 255f,255 / 255f,255 / 255f);
    private Color m_endColor = new Color(255 / 255f,255 / 255f,255 / 255f,0);

    private void Awake()
    {
        m_quitTipText = gameObject.GetComponent<Text>();
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android && 
            Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_clickTime == 0)
            {
                m_clickTime = Time.time;
            }
            else
            {
                if(m_canQuit)
                {
                    Application.Quit();
                }
            }
        }
        else if(m_clickTime != 0)
        {
            m_canQuit = true;
            gameObject.SetActive(true);
            m_quitTipText.color = Color.Lerp(m_startClor,m_endColor,
                (Time.time - m_clickTime) * m_tipDisappearTime);
            if(m_quitTipText.color.a == 0)
            {
                m_clickTime = 0;
                m_canQuit = false;
            }
        }
    }
}

