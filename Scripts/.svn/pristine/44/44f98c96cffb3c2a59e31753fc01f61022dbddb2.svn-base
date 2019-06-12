using System.Collections.Generic;
using UnityEngine;

class LogConsole: MonoBehaviour
{
    enum CurrentSelectType
    {
        All,
        Normal,
        Error,
        Warning
    }

    struct Log
    {
        public string message;
        public string stackTrace;
        public LogType type;
    }

    [Range(1,1000)]
    public int MaxLogNum = 500;
    private bool m_visible = false;

    private float m_timer = 0;
    private const float m_touchTime = 2f;

    private Rect m_windowRect = new Rect(0,0,Screen.width,Screen.height);
    private Vector2 m_scrollPosition;
    private readonly List<Log> m_logs = new List<Log>();

    private CurrentSelectType m_current = CurrentSelectType.All;

    private void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(m_visible)
            return;
        if(Input.GetKeyDown(KeyCode.L))
        {
            m_visible = true;
        }
#else
        if(m_visible)
            return;
        if(Input.touches.Length >= 3)
        {
            m_timer += Time.deltaTime;
            if(m_timer >= m_touchTime)
            {
                m_current = CurrentSelectType.All;
                m_visible = true;
                m_timer = 0;
            }
        }
#endif
    }

    private void OnGUI()
    {
        if(!m_visible)
        {
            return;
        }
        m_windowRect = GUILayout.Window(123456,m_windowRect,DrawConsoleWindow,"LogConsole");
    }

    private void DrawConsoleWindow(int windowID)
    {
        GUILayout.BeginVertical();
        DrawButton();
        DrawLog();
        GUILayout.EndVertical();
    }

    private float BtnWidth = (Screen.width - 100) / 6;
    private float BtnHeight = 50;

    private void DrawButton()
    {
        GUILayout.BeginHorizontal();

        if(GUILayout.Button("AllLog",GUILayout.Width(BtnWidth),
            GUILayout.Height(BtnHeight)))
        {
            m_current = CurrentSelectType.All;
        }

        if(GUILayout.Button("NormalLog",GUILayout.Width(BtnWidth),
           GUILayout.Height(BtnHeight)))
        {
            m_current = CurrentSelectType.Normal;
        }

        if(GUILayout.Button("WarningLog",GUILayout.Width(BtnWidth),
           GUILayout.Height(BtnHeight)))
        {
            m_current = CurrentSelectType.Warning;
        }

        if(GUILayout.Button("ErrorLog",GUILayout.Width(BtnWidth),
           GUILayout.Height(BtnHeight)))
        {
            m_current = CurrentSelectType.Error;
        }

        if(GUILayout.Button("ClearLog",GUILayout.Width(BtnWidth),
            GUILayout.Height(BtnHeight)))
        {
            m_logs.Clear();
        }

        if(GUILayout.Button("Close",GUILayout.Width(BtnWidth),
            GUILayout.Height(BtnHeight)))
        {
            m_current = CurrentSelectType.All;
            m_visible = false;
        }

        GUILayout.EndHorizontal();
    }

    private void DrawLog()
    {
        m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition);

        for(var i = 0; i < m_logs.Count; i++)
        {
            if(!Show(m_logs[i].type))
                continue;
            GUI.contentColor = GetColor(m_logs[i].type);
            GUILayout.Label(string.Format("Loginfo: {0}\n{1}",m_logs[i].message,m_logs[i].stackTrace));
        }

        GUILayout.EndScrollView();
    }

    private bool Show(LogType type)
    {
        switch(m_current)
        {
            case CurrentSelectType.All:
                return true;
            case CurrentSelectType.Normal:
                return type == LogType.Assert || type == LogType.Log;
            case CurrentSelectType.Error:
                return type == LogType.Error || type == LogType.Exception;
            case CurrentSelectType.Warning:
                return type == LogType.Warning;
            default:
                return true;
        }
    }

    private void HandleLog(string message,string stackTrace,LogType type)
    {
        m_logs.Add(new Log
        {
            message = message,
            stackTrace = stackTrace,
            type = type,
        });

        CheckMaxLog();
    }

    private void CheckMaxLog()
    {
        int count = Mathf.Max(m_logs.Count - MaxLogNum,0);
        if(count > 0)
        {
            m_logs.RemoveRange(0,count);
        }
    }

    private Color GetColor(LogType type)
    {
        switch(type)
        {
            case LogType.Error:
            case LogType.Exception:
                return Color.red;
            case LogType.Assert:
            case LogType.Log:
                return Color.white;
            case LogType.Warning:
                return Color.yellow;
            default:
                return Color.white;
        }
    }
}
