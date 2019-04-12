using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFortExploreMapPopup : MonoBehaviour
{
    public delegate void CallBack();
    public CallBack OnClose;
    public CallBack OnStart;

    public void OpenUI(ExploreMap exploreMap, float time)
    {
        GetObj();
        //
        this.exploreMap = exploreMap;
        //
        timeName.text = string.Format(TimeIntroStr, exploreMap.TimeLeft(time));
        //
        gameObject.SetActive(true);
    }

    public void UpdateTimeShow(float time)
    {
        if (!gameObject.activeInHierarchy) return;
        timeName.text = string.Format(TimeIntroStr, exploreMap.TimeLeft(time));
    }

    private void OnClickClose()
    {
        gameObject.SetActive(false);
        if (OnClose != null) OnClose();
        OnClose = null;
    }

    private void OnClickStart()
    {
        gameObject.SetActive(false);
        if (OnStart != null) OnStart();
        OnStart = null;
    }


    private void GetObj()
    {
        if (isFirst) return;
        starObj = transform.Find("Star").gameObject;
        icon = transform.Find("Icon").GetComponent<Image>();
        tagName = transform.Find("Tag").GetComponent<Text>();
        timeName = transform.Find("LeftTime").GetComponent<Text>();
        taskName = transform.Find("Task/Name").GetComponent<Text>();
        level = transform.Find("Level").GetComponent<Text>();
        eventName = transform.Find("EventName").GetComponent<Text>();
        closeButton = transform.Find("Close").GetComponent<Button>();
        startButton = transform.Find("Start").GetComponent<Button>();
        //
        closeButton.onClick.AddListener(OnClickClose);
        startButton.onClick.AddListener(OnClickStart);
        //
        isFirst = true;
    }
    //
    private GameObject starObj;
    private Image icon;
    private Text tagName;
    private Text timeName;
    private Text taskName;
    private Text level;
    private Text eventName;
    private Button closeButton;
    private Button startButton;
    //
    private ExploreMap exploreMap;
    //
    private bool isFirst;
    private const string TimeIntroStr = "剩余时间：<color=#fffff>{0} </color>天";
}
