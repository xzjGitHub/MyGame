using MCCombat;
using UnityEngine;
using UnityEngine.UI;

public class UICommonSkill : MonoBehaviour
{

    public delegate bool CallBack(CommonSkillInfo param);

    public CallBack CallClickButton;

    public void Init(CSkillInfo info, CombatUnit combatUnit)
    {
        _skillInfo = info as CommonSkillInfo;
        this.combatUnit = combatUnit;
        //
        GetObj();
        //
        _icon1.gameObject.SetActive(false);
        _icon2.gameObject.SetActive(false);
        _high1.SetActive(false);
        _high2.SetActive(false);
        _showIcon.gameObject.SetActive(true);
        //
        _button.interactable = _skillInfo.UseRound == 0 && GetEncourage() != 0;
        //
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateShow(bool isUse, int tempEncourage = 0)
    {
        if (!_isFirst)
        {
            return;
            //加载Icon
        }
        int encourage = GetEncourage() - tempEncourage;
        if (!isUse)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = _skillInfo.UseRound == 0 && encourage != 0;
        }
        _chargeObj.SetActive(_skillInfo.UseRound == 0 && encourage != 0);
        _chargeT.text = encourage.ToString();
        if (isUse)
        {
            _showHigh.SetActive(tempEncourage != 0);
        }
        else
        {
            _showHigh.SetActive(false);
        }
    }
    /// <summary>
    /// 更新按钮显示
    /// </summary>
    /// <param name="isShow"></param>
    public void UpdateButtonShow(bool isShow)
    {
        if (!_isFirst)
        {
            return;
        }
        _button.interactable = isShow;
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    private void OnClickButton()
    {
        if (CallClickButton != null)
        {
            bool isUse = CallClickButton(_skillInfo);
            UpdateShow(true, isUse ? 1 : 0);
        }
    }


    /// <summary>
    /// 获得能量
    /// </summary>
    /// <returns></returns>
    private int GetEncourage()
    {
        return UICombatTool.Instance.CombatSystem.GetCombatUnitInfo(combatUnit).charAttribute.FinalEncourage;
    }
    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }
        //
        _icon1 = transform.Find("Icon").GetComponent<Image>();
        _icon2 = transform.Find("Icon1").GetComponent<Image>();
        _high1 = transform.Find("High").gameObject;
        _high2 = transform.Find("High1").gameObject;
        _chargeObj = transform.Find("Charge").gameObject;
        _chargeT = _chargeObj.transform.Find("Text").GetComponent<Text>();
        _showIcon = _skillInfo.CommonType != CommonSkillType.All ? _icon1 : _icon2;
        _showHigh = _skillInfo.CommonType != CommonSkillType.All ? _high1 : _high2;
        _button = _showIcon.GetComponent<Button>();
        //
        _button.onClick.AddListener(OnClickButton);
        //
        _isFirst = true;
    }

    //
    private CommonSkillInfo _skillInfo;
    private bool _isFirst;
    //
    private Image _icon1;
    private Image _icon2;
    private GameObject _high1;
    private GameObject _high2;
    private Text _chargeT;
    private GameObject _chargeObj;
    private Button _button;
    private Image _showIcon;
    private GameObject _showHigh;
    //
    private CombatUnit combatUnit;

    public CommonSkillInfo SkillInfo
    {
        get
        {
            return _skillInfo;
        }
    }
}
