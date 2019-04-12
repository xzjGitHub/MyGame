using GameEventDispose;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Title: MonoBehaviour
{
    private GameObject m_nameObj;

    //
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
        InitObj();
        Utility.AddButtonListener(transform.Find("Back/Btn"),backAction);
        m_nameObj.gameObject.SetActive(true);
    }


    //2018/08/10 星期一
    //暂停中  
    private void InitObj()
    {
        m_nameObj = transform.Find("Name").gameObject;
        m_nameObj.SetActive(false);

        m_btn = transform.Find("Time/BtnBg/Sprite").GetComponent<Image>();
        m_coin = transform.Find("Coin/Num").GetComponent<Text>();
        m_mana = transform.Find("Mana/Num").GetComponent<Text>();
        m_time = transform.Find("Time/Time").GetComponent<Text>();
        m_name = transform.Find("Name/Name").GetComponent<Text>();

        m_startSpri = SpriteManager.Instance.GetSprite(
            StringDefine.SpriteTypeNameDefine.Other,StringDefine.SpriteNameDefine.Start);
        m_stopSpri = SpriteManager.Instance.GetSprite(StringDefine.SpriteTypeNameDefine.Other,
            StringDefine.SpriteNameDefine.Stop);
        Utility.AddButtonListener(transform.Find("Time/BtnBg/Btn"),OnBtnClick);

        EventDispatcher.Instance.SystemEvent.AddEventListener<GameSystemEventType>(EventId.SystemEvent,PlayerInfoChange);
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);

        UpdateCoin();
        UpdateMana();
        UpdateTime();
        UpdateSprite();
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
        m_nameObj.SetActive(show);
        if(show)
            m_name.text = name;
    }
}
