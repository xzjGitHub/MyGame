using MCCombat;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageIntroShow : MonoBehaviour
{

    public void OpenUI( HitResult hitResult, Vector3 pos, DamageIntroType type, int value, int criticalValue = 0, bool isLeft = false, int index = 0)
    {
        GetObj();
        //检查暴击
        // criticalValue = 1;
        isCritical = criticalValue == 1;
        //
        UpdateShow(type, value);
        //
        transform.position = pos;
        transform.localPosition += Vector3.up * (isCritical ? 70f : 50f);
        transform.localScale = Vector3.one * (criticalValue == -1 ? 0.5f : 0.8f);
        transform.localPosition += AmendPos(isLeft, type, index);
        //
        gameObject.SetActive(true);

        //
        animator.Play(isCritical ? CriticalAMStr : GeneralAMStr);
        isStartUpdate = true;
    }

    public void OpenUI(int value, HitResult hitResult, Vector3 pos, bool isLeft = false)
    {
        GetObj();
        //
        //  if (isLeft) _hitResult = HitResult.Critical;
        //
        UpdateShow(value, hitResult);
        //
        transform.position = pos;
        transform.localPosition += Vector3.up * (hitResult == HitResult.Critical ? 70f : 50f);
        transform.localScale = Vector3.one * 0.8f;
        //
        gameObject.SetActive(true);
        //
        animator.Play(hitResult == HitResult.Critical ? CriticalAMStr : GeneralAMStr);
        isStartUpdate = true;
    }


    /// <summary>
    /// 修正位置
    /// </summary>
    /// <param name="isLeft"></param>
    /// <param name="type"></param>
    private Vector3 AmendPos(bool isLeft, DamageIntroType type, int index)
    {
        Vector3 temp = (isLeft ? Vector3.left : Vector3.right) * (/*120 +*/ index * 10);
        temp += Vector3.up * (index * 30 /*- 50f*/);
        return temp;
    }


    private void Update()
    {
        if (!isStartUpdate)
        {
            return;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            isStartUpdate = false;
            DestroyImmediate(gameObject);
        }
    }




    /// <summary>
    /// 更新显示
    /// </summary>
    private void UpdateShow(int value, HitResult _hitResult)
    {
        shieldObj.SetActive(false);
        hpObj.SetActive(false);
        armorObj.SetActive(false);
        valueText.text = "-" + value;
        //
        valueText.font = ResourceLoadUtil.LoadFont(FontName(_hitResult, CRStateEffectType.HP, -value));
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    private void UpdateShow(DamageIntroType type, int value)
    {
        //stateTemplate = State_templateConfig.GetState_template(execState.stateID);
        //if (stateTemplate == null)
        //{
        //    return;
        //}
        //resultEffectConfig = ResultEffectConfigConfig.GetResultEffectConfig(stateTemplate.resultType);
        //if (resultEffectConfig == null)
        //{
        //    return;
        //}
        //
        GameObject obj;
        switch (type)
        {
            case DamageIntroType.HP:
                obj = value > 0 ? hpObj : hp1Obj;
                break;
            case DamageIntroType.Shield:
            case DamageIntroType.PeriodShield:
                obj = shieldObj;
                break;
            case DamageIntroType.Armor:
            case DamageIntroType.PeriodArmor:
                obj = armorObj;
                break;
            default:
                obj = hpObj;
                break;
        }
        obj.SetActive(isCritical);
        valueText.text = string.Format(IntroStr, value > 0 ? "+" : "", value.ToString());
        valueText.font = ResourceLoadUtil.LoadFont(FontName(type, value, isCritical));
    }
    /// <summary>
    /// 得到字体名字
    /// </summary>
    private string FontName(DamageIntroType type, int value, bool isCritical)
    {
        switch (type)
        {
            case DamageIntroType.HP:
                return value > 0 ? "Font1" : "Font7";
            case DamageIntroType.PeriodShield:
            case DamageIntroType.Shield:
                return value > 0 ? "Font2" : "Font2";
            case DamageIntroType.PeriodArmor:
            case DamageIntroType.Armor:
                return value > 0 ? "Font8" : "Font8";
            default:
                return value > 0 ? "Font1" : "Font1";
        }
    }

    /// <summary>
    /// 得到字体名字
    /// </summary>
    private string FontName(HitResult _hitResult, CRStateEffectType effectType, int value)
    {
        return _hitResult == HitResult.Critical ? (value > 0 ? "Font11" : "Font77") : (value > 0 ? "Font1" : "Font7");
        switch (effectType)
        {
            case CRStateEffectType.Shield:
                return _hitResult == HitResult.Critical ? "Font22" : "Font2";
            case CRStateEffectType.AddState:
            case CRStateEffectType.RemoveState:
            case CRStateEffectType.All:
            case CRStateEffectType.Absorb:
            case CRStateEffectType.HP:
                return _hitResult == HitResult.Critical ? (value > 0 ? "Font11" : "Font77") : (value > 0 ? "Font1" : "Font7");
            default:
                return _hitResult == HitResult.Critical ? "Font77" : "Font7";
        }
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        animator = transform.GetComponent<Animator>();
        valueText = transform.Find("Value").GetComponent<Text>();
        Transform bgTransfrom = valueText.transform.Find("Bg");
        hpObj = bgTransfrom.Find("Hp").gameObject;
        hp1Obj = bgTransfrom.Find("Hp1").gameObject;
        shieldObj = bgTransfrom.Find("Shield").gameObject;
        armorObj = bgTransfrom.Find("Armor").gameObject;
    }


    //
    private bool isCritical;
    private bool isStartUpdate;
    //
    private Animator animator;
    private Text valueText;
    private GameObject hpObj;
    private GameObject hp1Obj;
    private GameObject shieldObj;
    private GameObject armorObj;
    //
    private readonly State_template stateTemplate;
    private readonly ResultEffectConfig resultEffectConfig;
    //
    private const string GeneralAMStr = "pugong";
    private const string CriticalAMStr = "baoji";
    private const string IntroStr = "{0}{1}A";
}

public enum DamageIntroType
{
    HP = 1,
    Shield = 2,
    PeriodShield = 3,
    Armor = 4,
    PeriodArmor = 5,
}