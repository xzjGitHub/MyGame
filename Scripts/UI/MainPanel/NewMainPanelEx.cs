using System;
using Bag;

public partial class NewMainPanel
{
    public Action BackAction;

    private void ClickAdventure()
    {
        //创建前置探索
        if (ScriptSystem.Instance.ScriptPhase == ScriptPhase.Front)
        {
            //initialMap
            var mapID = ScriptSystem.Instance.ScriptTemplate.initialMap;
            //
            var zone = FortSystem.Instance.NewZone;
            FortSystem.Instance.PrepareExplore(new ExploreMap(mapID, 0, 0, zone));
            SceneManager.Instance.LoadScene(SceneType.Fight);
            return;
        }
        //
        UIPanelManager.Instance.Show<UIZone>();
        UIPanelManager.Instance.Hide<NewMainPanel>();
        BuildUIController.Instance.HidePanel();
    }

    private void ClickBack()
    {
        if (BackAction != null)
        {
            BackAction();
        }
        else
        {
            BuildUIController.Instance.ClickBack();
        }
    }

    private void ClickBuild(BuildingTypeIndex buildingTypeIndex)
    {
        if (BuildUIController.Instance.CanClick && 
            BuildUIController.Instance.CurreentBuild != buildingTypeIndex)
        {
            BuildUIController.Instance.ChangeBuild(buildingTypeIndex);
        }
    }

    private void ClickBag()
    {
        UIPanelManager.Instance.Show<BagPanel>(CavasType.SpecialUI);
    }

    private void ClickChar()
    {
        UIPanelManager.Instance.Show<Char.View.CharPanel>(CavasType.SpecialUI);
    }

    private void UpdateName(bool show,string name)
    {
        m_title.UpdateName(show,name);
    }
}

