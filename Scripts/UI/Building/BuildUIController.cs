using System;
using System.Collections.Generic;
using Barrack.View;
using College;
using Comomon.ItemList;
using Core.View;
using EventCenter;
using Shop.View;
using UnityEngine;
using WorkShop;
using System.Collections;

public enum BuildingTypeIndex
{
    None = 0,
    /// <summary>
    /// //大厅
    /// </summary>
    TownHall = 1,
    /// <summary>
    /// 核心
    /// </summary>
    Core = 2,
    /// <summary>
    /// 兵营
    /// </summary>
    Barracks = 3,
    /// <summary>
    /// 学院
    /// </summary>
    College = 4,
    /// <summary>
    /// 工坊
    /// </summary>
    WorkShop = 5,
    /// <summary>
    /// 市场
    /// </summary>
    Shop = 6
}

/// <summary>
/// 建筑类型
/// </summary>
public enum BuildingType
{
    None,
    /// <summary>
    /// //大厅
    /// </summary>
    TownHall = 101,
    /// <summary>
    /// 核心
    /// </summary>
    Core = 102,
    /// <summary>
    /// 兵营
    /// </summary>
    Barrack = 103,
    /// <summary>
    /// 学院=104
    /// </summary>
    College = 104,
    /// <summary>
    /// 工坊 105
    /// </summary>
    WorkShop = 105,
    /// <summary>
    /// 商店 105
    /// </summary>
    Shop = 106,
}

public class BuildUIController: Singleton<BuildUIController>
{
    public BuildingTypeIndex CurreentBuild;

    public List<BuildingTypeIndex> HaveShowList = new List<BuildingTypeIndex>();

    public const string ShowAnimName = "Show";
    public const string CloseAnimName = "Close";

    public bool CanClick;

    private BuildUIController()
    {
        //m_buildDict = new Dictionary<BuildingTypeIndex,IBuild>();
    }


    public void Reset()
    {
        CurreentBuild = BuildingTypeIndex.None;
    }

    public void AddBuild(BuildingTypeIndex buildingTypeIndex,IBuild build)
    {
        
    }

    public void ChangeBuild(BuildingTypeIndex targetBuild)
    {
        CanClick = false;

        ChangeBuild(!HaveShowList.Contains(targetBuild));
        ShowPanel(targetBuild);

        CanClick = true;
        CurreentBuild = targetBuild;
    }

    private IEnumerator Test(BuildingTypeIndex targetBuild)
    {
        yield return null;
        CanClick = true;
        CurreentBuild = targetBuild;
    }

    private void ChangeBuild(bool playHideAnim)
    {
        IBuild build = GetBuild(CurreentBuild);
        //if(build == null)
        //{
        //    Debug.LogError("build is null: " + CurreentBuild);
        //}
        if(build != null)
        {
            build.OnChangeBuild(playHideAnim);
        }
    }

    public IBuild GetBuild(BuildingTypeIndex targetBuild)
    {
        switch(targetBuild)
        {
            case BuildingTypeIndex.TownHall:
                return UIPanelManager.Instance.GetUiPanelBehaviour<HallPanel>();
            case BuildingTypeIndex.Core:
                return UIPanelManager.Instance.GetUiPanelBehaviour<CorePanel>();
            case BuildingTypeIndex.Barracks:
                return UIPanelManager.Instance.GetUiPanelBehaviour<BarrackPanel>();
            // return m_dict[]
            case BuildingTypeIndex.College:
                return UIPanelManager.Instance.GetUiPanelBehaviour<CollegePanel>();
            case BuildingTypeIndex.WorkShop:
                return UIPanelManager.Instance.GetUiPanelBehaviour<NewWorkShopPanel>();
            case BuildingTypeIndex.Shop:
                return UIPanelManager.Instance.GetUiPanelBehaviour<ShopPanel>();
        }
        return null;
    }

