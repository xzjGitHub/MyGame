using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProtoBuf;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;





public class Test : MonoBehaviour
{
    public Transform itemlist;

    private int ComputeNowIndex(int nowValue, List<int> values, int startIndex, int endIndex)
    {
        try
        {
            //检查最后一个
            if (values.First() == nowValue)
            {
                return 0;
            }
            //检查最后一个
            if (values.Last() == nowValue)
            {
                return values.Count - 1;
            }
            int middleIndex = (startIndex + endIndex + 1) / 2;
            if (nowValue == values[middleIndex])
            {
                return middleIndex;
            }
            //先查当前表的前端或后端
            //在左边
            if (nowValue < values[middleIndex])
            {
                endIndex = middleIndex;
            }
            else //在右边
            {
                startIndex = middleIndex;
            }
            if (endIndex - startIndex == 1)
            {
                if (nowValue == values[startIndex])
                {
                    return startIndex;
                }
                if (nowValue == values[endIndex])
                {
                    return endIndex;
                }
                return nowValue > values[startIndex] ? startIndex : startIndex - 1;
            }
            return ComputeNowIndex(nowValue, values, startIndex, endIndex);
        }
        catch (Exception)
        {
            LogHelperLSK.LogWarning("计算错误");
            return -2;
        }
    }

    public RawImage image;


    //  public BlurEffect blurEffect;
    public RenderTexture renderBuffer;

    private void Awake()
    {
        Test5();
        //float max = Math.Abs(ranges[0]) > Math.Abs(ranges[1]) ? Math.Abs(ranges[0]) : Math.Abs(ranges[1]);
        //float[] newRanges = { -max, max };
        //LogHelperLSK.Log("min=" + newRanges[0] + "  max=" + newRanges[1]);
        //List<float> values = RandomBuilder.Randoms(newRanges[0], newRanges[1]);
        //float StdDev = RandomBuilder.CalculateStdDev(values);
        //float ave = RandomBuilder.Ave(values);
        //LogHelperLSK.Log("StdDev=" + StdDev);
        //List<float> list = new List<float>();

        //for (int i = 0; i < 1000; i++)
        //{
        //    float temp;
        //    do
        //    {
        //        temp = GetRand(0, StdDev);
        //    } while (temp > newRanges[1] || temp < ranges[0]);

        //    str += temp + "\n";
        //    // (int)(RandomBuilder.Random_Normal(10, -10) * 100) / 100f + "\n";
        //}
        //ConfigManager.ResPath = Application.dataPath + "/Resources/Config";
        //ConfigManager.Instance.Init(delegate (int a, int b) { LogHelperLSK.Log("Progress: " + a + "     " + b); }, delegate { LogHelperLSK.Log("Finshed"); });
        //for (int i = 0; i < itemlist.childCount; i++)
        //{
        //    images.Add(i, itemlist.GetChild(i).GetComponent<Image>());
        //    texts.Add(i, images[i].transform.Find("Text").GetComponent<Text>());
        //    texts[i].fontSize = 30;
        //}
    }

    public void Test5()
    {

        TestEquip();
        TestChar();
    }

    private List<int> weaponList = new List<int>() { 1000113, 100014, 100002 };
    private List<int> clothesList = new List<int>() { 1000151, 1000154, 1000152 };
    private readonly List<int> necklaceList = new List<int>() { 1000072, };
    private readonly List<int> ringList = new List<int>() { 1000083, 1000082, 1000081 };
    private readonly List<int> weaponEnchant = new List<int>() { 900511, 900513, 900512 };
    private readonly List<int> clothesEnchant = new List<int>() { 900523, 900521, 900522 };
    private List<EquipAttribute> equipAttributes = new List<EquipAttribute>();
    private List<int> charList = new List<int>() { 6201, 6101, 1501 };
    private List<CharAttribute> charAttributes = new List<CharAttribute>();

    private void TestEquip()
    {
        equipAttributes.Clear();
        //创建装备
        int itemid = 0;
        foreach (int item in weaponList)
        {
            equipAttributes.Add(new EquipAttribute(new EquimentCreate(item, itemid)));
            itemid++;
        }
        foreach (int item in clothesList)
        {
            equipAttributes.Add(new EquipAttribute(new EquimentCreate(item, itemid)));
            itemid++;
        }
        foreach (int item in necklaceList)
        {
            equipAttributes.Add(new EquipAttribute(new EquimentCreate(item, itemid)));
            itemid++;
        }
        foreach (int item in ringList)
        {
            equipAttributes.Add(new EquipAttribute(new EquimentCreate(item, itemid)));
            itemid++;
        }
        //装备附魔
        for (int i = 0; i < weaponList.Count; i++)
        {
            equipAttributes.Find(a => a.instanceID == weaponList[i]).EquipEnchanted(weaponEnchant[i]);
        }
        for (int i = 0; i < clothesList.Count; i++)
        {
            equipAttributes.Find(a => a.instanceID == clothesList[i]).EquipEnchanted(clothesEnchant[i]);
        }
    }

