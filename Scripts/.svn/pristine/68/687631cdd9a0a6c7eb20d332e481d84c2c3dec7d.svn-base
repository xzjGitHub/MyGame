using UnityEngine;


/// <summary>
/// 播放主HitEvent
/// </summary>
public class UIPlayMainHitEvent : MonoBehaviour
{
    public int StateIndex { get { return stateIndex; } }
    public int HitCharIndex { get { return hitCharIndex; } }

    public bool IsPlayOk { get { return isPlayOk; } }

    public delegate void CallBack(UIPlayMainHitEvent param);
    public CallBack OnPlayHitEventOk;


    public void Init(int hitCharIndex, int stateIndex, Transform parent, string effectRP = "", string effectName = "", int sortingOrder = 0)
    {
        this.hitCharIndex = hitCharIndex;
        this.stateIndex = stateIndex;
        this.parent = parent;
        this.effectRP = effectRP;
        this.effectName = effectName;
        this.sortingOrder = sortingOrder;

    }



    /// <summary>
    /// hit播放结束
    /// </summary>
    /// <param name="playeffect"></param>
    private void OnCallHitPlayEnd(Object playeffect)
    {
        isPlayOk = true;
        if (OnPlayHitEventOk != null)
        {
            OnPlayHitEventOk(this);
        }
        //
        DestroyImmediate(playeffect);
    }

    //
    private  int stateIndex;
    private  int hitCharIndex;
    private  Transform parent;
    private  string effectRP;
    private  string effectName;
    private  int sortingOrder;
    private bool isPlayOk;
}

