using GameEventDispose;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBigMapPopup : MonoBehaviour
{

    /// <summary>
    /// 打开显示
    /// </summary>
    public void OpenUI(bool isNewCreate = true, bool isNeedSelect = true)
    {
        GetObj();
        _backButton.gameObject.SetActive(!isNeedSelect);
        if (isNewCreate)
        {
            CreateMap(ExploreSystem.Instance.WpAttributes, isNeedSelect);
            return;
        }
        _previousRoomID = ExploreSystem.Instance.PreviousRoomID();
        _roomID = ExploreSystem.Instance.NowRoomID();
        UpdateProgressPoint(false, isNeedSelect);
    }

    /// <summary>
    /// 更新进度点
    /// </summary>
    /// <param name="wpID">路点id</param>
    /// <param name="progressIndex">房间的进度为0</param>
    public void UpdateProgressPoint(bool isGate, bool isNeedSelect)
    {
        if (_openIndex != 0)
        {
            CloseGate();
        }
        if (ExploreSystem.Instance.NowWPAttribute.IsRoom)
        {
            _markerTrans.localPosition = _roomButtons[ExploreSystem.Instance.NowWaypointId].transform.localPosition;
        }
        else
        {
            Vector3 pos0 = _roomButtons[_previousRoomID].transform.localPosition;
            Vector3 pos1 = _roomButtons[_roomID].transform.localPosition;
            float x = (pos1.x - pos0.x) * ExploreSystem.Instance.VisitProgress;
            _markerTrans.localPosition = pos0 + Vector3.right * x + Vector3.up * GameTools.GetY(pos0, pos1, x);
        }
        _markerTrans.localPosition += Vector3.up * 27f;
        UpdateButtonShow(isGate, isNeedSelect);
        UpdateShow(true);
        _openIndex++;
    }

    public void UpdateShow(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    /// <summary>
    /// 创建地图
    /// </summary>
    /// <param name="wpAttribute"></param>
    private void CreateMap(List<WPAttribute> wpAttributes, bool isNeedSelect)
    {
        _openIndex = 0;
        _pathImage.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ExploreMapPath, ExploreSystem.Instance.NowMapId.ToString());
        //
        _roomButtons.Clear();
        _gates.Clear();
        ResourceLoadUtil.DeleteChildObj(_roomTrans);
        foreach (WPAttribute item in wpAttributes)
        {
            if (!item.IsRoom)
            {
                continue;
            }
            GameObject obj = ResourceLoadUtil.InstantiateRes(_tempObj.Find(item.WPInconType.ToString()).gameObject, _roomTrans);
            obj.GetComponent<RectTransform>().anchoredPosition = item.Position;
            _roomButtons.Add(item.WaypointId, obj.GetComponent<Button>());
            int id = item.WaypointId;
            if (item.IsHaveGate)
            {
                obj.transform.Find("Icon").gameObject.SetActive(false);
                ResourceLoadUtil.InstantiateRes(_tempObj.Find("Gate").gameObject, _roomTrans);
                _roomButtons[item.WaypointId].onClick.AddListener(delegate { OnClickGate(id); });
                _gates.Add(id);
                return;
            }
            _roomButtons[item.WaypointId].onClick.AddListener(delegate { OnClickWP(id); });
        }

        UpdateProgressPoint(true, isNeedSelect);
    }

    /// <summary>
    /// 关闭传送门
    /// </summary>
    private void CloseGate()
    {
        foreach (int item in _gates)
        {
            int id = item;
            _roomButtons[item].transform.Find("Icon").gameObject.SetActive(true);
            _roomButtons[item].transform.Find("Gate").gameObject.SetActive(false);
            _roomButtons[item].onClick.RemoveAllListeners();
            _roomButtons[item].onClick.AddListener(delegate { OnClickWP(id); });
        }
    }

    /// <summary>
    /// 更新按钮显示
    /// </summary>
    private void UpdateButtonShow(bool isHaveGate, bool isNeedSelect)
    {
        _isAutoSelect = false;
        List<int> list = ExploreSystem.Instance.GetAllUsableRoomIDs(isHaveGate);
        foreach (KeyValuePair<int, Button> item in _roomButtons)
        {
            item.Value.interactable = isNeedSelect && list.Contains(item.Key);
        }
        if (list.Count == 0)
        {
            _backButton.gameObject.SetActive(true);
            if (isHaveGate)
            {
                _isAutoSelect = true;
            }
        }
    }

    /// <summary>
    /// 点击了背景
    /// </summary>
    private void OnClickBack()
    {
        if (_isNeedSelect)
        {
            return;
        }
        UpdateShow(false);
        if (_isAutoSelect)
        {
            EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.WPStartReady, (object)true);
        }
    }

    /// <summary>
    /// 点击路点
    /// </summary>
    /// <param name="WPId"></param>
    private void OnClickWP(int WPId)
    {
        if (!ExploreSystem.Instance.SelectRoom(WPId))
        {
            return;
        }
        _previousRoomID = _roomID;
        _roomID = WPId;
        //路点开始
        ExploreSystem.Instance.SelectRoom(_roomID);
        UpdateShow(false);
        _isNeedSelect = false;
    }

    /// <summary>
    /// 点击传送门
    /// </summary>
    /// <param name="WPID"></param>
    private void OnClickGate(int WPID)
    {
        ExploreSystem.Instance.SelectRoom(WPID);
        UpdateShow(false);
        _isNeedSelect = false;
    }



    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        if (_isFirst)
        {
            return;
        }

        _tempObj = transform.Find("Temp");
        _backButton = transform.Find("Back").GetComponent<Button>();
        _pathImage = transform.Find("Path").GetComponent<Image>();
        _roomTrans = transform.Find("Room");
        _markerTrans = transform.Find("Marker/Marker");
        //
        _backButton.onClick.AddListener(OnClickBack);
        //
        _isFirst = true;
    }

    //
    private bool _isAutoSelect;
    private Button _backButton;
    private Image _pathImage;
    private Transform _roomTrans;
    private Transform _markerTrans;
    private Transform _tempObj;
    //
    private Dictionary<int, Button> _roomButtons = new Dictionary<int, Button>();
    private List<int> _gates = new List<int>();
    private bool _isFirst;
    private bool _isNeedSelect;
    private int _roomID;
    private int _previousRoomID;
    private int _openIndex;
}
