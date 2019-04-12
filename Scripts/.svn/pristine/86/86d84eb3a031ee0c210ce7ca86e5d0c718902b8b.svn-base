using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


[AddComponentMenu("UI/Effects/TextSpacing")]
public class TextSpacing: BaseMeshEffect
{
    //Font
    public string Font = "";
    //数字间隔
    public float _textSpacing = 1f;
    //字符默认 size
    //[SerializeField]
    public float DefaultSize = 32f;
    //字符当前 size
    public float CurrentSize = 32f;

    public override void ModifyMesh(VertexHelper vh)
    {
        if(!IsActive() || vh.currentVertCount == 0)
        {
            return;
        }
        List<UIVertex> vertexs = new List<UIVertex>();
        vh.GetUIVertexStream(vertexs);
        int indexCount = vh.currentIndexCount;
      //  Debug.Log("Bug测试" + indexCount + " --- " + gameObject.GetComponent<Text>().text);
        UIVertex vt;
        for(int i = 6; i < indexCount; i++)
        {
            //第一个字不用改变位置
            vt = vertexs[i];
            vt.position += new Vector3(_textSpacing * (i / 6),0,0);
            vertexs[i] = vt;
            //以下注意点与索引的对应关系
            if(i % 6 <= 2)
            {
                vh.SetUIVertex(vt,(i / 6) * 4 + i % 6);
            }
            if(i % 6 == 4)
            {
                vh.SetUIVertex(vt,(i / 6) * 4 + i % 6 - 1);
            }
        }
        if(DefaultSize == 0)
        {
            gameObject.transform.localScale = Vector3.one;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(CurrentSize / DefaultSize,CurrentSize / DefaultSize,
                CurrentSize / DefaultSize);
        }
        Font = gameObject.GetComponent<Text>().font.name;
    }
}