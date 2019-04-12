using UnityEngine;
using UnityEngine.UI;

namespace Barrack.View
{
    public class UseCharInfo: MonoBehaviour
    {
        private Text m_charNum;
        private Text m_info;

        public void InitComponent()
        {
            m_charNum = transform.Find("CharInfo/Num").GetComponent<Text>();
            m_info = transform.Find("GetInfo/Num").GetComponent<Text>();
        }

        public void UpdateInfo(string charNum,string detialInfo)
        {
            m_charNum.text = charNum;
            m_info.text = detialInfo;
        }

    }
}
