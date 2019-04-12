using UnityEngine;

public class ShowFPS_OnGUI : MonoBehaviour
{

    public float fpsMeasuringDelta = 2.0f;

    private float timePassed;
    private int m_FrameCount = 0;
    private float m_FPS = 0.0f;

    private void Start()
    {
        timePassed = 0.0f;
    }

    private void Update()
    {
        m_FrameCount = m_FrameCount + 1;
        timePassed = timePassed + Time.deltaTime;

        if (timePassed > fpsMeasuringDelta)
        {
            m_FPS = m_FrameCount / timePassed;

            timePassed = 0.0f;
            m_FrameCount = 0;
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.background = null;    //这是设置背景填充的
        style.normal.textColor = new Color(1.0f, 0.5f, 0.0f);
        style.fontSize = 20;

        //居中显示FPS
        GUI.Label(new Rect(50 - 40, 0, 200, 200), "FPS: " + m_FPS, style);
    }
}