    private void TestChar()
    {
        charAttributes.Clear();


        for (int i = 0; i < charList.Count; i++)
        {
            CharAttribute tempCharAttribute = new CharAttribute(new CharCreate(charList[i]));
            //穿戴装备
            EquipAttribute tempEquipAttribute = equipAttributes.Find(a => a.instanceID == weaponList[i]);
            tempCharAttribute.CharWearEquipment(tempEquipAttribute);
            //
            tempEquipAttribute = equipAttributes.Find(a => a.instanceID == clothesList[i]);
            tempCharAttribute.CharWearEquipment(tempEquipAttribute);
            //
            tempEquipAttribute = equipAttributes.Find(a => a.instanceID == necklaceList[0]);
            tempCharAttribute.CharWearEquipment(tempEquipAttribute);
            //
            tempEquipAttribute = equipAttributes.Find(a => a.instanceID == ringList[i]);
            tempCharAttribute.CharWearEquipment(tempEquipAttribute);
            //
            charAttributes.Add(tempCharAttribute);
        }
        return;
        List<string> strs = new List<string>();
        //创建角色
        List<int> temp = new List<int>() { /*6201, 6101, 6001,*/1501 };
        Dictionary<int, CharAttribute> charAttributes1 = new Dictionary<int, CharAttribute>();
        Dictionary<int, CharAttribute> charAttributes2 = new Dictionary<int, CharAttribute>();
        foreach (int item in temp)
        {
            charAttributes1.Add(item, new CharAttribute(new CharCreate(item)));
        }
        foreach (int item in temp)
        {
            charAttributes2.Add(item, new CharAttribute(new CharCreate(item)));
        }
        //创建装备
        temp = new List<int>()
            {1000111,1000152/*1000011, 1000013, 100004, 1000051, 1000052, 1000054, 1000111,1000113, 100014,  1000151, 1000152, 1000154*/};
        Dictionary<int, EquipAttribute> equipAttributes1 = new Dictionary<int, EquipAttribute>();
        foreach (int item in temp)
        {
            equipAttributes1.Add(item, new EquipAttribute(new EquimentCreate(item, item)));

            strs.Add("装备instanceID=" + item + "："
                     + "  upgradeAll=" + equipAttributes1[item].equipRnd.upgradeAll
                     + "  upgradeRnd=" + equipAttributes1[item].equipRnd.upgradeRnd
                     + "  tempArmor=" + equipAttributes1[item].tempArmor
                     + "  tempAttack=" + equipAttributes1[item].tempAttack
                     + "  tempEnergyReg=" + equipAttributes1[item].tempEnergyReg
                     + "  rndArmor1=" + equipAttributes1[item].equipRnd.rndArmor1
                     + "  rndAP1=" + equipAttributes1[item].equipRnd.rndAP1
                     + "  rndEnergyReg1=" + equipAttributes1[item].equipRnd.rndEnergyReg1);

            //LogHelperLSK.Log("装备instanceID=" + item + "：tempArmor" + equipAttributes[item].tempArmor
            //                 + "  tempAttack" + equipAttributes[item].tempAttack
            //                 + "  tempEnergyReg" + equipAttributes[item].tempEnergyReg
            //                 + "  rndArmor1" + equipAttributes[item].equipRnd.rndArmor1
            //                 + "  rndAP1" + equipAttributes[item].equipRnd.rndAP1
            //                 + "  rndEnergyReg1" + equipAttributes[item].equipRnd.rndEnergyReg1);

        }
        //角色穿0级装备
        //charAttributes1[1501].CharWearEquipment(equipAttributes1[1000111]);
        //charAttributes1[1501].CharWearEquipment(equipAttributes1[1000152]);
        ////
        //charAttributes2[1501].CharWearEquipment(equipAttributes1[1000111]);
        //charAttributes2[1501].CharWearEquipment(equipAttributes1[1000152]);

        //StateAttribute state1 = new StateAttribute(10001, 10001, 10001, new CombatUnit(charAttributes1[1501], 0), new CombatUnit(charAttributes2[1501], 0), 0);
        //StateAttribute state2 = new StateAttribute(15011, 15011, 15011, new CombatUnit(charAttributes1[1501], 0), new CombatUnit(charAttributes2[1501], 0), 0);
        //state1.ExecuteEffect();
        //state2.ExecuteEffect();
        //charAttributes1[6201].CharWearEquipment(equipAttributes[1000013]);
        //charAttributes1[6201].CharWearEquipment(equipAttributes[1000051]);
        ////
        //charAttributes1[6101].CharWearEquipment(equipAttributes[100004]);
        //charAttributes1[6101].CharWearEquipment(equipAttributes[1000054]);
        ////
        //charAttributes1[6001].CharWearEquipment(equipAttributes[1000011]);
        //charAttributes1[6001].CharWearEquipment(equipAttributes[1000052]);
        ////
        //Log(0, charAttributes1, ref strs);
        ////卸载装备
        //charAttributes1[6201].CharStripAllEquipment();
        //charAttributes1[6101].CharStripAllEquipment();
        //charAttributes1[6001].CharStripAllEquipment();
        ////角色穿1级装备
        //charAttributes1[6201].CharWearEquipment(equipAttributes[1000113]);
        //charAttributes1[6201].CharWearEquipment(equipAttributes[1000151]);
        ////
        //charAttributes1[6101].CharWearEquipment(equipAttributes[100014]);
        //charAttributes1[6101].CharWearEquipment(equipAttributes[1000154]);
        ////
        //charAttributes1[6001].CharWearEquipment(equipAttributes[1000111]);
        //charAttributes1[6001].CharWearEquipment(equipAttributes[1000152]);
        //
        GameDataManager.SaveData("charData", "test1", (object)charAttributes1);
        //   GameDataManager.SaveData("charData", "test2", (object)charAttributes2);
        Log(1, charAttributes1, ref strs);
        GameDataManager.SaveData("charData", "test3", (object)strs);
    }

    private void Log(int level, Dictionary<int, CharAttribute> charAttributes, ref List<string> strs)
    {
        strs.Add("//////角色穿戴" + level + "级装备属性//////");
        //  LogHelperLSK.Log("//////角色穿戴" + level + "级装备属性//////");
        foreach (int item in charAttributes.Keys)
        {
            strs.Add("角色" + item + "："
                             + "  finalHP=" + charAttributes[item].finalHP
                             + "  finalArmor=" + charAttributes[item].finalArmor
                             + "  finalAP=" + charAttributes[item].finalAP
                             + "  finalSP=" + charAttributes[item].finalSP
                             + "  finalEnergyReg=" + charAttributes[item].finalEnergyReg
                             + "  expDP=" + 0
                             + "  expDB=" + 0
            //         + "  finalHealing=" + charAttributes[item].finalHealing
            );
            //LogHelperLSK.Log("角色" + item + "："
            //                 + "  finalHP=" + charAttributes[item].finalHP
            //                 + "  finalArmor=" + charAttributes[item].finalArmor
            //                 + "  finalAP=" + charAttributes[item].finalAP
            //                 + "  finalSP=" + charAttributes[item].finalSP
            //                 + "  finalEnergyReg=" + charAttributes[item].finalEnergyReg
            //                 + "  expDP=" + charAttributes[item].expDP
            //                 + "  expDB=" + charAttributes[item].expDB
            //                 + "  finalHealing=" + charAttributes[item].finalHealing
            //);
        }



    }

    public float GetRand(float ave, float stdDev)
    {

        float u1 = RandomBuilder.RandomNum(0f, 1f), u2 = RandomBuilder.RandomNum(0f, 1f), r;
        //double a = Math.Log(u1, Math.E);
        //double b = 2 * Math.PI * u2;
        //double c = -Math.Sqrt(2.0f * a);

        //  r = (float)(20 + 5 * c * b);
        if (u1 < 0 || u1 > 1)
        {
            LogHelperLSK.Log(u1);
        }
        r = (float)(ave + stdDev * Math.Sqrt(-2 * Math.Log(u1/*, Math.E*/)) * Math.Cos(2 * Math.PI * u2));

        return r;
    }

    private void Test2()
    {
        List<int> a = new List<int>();
        List<int> b = new List<int>();
        int num = 100000;
        int sum = num;
        while (sum > 0)
        {
            a.Add(RandomBuilder.RandomNum(120, 80));
            b.Add(RandomBuilder.RandomNum(108, 72));
            sum--;
        }
        //
        Dictionary<int, float> c = new Dictionary<int, float>();
        Dictionary<int, float> d = new Dictionary<int, float>();
        for (int i = 80; i <= 108; i++)
        {
            c.Add(i, 0);
            d.Add(i, 0);
        }
        //
        List<int> key = new List<int>();
        key.AddRange(c.Keys);


        foreach (int item in key)
        {
            try
            {
                int e = a.FindAll(dg => dg == item).ToList().Count;
                c[item] = e / (float)num;
                e = b.FindAll(dg => dg == item).ToList().Count;
                d[item] = e / (float)num;
            }
            catch (Exception) { continue; }
        }

    }

    private void Test3()
    {
        List<int> lists = new List<int> { 0, 0, 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 0, 0, 0 };
        LogHelperLSK.Log(ComputeNowIndex(45, lists, 0, lists.Count - 1));
    }

    private void Start()
    {
        //foreach (int item in RandomBuilder.GetArr(1, 20, 4, 20))
        //{
        //    LogHelperLSK.Log(" " + item);
        //}

        // Test3();
        //   LogHelperLSK.LogError(RandomBuilder.GetRandom(new int[] { 80, 120 }, new int[] { 72, 108 }));
        // serialization();


        //skeletonAnimation = transform.GetComponent<SkeletonAnimation>();
        //skeletonAnimation.skeleton.A = 0;
        ////  skeletonAnimation.GetComponent<Renderer>().material.color=new Color(0,0,0,0);
        //aMaterial = transform.GetComponent<MeshRenderer>().sharedMaterial;

        //aMaterial.SetColor("_OutlineColor", new Color(1, 0, 0, 0));
        //   aMaterial.color=new Color(1,1,1,0);
        //  LogHelperLSK.LogError(aMaterial.color);
        //  TestCreateSpine();
    }

    //序列化User里面的信息内容
    private void serialization()
    {
        PlayerData _data = new PlayerData { token = 1000 };

        using (FileStream fs = File.Create(Application.dataPath + "/user.bin")) //文件输出流
        {
            Serializer.Serialize<PlayerData>(fs, _data); //把user对象序列化出二进制文件放入fs文件里面
        }
    }

    private void TestCreateSpine()
    {
        isCreate = true;
        StartCoroutine(CreateSpine());
    }


