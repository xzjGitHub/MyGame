using System;
using System.Collections.Generic;
using System.Linq;
using Char.View;
using UnityEngine;
using UnityEngine.UI;

public class UISelectCharInfo : MonoBehaviour
{

    public InputField size;
    public InputField aspd;
    public InputField x1;
    public InputField x2;

    public InputField wp1;
    public InputField wp2;
    public InputField wp3;
    public InputField wp4;
    public InputField wp5;
    public InputField wp6;

    public delegate void CallBack();
    public delegate void CallBack1(object param);

    public CallBack OnBack;
    public CallBack OnStart;
    public CallBack1 OnStart1;

    public void OpenUI(List<CombatUnit> _combatUnits = null, TeamLocation _location = TeamLocation.Explore)
    {
        TeamSystem.Instance.CreateTeam(_location);
        //
        selectCombatUnits.Clear();
        //
        Init();
        //
        isOpen = false;
        //
        InitShow();
        //
        OnClickOpen();
        //
        gameObject.SetActive(true);

    }


    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (isFirst)
        {
            return;
        }

        GetObj();
        //
        detialInfo.InitComponent();
        //
        isFirst = true;
    }

    private void InitShow()
    {
        InitSelectChars();
        LoadCharListShow();
        //
        //  InitSelectSkills();
        //   LoadSkillListShow();
    }

    /// <summary>
    /// 加载角色显示
    /// </summary>
    private void LoadCharListShow()
    {
        ResourceLoadUtil.DeleteChildObj(rightCharTransform);

        List<CharAttribute> charAttributes = new List<CharAttribute>();
        charLists.Clear();
        switch (teamLocation)
        {
            case TeamLocation.Explore:
                charAttributes.AddRange(
                    CharSystem.Instance.GetCharterListByType(playerType).Where(item => item.Status != CharStatus.Die));
                break;
            case TeamLocation.CycleInvasion:
                break;
            case TeamLocation.Expedition:
                break;
        }
        LoadCharShow(charAttributes);
    }

    private void LoadCharShow(List<CharAttribute> charAttributes)
    {
        if (charAttributes.Count == 0)
        {
            return;
        }
        int _row = charAttributes.Count / 4;
        int _index = 0;
        foreach (CharAttribute item in charAttributes)
        {
            LoadCharRes(item, _index / 4 + 1 != _row, selectChars.Values.Any(a => a == item.charID));
            _index++;
        }
    }



    /// <summary>
    /// 初始化选中角色列表
    /// </summary>
    private void InitSelectChars()
    {
        selectChars.Clear();
        for (int i = 0; i < maxCharSum; i++)
        {
            selectChars.Add(i, 0);
        }
    }


    /// <summary>
    /// 点击了角色物体
    /// </summary>
    private void OnClickCharTransfrom(int _charId)
    {
        //是否已经有了
        if (selectChars.ContainsValue(_charId))
        {
            for (int i = 0; i < selectChars.Count; i++)
            {
                if (selectChars[i] != _charId)
                {
                    continue;
                }

                selectChars[i] = 0;
                RemoveCharUpdateShow(i, _charId);
                return;
            }
        }
        //是否添加
        for (int i = 0; i < selectChars.Count; i++)
        {
            if (selectChars[i] != 0)
            {
                continue;
            }
            //添加
            selectChars[i] = _charId;
            AddCharUpdateShow(i, _charId);
            return;
        }
        if (selectChars.Count(item => item.Value != 0) >= maxCharSum)
        {
            return;
        }
        //
        UpdateAllCharShow();
    }


    private void OnClickMask()
    {
        rightObj.SetActive(true);
        detialInfo.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击了打开
    /// </summary>
    private void OnClickOpen()
    {
        //if (isOpen)
        //{
        //    openButton.transform.parent.DOLocalMoveX(602.5f, 0.2f).OnComplete(OnCloseMoveEnd);
        //}
        //else
        //{
        //    openButton.transform.parent.DOLocalMoveX(44.5f, 0.2f);
        //    OnClickCharTag();
        //}
        //isOpen = !isOpen;
    }


    /// <summary>
    /// 点击了返回
    /// </summary>
    private void OnClickBack()
    {
        gameObject.SetActive(false);
        if (OnBack != null)
        {
            OnBack();
        }
    }

    /// <summary>
    /// 点击了开始
    /// </summary>
    private void OnClickStart()
    {
        if (selectCombatUnits.Count < 1)
        {
            return;
        }

        TeamSystem.Instance.SetCharList(selectCombatUnits);
        gameObject.SetActive(false);
        //
        try { sizeValue = float.Parse(size.text); }
        catch (Exception) { sizeValue = 36; }

        try { aspdValue = float.Parse(aspd.text); }
        catch (Exception) { aspdValue = 8; }

        try { xValue = float.Parse(x1.text); }
        catch (Exception) { xValue = 400; }

        try { x1Value = float.Parse(x2.text); }
        catch (Exception) { x1Value = 400; }
        //
        try { wpValue1 = int.Parse(wp1.text); }
        catch (Exception) { wpValue1 = -1; }
        try { wpValue2 = int.Parse(wp2.text); }
        catch (Exception) { wpValue2 = -1; }
        try { wpValue3 = int.Parse(wp3.text); }
        catch (Exception) { wpValue3 = -1; }
        try { wpValue4 = int.Parse(wp4.text); }
        catch (Exception) { wpValue4 = -1; }
        try { wpValue5 = int.Parse(wp5.text); }
        catch (Exception) { wpValue5 = -1; }
        try { wpValue6 = int.Parse(wp6.text); }
        catch (Exception) { wpValue6 = -1; }


        //
        if (OnStart1 != null)
        {
            OnStart1(new float[] { sizeValue, aspdValue, xValue, x1Value, wpValue1, wpValue2, wpValue3, wpValue4, wpValue5, wpValue6 });
        }
        //
        if (OnStart != null)
        {
            OnStart();
        }
    }

    private void OnClickInfo(int index)
    {
        rightObj.SetActive(false);
        CombatUnit combatUnit = selectCombatUnits.Find(a => a.initIndex == index);
        if (combatUnit!=null)
        {
            detialInfo.UpdateInfo(combatUnit.charAttribute);
            detialInfo.transform.parent.gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// 添加角色更新
    /// </summary>
    /// <param name="_charId"></param>
    private void AddCharUpdateShow(int _index, int _charId)
    {
        ResourceLoadUtil.DeleteChildObj(leftCharTransforms[_index]);
        ResourceLoadUtil.LoadCharModel(_charId, leftCharTransforms[_index], Vector3.one * 36);
        charLists[_charId].Find("Select").gameObject.SetActive(true);
        UpdateAllCharShow();
        UpdateTeamCombatUnit();
        UpdateIncentiveInfo(_index, true);
        infoButtons[_index].gameObject.SetActive(true);
    }
    /// <summary>
    /// 移除角色更新
    /// </summary>
    /// <param name="_charId"></param>
    private void RemoveCharUpdateShow(int _index, int _charId)
    {
        ResourceLoadUtil.DeleteChildObj(leftCharTransforms[_index]);
        charLists[_charId].Find("Select").gameObject.SetActive(false);
        UpdateAllCharShow();
        UpdateTeamCombatUnit();
        UpdateIntros(_index, false);
        UpdateIncentiveInfo(_index, false);
        infoButtons[_index].gameObject.SetActive(false);
    }
    /// <summary>
    /// 更新所有角色显示
    /// </summary>
    private void UpdateAllCharShow()
    {
        int _sum = selectChars.Count(item => item.Value != 0);
        //
        foreach (KeyValuePair<int, Transform> item in charLists)
        {
            if (selectChars.ContainsValue(item.Key))
            {
                continue;
            }

            item.Value.Find("NotSelect").gameObject.SetActive(_sum >= maxCharSum);
        }
    }

    private void UpdateIntros(int index, bool isShow)
    {
        CombatUnit combatUnit = selectCombatUnits.Find(a => a.initIndex == index);
        if (isShow)
        {
            if (combatUnit != null)
            {
                intros[index].text = string.Format(incentiveInfoStr, combatUnit.charAttribute.FinalEncourage);
            }

        }

        if (combatUnit == null)
        {
            isShow = false;
        }
        intros[index].gameObject.SetActive(isShow);
        if (index == 0)
        {
            if (combatUnit != null)
            {
                if (isShow && combatUnit.charAttribute.IsCommander)
                {
                    commanderIntro.gameObject.SetActive(true);
                }
                else
                {
                    commanderIntro.gameObject.SetActive(false);
                }
            }
            else
            {
                commanderIntro.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateIncentiveInfo(int index, bool isShow)
    {
        UpdateAllCharIncentiveObj();
    }

    private void UpdateAllCharIncentiveObj()
    {
        for (int i = 0; i < 4; i++)
        {
            ResourceLoadUtil.DeleteChildObj(incentiveTrans[i]);
            CombatUnit combatUnit = selectCombatUnits.Find(a => a.initIndex == i);
            if (combatUnit==null)
            {
                UpdateIntros(i, false);
                continue;
            }
            GetIncentiveObj(combatUnit, incentiveTrans[i]);
            UpdateIntros(i, true);
        }
    }


    /// <summary>
    /// 更新队伍战斗单元
    /// </summary>
    private void UpdateTeamCombatUnit()
    {
        selectCombatUnits.Clear();
        foreach (KeyValuePair<int, int> item in selectChars)
        {
            if (item.Value == 0)
            {
                selectCombatUnits.Add(null);
                continue;
            }

            switch (teamLocation)
            {
                case TeamLocation.Explore:
                    selectCombatUnits.Add(new CombatUnit(CharSystem.Instance.GetCharAttribute(item.Value), item.Key));
                    break;
            }
        }

        teamInfo = new CombatTeamInfo(0, TeamType.Player, selectCombatUnits);
        teamInfo.PersonalityTake();
    }



    /// <summary>
    /// 得到激励信息
    /// </summary>
    private GameObject GetIncentiveObj(CombatUnit combatUnit, Transform parent)
    {
        GameObject obj = ResourceLoadUtil.InstantiateRes(incentiveObj, parent);
        PersonalityAddEncourage info = combatUnit.personalityAddEncourages.Find(a => a.disableTag == 1);
        bool isHave = false;
        if (info != null)
        {
            obj.transform.Find("No").gameObject.SetActive(true);
            isHave = true;
        }
        info = combatUnit.personalityAddEncourages.Find(a => a.disableTag == 2);
        if (info != null)
        {
            obj.transform.Find("NoOther").gameObject.SetActive(true);
            isHave = true;
        }
        foreach (int item in combatUnit.charAttribute.AddFinalEncourageIndexs)
        {
            if (item == combatUnit.initIndex)
            {
                obj.transform.Find("Oneself").gameObject.SetActive(true);
                isHave = true;
                continue;
            }
            if (item == combatUnit.initIndex + 1)
            {
                obj.transform.Find("Left").gameObject.SetActive(true);
                isHave = true;
                continue;
            }
            if (item == combatUnit.initIndex - 1 && combatUnit.initIndex != 0)
            {
                obj.transform.Find("Right").gameObject.SetActive(true);
                isHave = true;
                continue;
            }
        }
        if (!isHave)
        {
            obj.SetActive(false);
        }
        return obj;
    }
    /// <summary>
    /// 加载角色资源
    /// </summary>
    private void LoadCharRes(CharAttribute _attribute, bool isShowBottom = true, bool isShowCancelSelect = false)
    {
        GameObject _obj = ResourceLoadUtil.InstantiateRes(charTemplateObj);
        _obj.name = _attribute.charID.ToString();
        ResourceLoadUtil.ObjSetParent(_obj, rightCharTransform);
        if (_attribute.IsCommander)
        {
            _obj.transform.Find("King").gameObject.SetActive(true);
        }
        //
        _obj.transform.Find("Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon, _attribute.templateID);
        //_obj.transform.Find("Bottom").gameObject.SetActive(isShowBottom);
        _obj.transform.Find("Select").gameObject.SetActive(isShowCancelSelect);
        //
        //  Transform _info = _obj.transform.Find("Info");
        // _info.Find("Type").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharType, _attribute.char_template.templateID);
        _obj.transform.Find("Level").GetComponent<Text>().text = "Lv." + _attribute.charLevel.ToString();
        //_info.Find("Name").GetComponent<Text>().text = _attribute.char_template.charName;
        //
        int _index = _attribute.charID;
        //
        _obj.transform.Find("Icon").GetComponent<Button>().onClick.AddListener(delegate
        {
            OnClickCharTransfrom(_index);
        });
        //
        charLists.Add(_index, _obj.transform);
        _obj.SetActive(true);
    }

    private void OnAllValueChanged(bool arg0)
    {
        playerType = PlayerType.All;
        LoadCharListShow();
    }
    private void OnDefenseValueChanged(bool arg0)
    {
        playerType = PlayerType.Defense;
        LoadCharListShow();
    }
    private void OnAttackValueChanged(bool arg0)
    {
        playerType = PlayerType.Attack;
        LoadCharListShow();
    }
    private void OnFuZhuValueChanged(bool arg0)
    {
        playerType = PlayerType.FuZhu;
        LoadCharListShow();
    }



    private void OnDestroy()
    {

    }

    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        maskButton = transform.Find("Mask").GetComponent<Button>();
        maskButton.onClick.AddListener(OnClickMask);
        detialInfo = transform.Find("AttrPanel/AttrPanel").gameObject.AddComponent<DetialInfo>();
        //
        size = transform.Find("size").GetComponent<InputField>();
        aspd = transform.Find("aspd").GetComponent<InputField>();
        x1 = transform.Find("x").GetComponent<InputField>();
        x2 = transform.Find("x1").GetComponent<InputField>();
        //
        wp1 = transform.Find("1").GetComponent<InputField>();
        wp2 = transform.Find("2").GetComponent<InputField>();
        wp3 = transform.Find("3").GetComponent<InputField>();
        wp4 = transform.Find("4").GetComponent<InputField>();
        wp5 = transform.Find("5").GetComponent<InputField>();
        wp6 = transform.Find("6").GetComponent<InputField>();
        //
        transform.Find("Title").gameObject.AddComponent<Title>().Init(OnClickBack);
        //
        leftCharTransforms = new Transform[4];
        for (int i = 0; i < leftCharTransforms.Length; i++)
        {
            leftCharTransforms[i] = transform.Find("Left/Char").GetChild(i);
        }
        //
        intros.Clear();
        foreach (Transform item in transform.Find("Left/Intro"))
        {
            intros.Add(item.GetComponent<Text>());
        }
        //
        incentiveTrans.Clear();
        foreach (Transform item in transform.Find("Left/Incentive"))
        {
            incentiveTrans.Add(item);
        }
        //
        infoButtons.Clear();
        int index = 0;
        foreach (Transform item in transform.Find("Left/Button"))
        {
            int temp = index;
            infoButtons.Add(item.GetComponent<Button>());
            infoButtons.Last().onClick.AddListener(delegate { OnClickInfo(temp); });
            index++;
        }
        //
        commanderIntro = transform.Find("Left/Commander").gameObject;
        incentiveObj = transform.Find("Left/Temp/Info").gameObject;
        //
        toggles = new List<Toggle>();
        foreach (Transform item in transform.Find("Right/Tags/Tag"))
        {
            toggles.Add(item.GetComponent<Toggle>());
        }
        toggles[0].onValueChanged.AddListener(OnAllValueChanged);
        toggles[1].onValueChanged.AddListener(OnDefenseValueChanged);
        toggles[2].onValueChanged.AddListener(OnAttackValueChanged);
        toggles[3].onValueChanged.AddListener(OnFuZhuValueChanged);
        //
        start = transform.Find("Start/Button").GetComponent<Button>();
        start.onClick.AddListener(OnClickStart);
        //
        rightObj = transform.Find("Right").gameObject;
        charListObj = transform.Find("Right/CharList").gameObject;
        rightCharTransform = charListObj.transform.Find("Scroll/List");
        charTemplateObj = transform.Find("Right/Template/Char").gameObject;
        charScrollRect = charListObj.transform.Find("Scroll").GetComponent<ScrollRect>();
    }
    //
    private int wpValue1;
    private int wpValue2;
    private int wpValue3;
    private int wpValue4;
    private int wpValue5;
    private int wpValue6;
    private float sizeValue;
    private float aspdValue;
    private float xValue;
    private float x1Value;
    //
    private Transform[] leftCharTransforms;
    private readonly Transform[] leftSkillTransforms;
    //
    private Button maskButton;
    private Button start;
    private readonly Button openButton;
    private readonly Button charTagButton;
    private readonly Button skillTagButton;
    private readonly GameObject charTagH;
    private readonly GameObject skillTagH;
    private GameObject charListObj;
    private readonly GameObject skillListObj;
    private Transform rightCharTransform;
    private readonly Transform rightSkillListTransform;
    private GameObject charTemplateObj;
    private readonly GameObject skillTemplateObj;
    private ScrollRect charScrollRect;
    private readonly ScrollRect skillScrollRect;
    private GameObject rightObj;
    //
    private GameObject commanderIntro;
    private List<Text> intros = new List<Text>();
    private List<Transform> incentiveTrans = new List<Transform>();
    private List<Button> infoButtons = new List<Button>();
    private GameObject incentiveObj;
    //
    private bool isFirst;
    private bool isOpen;
    private PlayerType playerType = PlayerType.All;
    private List<Toggle> toggles;
    //
    private Dictionary<int, int> selectChars = new Dictionary<int, int>();
    private Dictionary<int, Transform> charLists = new Dictionary<int, Transform>();
    //
    private List<CombatUnit> selectCombatUnits = new List<CombatUnit>();
    private readonly List<int> selectPlayerskill = new List<int>();
    //
    private readonly Dictionary<int, int> selectSkills = new Dictionary<int, int>();
    private readonly Dictionary<int, Transform> skillLists = new Dictionary<int, Transform>();
    //
    private readonly TeamLocation teamLocation = TeamLocation.Explore;
    private readonly TimeUtil.PlayTimeInfo playTimeInfo;
    private CombatTeamInfo teamInfo;
    private DetialInfo detialInfo;
    //最大角色数量
    private const int maxCharSum = 4;
    private const int maxSkillSum = 4;
    private const string incentiveInfoStr = "+{0} 激励";

}

