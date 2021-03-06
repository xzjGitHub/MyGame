﻿using LskConfig;
using UnityEngine;

public class UISceneManager : MonoBehaviour
{
    void Awake()
    {
        ConfigManager.ResPath = "config";
        //Application.streamingAssetsPath + "/Config";
        ConfigManager.Instance.Init(delegate (int a, int b) { LogHelper_MC.Log("Progress: " + a + "     " + b); }, delegate { LogHelper_MC.Log("Finshed"); });

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