    private void ShowPanel(BuildingTypeIndex targetBuild)
    {
        switch(targetBuild)
        {
            case BuildingTypeIndex.TownHall:
                UIPanelManager.Instance.Show<HallPanel>();
                break;
            case BuildingTypeIndex.Core:
                UIPanelManager.Instance.Show<CorePanel>();
                break;
            case BuildingTypeIndex.Barracks:
                //GameObject obj = Resources.Load<GameObject>("UI/Panel/Building/Barrack/BarrackPanel");
                //GameObject temp = GameObject.Instantiate<GameObject>(obj);
                //Utility.SetParent(temp,UIPanelManager.Instance.ThreeUIParent);
                //BarrackPanel ba = temp.AddComponent<BarrackPanel>();
                //ba.name = name;
                //Debug.LogError("开始加载兵营： " + name);
                //if(m_dict.ContainsKey(targetBuild))
                //    m_dict[targetBuild]=ba;
                //else
                //    m_dict.Add(targetBuild,ba);
                UIPanelManager.Instance.Show<BarrackPanel>();
                //    ba.name = name;
                break;
            case BuildingTypeIndex.College:
                UIPanelManager.Instance.Show<CollegePanel>();
                break;
            case BuildingTypeIndex.WorkShop:
                UIPanelManager.Instance.Show<NewWorkShopPanel>();

                //GameObject obj1 = Resources.Load<GameObject>("UI/Panel/Building/NewWorkShop/NewWorkShopPanel");
                //GameObject temp1 = GameObject.Instantiate<GameObject>(obj1);
                //Utility.SetParent(temp1,UIPanelManager.Instance.ThreeUIParent);
                //NewWorkShopPanel ba1 = temp1.AddComponent<NewWorkShopPanel>();
                //if(m_dict.ContainsKey(targetBuild))
                //    m_dict[targetBuild] = ba1;
                //else
                //    m_dict.Add(targetBuild,ba1);
                //  ba1.name = name;
                break;
            case BuildingTypeIndex.Shop:
                UIPanelManager.Instance.Show<ShopPanel>();
                break;
        }
        // Debug.LogError("targetBuild="+ targetBuild);
    }
    //false,true

    public void HidePanel()
    {
        switch(CurreentBuild)
        {
            case BuildingTypeIndex.TownHall:
                UIPanelManager.Instance.Hide<HallPanel>();
                break;
            case BuildingTypeIndex.Core:
                UIPanelManager.Instance.Hide<CorePanel>();
                break;
            case BuildingTypeIndex.Barracks:
                UIPanelManager.Instance.Hide<BarrackPanel>();
                break;
            case BuildingTypeIndex.College:
                UIPanelManager.Instance.Hide<CollegePanel>();
                break;
            case BuildingTypeIndex.WorkShop:
                UIPanelManager.Instance.Hide<NewWorkShopPanel>();
                break;
            case BuildingTypeIndex.Shop:
                UIPanelManager.Instance.Hide<ShopPanel>();
                break;
        }
    }

    public void BackToStart(Action action)
    {
        ConfirmPanelUtil.ShowConfirmPanel("是否确定返回主页面","标题",
            () =>
            {
                HaveShowList.Clear();
                GameStatusManager.Instance.ChangeStatus(GameStatus.ExitScript);
                action();
            });

    }

    public void ClickBack()
    {
        IBuild build = GetBuild(CurreentBuild);
        build.ClickBack();
    }

    public GameObject LoadItemList(Transform parent,out NewItemList itemList)
    {
        GameObject obj = GameObjectPool.Instance.GetObjectByPrefabPath(StringDefine.ObjectPooItemKey.NewItemList,
            StringDefine.ObjectPooItemKey.NewItemList);
        itemList = Utility.RequireComponent<NewItemList>(obj);
        Utility.SetParent(obj,parent);
        return obj;
    }

    public string GetBuildName(BuildingTypeIndex targetBuild)
    {
        switch(targetBuild)
        {
            case BuildingTypeIndex.TownHall:
                return "大厅";
            case BuildingTypeIndex.Core:
                return "核心";
            case BuildingTypeIndex.Barracks:
                return "兵营";
            case BuildingTypeIndex.College:
                return "学院";
            case BuildingTypeIndex.WorkShop:
                return "工坊";
            case BuildingTypeIndex.Shop:
                return "商店";
        }
        return null;
    }
}

