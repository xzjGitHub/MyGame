using UnityEngine;
using UnityEngine.UI;

public class UIExitCombat : MonoBehaviour
{
    public void Init(int retreatReq)
    {
        this.retreatReq = retreatReq;
        if (!isFirst)
        {
            GetObj();
            UpdateShow(0);
        }
        //
        isFirst = true;
    }

    public void UpdateShow(int nowRound)
    {
        //
        button.enabled = nowRound - retreatReq > 0;
        grayObj.SetActive(nowRound - retreatReq < 0);
        if (nowRound >= retreatReq)
        {
            coolDownInfo.gameObject.SetActive(false);
        }
        coolDownInfo.UpdateValue(nowRound);
    }

    private void GetObj()
    {
        //
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(OnClickButton);
        //
        grayObj = transform.Find("Gray").gameObject;
        Transform obj = transform.Find("CoolDown");
        coolDownInfo = obj.GetComponent<UICoolDownInfo>();
        if (coolDownInfo == null)
        {
            coolDownInfo = obj.gameObject.AddComponent<UICoolDownInfo>();
            coolDownInfo.InitInfo(retreatReq);
        }
    }

    private void OnClickButton()
    {

    }

    //
    private bool isFirst;
    private int retreatReq;
    //
    private Button button;
    private GameObject grayObj;
    //
    private UICoolDownInfo coolDownInfo;
}
