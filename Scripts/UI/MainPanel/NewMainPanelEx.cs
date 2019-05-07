﻿using System;
using Bag;

public partial class NewMainPanel
{
    public Action BackAction;

    private void ClickAdventure()
    {
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
        UIPanelManager.Instance.Show<BagPanel>(CavasType.PopUI);
    }

    private void ClickChar()
    {
        UIPanelManager.Instance.Show<Char.View.CharPanel>(CavasType.PopUI);
    }

    private void UpdateName(bool show,string name)
    {
        m_title.UpdateName(show,name);
    }
}

