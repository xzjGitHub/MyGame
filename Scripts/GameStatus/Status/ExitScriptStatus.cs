
public class ExitScriptStatus:IGameStatus
{
    public void Enter()
    {
        GameObjectPool.Instance.DeatroyAllPool();
        UIEffectFactory.Instance.FreeAll(true);
        UIPanelManager.Instance.DestroyAllPanelNotContain(null);
        ControllerCenter.Instance.UnInitialize();
        GameStatusManager.Instance.Clear();
        BuildUIController.Instance.Reset();
        UIPanelManager.Instance.Hide<NewMainPanel>();
        UIPanelManager.Instance.Show<StartPanel>();
    }
}
