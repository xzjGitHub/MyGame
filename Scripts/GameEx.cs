using UnityEngine;

public partial class Game
{
    private void InitComponent()
    {
        UIPanelManager.Instance.PopUiParent = transform.Find("PopUICamera/PopUI");
        UIPanelManager.Instance.ThreeUIParent = transform.Find("3DCamera/3DUI");
        UIPanelManager.Instance.SpeicalUIParent = transform.Find("SpecialUI/SpecialUI");

        Utility.RequireComponent<RapidBlurEffectEx>(transform.Find("3DCamera").gameObject);
        Utility.RequireComponent<QuitListen>(transform.Find("PopUICamera/PopUI/QuitTip").gameObject);
    }

    private void ScreenSet()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
