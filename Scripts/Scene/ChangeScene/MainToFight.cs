using System.Collections.Generic;
using UnityEngine;

public class MainToFight: IChangeScene
{
    public override void Action(object obj)
    {
        bool showLoad = obj == null ? true : (bool)obj;
        if(showLoad)
        {
            LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.action = () => { ResourceLoadUtil.LoadExploreModule1(); SceneChange(); };
            loding.PlayCloseAnim();
        }
        else
        {
            ResourceLoadUtil.LoadExploreModule1();
            UIPanelManager.Instance.DestroyAllPanelNotContain(new List<string>() { "LodingPanel" });
            Resources.UnloadUnusedAssets();
        }
    }

    private void SceneChange()
    {
        GameObjectPool.Instance.DeatroyAllPool();
        PlayerPool.Instance.Clear();
        ItemCostFactory.Instance.Clear();
        SpriteManager.Instance.FreeAll();
        UIEffectFactory.Instance.FreeAll(true);
        UIPanelManager.Instance.DestroyAllPanelNotContain(new List<string>() { "LodingPanel" });

        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}

