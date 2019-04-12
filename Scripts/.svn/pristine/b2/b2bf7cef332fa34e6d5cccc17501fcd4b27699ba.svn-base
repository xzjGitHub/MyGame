using UnityEngine;
using System.Collections;

public class TestContent : MonoBehaviour
{

    public GameObject momo;
    public GameObject ruoruo;
    void Start()
    {
        //同步的把两个gameobject挂在layout下面
        GameObject go1 = GameObject.Instantiate<GameObject>(momo);
        go1.transform.SetParent(transform, false);

        GameObject go2 = GameObject.Instantiate<GameObject>(ruoruo);
        go2.transform.SetParent(transform, false);

        ContentImmediate content = GetComponent<ContentImmediate>() ?? gameObject.AddComponent<ContentImmediate>();
        //同步获取ContentSizeFitter的size
        Debug.Log("ContentSizeFitter size  = " + " " + content.GetPreferredSize());
    }
}
