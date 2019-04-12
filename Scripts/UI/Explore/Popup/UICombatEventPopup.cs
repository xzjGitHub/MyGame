using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICombatEventPopup : MonoBehaviour
{
    public delegate void CallBack();

    public CallBack OnPlayFinish;
    //
    private Button startCombatButton;
    private Image startCombatImage;
    //
    private bool isFirst;
    //
    private UICombatStartAnimation combatStartAnimation;


    public void Show()
    {
        Init();
        //
        startCombatImage.color = new Color(1, 1, 1, 0);
        startCombatButton.gameObject.SetActive(true);
        //
        startCombatImage.DOFade(1, 0.5f);
        gameObject.SetActive(true);
    }

    private void Init()
    {
        if (isFirst) return;
        //
        combatStartAnimation = transform.Find("CombatStart").GetComponent<UICombatStartAnimation>();
        combatStartAnimation.OnPlayEnd = OnPlayEnd;
        //
        startCombatButton = transform.Find("StartCombat").GetComponent<Button>();
        startCombatImage = startCombatButton.GetComponent<Image>();
        startCombatButton.onClick.AddListener(OnClickStartCombat);
        //
        //
        isFirst = true;
    }


    private void OnPlayEnd()
    {
        if (OnPlayFinish != null)
        {
            OnPlayFinish();
        }
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 点击了开始战斗
    /// </summary>
    private void OnClickStartCombat()
    {
        startCombatImage.color = new Color(1, 1, 1, 0);
        combatStartAnimation.gameObject.SetActive(true);
        ScriptTimeSystem.Instance.StopTiming();
    }



}
