using LskConfig;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class UITestMap : MonoBehaviour
{
    public InputField mapIDInput;
    public Transform girdTrans;
    public Transform pointTrans;
    public GameObject point;
    public int smoothness = 10;
    public InputField smoothnessInput;
    public InputField indexInput;
    public InputField wpIDInput1;
    public InputField wpIDInput2;
    private Transform addPointObj;
    public Text Intro;

    public List<int> wpIndex = new List<int>();

    public int mapID;



    private List<Vector3> GetVector3s(List<Vector3> pos, int sum)
    {
        List<Vector3> point = new List<Vector3>();
        for (int i = 0; i < sum; i++)
        {
            point.Add(GetLerpVector(pos, i)[0]);
        }
        return point;
    }


    private List<Vector3> GetLerpVector(List<Vector3> pos, int index)
    {
        List<Vector3> posVetors = new List<Vector3>();
        for (int i = 1; i < pos.Count; i++)
        {
            posVetors.Add(Vector3.Lerp(pos[i - 1], pos[i], index / (float)smoothness));
        }
        return posVetors.Count == 1 ? posVetors : GetLerpVector(posVetors, index);
    }


    public void OnClickInput()
    {
        ConfigManager.Instance.isResource = false;
        try
        {
            mapID = int.Parse(mapIDInput.text);
        }
        catch (Exception e) { return; }
        //
        Map_template map_Template = Map_templateConfig.GetMap_templat(mapID);
        if (map_Template == null) return;
        WP_template wP_Template = WP_templateConfig.GetWpTemplate(map_Template.initialWP);
        if (wP_Template == null) return;
        ConfigManager.Instance.isResource = true;
        //
        Start1();
    }
    public void OnClickOutput()
    {
        string _filePath = Application.persistentDataPath + "/";
        //不存在就创建目录 
        if (!System.IO.Directory.Exists(_filePath))
        {
            System.IO.Directory.CreateDirectory(_filePath);
        }
        _filePath += exploreMap.MapId + "地图配置.txt";
        //
        FileStream fs1 = new FileStream(_filePath, FileMode.Create, FileAccess.Write);//创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);
        //
        sw.Write("wpID\tposition\taddPoint");//开始换行 
        foreach (var wp in ExploreSystem.Instance.WpAttributes)
        {
            sw.Write("\r\n");
            int wpid = wp.waypointId;
            if (wp.wp_template.WPCategory == 2)
            {
                Vector2 point = mapObjs[wpid].localPosition;
                sw.Write(wpid + "\t" + point.x + "," + point.y + "\t ");
                continue;
            }
            sw.Write(wpid + "\t" + " " + "\t");
            if (!wpPathAddObjs.ContainsKey(wpid)) continue;
            for (int i = 0; i < wpPathAddObjs[wpid].Count; i++)
            {
                Vector2 point1 = wpPathAddObjs[wpid][i].localPosition;
                if (i != 0) sw.Write("-");
                sw.Write("[" + point1.x + "," + point1.y + "]");
            }
        }

        sw.Close();
        fs1.Close();
    }

    /// <summary>
    /// 点击了添加点
    /// </summary>
    public void OnClickDlePoint()
    {
        int wpID = 0;
        try
        {
            wpID = int.Parse(wpIDInput2.text);
        }
        catch (Exception e) { return; }
        int index = 0;
        try
        {
            index = int.Parse(indexInput.text);
        }
        catch (Exception e) { return; }
        if (wpID > wpIndex.Count) return;
        if (!wpPathAddObjs.ContainsKey(wpIndex[wpID])) return;
        string name = wpIndex[wpID] + "_" + index;
        GameObject obj = null;
        for (int i = 0; i < wpPathAddObjs[wpIndex[wpID]].Count; i++)
        {
            if (wpPathAddObjs[wpIndex[wpID]][i].name == name)
            {
                obj = wpPathAddObjs[wpIndex[wpID]][i].gameObject;
                wpPathAddObjs[wpIndex[wpID]].RemoveAt(i);
                break;
            }
        }
        if (obj != null) DestroyImmediate(obj);
    }

    /// <summary>
    /// 点击了添加点
    /// </summary>
    public void OnClickAddPoint()
    {
        int wpID = 0;
        try
        {
            wpID = int.Parse(wpIDInput1.text);
        }
        catch (Exception e) { return; }
        if (wpID > wpIndex.Count) return;
        if (wpPathAddObjs.ContainsKey(wpIndex[wpID]))
        {
            if (wpPathAddObjs[wpIndex[wpID]].Count > 2) return;
        }
        addPointObj = ResourceLoadUtil.InstantiateRes(point, pointTrans).transform;
        if (!wpPathAddObjs.ContainsKey(wpIndex[wpID])) wpPathAddObjs.Add(wpIndex[wpID], new List<Transform>());
        wpPathAddObjs[wpIndex[wpID]].Add(addPointObj);
        string name = wpIndex[wpID] + "_" + wpPathAddObjs[wpIndex[wpID]].Count;
        addPointObj.Find("Text").GetComponent<Text>().text = name;
        addPointObj.name = name;
        addPointObj = null;
    }

    /// <summary>
    /// 点击画线
    /// </summary>
    public void OnClickGline()
    {
        try
        {
            smoothness = int.Parse(smoothnessInput.text);
        }
        catch (Exception e)
        {
            smoothness = 10;
        }

        ResourceLoadUtil.DeleteChildObj(pathTrans);
        wpPathObjs.Clear();
        //新建路点路径列表
        foreach (var item in ExploreSystem.Instance.WpAttributes)
        {
            //加载房间的资源   
            if (item.wp_template.WPCategory == 2) continue;
            //加载路径的资源 路劲只有一个后续点（房间有可能有多个后续点）
            if (!mapObjs.ContainsKey(item.previousWP)) continue;
            if (item.nextWP.Count == 0) continue;
            if (!mapObjs.ContainsKey(item.nextWP[0])) continue;

            Vector3 startPos = mapObjs[item.previousWP].localPosition;
            Vector3 endPos = mapObjs[item.nextWP[0]].localPosition;
            List<PathVectorInfo> pathVectors = CreatePath(item.waypointId, startPos, endPos, pathWidth, smoothness);
            //开始加载资源
            foreach (var posInfo in pathVectors)
            {
                var obj = ResourceLoadUtil.InstantiateRes(pathBg, pathTrans);
                obj.transform.localPosition = posInfo.pos;
                obj.transform.eulerAngles = posInfo.angle;
                //添加到列表
                if (!wpPathObjs.ContainsKey(item.waypointId)) wpPathObjs.Add(item.waypointId, new List<GameObject>());
                wpPathObjs[item.waypointId].Add(obj);
            }
        }
    }

    private ExploreMap exploreMap;

    /// <summary>
    /// 打开显示
    /// </summary>
    private void Start1()
    {
        new TeamSystem();
        Zone zone = new Zone(1001, FortType.Base);
        exploreMap = new ExploreMap( 11001, 0, 0, zone);
        //
        TeamSystem.Instance.CreateTeam();
        //
        new ExploreSystem(exploreMap);

        //
        GetObj();
        //
        UpdateGridShow(girdTrans);
        //
        LoadMapRes();
        //
        string str = "路径所对应的索引";
        for (int i = 0; i < wpIndex.Count; i++)
        {
            str += "\n" + wpIndex[i] + ":" + i;
        }
        Intro.text = str;
    }


    /// <summary>
    /// 加载地图资源
    /// </summary>
    private void LoadMapRes()
    {
        //wpIndex.Clear();
        //ResourceLoadUtil.DeleteChildObj(roomTrans);
        //mapObjs.Clear();
        //pathWidth = pathBg.GetComponent<RectTransform>().sizeDelta.x;
        ////新建路点路径列表
        //foreach (var item in ExploreSystem.Instance.WpAttributes)
        //{
        //    //加载房间的资源   
        //    if (item.wp_template.WPCategory != 2)
        //    {
        //        wpIndex.Add(item.waypointId);
        //        continue;
        //    };
        //    var obj = ResourceLoadUtil.InstantiateRes(roomBg, roomTrans);
        //    obj.transform.localPosition = GetShowPos(item.wp_template.position, showSize, grid, gridWidth);
        //    obj.name = item.waypointId.ToString();
        //    obj.transform.Find("Text").GetComponent<Text>().text = item.waypointId.ToString();
        //    //添加到列表
        //    if (!mapObjs.ContainsKey(item.waypointId)) mapObjs.Add(item.waypointId, null);
        //    mapObjs[item.waypointId] = obj.transform;
        //}
    }

    /// <summary>
    /// 更新格子显示
    /// </summary>
    /// <param name="grid"></param>
    private void UpdateGridShow(Transform grid)
    {
        int index = 0;
        foreach (Transform item in grid)
        {
            index++;
            item.Find("Text").GetComponent<Text>().text = index + "";
        }
    }


    /// <summary>
    /// 创建路径
    /// </summary>
    /// <param name="startPos">起点</param>
    /// <param name="endPos">结束点</param>
    /// <param name="resWidth">资源宽度</param>
    /// <param name="smoothness">光滑度</param>
    /// <returns>当前起点和结束点之前的路径</returns>
    private List<PathVectorInfo> CreatePath(int wpid, Vector3 startPos, Vector3 endPos, float resWidth, int smoothness)
    {
        List<Vector3> tempPos = new List<Vector3>();
        foreach (var item in wpPathAddObjs)
        {
            if (item.Key != wpid) continue;
            tempPos.AddRange(item.Value.Select(point => point.localPosition));
        }

        List<Vector3> nowPos = new List<Vector3> { startPos };
        nowPos.AddRange(tempPos);
        nowPos.Add(endPos);
        //先得到当前显示所有的点

        nowPos = GetVector3s(nowPos, smoothness);

        return CreatePath(nowPos, resWidth);
    }
    /// <summary>
    /// 得到显示位置
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="showSize">显示的区域大小：长*高</param>
    /// <param name="grid">格子排列：列*行</param>
    /// <param name="gridWidth">格子宽度</param>
    /// <returns></returns> 
    private Vector3 GetShowPos(int index, float[] showSize, int[] grid, float gridWidth)
    {
        Vector3 startPos = new Vector3(-(showSize[0] - gridWidth) / 2, (showSize[1] - gridWidth) / 2, 0);
        int nowIndex = index - 1;
        //行
        int row = nowIndex / grid[0];
        //列
        int column = nowIndex % grid[0];
        return startPos + Vector3.down * gridWidth * row + Vector3.right * gridWidth * column;
    }

    /// <summary>
    /// 创建路径
    /// </summary>
    private List<PathVectorInfo> CreatePath(List<Vector3> pos, float resWidth)
    {
        var savePos = new List<PathVectorInfo>();
        //
        var _temp = pos[0];
        float width, height;
        for (int i = 1; i < pos.Count; i++)
        {
            width = Math.Abs(pos[i].x - _temp.x);
            height = Math.Abs(pos[i].y - _temp.y);
            //是否相等
            if (Math.Pow(width, 2) + Math.Pow(height, 2) < Math.Pow(resWidth, 2)) continue;
            savePos.Add(new PathVectorInfo((_temp + pos[i]) / 2, new Vector3(0, 0, Angle_360(_temp, pos[i]))));
            _temp = pos[i];
        }
        return savePos;
    }
    /// <summary>
    /// 角度
    /// </summary>
    private float Angle_360(Vector3 from, Vector3 to)
    {
        //两点的x、y值
        float x = from.x - to.x;
        float y = from.y - to.y;
        //斜边长度
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));
        //求出弧度
        float cos = x / hypotenuse;
        float radian = Mathf.Acos(cos);
        //用弧度算出角度    
        float angle = 180 / (Mathf.PI / radian);
        if (y < 0)
        {
            angle = -angle;
        }
        else if ((y == 0) && (x < 0))
        {
            angle = 180;
        }
        return angle;
    }

    private void OnClickButton()
    {
        gameObject.SetActive(false);
    }

    private void GetObj()
    {
        if (isFirst) return;
        button = transform.GetComponent<Button>();
        roomBg = transform.Find("Temp/Room").gameObject;
        pathBg = transform.Find("Temp/Path").gameObject;
        pathTrans = transform.Find("Path");
        roomTrans = transform.Find("Room");
        markerTrans = transform.Find("Marker/Marker");
        //
        button.onClick.AddListener(OnClickButton);
        //
        isFirst = true;
    }

    //
    private Button button;
    private GameObject roomBg;
    private GameObject pathBg;
    private Transform pathTrans;
    private Transform roomTrans;
    private Transform markerTrans;
    //
    private float[] showSize = new float[] { 880f, 480 };
    private int[] grid = new int[] { 11, 6 };
    private float gridWidth = 80f;
    private Dictionary<int, List<GameObject>> wpPathObjs = new Dictionary<int, List<GameObject>>();
    private Dictionary<int, List<Transform>> wpPathAddObjs = new Dictionary<int, List<Transform>>();
    private Dictionary<int, Transform> mapObjs = new Dictionary<int, Transform>();
    private float pathWidth;
    private bool isFirst;

    /// <summary>
    /// 路径位置信息
    /// </summary>
    public class PathVectorInfo
    {
        public Vector3 pos;
        public Vector3 angle;

        public PathVectorInfo(Vector3 pos, Vector3 angle)
        {
            this.pos = pos;
            this.angle = angle;
        }
    }

}
