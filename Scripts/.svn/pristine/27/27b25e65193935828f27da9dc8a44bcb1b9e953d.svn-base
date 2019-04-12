
/*------------------------------------------------------------------------
 Author：     xuyan
 CreateTime： 2018/12/13 15:21:22
------------------------------------------------------------------------*/
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class SDKBase
{
    protected AndroidJavaObject m_activity;

    public SDKBase() {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        m_activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init() { }

    /// <summary>
    /// 登陆
    /// </summary>
    public virtual void Login() { }

    /// <summary>
    /// 登出
    /// </summary>
    public virtual void Logout() { }

    /// <summary>
    /// 支付
    /// </summary>
    public virtual void Pay(string info) { }



}

