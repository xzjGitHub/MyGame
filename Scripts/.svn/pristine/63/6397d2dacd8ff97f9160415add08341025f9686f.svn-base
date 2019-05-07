using System.Collections;

public class StartToFront: IChangeScene
{
    public override void Action(object obj)
    {
        LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
        loding.PlayOpenAnimAction = () =>
        {
            DiaLogPanel dia = UIPanelManager.Instance.Show<DiaLogPanel>();
            dia.CloseCallBack = StartLoadFront;
        };
        loding.PlayCloseAnim();

        loding.Close();
    }

    private void StartLoadFront()
    {
        LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
        loding.action = () =>
        {
            Game.Instance.StartCoroutine(LoadFront());
        };
        loding.PlayCloseAnim();
    }

    private IEnumerator LoadFront()
    {

        var mapID = ScriptSystem.Instance.ScriptTemplate.initialMap;
        //
        var zone = FortSystem.Instance.NewZone;
        FortSystem.Instance.PrepareExplore(new ExploreMap(mapID,0,0,zone));
        yield return null;
        SceneManagerUtil.LoadScene(SceneType.Fight,false);
        yield return null;

        LodingPanel loding = UIPanelManager.Instance.GetUiPanelBehaviour<LodingPanel>();
        loding.Close();
    }
}

