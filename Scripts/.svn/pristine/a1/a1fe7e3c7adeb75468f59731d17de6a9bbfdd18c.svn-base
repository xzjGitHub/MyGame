using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIInvasionResist : MonoBehaviour
{
    public delegate void CallBack();

    public CallBack OnCallBack;
    //
    private Button backButton;
    private Text siegeTimeText;
    private ScrollRect invasionTeamScrollRect;
    private Transform invasionTeamList;
    private GameObject teamGameObject;
    //
    private string siegeTimeStr = "围攻时间\n{0}";
    private string hpValueStr1 = "剩余\n{0}%生命";
    private string hpValueStr2 = "被击败";
    //
    private bool isFirst;
    //


    public void OpenUI(CycleInvasionSystem _cycleInvasionSystem)
    {

        Init();
        //
        UpdateTeamShow(_cycleInvasionSystem.InvasionMobTeams);
        //
        gameObject.SetActive(true);
    }

    private void Init()
    {
        GetObj();
        //
        invasionTeamScrollRect.verticalNormalizedPosition = 0;
    }


    private void UpdateTeamShow(List<InvasionMobTeam> _teams)
    {
        ResourceLoadUtil.DeleteChildObj(invasionTeamList);
        foreach (var item in _teams)
        {
            LoadTeamRes(item);
        }
    }

    private void OnClickBack()
    {
        gameObject.SetActive(false);
        if (OnCallBack == null) return;
        OnCallBack();
    }

    /// <summary>
    /// 点击了队伍
    /// </summary>
    private void OnClickTeam(int _teamId)
    {
        LogHelperLSK.LogError("点击了队伍" + _teamId);
    }
    /// <summary>
    /// 加载队伍资源
    /// </summary>
    private void LoadTeamRes(InvasionMobTeam _invasionMobTeam)
    {
        GameObject _obj = ResourceLoadUtil.InstantiateRes(teamGameObject, invasionTeamList);
        //
        _obj.transform.Find("Damage/Text").GetComponent<Text>().text = ((int)_invasionMobTeam.coreDamage).ToString();
        //
        int _id = _invasionMobTeam.teamId;
        float _hpSum = _invasionMobTeam.combatUnits.Sum(a => a.maxHp);
        float _hp = _invasionMobTeam.combatUnits.Sum(a => a.hp);
        if (_hp <= 0)
        {
            _obj.transform.Find("Vaule/Text").GetComponent<Text>().text = hpValueStr2;
            _obj.transform.Find("Revive").gameObject.SetActive(_invasionMobTeam.IsRevivable);
            _obj.transform.Find("Button").gameObject.SetActive(false);
            return;
        }
        //
        _obj.transform.Find("Vaule").GetComponent<Image>().fillAmount = 1 - _hp / _hpSum;
        _obj.transform.Find("Vaule/Text").GetComponent<Text>().text = string.Format(hpValueStr1, (int)(_hp / _hpSum * 100));
        //
        _obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate
       {
           OnClickTeam(_id);
       });
    }

    void Update()
    {
    }

    private void GetObj()
    {
        if (isFirst) return;
        backButton = transform.Find("Back").GetComponent<Button>();
        siegeTimeText = transform.Find("SiegeTime").GetComponent<Text>();
        invasionTeamScrollRect = transform.Find("InvasionTeam").GetComponent<ScrollRect>();
        invasionTeamList = invasionTeamScrollRect.transform.Find("Viewport/Content");
        teamGameObject = transform.Find("temp/Team").gameObject;
        //
        isFirst = true;
    }

}
