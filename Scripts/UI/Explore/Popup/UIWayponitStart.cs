using GameEventDispose;
using UnityEngine;
using UnityEngine.UI;

public class UIWayponitStart : MonoBehaviour
{
    private Text mapText;
    private Text waypointText;
    private GameObject lineGameObject;
    //
    private bool isFirst;

    void Init()
    {
        if (isFirst) return;
        mapText = transform.Find("MapName").GetComponent<Text>();
        waypointText = transform.Find("WayponitName").GetComponent<Text>();
        lineGameObject = transform.Find("Line").gameObject;
        isFirst = true;
    }


    public void PlayShow()
    {
        Init();
        //
        mapText.text = ExploreSystem.Instance.MapTemplate.mapName;
        waypointText.text = ExploreSystem.Instance.NowWPAttribute.wp_template.WPName;
        gameObject.SetActive(true);
    }

    private void OnComlete()
    {
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.WPStart, (object)null);
        gameObject.SetActive(false);
    }

}
