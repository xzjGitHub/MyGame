using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class UIExploreItemMove : MonoBehaviour
{
    public float aspd = 10.5f;
    //
    private RectTransform rectTransform;
    private RectTransform itemRectTransform;
    private Transform itemTransform;
    private GameObject itemObj;
    //
    private Vector3 endVector1 = new Vector3(108, 330, 0);
    private Vector3 endVector2 = new Vector3(-360, -85, 0);
    private Vector3 localPosition;
    private Vector3 startVector3;
    //
    List<Transform> items = new List<Transform>();
    //
    private CoroutineUtil IE_UpdateMove;

    private void Awake()
    {
        itemTransform = transform.Find("ItemList");
        itemObj = transform.Find("Temp/Item").gameObject;
        itemRectTransform = transform.Find("ItemList").GetComponent<RectTransform>();
    }



    /// <summary>
    /// 更新物品移动位置
    /// </summary>
    public void UpdateItemPos(Vector2 _vector2)
    {
        gameObject.SetActive(true);
        itemRectTransform.anchoredPosition = _vector2;
    }

    /// <summary>
    /// 点击物品移动
    /// </summary>
    public void OnClickItemMove(object param)
    {
        IE_UpdateMove = new CoroutineUtil(UpdateMove(LoadEffect((Vector3)param)));
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 点击生命球移动
    /// </summary>
    public void OnClicHealingGlobMove(object param)
    {
        IE_UpdateMove = new CoroutineUtil(UpdateMove(LoadEffect((Vector3)param, false), false));
        gameObject.SetActive(true);
    }

    IEnumerator UpdateMove(Transform _transform, bool _isItem = true)
    {
        localPosition = _transform.localPosition;
        _transform.localPosition = new Vector3(localPosition.x, localPosition.y, 0);
        while (Vector3.SqrMagnitude(_transform.localPosition - (_isItem ? endVector1 : endVector2)) > 100)
        {
            _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, (_isItem ? endVector1 : endVector2),
                aspd);
            yield return null;
        }
        DestroyImmediate(_transform.gameObject);
    }

    private Transform LoadEffect(Vector3 _pos, bool _isItem = true)
    {
        GameObject _obj = ResourceLoadUtil.LoadRes<GameObject>("Exp/Event/Effect");
        _obj = ResourceLoadUtil.InstantiateRes(_obj);
        _obj = ResourceLoadUtil.ObjSetParent(_obj, transform);
        _obj.transform.position = _pos;
        if (!_isItem)
        {
            _obj.AddComponent<UIAlterParticleSystemLayer>().sortingLayer = "skill";
        }

        return _obj.transform;
    }


    public List<Transform> LoadItemReward(WPVisitEventResult _resul, Action<int> OnClickItem, Action<int> OnClickHealingGlob, bool _isDel = false)
    {
        List<Transform> items = new List<Transform>();
        gameObject.SetActive(true);
        //  SetItemParent();
        if (_isDel)
        {
            DelItem();
            items.Clear();
        }
        //
        foreach (var item in _resul.itemRewards)
        {
            items.Add(LoadItem(item, OnClickItem));
        }
        //
        for (int i = 0; i < _resul.healingGlobSum; i++)
        {
            items.Add(LoadHealingGlob(_resul.itemRewards.Count, OnClickHealingGlob));
        }
        return items;
    }



    /// <summary>
    /// 加载物品
    /// </summary>
    private Transform LoadItem(ItemData data, Action<int> OnClickItem)
    {
        int _index = items.Count;
        //
        var _obj = ResourceLoadUtil.InstantiateRes(itemObj, itemTransform);
        _obj.AddComponent<UIExploreItem>().Init(data);
        //_obj.name = data.itemID.ToString();
        //_obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate { OnClickItem(_index); });
        ////
        //_obj.transform.Find("Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
        //    data.instanceID);
        ////
        //if (data.itemQuality == 2 || data.itemQuality == 3 || data.itemQuality == 4)
        //{
        //    _obj.transform.Find("Effect").Find(2.ToString()).gameObject.SetActive(true);
        //    _obj.transform.Find("Effect").Find(data.itemQuality.ToString()).gameObject.SetActive(true);
        //}
        //switch ((ItemType)data.itemType)
        //{
        //    case ItemType.WuQi:
        //    case ItemType.KuiJia:
        //    case ItemType.ShiPing:
        //        if (data.resultItemLevel > TeamSystem.Instance.GetTeamEquipMaxLevel()) _obj.transform.Find("Icon/Up").gameObject.SetActive(true);
        //        break;
        //}


        //ItemFactory.Instance.CreateItem(data, itemTransform, Vector3.one * 0.7f, false, false, true, delegate
        //{
        //    OnClickItem(_index);
        //});

        _obj.SetActive(false);
        items.Add(_obj.transform);

        return _obj.transform;
        return items.Last();
    }

    /// <summary>
    /// 加载生命球
    /// </summary>
    private Transform LoadHealingGlob(int _index, Action<int> OnClickHealingGlob, GameObject _PhysicsItem = null)
    {
        GameObject _obj;
        if (_PhysicsItem != null)
        {
            _obj = ResourceLoadUtil.InstantiateRes(_PhysicsItem);
            _obj = ResourceLoadUtil.ObjSetParent(_obj, itemTransform);
            LoadHealingGlobRes(OnClickHealingGlob, _index, _obj.transform);
            _obj.SetActive(false);
        }
        else
        {
            _obj = LoadHealingGlobRes(OnClickHealingGlob, _index, itemTransform);
        }
        _obj.SetActive(false);
        items.Add(_obj.transform);
        return _obj.transform;
        return items.Last();
    }


    private GameObject LoadHealingGlobRes(Action<int> OnClickHealingGlob, int _index, Transform _transform)
    {
        GameObject _obj = ResourceLoadUtil.LoadHealingGlobRes<GameObject>();
        _obj = ResourceLoadUtil.InstantiateRes(_obj);
        _obj = ResourceLoadUtil.ObjSetParent(_obj, _transform);
        _obj.GetComponent<Button>().onClick.AddListener(delegate { OnClickHealingGlob(_index); });
        return _obj;
    }


    public void DelItem()
    {
        items.Clear();
        foreach (Transform item in itemTransform)
        {
            items.Add(item);
        }
        //
        foreach (Transform item in items)
        {
            if (item.name.Contains("Item"))
            {
                //ItemFactory.Instance.Release(item.gameObject);
                continue;
            }
            DestroyImmediate(item.gameObject);
        }
        items.Clear();
    }



    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_UpdateMove != null) IE_UpdateMove.Stop();
        IE_UpdateMove = null;
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        StopAllCoroutine();
    }

}
