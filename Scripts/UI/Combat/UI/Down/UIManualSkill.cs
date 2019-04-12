using MCCombat;
using UnityEngine;
using UnityEngine.UI;

public class UIManualSkill : MonoBehaviour
{
    public int SkillID { get { return _skillID; } }

    public CSkillInfo SkillInfo { get { return _skillInfo; } }

    public bool IsCanAlternative { get { return _isCanAlternative; } }

    public int EnergyCost { get { return _energyCost; } }

    public delegate bool CallBack(UIManualSkill info);

    public CallBack CallCancelSkill;
    public CallBack CallClickSkill;

    public void ResetShow(int nowMP)
    {
        _isOnClick = false;
        UpdateShow(nowMP);
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    public void MPUpdateShow(int nowMP = 0)
    {
        UpdateShow(nowMP);
    }

    /// <summary>
    /// 选择激励技能更新显示
    /// </summary>
    /// <param name="isincentive"></param>
    /// <param name="incentiveType"></param>
    public void UpdateUseCommonSkillShow(bool isincentive, int incentiveType = 0)
    {
        _isincentive = isincentive;
        _incentiveType = incentiveType;
        if (!isincentive)
        {
            _heightObj1.SetActive(false);
            _heightObj2.SetActive(false);
            _incentive2.SetActive(false);
            _incentive1.SetActive(false);
            return;
        }
        //检查是否能替换
        if (_skillInfo.Combatskill.alternativeSkill == 0)
        {
            _isincentive = false;
            _heightObj1.SetActive(false);
            _heightObj2.SetActive(false);
            return;
        }
        //
        _heightObj1.SetActive(incentiveType != 2);
        _heightObj2.SetActive(incentiveType == 2);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(CombatUnit combatUnit, CSkillInfo skillInfo, int nowMP = 0)
    {
        if (!_isFirst)
        {
            GetObj();
        }
        _heightObj1.SetActive(false);
        _heightObj2.SetActive(false);
        _incentive1.SetActive(false);
        _incentive2.SetActive(false);
        //
        _isFirst = true;
        //
        _combatUnit = combatUnit;
        _skillInfo = skillInfo;
        _skillID = skillInfo.ID;
        _isCanAlternative = _skillInfo.Combatskill.alternativeSkill != 0;
        //
        _energyCost = _skillInfo.Combatskill.energyCost;
        //
        _mpText.text = "MP:" + skillInfo.ManaCost;
        _icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.SkillIcon, skillInfo.ID);
        UpdateShow(nowMP);
        _isOnClick = false;
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新显示
    /// </summary>
    /// <param name="nowMP"></param>
    private void UpdateShow(int nowMP)
    {

        //检查魔力
        if (_isOnClick)
        {
            return;
        }
        //检查冷却影响
        _coolDownInfo.InitInfo(_skillInfo.Cooldown, true);
        _isCanUse = _coolDownInfo.UpdateValue(_skillInfo.Cooldown - _skillInfo.UseRound);
        _mpText.gameObject.SetActive(_isCanUse);
        _button.enabled = _isCanUse;
        if (_isCanUse)
        {
            if (nowMP < _skillInfo.ManaCost)
            {
                _mpMask.SetActive(true);
                _mpText.color = Color.red;
            }
            else
            {
                _mpMask.SetActive(false);
                _mpText.color = Color.white;
            }
        }
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    private void OnClickButton()
    {
        _isOnClick = true;
        if (CallClickSkill(this))
        {
            if (!_isCanAlternative || !_isincentive)
            {
                _coolDownInfo.SetMaxValue();
                _incentive2.SetActive(false);
                _incentive1.SetActive(false);
            }
            else
            {
                _coolDownInfo.SetMinValue();
                switch (_incentiveType)
                {
                    case 2:
                        _incentive2.SetActive(true);
                        break;
                    default:
                        _incentive1.SetActive(true);
                        break;
                }
            }
            _mpText.gameObject.SetActive(false);
        }
        else
        {
            CancelClick();
        }
        _heightObj1.SetActive(false);
        _heightObj2.SetActive(false);
    }

    /// <summary>
    /// 取消点击
    /// </summary>
    public void CancelClick()
    {
        //取消技能 更新MP显示、角色顺序显示
        _coolDownInfo.SetMinValue();

        if (_incentive1.activeInHierarchy)
        {
            _incentive1.SetActive(false);
        }
        if (_incentive2.activeInHierarchy)
        {
            _incentive2.SetActive(false);
        }
        _mpText.gameObject.SetActive(true);
        _isOnClick = false;
        if (CallCancelSkill != null)
        {
            CallCancelSkill(this);
        }
    }

    private void GetObj()
    {
        _button = transform.Find("Button").GetComponent<Button>();
        _icon = transform.Find("Icon").GetComponent<Image>();
        _heightObj1 = transform.Find("Height1").gameObject;
        _heightObj2 = transform.Find("Height2").gameObject;
        _incentive1 = transform.Find("Incentive1").gameObject;
        _incentive2 = transform.Find("Incentive2").gameObject;
        _mpText = transform.Find("MP").GetComponent<Text>();
        _mpMask = transform.Find("MPMask").gameObject;
        Transform coolDown = transform.Find("CoolDown");
        _coolDownInfo = coolDown.GetComponent<UICoolDownInfo>();
        if (_coolDownInfo == null)
        {
            _coolDownInfo = coolDown.gameObject.AddComponent<UICoolDownInfo>();
        }
        //
        _button.onClick.AddListener(OnClickButton);
    }
    //
    private Button _button;
    private Image _icon;
    private GameObject _heightObj1;
    private GameObject _heightObj2;
    private GameObject _incentive1;
    private GameObject _incentive2;
    private Text _mpText;
    private GameObject _mpMask;
    //
    private bool _isCanUse;
    private bool _isOnClick;
    //
    private bool _isincentive;
    private int _incentiveType;
    private bool _isCanAlternative;
    private int _energyCost;
    private bool _isFirst;
    private int _skillID;
    private UICoolDownInfo _coolDownInfo;
    private CSkillInfo _skillInfo;
    private CombatUnit _combatUnit;
}
