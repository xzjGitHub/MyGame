﻿using GameFsmMachine;
using UnityEngine;

public class Game: MonoBehaviour
{
    public static Game Instance;
    public bool ShowFront;

    private void Awake()
    {
        Instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        DontDestroyOnLoad(this);
    }
      
    private void Update()
    {
        GameModules.UpdateModules();
        GameFsmManager.Instance.Update(Time.deltaTime);
        TipManager.Instance.Tick();
        Res.ResManager.Instance.Update();
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UIPanelManager.Instance.Show<GmPanel>(CavasType.PopUI);
        }
#endif
    }

    private void OnDestroy()
    {
        GameModules.OnFreeScene();
    }

    private void OnApplicationQuit()
    {
        if(ScriptSystem.Instance != null)
            GameDataManager.SaveGameData();
    }
}
