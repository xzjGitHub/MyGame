using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class TestCreatMap : MonoBehaviour
{
    public GameObject _MapGameObject;
    public GridLayoutGroup _MapGridLayoutGroup;

    public Transform itemlist;
    //
    public bool startCalculate;
    public const int rowSum = 10;
    public const int columnSum = 10;
    //连续重复方向的最大数量
    public int repetitionMaxSum = 4;
    //垂直方向的最大数量
    public int verticalMaxNum = 5;
    public int horizontalRatio = 70;
    public int startPoint = -1;
    public int maxCreateNum = 10;
    //
    private int maxSum;
    private List<int> labelList = new List<int>();
    //
    private Dictionary<int, Image> images = new Dictionary<int, Image>();
    private Dictionary<int, Text> texts = new Dictionary<int, Text>();
    //
    private Dictionary<int, List<int>> relations = new Dictionary<int, List<int>>();

    /// <summary>
    /// 不可用的索引列表
    /// </summary>
    private List<int> unusableIndexs = new List<int>();

    private Dictionary<int, List<int>> gridIndexBackups = new Dictionary<int, List<int>>();

    private Dictionary<int, Dictionary<int, List<MapGridInfo>>> gridBackupInfos =
        new Dictionary<int, Dictionary<int, List<MapGridInfo>>>();

    //生成的格子
    private Dictionary<int, List<int>> gridRelations = new Dictionary<int, List<int>>();
    private List<int> gridIds = new List<int>();
    private List<MapGridInfo> mapGridInfos = new List<MapGridInfo>();
    //
    private Dictionary<int, List<MapGridInfo>> gridInfos = new Dictionary<int, List<MapGridInfo>>();



    void Awake()
    {
        for (int i = 0; i < itemlist.childCount; i++)
        {
            images.Add(i, itemlist.GetChild(i).GetComponent<Image>());
            texts.Add(i, images[i].transform.Find("Text").GetComponent<Text>());
            texts[i].fontSize = 40;
        }
    }


    void Start()
    {



    }

    public bool isCreateMap = false;




    IEnumerator CreateMap()
    {

        ResourceLoadUtil.DeleteChildObj(_MapGridLayoutGroup.transform);
        List<RandomMapConfig> _list = RandomMapConfigConfig.GetRandomMapConfig(MapID);
        LogHelperLSK.LogWarning(_list.Count);
        _MapGridLayoutGroup.enabled = true;
        yield return null;
        if (_list.Count > 0)
        {
            _MapGridLayoutGroup.constraintCount = _list[0].columnSum;

            for (int i = 0; i < _list[0].columnSum * _list[0].rowSum; i++)
            {
                GameObject _obj = ResourceLoadUtil.InstantiateRes(_MapGameObject);

                ResourceLoadUtil.ObjSetParent(_obj, _MapGridLayoutGroup.transform);

                _obj.gameObject.SetActive(true);
            }
            yield return null;
            _MapGridLayoutGroup.enabled = false;
            yield return null;
            foreach (var item in _list)
            {
                _MapGridLayoutGroup.transform.GetChild(item.index - 1).Find("Text").GetComponent<Text>().text =
                    item.WPId.ToString();
            }
            //
            List<int> _list1 = new List<int>();
            //
            for (int i = 0; i < _MapGridLayoutGroup.transform.childCount; i++)
            {
                bool _isHave = false;
                foreach (var _info in _list)
                {
                    if (_info.index - 1 != i) continue;
                    _isHave = true;
                    continue;
                }
                if (!_isHave)
                {
                    _MapGridLayoutGroup.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    public int MapID = 0;

    void Update()
    {
        if (isCreateMap)
        {
            StartCoroutine(CreateMap());
            isCreateMap = false;
        }

        if (startCalculate)
        {
            startCalculate = false;
            Test();
        }
    }


    void Test()
    {
        for (int i = 0; i < itemlist.childCount; i++)
        {
            images[i].enabled = false;
            texts[i].text = "";
        }
        //
        labelList.Clear();
        relations.Clear();
        maxSum = rowSum * columnSum;
        int initNum = 10100;
        //初始化格子
        for (int i = 0; i < maxSum; i++)
        {
            labelList.Add(i + initNum);
        }
        Map_template _mapTemplate = Map_templateConfig.GetMap_templat(10001);
        WP_template _wpTemplate;
        //_mapTemplate.WPList
        List<int> list = new List<int>();
        foreach (var item in list)
        {
            _wpTemplate = WP_templateConfig.GetWpTemplate(item);
            relations.Add(item, _wpTemplate.nextWP);
        }
        //
        gridRelations = relations;
        GetGrids(gridRelations);
        //
        foreach (var item in mapGridInfos)
        {
            images[item.index].enabled = true;
            texts[item.index].text = (item.gridId - 10100).ToString();
        }
    }


    /// <summary>
    /// 获得格子
    /// </summary>
    private void GetGrids(Dictionary<int, List<int>> _relations)
    {
        int _index = 0;
        int _sum = 0;
        //
        gridInfos.Clear();
        while (_index < maxCreateNum)
        {
            gridIds.Clear();
            gridIds.AddRange(_relations.Keys);
            for (int i = 0; i < gridIds.Count; i++)
            {
                if (i == 0)
                {
                    CreateFirstGird();
                    continue;
                }
                //从第二开始检测了
                if (GetInfo(i)) continue;
                _sum++;
                break;
            }
            if (mapGridInfos.Count < _relations.Count)
            {
                _index++;
                _sum++;
                continue;
            }
            List<MapGridInfo> _list = FormatMapGridInfo(mapGridInfos);
            if (gridInfos.Count == 0)
            {
                gridInfos.Add(_index, _list);
            }
            else
            {
                bool isAdd = false;
                foreach (var item in gridInfos)
                {
                    isAdd = item.Value.Where((t, i) => t.index != _list[i].index).Any();
                    if (!isAdd) break;
                }
                if (isAdd)
                {
                    try
                    {
                        gridInfos.Add(gridInfos.Count, _list);
                    }
                    catch (Exception e)
                    {
                        LogHelperLSK.LogWarning(e);
                    }
                }
            }
            _index++;
        }
        LogHelperLSK.LogError("生成出错" + _sum);
        if (gridInfos.Count == 0)
        {
            LogHelperLSK.LogError("生成失败");
            return;
        }
        string _log = String.Empty;
        foreach (var item in gridInfos)
        {
            _log = String.Empty;
            _log += "第" + item.Key + "组：";
            foreach (var _info in item.Value)
            {
                _log += _info.gridId + ":" + _info.index + "  ";
            }
            _log += " " + item.Value[0].rowSum + "*" + item.Value[0].columnSum;
            _log += "\n";
            LogHelperLSK.LogError(_log);
        }
        LogHelperLSK.LogError(Application.dataPath);
        CreateText(Application.dataPath, gridInfos);
    }

    /// <summary>
    /// 最终格式化格子信息
    /// </summary>
    private List<MapGridInfo> FormatMapGridInfo(List<MapGridInfo> _list)
    {
        List<MapGridInfo> tempGridInfos = new List<MapGridInfo>();
        tempGridInfos.AddRange(_list);
        int minRow = tempGridInfos.OrderBy(a => a.row).ToList().First().row;
        int maxRow = tempGridInfos.OrderByDescending(a => a.row).ToList().First().row;
        int minColumn = tempGridInfos.OrderBy(a => a.column).ToList().First().column;
        int maxColumn = tempGridInfos.OrderByDescending(a => a.column).ToList().First().column;
        //得到现在的行和列
        int _rowSum = maxRow - minRow + 1;
        int _columnSum = maxColumn - minColumn + 1;
        //
        foreach (var item in tempGridInfos)
        {
            //先移动到顶点
            item.row = item.row - minRow + 1;
            item.column = item.column - minColumn + 1;
            item.index = GetGridIndex(item.row, item.column, _rowSum, _columnSum);
            item.rowSum = _rowSum;
            item.columnSum = _columnSum;
        }
        //
        return tempGridInfos;
    }

    private int GetGridIndex(int _nowRow, int _nowColumn, int _rowSum = rowSum, int _columnSum = columnSum)
    {
        return (_nowRow - 1) * _columnSum + _nowColumn;
    }


    private bool GetInfo(int _startIndex)
    {
        return ResetCreateGird(_startIndex) || BackupsCreateGird(_startIndex - 1);
    }

    /// <summary>
    /// 创建第一个格子
    /// </summary>
    private bool CreateFirstGird()
    {
        mapGridInfos.Clear();
        gridIndexBackups.Clear();
        gridBackupInfos.Clear();
        unusableIndexs.Clear();
        //
        int _nowGridIndex = startPoint == -1 ? RandomBuilder.RandomNum(labelList.Count - 1, 0) : startPoint;
        mapGridInfos.Add(new MapGridInfo
        {
            gridId = gridIds[0],
            index = _nowGridIndex,
            row = GetRow(_nowGridIndex),
            column = GetColumn(_nowGridIndex)
        });
        return true;
    }

    /// <summary>
    /// 重新生成格子
    /// </summary>
    private bool ResetCreateGird(int _startIndex)
    {
        if (_startIndex < 0) return false;
        if (_startIndex >= gridIds.Count) return true;

        //当前格子的父亲列表
        List<int> _parentLists = new List<int>();
        Dictionary<int, List<MapGridInfo>> nowUsableLists = new Dictionary<int, List<MapGridInfo>>();
        //
        int _nowGirdId = gridIds[_startIndex];
        UpdateUnusableIndexs(_nowGirdId);
        //
        _parentLists = GetParentLists(_nowGirdId, gridRelations);
        foreach (var item in _parentLists)
        {
            if (mapGridInfos.Any(a => a.gridId == item))
            {
                //生成的格子中有这个
                nowUsableLists.Add(item, GetUsableGridinfos(item, unusableIndexs));
            }
        }
        //得到现在可以用的格子索引
        Dictionary<int, List<MapGridInfo>> _list = GetSameLists(nowUsableLists);
        List<int> _nowUsableIndexs = new List<int>();
        _nowUsableIndexs.AddRange(_list.Keys);
        //如果没有可以使用的格子
        if (_nowUsableIndexs.Count == 0)
        {
            return BackupsCreateGird(_startIndex - 1);
        }
        //生成当前格子
        int _index = GetRandomIndex(_nowUsableIndexs);
        if (_index <= 0)
        {
            return BackupsCreateGird(_startIndex - 1);
        }
        MapGridInfo _info = new MapGridInfo
        {
            gridId = _nowGirdId,
            index = _index,
            row = GetRow(_index),
            column = GetColumn(_index)
        };
        foreach (var item in _list[_index])
        {
            foreach (var _parentInfo in item.parentInfos)
            {
                _info.parentInfos.Add(new[] { _parentInfo[0], _parentInfo[1], _parentInfo[2] });
            }
        }
        mapGridInfos.Add(_info);
        _nowUsableIndexs.Remove(_index);
        _list.Remove(_index);
        //备份当前ID可用的格子
        // gridIndexBackups.Add(_nowGirdId, _nowUsableIndexs);
        gridBackupInfos.Add(_nowGirdId, _list);
        return true;
    }

    /// <summary>
    /// 备份生成格子
    /// </summary>
    /// <returns></returns>
    private bool BackupsCreateGird(int _startIndex)
    {
        if (_startIndex > gridIds.Count - 1) return false;
        //
        if (_startIndex == 0)
        {
            CreateFirstGird();
            return ResetCreateGird(1);
        }
        int _nowGirdId = gridIds[_startIndex];
        Dictionary<int, List<MapGridInfo>> nowUsableGridInfos = new Dictionary<int, List<MapGridInfo>>();
        if (mapGridInfos.Any(a => a.gridId == _nowGirdId))
        {
            mapGridInfos.Remove(mapGridInfos.Find(a => a.gridId == _nowGirdId));
        }
        //备份中没有改格子了
        if (!gridBackupInfos.ContainsKey(_nowGirdId))
        {
            if (BackupsCreateGird(_startIndex - 1))
            {
                return ResetCreateGird(_startIndex + 1);
            }
            return false;
        }
        //有该格子
        nowUsableGridInfos = gridBackupInfos[_nowGirdId];
        if (nowUsableGridInfos.Count == 0)
        {
            gridBackupInfos.Remove(_nowGirdId);
            if (BackupsCreateGird(_startIndex - 1))
            {
                return ResetCreateGird(_startIndex + 1);
            }
            return false;
        }
        //
        try
        {
            List<int> _nowUsableIndexs = new List<int>();
            _nowUsableIndexs.AddRange(nowUsableGridInfos.Keys);
            int _index = GetRandomIndex(_nowUsableIndexs);
            //
            MapGridInfo _info = new MapGridInfo
            {
                gridId = _nowGirdId,
                index = _index,
                row = GetRow(_index),
                column = GetColumn(_index)
            };
            foreach (var item in nowUsableGridInfos[_index])
            {
                foreach (var _parentInfo in item.parentInfos)
                {
                    _info.parentInfos.Add(new[] { _parentInfo[0], _parentInfo[1], _parentInfo[2] });
                }
            }
            //生成当前格子
            mapGridInfos.Add(_info);
            //备份当前ID可用的格子
            gridBackupInfos[_nowGirdId].Remove(_index);

            if (gridBackupInfos[_nowGirdId].Count == 0)
            {
                gridBackupInfos.Remove(_nowGirdId);
            }
            return ResetCreateGird(_startIndex + 1);
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <summary>
    /// 得到当前格子的父列表
    /// </summary>
    private List<int> GetParentLists(int _index, Dictionary<int, List<int>> _relations)
    {
        List<int> _list = new List<int>();
        foreach (var item in _relations)
        {
            if (!item.Value.Contains(_index)) continue;
            AddValue(_list, item.Key);
        }
        return _list;
    }

    /// <summary>
    /// 得到相同的列表
    /// </summary>
    private List<int> GetSameLists(Dictionary<int, List<int>> _listDictionary)
    {
        if (_listDictionary.Count == 1) return _listDictionary.First().Value;
        //
        List<int> _list = new List<int>();
        List<int> _keys = new List<int>();
        _keys.AddRange(_listDictionary.Keys);
        for (int i = 0; i < _listDictionary.Count - 1; i++)
        {
            if (i == 0)
            {
                _list = _listDictionary[_keys[i]].FindAll(a => _listDictionary[_keys[i + 1]].Contains(a));
                continue;
            }
            _list = _listDictionary[_keys[i + 1]].FindAll(a => _list.Contains(a));
        }
        return _list;
    }

    /// <summary>
    /// 得到相同的列表
    /// </summary>
    private Dictionary<int, List<MapGridInfo>> GetSameLists(Dictionary<int, List<MapGridInfo>> _lists)
    {
        Dictionary<int, List<MapGridInfo>> _dictionary = new Dictionary<int, List<MapGridInfo>>();
        if (_lists.Count == 1)
        {
            foreach (var item in _lists.First().Value)
            {
                MapGridInfo _info = new MapGridInfo { gridId = item.gridId, index = item.index };

                _info.parentInfos.Add(new[] { item.parentInfos[0][0], item.parentInfos[0][1], item.parentInfos[0][2] });
                _dictionary.Add(item.index, new List<MapGridInfo> { _info });
            }
            return _dictionary;
        }
        List<MapGridInfo> _list = new List<MapGridInfo>();
        List<MapGridInfo> _tempList = new List<MapGridInfo>();
        //先复制所有格子
        foreach (var item in _lists)
        {
            _list.AddRange(item.Value);
        }
        //
        foreach (var item in _list)
        {
            if (_dictionary.ContainsKey(item.index)) continue;
            _tempList = _list.FindAll(a => a.index == item.index);
            if (_tempList.Count == 1) continue;
            _dictionary.Add(item.index, FormattingMapGridInfo(_tempList));
        }
        return _dictionary;
    }

    /// <summary>
    /// 格式化地图格子信息
    /// </summary>
    private List<MapGridInfo> FormattingMapGridInfo(List<MapGridInfo> _mapGridInfos)
    {
        List<MapGridInfo> _tempList = new List<MapGridInfo>();
        foreach (var _info in _mapGridInfos)
        {
            MapGridInfo _gridInfo = new MapGridInfo { gridId = _info.gridId, index = _info.index };
            foreach (var _parentInfo in _info.parentInfos)
            {
                _gridInfo.parentInfos.Add(new[] { _parentInfo[0], _parentInfo[1], _parentInfo[2] });
            }
            _tempList.Add(_gridInfo);
        }
        return _tempList;
    }


    /// <summary>
    /// 更新不能使用的格子
    /// </summary>
    private void UpdateUnusableIndexs(int _gridid)
    {
        unusableIndexs.Clear();
        //
        foreach (var item in mapGridInfos)
        {
            unusableIndexs.Add(item.index);
        }
        //得到不能使用的位置列表
        GetUnusableIndexs(_gridid);
        //清除不可能出现的位置 即==-1
        CleanList(unusableIndexs);
    }


    private void GetUnusableIndexs(int _gridId)
    {
        SetUnusableIndexs(_gridId);
        foreach (var item in GetParentLists(_gridId, gridRelations))
        {
            GetUnusableIndexs(item);
        }
    }

    /// <summary>
    /// 得到不能使用的位置列表
    /// </summary>
    private void SetUnusableIndexs(int _gridId)
    {
        foreach (var id in gridRelations.ContainsKey(_gridId) ? gridRelations[_gridId] : new List<int>())
        {
            foreach (var _info in mapGridInfos)
            {
                if (_info.gridId != id) continue;
                GetDirectionAddValue2(unusableIndexs, _info.index);
                break;
            }
        }
    }

    /// <summary>
    /// 列表中添加指定值
    /// </summary>
    private void AddValue(List<int> _list, int _value)
    {
        if (_list == null || _list.Contains(_value)) return;
        _list.Add(_value);
    }

    /// <summary>
    /// 清除列表中制定值
    /// </summary>
    private void CleanList(List<int> _list, int _value = -1)
    {
        var _tempUpdate = false;
        while (!_tempUpdate)
        {
            var _isMove = false;
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] != _value) continue;
                _isMove = true;
                _list.Remove(_value);
            }
            _tempUpdate = !_isMove;
        }
    }

    /// <summary>
    /// 其他链接当前格子
    /// </summary>
    private void GetDirectionAddValue1(List<int> _list, int _direction, int _index)
    {
        switch (_direction)
        {
            case 1:
                AddValue(_list, GetUpIndex(_index));
                break;
            case 5:
                AddValue(_list, GetLeftUpperIndex(_index));
                break;
            case 7:
                AddValue(_list, GetRightUpperIndex(_index));
                break;
            case 2:
                AddValue(_list, GetDownIndex(_index));
                break;
            case 6:
                AddValue(_list, GetLeftLowerIndex(_index));
                break;
            case 8:
                AddValue(_list, GetRightLowerIndex(_index));
                break;
            case 3:
                AddValue(_list, GetLeftIndex(_index));
                break;
            case 4:
                AddValue(_list, GetRightIndex(_index));
                break;
        }
    }

    /// <summary>
    /// 自己链接其他
    /// </summary>
    private void GetDirectionAddValue2(List<int> _list, int _index)
    {
        if (IsUpDirectionUsed(_index)) AddValue(_list, GetUpIndex(_index));
        if (IsDownDirectionUsed(_index)) AddValue(_list, GetDownIndex(_index));
        if (IsLeftDirectionUsed(_index)) AddValue(_list, GetLeftIndex(_index));
        if (IsRightDirectionUsed(_index)) AddValue(_list, GetRightIndex(_index));
        if (IsLeftUpperDirectionUsed(_index)) AddValue(_list, GetLeftUpperIndex(_index));
        if (IsLeftLowerDirectionUsed(_index)) AddValue(_list, GetLeftLowerIndex(_index));
        if (IsRightUpperDirectionUsed(_index)) AddValue(_list, GetRightUpperIndex(_index));
        if (IsRightLowerDirectionUsed(_index)) AddValue(_list, GetRightLowerIndex(_index));
    }

    /// <summary>
    /// 上方向是否已使用
    /// </summary>
    private bool IsUpDirectionUsed(int _index)
    {
        //上已被用
        if (IsContainsGrid(1, _index)) return true;
        //左和左上都在
        if (IsContainsGrid(3, _index) && IsContainsGrid(5, _index)) return true;
        if (IsContainsGrid(2, _index) && IsContainsGrid(6, _index) && IsContainsGrid(7, _index)) return true;
        if (IsContainsGrid(4, _index) && IsContainsGrid(8, _index) && IsContainsGrid(6, _index) &&
            IsContainsGrid(7, _index)) return true;
        //右和右上都在
        if (IsContainsGrid(4, _index) && IsContainsGrid(7, _index)) return true;
        if (IsContainsGrid(2, _index) && IsContainsGrid(8, _index) && IsContainsGrid(7, _index)) return true;
        if (IsContainsGrid(3, _index) && IsContainsGrid(6, _index) && IsContainsGrid(8, _index) &&
            IsContainsGrid(7, _index)) return true;
        return false;
    }

    /// <summary>
    /// 下方向是否已使用
    /// </summary>
    private bool IsDownDirectionUsed(int _index)
    {
        //下已被用
        if (IsContainsGrid(2, _index)) return true;
        //左和左下都在
        if (IsContainsGrid(3, _index) && IsContainsGrid(6, _index)) return true;
        if (IsContainsGrid(1, _index) && IsContainsGrid(5, _index) && IsContainsGrid(6, _index)) return true;
        if (IsContainsGrid(4, _index) && IsContainsGrid(7, _index) && IsContainsGrid(5, _index) &&
            IsContainsGrid(6, _index)) return true;
        //右和右下都在
        if (IsContainsGrid(4, _index) && IsContainsGrid(8, _index)) return true;
        if (IsContainsGrid(1, _index) && IsContainsGrid(7, _index) && IsContainsGrid(8, _index)) return true;
        if (IsContainsGrid(3, _index) && IsContainsGrid(5, _index) && IsContainsGrid(7, _index) &&
            IsContainsGrid(8, _index)) return true;
        return false;
    }

    /// <summary>
    /// 左方向是否已使用
    /// </summary>
    private bool IsLeftDirectionUsed(int _index)
    {
        //左已被用
        if (IsContainsGrid(3, _index)) return true;
        //上
        if (IsContainsGrid(1, _index) && IsContainsGrid(5, _index)) return true;
        if (IsContainsGrid(4, _index) && IsContainsGrid(7, _index) && IsContainsGrid(5, _index)) return true;
        if (IsContainsGrid(2, _index) && IsContainsGrid(8, _index) && IsContainsGrid(7, _index) &&
            IsContainsGrid(5, _index)) return true;
        //下
        if (IsContainsGrid(2, _index) && IsContainsGrid(6, _index)) return true;
        if (IsContainsGrid(4, _index) && IsContainsGrid(8, _index) && IsContainsGrid(6, _index)) return true;
        if (IsContainsGrid(1, _index) && IsContainsGrid(7, _index) && IsContainsGrid(8, _index) &&
            IsContainsGrid(6, _index)) return true;
        return false;
    }

    /// <summary>
    /// 右方向是否已使用
    /// </summary>
    private bool IsRightDirectionUsed(int _index)
    {
        //右已被用
        if (IsContainsGrid(4, _index)) return true;
        //上
        if (IsContainsGrid(1, _index) && IsContainsGrid(7, _index)) return true;
        if (IsContainsGrid(3, _index) && IsContainsGrid(5, _index) && IsContainsGrid(7, _index)) return true;
        if (IsContainsGrid(2, _index) && IsContainsGrid(6, _index) && IsContainsGrid(5, _index) &&
            IsContainsGrid(7, _index)) return true;
        //下
        if (IsContainsGrid(2, _index) && IsContainsGrid(8, _index)) return true;
        if (IsContainsGrid(3, _index) && IsContainsGrid(6, _index) && IsContainsGrid(8, _index)) return true;
        if (IsContainsGrid(1, _index) && IsContainsGrid(5, _index) && IsContainsGrid(6, _index) &&
            IsContainsGrid(8, _index)) return true;
        return false;
    }

    /// <summary>
    /// 左上方向是否已使用
    /// </summary>
    private bool IsLeftUpperDirectionUsed(int _index)
    {
        //左上已被用
        if (IsContainsGrid(5, _index)) return true;
        //
        if ((IsContainsGrid(3, _index) ||
             ((IsContainsGrid(2, _index) || (IsContainsGrid(4, _index) && IsContainsGrid(8, _index))) &&
              IsContainsGrid(6, _index)))
            &&
            (IsContainsGrid(1, _index) ||
             ((IsContainsGrid(4, _index) || (IsContainsGrid(2, _index) && IsContainsGrid(8, _index))) &&
              IsContainsGrid(7, _index)))) return true;
        return false;
    }

    /// <summary>
    /// 左下方向是否已使用
    /// </summary>
    private bool IsLeftLowerDirectionUsed(int _index)
    {
        //左下已被用
        if (IsContainsGrid(6, _index)) return true;
        //
        if ((IsContainsGrid(3, _index) ||
             ((IsContainsGrid(1, _index) || (IsContainsGrid(4, _index) && IsContainsGrid(7, _index))) &&
              IsContainsGrid(5, _index))) &&
            (IsContainsGrid(2, _index) ||
             ((IsContainsGrid(4, _index) || (IsContainsGrid(1, _index) && IsContainsGrid(7, _index))) &&
              IsContainsGrid(8, _index)))) return true;
        return false;
    }

    /// <summary>
    /// 右上方向是否已使用
    /// </summary>
    private bool IsRightUpperDirectionUsed(int _index)
    {
        //右上已被用
        if (IsContainsGrid(7, _index)) return true;
        //
        if ((IsContainsGrid(1, _index) ||
             ((IsContainsGrid(3, _index) || (IsContainsGrid(2, _index) && IsContainsGrid(6, _index))) &&
              IsContainsGrid(5, _index))) &&
            (IsContainsGrid(4, _index) ||
             ((IsContainsGrid(2, _index) || (IsContainsGrid(3, _index) && IsContainsGrid(6, _index))) &&
              IsContainsGrid(8, _index)))) return true;
        return false;
    }

    /// <summary>
    /// 右下方向是否已使用
    /// </summary>
    private bool IsRightLowerDirectionUsed(int _index)
    {
        //右下已被用
        if (IsContainsGrid(8, _index)) return true;
        //
        if ((IsContainsGrid(2, _index) ||
             ((IsContainsGrid(3, _index) || (IsContainsGrid(1, _index) && IsContainsGrid(5, _index))) &&
              IsContainsGrid(6, _index))) &&
            (IsContainsGrid(4, _index) ||
             ((IsContainsGrid(1, _index) || (IsContainsGrid(3, _index) && IsContainsGrid(5, _index))) &&
              IsContainsGrid(7, _index)))) return true;
        return false;
    }


    /// <summary>
    /// 是否包含当前格子
    /// </summary>
    private bool IsContainsGrid(int _direction, int _index)
    {
        var _temp = GetGridDirectionIndex(_direction, _index);

        return unusableIndexs.Contains(_temp) || mapGridInfos.Any(item => item.index == _temp);
    }


    /// <summary>
    /// 得到行
    /// </summary>
    private int GetRow(int _index, int _columnSum = columnSum)
    {
        return _index / _columnSum + 1;
    }

    /// <summary>
    /// 得到列
    /// </summary>
    private int GetColumn(int _index, int _columnSum = columnSum)
    {
        if (_index <= _columnSum - 1)
        {
            return _index + 1;
        }
        return _index + 1 - _index / _columnSum * _columnSum;
    }


    //方向位：上1、下2、左3、右4、左上5、左下6、右上7、右下8  编号=-1时，获取编号出错
    /// <summary>
    /// 得到向上方向的编号
    /// </summary>
    private int GetUpIndex(int _index, int _columnSum = columnSum)
    {
        return GetRow(_index) == 1 ? -1 : _index - _columnSum;
    }

    /// <summary>
    /// 得到向下方向的编号
    /// </summary>
    private int GetDownIndex(int _index, int _rowSum = rowSum, int _columnSum = columnSum)
    {
        return GetRow(_index) == _rowSum ? -1 : _index + _columnSum;
    }

    /// <summary>
    /// 得到向左方向的编号
    /// </summary>
    private int GetLeftIndex(int _index, int _columnSum = columnSum)
    {
        return GetColumn(_index, _columnSum) == 1 ? -1 : _index - 1;
    }

    /// <summary>
    /// 得到向右方向的编号
    /// </summary>
    private int GetRightIndex(int _index, int _columnSum = columnSum)
    {
        return GetColumn(_index, _columnSum) == _columnSum ? -1 : _index + 1;
    }

    /// <summary>
    /// 得到向左下方向的编号
    /// </summary>
    private int GetLeftLowerIndex(int _index, int _rowSum = rowSum, int _columnSum = columnSum)
    {
        if (GetColumn(_index, _columnSum) == 1 || GetRow(_index, _columnSum) == _rowSum)
        {
            return -1;
        }
        return _index + _columnSum - 1;
    }

    /// <summary>
    /// 得到向左上方向的编号
    /// </summary>
    private int GetLeftUpperIndex(int _index, int _columnSum = columnSum)
    {
        if (GetColumn(_index, _columnSum) == 1 || GetRow(_index, _columnSum) == 1)
        {
            return -1;
        }
        return _index - _columnSum - 1;
    }

    /// <summary>
    /// 得到向右下方向的编号
    /// </summary>
    private int GetRightLowerIndex(int _index, int _rowSum = rowSum, int _columnSum = columnSum)
    {
        if (GetColumn(_index, _columnSum) == _columnSum || GetRow(_index, _columnSum) == _rowSum)
        {
            return -1;
        }
        return _index + _columnSum + 1;
    }

    /// <summary>
    /// 得到向右上方向的编号
    /// </summary>
    private int GetRightUpperIndex(int _index, int _columnSum = columnSum)
    {
        if (GetColumn(_index, _columnSum) == _columnSum || GetRow(_index, _columnSum) == 1)
        {
            return -1;
        }
        return _index - _columnSum + 1;
    }

    /// <summary>
    /// 得到方向
    /// </summary>
    private int GetGridDirection(int _index, int _target)
    {
        if (_target == GetUpIndex(_index)) return 1;
        if (_target == GetDownIndex(_index)) return 2;
        if (_target == GetLeftIndex(_index)) return 3;
        if (_target == GetRightIndex(_index)) return 4;
        if (_target == GetLeftUpperIndex(_index)) return 5;
        if (_target == GetLeftLowerIndex(_index)) return 6;
        if (_target == GetRightUpperIndex(_index)) return 7;
        if (_target == GetRightLowerIndex(_index)) return 8;
        return -1;
    }

    /// <summary>
    /// 根据方向获取Id
    /// </summary>
    private int GetGridDirectionIndex(int _direction, int _index)
    {
        switch (_direction)
        {
            case 1:
                return GetUpIndex(_index);
            case 2:
                return GetDownIndex(_index);
            case 3:
                return GetLeftIndex(_index);
            case 4:
                return GetRightIndex(_index);
            case 5:
                return GetLeftUpperIndex(_index);
            case 6:
                return GetLeftLowerIndex(_index);
            case 7:
                return GetRightUpperIndex(_index);
            case 8:
                return GetRightLowerIndex(_index);
            default:
                return -1;
        }
    }


    /// <summary>
    /// 获取可以使用的个格子
    /// </summary>
    private List<MapGridInfo> GetUsableGridinfos(int _gridId, List<int> _existings)
    {
        List<MapGridInfo> _infos = new List<MapGridInfo>();
        //
        int _index = mapGridInfos.Find(a => a.gridId == _gridId).index;
        //
        int _temp = GetUpIndex(_index);
        //上
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            _infos.Add(new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 1, 0 } } });
        }
        //下
        _temp = GetDownIndex(_index);
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            _infos.Add(new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 2, 0 } } });
        }
        //左
        _temp = GetLeftIndex(_index);
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            _infos.Add(new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 3, 0 } } });
        }
        //右
        _temp = GetRightIndex(_index);
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            _infos.Add(new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 4, 0 } } });
        }
        //左上
        _temp = GetLeftUpperIndex(_index);
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            int _x = GetLeftIndex(_index);
            _infos.Add(mapGridInfos.Find(a => a.index == _x) != null
                ? new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 5, 0 } } }
                : new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 5, 1 } } });
        }
        //左下
        _temp = GetLeftLowerIndex(_index);
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            int _x = GetLeftIndex(_index);
            _infos.Add(mapGridInfos.Find(a => a.index == _x) != null
                ? new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 6, 0 } } }
                : new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 6, 1 } } });
        }
        //右上
        _temp = GetRightUpperIndex(_index);
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            int _x = GetRightIndex(_index);
            _infos.Add(mapGridInfos.Find(a => a.index == _x) != null
                ? new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 7, 0 } } }
                : new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 7, 1 } } });
        }
        //右下
        _temp = GetRightLowerIndex(_index);
        if (!_existings.Contains(_temp) && _temp != -1)
        {
            int _x = GetRightIndex(_index);
            _infos.Add(mapGridInfos.Find(a => a.index == _x) != null
                ? new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 8, 0 } } }
                : new MapGridInfo { index = _temp, parentInfos = new List<int[]> { new int[] { _gridId, 8, 1 } } });
        }
        return _infos;
    }

    public int rightProbability = 40;
    public int rightIinclinedProbability = 20;
    public int verticalProbability = 15;
    public int leftProbability = 15;
    public int leftIinclinedProbability = 10;

    //随机抽取位置
    private int GetRandomIndex(List<int> _list)
    {
        //先用第一个点作为基点
        if (mapGridInfos.Count == 0) return RandomBuilder.RandomList(1, _list)[0];
        int _index = mapGridInfos.First().index;
        //得到第一个点的列
        int _firstColumn = mapGridInfos.First().column;
        //随机规则 右1>右上、右下2>上、下3>左4>左上、左下5
        List<float> _randomList = new List<float>();
        List<List<int>> _dictionarys = new List<List<int>>();
        List<int> _temp = GetRightList(_list, _index);
        if (_temp.Count > 0)
        {
            _randomList.Add(rightProbability + (_randomList.Count == 0 ? 0 : _randomList.Last()));
            _dictionarys.Add(_temp);
        }
        _temp = GetRightIinclinedList(_list, _index);
        if (_temp.Count > 0)
        {
            _randomList.Add(rightIinclinedProbability + (_randomList.Count == 0 ? 0 : _randomList.Last()));
            _dictionarys.Add(_temp);
        }
        _temp = GetVerticalList(_list, _index);
        if (_temp.Count > 0)
        {
            _randomList.Add(verticalProbability + (_randomList.Count == 0 ? 0 : _randomList.Last()));
            _dictionarys.Add(_temp);
        }
        _temp = DislodgeLeftList(GetLeftList(_list, _index), _firstColumn);
        if (_temp.Count > 0)
        {
            _randomList.Add(leftProbability + (_randomList.Count == 0 ? 0 : _randomList.Last()));
            _dictionarys.Add(_temp);
        }
        _temp = DislodgeLeftList(GetLeftIinclinedList(_list, _index), _firstColumn);
        if (_temp.Count > 0)
        {
            _randomList.Add(leftIinclinedProbability + (_randomList.Count == 0 ? 0 : _randomList.Last()));
            _dictionarys.Add(_temp);
        }
        if (_randomList.Count == 0) return -1;
        _temp = _dictionarys[RandomBuilder.RandomIndex_Chances(_randomList, _randomList.Last())];
        if (_temp.Count == 0) return -1;
        return RandomBuilder.RandomList(1, _temp)[0];
    }

    /// <summary>
    /// 去除左侧
    /// </summary>
    private List<int> DislodgeLeftList(List<int> _dictionarys, int _firstColumn)
    {
        if (_dictionarys.Count == 0) return new List<int>();

        List<int> _indexs = new List<int>();

        foreach (var item in _dictionarys)
        {
            if (GetColumn(item) < _firstColumn)
            {
                _indexs.Add(item);
            }
        }

        foreach (var item in _indexs)
        {
            _dictionarys.Remove(item);
        }
        return _dictionarys;
    }
    /// <summary>
    /// 去除上方
    /// </summary>
    private List<int> DislodgeUpList(List<int> _dictionarys, int _firstColumn)
    {
        if (_dictionarys.Count == 0) return new List<int>();

        List<int> _indexs = new List<int>();

        foreach (var item in _dictionarys)
        {
            if (GetColumn(item) < _firstColumn)
            {
                _indexs.Add(item);
            }
        }

        foreach (var item in _indexs)
        {
            _dictionarys.Remove(item);
        }
        return _dictionarys;
    }
    /// <summary>
    /// 去除下方
    /// </summary>
    private List<int> DislodgeDownList(List<int> _dictionarys, int _firstColumn)
    {
        if (_dictionarys.Count == 0) return new List<int>();

        List<int> _indexs = new List<int>();

        foreach (var item in _dictionarys)
        {
            if (GetColumn(item) < _firstColumn)
            {
                _indexs.Add(item);
            }
        }

        foreach (var item in _indexs)
        {
            _dictionarys.Remove(item);
        }
        return _dictionarys;
    }

    /// <summary>
    /// 获取右方的列表
    /// </summary>
    private List<int> GetRightList(List<int> _list, int _index)
    {
        List<int> _temp = new List<int>();
        foreach (var item in _list)
        {
            if (GetColumn(item) > GetColumn(_index) && GetRow(item) == GetRow(_index))
            {
                _temp.Add(item);
            }
        }
        return _temp;
    }

    /// <summary>
    /// 获取左方的列表
    /// </summary>
    private List<int> GetLeftList(List<int> _list, int _index)
    {
        List<int> _temp = new List<int>();
        foreach (var item in _list)
        {
            if (GetColumn(item) < GetColumn(_index) && GetRow(item) == GetRow(_index))
            {
                _temp.Add(item);
            }
        }
        return _temp;
    }

    /// <summary>
    /// 获取垂直方的列表
    /// </summary>
    private List<int> GetVerticalList(List<int> _list, int _index)
    {
        List<int> _temp = new List<int>();
        foreach (var item in _list)
        {
            if (GetColumn(item) == GetColumn(_index) && GetRow(item) != GetRow(_index))
            {
                _temp.Add(item);
            }
        }
        return _temp;
    }

    /// <summary>
    /// 获取右斜方的列表
    /// </summary>
    private List<int> GetRightIinclinedList(List<int> _list, int _index)
    {
        List<int> _temp = new List<int>();
        foreach (var item in _list)
        {
            if (GetColumn(item) > GetColumn(_index) && GetRow(item) != GetRow(_index))
            {
                _temp.Add(item);
            }
        }
        return _temp;
    }

    /// <summary>
    /// 获取左斜的列表
    /// </summary>
    private List<int> GetLeftIinclinedList(List<int> _list, int _index)
    {
        List<int> _temp = new List<int>();
        foreach (var item in _list)
        {
            if (GetColumn(item) < GetColumn(_index) && GetRow(item) != GetRow(_index))
            {
                _temp.Add(item);
            }
        }
        return _temp;
    }

    /// <summary>
    /// 创建Text文件
    /// </summary>
    private void CreateText(string _filePath, Dictionary<int, List<MapGridInfo>> _gridInfos)
    {
        //不存在就创建目录 
        if (!System.IO.Directory.Exists(_filePath))
        {
            System.IO.Directory.CreateDirectory(_filePath);
        }
        _filePath += "/RandomMapConfig.txt";
        DeleteFile(_filePath);
        //
        FileStream fs1 = new FileStream(_filePath, FileMode.Create, FileAccess.Write); //创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);
        int _index = 1;
        int _name = 0;
        string _str = string.Empty;

        for (int i = 0; i < _gridInfos.Count; i++)
        {
            if (i != 0)
            {
                sw.Write("\r\n"); //开始换行 
            }
            _name = 0;
            try
            {
                foreach (var _info in _gridInfos[i])
                {
                    _str = string.Empty;
                    _str += i + "\t";
                    _str += _info.gridId + "\t";
                    _str += _info.index + "\t";
                    _str += _info.row + "\t";
                    _str += _info.column + "\t";
                    _str += _info.rowSum + "\t";
                    _str += _info.columnSum + "\t";
                    _index = 1;
                    foreach (var _parent in _info.parentInfos)
                    {
                        _str += "[" + _parent[0] + "," + _parent[1] + (_parent.Length != 3 ? String.Empty : "," + _parent[2]) + "]";
                        if (_index != _info.parentInfos.Count)
                        {
                            _str += "-";
                        }
                        _index++;
                    }
                    if (_info.parentInfos.Count == 0)
                    {
                        _str += " ";
                    }
                    sw.Write(_str);//开始写入值
                    if (_name != _gridInfos[i].Count - 1)
                    {
                        sw.Write("\r\n");//开始换行 
                    }
                    _name++;
                }
            }
            catch (Exception e)
            {
                LogHelperLSK.LogWarning(e);
            }

        }
        sw.Close();
        fs1.Close();
    }
    /// <summary>
    /// 删除一个指定的文件
    /// </summary>
    /// <param name="FilePath">文件路径</param>
    /// <returns></returns>
    private bool DeleteFile(string FilePath)
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
        return true;
    }
}


/// <summary>
/// 地图格子信息
/// </summary>
public class MapGridInfo
{
    /// <summary>
    /// 格子ID
    /// </summary>
    public int gridId;
    /// <summary>
    /// 索引
    /// </summary>
    public int index;
    /// <summary>
    /// 行
    /// </summary>
    public int row;
    /// <summary>
    /// 列
    /// </summary>
    public int column;
    /// <summary>
    /// 行总数
    /// </summary>
    public int rowSum;
    /// <summary>
    /// 列总数
    /// </summary>
    public int columnSum;
    /// <summary>
    /// 父点的信息 int[父点格子id,父点的方向]
    /// </summary>
    public List<int[]> parentInfos = new List<int[]>();
}