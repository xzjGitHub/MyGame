using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 冷却信息
/// </summary>
public class UICoolDownInfo : MonoBehaviour
{


    /// <summary>
    /// 初始化信息
    /// </summary>
    /// <param name="maxValue"></param>
    /// <param name="isSkill"></param>
    /// <param name="initStr"></param>
    public void InitInfo(float maxValue, bool isSkill = false, string initStr = "")
    {
        isAuto = false;
        this.initStr = initStr;
        this.maxValue = (int)maxValue;
        this.isSkill = isSkill;
        Init();
        //
        numText.text = initStr + (int)maxValue;
    }

    /// <summary>
    /// 更新值
    /// </summary>
    public bool UpdateValue(float value)
    {
        Init();
        if (maxValue==0)
        {
            nowVaule = 0;
        }
        else
        {
            nowVaule = 1 - value / maxValue;
        }

        valueImage.fillAmount = nowVaule;
        numText.text = nowVaule == 0 ? string.Empty : initStr + (maxValue - (int)value);
        if (nowVaule <= 0)
        {
            time = 0;
            nowVaule = 0;
            topObj.SetActive(false);
            angleTrans.gameObject.SetActive(false);
            if (maskObj != null && maskObj.activeSelf)
            {
                maskObj.SetActive(false);
            }
            return true;
        }
        UpdateShow(nowVaule);
        return false;
    }

    public void SetMaxValue()
    {
        valueImage.fillAmount = 1;
    }

    public void SetMinValue()
    {
        valueImage.fillAmount = 0;
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
        //
        GetObj();
        //
        isFirst = true;
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    private void UpdateShow(float vaule)
    {
        if (maskObj != null && !maskObj.activeSelf)
        {
            maskObj.SetActive(true);
        }
        if (vaule >= 1)
        {
            topObj.SetActive(false);
            angleTrans.gameObject.SetActive(false);
            return;
        }
        topObj.SetActive(true);
        angleTrans.gameObject.SetActive(true);
        angleTrans.eulerAngles = Vector3.forward * (vaule * 360f * 1f);
    }


    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        if (!isAuto)
        {
            return;
        }
        time += Time.deltaTime;
        //
        UpdateValue(time);
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        Transform obj = transform.Find("Mask");
        if (obj != null)
        {
            maskObj = obj.gameObject;
        }
        if (isSkill)
        {
            valueImage = transform.Find("Value").GetComponent<Image>();
        }
        else
        {
            valueImage = transform.GetComponent<Image>();
        }


        topObj = transform.Find("Top").gameObject;
        angleTrans = transform.Find(isSkill ? "Mask/Angle" : "Angle");
        numText = transform.Find("Text").GetComponent<Text>();
    }
    //
    private float time;
    private bool isFirst;
    private bool isAuto = true;
    private string initStr;
    public float nowVaule;
    public int maxValue = 2;
    public bool isSkill = false;
    //
    private Image valueImage;
    private GameObject topObj;
    private Transform angleTrans;
    private Text numText;
    private GameObject maskObj;
}
