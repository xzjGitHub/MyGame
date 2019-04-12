using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextShowTest : MonoBehaviour
{
    public bool isGoing;
    public Text text1;
    public float speed = 0.2f;

    void Update()
    {
        //循环播放文字  
        if (isGoing)
        {
            isGoing = false;
            StartOne();
        }
    }

    private void StartOne()
    {
        cc.Clear();
        text1.text = "";
        textTemp = "<b>A</b><size=20>B</size><color=red>C</color>\n";
        textTemp += "我们的世界叫苏米娜。\n我们的世界的中心沉睡着神。\n我们偷走了紫色的神之血。\n我们制造了蓝色的神之石。\n我们用来自神的力量征服了世界，创造了文明，实现了奇迹。\n然而我们都忘记了… ...我们的神并不仁慈。\n今天是3月23日，雨天。\n";
        string _temp;
        int _length;
        while (textTemp.Length > 0)
        {
            if (!textTemp.Contains(b1) && !textTemp.Contains(s1) && !textTemp.Contains(c1))
            {
                _temp = textTemp.Substring(0, 1);
                if (_temp == @"\") _temp = textTemp.Substring(0, 2);
                cc.Add(_temp);
                textTemp = textTemp.Remove(0, _temp.Length);
                //textTemp.Replace(_temp, "");
                continue;
            }
            //包含的有富文本
            int _index = textTemp.IndexOf("<", 0);
            if (_index < 0)
            {
                cc.Add(textTemp);
                textTemp = string.Empty;
                break;
            }
            switch (textTemp.Substring(_index, 2))
            {
                case b1:
                    _length = textTemp.IndexOf(b2, 0);
                    _temp = textTemp.Substring(0, _length + b2.Length);
                    cc.Add(_temp);
                    textTemp = textTemp.Remove(0, _temp.Length);
                    break;
                case s1:
                    _length = textTemp.IndexOf(s2, 0);
                    _temp = textTemp.Substring(0, _length + s2.Length);
                    cc.Add(_temp);
                    textTemp = textTemp.Remove(0, _temp.Length);
                    break;
                case c1:
                    _length = textTemp.IndexOf(c2, 0);
                    _temp = textTemp.Substring(0, _length + c2.Length);
                    cc.Add(_temp);
                    textTemp = textTemp.Remove(0, _temp.Length);
                    break;
            }
        }


        StartCoroutine(TextOne());
    }

    IEnumerator TextOne()
    {
        for (int i = 0; i < cc.Count; i++)
        {
            yield return new WaitForSeconds(speed);
            text1.text += cc[i];
        }
    }


    private List<string> cc = new List<string>();
    private string textTemp;


    private const string b1 = "<b";
    private const string b2 = "</b>";
    private const string s1 = "<s";
    private const string s2 = "</size>";
    private const string c1 = "<c";
    private const string c2 = "</color>";
}
