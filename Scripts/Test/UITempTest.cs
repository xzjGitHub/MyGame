using UnityEngine;
using System.Collections;

public class UITempTest : MonoBehaviour
{

    public Transform a;
    public Transform b;

    // Use this for initialization
    void Start()
    {
        Vector2 vector2 = GameTools.WorldToScreenPoint(a);

        vector2 += Vector2.right*50*1.5f;
        Vector3 temp = Camera.main.ScreenToWorldPoint(vector2);
        b.position = temp;
        temp = new Vector3(b.localPosition.x, b.localPosition.y, 0);
        b.localPosition = temp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
