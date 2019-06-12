using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 探索事件选项
/// </summary>
public class UIEventSelection : MonoBehaviour
{
    public long SlecetionID { get { return _selectionAttribute.SelectionID; } }
    /// <summary>
    /// 访问
    /// </summary>
    public Action<SelectionAttribute> OnClickSelection;


    /// <summary>
    /// 新建
    /// </summary>
    public void CreateSelection(EventAttribute eventAttribute, SelectionAttribute selectionAttribute)
    {
        _eventAttribute = eventAttribute;
        _selectionAttribute = selectionAttribute;
        _selection = selectionAttribute.event_selection;
        //
        UpdateShow();
    }

    /// <summary>
    /// 更新显示
    /// </summary>
    private void UpdateShow()
    {
        gameObject.SetActive(false);
        ButtonInit();
        if (_eventIntro != null)
        {
            Text_template template = Text_templateConfig.GetText_config(_selection.selectionText);
            _eventIntro.text = template == null ? string.Empty : template.text;
        }
        switch (_selectionAttribute.SelectionType)
        {
            case SelectionType.Default:
            case SelectionType.Base:
                UpdateLevelShow();
                break;
            case SelectionType.Page:
                return;
            case SelectionType.Combat:
                UpdateCombatShow();
                break;
            case SelectionType.Ignore:
            case SelectionType.Back:
                return;
            case SelectionType.Resurrection:
                transform.Find("Resurrection").gameObject.SetActive(true);
                break;
            case SelectionType.Pullout:
                transform.Find("Pullout").gameObject.SetActive(true);
                return;
        }
        UpdateConsumetShow();
        new CoroutineUtil(IEUpdateShow());
    }


    private IEnumerator IEUpdateShow()
    {
        yield return null;
        UpdatePos();
    }

    private void UpdatePos()
    {
        switch (_selectionAttribute.SelectionType)
        {
            case SelectionType.Ignore:
            case SelectionType.Back:
                return;
            default:
                switch (transform.GetSiblingIndex())
                {
                    case 0:
                        transform.localPosition = _pos1;
                        break;
                    case 1:
                        transform.localPosition = _pos2;
                        break;
                    case 2:
                        transform.localPosition = _pos3;
                        break;
                }
                break;
        }
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 更新消耗显示
    /// </summary>
    private void UpdateConsumetShow()
    {
        //todo 数量是否够
        bool isConsumet = false;
        Transform consumetTransform = transform.Find("Consume");
        //物品
        int index = 0;

        foreach (System.Collections.Generic.List<int> itemCost in _selection.itemCost)
        {
            index++;
            if (itemCost.Count <= 0)
            {
                continue;
            }
            Item_instance instance = Item_instanceConfig.GetItemInstance(itemCost[0]);
            if (instance == null)
            {
                continue;
            }

            string str = "Item" + (index + 1);
            consumetTransform.Find(str).GetComponent<Image>().sprite =
                ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon, instance.instanceID);
            consumetTransform.Find(str).gameObject.SetActive(true);
            consumetTransform.Find(str + "/Sum").GetComponent<Text>().text = itemCost[1].ToString();
            isConsumet = true;
        }
        //金币、魔力、代币
        index = 0;
        foreach (int currencyCost in _selection.currencyCost)
        {
            if (currencyCost > 0)
            {
                consumetTransform.Find(GetStr(index)).gameObject.SetActive(true);
                consumetTransform.Find(GetStr(index) + "/Sum").GetComponent<Text>().text = currencyCost.ToString();
                isConsumet = true;
            }
            index++;
        }
        consumetTransform.gameObject.SetActive(isConsumet);
    }
    /// <summary>
    /// 更新难度等级显示
    /// </summary>
    private void UpdateLevelShow()
    {
        if (_selectionAttribute.Difficulty < 1000)
        {
            return;
        }
        Transform levelTransform = transform.Find("Level");
        levelTransform.Find("Text").GetComponent<Text>().text = _selectionAttribute.Difficulty.ToString();
        levelTransform.gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新战斗显示
    /// </summary>
    private void UpdateCombatShow()
    {
        Transform combaTransform = transform.Find("Combat");
        combaTransform.Find("Text").GetComponent<Text>().text = _selectionAttribute.MobLevel.ToString();
        combaTransform.gameObject.SetActive(true);
    }

    private string GetStr(int index)
    {
        switch (index)
        {
            case 1:
                return "Mana";
            case 2:
                return "Token";
            default:
                return "Gold";
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void ButtonInit(bool isBase = true)
    {
        switch (_selectionAttribute.SelectionType)
        {
            case SelectionType.Ignore:
            case SelectionType.Back:
                _button = transform.GetComponent<Button>();
                break;
            default:
                _button = transform.Find("Button").GetComponent<Button>();
                _eventIntro = transform.Find("Button/Intro").GetComponent<Text>();
                break;
        }
        if (!_selectionAttribute.IsCanUse())
        {
            _button.enabled = false;
            return;
        }
        //
        if (_button != null)
        {
            _button.onClick.AddListener(OnClickButton);
        }
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    private void OnClickButton()
    {
        if (OnClickSelection != null)
        {
            OnClickSelection(_selectionAttribute);
        }
    }

    //
    private Text _eventIntro;
    private Button _button;
    //
    private Event_selection _selection;
    //
    private EventAttribute _eventAttribute;
    private SelectionAttribute _selectionAttribute;
    //
    private Vector3 _pos1 = Vector3.up * 0f;
    private Vector3 _pos2 = Vector3.up* -70f;
    private Vector3 _pos3 = Vector3.up * -140f;
}