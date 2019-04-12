using UnityEngine;

public partial class Game: MonoBehaviour
{
    public static Game Instance;

    private void Awake()
    {
        Instance = this;
        ScreenSet();
        InitComponent();
        GameStatusManager.Instance.ChangeStatus(GameStatus.StartGame); 
        DontDestroyOnLoad(this);
    }
      
    private void Update()
    {
        GameModules.UpdateModules();
        TipManager.Instance.Tick();
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UIPanelManager.Instance.Show<GmPanel>(CavasType.PopUI);
        }
#endif
    }

    private void OnDestroy()
    {
        GameModules.OnFreeScene();
    }

    private void OnApplicationQuit()
    {
        if(ScriptSystem.Instance != null)
            GameDataManager.SaveGameData();
    }
}
