
namespace GameFsmMachine
{
    public enum GameFsmType
    {
        None,
        /// <summary>
        /// 开始游戏
        /// </summary>
        StartGame,
        /// <summary>
        /// 初始化剧本
        /// </summary>
        InitScript,
        /// <summary>
        /// 前置探索
        /// </summary>
        FrontFight,
        /// <summary>
        /// 主场景
        /// </summary>
        MainScene,
        /// <summary>
        /// 战斗
        /// </summary>
        Fight,
        /// <summary>
        /// 退出剧本
        /// </summary>
        ExitScript,
        /// <summary>
        /// 退出游戏
        /// </summary>
        ExitGame,
    }

    public class GameFsmStateNameDefine
    {
        public const string GameFsm = "GameFsm";
        public const string Defaults = "defaults";
        public const string FSMState = "FSMState";
        public const string StartGameState = "StartGameState";
        public const string InitScriptState = "InitScriptState";
        public const string FrontState = "FrontState";
        public const string MainSceneState = "MainSceneState";
        public const string FightState = "FightState";
    }

    public class TrisitionNameDefine
    {
        public const string DefaultStateToStartGame = "DefaultStateToStartGame";
        public const string StartGameStateToInitScriptState = "StartGameStateToInitScriptState";
        public const string InitScriptStateToFront = "InitScriptStateToFront";
        public const string InitScriptToMain = "InitScriptToMain";
        public const string MainToFight = "MainToFight";
        public const string MainToStart = "MainToStart";
        public const string FightToMain = "FightToMain";
    }
}
