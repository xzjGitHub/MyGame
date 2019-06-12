﻿using UnityEngine;

[RequireComponent(typeof(GameSetting))]
public class Game: MonoSingleton<Game>
{
    private GameSetting m_gameSetting;
    public GameSetting GameSetting { get { return m_gameSetting; } }

    private void Awake()
    {
        m_gameSetting = gameObject.GetComponent<GameSetting>();
        m_gameSetting.Set();
    }

    private void Update()
    {
        GameModules.UpdateModules();
        GameFsmMachine.GameFsmManager.Instance.Update(Time.deltaTime);
        TipManager.Instance.Tick();
        Res.ResManager.Instance.Update();
    }

    private void OnApplicationQuit()
    {
        if(ScriptSystem.Instance != null)
        {
            GameDataManager.SaveGameData();
            GameModules.OnFreeScene();
        }
    }
}
