using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInvasionTeamInfo : MonoBehaviour
{
    public delegate void CallBack();

    public CallBack OnCallBack;
    //
    private Button backButton;
    private Transform teamList;
    private GameObject teamGameObject;
    private ScrollRect teamScrollRect;
    //
    private bool isFirst;
    //
    private CycleInvasionSystem cycleInvasionSystem;

    public void OpenUI(CycleInvasionSystem _cycleInvasionSystem)
    {
        Init();
        cycleInvasionSystem = _cycleInvasionSystem;
        //
        UpdateTeamShow(_cycleInvasionSystem.InvasionMobTeams);
        //
        gameObject.SetActive(true);
    }

    private void Init()
    {
        GetObj();
        teamScrollRect.verticalNormalizedPosition = 0;
    }


    /// <summary>
    /// 点击了返回
    /// </summary>
    private void OnClickBack()
    {
        gameObject.SetActive(false);
        if (OnCallBack != null)
        {
            OnCallBack();
        }
    }
    /// <summary>
    /// 点击了角色
    /// </summary>
    private void OnClickChar(int _teamId, int _charId)
    {
        CombatUnit _combatUnit = cycleInvasionSystem.GetInvasionMobUnitInfo(_teamId, _charId);
    }

    /// <summary>
    /// 更新队伍显示
    /// </summary>
    private void UpdateTeamShow(List<InvasionMobTeam> _teams)
    {
        ResourceLoadUtil.DeleteChildObj(teamList);
        foreach (var item in _teams)
        {
            LoadTeamRes(item);
        }
    }
    /// <summary>
    /// 加载队伍信息
    /// </summary>
    private void LoadTeamRes(InvasionMobTeam _invasionMobTeam)
    {
        GameObject _obj = ResourceLoadUtil.InstantiateRes(teamGameObject);
        ResourceLoadUtil.ObjSetParent(_obj, teamList, Vector3.one * 0.8f);
        _obj.transform.Find("Damage/Text").GetComponent<Text>().text = _invasionMobTeam.coreDamage.ToString();
        for (int i = 0; i < _invasionMobTeam.combatUnits.Count; i++)
        {
            UpdateCharRes(_obj.transform.Find("List").GetChild(i), _invasionMobTeam.combatUnits[i], _invasionMobTeam.teamId);
        }
    }
    /// <summary>
    /// 更新角色信息
    /// </summary>
    private void UpdateCharRes(Transform _obj, CombatUnit _combatUnit, int _teamId)
    {
        _obj.Find("Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon, _combatUnit.charAttribute.templateID);
        _obj.Find("Info/Level").GetComponent<Text>().text = _combatUnit.charAttribute.charLevel.ToString();
        _obj.Find("Info/Name").GetComponent<Text>().text = _combatUnit.charAttribute.char_template.charName;
        //
        int _team = _teamId;
        int _char = _combatUnit.charAttribute.charID;
        _obj.Find("Select").GetComponent<Button>().onClick.AddListener(delegate
        {
            OnClickChar(_team, _char);
        });
        _obj.gameObject.SetActive(true);
    }


    private void GetObj()
    {
        if (isFirst)return;
        backButton = transform.Find("Back").GetComponent<Button>();
        backButton.onClick.AddListener(OnClickBack);
        //
        teamList = transform.Find("TeamList/Viewport/Content");
        teamScrollRect = transform.Find("TeamList").GetComponent<ScrollRect>();
        teamGameObject = transform.Find("temp/Team").gameObject;
        //
        isFirst = true;
    }

}
