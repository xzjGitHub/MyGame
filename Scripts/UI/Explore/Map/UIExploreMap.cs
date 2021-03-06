﻿using System.Collections.Generic;
using UnityEngine;

public class UIExploreMap : MonoBehaviour
{
    public void InitMapInfo(Vector2 size)
    {
        _mapId = ExploreSystem.Instance.NowMapId;
        _WPId = ExploreSystem.Instance.NowWaypointId;
        //
        LoadRes(_mapId);
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }
    public void StopMoveMap()
    {
        _isMove = false;
    }
    public void MoveMap(float moveSpeed, bool isLeft = true)
    {
        _isMove = true;
        _moveSpeed = moveSpeed;
        _isLeft = isLeft;
    }



    private void LoadRes(int mapID)
    {
        if (_isFirst)
        {
            transform.GetChild(0).rectTransform().anchoredPosition = Vector2.zero;
            return;
        }
        ResourceLoadUtil.DeleteChildObj(transform);
        GameObject obj = ResourceLoadUtil.LoadExploreBigMap(mapID, transform);
        obj.transform.rectTransform().anchoredPosition = Vector2.zero;
        foreach (Transform item in obj.transform)
        {
            UIExploreMapMove tempExploreMapMove = item.gameObject.AddComponent<UIExploreMapMove>();
            if (!item.name.Contains("Auto"))
            {
                tempExploreMapMove.aspdRate = float.Parse(item.name)/* _mapMoveAspds[item.GetSiblingIndex()]*/;
            }
            _mapMoves.Add(tempExploreMapMove);
        }

        _isFirst = true;
    }

    private void Update()
    {
        if (!_isMove)
        {
            return;
        }

        for (int i = 0; i < _mapMoves.Count; i++)
        {
            _mapMoves[i].UpdateSartMove(_moveSpeed, _isLeft);
        }
    }


    //
    private List<UIExploreMapMove> _mapMoves = new List<UIExploreMapMove>();
    //
    private bool _isFirst;
    private int _mapId;
    private int _WPId;
    private bool _isMove;
    private float _moveSpeed;
    private bool _isLeft;
    //
    private readonly WPAttribute _wpAttribute;
    private readonly float[] _mapMoveAspds = { 0.6f, 0.75f, 0.75f, 1f, 1f, 1.2f };
}
