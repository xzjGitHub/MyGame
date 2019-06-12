using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIAmendAttributePopup : MonoBehaviour
{
    public Action<object> OnPlayEnd;
    public object Param;

    public void OpenUI(List<float> amendValues, Vector3 posVector3)
    {
        GetObj();
        //
        ResourceLoadUtil.DeleteChildObj(_list);
        transform.position = new Vector3(posVector3.x, posVector3.y, transform.position.z);
        transform.localPosition += Vector3.up * 200f;
        gameObject.SetActive(true);
        new CoroutineUtil(UpdateShow(amendValues));
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    /// <param name="amendValues"></param>
    private IEnumerator UpdateShow(List<float> amendValues)
    {
        _moves.Clear();
        for (int i = 0; i < amendValues.Count; i++)
        {
            float value = amendValues[i];
            if (value == 0)
            {
                continue;
            }
            switch ((AmendAttributeType)i)
            {
                case AmendAttributeType.Shield:
                    LoadRes(string.Format(shieldStr, GetSymbol(value), (int)(value * 100f)), i);
                    break;
                case AmendAttributeType.Armor:
                    LoadRes(string.Format(armorStr, GetSymbol(value), (int)(value * 100f)), i);
                    break;
                case AmendAttributeType.HP:
                    LoadRes(string.Format(hpStr, GetSymbol(value), (int)(value * 100f)), i);
                    break;
                case AmendAttributeType.ATK:
                    LoadRes(string.Format(atkStr, GetSymbol(value), (int)(value * 100f)), i);
                    break;
                case AmendAttributeType.EXP:
                    LoadRes(string.Format(expStr, GetSymbol(value), (int)(value)), i);
                    break;
            }

            int sum = 0;
            while (sum < 35)
            {
                sum++;
                yield return null;
            }
        }

        while (_moves.Count > 0 && _moves.Any(a => a.IsMove))
        {
            yield return null;
        }
        gameObject.SetActive(false);
        if (OnPlayEnd != null)
        {
            OnPlayEnd(Param);
        }
        OnPlayEnd = null;
    }

    private void LoadRes(string msg, int index)
    {
        _moves.Add(ResourceLoadUtil.InstantiateRes(_intro, _list).AddComponent<UIAmendAttributeMove>());
        _moves.Last().OpenUI(msg, GetColor((AmendAttributeType)index));
    }

    private Color GetColor(AmendAttributeType type)
    {
        switch (type)
        {
            case AmendAttributeType.EXP:
                return new Color(69 / 255f, 253 / 255f, 255 / 255f);
            case AmendAttributeType.Shield:
                return new Color(69 / 255f, 253 / 255f, 255 / 255f);
            case AmendAttributeType.Armor:
                return new Color(255 / 255f, 253 / 255f, 73 / 255f);
            case AmendAttributeType.HP:
                return new Color(76 / 255f, 255 / 255f, 72 / 255f);
            case AmendAttributeType.ATK:
                return new Color(240 / 255f, 40 / 255f, 50 / 255f);
            default:
                return new Color(69 / 255f, 253 / 255f, 255 / 255f);
        }
    }

    private string GetSymbol(float value)
    {
        return value > 0 ? "+" : "-";
    }

    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }
        _intro = transform.Find("Temp/Intro").gameObject;
        _list = transform.Find("List");
        //
        _isFirst = true;
    }


    //
    private readonly bool _isMove;
    private bool _isFirst;
    private const string atkStr = "攻击 {0}{1}%";
    private const string hpStr = "生命 {0}{1}%";
    private const string armorStr = "护甲 {0}{1}%";
    private const string shieldStr = "护盾 {0}{1}%";
    private const string expStr = "经验 {0}{1}";
    //
    private List<UIAmendAttributeMove> _moves = new List<UIAmendAttributeMove>();
    //
    private GameObject _intro;
    private Transform _list;

    //
    private enum AmendAttributeType
    {
        EXP = 0,
        Shield = 1,
        Armor = 2,
        HP = 3,
        ATK = 4,
    }

}


public class UIAmendAttributeMove : MonoBehaviour
{
    public bool IsMove { get { return _isMove; } }

    public void OpenUI(string msg, object color = null)
    {
        _isMove = true;
        Text text = transform.GetComponent<Text>();
        text.text = msg;
        if (color != null)
        {
            text.color = (Color)color;
        }
        _canvasGroup = transform.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        _IEMove = new CoroutineUtil(UpdateShow());
    }

    private IEnumerator UpdateShow()
    {
        _canvasGroup.alpha = 1;
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime;
            transform.localPosition += Vector3.up * 1f;
            if (_canvasGroup.alpha <= 0.5f)
            {
                _isMove = false;
            }
            yield return null;
        }
        _isMove = false;
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        _IEMove.Stop();
    }



    //
    private CoroutineUtil _IEMove;
    private bool _isMove;
    //
    private CanvasGroup _canvasGroup;
}
