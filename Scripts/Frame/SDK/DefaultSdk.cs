
/*------------------------------------------------------------------------
 Author：     xuyan
 CreateTime： 2018/12/13 16:08:47
------------------------------------------------------------------------*/
/// <summary>
/// 
/// </summary>
public class DefaultSdk : SDKBase
{
 
    public override void Init()
    {
        base.Init();
        m_activity.Call("Init");
    }


    public override void Login()
    {
        base.Login();
        m_activity.Call("Login");
    }

    public override void Logout()
    {
        base.Logout();
        m_activity.Call("Logout");
    }

    public override void Pay(string info)
    {
        base.Pay(info);
        m_activity.Call("Pay");
    }


}

