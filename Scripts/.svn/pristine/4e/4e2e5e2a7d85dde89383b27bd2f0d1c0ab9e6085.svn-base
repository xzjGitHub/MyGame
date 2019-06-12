
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/28 15:10:30
//Note:     
//--------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GameSetting: MonoBehaviour
{
    [Header("是否显示前置探索")]
    public bool ShowFront = true;

    [Header("是否显示新手引导")]
    public bool ShowGuide = true;

    public void Set()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if DEBUG
        Utility.RequireComponent<LogConsole>(gameObject);
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UIPanelManager.Instance.Show<GmPanel>(CavasType.PopUI);
        }
#endif
    }
}