    private float time = 0;
    private bool isCreate;

    public bool startCalculate;

    private void Update()
    {
        if (startCalculate)
        {
            startCalculate = false;
            Test1();
        }

        if (!isCreate)
        {
            return;
        }
        //  aMaterial.SetColor("_OutlineColor", new Color(0, 0, 0, 0.5f));

        time += Time.deltaTime;

        //if (skeletonAnimation.skeleton.A < 1)
        //{

        //    skeletonAnimation.skeleton.A += Time.deltaTime * 0.1f;
        //    if (skeletonAnimation.skeleton.A > 1)
        //    {
        //        aMaterial.SetColor("_OutlineColor", new Color(1, 0, 0, skeletonAnimation.skeleton.A));
        //        skeletonAnimation.skeleton.A = 1;
        //    }
        //}
    }

    private IEnumerator CreateSpine()
    {
        string _path = "Test/1014/Anima_1014";
        SpineNameInfo _info = new SpineNameInfo
        {
            jsonName = _path,
            atlasName = _path + ".atlas",
            textureName = "Test/Anima_1014",
        };


        yield return null;
        SkeletonTool.CreateSpine(_info, new GameObject());
        //for (int i = 0; i < 8; i++)
        //{

        //    ///*  yield return*/ StartCoroutine( addSpineComponent(new GameObject(), _path + ".atlas", _path));
        //}

        isCreate = false;
        yield return null;
        LogHelperLSK.LogError("time=" + time);
    }

    private IEnumerator addSpineComponent(UnityEngine.Object obj, string AtlasFileNames, string JsonFileName)
    {
        GameObject go = GameObject.Instantiate(obj) as GameObject;
        go.name = obj.name;
        GameObject.DontDestroyOnLoad(go);

        SkeletonAnimation sa;
        SkeletonDataAsset sda;

        sda = ScriptableObject.CreateInstance<SkeletonDataAsset>();
        sda.fromAnimation = new string[0];
        sda.toAnimation = new string[0];
        sda.duration = new float[0];
        sda.scale = 0.01f;
        sda.defaultMix = 0.15f;
        //SpineNameInfo中存着.json,.atlas,.mat的名称
        //   SpineNameInfo sni = dicMonsterSpineInfos[go.name];

        AtlasAsset[] arrAtlasData = new AtlasAsset[1];
        for (int i = 0; i < arrAtlasData.Length; i++)
        {
            string atlasFileName = AtlasFileNames;
            AtlasAsset atlasdata = ScriptableObject.CreateInstance<AtlasAsset>();
            atlasdata.atlasFile = Resources.Load(AtlasFileNames) as TextAsset;
            atlasdata.materials = new Material[1];
            //     atlasdata.materials[0] = Resources.Load(MaterialName) as Material;
            atlasdata.materials[0] = new Material(Shader.Find("Spine/Skeleton"));
            atlasdata.materials[0].SetTexture("_MainTex", Resources.Load("Test/Anima_1014") as Texture);
            //atlasdata.materials = mats;
            arrAtlasData[i] = atlasdata;
        }

        sda.atlasAssets = arrAtlasData;
        sda.skeletonJSON = Resources.Load(JsonFileName) as TextAsset;

        sa = go.AddComponent<SkeletonAnimation>();
        sa.skeletonDataAsset = sda;
        //  sa.calculateNormals = true;
        //   sa.skeletonDataAsset.();
        //  sa.AnimationName = "run";
        //   sa.loop = true;

        sa.Initialize(false);
        LogHelperLSK.LogError("time=" + time);
        yield return null;
    }

    /*
     * 1、确定x*y格子
     * 2、添加格子  先行再列的方式排序
     * 3、格子编号 <key=编号、value=List<int>可连接的点列表>
     * 4、从一个格子编号编号计算位置  <key= 格子编号、value=int[x,y]>
     * 5、没有结果就退出，重新开始计算
     */

    public int rowSum = 1;
    public int columnSum = 4;
    //计算的最大次数
    public int calculateMaxSum = 10000;
    //格子减少的比例
    public int gridReduceRatio = 1;
    //连续重复方向的最大数量
    public int repetitionMaxSum = 4;
    //垂直方向的最大数量
    public int verticalMaxNum = 5;
    public int horizontalRatio = 70;
    public int startPoint = -1;

    private void Test1()
    {
        for (int i = 0; i < itemlist.childCount; i++)
        {
            images[i].enabled = false;
            texts[i].text = "";
        }

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
        foreach (int item in list)
        {
            _wpTemplate = WP_templateConfig.GetWpTemplate(item);
            relations.Add(item, _wpTemplate.nextWP);
        }

        gridRelations = relations;
        GetGrids(gridRelations);

        LogHelperLSK.Log(nowGrids);
        foreach (KeyValuePair<int, int> item in nowGrids)
        {
            //itemlist.GetChild(item.Value).Find("Text").GetComponent<Text>().text = item.Key.ToString();
            images[item.Value].enabled = true;
            texts[item.Value].text = item.Key.ToString();
        }

        //int _column = 0;
        //bool isUpdate = false;
        //bool isOk = true;

        //while (!isUpdate)
        //{
        //    if (_column > calculateMaxSum)
        //    {
        //        isUpdate = true;
        //        isOk = false;
        //        LogHelperLSK.LogError("计算失败" + grids.First().Key);
        //        continue;
        //    }
        //    _column++;
        //    isUpdate = GetGridList();
        //}
        //if (!isOk) return;
        //LogHelperLSK.LogWarning("计算次数" + _column);
        //foreach (var item in grids)
        //{
        //    //itemlist.GetChild(item.Value).Find("Text").GetComponent<Text>().text = item.Key.ToString();
        //    images[item.Value].enabled = true;
        //    texts[item.Value].text = item.Key.ToString();
        //}


    }



    private int tempNowIndex;
    //当前行
    private int nowRow;
    //当前列
    private int nowColumn;
    private int tempUpIndex;
    //垂直方向的最小点
    private int verticalMinIndex;
    //垂直方向的最大点
    private int verticalMaxIndex;

    private bool GetGridList()
    {
        grids.Clear();
        bool ok = false;
        foreach (KeyValuePair<int, List<int>> _relation in relations)
        {
            ok = GetGridInfo(_relation.Key, relations);
            if (!ok)
            {
                return false;
            }
            continue;
            //先算第一个格子
            if (_relation.Key == relations.First().Key)
            {
                tempNowIndex = startPoint == -1
                    ? RandomBuilder.RandomNum(labelList.Count - 1, 0)
                    : startPoint;
                nowRow = GetRow(tempNowIndex);
                verticalMinIndex = nowRow;
                verticalMaxIndex = nowRow;
                grids.Add(_relation.Key, tempNowIndex);
                //
                if (GetGridInfo(_relation.Key, relations))
                {
                    continue;
                }
                return false;
            }
            //
            if (!grids.ContainsKey(_relation.Key))
            {
                continue;
            }
            if (!relations.ContainsKey(_relation.Key))
            {
                continue;
            }
            if (GetGridInfo(_relation.Key, relations))
            {
                continue;
            }
            return false;
        }
        return true;
    }

    /// <summary>
    /// / 获取格子信息
    /// </summary>
    /// <param name="_girdId">格子ID</param>
    /// <param name="_relationList">链接表</param>
    /// <returns></returns>
    private bool GetGridInfo(int _girdId, Dictionary<int, List<int>> _relationLists)
    {
        // NewGirdIndex(_girdId, _relationLists);

        //int _nowGridIndex = grids.ContainsKey(_girdId) ? grids[_girdId] : -1;
        //if (!grids.ContainsKey(_girdId))
        //{
        int _nowGridIndex = NewGirdIndex(_girdId, _relationLists);
        if (_nowGridIndex == -1)
        {
            return false;
        }
        //    }
        //二、计算链接格子
        //NewRelationGridIndex(_girdId, _relationLists, _nowGridIndex);
        return true;
    }


