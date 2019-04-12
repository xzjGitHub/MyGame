using UnityEngine;
using UnityEngine.UI;

public class CheckBoundsUtil
{
    public enum CheckBoundsResult
    {
        None,
        Left,
        Right,
        Top,
        Bottom
    }

    public static float GetScale(CanvasScaler canvasScaler)
    {
        float scale = canvasScaler.matchWidthOrHeight == 1 ? canvasScaler.referenceResolution.y
       / Screen.height : canvasScaler.referenceResolution.x / Screen.width;
        return scale;
    }

    public static Rect GetRect(CanvasScaler canvasScaler)
    {
        float scale = GetScale(canvasScaler);
        Rect rect = new Rect(-Screen.width / 2,-Screen.height / 2,Screen.width,Screen.height);
        rect = new Rect(rect.x * scale,rect.y * scale,rect.width * scale,rect.height * scale);
        return rect;
    }

    public static CheckBoundsResult GetCheckBoundsResult(CanvasScaler canvasScaler,
        Transform parent,Transform child,float leftDis=0f,float rightDis=0f,
        float topDis=0f,float bottomDis=0f)
    {
        Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(parent,child);
        Rect rect = GetRect(canvasScaler);
        float scale = GetScale(canvasScaler);
        if(bounds.center.x + bounds.extents.x > rect.width / 2-rightDis*scale)//target超出rect的右边框
        {
            return CheckBoundsResult.Right;
        }
        if(bounds.center.x - bounds.extents.x < rect.x+leftDis*scale)      //target超出rect的左边框
        {
            return CheckBoundsResult.Left;
        }
        if(bounds.center.y - bounds.extents.y < rect.y-topDis*scale)     //target超出rect的上边框
        {
            return CheckBoundsResult.Top;
        }
        if(bounds.center.y + bounds.extents.y > rect.height / 2+bottomDis*scale)//target超出rect的下边框
        {
            return CheckBoundsResult.Bottom;
        }
        return CheckBoundsResult.None;
    }

    public static CheckBoundsResult GetCheckBoundsResult(CanvasScaler canvasScaler,Vector3 pos,
        float leftDis = 0f,float rightDis = 0f,
        float topDis = 0f,float bottomDis = 0f)
    {
        float scale = GetScale(canvasScaler);
        if (pos.x > Screen.width - rightDis*scale)
        {
            return CheckBoundsResult.Right;
        }
        if (pos.x <leftDis*scale)
        {
            return CheckBoundsResult.Left;
        }
        if (pos.y > Screen.height - topDis)
        {
            return CheckBoundsResult.Top;
        }
        if (pos.y < bottomDis)
        {
            return CheckBoundsResult.Bottom;
        }
        return CheckBoundsResult.None;
    }
}

