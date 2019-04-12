using System;
using UnityEngine;
using UnityEngine.UI;

namespace Barrack.View
{
    public class CharTrainTip: MonoBehaviour
    {

        private Image m_charHeadIcon;
        private Text m_getExp;
        private Text m_manaCost;
        private Text m_remainDays;

        private Action m_endTrainAction;
        private Action m_useTokenAction;
        private Action m_closeAction;

        private bool m_hasInit;

        private string m_fromat = "距离达到下级，还需要{0}日";

        public void InitComponent(Action enAction,Action useTokenAction,Action closeAction)
        {
            m_charHeadIcon = transform.Find("Center/CharInfo/Icon").GetComponent<Image>();
            m_getExp = transform.Find("Center/Exp/Content/Num").GetComponent<Text>();
            m_manaCost = transform.Find("Center/ManaCost/Content/Num").GetComponent<Text>();
            m_remainDays = transform.Find("Center/RemainDays").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Center/EndTrain"),ClickEndTrain);
            Utility.AddButtonListener(transform.Find("Center/Token"),ClickUseToken);
            Utility.AddButtonListener(transform.Find("Center/Back"),ClickClose);

            m_endTrainAction = enAction;
            m_useTokenAction = useTokenAction;
            m_closeAction = closeAction;
        }

        public void UpdateInfo(string charIcon,float addExp, float manaCost, int remianDays)
        {
            m_charHeadIcon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon,charIcon);
            m_getExp.text=addExp.ToString();
            m_manaCost.text = manaCost.ToString();
            m_remainDays.text = string.Format(m_fromat, remianDays);
        }

        private void ClickEndTrain()
        {
            if (m_endTrainAction != null)
            {
                m_endTrainAction();
            }
        }

        private void ClickUseToken()
        {
            if (m_useTokenAction != null)
            {
                m_useTokenAction();
            }
        }

        private void ClickClose()
        {
            if(m_closeAction != null)
            {
                m_closeAction();
                gameObject.SetActive(false);
            }
        }

    }
}