    /// <summary>
    /// 得到行
    /// </summary>
    private int GetRow(int _index)
    {
        return _index / columnSum + 1;
    }

    /// <summary>
    /// 得到列
    /// </summary>
    private int GetColumn(int _index)
    {
        if (_index <= columnSum - 1)
        {
            return _index + 1;
        }
        return _index + 1 - _index / columnSum * columnSum;
    }

    //方向位：上、下、左、右  、左上、左下、右上、右下  编号=-1时，获取编号出错
    /// <summary>
    /// 得到向上方向的编号
    /// </summary>
    private int GetUpIndex(int _index)
    {
        return GetRow(_index) == 1 ? -1 : _index - columnSum;
    }

    /// <summary>
    /// 得到向下方向的编号
    /// </summary>
    private int GetDownIndex(int _index)
    {
        return GetRow(_index) == rowSum ? -1 : _index + columnSum;
    }

    /// <summary>
    /// 得到向左方向的编号
    /// </summary>
    private int GetLeftIndex(int _index)
    {
        return GetColumn(_index) == 1 ? -1 : _index - 1;
    }

    /// <summary>
    /// 得到向右方向的编号
    /// </summary>
    private int GetRightIndex(int _index)
    {
        return GetColumn(_index) == columnSum ? -1 : _index + 1;
    }

    /// <summary>
    /// 得到向左下方向的编号
    /// </summary>
    private int GetLeftLowerIndex(int _index)
    {
        if (GetColumn(_index) == 1 || GetRow(_index) == rowSum)
        {
            return -1;
        }
        return _index + columnSum - 1;
    }

    /// <summary>
    /// 得到向左上方向的编号
    /// </summary>
    private int GetLeftUpperIndex(int _index)
    {
        if (GetColumn(_index) == 1 || GetRow(_index) == 1)
        {
            return -1;
        }
        return _index - columnSum - 1;
    }

    /// <summary>
    /// 得到向右下方向的编号
    /// </summary>
    private int GetRightLowerIndex(int _index)
    {
        if (GetColumn(_index) == columnSum || GetRow(_index) == rowSum)
        {
            return -1;
        }
        return _index + columnSum + 1;
    }

    /// <summary>
    /// 得到向右上方向的编号
    /// </summary>
    private int GetRightUpperIndex(int _index)
    {
        if (GetColumn(_index) == columnSum || GetRow(_index) == 1)
        {
            return -1;
        }
        return _index - columnSum + 1;
    }

    /// <summary>
    /// 得到下一个格子的位置 ,-1时得到格子失败
    /// </summary>
    private int GetNextIndex(int _index, List<int> _list = null)
    {
        nowRow = GetRow(_index);
        nowColumn = GetColumn(_index);
        //方向列表为空
        if (_list == null)
        {
            _list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        }
        //第一行
        if (nowRow == 1)
        {
            _list.Remove(1);
            _list.Remove(5);
            _list.Remove(7);
        }
        //最后一行
        if (nowRow == rowSum)
        {
            _list.Remove(2);
            _list.Remove(6);
            _list.Remove(8);
        }
        //第一列
        if (nowColumn == 1)
        {
            _list.Remove(3);
            _list.Remove(5);
            _list.Remove(6);
        }
        //最后一列
        if (nowColumn == columnSum)
        {
            _list.Remove(4);
            _list.Remove(7);
            _list.Remove(8);
        }
        //判断垂直方向的最高度
        //if (verticalMaxIndex - verticalMinIndex >= verticalMaxNum - 1)
        //{
        //    _list.Remove(1);
        //    _list.Remove(2);
        //    _list.Remove(5);
        //    _list.Remove(6);
        //    _list.Remove(7);
        //    _list.Remove(8);
        //}
        //没有可用的方向
        if (_list.Count == 0)
        {
            return -1;
        }
        //方向连续个数
        if (tempList.Count > repetitionMaxSum)
        {
            //连续上下
            if (_list.Count > 1 && _list.Contains(tempUpIndex) && (tempUpIndex == 1 || tempUpIndex == 2))
            {
                _list.Remove(tempUpIndex);
            }
            tempList.Clear();
        }
        //同时存在上|下&&左|右
        //if (_list.Count == 2 && (_list.Contains(1) || _list.Contains(2)) && (_list.Contains(3) || _list.Contains(4)))
        //{
        //    tempUpIndex = RandomBuilder.RandomChanceIndex(new List<float> { horizontalRatio, 100 - horizontalRatio }, 100) == 0 ? _list[1] : _list[0];
        //}
        //else
        //{
        //    if (_list.Count >= 3)
        //    {
        //        if (_list.Contains(3) || _list.Contains(4))
        //        {
        //            _list.Remove(1);
        //            _list.Remove(2);
        //        }
        //    }
        //    tempUpIndex = RandomBuilder.GetRandomList(1, _list)[0];
        //    tempList.Add(tempUpIndex);
        //}
        tempUpIndex = RandomBuilder.RandomList(1, _list)[0];

        return GetGridIndex(tempUpIndex, _index);
    }
    /// <summary>
    /// 得到方向
    /// </summary>
    private int GetDirection(int _index, int _target)
    {
        if (_target == GetUpIndex(_index))
        {
            return 1;
        }

        if (_target == GetDownIndex(_index))
        {
            return 2;
        }

        if (_target == GetLeftIndex(_index))
        {
            return 3;
        }

        if (_target == GetRightIndex(_index))
        {
            return 4;
        }

        if (_target == GetLeftUpperIndex(_index))
        {
            return 5;
        }

        if (_target == GetLeftLowerIndex(_index))
        {
            return 6;
        }

        if (_target == GetRightUpperIndex(_index))
        {
            return 7;
        }

        if (_target == GetRightLowerIndex(_index))
        {
            return 8;
        }

        return -1;
    }
    /// <summary>
    /// 得到方向
    /// </summary>
    private int GetOppositeDirection(int _index, int _target)
    {
        if (_target == GetUpIndex(_index))
        {
            return 2;
        }

        if (_target == GetDownIndex(_index))
        {
            return 1;
        }

        if (_target == GetLeftIndex(_index))
        {
            return 4;
        }

        if (_target == GetRightIndex(_index))
        {
            return 3;
        }

        if (_target == GetLeftUpperIndex(_index))
        {
            return 6;
        }

        if (_target == GetLeftLowerIndex(_index))
        {
            return 5;
        }

        if (_target == GetRightUpperIndex(_index))
        {
            return 8;
        }

        if (_target == GetRightLowerIndex(_index))
        {
            return 7;
        }

        return -1;
    }

    /// <summary>
    /// 清除已经占用了的方向
    /// </summary>
    /// <param name="_gridIndex"></param>
    /// <param name="_grids"></param>
    /// <returns></returns>
    private List<int> CleanUsedDirection(int _gridIndex, List<int> _grids)
    {
        List<int> _list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        //已经有上||左上||右上
        if (_grids.Contains(GetUpIndex(_gridIndex)) ||
            _grids.Contains(GetLeftUpperIndex(_gridIndex)) ||
            _grids.Contains(GetRightUpperIndex(_gridIndex)))
        {
            _list.Remove(1);
            _list.Remove(5);
            _list.Remove(7);
        }
        //已经有已经有下||左下||右下
        if (_grids.Contains(GetDownIndex(_gridIndex)) ||
            _grids.Contains(GetLeftLowerIndex(_gridIndex)) ||
            _grids.Contains(GetRightLowerIndex(_gridIndex)))
        {
            _list.Remove(2);
            _list.Remove(6);
            _list.Remove(8);
        }
        //已经有左
        if (_grids.Contains(GetLeftIndex(_gridIndex)))
        {
            _list.Remove(3);
        }
        //已经有右
        if (_grids.Contains(GetRightIndex(_gridIndex)))
        {
            _list.Remove(4);
        }

        return _list;
    }

