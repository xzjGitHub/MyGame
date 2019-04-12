using UnityEngine;
using UnityEngine.UI;

public class TipPanelPosUtil
{

    /// <summary>
    /// 根据屏幕坐标来判断位置
    /// </summary>
    /// <param name="canvas">渲染此物体的画布</param>
    /// <param name="current">当前物体</param>
    /// <param name="clickObj">点击的物体</param>
    /// <param name="xDis">当前物体与点击物体x方向间隔</param>
    /// <param name="yDis">当前物体与点击物体y方向间</param>
    /// <param name="leftDis">距离最左边的距离</param>
    /// <param name="rightDis">距离最右边的距离</param>
    /// <param name="topDis">距离最上边的距离</param>
    /// <param name="bottomDis">距离最下边的距离</param>
    public static void UpdatePanelPos(Canvas canvas,Transform current,GameObject clickObj,
      int xDis = 0,int yDis = 0,
      int leftDis = 0,int rightDis = 0,int topDis = 0,int bottomDis = 0)
    {
        RectTransform clickRect = clickObj.GetComponent<RectTransform>();
        RectTransform currentRect = current.GetComponent<RectTransform>();

        Camera camera = canvas.worldCamera;

        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(camera,clickObj.transform.position);
        Vector2 finaPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(currentRect,
            new Vector2(screenPos.x,screenPos.y),camera,out finaPos);

        float scaler = CheckBoundsUtil.GetScale(canvas.transform.GetComponent<CanvasScaler>());
        Vector3 pos = new Vector3(screenPos.x + xDis* scaler + currentRect.sizeDelta.x,
            screenPos.y + yDis * scaler + currentRect.sizeDelta.y,0);

        CheckBoundsUtil.CheckBoundsResult result = CheckBoundsUtil.GetCheckBoundsResult(
            canvas.transform.GetComponent<CanvasScaler>(),
            pos,leftDis,rightDis,topDis,bottomDis);

        //这里情况一直是右上方
        float finalX = finaPos.x + clickRect.sizeDelta.x / 2 + xDis * scaler + currentRect.sizeDelta.x / 2;
        if(result == CheckBoundsUtil.CheckBoundsResult.Right)
        {
            finalX = finaPos.x - clickRect.sizeDelta.x / 2 - xDis * scaler - currentRect.sizeDelta.x / 2;
        }

        float finalY = finaPos.y + clickRect.sizeDelta.y / 2 + yDis * scaler + currentRect.sizeDelta.y / 2;
        if(result == CheckBoundsUtil.CheckBoundsResult.Top)
        {
            finalY = finaPos.y - clickRect.sizeDelta.y / 2 -yDis * scaler - currentRect.sizeDelta.y / 2;
        }
        //其他情况 todo
        currentRect.transform.localPosition += new Vector3(finalX,finalY,0);

    }
}
