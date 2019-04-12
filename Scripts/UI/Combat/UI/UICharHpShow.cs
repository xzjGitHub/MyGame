using GameEventDispose;
using MCCombat;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharHpShow : UICharBase
{
    public void Init(UICharUnit charUnit, int sortingOrder)
    {
        this.charUnit = charUnit;
        //
        base.InitInfo(charUnit);
        GetObj();
        //
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
        EventDispatcher.Instance.CombatEvent.AddEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatStageEvent);
        //
        isShow = true;
        canvas.sortingOrder = sortingOrder;
        canvasGroup.alpha = 1;
        gameObject.SetActive(true);
    }

    public void Init(CombatUnit combatUnit, int sortingOrder)
    {
        base.InitInfo(combatUnit);
        GetObj();
        //
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
        isShow = true;
        canvas.sortingOrder = sortingOrder;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 开始显示
    /// </summary>
    /// <param name="execState"></param>
    /// <param name="_hitResult"></param>
    private void StartShow(CRExecState execState, Vector3 pos, int sortingOrder)
    {
        _execState = execState;
        if (execState.stateInfo != null)
        {
            _hitResult = execState.stateInfo.hitResult;
        }
        StartShow(execState.effectResult, pos, sortingOrder, execState.stateAttribute);
        return;
        _pos = pos;
        if (execState.effectResult != null && !execState.effectResult.isShow)
        {
            return;
        }
        canvas.sortingOrder = sortingOrder;
        transform.position = pos;
        //更新Hp显示
        // float maxVlaue = execState.effectResult.maxHp;
        // int _tempValue = execState.effectResult.currentHp < 0 ? 0 : execState.effectResult.currentHp;
        //
        UpdateObjShow(execState.effectResult);
        //hpImage.fillAmount = _tempValue / maxVlaue;
        //_tempValue = execState.effectResult.currentShield < 0 ? 0 : execState.effectResult.currentShield;
        //shieldImage.fillAmount = _tempValue / maxVlaue;
        //// shieldObj.SetActive(shieldImage.fillAmount > 0);
        //_tempValue = execState.effectResult.currentArmor < 0 ? 0 : execState.effectResult.currentArmor;
        //armorImage.fillAmount = _tempValue / maxVlaue;
        ////  armorObj.SetActive(armorImage.fillAmount > 0);
        //
        gameObject.SetActive(true);
        //
        new CoroutineUtil(IEDamageIntroShow(execState.effectResult));
        //GameObject _obj = ResourceLoadUtil.InstantiateRes(ResourceLoadUtil.LoadDamageIntroShow());
        //ResourceLoadUtil.ObjSetParent(_obj, introParent.parent);
        //damageIntroShow = _obj.AddComponent<UIDamageIntroShow>();
        //damageIntroShow.OpenUI(execState, _hitResult, pos, introParent.name == "Left");
        //
        if (hpImage.fillAmount <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        if (IHideHpObj != null)
        {
            IHideHpObj.Start();
            return;
        }
        IHideHpObj = new CoroutineUtil(IEHideHpObj(1));
    }

    /// <summary>
    /// 开始显示
    /// </summary>
    private void StartShow(CREffectResult effectResult, Vector3 pos, int sortingOrder, StateAttribute stateAttribute = null)
    {
        _pos = pos;
        if (effectResult != null && !effectResult.isShow)
        {
            return;
        }
        canvas.sortingOrder = sortingOrder;
        transform.position = pos;
        //更新Hp显示
        UpdateObjShow(effectResult);
        //
        gameObject.SetActive(true);
        //
        new CoroutineUtil(IEDamageIntroShow(effectResult, stateAttribute));
        //
        if (hpImage.fillAmount <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        if (IHideHpObj != null)
        {
            IHideHpObj.Start();
            return;
        }
        IHideHpObj = new CoroutineUtil(IEHideHpObj(1));
    }



    private CoroutineUtil IHideHpObj;
    /// <summary>
    /// 开始显示
    /// </summary>
    private void StartShow(int maxHP, int primevalHP, int costHP, object _pos, int sortingOrder)
    {
        canvas.sortingOrder = sortingOrder;
        transform.position = (Vector3)_pos;
        //更新Hp显示

        hpImage.fillAmount = primevalHP / (float)maxHP;
        shieldImage.fillAmount = 0;
        //
        gameObject.SetActive(true);
        //
        new CoroutineUtil(IEStartShow(maxHP, primevalHP, costHP));
        //
        GameObject _obj = ResourceLoadUtil.InstantiateRes(ResourceLoadUtil.LoadDamageIntroShow());
        ResourceLoadUtil.ObjSetParent(_obj, introParent.parent);
        damageIntroShow = _obj.AddComponent<UIDamageIntroShow>();
        damageIntroShow.OpenUI(costHP, HitResult.Hit, (Vector3)_pos, introParent.name == "Left");
    }
    private IEnumerator IEStartShow(int maxHP, int primevalHP, int costHP)
    {
        float temp = 0;
        while ((int)temp != costHP)
        {
            temp += Time.deltaTime * aspd;
            if ((int)temp >= costHP)
            {
                temp = costHP;
            }

            hpImage.fillAmount = (primevalHP - (int)temp) / (float)maxHP;
            yield return null;
        }

        if (hpImage.fillAmount <= 0)
        {
            gameObject.SetActive(false);
            yield break;
        }
        Invoke("HideHpObj", 0.2f);
    }

    private IEnumerator IEHideHpObj(float value)
    {
        float time = 0;
        while (time < value)
        {
            time += Time.deltaTime;
            yield return null;
        }
        showType = 0;
        EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.HPCostShowEnd, (object)null);
    }

    /// <summary>
    /// 伤害描述显示
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEDamageIntroShow(CREffectResult effectResult, StateAttribute stateAttribute = null)
    {
        //护盾
        float time = 0;
        int value = effectResult.shield;
        int index = 0;
        if (value != 0)
        {
            LoadDamageShow(DamageIntroType.Shield, value, index, stateAttribute);
            while (time < intervalTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            index++;
        }
        //临时护盾
        time = 0;
        value = (int)effectResult.periodShield;
        if (value != 0)
        {
            LoadDamageShow(DamageIntroType.PeriodShield, value, index, stateAttribute);
            while (time < intervalTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            index++;
        }
        //护甲
        time = 0;
        value = effectResult.armor;
        if (value != 0)
        {
            LoadDamageShow(DamageIntroType.Armor, value, index, stateAttribute);
            while (time < intervalTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            index++;
        }
        //临时护甲
        time = 0;
        value = (int)effectResult.periodArmor;
        if (value != 0)
        {
            LoadDamageShow(DamageIntroType.PeriodArmor, value, index, stateAttribute);
            while (time < intervalTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            index++;
        }
        //生命值
        time = 0;
        value = effectResult.hp;
        if (value != 0)
        {
            LoadDamageShow(DamageIntroType.HP, value, index, stateAttribute);
            while (time < intervalTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            index++;
        }
    }
    /// <summary>
    /// 加载伤害显示
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    private void LoadDamageShow(DamageIntroType type, int value, int index, StateAttribute stateAttribute)
    {
        GameObject obj = ResourceLoadUtil.InstantiateRes(ResourceLoadUtil.LoadDamageIntroShow());
        ResourceLoadUtil.ObjSetParent(obj, introParent.parent);
        damageIntroShow = obj.AddComponent<UIDamageIntroShow>();
        damageIntroShow.OpenUI(_hitResult, _pos, type, value, IsCriticalValue(stateAttribute, type), introParent.name == "Left", index);
    }

    /// <summary>
    /// 是否暴击
    /// </summary>
    /// <param name="stateAttribute"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private int IsCriticalValue(StateAttribute stateAttribute, DamageIntroType type)
    {
        if (stateAttribute == null)
        {
            return -1;
        }
        switch (type)
        {
            case DamageIntroType.HP:
                if (stateAttribute.finalHPDB > 1)
                {
                    return 1;
                }
                else if (stateAttribute.finalHPDB == 1)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            case DamageIntroType.Shield:
            case DamageIntroType.PeriodShield:
                if (stateAttribute.finalShieldDB > 1)
                {
                    return 1;
                }
                else if (stateAttribute.finalShieldDB == 1)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            case DamageIntroType.Armor:
            case DamageIntroType.PeriodArmor:
                if (stateAttribute.finalArmorDB > 1)
                {
                    return 1;
                }
                else if (stateAttribute.finalArmorDB == 1)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            default:
                return 0;
        }
    }


    /// <summary>
    /// 角色事件
    /// </summary>
    private void OnCharEvent(CharActionOperation arg1, int teamID, int charIndex, object arg2)
    {
        if (teamID != base.teamID || charIndex != base.charIndex)
        {
            //if (arg1 == CharActionOperation.Hit)
            //{
            //    showType = 0;
            //    UpdateObjShow();
            //}
            return;
        }

        switch (arg1)
        {
            case CharActionOperation.Hit:
                showType = 1;
                object[] obj = arg2 as object[];
                if (obj[0] is int)
                {
                    StartShow((int)obj[0], (int)obj[1], (int)obj[2], obj[3], (int)obj[4]);
                    return;
                }
                if (obj[0] is CRExecState)
                {
                    StartShow((CRExecState)obj[0], (Vector3)obj[1], (int)obj[2]);
                    return;
                }
                if (obj[0] is CREffectResult)
                {
                    StartShow((CREffectResult)obj[0], (Vector3)obj[1], (int)obj[2]);
                    return;
                }

                break;
            case CharActionOperation.Idle:
                showType = 0;
                break;
        }
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
                break;
            case PlayCombatStage.ChooseSkill:
                break;
            case PlayCombatStage.ImmediateSkillEffect:
                break;
            case PlayCombatStage.RoundInfo:
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
                showType = 0;
                UpdateObjShow(combatUnit);
                break;
        }
    }

    /// <summary>
    /// 更新组件显示
    /// </summary>
    private void UpdateObjShow(int hp, float damageAbsorb, int shield, int tempShield, int armor, int tempArmor, float maxHP, float maxShield, float maxArmor)
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        float value = 0;
        //
        hpObj.SetActive(true);
        if (maxHP <= 0)
        {
            hpImage.fillAmount = 0;
            value = 0;
        }
        else
        {
            hpImage.fillAmount = hp / maxHP;
            value = damageAbsorb / maxHP;
        }
        hpDun.SetActive(value > 0);
        tempHP.fillAmount = value;
        //
        shieldObj.SetActive(true);
        if (maxShield <= 0)
        {
            shieldImage.fillAmount = 0;
            value = 0;
        }
        else
        {
            shieldImage.fillAmount = shield / maxShield;
            value = tempShield / maxShield;
        }
        shieldDun.SetActive(value > 0);
        this.tempShield.fillAmount = value;
        //
        armorObj.SetActive(true);
        if (maxArmor <= 0)
        {
            armorImage.fillAmount = 0;
            value = 0;
        }
        else
        {
            armorImage.fillAmount = armor / maxArmor;
            value = tempArmor / maxArmor;
        }
        armorDun.SetActive(value > 0);
        this.tempArmor.fillAmount = value;
    }


    /// <summary>
    /// 更新组件显示
    /// </summary>
    private void UpdateObjShow(CombatUnit combatUnit)
    {
        UpdateObjShow(combatUnit.hp, combatUnit.TempDamageAbsorb, combatUnit.CurrentShield, combatUnit.TempShield,
            combatUnit.CurrentArmor, combatUnit.TempArmor, combatUnit.maxHp, combatUnit.maxShield, combatUnit.maxArmor);
    }

    /// <summary>
    /// 更新组件显示
    /// </summary>
    private void UpdateObjShow(CREffectResult effectResult)
    {
        int hp = effectResult.currentHp < 0 ? 0 : effectResult.currentHp;
        int shield = effectResult.currentShield < 0 ? 0 : effectResult.currentShield;
        int armor = effectResult.currentArmor < 0 ? 0 : effectResult.currentArmor;
        UpdateObjShow(hp, effectResult.currentDamageAbsorb, shield, effectResult.currentTempShield,
            armor, effectResult.currentTempArmor, effectResult.maxHp, effectResult.maxShield, effectResult.maxArmor);
    }


    private void Update()
    {
        if (!isShow)
        {
            return;
        }
        //更新位置
        transform.position = charUnit.ActionOperation.HpPos;
        //更新血条显示模式
        UpdateHpShowType();
    }

    /// <summary>
    /// 更新血条显示模式
    /// </summary>
    private void UpdateHpShowType()
    {
        return;
        switch (showType)
        {
            case 0:
                if (canvasGroup.alpha > 0.3f)
                {
                    canvasGroup.alpha = 0.3f;
                }
                break;
            case 1:
                if (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha = 1;
                }
                break;
        }
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        canvas = transform.GetComponent<Canvas>();
        //
        hpObj = transform.Find("Hp").gameObject;
        shieldObj = transform.Find("Shield").gameObject;
        armorObj = transform.Find("Armor").gameObject;
        hpImage = hpObj.transform.Find("Value").GetComponent<Image>();
        tempHP = hpObj.transform.Find("TempValue").GetComponent<Image>();
        hpDun = hpObj.transform.Find("Dun").gameObject;
        shieldImage = shieldObj.transform.Find("Value").GetComponent<Image>();
        tempShield = shieldObj.transform.Find("TempValue").GetComponent<Image>();
        shieldDun = shieldObj.transform.Find("Dun").gameObject;
        armorImage = armorObj.transform.Find("Value").GetComponent<Image>();
        tempArmor = armorObj.transform.Find("TempValue").GetComponent<Image>();
        armorDun = armorObj.transform.Find("Dun").gameObject;
        //
        introParent = transform.parent.parent;
        UpdateObjShow(combatUnit);
    }

    private void OnDestroy()
    {
        //
        EventDispatcher.Instance.CharEvent.RemoveEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<PlayCombatStage, object>(EventId.CombatEvent, OnCombatStageEvent);
    }

    private const float intervalTime = 0.1f;
    //
    private UIDamageIntroShow damageIntroShow;
    private Transform introParent;
    //
    private GameObject hpObj;
    private GameObject shieldObj;
    private GameObject armorObj;
    private GameObject hpDun;
    private GameObject shieldDun;
    private GameObject armorDun;
    private Image hpImage;
    private Image tempHP;
    private Image shieldImage;
    private Image tempShield;
    private Image armorImage;
    private Image tempArmor;
    //
    private bool isShow;
    /// <summary>
    /// 显示模式 1=受击 0=静止
    /// </summary>
    private int showType;
    private readonly float aspd = 20f;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private UICharUnit charUnit;
    //
    private CRExecState _execState;
    private HitResult _hitResult;
    private Vector3 _pos;
}

