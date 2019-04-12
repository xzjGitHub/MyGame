using System;
using System.Collections;
using GameEventDispose;
using UnityEngine;
using UnityEngine.UI;


public class UIInvasionDirection : MonoBehaviour
{


    private void Start()
    {
        if (isFirst) return;
        //
        GetObj();
        EventDispatcher.Instance.InvasionEvent.AddEventListener<CycleInvasionPhase, object>(EventId.InvasionEvent, OnInvasionEvent);
        //
        isFirst = true;
    }

    /// <summary>
    /// 更新进度
    /// </summary>
    /// <param name="nowTime"></param>
    /// <param name="sumTime"></param>
    private void UpdateProgress(float nowTime, float sumTime)
    {
        if (!startObj.activeInHierarchy) startObj.SetActive(true);
        if (warningButton.gameObject.activeInHierarchy) warningButton.gameObject.SetActive(false);
        timeText.text = string.Format(timeStr, sumTime - nowTime);
        //
        enemyRT.anchoredPosition = Vector2.right * (MoveMaxX / sumTime) * nowTime;
    }


    private void OnClickWaening()
    {
        if (isWarningShow) return;
        if (IEWarningText != null) IEWarningText.Stop();
        IEWarningText = new CoroutineUtil(IEWarningTextShow());
    }

    /// <summary>
    /// 警告文字显示
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEWarningTextShow()
    {
        isWarningShow = true;
        warningCanvas.alpha = 1;
        while (warningCanvas.alpha > 0)
        {
            if (warningCanvas.alpha - Time.deltaTime / 2f < 0)
            {
                warningCanvas.alpha = 0;
                break;
            }
            warningCanvas.alpha -= Time.deltaTime / 2f;
            yield return null;
        }
        isWarningShow = false;
    }

    /// <summary>
    /// 更新入侵显示
    /// </summary>
    /// <param name="type"></param>
    private void UpdateInvasionBeforeStartingShow(InvasionShowType type)
    {
        showType = type;
        int id = 0;
        switch (type)
        {
            case InvasionShowType.Nul:
                return;
            case InvasionShowType.Phase1:
                id = 3001;
                break;
            case InvasionShowType.Phase2:
                id = 3002;
                break;
            case InvasionShowType.Phase3:
                id = 3003;
                break;
            case InvasionShowType.Phase4:
                id = 3004;
                break;

        }
        try
        {
            warningText.text = Text_templateConfig.GetText_config(id).text;
            if (startObj.activeInHierarchy) startObj.SetActive(false);
            if (!warningButton.gameObject.activeInHierarchy) warningButton.gameObject.SetActive(true);
        }
        catch (Exception e)
        {
            return;
        }

    }

    private void OnInvasionEvent(CycleInvasionPhase arg1, object arg2)
    {
        if (!gameObject.activeInHierarchy) return;

        switch (arg1)
        {
            case CycleInvasionPhase.Idle:
                break;
            case CycleInvasionPhase.Preposition:
                break;
            case CycleInvasionPhase.Warning:
                break;
            case CycleInvasionPhase.Siege:
                break;
            case CycleInvasionPhase.SiegeEnd:
                break;
            case CycleInvasionPhase.InvasionEnd:
                break;
            case CycleInvasionPhase.Updateing:
                UpdateProgress((float)arg2, CycleInvasionSystem.Instance.WarningTime);
                break;
            case CycleInvasionPhase.BeforeStarting:
                if (showType != (InvasionShowType)arg2)
                {
                    UpdateInvasionBeforeStartingShow((InvasionShowType)arg2);
                    if ((InvasionShowType)arg2 != InvasionShowType.Phase1) OnClickWaening();
                }
                break;
        }
    }




    private void OnDestroy()
    {
        EventDispatcher.Instance.InvasionEvent.RemoveEventListener<CycleInvasionPhase, object>(EventId.InvasionEvent, OnInvasionEvent);
    }



    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        var bg = transform.Find("Bg");
        startObj = bg.Find("Start").gameObject;
        introObj = startObj.transform.Find("Intro").gameObject;
        enemyImage = startObj.transform.Find("Enemy").GetComponent<Image>();
        enemyRT = enemyImage.GetComponent<RectTransform>();
        timeText = startObj.transform.Find("Time").GetComponent<Text>();
        warningButton = bg.Find("Warning").GetComponent<Button>();
        warningText = warningButton.transform.Find("Text").GetComponent<Text>();
        warningCanvas = warningText.GetComponent<CanvasGroup>();
        //
        warningButton.onClick.AddListener(OnClickWaening);
    }

    //
    private CoroutineUtil IEWarningText;
    private InvasionShowType showType = InvasionShowType.Nul;
    private const float MoveMaxX = 500;
    private bool isFirst;
    private bool isWarningShow;
    //
    private const string timeStr = "距离敌人到达尚余 <color=#ff0000>{0}</color> 日";
    //
    private GameObject startObj;
    private GameObject introObj;
    private Image enemyImage;
    private Text timeText;
    private Button warningButton;
    private Text warningText;
    private RectTransform enemyRT;
    private CanvasGroup warningCanvas;
}
