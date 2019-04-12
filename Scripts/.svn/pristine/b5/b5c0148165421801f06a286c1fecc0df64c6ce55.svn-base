using LskConfig;
using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    void Awake()
    {
        ConfigManager.ResPath = "config";
        //Application.streamingAssetsPath + "/Config";
        ConfigManager.Instance.Init(delegate (int a, int b) { LogHelperLSK.Log("Progress: " + a + "     " + b); }, delegate { LogHelperLSK.Log("Finshed"); });

        GameModules.Init();
    }


    void Start()
    {
        gameObject.BroadcastMessage("OnInitScene", SendMessageOptions.DontRequireReceiver);
    }

    void Update()
    {
        GameModules.UpdateModules();
    }

    void OnDestroy()
    {
        GameModules.OnFreeScene();
    }

}