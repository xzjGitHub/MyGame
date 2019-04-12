
public class BeginEnterScriptStatus:IGameStatus
{
    public void Enter()
    {
        ControllerCenter.Instance.Init();

        Script_template scriptTemplate = Script_templateConfig.GetAll()[0];
        ScriptController.InitLevelData(scriptTemplate.templateID);

        Game.Instance.StartCoroutine(PreLoadUti.Instance.PreLoad());
    }
}

