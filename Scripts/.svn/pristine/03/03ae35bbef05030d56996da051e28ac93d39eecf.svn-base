
public class EnterGamer: IChangeScene
{
    public override void Action(object obj)
    {
        IChangeScene change = null;

        if(!Game.Instance.ShowFront)
            ScriptSystem.Instance.ScriptPhase = ScriptPhase.Normal;

        if(ScriptSystem.Instance.ScriptPhase == ScriptPhase.Front)
        {
            change = new StartToFront();
        }
        else
        {
            change = new StartToMain();
        }
        change.Action(obj);
    }
}

