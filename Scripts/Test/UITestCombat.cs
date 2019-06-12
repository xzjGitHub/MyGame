﻿using System.Collections.Generic;
using System.Linq;
using GameEventDispose;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class UITestCombat : MonoBehaviour
{

    public SkeletonAnimation animation;
    //
    public List<InputField> leftInputFields = new List<InputField>();
    public List<InputField> rightInputFields = new List<InputField>();
    public List<InputField> leftSkillID = new List<InputField>();
    public List<InputField> rightSkillID = new List<InputField>();
    //
    public InputField leftCSYS;
    public InputField leftCSYS1;
    public InputField leftCSYS2;
    public InputField rightCSYS;
    public InputField rightCSYS1;
    public InputField rightCSYS2;
    //
    public List<InputField> leftShileds = new List<InputField>();
    public List<InputField> leftArmors = new List<InputField>();
    public List<InputField> rightShileds = new List<InputField>();
    public List<InputField> rightArmors = new List<InputField>();


    public void OnClick111()
    {
        SkeletonTool.PlayAnimation(animation, "Atk_chongci1", false);
        // SkeletonTool.AnimationPlayTest(animation);
    }

    public void OnClickTest()
    {
        SkeletonTool.CreateSpine("Anima_6001");
    }

    private void Start()
    {
        LogHelper_MC.Log("00000000000000000000");
        charSystem = new CharSystem();
        //
        Init();
        skillUI.SetActive(isSkillTest);
        npcObj.SetActive(isNpc);
        npcTest.gameObject.SetActive(false);
    }


    private void Update()
    {
        Test();
    }

    public Transform A;
    public Transform B;
    public Transform C;
    public bool isMove;
    public float moveTime;
    private bool isInit;
    public float time;
    public float x;
    private void Test()
    {
        if (isMove && !isInit)
        {
            A.localPosition = Vector3.zero;
            B.localPosition = Vector3.right * 1000;
            x = Vector3.Distance(A.localPosition, B.localPosition);
            isInit = true;
            time = 0;
        }
        if (!isMove)
        {
            return;
        }
        if (Vector3.Magnitude(A.localPosition - C.localPosition) < 1)
        {
            isMove = false;
            isInit = false;
            return;
        }

        A.localPosition = Vector3.MoveTowards(A.localPosition, C.localPosition, x / moveTime * Time.deltaTime);
        B.localPosition = Vector3.MoveTowards(B.localPosition, C.localPosition, x / moveTime * Time.deltaTime);
        time += Time.deltaTime;
    }

    private void Init()
    {
        ui = transform.Find("UI").gameObject;
        openButton = transform.Find("Open").GetComponent<Button>();
        closeButton = transform.Find("Close").GetComponent<Button>();
        startCombat = transform.Find("StartCombat").GetComponent<Button>();
        createButton = ui.transform.Find("Create").GetComponent<Button>();
        skillTest = transform.Find("SkillTest").GetComponent<Button>();
        skillUI = transform.Find("Skillid").gameObject;
        npcTest = transform.Find("NpcTest").GetComponent<Button>();
        npcObj = transform.Find("Npc").gameObject;
        //
        openButton.onClick.AddListener(Open);
        closeButton.onClick.AddListener(Close);
        startCombat.onClick.AddListener(OnClickStartCombat);
        createButton.onClick.AddListener(CreateCombat);
        skillTest.onClick.AddListener(OnSkillTest);
        npcTest.onClick.AddListener(OnClickNpc);
    }

    private void Open()
    {
        openButton.gameObject.SetActive(false);
        ui.SetActive(true);
        closeButton.gameObject.SetActive(true);
    }

    private void Close()
    {
        closeButton.gameObject.SetActive(false);
        ui.SetActive(false);
        openButton.gameObject.SetActive(true);
    }

    private void OnSkillTest()
    {
        isSkillTest = !isSkillTest;
        skillUI.SetActive(isSkillTest);
    }

    private void OnClickNpc()
    {
        isNpc = !isNpc;
        npcObj.SetActive(isNpc);
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    private void OnClickStartCombat()
    {
        leftSkill.Clear();
        rightSkill.Clear();
        int temp = 0;
        foreach (InputField item in leftSkillID)
        {
            temp = GetInputFieldsValue(item);
            if (temp != 0)
            {
                leftSkill.Add(temp);
            }
        }
        foreach (InputField item in rightSkillID)
        {
            temp = GetInputFieldsValue(item);
            if (temp != 0)
            {
                rightSkill.Add(temp);
            }
        }

        if (isSkillTest)
        {
            if (leftSkill.All(a => a == 0) || rightSkill.All(a => a == 0))
            {
                return;
            }
        }
        startCombat.gameObject.SetActive(false);
        if (combatManager != null)
        {
            int index = 0;
            foreach (UICombatTeam team in combatManager.CombatTeams)
            {
                foreach (UICharUnit charUnit in team.charUnits)
                {
                    charUnit.ActionOperation.erCSYS = new List<float> { GetInputFieldsValue(index == 0 ? leftCSYS : rightCSYS) };
                    charUnit.erCSYS = new List<float> { GetInputFieldsValue(index == 0 ? leftCSYS1 : rightCSYS1), GetInputFieldsValue(index == 0 ? leftCSYS2 : rightCSYS2) };
                }
                index++;
            }
        }
        //
        List<int> shileds = GetInputFieldsValues(leftShileds);
        List<int> armors = GetInputFieldsValues(leftArmors);
        foreach (CombatUnit item in combatSystem.PlayerTeamInfo.combatUnits)
        {
            item.TestSetShiled(shileds[item.initIndex]);
            item.TestSetArmor(armors[item.initIndex]);
        }
        //
        shileds = GetInputFieldsValues(rightShileds);
        armors = GetInputFieldsValues(rightArmors);
        foreach (CombatUnit item in combatSystem.EnemyTeamInfo.combatUnits)
        {
            item.TestSetShiled(shileds[item.initIndex]);
            item.TestSetArmor(armors[item.initIndex]);
        }
        //
        combatUiOperation.Init(true);
        combatManager = combatUiOperation.CombatManager;
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.StartRound, (object)new object[] { isSkillTest, leftSkill, rightSkill });
    }

    private CombatTeamInfo playerInfo, enemyInfo;

    private void InitCombt()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatStageEvent);
        EventDispatcher.Instance.CombatEvent.AddEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatStageEvent);
        combatSystem = new CombatSystem();
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateCombat, (object)new CombatTeamInfo[] { playerInfo, enemyInfo });
    }
    /// <summary>
    /// 创建战斗
    /// </summary>
    private void CreateCombat()
    {
        //
        if (combatUiOperation != null)
        {
            Destroy(combatUiOperation.gameObject);
        }

        foreach (InputField item in leftSkillID)
        {
            item.text = "";
        }
        foreach (InputField item in rightSkillID)
        {
            item.text = "";
        }
        //
        GetCharIds(leftInputFields, leftCharIds);
        GetCharIds(rightInputFields, rightCharIds);
        if (leftCharIds.Count == 0 || rightCharIds.Count == 0)
        {
            return;
        }
        //
        Close();
        Char_template template;
        //创建玩家
        List<CombatUnit> _leftoCmbatUnits = new List<CombatUnit>();
        for (int i = 0; i < leftCharIds.Count; i++)
        {
            int charId = leftCharIds[i];
            template = Char_templateConfig.GetTemplate(charId);
            _leftoCmbatUnits.Add(template == null
                ? new CombatUnit(new MobAttribute(new CharCreate(leftCharIds[i])), i)
                : new CombatUnit(CharSystem.Instance.CreateChar(new CharCreate(leftCharIds[i]), false), i));
            _leftoCmbatUnits.Last().charAttribute.charID = i * 10;
        }
        //初始化左边
        //   EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, PlayCombatStage.InitLeft, true, (object)_leftoCmbatUnits);
        playerInfo = new CombatTeamInfo(0, TeamType.Player, _leftoCmbatUnits);
        //创建Npc
        List<CombatUnit> _rightCombatUnit = new List<CombatUnit>();
        for (int i = 0; i < rightCharIds.Count; i++)
        {
            int charId = rightCharIds[i];
            template = Char_templateConfig.GetTemplate(charId);
            _rightCombatUnit.Add(template == null
                ? new CombatUnit(new MobAttribute(new CharCreate(rightCharIds[i])), i)
                : new CombatUnit(CharSystem.Instance.CreateChar(new CharCreate(rightCharIds[i]), false), i));
            _rightCombatUnit.Last().charAttribute.charID = i * 10 + 5;
        }
        enemyInfo = new CombatTeamInfo(1, TeamType.Enemy, _rightCombatUnit);
        //
        InitCombt();
        //
        combatUiOperation = ResourceLoadUtil.LoadCombatModule();
        combatUiOperation.transform.localPosition += Vector3.down * 0.48f;
        //
        combatUiOperation.Init(true);
        combatManager = combatUiOperation.CombatManager;
    }

    private void GetCharIds(List<InputField> _inputFields, List<int> _chars)
    {
        _chars.Clear();
        foreach (InputField item in _inputFields)
        {
            int temp = GetInputFieldsValue(item);
            if (temp == 0)
            {
                continue;
            }

            _chars.Add(int.Parse(item.text));
        }
    }

    private List<int> GetInputFieldsValues(List<InputField> _inputFields)
    {
        List<int> values = new List<int>();
        foreach (InputField item in _inputFields)
        {
            values.Add(GetInputFieldsValue(item));
        }

        return values;
    }

    private int GetInputFieldsValue(InputField inputField)
    {
        return inputField.text.Length == 0 ? 0 : int.Parse(inputField.text);
    }


    /// <summary>
    /// 战斗阶段事件
    /// </summary>
    private void OnCombatStageEvent(PlayCombatStage arg1, object arg2)
    {
        switch (arg1)
        {
            case PlayCombatStage.CreateCombat:
                break;
            case PlayCombatStage.Opening:
                break;
            case PlayCombatStage.CombatPrepare:
                break;
            case PlayCombatStage.CreateRound:
                startCombat.gameObject.SetActive(true);
                break;
            case PlayCombatStage.ChooseSkill:
                break;
            case PlayCombatStage.ImmediateSkillEffect:
                break;
            case PlayCombatStage.RoundInfo:
                startCombat.gameObject.SetActive(false);
                break;
            case PlayCombatStage.CombatEnd:
                break;
            case PlayCombatStage.InitLeft:
                break;
            case PlayCombatStage.InitRight:
                break;
            case PlayCombatStage.InitRes:
                break;
            case PlayCombatStage.PlayRoundInfoEnd:
                startCombat.gameObject.SetActive(true);
                break;
        }
    }
    //
    private Button npcTest;
    private GameObject npcObj;
    private List<int> leftSkill = new List<int>();
    private List<int> rightSkill = new List<int>();
    private Button skillTest;
    private GameObject skillUI;
    private bool isSkillTest;
    private bool isNpc;
    private Button openButton;
    private Button closeButton;
    private Button createButton;
    private GameObject ui;
    private CombatSystem combatSystem;
    private List<int> leftCharIds = new List<int>();
    private List<int> rightCharIds = new List<int>();
    //
    private Button startCombat;
    //
    private CharSystem charSystem;

    private UICombatUIOperation combatUiOperation;
    private CombatManager combatManager;
}
