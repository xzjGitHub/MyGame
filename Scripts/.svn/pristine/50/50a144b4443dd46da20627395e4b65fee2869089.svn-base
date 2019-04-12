using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextInfo: MonoBehaviour
{
    //0.7  1.4  0.7  1.4  0.6  1  1.5  1.2  0.8  1
    public List<float> LineWaitTime;

    private Vector3 m_startPos = new Vector3(-50,206);
    private int m_xinteral = 1;
    private int m_yinteral = 39;

    private Action m_endAction;

    [HideInInspector]
    public bool NeedWait;

    private List<List<GameObject>> m_list = new List<List<GameObject>>();

    private Color m_defaultColor = new Color(46 / 255f,42 / 255f,42 / 255f,0);


    public void ShowText(List<string> list,Action endAction)
    {
        m_endAction = endAction;
        StartCoroutine(InitInfo(list));
    }

    public IEnumerator InitInfo(List<string> list)
    {
        NeedWait = true;

        GameObject prefab = transform.Find("Text").gameObject;
        prefab.GetComponent<Text>().color = m_defaultColor;
        prefab.SetActive(false);

        GameObject last = null;
        for(int i = 0; i < list.Count; i++)
        {
            List<GameObject> objList = new List<GameObject>();

            for(int j = 0; j < list[i].Length; j++)
            {
                GameObject temp = GameObject.Instantiate<GameObject>(prefab);
                temp.GetComponent<Text>().color = m_defaultColor;
                temp.GetComponent<Text>().text = list[i][j].ToString();
                Utility.SetParent(temp,transform);

                if(j == 0)
                {
                    temp.transform.localPosition = m_startPos + new Vector3(0,-m_yinteral * i,0);
                }
                else
                {
                    float lastWidth = last.GetComponent<RectTransform>().sizeDelta.x;
                    float nowWidth = temp.GetComponent<RectTransform>().sizeDelta.x;
                    float nowPosX = lastWidth / 2 + nowWidth / 2 + m_xinteral;
                    //if(list[i][j].ToString() == " ")
                    //{
                    //    nowPosX = 11.5f;
                    //}
                    //if(lastString == " ")
                    //{
                    //   // nowPosX =0f;
                    //}

                    Vector3 lastPos = last.transform.localPosition;
                    temp.transform.localPosition =
                        new Vector3(nowPosX + lastPos.x,lastPos.y);
                }

                temp.GetComponent<Animator>().SetBool("play",true);

                last = temp;
                objList.Add(temp);
                if(NeedWait)
                    yield return new WaitForSeconds(0.1f);
            }
            m_list.Add(objList);
            if(NeedWait && i != list.Count - 1)
                yield return new WaitForSeconds(LineWaitTime[i]);
        }

        if(m_endAction != null)
        {
            m_endAction();
        }

       // yield return null;
        //UpdatePos();
    }

    private void UpdatePos()
    {
        GameObject ls = null;

        for(int i = 0; i < m_list.Count; i++)
        {
            for(int j = 0; j < m_list[i].Count; j++)
            {
                if(j == 0)
                {
                    m_list[i][j].transform.localPosition =
                        m_startPos + new Vector3(0,-m_yinteral * i,0);
                }
                else
                {
                    float lastWidth = ls.GetComponent<RectTransform>().sizeDelta.x;
                    float nowWidth = m_list[i][j].GetComponent<RectTransform>().sizeDelta.x;
                    float nowPosX = lastWidth / 2 + nowWidth / 2 + m_xinteral;

                    Vector3 lastPos = ls.transform.localPosition;
                    m_list[i][j].transform.localPosition =
                        new Vector3(nowPosX + lastPos.x,lastPos.y);
                }
                ls = m_list[i][j];
            }

        }
    }

    /// <summary>
    /// 测试代码 后面会删除
    /// </summary>
    public void Dis()
    {
        for(int i = 0; i < m_list.Count; i++)
        {
            for(int j = 0; j < m_list[i].Count; j++)
            {
                m_list[i][j].transform.SetParent(transform);
            }

        }

        for(int i = 0; i < m_list.Count; i++)
        {
            for(int j = 0; j < m_list[i].Count; j++)
            {
                m_list[i][j].transform.SetParent(transform);
                GameObject.DestroyImmediate(m_list[i][j]);
            }

        }

        m_list.Clear();
    }

}

