using Core.View;
using EventCenter;
using System;
using System.Collections;

public class ClassNameDefine
{
    public const string Main = "Main";
    public const string Combat = "Combat";
    public const string Explore = "Explore";
}

public enum SceneType
{
    None,
    Main,
    Fight,
    MainPanel
}

public class SceneManager: Singleton<SceneManager>
{
    private bool Show = false;

    private SceneManager() { }

    public void LoadScene(SceneType type,bool showLoadPanel = true,
        Action loadDoneAction = null)
    {
        if(showLoadPanel)
        {
            LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.PlayClose();
        }
        switch(type)
        {
            case SceneType.Fight:
                SceneChange();
                (UIPanelManager.Instance.GetUiPanelBehaviour<LodingPanel>() as LodingPanel).Close();
                ResourceLoadUtil.LoadExploreModule1();
                break;
            case SceneType.Main:
                UIPanelManager.Instance.Show<NewMainPanel>();
                break;
        }
    }

    private void SceneChange()
    {
        GameObjectPool.Instance.DeatroyAllPool();
        PlayerPool.Instance.Clear();
        ItemCostFactory.Instance.Clear();
        SpriteManager.Instance.FreeAll();
        UIEffectFactory.Instance.FreeAll(true);
      //  BuildUIController.Instance.Reset();
    }


    public void BackToUIScene()
    {
        UIPanelManager.Instance.Show<NewMainPanel>();
        EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.ShowMainPanelBtn);
        // UIPanelManager.Instance.Show<CorePanel>();
        BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Core);
    }

    public void EnterGame()
    {
        LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
        loding.action = ProLoad;
        loding.HideAction = LoadHide;
        loding.PlayClose();
    }

    private void ProLoad()
    {
        Game.Instance.StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return null;
        GameStatusManager.Instance.ChangeStatus(GameStatus.BeginEnterScript);
    }

    public void PreLoadEnd()
    {
        Game.Instance.StartCoroutine(LoadRes());
    }

    private IEnumerator LoadRes()
    {
        if(Show)
        {
            //创建前置探索
            if(ScriptSystem.Instance.ScriptPhase == ScriptPhase.Front)
            {
                var mapID = ScriptSystem.Instance.ScriptTemplate.initialMap;
                //
                var zone = FortSystem.Instance.NewZone;
                FortSystem.Instance.PrepareExplore(new ExploreMap(mapID,0,0,zone));
                yield return null;
                Instance.LoadScene(SceneType.Fight);
                yield return null;
            }
            else
            {
                ShowMain();
                yield return null;
                LodingPanel loding = UIPanelManager.Instance.GetUiPanelBehaviour<LodingPanel>();
                loding.Close();
            }
        }
        else
        {
            ShowMain();
            yield return null;
            LodingPanel loding = UIPanelManager.Instance.GetUiPanelBehaviour<LodingPanel>();
            loding.Close();
        }
    }


    private void ShowMain()
    {
        UIPanelManager.Instance.Show<NewMainPanel>();
        BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Core);
        // UIPanelManager.Instance.Show<CorePanel>(CavasType.Three,new List<object> { });
    }
    
    private void LoadHide()
    {
        CorePanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<CorePanel>();
        if(panel != null)
            panel.PlayNpcWalk();
    }
}
