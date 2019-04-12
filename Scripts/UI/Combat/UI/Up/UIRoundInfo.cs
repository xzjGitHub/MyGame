using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRoundInfo : MonoBehaviour
{
    private Text roundText;
    private string m_round = "第<color=#fffff>{0}</color>回合";

    public void UpateShow(int round)
    {
        if (roundText == null)
        {
            roundText = transform.Find("Text").GetComponent<Text>();
        }
        //
        roundText.text = string.Format(m_round, round.ToString("00"));
    }


}
