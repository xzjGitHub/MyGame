using System.Collections.Generic;

public class GameModules
{
    private static Dictionary<string, IGameModule> modules = new Dictionary<string, IGameModule>();
    //
    private static bool isLoad = false;
    //
    public static PlayerSystem playerSystem;
    public static PopupSystem popupSystem;


    /// <summary>
    /// 添加模块
    /// </summary>
    /// <param name="ModuleName">模块名字</param>
    /// <param name="mdl">模块</param>
    public static void AddModule(string ModuleName, IGameModule mdl)
    {
        if (modules.ContainsKey(ModuleName))
        {
            modules[ModuleName] = mdl;
            return;
        }
        modules.Add(ModuleName, mdl);
    }

    /// <summary>
    /// 移除模块
    /// </summary>
    /// <param name="ModuleName">模块名字</param>
    public static void RemoveModule(string ModuleName)
    {
        if (!modules.ContainsKey(ModuleName)) return;
        modules.Remove(ModuleName);
    }

    /// <summary>
    /// 查找模块
    /// </summary>
    /// <param name="name">模块名字</param>
    /// <returns></returns>
    public static IGameModule Find(string name)
    {
        return modules.ContainsKey(name) ? modules[name] : null;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public static void Init()
    {
        if (isLoad) return;
        //
        isLoad = true;
        SetScreen();
        //
        playerSystem = new PlayerSystem();
        AddModule(ModuleName.playerSystem, playerSystem);
        //
        popupSystem=new PopupSystem();
        AddModule(ModuleName.popupSystem,popupSystem);
        //
        StartModules();
    }

    /// <summary>
    /// 设置游戏参数
    /// </summary>
    private static void SetScreen()
    {
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //Screen.orientation = ScreenOrientation.AutoRotation;
        //Screen.autorotateToLandscapeLeft = true;
        //Screen.autorotateToLandscapeRight = true;
        //Screen.autorotateToPortrait = false;
        //Screen.autorotateToPortraitUpsideDown = false;
    }

    /// <summary>
    /// 开始模块
    /// </summary>
    static void StartModules()
    {
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.BeforeStartModule();
            }
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.StartModule();
            }
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.AfterStartModule();
            }
        }
    }

    /// <summary>
    /// 停止模块
    /// </summary>
    public static void StopModules()
    {
        if (!isLoad)
        {
            return;
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.BeforeStopModule();
            }
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.StopModule();
            }
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.AfterStopModule();
            }
        }
    }

    /// <summary>
    /// 更新模块
    /// </summary>
    public static void UpdateModules()
    {
        if (!isLoad)
        {
            return;
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.BeforeUpdateModule();
            }
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.UpdateModule();
            }
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                item.Value.AfterUpdateModule();
            }
        }
    }

    /// <summary>
    /// 释放场景
    /// </summary>
    public static void OnFreeScene()
    {
        if (!isLoad)
        {
            return;
        }
        foreach (KeyValuePair<string, IGameModule> item in modules)
        {
            if (item.Key != null)
            {
                //   x.Value.OnFreeScene();
            }
        }
    }

}

