using Core.Data;
using EventCenter;
using UnityEngine;
using UnityEngine.UI;

public class CoreLevelUp: MonoBehaviour
{
    private GameObject m_bgSpe;
    private GameObject m_foreSpe;

    private Image m_fore;
    private Text m_level;

    private SliderPos m_sliderPos;
    private Core_lvup m_now_core_Lvup;

    private void Awake()
    {
        m_bgSpe = transform.Find("BgSpecial").gameObject;
        m_foreSpe = transform.Find("Exp/ForeSpe").gameObject;
        m_fore = transform.Find("Exp/Fore").GetComponent<Image>();
        m_level = transform.Find("Exp/Num").GetComponent<Text>();

        m_sliderPos = transform.Find("Exp/Mask/Eff").GetComponent<SliderPos>();

        EventManager.Instance.RegEventListener(EventSystemType.UI,
           EventTypeNameDefine.UpdateCoreLevelInfo,UpdateInfo);

        UpdateInfo();
    }

    private void OnDestroy()
    {
        EventManager.Instance.UnRegEventListener(EventSystemType.UI,
          EventTypeNameDefine.UpdateCoreLevelInfo,UpdateInfo);
    }

    private void UpdateInfo()
    {
        float havePower = CoreSystem.Instance.GetPower();
        float nextLevelNeed = ControllerCenter.Instance.CoreController.GetNextLevelNeed();

        if(nextLevelNeed == -1)
        {
            m_bgSpe.SetActive(false);
            m_foreSpe.SetActive(false);
            m_sliderPos.gameObject.SetActive(false);
            m_fore.fillAmount = 1f;
        }
        else
        {
            m_bgSpe.SetActive(havePower >= nextLevelNeed);
            m_foreSpe.SetActive(havePower >= nextLevelNeed);

            if(havePower < nextLevelNeed)
            {
                m_fore.fillAmount = havePower / nextLevelNeed;
                m_sliderPos.UpdatePos();
            }
        }

        m_level.text = "Lv." + CoreSystem.Instance.GetLevel();
    }

}

