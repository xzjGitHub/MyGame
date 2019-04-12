using UnityEngine;
using EventCenter;

public class RapidBlurEffectEx:MonoBehaviour
{
    private RapidBlurEffect m_eff;

    private void Awake()
    {
        m_eff = Utility.RequireComponent<RapidBlurEffect>(gameObject);
        m_eff.enabled = false;

        EventManager.Instance.RegEventListener<bool>(EventSystemType.UI,
            EventTypeNameDefine.UpdateSepcialCamera,UpdateEffEnableStatus);
    }

    private void OnDestroy()
    {
        EventManager.Instance.UnRegEventListener<bool>(EventSystemType.UI,
          EventTypeNameDefine.UpdateSepcialCamera,UpdateEffEnableStatus);
    }

    private void UpdateEffEnableStatus(bool enable)
    {
        m_eff.enabled = enable;
    }
}