    /// <summary>
    /// 获取已经占用了的方向
    /// </summary>
    /// <param name="_gridIndex"></param>
    /// <param name="_grids"></param>
    /// <returns></returns>
    private List<int> GetUsedDirection(int _gridIndex, List<int> _grids)
    {
        List<int> _list = new List<int>();
        //已经有上||左上||右上
        if (_grids.Contains(GetUpIndex(_gridIndex)) ||
            _grids.Contains(GetLeftUpperIndex(_gridIndex)) ||
            _grids.Contains(GetRightUpperIndex(_gridIndex)))
        {
            _list.Add(1);
            _list.Add(5);
            _list.Add(7);
        }
        //已经有已经有下||左下||右下
        if (_grids.Contains(GetDownIndex(_gridIndex)) ||
            _grids.Contains(GetLeftLowerIndex(_gridIndex)) ||
            _grids.Contains(GetRightLowerIndex(_gridIndex)))
        {
            _list.Add(2);
            _list.Add(6);
            _list.Add(8);
        }
        //已经有左
        if (_grids.Contains(GetLeftIndex(_gridIndex)))
        {
            _list.Add(3);
        }
        //已经有右
        if (_grids.Contains(GetRightIndex(_gridIndex)))
        {
            _list.Add(4);
        }

        return _list;
    }



    private int NewGirdIndex(int _girdId, Dictionary<int, List<int>> _relationList, List<int> _usedList = null)
    {
        if (_usedList != null && _usedList.Count >= labelList.Count)
        {
            return -1;
        }

        int _nowGridIndex = -1;
        //一、计算现在格子的位置
        //1、先判断格子列表中是否包含有链接列表中的格子
        List<int> _relationGridIds = new List<int>();
        foreach (int item in _relationList[_girdId])
        {
            //包含有当前链接格子
            if (grids.Any(a => a.Key == item))
            {
                _relationGridIds.Add(grids[item]);
            }
        }
        //2、如果_relationGridIds不为空 需要根据现在有个格子来确定当前格子生成的位置
        if (_relationGridIds.Count > 0)
        {
            Dictionary<int, List<int>> _dictionary = new Dictionary<int, List<int>>();
            //a、计算每一项 8个位置的id
            foreach (int _relation in _relationGridIds)
            {
                //得到八个方向的索引
                _dictionary.Add(_relation, new List<int>
                {
                    GetUpIndex(_relation),
                    GetDownIndex(_relation),
                    GetLeftIndex(_relation),
                    GetRightIndex(_relation),
                    GetLeftUpperIndex(_relation),
                    GetLeftLowerIndex(_relation),
                    GetRightUpperIndex(_relation),
                    GetRightLowerIndex(_relation),
                });
            }
            //b、根据词典得出每一个列表中都有新的相同id列表
            List<int> _sameList = new List<int>();
            List<int> _keys = new List<int>();
            _keys.AddRange(_dictionary.Keys);

            for (int i = 0; i < _dictionary.Count - 1; i++)
            {
                if (i == 0)
                {
                    _sameList = _dictionary[_keys[i]].FindAll(a => _dictionary[_keys[i + 1]].Contains(a));
                    continue;
                }
                _sameList = _dictionary[_keys[i + 1]].FindAll(a => _sameList.Contains(a));
            }
            //清除不可能出现的位置 即==-1
            bool _tempUpdate = false;
            while (!_tempUpdate)
            {
                bool _isMove = false;
                for (int i = 0; i < _sameList.Count; i++)
                {
                    if (_sameList[i] != -1)
                    {
                        continue;
                    }

                    _isMove = true;
                    _sameList.Remove(-1);
                }
                _tempUpdate = !_isMove;
            }
            //现在当前格子只能在_sameList中的位置进行筛选 得到一个位置
            if (_sameList.Count > 0)
            {
                _nowGridIndex = RandomBuilder.RandomList(1, _sameList)[0];
            }
        }
        else //3、没有链接格子 直接取一个位置
        {
            //全部可以用
            if (_usedList == null)
            {
                _nowGridIndex = startPoint == -1
                    ? RandomBuilder.RandomNum(labelList.Count - 1, 0)
                    : startPoint;
            }
            else
            {
                //列表中可用
                _nowGridIndex = RandomBuilder.RandomList(1, _usedList)[0];
            }
        }
        //判断是否已经暂用了
        if (grids.ContainsValue(_nowGridIndex))
        {
            return -1;
        }

        if (!grids.ContainsKey(_girdId))
        {
            grids.Add(_girdId, _nowGridIndex);
            //更新行列信息
            nowRow = GetRow(_nowGridIndex);
            nowColumn = GetColumn(_nowGridIndex);
            verticalMinIndex = nowRow <= verticalMinIndex ? nowRow : verticalMinIndex;
            verticalMaxIndex = nowRow > verticalMaxIndex ? nowRow : verticalMaxIndex;
        }

        //新建链接格子
        NewRelationGridIndex(_girdId, _relationList, _nowGridIndex);
        return _nowGridIndex;
    }

    private bool NewRelationGridIndex(int _girdId, Dictionary<int, List<int>> _relationList, int _nowGridIndex)
    {
        foreach (int item in _relationList[_girdId])
        {
            //已经有了检查位置是否符合要求
            if (grids.ContainsKey(item))
            {
                //判断链接方向是否正确
                if (grids[item] == GetUpIndex(_girdId) ||
                    grids[item] == GetDownIndex(_girdId) ||
                    grids[item] == GetLeftIndex(_girdId) ||
                    grids[item] == GetRightIndex(_girdId) ||
                    grids[item] == GetLeftUpperIndex(_girdId) ||
                    grids[item] == GetLeftLowerIndex(_girdId) ||
                    grids[item] == GetRightUpperIndex(_girdId) ||
                    grids[item] == GetRightLowerIndex(_girdId))
                {
                    continue;
                }

                tempList.Clear();
                return false;
            }
            List<int> _useList = new List<int>();
            ////遍历可用方向 得到可用的列表
            //foreach (var _direction in CleanUsedDirection(_nowGridIndex, grids.Keys))
            //{
            //    _useList.Add(GetGridIndex(_direction, _nowGridIndex));
            //}
            int _tempNowIndex = NewGirdIndex(item, _relationList, _useList.Count == 0 ? null : _useList);
            //获得下一个格子失败 ||检测是否已经有这个格子了
            if (_tempNowIndex == -1 || grids.Any(_temp => _temp.Value == _tempNowIndex))
            {
                tempList.Clear();
                return false;
            }
            ////清除已经占用了的位置
            //     _tempNowIndex = GetNextIndex(grids[_girdId], CleanUsedDirection(_nowGridIndex, grids));
            ////获得下一个格子失败 ||检测是否已经有这个格子了
            //if (_tempNowIndex == -1 || grids.Any(_temp => _temp.Value == _tempNowIndex))
            //{
            //    tempList.Clear();
            //    return false;
            //}
            //grids.Add(item, _tempNowIndex);
            ////更新行列信息
            //nowRow = GetRow(_tempNowIndex);
            //nowColumn = GetColumn(_tempNowIndex);
            //verticalMinIndex = nowRow <= verticalMinIndex ? nowRow : verticalMinIndex;
            //verticalMaxIndex = nowRow > verticalMaxIndex ? nowRow : verticalMaxIndex;
        }
        return true;
    }


    private int GetGridIndex(int _direction, int _index)
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

    private List<int> tempList = new List<int>();

