﻿
public class ExitScriptStatus:IGameStatus
{
    public void Enter()
    {
        ControllerCenter.Instance.UnInitialize();
        GameStatusManager.Instance.Clear();
        BuildUIController.Instance.Reset();
    }
}
