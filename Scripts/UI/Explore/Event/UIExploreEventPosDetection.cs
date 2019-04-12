using UnityEngine;

/// <summary>
/// 探索事件位置检测
/// </summary>
public class UIExploreEventPosDetection : MonoBehaviour
{
    public delegate void CallBack();

    public CallBack OnCanVisit;
    public CallBack OnAutoVisit;
    public CallBack OnAutoAbandonVisit1;
    public CallBack OnAutoAbandonVisit2;
    public CallBack OnEnterScreen;
    public CallBack OnShow1;


    private void Start()
    {
        float temp = Screen.width / ScreenDefaultWidth;
        float tempOffset = Screen.width - ScreenDefaultWidth / 2 * temp;
        //
        objWidth = transform.GetComponent<RectTransform>().rect.width;
        show1Width = offset1 * temp + tempOffset;
        canVisitWidth = offset1 * temp + tempOffset;
        autoVisitWidth = tempOffset - 100 * temp;
        autoAbandonVisit1 = (offset2 - objWidth) * temp;
        autoAbandonVisit2 = 0;
        //
        canVisitWidth = GetPos(canVisitWidth).x;
        autoVisitWidth = GetPos(autoVisitWidth).x;
        autoAbandonVisit1 = GetPos(autoAbandonVisit1).x;
        autoAbandonVisit2 = GetPos(autoAbandonVisit2).x;
        ScreenWidth = GetPos(ScreenWidth - objWidth * 0.5f * temp).x;
    }


    private Vector3 GetPos(float x)
    {
        if (_camera == null)
        {
            _camera = GameTools.GetCanvas(transform).worldCamera;
        }
        return  GameTools.ScreenToWorldPoint(new Vector3(x, 0, 0), _camera);
    }

    /// <summary>
    /// 获得Canvas
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private Canvas GetCanvas(Transform obj)
    {
        Canvas canvas = null;
        while (canvas == null && obj != null)
        {
            canvas = obj.GetComponent<Canvas>();
            obj = obj.parent;
        }

        return canvas;
    }

    private void CheckEnterScreen()
    {
        if (isEnterScreen)
        {
            return;
        }

        if (transform.position.x >= ScreenWidth)
        {
            return;
        }
        //    if (OnEnterScreen != null) OnEnterScreen();
        isEnterScreen = true;
    }

    private void CheckShow1()
    {
        if (isShow1)
        {
            return;
        }

        if (transform.position.x >= show1Width)
        {
            return;
        }

        isShow1 = true;
        //    if (OnShow1 != null) OnShow1();
    }
    private void CheckCanVisit()
    {
        if (isCan)
        {
            return;
        }

        if (transform.position.x >= canVisitWidth)
        {
            return;
        }
        //  if (OnCanVisit != null) OnCanVisit();
        isCan = true;
    }
    private void CheckAutoVisit()
    {
        if (isAuto)
        {
            return;
        }

        if (transform.position.x >= autoVisitWidth)
        {
            return;
        }
        //     if (OnAutoVisit != null) OnAutoVisit();
        isAuto = true;
    }
    /// <summary>
    /// 自动放弃访问
    /// </summary>
    private void AutoAbandonVisit1()
    {
        if (isAuto1)
        {
            return;
        }

        if (transform.position.x >= autoAbandonVisit1)
        {
            return;
        }

        isAuto1 = true;
        //     if (OnAutoAbandonVisit1 != null) OnAutoAbandonVisit1();
    }
    /// <summary>
    /// 自动放弃访问
    /// </summary>
    private void AutoAbandonVisit2()
    {
        if (isAuto2)
        {
            return;
        }

        if (transform.position.x >= autoAbandonVisit2)
        {
            return;
        }

        isAuto2 = true;
        if (OnAutoAbandonVisit2 != null)
        {
            OnAutoAbandonVisit2();
        }
    }

    private void CheckScenePos()
    {

    }

    private void Update()
    {
        CheckEnterScreen();
        //   CheckShow1();
        //   CheckCanVisit();
        //   CheckAutoVisit();
        //   AutoAbandonVisit1();
        AutoAbandonVisit2();
    }

    //
    private Camera _camera;
    //
    private float show1Width;
    private float canVisitWidth;
    private float autoVisitWidth;
    private float autoAbandonVisit1;
    private float autoAbandonVisit2;
    private readonly float nowX;
    private float objWidth;
    //
    private bool isEnterScreen;
    private bool isShow1;
    private bool isAuto1;
    private bool isAuto2;
    private bool isCan;
    private bool isAuto;
    private float ScreenWidth = Screen.width;
    //
    private const float ScreenDefaultWidth = 1280f;
    public float offset2 = 400f;
    public float offset1 = 50f;
    private const float offset = 100f;

    public bool IsEnterScreen { get { return isEnterScreen; } }
}