    /// <summary>
    /// 获取不能使用的索引
    /// </summary>
    private List<int> GetUnusableIndexs(int _index, List<int> _existings)
    {
        List<int> _lists = new List<int>();
        foreach (int item in GetUsedDirection(_index, _existings))
        {
            int _tempIndex = GetGridIndex(item, _index);
            if (_tempIndex != -1 && !_lists.Contains(_tempIndex))
            {
                _lists.Add(_tempIndex);
            }
        }
        return _lists;
    }

    private List<int> GetUsableIndexs(int _index, List<int> _existings)
    {
        List<int> _lists = new List<int>();
        if (!_existings.Contains(GetUpIndex(_index)))
        {
            _lists.Add(GetUpIndex(_index));
        }

        if (!_existings.Contains(GetDownIndex(_index)))
        {
            _lists.Add(GetDownIndex(_index));
        }

        if (!_existings.Contains(GetRightIndex(_index)))
        {
            _lists.Add(GetRightIndex(_index));
        }

        if (!_existings.Contains(GetLeftUpperIndex(_index)))
        {
            _lists.Add(GetLeftUpperIndex(_index));
        }

        if (!_existings.Contains(GetLeftLowerIndex(_index)))
        {
            _lists.Add(GetLeftLowerIndex(_index));
        }

        if (!_existings.Contains(GetRightUpperIndex(_index)))
        {
            _lists.Add(GetRightUpperIndex(_index));
        }

        if (!_existings.Contains(GetRightLowerIndex(_index)))
        {
            _lists.Add(GetRightLowerIndex(_index));
        }
        //清除不可能出现的位置 即==-1
        CleanList(_lists);
        return _lists;
    }


    //不可用的索引列表
    private List<int> unusableIndexs = new List<int>();
    private Dictionary<int, List<int>> gridIndexBackups = new Dictionary<int, List<int>>();
    //
    //生成的格子
    private Dictionary<int, int> nowGrids = new Dictionary<int, int>();
    private Dictionary<int, List<int>> gridRelations = new Dictionary<int, List<int>>();
    private List<int> gridIds = new List<int>();

    public int maxNum = 10;

