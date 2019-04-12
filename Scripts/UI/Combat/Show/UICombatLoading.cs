using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UICombatLoading : MonoBehaviour
{

    public Action OnLoadOK;
    public void Init()
    {
        if (isFirst) return;
        value = transform.Find("Value").GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        isFirst = true;
    }

    public void StartLoad()
    {
        value.fillAmount = 0;
        text.text = "0%";
        gameObject.SetActive(true);
        new CoroutineUtil(StartUpdate());
    }

    private IEnumerator StartUpdate()
    {
        float num = 0;
        while (num < 100)
        {
            num += Time.deltaTime * 50;
            value.fillAmount = num / 100;
            text.text = (int)((num / 100) * 100) + "%";
            yield return null;
        }
        yield return null;
        gameObject.SetActive(false);
        if (OnLoadOK != null) OnLoadOK();
    }


    //
    private Image value;
    private Text text;
    private bool isFirst;
}
