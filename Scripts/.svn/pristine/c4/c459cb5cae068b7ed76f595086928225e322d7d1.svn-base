using UnityEngine;
using System.Collections;

public class MapMoveTest : MonoBehaviour
{
    public float aspd = 5;
    public RectTransform MoveRectTransform;
    //
    private RectTransform Map1Transform;
    private RectTransform Map2Transform;
    //
    private Vector3 endVector3=new Vector3(-1280,0,0);


    void Start()
    {

    }


    void Update()
    {
        //if (MoveRectTransform.anchoredPosition.x <= -1280)
        //{
        //    MoveRectTransform.anchoredPosition = Vector2.zero;
        //}
        //MoveTransform(MoveRectTransform, aspd);
    }

    private void FixedUpdate()
    {
        if (MoveRectTransform.anchoredPosition.x == -1280)
        {
            MoveRectTransform.anchoredPosition = Vector2.zero;
        }
        MoveTransform(MoveRectTransform, aspd);
    }






    void MoveTransform(RectTransform _transform, float _aspd)
    {
        if (_transform == null) return;
        _transform.anchoredPosition = Vector3.MoveTowards(_transform.anchoredPosition, endVector3, _aspd);
    }

}
