
public class BeginEnterScriptStatus:IGameStatus
{
    public void Enter()
    {
        ControllerCenter.Instance.Init();
        Script_template scriptTemplate = Script_templateConfig.GetAll()[0];
        ScriptController.InitLevelData(scriptTemplate.templateID);
        ControllerCenter.Instance.Initialize();
        ScriptTimeSystem.Instance.StartTiming();
    }
}

