using System;
using UnityEngine;
using System.Collections;
using Spine.Unity;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public SkeletonAnimation skeletonAnimation;
    public Text introText;
    public float delayedTime = 1;
    public float aspd = 1;
    public bool isOpen;
    public bool isClose;
    //
    private bool isFirst;
    private const string intro1 = "Loading";
    private const string intro2 = ".";
    private const int maxSum = 6;
    private int sum;
    private string tempIntro;
    private float time;

    private void Init()
    {
        if (isFirst)return;

        isFirst = true;
    }


    public void Show()
    {
        
    }

    public void Close()
    {
        
    }



    void Update()
    {
        UpdateOpen();
        UpdateClose();
        UpdateShowIntro();
    }

    void UpdateOpen()
    {
        if (!isOpen) return;
        skeletonAnimation.skeleton.A += Time.deltaTime * aspd;
        canvasGroup.alpha += Time.deltaTime * aspd;
        if (canvasGroup.alpha >= 1 || skeletonAnimation.skeleton.A >= 1)
        {
            isOpen = false;
            canvasGroup.alpha = 1;
            skeletonAnimation.skeleton.A = 1;
        }

    }

    void UpdateClose()
    {
        if (!isClose) return;
        skeletonAnimation.skeleton.A -= Time.deltaTime * aspd;
        canvasGroup.alpha -= Time.deltaTime * aspd;
        if (canvasGroup.alpha < 0 || skeletonAnimation.skeleton.A < 0)
        {
            isClose = false;
            canvasGroup.alpha = 0;
            skeletonAnimation.skeleton.A = 0;
        }
    }


    void UpdateShowIntro()
    {
        time += Time.deltaTime;
        if (time >= delayedTime)
        {
            time = 0; sum++;
            if (sum > +maxSum)
            {
                sum = 0;
            }
            introText.text = String.Format(intro1 + GetIntro2(sum));
        }
    }

    string GetIntro2(int _sum)
    {
        tempIntro=String.Empty;
        for (int i = 0; i < sum; i++)
        {
            tempIntro += intro2;
        }
        return tempIntro;
    }

}