    /// <summary>
    /// 获得格子
    /// </summary>
    private void GetGrids(Dictionary<int, List<int>> _relations)
    {
        int _index = 0;
        int _sum = 0;

        while (_index < maxNum)
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
                if (!GetInfo(i))
                {
                    LogHelperLSK.LogError(i);
                    _sum++;
                    break;
                }
            }
            _index++;
        }
        LogHelperLSK.LogError("生成出错" + _sum);
    }

    private bool GetInfo(int _startIndex, bool _call = false, int _errorIndex = -1)
    {
        return ResetCreateGird(_startIndex) || BackupsCreateGird(_startIndex - 1);
    }

    /// <summary>
    /// 创建第一个格子
    /// </summary>
    private bool CreateFirstGird()
    {
        nowGrids.Clear();
        gridIndexBackups.Clear();
        unusableIndexs.Clear();
        //
        int _nowGridIndex = startPoint == -1 ? RandomBuilder.RandomNum(labelList.Count - 1, 0) : startPoint;
        nowGrids.Add(gridIds[0], _nowGridIndex);
        return true;
    }
    /// <summary>
    /// 重新生成格子
    /// </summary>
    private bool ResetCreateGird(int _startIndex)
    {
        if (_startIndex < 0)
        {
            return false;
        }
        if (_startIndex >= gridIds.Count)
        {
            return true;
        }
        //当前格子的父亲列表
        List<int> _parentLists = new List<int>();
        //
        List<int> _nowUsableIndexs = new List<int>();
        Dictionary<int, List<int>> _nowUsableLists = new Dictionary<int, List<int>>();
        //
        int _nowGirdId = gridIds[_startIndex];
        if (_nowGirdId == 10106)
        {
            //     LogHelperLSK.LogWarning("");
        }
        UpdateUnusableIndexs(_nowGirdId);
        //
        _parentLists = GetParentLists(_nowGirdId, gridRelations);
        foreach (int item in _parentLists)
        {
            if (!nowGrids.ContainsKey(item))
            {
                continue;
            }
            //生成的格子中有这个
            _nowUsableLists.Add(item, GetUsableIndexs(nowGrids[item], unusableIndexs));
        }

        //得到现在可以用的格子索引
        _nowUsableIndexs = GetSameLists(_nowUsableLists);
        //如果没有可以使用的格子
        if (_nowUsableIndexs.Count == 0)
        {
            return BackupsCreateGird(_startIndex - 1);
        }
        //生成当前格子
        nowGrids.Add(_nowGirdId, RandomBuilder.RandomList(1, _nowUsableIndexs)[0]);
        _nowUsableIndexs.Remove(nowGrids[_nowGirdId]);
        //备份当前ID可用的格子
        gridIndexBackups.Add(_nowGirdId, _nowUsableIndexs);
        return true;
    }
    /// <summary>
    /// 备份生成格子
    /// </summary>
    /// <returns></returns>
    private bool BackupsCreateGird(int _startIndex)
    {
        if (_startIndex > gridIds.Count - 1)
        {
            return false;
        }
        //
        if (_startIndex == 0)
        {
            CreateFirstGird();
            return ResetCreateGird(1);
        }
        int _nowGirdId = gridIds[_startIndex];
        List<int> _nowUsableIndexs = new List<int>();
        if (nowGrids.ContainsKey(_nowGirdId))
        {
            nowGrids.Remove(_nowGirdId);
        }
        //
        if (!gridIndexBackups.ContainsKey(_nowGirdId))
        {
            if (BackupsCreateGird(_startIndex - 1))
            {
                return ResetCreateGird(_startIndex + 1);
            }
            return false;
        }
        _nowUsableIndexs = gridIndexBackups[_nowGirdId];
        if (_nowUsableIndexs.Count == 0)
        {
            gridIndexBackups.Remove(_nowGirdId);
            if (BackupsCreateGird(_startIndex - 1))
            {
                return ResetCreateGird(_startIndex + 1);
            }
            return false;
        }
        //
        try
        {
            //生成当前格子
            nowGrids.Add(_nowGirdId, RandomBuilder.RandomList(1, _nowUsableIndexs)[0]);
            //备份当前ID可用的格子
            gridIndexBackups[_nowGirdId].Remove(nowGrids[_nowGirdId]);
            if (gridIndexBackups[_nowGirdId].Count == 0)
            {
                gridIndexBackups.Remove(_nowGirdId);
            }
            return ResetCreateGird(_startIndex + 1);
        }
        catch (Exception e)
        {
            LogHelperLSK.LogError(e.Message);
            return false;
        }

    }


    /// <summary>
    /// 得到当前格子的父亲列表
    /// </summary>
    private List<int> GetParentLists(int _index, Dictionary<int, List<int>> _relations)
    {
        List<int> _list = new List<int>();
        foreach (KeyValuePair<int, List<int>> item in _relations)
        {
            if (!item.Value.Contains(_index))
            {
                continue;
            }

            AddValue(_list, item.Key);
        }
        return _list;
    }

    private List<int> GetOppositeParentLists(int _index, Dictionary<int, List<int>> _relations)
    {
        if (!_relations.ContainsKey(_index))
        {
            return new List<int>();
        }

        List<int> _list = new List<int>();
        foreach (int temp in _relations[_index])
        {
            AddValue(_list, temp);
        }
        return _list;
    }
    /// <summary>
    /// 得到相同的列表
    /// </summary>
    private List<int> GetSameLists(Dictionary<int, List<int>> _listDictionary)
    {
        List<int> _list = new List<int>();
        if (_listDictionary.Count == 1)
        {
            _list.AddRange(_listDictionary.First().Value);
        }
        else
        {
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
        }
        return _list;
    }

    /// <summary>
    /// 更新不能使用的格子
    /// </summary>
    private void UpdateUnusableIndexs(int _gridid)
    {
        unusableIndexs.Clear();
        //
        unusableIndexs.AddRange(nowGrids.Values);
        //当前格子的父列表
        List<int> _parentLists = new List<int>();
        //找到要链接它的点
        foreach (int item in GetParentLists(_gridid, gridRelations))
        {
            _parentLists = GetParentLists(item, gridRelations);
            foreach (int _parent in _parentLists)
            {
                if (!nowGrids.ContainsKey(_parent))
                {
                    continue;
                }

                int _temp = GetDirection(item, nowGrids[_parent]);
                GetDirectionAddValue1(unusableIndexs, _temp, nowGrids[item]);
            }
            //计算他去链接其他的点
            _parentLists = GetOppositeParentLists(item, gridRelations);
            foreach (int _parent in _parentLists)
            {
                if (!nowGrids.ContainsKey(_parent))
                {
                    continue;
                }

                GetDirectionAddValue2(unusableIndexs, nowGrids[_parent]);
            }
        }

        //foreach (var item in nowGrids)
        //{
        //    //先添加自己
        //    AddValue(unusableIndexs, nowGrids[item.Key]);
        //    //所有父列表
        //    _parentLists = GetParentLists(item.Key, gridRelations);
        //    foreach (var _parent in _parentLists)
        //    {
        //        //现在格子中没有这个父点
        //        if (!nowGrids.ContainsKey(_parent)) continue;
        //        int _temp = GetOppositeDirection(item.Value, nowGrids[_parent]);
        //        //
        //        GetDirectionAddValue(unusableIndexs, _temp, item.Value);
        //    }
        //    //得到相反的方向
        //    _parentLists.Clear();
        //    _parentLists = GetOppositeParentLists(item.Key, gridRelations);
        //    foreach (var _parent in _parentLists)
        //    {
        //        //现在格子中没有这个父点
        //        if (!nowGrids.ContainsKey(_parent)) continue;
        //        int _temp = GetDirection(item.Value, nowGrids[_parent]);
        //        //
        //        GetDirectionAddValue(unusableIndexs, _temp, item.Value);
        //    }
        //}
        //清除不可能出现的位置 即==-1
        CleanList(unusableIndexs);
    }

    private void AddValue(List<int> _list, int _value)
    {
        if (_list == null || _list.Contains(_value))
        {
            return;
        }

        _list.Add(_value);
    }

    private void CleanList(List<int> _list, int _value = -1)
    {
        bool _tempUpdate = false;
        while (!_tempUpdate)
        {
            bool _isMove = false;
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] != _value)
                {
                    continue;
                }

                _isMove = true;
                _list.Remove(_value);
            }
            _tempUpdate = !_isMove;
        }
    }

    private void GetDirectionAddValue(List<int> _list, int _direction, int _index)
    {
        switch (_direction)
        {
            case 1:
            case 5:
            case 7:
                AddValue(unusableIndexs, GetUpIndex(_index));
                AddValue(unusableIndexs, GetLeftUpperIndex(_index));
                AddValue(unusableIndexs, GetRightUpperIndex(_index));
                break;
            case 2:
            case 6:
            case 8:
                AddValue(unusableIndexs, GetDownIndex(_index));
                AddValue(unusableIndexs, GetLeftLowerIndex(_index));
                AddValue(unusableIndexs, GetRightLowerIndex(_index));
                break;
            case 3:
                AddValue(unusableIndexs, GetLeftIndex(_index));
                break;
            case 4:
                AddValue(unusableIndexs, GetRightIndex(_index));
                break;
        }
    }

    private void GetDirectionAddValue1(List<int> _list, int _direction, int _index)
    {
        switch (_direction)
        {
            case 1:
                AddValue(unusableIndexs, GetUpIndex(_index));
                break;
            case 5:
                AddValue(unusableIndexs, GetLeftUpperIndex(_index));
                break;
            case 7:
                AddValue(unusableIndexs, GetRightUpperIndex(_index));
                break;
            case 2:
                AddValue(unusableIndexs, GetDownIndex(_index));
                break;
            case 6:
                AddValue(unusableIndexs, GetLeftLowerIndex(_index));
                break;
            case 8:
                AddValue(unusableIndexs, GetRightLowerIndex(_index));
                break;
            case 3:
                AddValue(unusableIndexs, GetLeftIndex(_index));
                break;
            case 4:
                AddValue(unusableIndexs, GetRightIndex(_index));
                break;
        }
    }
    /// <summary>
    /// 自己链接其他
    /// </summary>
    private void GetDirectionAddValue2(List<int> _list, int _index)
    {
        #region 判断上
        //上已被用
        if (GetBool(1, _index))
        {
            AddValue(_list, GetUpIndex(_index));
        }
        //左和左上都在
        if ((GetBool(3, _index) || GetBool(6, _index) && (GetBool(2, _index) || GetBool(4, _index) && GetBool(8, _index))) && GetBool(5, _index))
        {
            AddValue(_list, GetUpIndex(_index));
        }
        //右和右上都在
        if ((GetBool(4, _index) || GetBool(8, _index) && (GetBool(2, _index) || GetBool(3, _index) && GetBool(6, _index))) && GetBool(7, _index))
        {
            AddValue(_list, GetUpIndex(_index));
        }
        #endregion

        #region 判断下
        //下已被用
        if (GetBool(2, _index))
        {
            AddValue(_list, GetDownIndex(_index));
        }
        //左和左下都在
        if ((GetBool(3, _index) || (GetBool(1, _index) || GetBool(4, _index) && GetBool(7, _index)) && GetBool(5, _index)) && GetBool(6, _index))
        {
            AddValue(_list, GetDownIndex(_index));
        }
        //右和右下都在
        if ((GetBool(4, _index) || (GetBool(1, _index) || GetBool(3, _index) && GetBool(5, _index)) && GetBool(7, _index)) && GetBool(8, _index))
        {
            AddValue(_list, GetDownIndex(_index));
        }
        #endregion

        #region 判断左
        //左已被用
        if (GetBool(3, _index))
        {
            AddValue(_list, GetLeftIndex(_index));
        }
        //
        if ((GetBool(1, _index) || (GetBool(4, _index) || GetBool(4, _index) && GetBool(8, _index)) && GetBool(7, _index)) && GetBool(5, _index))
        {
            AddValue(_list, GetLeftIndex(_index));
        }
        //
        if ((GetBool(2, _index) || (GetBool(4, _index) || GetBool(1, _index) && GetBool(7, _index)) && GetBool(8, _index)) && GetBool(6, _index))
        {
            AddValue(_list, GetLeftIndex(_index));
        }
        #endregion

        #region 判断右
        //右已被用
        if (GetBool(4, _index))
        {
            AddValue(_list, GetRightIndex(_index));
        }
        //
        if ((GetBool(1, _index) || (GetBool(3, _index) || GetBool(2, _index) && GetBool(6, _index)) && GetBool(5, _index)) && GetBool(7, _index))
        {
            AddValue(_list, GetRightIndex(_index));
        }
        //
        if ((GetBool(2, _index) || (GetBool(3, _index) || GetBool(1, _index) && GetBool(5, _index)) && GetBool(6, _index)) && GetBool(8, _index))
        {
            AddValue(_list, GetRightIndex(_index));
        }
        #endregion

        #region 判断左上
        //左上已被用
        if (GetBool(5, _index))
        {
            AddValue(_list, GetLeftUpperIndex(_index));
        }
        //
        if ((GetBool(3, _index) || (GetBool(6, _index) && (GetBool(2, _index) || GetBool(4, _index) && GetBool(8, _index))))
            && (GetBool(1, _index) || GetBool(7, _index) && (GetBool(4, _index) || GetBool(2, _index) && GetBool(8, _index))))
        {
            AddValue(_list, GetLeftUpperIndex(_index));
        }

        #endregion

        #region 判断左下
        //左下已被用
        if (GetBool(6, _index))
        {
            AddValue(_list, GetLeftLowerIndex(_index));
        }
        //
        if ((GetBool(3, _index) || (GetBool(5, _index) && (GetBool(1, _index) || GetBool(4, _index) && GetBool(7, _index))))
            && (GetBool(2, _index) || GetBool(8, _index) && (GetBool(4, _index) || GetBool(1, _index) && GetBool(7, _index))))
        {
            AddValue(_list, GetLeftLowerIndex(_index));
        }
        #endregion

        #region 判断右上
        //右上已被用
        if (GetBool(7, _index))
        {
            AddValue(_list, GetRightUpperIndex(_index));
        }
        //
        if ((GetBool(1, _index) || (GetBool(5, _index) && (GetBool(3, _index) || GetBool(2, _index) && GetBool(6, _index))))
            && (GetBool(4, _index) || GetBool(8, _index) && (GetBool(2, _index) || GetBool(3, _index) && GetBool(6, _index))))
        {
            AddValue(_list, GetLeftUpperIndex(_index));
        }
        #endregion

        #region 判断右下
        //右下已被用
        if (GetBool(8, _index))
        {
            AddValue(_list, GetRightLowerIndex(_index));
        }
        //
        if ((GetBool(2, _index) || (GetBool(6, _index) && (GetBool(3, _index) || GetBool(1, _index) && GetBool(5, _index))))
            && (GetBool(4, _index) || GetBool(7, _index) && (GetBool(1, _index) || GetBool(3, _index) && GetBool(5, _index))))
        {
            AddValue(_list, GetRightLowerIndex(_index));
        }
        #endregion
    }


    private bool GetBool(int _direction, int _index)
    {
        return nowGrids.ContainsValue(GetGridIndex(_direction, _index));
    }

    //
    private int maxSum;
    //
    public Transform TesTransform;
    public SkeletonAnimation skeletonAnimation;

    private Vector3 startVector3 = new Vector3(-550, -250, 0);
    private Vector3 endVector3 = new Vector3(150, -250, 0);

    //
    private List<int> labelList = new List<int>();

    //
    private Dictionary<int, int> grids = new Dictionary<int, int>();
    private Dictionary<int, List<int>> relations = new Dictionary<int, List<int>>();

    private readonly Dictionary<int, Image> images = new Dictionary<int, Image>();
    private readonly Dictionary<int, Text> texts = new Dictionary<int, Text>();

    private readonly ScriptSystem scriptSystem;
    private readonly Material aMaterial;
}




