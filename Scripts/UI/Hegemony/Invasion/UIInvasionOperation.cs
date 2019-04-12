using System;
using GameEventDispose;
using UnityEngine;
using UnityEngine.UI;

public class UIInvasionOperation : MonoBehaviour
{
    public delegate void CallBack();

    public CallBack OnWarning;
    public CallBack OnResist;
    //
    private Button warningButton;
    private Button resistButton;
    //
    private bool isFirst;
    //
    private UIInvasionWarning invasionWarning;
    private UIInvasionResist invasionResist;
    private CycleInvasionSystem cycleInvasionSystem;


    public void OpenUI(CycleInvasionSystem _cycleInvasionSystem)
    {
        Init();
        //
        cycleInvasionSystem = _cycleInvasionSystem;
        //
        UpdateButtonShow();
        //
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 更新按钮显示
    /// </summary>
    private void UpdateButtonShow()
    {
        if (cycleInvasionSystem == null) return;
        switch (cycleInvasionSystem.InvasionPhase)
        {
            case CycleInvasionPhase.Idle:
                break;
            case CycleInvasionPhase.Preposition:
                break;
            case CycleInvasionPhase.Warning:
                if (!warningButton.gameObject.activeInHierarchy) warningButton.gameObject.SetActive(true);
                break;
            case CycleInvasionPhase.Siege:
                if (!resistButton.gameObject.activeInHierarchy) resistButton.gameObject.SetActive(true);
                break;
            case CycleInvasionPhase.SiegeEnd:
                break;
        }
    }

    private void Init()
    {
        if (!isFirst)
        {
            GetObj();
            //
            EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
            isFirst = true;
        }
        //
        invasionWarning.gameObject.SetActive(false);
        invasionResist.gameObject.SetActive(false);
        warningButton.gameObject.SetActive(false);
        resistButton.gameObject.SetActive(false);
    }


    private void OnClickWarning()
    {
        invasionWarning.OpenUI(cycleInvasionSystem);
        if (OnWarning == null) return;
        OnWarning();
    }
    private void OnClickResist()
    {
        invasionResist.OpenUI(cycleInvasionSystem);
        if (OnResist == null) return;
        OnResist();
    }

    private void GetObj()
    {
        warningButton = transform.Find("Warning").GetComponent<Button>();
        resistButton = transform.Find("Resist").GetComponent<Button>();
        warningButton.onClick.AddListener(OnClickWarning);
        resistButton.onClick.AddListener(OnClickResist);
        //
        invasionWarning = transform.Find("InvasionWarning").gameObject.AddComponent<UIInvasionWarning>();
        invasionResist = transform.Find("ResistInvasion").gameObject.AddComponent<UIInvasionResist>();
    }


    /// <summary>
    /// 剧本时间更新事件
    /// </summary>
    private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1, object arg2)
    {
        if (arg1 != ScriptTimeUpdateType.Day || !gameObject.activeSelf) return;
        //
        UpdateButtonShow();
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
    }



}
