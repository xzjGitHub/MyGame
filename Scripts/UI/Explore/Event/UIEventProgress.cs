﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIEventProgress : MonoBehaviour
{
    public delegate void CallBack(object param);

    public CallBack OnProgressAchieve;
    public object param;


    /// <summary>
    /// 打开UI
    /// </summary>
    public void OpenUI(float spd = 1)
    {
        if (spd == 0)
        {
            if (OnProgressAchieve != null)
            {
                OnProgressAchieve(param);
            }

            return;
        }
        aspd = spd;
        gameObject.SetActive(false);
        if (valueImage == null)
        {
            valueImage = transform.Find("value").GetComponent<Image>();
        }
        //
        valueImage.fillAmount = 0;
        //
        gameObject.SetActive(true);
        IE_UpdateProgressShow = new CoroutineUtil(UpdateProgressShow());
    }
    /// <summary>
    /// 停止打开
    /// </summary>
    public void StopOpen()
    {
        StopAllCoroutine();
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 更新进度条
    /// </summary>
    private IEnumerator UpdateProgressShow()
    {

        valueImage.fillAmount = aspd <= 0 ? 1 : 0;
        //
        while (valueImage.fillAmount < 1)
        {
            valueImage.fillAmount += Time.deltaTime / aspd;
            yield return null;
        }
        if (OnProgressAchieve != null)
        {
            OnProgressAchieve(param);
        }
        //
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_UpdateProgressShow != null)
        {
            IE_UpdateProgressShow.Stop();
        }

        IE_UpdateProgressShow = null;
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        StopAllCoroutine();
    }


    private float aspd = 1;
    //
    private Image valueImage;
    //
    private CoroutineUtil IE_UpdateProgressShow;
}
