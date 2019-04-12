using GameEventDispose;
using MCCombat;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICharPlayerSkillInfo : MonoBehaviour, IDragHandler, IEndDragHandler,IBeginDragHandler
{
    /// <summary>
    /// 加载技能显示
    /// </summary>
    public void LoadSkillShow(PlayerSkillInfo playerSkillInfo, Vector3 pos)
    {
        InitVector = pos;
        UpVector = new Vector3(pos.x, 0);
        if (rectTransform == null) rectTransform = transform.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = InitVector;
        if (playerSkillInfo == null)
        {
            isFirst = false;
            gameObject.SetActive(false);
            return;
        }
        //
        Init();
        this.playerSkillInfo = playerSkillInfo;
        combatSystem = GameModules.Find(ModuleName.combatSystem) as CombatSystem;
        UpdateSkillShow(!isFirst);
        //
        isFirst = true;
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新显示
    /// </summary>
    private void UpdateSkillShow(bool isNew = false)
    {
        //  skillButton.enabled = !(combatSystem.PlayerTeamInfo.CurrentEnergy < playerSkillInfo.energyCost);
        if (!isNew) return;
        //
        nameText.text = playerSkillInfo.CombatskillTemplate.skillName;
        introText.text = playerSkillInfo.CombatskillTemplate.skillDescription1;
        sumText.text = playerSkillInfo.energyCost.ToString();
        skillIcon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.SkillIcon, playerSkillInfo.skillId);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (isFirst) return;
        //
        nameText = transform.Find("Name").GetComponent<Text>();
        introText = transform.Find("Intro").GetComponent<Text>();
        sumText = transform.Find("Sum/Text").GetComponent<Text>();
        abandonButton = transform.Find("Abandon").GetComponent<Button>();
        skillIcon = transform.Find("SkillIcon/Icon").GetComponent<Image>();
        skillButton = transform.GetComponent<Button>();
        //
        skillButton.onClick.AddListener(OnClickSkill);
        abandonButton.onClick.AddListener(OnClickAbandon);
    }

    /// <summary>
    /// 点击了技能
    /// </summary>
    private void OnClickSkill()
    {
        if (rectTransform.anchoredPosition.y > InitVector.y)
        {
            rectTransform.anchoredPosition = InitVector;
            return;
        }
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);

        return;
    }
    /// <summary>
    /// 点击放弃技能
    /// </summary>
    private void OnClickAbandon()
    {
        gameObject.SetActive(false);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.AbandonSkill, (object)playerSkillInfo);
    }



    private void Update()
    {
        if (!isUpdate) return;
        if (rectTransform.anchoredPosition.y < 280) return;
        if (rectTransform.anchoredPosition.x > 327) return;
        if (rectTransform.anchoredPosition.x < -280) return;
        if (combatSystem.CurrentPower < playerSkillInfo.energyCost) return;
        gameObject.SetActive(false);
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.ChooseSkill, (object)playerSkillInfo);
        isUpdate = false;
    }

    //
    bool isUpdate;
    //
    private Vector3 InitVector;
    private Vector3 UpVector;
    private Vector3 tempVector;
    //
    private Text nameText;
    private Text introText;
    private Text sumText;
    private Button abandonButton;
    private Image skillIcon;
    private Button skillButton;
    //
    private bool isFirst;
    //
    private CombatSystem combatSystem;
    private PlayerSkillInfo playerSkillInfo;
    private RectTransform rectTransform;

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = tempVector;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        tempVector = rectTransform.anchoredPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //  if (combatSystem.PlayerTeamInfo.CurrentEnergy < playerSkillInfo.energyCost) return;
        transform.position = transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = Vector3.right * transform.localPosition.x + Vector3.up * transform.localPosition.y;
        isUpdate = true;
    }

}