#region MyRegion
//private bool GetInfo(int _startIndex, bool _call = false, int _errorIndex = -1)
//{
//    if (_startIndex < 1)
//    {
//        return false;
//    }
//    //使用了的索引
//    List<int> usableIndexs = new List<int>();
//    //当前格子的父亲列表
//    List<int> _parentLists = new List<int>();
//    //
//    int _nowGirdId;
//    List<int> _nowUsableIndexs = new List<int>();
//    Dictionary<int, List<int>> _nowUsableLists = new Dictionary<int, List<int>>();
//    //
//    _nowGirdId = gridIds[_startIndex];

//    _errorIndex = _errorIndex == -1 ? _startIndex : _errorIndex;

//    usableIndexs.AddRange(unusableIndexs);
//    //不是回滚
//    if (!_call)
//    {
//        _parentLists = GetParentLists(_nowGirdId, gridRelations);
//        foreach (var item in _parentLists)
//        {
//            if (!nowGrids.ContainsKey(item)) continue;
//            //生成的格子中有这个
//            _nowUsableLists.Add(item, GetUsableIndexs(nowGrids[item], usableIndexs));
//        }
//        //得到现在可以用的格子索引
//        _nowUsableIndexs = GetSameLists(_nowUsableLists);
//        LogHelperLSK.LogError(_startIndex + _call.ToString() + _nowGirdId);
//    }
//    else
//    {
//        LogHelperLSK.LogError(_startIndex + _call.ToString() + _nowGirdId + "备份表包含" + _nowGirdId + gridIndexBackups.ContainsKey(_nowGirdId));
//        if (gridIndexBackups.ContainsKey(_nowGirdId))
//        {
//            LogHelperLSK.LogError(_startIndex + _call.ToString() + _nowGirdId + "备份表" + gridIndexBackups[_nowGirdId].Count);
//            _nowUsableIndexs = gridIndexBackups[_nowGirdId];
//        }
//    }
//    //如果没有可以使用的格子
//    if (_nowUsableIndexs.Count == 0)
//    {
//        if (nowGrids.Count == 0)
//        {
//            return false;
//        }
//        //并且备份当前ID可用的格子为空
//        if (gridIndexBackups.Count == 0)
//        {
//            LogHelperLSK.LogError(_startIndex + _call.ToString() + "没有备份表");
//            return false;
//            return GetInfo(_startIndex);
//        }
//        //移除前一个格子
//        int _removeId = nowGrids.Last().Key;
//        LogHelperLSK.LogError(_startIndex + _call.ToString() + _nowGirdId + "没有可用的位置");
//        unusableIndexs.Remove(nowGrids[_removeId]);
//        nowGrids.Remove(_removeId);
//        //首先检测备份中还是否有可用的格子
//        if (gridIndexBackups.ContainsKey(_removeId))
//        {
//            if (gridIndexBackups[_removeId].Count == 0)
//            {
//                gridIndexBackups.Remove(_removeId);
//                if (_startIndex - 1 < 1)
//                {
//                    return false;
//                }
//                LogHelperLSK.LogError(_startIndex + _call.ToString() + "备份表为空新建前一个");
//                return GetInfo(_startIndex - 1, false, _startIndex);
//            }
//            //移除最后一个
//            LogHelperLSK.LogError(_startIndex + _call.ToString() + "备份表" + _removeId + "移除之前总数" + gridIndexBackups[_removeId].Count);
//            gridIndexBackups[_removeId].Remove(gridIndexBackups[_removeId].Last());
//            //
//            if (gridIndexBackups[_removeId].Count == 0)
//            {
//                gridIndexBackups.Remove(_removeId);
//                if (_startIndex - 1 < 1)
//                {
//                    return false;
//                }
//                LogHelperLSK.LogError(_startIndex + _call.ToString() + "备份表移除当前最后一个位置新建前一个" + (_startIndex - 2));
//                return GetInfo(_startIndex - 1, false, _startIndex);
//            }
//            try
//            {
//                //递归这里出错 还要详细分析 成死循环
//                //重新生成前一个格子
//                nowGrids.Add(_removeId, RandomBuilder.GetRandomList(1, gridIndexBackups[_removeId])[0]);
//                unusableIndexs.Add(nowGrids[_removeId]);
//                LogHelperLSK.LogError(_startIndex + _call.ToString() + _removeId + "备份表选择位置新建前一个OK" + gridIndexBackups[_removeId].Count);
//                return GetInfo(_startIndex, false);
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }
//        LogHelperLSK.LogError(_startIndex + _call.ToString() + "不包含" + _removeId + "当前备份表重新新建前一个");
//        return GetInfo(_startIndex - 1, true, _startIndex);
//    }
//    //生成当前格子
//    nowGrids.Add(_nowGirdId, RandomBuilder.GetRandomList(1, _nowUsableIndexs)[0]);
//    _nowUsableIndexs.Remove(nowGrids[_nowGirdId]);
//    //备份当前ID可用的格子
//    gridIndexBackups.Add(_nowGirdId, _nowUsableIndexs);
//    //生成不能使用索引
//    unusableIndexs.Add(nowGrids[_nowGirdId]);
//    LogHelperLSK.LogError(_startIndex + _call.ToString() + _nowGirdId + "重新新建一个Ok");
//    if (_startIndex != _errorIndex)
//    {
//        return GetInfo(_errorIndex);
//    }
//    return true;
//}


#endregion