using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


/// <summary>
/// 角色升级显示
/// </summary>
public class UICharUpgradeShow : MonoBehaviour
{

    public bool IsOk {  get { return isOk; }  }
    /// <summary>
    /// 显示
    /// </summary>
    public void Show(CharUpgradeInfo _info)
    {
        GetObj();
        //
        isOk = false;
        startMove = false;
        charUpgradeInfo = _info;
        charLevelup = Char_lvupConfig.GetChar_Lvup(_info.initLevel);
        //
        ExpImage.fillAmount = _info.initExp / charLevelup.levelupExp;
        LevelText.text = "Lv." + _info.initLevel;
        gameObject.SetActive(true);
        //
        LoadTipsShow();
        //
        IE_UpdateShow = new CoroutineUtil(UpdateShow());
    }

    /// <summary>
    /// 加载弹出显示
    /// </summary>
    private void LoadTipsShow()
    {
        introObjList.Clear();
        //伤害
        if (charUpgradeInfo.finalDamage > 1)
        {
            introText = LaodRes().GetComponent<Text>();
            introText.text = "+" + (int)charUpgradeInfo.finalDamage + "B";
            introText.font = ResourceLoadUtil.LoadFont(GetFontName(5));
        }
        //生命
        if (charUpgradeInfo.finalHP > 1)
        {
            introText = LaodRes().GetComponent<Text>();
            introText.text = "+" + (int)charUpgradeInfo.finalHP + "A";
            introText.font = ResourceLoadUtil.LoadFont(GetFontName(1));
        }
        //治疗
        if (charUpgradeInfo.finalHealing > 1)
        {
            introText = LaodRes().GetComponent<Text>();
            introText.text = "+" + (int)charUpgradeInfo.finalHealing + "B";
            introText.font = ResourceLoadUtil.LoadFont(GetFontName(4));
        }
        //防御
        if (charUpgradeInfo.finalDRBRating > 1)
        {
            introText = LaodRes().GetComponent<Text>();
            introText.text = "+" + (int)charUpgradeInfo.finalDRBRating + "B";
            introText.font = ResourceLoadUtil.LoadFont(GetFontName(2));
        }
        //格挡
        if (charUpgradeInfo.finalBCBRating > 1)
        {
            introText = LaodRes().GetComponent<Text>();
            introText.text = "+" + (int)charUpgradeInfo.finalBCBRating + "B";
            introText.font = ResourceLoadUtil.LoadFont(GetFontName(7));
        }
        //暴击
        if (charUpgradeInfo.finalCHCRating > 1)
        {
            introText = LaodRes().GetComponent<Text>();
            introText.text = "+" + (int)charUpgradeInfo.finalCHCRating + "B";
            introText.font = ResourceLoadUtil.LoadFont(GetFontName(8));
        }
        //精通
        if (charUpgradeInfo.finalSBRating > 1)
        {
            introText = LaodRes().GetComponent<Text>();
            introText.text = "+" + (int)charUpgradeInfo.finalSBRating + "B";
            introText.font = ResourceLoadUtil.LoadFont(GetFontName(6));
        }
        //
        IE_UpdateMoveShow = new CoroutineUtil(UpdateMoveShow());
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    private IEnumerator UpdateShow()
    {
        float _maxValue = 1;
        float _tempValue = 0;
        aspd = fixedAspd;
        for (int i = 0; i <= charUpgradeInfo.upgradeNum; i++)
        {
            LevelText.text = "Lv." + (charUpgradeInfo.initLevel + i);
            if (i == charUpgradeInfo.upgradeNum)
            {
                _maxValue = charUpgradeInfo.charNowExp / Char_lvupConfig.GetChar_Lvup(charUpgradeInfo.charNowLevel).levelupExp;
                _tempValue = _maxValue - ExpImage.fillAmount;
                aspd = fixedAspd * _tempValue / slowDown;
            }
            while (ExpImage.fillAmount < _maxValue)
            {
                ExpImage.fillAmount += Time.deltaTime * aspd;
                yield return null;
            }
            //
            ExpImage.fillAmount = i != charUpgradeInfo.upgradeNum ? 0 : _maxValue;
            _maxValue = 1;
            if (i != 0) continue;
            startMove = true;
        }
        isOk = true;
    }

    private IEnumerator UpdateMoveShow()
    {
        while (!startMove)
        {
            yield return null;
        }
        for (int i = 0; i < introObjList.Count; i++)
        {
            if (i != 0)
            {
                yield return new WaitForSeconds(introDelayedTime);
            }
            //
            IE_StartMove = new CoroutineUtil(StartMove(introObjList[i]));
        }
    }


    IEnumerator StartMove(CanvasGroup _obj)
    {
        RectTransform _temp;
        float time = 0;
        _temp = _obj.GetComponent<RectTransform>();
        _temp.gameObject.SetActive(true);
        while (_obj.alpha > 0)
        {
            time += Time.deltaTime;
            if (time > delayedTime)
            {
                _obj.alpha -= Time.deltaTime * introMoveSlowDown;
            }
            _temp.localPosition += Vector3.up * Time.deltaTime * introMoveAspd;
            yield return null;
        }
        yield return null;
        Destroy(_obj.gameObject);
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    private GameObject LaodRes()
    {
        tempIntroObj = ResourceLoadUtil.ObjSetParent(ResourceLoadUtil.InstantiateRes(tipObj.gameObject), transform, tipObj.transform.localScale,
         new Vector3(transform.localPosition.x, transform.localPosition.y + 130, 0));
        //
        introObjList.Add(tempIntroObj.GetComponent<CanvasGroup>());
        return tempIntroObj;
    }

    /// <summary>
    /// 得到字体名字
    /// </summary>
    private string GetFontName(int _type)
    {
        switch (_type)
        {
            case 0:
                return String.Empty;
            case 1:
                return "Font1";
            case 2:
                return "Font2";
            case 3:
                return "Font3";
            case 4:
                return "Font4";
            case 5:
                return "Font5";
            case 6:
                return "Font6";
            case 7:
                return "Font7";
            case 8:
                return "Font8";
            case 9:
                return "Font9";
            case 10:
                return "Font10";
            default:
                return String.Empty;
        }
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    void GetObj()
    {
        if (isFirst) return;
        ExpImage = transform.Find("Exp/Image").GetComponent<Image>();
        LevelText = transform.Find("Level").GetComponent<Text>();
        tipObj = transform.Find("Tip").GetComponent<CanvasGroup>();
        isFirst = true;
    }


    private void OnDestroy()
    {
        if (IE_UpdateShow != null) IE_UpdateShow.Stop();
        if (IE_UpdateMoveShow != null) IE_UpdateMoveShow.Stop();
        if (IE_StartMove != null) IE_StartMove.Stop();
        IE_UpdateShow = null;
        IE_UpdateMoveShow = null;
        IE_StartMove = null;
    }
    //
    private const float fixedAspd = 2f;
    private const float slowDown = 2.5f;
    private const float delayedTime = 0.6f;
    private const float introMoveAspd = 200f;
    private const float introMoveSlowDown = 4f;
    private const float introDelayedTime = 0.2f;
    private float aspd;
    //
    private Image ExpImage;
    private Text LevelText;
    private CanvasGroup tipObj;
    //
    private bool isFirst;
    private bool startMove;
    private bool isOk = true;
    //
    private Text introText;
    private GameObject tempIntroObj;
    private List<CanvasGroup> introObjList = new List<CanvasGroup>();
    //
    private CharUpgradeInfo charUpgradeInfo;
    private Char_lvup charLevelup;
    //
    private CoroutineUtil IE_UpdateShow;
    private CoroutineUtil IE_UpdateMoveShow;
    private CoroutineUtil IE_StartMove;
}
