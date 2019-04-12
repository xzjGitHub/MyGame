using GameEventDispose;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICombatTest : UIPanelBehaviour
{
    private Button closeButton;
    private Button startButton;
    private InputField combatCount;
    private List<InputField> myTeams;
    private List<InputField> enemyTeam;
    private Text introText;
    private Text errorText;
    //
    private const string introStr = "战斗测试结果：胜利{0}场，失败{1}场";
    private const string errorStr1 = "角色ID输入错误,数据库无ID={0}的角色";
    private const string errorStr2 = "我方战队有效角色为空";
    private const string errorStr3 = "敌方战队有效角色为空";
    //
    private List<int> myTeamCharIds = new List<int>();
    private List<int> enemyTeamCharIds = new List<int>();
    private int combatCountNum;
    private int winCount;
    private List<CombatUnit> palyerList = new List<CombatUnit>();
    private List<CombatUnit> enemyList = new List<CombatUnit>();
    private List<CombatRound> combatRounds = new List<CombatRound>();
    //
    private bool isFirst;
    private bool isLastCombat;
    private bool isEnd;
    //
    private CombatSystem combatSystem;
    private CombatTeamInfo playerTeamInfo;
    private CombatTeamInfo enemyTeamInfo;
    private CombatResult combatResult;
    //
    private CoroutineUtil errorCoroutineUtil;


    protected override void OnShow(List<System.Object> parmers = null)
    {
        Init();
        //
        UIPanelManager.Instance.Hide<NewMainPanel>();
        //
        introText.text = String.Empty;

    }

    private void OnClickClose()
    {
        UIPanelManager.Instance.Show<NewMainPanel>(CavasType.Three);
        UIPanelManager.Instance.Hide<UICombatTest>();
    }

    private void OnClickStartCombat()
    {
        winCount = 0;
        if (combatCount.text == string.Empty) combatCount.text = "1";
        combatCountNum = int.Parse(combatCount.text);
        //
        UpdateTeamInfo();
        UpdateCombatUnitInfo();
        //
        if (palyerList.Count <= 0)
        {
            if (errorCoroutineUtil != null) errorCoroutineUtil.Stop();
            errorCoroutineUtil = new CoroutineUtil(IECloseIntro(errorStr2));
            return;
        }
        if (enemyList.Count <= 0)
        {
            if (errorCoroutineUtil != null) errorCoroutineUtil.Stop();
            errorCoroutineUtil = new CoroutineUtil(IECloseIntro(errorStr3));
            return;
        }
        //
        startButton.enabled = false;
        StartCoroutine(StartCombat());
    }
    /// <summary>
    /// 开始战斗
    /// </summary>
    /// <returns></returns>
    IEnumerator StartCombat()
    {
        combatRounds.Clear();
        //开始战斗
        for (int i = 0; i < combatCountNum; i++)
        {
            isLastCombat = i == combatCountNum - 1;
            yield return CreateCombat();
        }
        //
        introText.text = string.Format(introStr, winCount, combatCountNum - winCount);
        //
        GameDataManager.SaveCombatTestData<CombatTestData>(new CombatTestData { combatRounds = combatRounds });
        //
        startButton.enabled = true;
    }

    /// <summary>
    /// 创建战斗
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateCombat()
    {
        EventDispatcher.Instance.CombatEvent.AddEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
        //
        combatSystem = new CombatSystem();
        UpdateCombatUnitInfo();
        playerTeamInfo = new CombatTeamInfo(0, TeamType.Player, palyerList);
        enemyTeamInfo = new CombatTeamInfo(1, TeamType.Enemy, enemyList);
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateCombat,
            (object)new CombatTeamInfo[] { playerTeamInfo, enemyTeamInfo });
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateRound, (object)null);
        while (!isEnd)
        {
            yield return null;
        }
    }

    IEnumerator IECloseIntro(string _info)
    {
        errorText.text = _info;
        errorText.gameObject.SetActive(true);
        float _time = 0;
        yield return null;
        while (_time <= 1.5f)
        {
            _time += Time.deltaTime;
            yield return null;
        }
        errorText.gameObject.SetActive(false);
    }

    private void OnCombatEvent(PlayCombatStage arg1, object arg2)
    {
        switch (arg1)
        {
            case PlayCombatStage.CreateCombat:
                isEnd = false;
                break;
            case PlayCombatStage.CreateRound:
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.StartRound, (object)null);
                break;
            case PlayCombatStage.RoundInfo:
                combatResult = ((CombatRound)arg2).combatResult;
                if (isLastCombat)
                {
                    combatRounds.Add(new CombatRound { combatRoundResults = ((CombatRound)arg2).combatRoundResults });
                }
                if (!combatResult.isEnd)
                {
                    EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateRound, (object)null);
                    return;
                }
                if (combatResult.victoryTeam == 0) winCount++;
                EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CombatEnd, (object)null);
                break;
            case PlayCombatStage.CombatEnd:
                isEnd = true;
                EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatEvent);
                break;
        }
    }

    private void UpdateTeamInfo()
    {
        myTeamCharIds.Clear();
        enemyTeamCharIds.Clear();
        //
        foreach (var item in myTeams)
        {
            if (item.text != string.Empty)
            {
                myTeamCharIds.Add(int.Parse(item.text));
            }
        }
        foreach (var item in enemyTeam)
        {
            if (item.text != string.Empty)
            {
                enemyTeamCharIds.Add(int.Parse(item.text));
            }
        }

    }

    private void UpdateCombatUnitInfo()
    {
        palyerList.Clear();
        enemyList.Clear();
        //
        foreach (var item in myTeamCharIds)
        {
            if (CharSystem.Instance.GetCharAttribute(item) != null)
            {
                palyerList.Add(new CombatUnit(CharSystem.Instance.GetCharAttribute(item), palyerList.Count));
            }
        }
        foreach (var item in enemyTeamCharIds)
        {
            if (CharSystem.Instance.GetCharAttribute(item) != null)
            {
                enemyList.Add(new CombatUnit(CharSystem.Instance.GetCharAttribute(item), palyerList.Count));
            }
        }
    }

    private void Init()
    {
        if (isFirst) return;
        GetObj();
        //
        isFirst = true;
    }

    private void GetObj()
    {
        errorText = transform.Find("Error").GetComponent<Text>();
        closeButton = transform.Find("Close").GetComponent<Button>();
        startButton = transform.Find("Left/StartCombat").GetComponent<Button>();
        //
        closeButton.onClick.AddListener(OnClickClose);
        startButton.onClick.AddListener(OnClickStartCombat);
        //
        combatCount = transform.Find("Left/CombatCount/InputField").GetComponent<InputField>();
        introText = transform.Find("Right/Intro").GetComponent<Text>();
        //
        myTeams = new List<InputField>();
        enemyTeam = new List<InputField>();
        //
        foreach (Transform item in transform.Find("Left/MyTeam"))
        {
            if (item.name == "Lable") continue;
            myTeams.Add(item.Find("InputField").GetComponent<InputField>());
            myTeams.Last().onEndEdit.AddListener(OnEndEdit);
        }
        foreach (Transform item in transform.Find("Left/EnemyTeam"))
        {
            if (item.name == "Lable") continue;
            enemyTeam.Add(item.Find("InputField").GetComponent<InputField>());
            enemyTeam.Last().onEndEdit.AddListener(OnEndEdit);
        }
    }

    private void OnEndEdit(string arg0)
    {
        if (arg0 == String.Empty) return;
        int _id = int.Parse(arg0);
        if (CharSystem.Instance.GetCharAttribute(_id) == null)
        {
            if (errorCoroutineUtil != null) errorCoroutineUtil.Stop();
            errorCoroutineUtil = new CoroutineUtil(IECloseIntro(string.Format(errorStr1, _id)));
        }
    }
}

public class CombatTestData
{
    public List<CombatRound> combatRounds = new List<CombatRound>();
}