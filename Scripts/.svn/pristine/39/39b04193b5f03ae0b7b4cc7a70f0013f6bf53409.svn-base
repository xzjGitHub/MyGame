using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class ResourceLoadUtil
{
    private const string charModuelPath = "char/model/";
    private const string skillEffcetPath = "char/skill/";
    private const string healingGlobPath = "Exp/Event/HealingGlob";
    private const string eventPath = "Exp/Event/";
    private const string NPCModuelPath = "char/model/Npc/";
    //
    private const string combatResName = "Combat";
    private const string invasionResName = "Invasion";
    private const string combatModulePath = "UI/Modules/Combat";
    private const string exploreModulePath = "UI/Modules/Explore";
    private const string selectCharModulePath = "UI/Modules/SelectChar";
    private const string invasionModulePath = "UI/Modules/Invasion";
    private const string damageShow = "UI/Modules/DamageIntroShow";
    private const string hpShow = "UI/Modules/CharHpShow";
    private const string tagShow = "UI/Modules/Tag";
    //要塞
    private const string fortRes = "UI/Prefab/Fort";
    private const string fortIconRes = "FortIcon/";
    private const string fortMapRes = "UI/Prefab/FortMap";
    private const string fortMapIconRes = "FortMapIcon/";
    private const string zoneRes = "UI/Prefab/Zone";
    private const string mapIcon = "FortMapIcon/";
    //
    public const string armorblockRes = "armorblock";
    public const string armorbreakRes = "armorbreak";
    public const string shieldblockRes = "shieldblock";
    public const string shieldbreakRes = "shieldbreak";
    public const string armorRegRes = "armorReg";
    public const string shieldRegRes = "shieldReg";
    //
    private static Dictionary<string, Dictionary<string, object>> alreadyLoadRes = new Dictionary<string, Dictionary<string, object>>();
    private static Dictionary<string, object> spineRes = new Dictionary<string, object>();




    /// <summary>
    /// 加载护盾
    /// </summary>
    public static GameObject LoadShieldBlock(object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(skillEffcetPath + shieldblockRes))
        {
            AddResDictionary(combatResName, skillEffcetPath + shieldblockRes, LoadRes<GameObject>(skillEffcetPath + shieldblockRes));
        }
        GameObject obj= ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][skillEffcetPath + shieldblockRes] as GameObject), parent, localScale, localPosition);
        obj.name = shieldblockRes;
        return obj;
    }
    /// <summary>
    /// 加载护盾损坏
    /// </summary>
    public static GameObject LoadShieldBreak(object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(skillEffcetPath + shieldbreakRes))
        {
            AddResDictionary(combatResName, skillEffcetPath + shieldbreakRes, LoadRes<GameObject>(skillEffcetPath + shieldbreakRes));
        }
        GameObject obj = ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][skillEffcetPath + shieldbreakRes] as GameObject), parent, localScale, localPosition);

        obj.name = shieldbreakRes;
        return obj;
    }
    /// <summary>
    /// 加载护甲
    /// </summary>
    public static GameObject LoadArmorBlock(object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(skillEffcetPath + armorblockRes))
        {
            AddResDictionary(combatResName, skillEffcetPath + armorblockRes, LoadRes<GameObject>(skillEffcetPath + armorblockRes));
        }
        GameObject obj = ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][skillEffcetPath + armorblockRes] as GameObject), parent, localScale, localPosition);
        obj.name = armorblockRes;
        return obj;
    }
    /// <summary>
    /// 加载护甲损坏
    /// </summary>
    public static GameObject LoadArmorBreak(object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(skillEffcetPath + armorbreakRes))
        {
            AddResDictionary(combatResName, skillEffcetPath + armorbreakRes, LoadRes<GameObject>(skillEffcetPath + armorbreakRes));
        }
        GameObject obj = ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][skillEffcetPath + armorbreakRes] as GameObject), parent, localScale, localPosition);
        obj.name = armorbreakRes;
        return obj;
    }

    /// <summary>
    /// 加载地图sprite
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadMapSprite(string path)
    {
        return LoadSprite(mapIcon + path);
    }
    /// <summary>
    /// 加载地图sprite
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadFortSprite(string path)
    {
        return LoadSprite(fortIconRes + path);
    }

    /// <summary>
    /// 加载角色模型
    /// </summary>
    public static GameObject LoadNpcModel(string path, object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(NPCModuelPath + path))
        {
            AddResDictionary(combatResName, NPCModuelPath + path, LoadRes<GameObject>(NPCModuelPath + path));
            // AddResDictionary(combatResName, path, SkeletonTool.CreateSpine(path));
        }
        //
        return ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][NPCModuelPath + path] as GameObject), parent, localScale, localPosition);
    }

    /// <summary>
    /// 加载区域
    /// </summary>
    public static GameObject LoadZoneRes(object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(zoneRes))
        {
            AddResDictionary(combatResName, zoneRes, LoadRes<GameObject>(zoneRes));
        }
        return InstantiateRes(alreadyLoadRes[combatResName][zoneRes] as GameObject, parent, localScale, localPosition);
    }
    /// <summary>
    /// 加载要塞
    /// </summary>
    public static GameObject LoadFortRes(string name, object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(fortRes))
        {
            AddResDictionary(combatResName, fortRes, LoadRes<GameObject>(fortRes));
        }
        //加载图标
        GameObject obj = alreadyLoadRes[combatResName][fortRes] as GameObject;
        Image image = obj.transform.Find("Button").GetComponent<Image>();
        image.sprite = LoadSprite(fortIconRes + name);
        //image.sprite = SpriteManager.Instance.GetSprite(SpriteTypeNameDefine.FortIcon,name);
        image.SetNativeSize();
        //
        return InstantiateRes(obj, parent, localScale, localPosition);
    }
    /// <summary>
    /// 加载要塞地图
    /// </summary>
    public static UIFortExploreMap LoadFortMapRes(int mapType, object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(fortMapRes))
        {
            AddResDictionary(combatResName, fortMapRes, LoadRes<GameObject>(fortMapRes));
        }
        //加载图标
        GameObject obj = alreadyLoadRes[combatResName][fortMapRes] as GameObject;
        Image image = obj.transform.Find("Button").GetComponent<Image>();
        image.sprite = LoadSprite(fortMapIconRes + mapType);
        // image.sprite = SpriteManager.Instance.GetSprite(SpriteTypeNameDefine.FortMapIcon,mapType.ToString());
        image.SetNativeSize();
        //
        return InstantiateRes(obj, parent, localScale, localPosition).AddComponent<UIFortExploreMap>();
    }


    /// <summary>
    /// 删除战斗资源
    /// </summary>
    public static void DeleteCombatRes()
    {
        alreadyLoadRes.Remove(combatResName);
    }

    /// <summary>
    /// 加载地图
    /// </summary>
    public static GameObject LoadMap(string path, object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(path))
        {
            AddResDictionary(combatResName, path, LoadRes<GameObject>(path));
        }
        //
        return ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][path] as GameObject), parent, localScale, localPosition);
    }


    /// <summary>
    /// 加载角色模型
    /// </summary>
    public static GameObject LoadCharModel(int _charId, object parent = null, object localScale = null, object localPosition = null)
    {
        CharRPack charRPack = CharRPackConfig.GeCharShowTemplate(CharSystem.Instance.GetCharAttribute(_charId).templateID);
        string path = charRPack.charRP;
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(path))
        {
            AddResDictionary(combatResName, path, LoadRes<GameObject>(charModuelPath + path));
            // AddResDictionary(combatResName, path, SkeletonTool.CreateSpine(path));
        }
        //
        return ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][path] as GameObject), parent, localScale, localPosition);
    }

    /// <summary>
    /// 加载角色模型
    /// </summary>
    public static GameObject LoadCharModel(string path, object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(path))
        {
            AddResDictionary(combatResName, path, LoadRes<GameObject>(charModuelPath + path));
            // AddResDictionary(combatResName, path, SkeletonTool.CreateSpine(path));
        }
        //
        return ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][path] as GameObject), parent, localScale, localPosition);
    }
    /// <summary>
    /// 加载技能特效
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject LoadSkillEffect(string path, object parent = null, object localScale = null, object localPosition = null)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(path))
        {
            AddResDictionary(combatResName, path, LoadRes<GameObject>(skillEffcetPath + path));
        }
        //
        return ObjSetParent(InstantiateRes(alreadyLoadRes[combatResName][path] as GameObject), parent, localScale, localPosition);
    }


    /// <summary>
    /// 加载选择角色模块
    /// </summary>
    /// <returns></returns>
    public static UISelectCharInfo LoadSelectCharModule()
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(selectCharModulePath))
        {
            AddResDictionary(combatResName, selectCharModulePath, LoadRes<GameObject>(selectCharModulePath));
        }
        //
        return InstantiateRes(alreadyLoadRes[combatResName][selectCharModulePath] as GameObject).AddComponent<UISelectCharInfo>();
    }

    /// <summary>
    /// 加载选择角色模块
    /// </summary>
    /// <returns></returns>
    public static void LoadSelectCharModule(System.Action<UISelectCharInfo> action)
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(selectCharModulePath))
        {
            new ResourceLoadAsync(selectCharModulePath, LoadSelectCharModuleAction, action);
            return;
        }
        //
        UISelectCharInfo obj = InstantiateRes(alreadyLoadRes[combatResName][selectCharModulePath] as GameObject).AddComponent<UISelectCharInfo>();
        if (action != null)
        {
            action(obj);
        }
    }


    private static void LoadSelectCharModuleAction(ResourceLoadAsync loadAsync, object action)
    {
        AddResDictionary(combatResName, selectCharModulePath, loadAsync.Res.asset);
        UISelectCharInfo obj = InstantiateRes(alreadyLoadRes[combatResName][selectCharModulePath] as GameObject).AddComponent<UISelectCharInfo>();
        if (action != null)
        {
            (action as System.Action<UISelectCharInfo>)(obj);
        }

    }


    /// <summary>
    /// 加载战斗模块
    /// </summary>
    /// <returns></returns>
    public static UICombatUIOperation LoadCombatModule()
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(combatModulePath))
        {
            AddResDictionary(combatResName, combatModulePath, LoadRes<GameObject>(combatModulePath));
        }
        //
        return InstantiateRes(alreadyLoadRes[combatResName][combatModulePath] as GameObject).AddComponent<UICombatUIOperation>();
    }

    /// <summary>
    /// 加载探索模块
    /// </summary>
    /// <returns></returns>
    public static void LoadExploreModule()
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(exploreModulePath))
        {
            new ResourceLoadAsync(exploreModulePath, LoadExploreModuleAction);
            return;
        }
        //
        InstantiateRes(alreadyLoadRes[combatResName][exploreModulePath] as GameObject).AddComponent<UIExploreOperation>();
    }

    private static void LoadExploreModuleAction(ResourceLoadAsync loadAsync)
    {
        AddResDictionary(combatResName, exploreModulePath, loadAsync.Res.asset);
        InstantiateRes(alreadyLoadRes[combatResName][exploreModulePath] as GameObject).AddComponent<UIExploreOperation>();
    }


    /// <summary>
    /// 加载探索模块
    /// </summary>
    /// <returns></returns>
    public static UIExploreOperation LoadExploreModule1()
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(exploreModulePath))
        {
            AddResDictionary(combatResName, exploreModulePath, LoadRes<GameObject>(exploreModulePath));
        }
        //
        return InstantiateRes(alreadyLoadRes[combatResName][exploreModulePath] as GameObject).AddComponent<UIExploreOperation>();
    }
    /// <summary>
    /// 加载入侵模块
    /// </summary>
    /// <returns></returns>
    public static UIInvasionOperation LoadInvasionModule()
    {
        if (!alreadyLoadRes.ContainsKey(invasionResName) || !alreadyLoadRes[invasionResName].ContainsKey(invasionModulePath))
        {
            AddResDictionary(invasionResName, invasionModulePath, LoadRes<GameObject>(invasionModulePath));
        }
        //
        return InstantiateRes(alreadyLoadRes[invasionResName][invasionModulePath] as GameObject).AddComponent<UIInvasionOperation>();
    }

    /// <summary>
    /// 加载事件资源
    /// </summary>
    public static T LoadEventRes<T>(string _path) where T : Object
    {
        return Resources.Load<T>(eventPath + _path);
    }

    /// <summary>
    /// 加载生命球资源
    /// </summary>
    public static T LoadHealingGlobRes<T>() where T : Object
    {
        return Resources.Load<T>(healingGlobPath);
    }

    /// <summary>
    /// 加载字体
    /// </summary>
    public static Font LoadFont(string path)
    {
        return Resources.Load<Font>("Font/" + path);
    }

    /// <summary>
    /// 加载hp显示
    /// </summary>
    public static GameObject LoadHpShow()
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(hpShow))
        {
            AddResDictionary(combatResName, hpShow, LoadRes<GameObject>(hpShow));
        }
        return alreadyLoadRes[combatResName][hpShow] as GameObject;
    }

    /// <summary>
    /// 加载hp显示
    /// </summary>
    public static GameObject LoadTagShow()
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(tagShow))
        {
            AddResDictionary(combatResName, tagShow, LoadRes<GameObject>(tagShow));
        }
        return alreadyLoadRes[combatResName][tagShow] as GameObject;
    }

    /// <summary>
    /// 加载伤害显示
    /// </summary>
    public static GameObject LoadDamageIntroShow()
    {
        if (!alreadyLoadRes.ContainsKey(combatResName) || !alreadyLoadRes[combatResName].ContainsKey(damageShow))
        {
            AddResDictionary(combatResName, damageShow, LoadRes<GameObject>(damageShow));
        }
        return alreadyLoadRes[combatResName][damageShow] as GameObject;
    }

    //加载Spine资源
    public static T LoadSpineRes<T>(string _path) where T : Object
    {
        if (!spineRes.ContainsKey(_path))
        {
            spineRes.Add(_path, null);
            spineRes[_path] = LoadRes<T>(_path);
        }
        return spineRes[_path] as T;
    }



    /// <summary>
    /// 加载资源
    /// </summary>
    public static T LoadRes<T>(string _path) where T : Object
    {
        return Resources.Load<T>(_path);
    }


    /// <summary>
    /// 实例化资源
    /// </summary>
    public static GameObject InstantiateRes(GameObject obj, object parent = null, object localScale = null, object localPosition = null)
    {
        return obj == null ? null : ObjSetParent(GameObject.Instantiate(obj), parent, localScale, localPosition);
    }


    /// <summary>
    /// 添加到资源字典
    /// </summary>
    private static void AddResDictionary(string key, string path, object obj)
    {
        if (alreadyLoadRes.ContainsKey(key))
        {
            if (alreadyLoadRes[key].ContainsKey(path))
            {
                return;
            }

            alreadyLoadRes[key].Add(path, obj);
            return;
        }
        alreadyLoadRes.Add(key, new Dictionary<string, object> { { path, obj } });
    }

    /// <summary>
    /// 字典移除指定类型资源
    /// </summary>
    public static void RemoveResDictionary(string key)
    {
        if (alreadyLoadRes.ContainsKey(key))
        {
            alreadyLoadRes.Remove(key);
        }
    }

    /// <summary>
    /// Obj设置父层级
    /// </summary>
    public static GameObject ObjSetParent(GameObject obj, object parent = null, object localScale = null, object localPosition = null)
    {
        if (parent == null || obj == null)
        {
            return obj;
        }
        //
        obj.transform.SetParent(parent is GameObject ? (parent as GameObject).transform : parent as Transform);
        obj.transform.position = Vector3.zero;
        obj.transform.localScale = localScale == null ? Vector3.one : (Vector3)localScale;
        obj.transform.localPosition = localPosition == null ? Vector3.zero : (Vector3)localPosition;
        return obj;
    }



    /// <summary>
    /// 删除子GameObject
    /// </summary>
    public static void DeleteChildObj(Transform _praent)
    {
        GameObject[] _obj = new GameObject[_praent.childCount];
        int _index = 0;
        for (int i = 0; i < _praent.childCount; i++)
        {
            _obj[_index] = _praent.GetChild(i).gameObject;
            _index++;
        }
        //
        for (int i = 0; i < _obj.Length; i++)
        {
            if (_obj[i] == null)
            {
                continue;
            }

            DeleteObj(_obj[i]);
        }
    }

    /// <summary>
    /// 删除子GameObject
    /// </summary>
    public static void DeleteChildObj(Transform _praent, List<string> _exceptionList)
    {
        GameObject[] _obj = new GameObject[_praent.childCount];
        int _index = 0;
        for (int i = 0; i < _praent.childCount; i++)
        {
            _obj[_index] = _praent.GetChild(i).gameObject;
            if (_exceptionList.Contains(_obj[_index].name))
            {
                _obj[_index] = null;
            }
            _index++;
        }
        //
        for (int i = 0; i < _obj.Length; i++)
        {
            if (_obj[i] == null)
            {
                continue;
            }
            DeleteObj(_obj[i]);
        }
    }

    /// <summary>
    /// 删除GameObject
    /// </summary>
    public static void DeleteObj(object obj)
    {
        if (obj is GameObject)
        {
            Object.Destroy((GameObject)obj);
        }
        else
        {
            Object.Destroy(((Transform)obj).gameObject);
        }
    }
}
