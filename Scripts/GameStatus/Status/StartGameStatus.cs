
public class StartGameStatus:IGameStatus
{
    public void Enter()
    {
        GameModules.Init();
        UIPanelManager.Instance.Show<StartPanel>();
    }
}
