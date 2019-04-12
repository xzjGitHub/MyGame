using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏工具
/// </summary>
public class GameTools
{

    /// <summary>
    /// 获得的物体的Renderer
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Renderer GetObjRenderer(GameObject obj)
    {
        if (obj == null)
        {
            return null;
        }
        return obj.GetComponent<Renderer>();
    }

    /// <summary>
    /// 是否是装备
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public static bool IsEquip(int itemType)
    {
        return IsEquip((ItemType)itemType);
    }

    /// <summary>
    /// 是否是装备
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public static bool IsEquip(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.WuQi:
            case ItemType.KuiJia:
            case ItemType.ShiPing:
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 获得Canvas
    /// </summary>
    /// <param name="obj">目标</param>
    /// <returns></returns>
    public static Canvas GetCanvas(Transform obj)
    {
        Canvas canvas = null;
        while (canvas == null && obj != null)
        {
            canvas = obj.GetComponent<Canvas>();
            obj = obj.parent;
        }

        return canvas;
    }

    /// <summary>
    /// 获取摄像机
    /// </summary>
    /// <param name="obj">目标</param>
    /// <returns></returns>
    public static Camera GetCamera(Transform obj)
    {
        return GetCanvas(obj).worldCamera;
    }

    /// <summary>
    /// 世界坐标转本地坐标
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="carrierTransform"></param>
    /// <returns></returns>
    public static Vector3 WorldToLocalPoint(Vector3 pos,Transform carrierTransform)
    {
        return carrierTransform.InverseTransformPoint(pos);
    }

    /// <summary>
    /// 屏幕转世界坐标
    /// </summary>
    /// <param name="posVector3">目标位置</param>
    /// <param name="obj">目标</param>
    /// <returns></returns>
    public static Vector3 ScreenToWorldPoint(Vector3 posVector3, Transform obj = null)
    {
        return (obj == null ? Camera.main : GetCanvas(obj).worldCamera).ScreenToWorldPoint(posVector3);
    }
    /// <summary>
    /// 屏幕转世界坐标
    /// </summary>
    /// <param name="posVector3">目标位置</param>
    /// <param name="camera">摄像机</param>
    /// <returns></returns>
    public static Vector3 ScreenToWorldPoint(Vector3 posVector3, Camera camera = null)
    {
        return (camera == null ? Camera.main : camera).ScreenToWorldPoint(posVector3);
    }

    /// <summary>
    /// 世界坐标转屏幕坐标
    /// </summary>
    /// <param name="posVector3">目标位置</param>
    /// <param name="obj">目标</param>
    /// <returns></returns>
    public static Vector3 WorldToScreenPoint(Vector3 posVector3, Transform obj = null)
    {
        return (obj == null ? Camera.main : GetCanvas(obj).worldCamera).WorldToScreenPoint(posVector3);
    }
    /// <summary>
    /// 世界坐标转屏幕坐标
    /// </summary>
    /// <param name="posVector3">目标位置</param>
    /// <param name="camera">摄像机</param>
    /// <returns></returns>
    public static Vector3 WorldToScreenPoint(Vector3 posVector3, Camera camera = null)
    {
        return (camera == null ? Camera.main : camera).WorldToScreenPoint(posVector3);
    }
    /// <summary>
    /// 世界坐标转屏幕坐标
    /// </summary>
    /// <param name="obj">目标</param>
    /// <returns></returns>
    public static Vector3 WorldToScreenPoint(Transform obj)
    {
        return (obj == null ? Camera.main : GetCanvas(obj).worldCamera).WorldToScreenPoint(obj.position);
    }

    /// <summary>
    /// 修正点
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="localScale"></param>
    /// <param name="isZzero"></param>
    /// <returns></returns>
    public static Vector3 AmendVector(Vector3 vector3, float x, float y, float localScale = 1, bool isZzero = false)
    {
        if (isZzero)
        {
            vector3=new Vector3(vector3.x,vector3.y,0);
        }
        return vector3 + Vector3.right * x / localScale + Vector3.up * y / localScale;
    }
    /// <summary>
    /// 修正点
    /// </summary>
    public static Vector3 AmendVector(Vector3 vector3, List<float> CSYS, float localScale = 1,bool isZzero=false)
    {
        if (CSYS.Count > 0)
        {
            vector3 += Vector3.right * CSYS[0] / localScale;
        }

        if (CSYS.Count > 1)
        {
            vector3 += Vector3.up * CSYS[1] / localScale;
        }

        if (isZzero)
        {
            vector3 = new Vector3(vector3.x, vector3.y, 0);
        }
        return vector3;
    }
}
