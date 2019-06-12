﻿using UnityEngine;

public class UIExploreMapMove : MonoBehaviour
{
    public float aspdRate = 1;

    private void Awake()
    {
        Init();
    }



    private void Init()
    {
        if (!_isFirst)
        {
            _isAuto = transform.name.Contains("Auto");
            aspdRate = float.Parse(transform.name.Replace("Auto", ""));
            //
            if (_rectTrans == null)
            {
                _rectTrans = transform.GetComponent<RectTransform>();
                _width = _rectTrans.rect.width ;
            }
        }
        _isFirst = true;
    }


    private void Update()
    {
        if (!_isAuto)
        {
            return;
        }
        Init();


        if (_rectTrans.anchoredPosition.x <= -_width)
        {
            _rectTrans.anchoredPosition = Vector2.zero;
        }
        _rectTrans.anchoredPosition =// Vector3.Lerp(_rectTrans.anchoredPosition, _sceneLeft, x);
            Vector3.MoveTowards(_rectTrans.anchoredPosition, _sceneLeft,/* distance /*/ 300 * aspdRate * Time.deltaTime);
    }

    public void UpdateSartMove(float moveSpeed, bool isLeft = true)
    {
        if (_isAuto)
        {
            return;
        }
        Init();
        //if (_rectTrans == null)
        //{
        //    Transform temp = transform.GetChild(0);

        //    _width = temp.rectTransform().rect.width * temp.localScale.x;
        //    _rectTrans = transform.GetComponent<RectTransform>();
        //}

        if (!isLeft)
        {

            if (_rectTrans.anchoredPosition.x <= -_width)
            {
                _rectTrans.anchoredPosition = Vector2.zero;
            }
        }
        else
        {

            if (_rectTrans.anchoredPosition.x >= 0)
            {
                _rectTrans.anchoredPosition = new Vector2(-_width, 0);
            }
        }



        _rectTrans.anchoredPosition =// Vector3.Lerp(_rectTrans.anchoredPosition, _sceneLeft, x);
        Vector3.MoveTowards(_rectTrans.anchoredPosition, !isLeft ? _sceneLeft : _sceneRight,/* distance /*/ moveSpeed * aspdRate * Time.deltaTime);
    }


    //
    private bool _isFirst;
    public bool _isAuto;
    private RectTransform _rectTrans;
    private Vector2 _sceneLeft = Vector2.left * 10000;
    private Vector2 _sceneRight = Vector2.right * 10000;
    private float _width;

}
