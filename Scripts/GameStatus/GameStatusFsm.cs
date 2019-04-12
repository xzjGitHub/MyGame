using System.Collections.Generic;

public enum GameStatus
{
    StartGame,
    ExitGame,
    BeginEnterScript,
    EnterScriptEnd,
    ExitScript
}

public class GameStatusFsm
{
    private Dictionary<GameStatus, IGameStatus> m_dict;

    public GameStatusFsm()
    {
        m_dict = new Dictionary<GameStatus,IGameStatus>();
    }


    private IGameStatus GetGameStatus(GameStatus gameStatus)
    {
        IGameStatus status = null;
        if (m_dict.ContainsKey(gameStatus))
        {
            status = m_dict[gameStatus];
        }
        else
        {
            switch(gameStatus)
            {
                case GameStatus.StartGame:
                    status = new StartGameStatus();
                    break;
                case GameStatus.ExitGame:
                    status = new ExitGameStatus();
                    break;
                case GameStatus.BeginEnterScript:
                    status = new BeginEnterScriptStatus();
                    break;
                case GameStatus.EnterScriptEnd:
                    status = new EnterScriptEndStatus();
                    break;
                case GameStatus.ExitScript:
                    status = new ExitScriptStatus();
                    break;
            }
            m_dict.Add(gameStatus, status);
        }
        return status;
    }

    public void ChangeState(GameStatus gameStatus)
    {
        IGameStatus status = GetGameStatus(gameStatus);
        status.Enter();
    }

    public void Clear()
    {
        m_dict.Clear();
    }
}

