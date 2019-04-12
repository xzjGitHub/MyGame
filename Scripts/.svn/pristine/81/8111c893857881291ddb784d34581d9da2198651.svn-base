
public class GameStatusManager:Singleton<GameStatusManager>
{
    private GameStatusFsm m_gameStatusFsm;

    private GameStatusManager()
    {
        m_gameStatusFsm = new GameStatusFsm();
    }

    public void ChangeStatus(GameStatus gameStatus)
    {
        m_gameStatusFsm.ChangeState(gameStatus);
    }

    public void Clear()
    {
        m_gameStatusFsm.Clear();
    }
}
