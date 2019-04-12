using GameEventDispose;
using System;

/// <summary>
/// 玩家系统
/// </summary>
public class PlayerSystem : IGameData, IGameModule
{
    public static PlayerSystem Instance { get { return instance; } }

    public int Token { get { return token; } }

    public int SelectIndex { get { return selectIndex; } }


    public PlayerSystem()
    {
        instance = this;
        Init();
    }

    /// <summary>
    /// 新建剧本系统
    /// </summary>
    /// <param name="_scriptID"></param>
    /// <param name="_initTime"></param>
    public ScriptSystem InitScriptSystem(int _scriptID, float _initTime = 0, int _selectIndex = 0)
    {
        selectIndex = _selectIndex;
        playerData.lastScriptID = _scriptID;
        //
        if (HasPlay(_scriptID))
        {
            return new ScriptSystem(_scriptID, _initTime, selectIndex, false);
        }
        return new ScriptSystem(_scriptID, _initTime, _selectIndex);
    }
    /// <summary>
    /// 新建剧本系统
    /// </summary>
    public ScriptSystem InitScriptSystem(bool _isNew, int _scriptID, float _initTime = 0, int _selectIndex = 0)
    {
        selectIndex = _selectIndex;
        playerData.lastScriptID = _scriptID;
        return new ScriptSystem(_scriptID, _initTime, _selectIndex, _isNew);
    }

    public void AddScriptId(int _scriptID)
    {
        if (!playerData.ScriptsPlayTimes.ContainsKey(_scriptID))
        {
            playerData.ScriptsPlayTimes[_scriptID] = 1;
        }
        else
        {
            long havePlayTime = playerData.ScriptsPlayTimes[_scriptID];
            playerData.ScriptsPlayTimes[_scriptID] = havePlayTime + 1;
        }
    }

    /// <summary>
    /// 是否已经玩了这个剧本
    /// </summary>
    /// <param name="_scriptID"></param>
    /// <returns></returns>
    public bool HasPlay(int _scriptID)
    {
        return playerData.ScriptsPlayTimes.ContainsKey(_scriptID);
    }


    /// <summary>
    /// 获得上一次玩的剧本Id
    /// </summary>
    /// <returns></returns>
    public int GetLastPlayScriptId()
    {
        return playerData.lastScriptID;
    }

    /// <summary>
    /// 获得当前
    /// </summary>
    /// <returns></returns>
    public int GetCurrentScriptId()
    {
        return playerData.lastScriptID;
    }

    public long GetPlayCurrentScriptTimes()
    {
        if (playerData.ScriptsPlayTimes.ContainsKey(playerData.lastScriptID))
            return playerData.ScriptsPlayTimes[playerData.lastScriptID];
        return 0;
    }


    /// <summary>
    /// 添加代币
    /// </summary>
    /// <param name="num"></param>
    public void AddToken(int num)
    {
        token += num;
        EventDispatcher.Instance.SystemEvent.DispatchEvent(EventId.SystemEvent, GameSystemEventType.Token);
    }
    /// <summary>
    /// 减少代币
    /// </summary>
    /// <param name="num"></param>
    public void SubToken(int num)
    {
        token -= num;
        token = Math.Max(0, token);
        EventDispatcher.Instance.SystemEvent.DispatchEvent(EventId.SystemEvent, GameSystemEventType.Token);
    }

    public void SaveData(string parentPath = null)
    {
        playerData.token = token;
        playerData.selectIndex = selectIndex;
        GameDataManager.SaveData(filePath, PlayerFileName, playerData);
    }

    public void ReadData(string parentPath = null)
    {
        playerData = GameDataManager.ReadData<PlayerData>(parentPath + PlayerFileName) as PlayerData;
    }
    public void Init()
    {
        ReadData();
        //初始化
        if (playerData == null) playerData = new PlayerData();
        token = playerData.token;
    }

    private static PlayerSystem instance;
    private const string PlayerFileName = "Player";
    private string filePath;
    private int token;
    private int selectIndex;
    //
    private PlayerData playerData;

    #region 重新接口
    public void BeforeStartModule()
    {

    }

    public void StartModule()
    {

    }

    public void AfterStartModule()
    {

    }

    public void BeforeStopModule()
    {

    }

    public void StopModule()
    {

    }

    public void AfterStopModule()
    {

    }

    public void BeforeUpdateModule()
    {

    }

    public void UpdateModule()
    {

    }

    public void AfterUpdateModule()
    {

    }

    public void OnFreeScene()
    {

    }
    #endregion

}
