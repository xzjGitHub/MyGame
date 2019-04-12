using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 探索事件选项
/// </summary>
public class UIExploreEventSelection : MonoBehaviour
{
    /// <summary>
    /// 访问
    /// </summary>
    public Action<SelectionAttribute> OnVisit;



    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="eventAttribute"></param>
    public void NewCreate(EventAttribute eventAttribute, int phase, SelectionAttribute selectionAttribute)
    {
        Initialization();
        this.eventAttribute = eventAttribute;
        this.selectionAttribute = selectionAttribute;
        selectionID = selectionAttribute.SelectionID;
        this.phase = phase;
        selection = selectionAttribute.event_selection;
        //
        // icon.sprite=
        selectionName.text = selection.selectionName;
        eventIntro.text = eventAttribute.event_template.eventInfo1;
    }


    /// <summary>
    /// 初始化
    /// </summary>
    private void Initialization()
    {
        Transform intro = transform.Find("Intro");
        icon = intro.Find("Icon").GetComponent<Image>();
        selectionName = intro.Find("Name").GetComponent<Text>();
        eventIntro = intro.Find("Text").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
        //
        button.onClick.AddListener(OnClickButton);
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    private void OnClickButton()
    {
        if (OnVisit != null)
        {
            OnVisit(selectionAttribute);
        }
    }

    //
    private EventAttribute eventAttribute;
    private readonly WPEventSelectionType selectionType;
    private SelectionAttribute selectionAttribute;
    private readonly int selectionValue;
    private int selectionID;
    private int phase;
    //
    private Image icon;
    private Text selectionName;
    private Text eventIntro;
    private Button button;
    //
    private Event_selection selection;
}

/// <summary>
/// 路点事件选项类型
/// </summary>
public enum WPEventSelectionType
{
    /// <summary>
    /// 深入
    /// </summary>
    Next = 1,
    /// <summary>
    /// 高级深入
    /// </summary>
    AdvancedNext = 2,
    /// <summary>
    /// 奖励
    /// </summary>
    Award = 3,
    /// <summary>
    /// 大奖
    /// </summary>
    SuperAward = 4,
    /// <summary>
    /// 战斗
    /// </summary>
    Combat = 5,
    /// <summary>
    /// 贿赂
    /// </summary>
    Bribery = 6,
    /// <summary>
    /// 召唤
    /// </summary>
    Summon = 7,
    /// <summary>
    /// 草药
    /// </summary>
    Herb = 8,
}
