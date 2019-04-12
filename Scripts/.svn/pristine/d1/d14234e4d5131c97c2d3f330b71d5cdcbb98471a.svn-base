using Core.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TrainCharItem:MonoBehaviour
{
    private GameObject m_select;

    private Image m_headIcon;
    private Image m_slider;
    private Text m_exp;
    private Text m_progress;
    private Text m_level;

    private TraniCharInfo m_info;

    private bool m_hasInit;

    private Action<int> m_clickAction;

    private void InitComponent()
    {
        m_select = transform.Find("Select").gameObject;

        m_headIcon = transform.Find("Char/Icon").GetComponent<Image>();
        m_slider = transform.Find("Slider/Slider").GetComponent<Image>();
        m_exp = transform.Find("Slider/Exp").GetComponent<Text>();
        m_level = transform.Find("Level").GetComponent<Text>();
        m_progress = transform.Find("Slider/Progress").GetComponent<Text>();

        Utility.AddButtonListener(transform.Find("Btn"), Click);

        m_hasInit = true;
    }

    private void OnEnable()
    {
        ControllerCenter.Instance.BarrackController.ExpChange += OnExpChange;
    }

    private void OnDisable()
    {
        ControllerCenter.Instance.BarrackController.ExpChange -= OnExpChange;
    }

    public void InitInfo(TraniCharInfo info,Action<int> clickAction)
    {
        m_info = info;
        m_clickAction = clickAction;

        if (!m_hasInit)
        {
            InitComponent();
        }

        CharAttribute attr = CharSystem.Instance.GetCharAttribute(info.CharId);
        m_headIcon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon,attr.char_template.HeadIcon);

        UpdatText();
        UpdateSlectShow(false);
    }

    private void UpdatText()
    {
        CharAttribute attr = CharSystem.Instance.GetAttribute(m_info.CharId);
        int nextNeed = ControllerCenter.Instance.BarrackController.GetNextLevelExp(attr);
        m_exp.text = "经验: " +attr.charExp + "/" + nextNeed;
        Core_lvup core = CoreSystem.Instance.GetCoreLvup();
        m_level.text = "Lv." + attr.charLevel+"/"+core.trainningMaxLevel;
        float value = attr.charExp/nextNeed;
        m_slider.fillAmount = value;
        m_progress.text = (int) (value*100)/100f*100 + "%";
    }


    private void OnExpChange(int id)
    {
        if (id == m_info.CharId)
        {
            UpdatText();
        }
    }

    public void UpdateSlectShow(bool show)
    {
        m_select.SetActive(show);
    }

    private void Click()
    {
        if (m_clickAction != null)
        {
            m_clickAction(m_info.CharId);
        }
    }

}

