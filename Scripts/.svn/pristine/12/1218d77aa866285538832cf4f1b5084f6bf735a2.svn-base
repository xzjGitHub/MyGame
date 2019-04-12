using UnityEngine;

/*------------------------------------------------------------------------
 Author：     xuyan
 CreateTime： 2018/12/13 15:29:47
------------------------------------------------------------------------*/
/// <summary>
/// 
/// </summary>
public class SDKManager : MonoBehaviour
{
    private static SDKManager m_instance = null;
    public static SDKManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new SDKManager();
            }
            return m_instance;
        }
    }

    private SDKBase m_sdk;

    #region unityCallSdk
    public void Init(SDKType sdkType)
    {
        switch (sdkType)
        {
            case SDKType.Default:
                m_sdk = new DefaultSdk();
                break;
        }
        m_sdk.Init();
        DontDestroyOnLoad(this);
    }

    public void Login()
    {
        m_sdk.Login();
    }

    public void Logout()
    {
        m_sdk.Logout();
    }

    public void Pay(string info)
    {
        m_sdk.Pay(info);
    }
    #endregion


    #region SdkCallUnity

    public void InitCallBack(string info)
    {

    }

    public void LoginCallBack(string info)
    {

    }

    public void LogoutCallBack(string info)
    {

    }

    public void PayCallBack(string info)
    {

    }

    #endregion
}

