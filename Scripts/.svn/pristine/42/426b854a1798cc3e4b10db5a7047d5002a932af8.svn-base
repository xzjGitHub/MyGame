using Spine;
using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

public class UIItemLaunchPosition : MonoBehaviour
{
    private SkeletonAnimation itemSkeleton;
    //
    private const string itemNameStr = "Ui_baofaliwu";
    private const string itemBoneName1 = "wuqi_2";
    private const string itemBoneName2 = "wuqi_3";
    private const string itemBoneName3 = "wuqi_4";
    private const string itemBoneName4 = "wuqi_5";
    private const string itemBoneName5 = "wuqi_6";
    private const string itemBoneName6 = "wuqi_7";
    private const string itemBoneName7 = "wuqi_8";
    private const string itemBoneName8 = "wuqi_9";
    private const string itemBoneName9 = "wuqi_10";
    //
    private List<string> itemBoneNames = new List<string>();
    private List<Vector3> posList = new List<Vector3>();
    //
    private bool isUpdateItemPos;
    private bool isFirst;
    private bool isPlayEnd;
    //

    public List<Vector3> PosList { get { return posList; } }

    public bool IsUpdateItemPos { get { return isUpdateItemPos; } }

    public bool IsPlayEnd
    {
        get { return isPlayEnd; }
    }

    public List<bool> IsOk
    {
        get
        {
            return isOk;
        }
    }

    public void PlayLaunchItem()
    {
        if (itemSkeleton == null)
        {
            itemSkeleton = transform.Find("ItemMove").GetComponent<SkeletonAnimation>();
        }
        //
        InitItemInfo();
        //
        isPlayEnd = false;
        itemSkeleton.state.Complete += OnPlayEnd;
        itemSkeleton.AnimationState.SetAnimation(0, itemNameStr, false);
        isUpdateItemPos = true;
    }

    public void Reset()
    {
        isPlayEnd = false;
        isUpdateItemPos = false;
    }



    private void EventList(TrackEntry trackEntry, Spine.Event e)
    {
        isOk[int.Parse(e.Data.Name)] = true;
    }


    private void OnPlayEnd(TrackEntry trackentry)
    {
        itemSkeleton.state.Complete -= OnPlayEnd;
        isUpdateItemPos = false;
        isPlayEnd = true;
    }

    private void UpdateBonePos()
    {
        if (!isUpdateItemPos || itemSkeleton == null) return;
        for (int i = 0; i < posList.Count; i++)
        {
            posList[i] = GetBoneWorldPos(itemSkeleton, itemBoneNames[i]);
        }
    }

    private Bone tempBone;

    /// <summary>
    /// 获取骨头在UNITY世界坐标的位置
    /// </summary>
    private Vector3 GetBoneWorldPos(SkeletonAnimation skeletonAnimation, string boneName)
    {
        tempBone = skeletonAnimation.skeleton.FindBone(boneName);
        if (tempBone == null) return Vector3.zero;
        return skeletonAnimation.transform.TransformPoint(new Vector3(tempBone.WorldX, tempBone.WorldY, 1));
    }

    private void InitItemInfo()
    {
        if (isFirst) return;
        itemSkeleton.AnimationState.Event += EventList;
        for (int i = 0; i < 9; i++)
        {
            isOk.Add(false);
        }

        itemBoneNames = new List<string>
        {
            itemBoneName1,
            itemBoneName2,
            itemBoneName3,
            itemBoneName4,
            itemBoneName5,
            itemBoneName6,
            itemBoneName7,
            itemBoneName8,
            itemBoneName9,
        };
        Vector3 _temp=Vector3.zero;
        posList = new List<Vector3>
        {
            _temp,
            _temp,
            _temp,
            _temp,
            _temp,
            _temp,
            _temp,
            _temp,
            _temp
        };
        isFirst = true;
    }


    private List<bool> isOk = new List<bool>();

    void Update()
    {
        UpdateBonePos();
    }
}
