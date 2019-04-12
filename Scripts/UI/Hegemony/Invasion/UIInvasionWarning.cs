using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 入侵警告界面
/// </summary>
public class UIInvasionWarning : MonoBehaviour
{
    public delegate void CallBack();

    public CallBack OnCallBack;
    //
    public float maxTime = 10f;
    public bool startMove;
    //
    private Transform progress;
    private RectTransform startTransform;
    private Image progressValueImage;
    private Text timeText;
    private Button npcButton;
    private Button backButton;
    //
    private float nowTime;
    private float progressValue;
    private float maxX;
    private float tempAspd;
    private float tempX;
    private float time;
    private string timeStr = "剩余{0}天";
    //
    private bool isFirst;
    //
    private UIInvasionTeamInfo invasionTeamInfo;
    private CycleInvasionSystem cycleInvasionSystem;

    public void OpenUI(CycleInvasionSystem _cycleInvasionSystem)
    {
        Init();
        //
        cycleInvasionSystem = _cycleInvasionSystem;
        maxTime = cycleInvasionSystem.WarningTime;
        nowTime = TimeUtil.GetPlayDays() - cycleInvasionSystem.WarningStartTime;
        //
        gameObject.SetActive(true);
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        GetObj();
        progressValue = 0;
        maxX = progressValueImage.GetComponent<RectTransform>().sizeDelta.x;
        nowTime = 0;
    }

    private void UpdateMove()
    {
        if (!startMove) return;
        nowTime += Time.deltaTime;
        timeText.text = string.Format(timeStr, (int)(maxTime - nowTime));
        tempAspd = 1f / maxTime * Time.deltaTime;
        tempX = (maxX / maxTime) * Time.deltaTime;
        progressValue += tempAspd;
        progressValue = Math.Min(1, progressValue);
        progressValueImage.fillAmount = progressValue;
        if (progressValue >= 1)
        {
            startMove = false;
        }
        startTransform.anchoredPosition += Vector2.right * tempX;

    }


    private void OnClickNpc()
    {
        invasionTeamInfo.OpenUI(cycleInvasionSystem);
        transform.localScale=Vector3.zero;
        gameObject.SetActive(false);
    }
    private void OnClickBack()
    {
        gameObject.SetActive(false);
        if (OnCallBack!=null)
        {
            OnCallBack();
        }

    }
    private void OnClallInvasionTeamBack()
    {
        transform.localScale = Vector3.one;
    }



    // Update is called once per frame
    void Update()
    {
        UpdateMove();
    }

    private void GetObj()
    {
        if (isFirst)return;
        //
        invasionTeamInfo = transform.Find("TeamInfoPop").gameObject.AddComponent<UIInvasionTeamInfo>();
        invasionTeamInfo.OnCallBack = OnClallInvasionTeamBack;
        backButton = transform.Find("Back").GetComponent<Button>();
        backButton.onClick.AddListener(OnClickBack);
        //
        progress = transform.Find("Progress");
        //
        startTransform = progress.Find("Npc").GetComponent<RectTransform>();
        timeText = startTransform.transform.Find("Text").GetComponent<Text>();
        npcButton = startTransform.transform.Find("Button").GetComponent<Button>();
        npcButton.onClick.AddListener(OnClickNpc);
        //
        progressValueImage = progress.Find("Value").GetComponent<Image>();
        //
        isFirst = true;
    }

}
