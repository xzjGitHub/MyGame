﻿using UnityEngine;

class BackToStartScene: IChangeScene
{
    public override void Action(object obj)
    {
        UIPanelManager.Instance.DestroyAllPanelNotContain(null);
        UIPanelManager.Instance.Hide<NewMainPanel>();
        UIPanelManager.Instance.Show<StartPanel>();
        Resources.UnloadUnusedAssets();
    }
}

