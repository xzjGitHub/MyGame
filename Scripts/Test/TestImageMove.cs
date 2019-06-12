﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestImageMove : MonoBehaviour
{
    public bool isMove;
    public float moveTime=1f;
    public float maxValue;
    public Image image;
    public float aspd;
    private float time;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove) return;
        StartCoroutine(IEMove());
        isMove = false;
    }

    IEnumerator IEMove()
    {
        image.fillAmount = 0;
        aspd = aspd * maxValue / moveTime;

        while (image.fillAmount < maxValue)
        {
            time += Time.deltaTime;
            LogHelper_MC.LogWarning(time);
            image.fillAmount += Time.deltaTime * aspd;
            yield return null;
        }

        yield return null;

        time = 0;
    }
}
