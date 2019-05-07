﻿using EventCenter;
using GameFsmMachine;

public enum SceneType
{
    None,
    EnterGame,
    StartToMain,
    StartToFront,
    Fight,
    BackMain,
    BackToStart
}

public class SceneManagerUtil
{
    public static void LoadScene(SceneType type,object obj=null)
    {
        IChangeScene changeScene = null;
        switch(type)
        {
            case SceneType.EnterGame:
                changeScene = new EnterGamer();
                break;
            case SceneType.Fight:
                bool showLoad = obj == null ? true : (bool)obj;
              //  GameFsmManager.Instance.IsFrontBack = showLoad;
              //  EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.MainScene);
                // changeScene = new MainToFight();
                break;
            case SceneType.BackMain:
                changeScene = new FightToMainScene();
                break;
            case SceneType.BackToStart:
                changeScene = new BackToStartScene();
                break;
            default:
                LogHelperLSK.LogError("sceneType error...");
                break;
        }
        if(changeScene != null)
            changeScene.Action(obj);
    }
}
