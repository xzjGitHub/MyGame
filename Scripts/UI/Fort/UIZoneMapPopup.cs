using System;
using UnityEngine;
using UnityEngine.UI;

public class UIZoneMapPopup : MonoBehaviour
{
    public Action<int> OnClickMap;
    public Action<int> OnClickFort;

    public void OpenUI(int mapID)
    {
        bool isUpdate = mapID != this.mapID;
        this.mapID = mapID;
        //
        GetObj();
        //
        UpdateShow(isUpdate);
    }


    private void UpdateShow(bool isUpdate)
    {
        if (!isUpdate)
        {
            gameObject.SetActive(true);
            UpdateFortShow(isUpdate);
            return;
        }
        Text_template textTemplate = null;
        Map_template mapTemplate = Map_templateConfig.GetMap_templat(mapID);

        if (mapTemplate != null)
        {
            fortID = mapTemplate.unlockFort;
            mapName.text = mapTemplate.mapName;
            mapIcon.sprite = ResourceLoadUtil.LoadMapSprite(mapTemplate.mapIcon);
            textTemplate = Text_templateConfig.GetText_config(mapTemplate.mapText);
            mapIntro.text = textTemplate != null ? textTemplate.text : String.Empty;
        }
        UpdateFortShow(isUpdate);

        gameObject.SetActive(true);
    }


    private void UpdateFortShow(bool isUpdate)
    {
        Text_template textTemplate = null;
        Fort_template fortTemplate = Fort_templateConfig.GetFort_template(fortID);
        if (fortTemplate != null)
        {
            if (isUpdate)
            {
                fortName.text = fortTemplate.fortName;
                fortIncon.sprite = ResourceLoadUtil.LoadFortSprite(fortTemplate.fortIcon);
                textTemplate = Text_templateConfig.GetText_config(fortTemplate.fortText);
                fortIntro.text = textTemplate!=null ? textTemplate.text : String.Empty;
            }
            frot.gameObject.SetActive(true);
            ExploreMap exploreMap = FortSystem.Instance.NewZone.GetMap(mapID);
            if (exploreMap != null)
            {
                fortButton.enabled = exploreMap.IsUnLockFort;
                fortLock.SetActive(!exploreMap.IsUnLockFort);
            }
        }
        else
        {
            frot.gameObject.SetActive(false);
        }
    }

    private void ClickMap()
    {
        if (OnClickMap != null)
        {
            OnClickMap(mapID);
        }

        OnClickMap = null;
    }

    private void ClickFort()
    {
        if (OnClickFort != null)
        {
            OnClickFort(fortID);
        }

        OnClickFort = null;
    }

    private void ClickMask()
    {
        gameObject.SetActive(false);
    }

    private void GetObj()
    {
        if (isFirst)
        {
            return;
        }
        //
        Transform bg = transform.Find("Bg");
        map = bg.Find("Map");
        mapIntro = map.Find("Intro/Text").GetComponent<Text>();
        mapName = map.Find("Name/Text").GetComponent<Text>();
        mapIcon = map.Find("Icon").GetComponent<Image>();
        mapButton = mapIcon.GetComponent<Button>();
        //
        mapButton.onClick.AddListener(ClickMap);
        //
        frot = bg.Find("Fort");
        fortIntro = frot.Find("Intro/Text").GetComponent<Text>();
        fortName = frot.Find("Name/Text").GetComponent<Text>();
        fortLock = frot.Find("Mask").gameObject;
        fortIncon = frot.Find("Icon").GetComponent<Image>();
        fortButton = fortIncon.GetComponent<Button>();
        //
        fortButton.onClick.AddListener(ClickFort);
        //
        maskButton = transform.GetComponent<Button>();
        maskButton.onClick.AddListener(ClickMask);
        //
        isFirst = true;
    }

    //
    private Transform map;
    private Transform frot;
    private Button maskButton;
    private Button mapButton;
    private Button fortButton;
    private Text mapIntro;
    private Text fortIntro;
    private Text mapName;
    private Text fortName;
    private Image mapIcon;
    private Image fortIncon;
    private GameObject fortLock;
    //
    private bool isFirst;
    private int mapID;
    private int fortID;
}
