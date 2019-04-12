using UnityEngine;
using System.Collections;

public class TestLineRender : MonoBehaviour
{

    private GameObject clone;
    private LineRenderer lineRenderer;
    int i;
    //带有LineRender物体  
    public GameObject target;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //实例化对象  
            clone = (GameObject)Instantiate(target, target.transform.position, Quaternion.identity);
            //获得该物体上的LineRender组件  
            lineRenderer = clone.GetComponent<LineRenderer>();
            //设置起始和结束的颜色  
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.blue;
            //设置起始和结束的宽度 
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            //计数  
            i = 0;
        }
        if (Input.GetMouseButton(0))
        {
            i++;
            //设置顶点数  
            lineRenderer.positionCount=i;
            //设置位置
            lineRenderer.SetPosition(i - 1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15)));
        }
        if (Input.GetMouseButtonUp(0))
        {
            clone.AddComponent<Rigidbody2D>();
            clone.AddComponent<PolygonCollider2D>();
        }
    }
}