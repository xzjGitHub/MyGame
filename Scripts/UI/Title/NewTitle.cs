
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/17 17:21:45
//Note:     
//--------------------------------------------------------------

using GameEventDispose;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 标题类
/// </summary>
public class NewTitle: MonoBehaviour
{
    private GameObject m_nameText;
    private GameObject m_nameBg;

    private Image m_btn;
    private Text m_coin;
    private Text m_mana;
    private Text m_time;
    private Text m_name;

    private Sprite m_startSpri;
    private Sprite m_stopSpri;

    private bool m_isStart = true;

    private string m_timeFormat = "{0}\n{1}";

    public void Init(UnityAction backAction)
    {
        InitInfo(backAction);
    }

    private void InitInfo(UnityAction backAction)
    {
        m_nameText = transform.Find("Dynamic/Name").gameObject;
        m_nameBg = transform.Find("Static/NameBg").gameObject;

        m_btn = transform.Find("Dynamic/Time/Sprite").GetComponent<Image>();
        m_coin = transform.Find("Dynamic/CoinNum").GetComponent<Text>();
        m_mana = transform.Find("Dynamic/ManaNum").GetComponent<Text>();
        m_time = transform.Find("Dynamic/Time/Time").GetComponent<Text>();
        m_name = transform.Find("Dynamic/Name").GetComponent<Text>();

        m_startSpri = SpriteManager.Instance.GetSprite(
            StringDefine.SpriteTypeNameDefine.Other,StringDefine.SpriteNameDefine.Start);
        m_stopSpri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.Other,
            StringDefine.SpriteNameDefine.Stop);

        Utility.AddButtonListener(transform.Find("Dynamic/Time/Btn"),OnBtnClick);
        Utility.AddButtonListener(transform.Find("Dynamic/Back/Btn"),backAction);

        EventDispatcher.Instance.SystemEvent.AddEventListener<GameSystemEventType>(EventId.SystemEvent,PlayerInfoChange);
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);

        UpdateCoin();
        UpdateMana();
        UpdateTime();
        UpdateSprite();

        SetNameShow(false);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        EventDispatcher.Instance.SystemEvent.RemoveEventListener<GameSystemEventType>(EventId.SystemEvent,PlayerInfoChange);
    }

    private void PlayerInfoChange(GameSystemEventType type)
    {
        switch(type)
        {
            case GameSystemEventType.Gold:
                UpdateCoin();
                break;
            case GameSystemEventType.Mana:
                UpdateMana();
                break;
        }
    }

    private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1,object arg2)
    {
        if(arg1 == ScriptTimeUpdateType.Day)
            UpdateTime();
    }

    private void UpdateCoin()
    {
        int gold = ScriptSystem.Instance.Gold;
        if(gold >= 10000)
        {
            m_coin.text = "10K";
        }
        else
        {
            m_coin.text = gold.ToString();
        }
    }

    private void UpdateMana()
    {
        float mana = ScriptSystem.Instance.Mana;
        if(mana >= 10000)
        {
            m_mana.text = "10K";
        }
        else
        {
            m_mana.text = mana.ToString();
        }
    }

    private void UpdateTime()
    {
        if(m_isStart)
        {
            ShowNormalTime();
        }
    }

    private void ShowNormalTime()
    {
        string s1 = TimeUtil.GetXQDes();
        string s2 = TimeUtil.GetTimeDescription();
        m_time.text = string.Format(m_timeFormat,s1,s2);
    }

    private void OnBtnClick()
    {
        m_isStart = !m_isStart;
        if(m_isStart)
        {
            ScriptTimeSystem.Instance.StartTiming();
            ShowNormalTime();
        }
        else
        {
            ScriptTimeSystem.Instance.StopTiming();
            m_time.text = "暂停中";
        }
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        m_btn.sprite = m_isStart ? m_stopSpri : m_startSpri;
        m_btn.SetNativeSize();
    }

    public void UpdateName(bool show,string name)
    {
        SetNameShow(show);
        if(show)
            m_name.text = name;
    }

    private void SetNameShow(bool show)
    {
        m_nameText.SetActive(show);
        m_nameBg.SetActive(show);
    }
}

