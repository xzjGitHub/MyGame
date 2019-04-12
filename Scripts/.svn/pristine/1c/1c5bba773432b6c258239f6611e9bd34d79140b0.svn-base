using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 探索随机地图弹窗
/// </summary>
public class UIExploreMinMapPopup : MonoBehaviour
{
    private Transform directionResTransform;
    private Transform mapListTransform;
    private GameObject mapRes;
    //
    private Dictionary<int, RectTransform> DirectionRes = new Dictionary<int, RectTransform>();
    private Dictionary<int, Transform> maps = new Dictionary<int, Transform>();
    //
    private bool isFirst;
    private float distance;
    //
    private const float mapWidth = 80;
    private const float mapDistance = 60;
    //
    private CoroutineUtil IE_CreateDirection;
    private CoroutineUtil IE_CreateMap;


    void Start()
    {
        distance = mapWidth + mapDistance;
        //
        Init();
        IE_CreateMap = new CoroutineUtil(CreateMap(0));
        //
    }


    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (isFirst) return;

        GetObj();
        GetDirectionRes();
        //
        isFirst = true;
    }
    /// <summary>
    /// 获得方向资源
    /// </summary>
    private void GetDirectionRes()
    {
        DirectionRes.Clear();
        RectTransform _tempTransform;
        for (int i = 0; i < directionResTransform.childCount; i++)
        {
            _tempTransform = directionResTransform.GetChild(i).GetComponent<RectTransform>();

            DirectionRes.Add(int.Parse(_tempTransform.name), _tempTransform);
        }
    }

    /// <summary>
    /// 获得组件
    /// </summary>
    void GetObj()
    {
        mapListTransform = transform.Find("MapList/List/List");
        directionResTransform = transform.Find("DirectionRes");
        mapRes = transform.Find("Map").gameObject;
    }


    IEnumerator CreateMap(int _mapId)
    {
        List<RandomMapConfig> _list = RandomMapConfigConfig.GetRandomMapConfig(_mapId);
        maps.Clear();
        yield return null;
        GameObject _obj;
        RectTransform rectTransform;
        for (int i = 0; i < _list.Count; i++)
        {
            _obj = ResourceLoadUtil.InstantiateRes(mapRes);
            ResourceLoadUtil.ObjSetParent(_obj, mapListTransform);
            _obj.transform.Find("Text").GetComponent<Text>().text = _list[i].WPId.ToString();
            //
            rectTransform = _obj.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.up * 1;
            rectTransform.anchorMax = Vector2.up * 1;
            rectTransform.pivot = Vector2.up * 1;
            rectTransform.anchoredPosition = Vector2.right * ((_list[i].column - 1) * distance) + Vector2.down * ((_list[i].row - 1) * distance);
            //
            // _obj.gameObject.SetActive(true);
            //
            maps.Add(_list[i].WPId, _obj.transform);
            yield return null;
        }
        //
        rectTransform = mapListTransform.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2((_list[0].columnSum - 1) * distance + mapWidth, (_list[0].rowSum - 1) * distance + mapWidth);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
        //
        IE_CreateDirection = new CoroutineUtil(CreateDirection(_list));
    }

    IEnumerator CreateDirection(List<RandomMapConfig> _list)
    {
        GameObject _obj;
        RectTransform rectTransform;
        int _direction;
        foreach (var item in _list)
        {
            foreach (var _parent in item.parentInfos)
            {
                _direction = _parent[1] * 10 + _parent[2];
                if (!DirectionRes.ContainsKey(_direction)) continue;
                _obj = ResourceLoadUtil.InstantiateRes(DirectionRes[_direction].gameObject);
                ResourceLoadUtil.ObjSetParent(_obj, maps[_parent[0]]);
                //
                rectTransform = _obj.GetComponent<RectTransform>();
                rectTransform.anchorMin = DirectionRes[_direction].anchorMin;
                rectTransform.anchorMax = DirectionRes[_direction].anchorMax;
                rectTransform.pivot = DirectionRes[_direction].pivot;
                rectTransform.anchoredPosition = DirectionRes[_direction].anchoredPosition;
                yield return null;
            }
        }
        yield return null;
        foreach (var item in maps)
        {
            item.Value.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_CreateDirection != null) IE_CreateDirection.Stop();
        IE_CreateDirection = null;
        if (IE_CreateMap != null) IE_CreateMap.Stop();
        IE_CreateMap = null;
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        StopAllCoroutine();
    }
}
