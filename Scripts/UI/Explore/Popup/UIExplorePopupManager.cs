using UnityEngine;
using System.Collections;

public class UIExplorePopupManager : MonoBehaviour
{
    private GameObject combatItemRewards;
    private GameObject exploreWayponitStart;
    private GameObject mapExploreFinish;
    private GameObject combatEventPopup;
    private GameObject combatEndPopup;
    private GameObject exploreEventPopup;
    private GameObject itemMovePopup;
    private GameObject eventIntro;
    private GameObject bagPopup;


    public void Init()
    {
        AddComponent();
        //
        RegisterPopupObj();
        //
        itemMovePopup.gameObject.SetActive(true);
    }


    private void AddComponent()
    {
     
        combatItemRewards = transform.Find("CombatItemRewards").gameObject.AddComponent<UICombatItemRewards>().gameObject;
        exploreWayponitStart = transform.Find("WayponitStart").gameObject.GetComponent<UIWayponitStart>().gameObject;
        mapExploreFinish = transform.Find("MapExploreFinish").gameObject.AddComponent<UIMapExploreFinish>().gameObject;
        combatEventPopup = transform.Find("CombatEventPopup").gameObject.AddComponent<UICombatEventPopup>().gameObject;
      //  combatEndPopup = transform.Find("CombatEndMask").gameObject.AddComponent<UICombatEndPopup>().gameObject;
        exploreEventPopup = transform.Find("ExploreEventPopup").gameObject.AddComponent<UIExploreEventPopup>().gameObject;
        itemMovePopup = transform.Find("ItemMovePopup").gameObject.AddComponent<UIExploreItemMove>().gameObject;
        eventIntro = transform.Find("EventIntro").gameObject.AddComponent<UIEventIntroPopup>().gameObject;
        bagPopup = transform.Find("BagPopup").gameObject.AddComponent<UIExploreBagPopup>().gameObject;
    }

    private void RegisterPopupObj()
    {
        GameModules.popupSystem.RegisterPopupObj(ModuleName.combatItemRewards, combatItemRewards);
        GameModules.popupSystem.RegisterPopupObj(ModuleName.exploreWayponitStart, exploreWayponitStart);
        GameModules.popupSystem.RegisterPopupObj(ModuleName.mapExploreFinish, mapExploreFinish);
        GameModules.popupSystem.RegisterPopupObj(ModuleName.combatEventPopup, combatEventPopup);
    //    GameModules.popupSystem.RegisterPopupObj(ModuleName.combatEndPopup, combatEndPopup);
        GameModules.popupSystem.RegisterPopupObj(ModuleName.exploreEventPopup, exploreEventPopup);
        GameModules.popupSystem.RegisterPopupObj(ModuleName.itemMovePopup, itemMovePopup);
        GameModules.popupSystem.RegisterPopupObj(ModuleName.eventIntroPopup, eventIntro);
        GameModules.popupSystem.RegisterPopupObj(ModuleName.bagPopup, bagPopup);
    }

}
