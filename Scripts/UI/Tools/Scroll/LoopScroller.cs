﻿using System;
using UnityEngine;
using System.Collections.Generic;


public class LoopScroller: MonoBehaviour
{
    public enum Arrangement { Horizontal, Vertical, }
    public Arrangement Movement = Arrangement.Horizontal;

    [Range(1,100)]
    [Tooltip("单行或单列的Item数量")]
    [SerializeField]
    public int MaxPerLine = 5;

    [Range(0,100)]
    [Tooltip("x方向间距")]
    [SerializeField]
    public float CellPadidingX;

    [Range(0,100)]
    [Tooltip("y方向间距")]
    [SerializeField]
    public float CellPadidingY;

    [Range(0,1000)]
    [Tooltip("宽度")]
    [SerializeField]
    public int CellWidth = 500;

    [Range(0,1000)]
    [Tooltip("高度")]
    [SerializeField]
    public int CellHeight = 100;

    [Range(0,50)]
    [Tooltip("默认加载的个数")]
    [SerializeField]
    public int ViewCount = 6;

    [SerializeField]
    public float LeftPadding;

    [SerializeField]
    public float TopPadding;

    [SerializeField]
    public RectTransform Content;

    private int m_allCount;
    private int m_index = -1;


    //默认的缓存
    private List<LoopScrollItem> m_defaultOutScrollList = new List<LoopScrollItem>();
    private List<LoopScrollItem> m_defaulInScrollList = new List<LoopScrollItem>();

    //列表改变时回掉 [索引,对应的物体]
    private Action<int,GameObject> m_onScrollChangeAction;
    //创建物体时回掉 [要创建的物体索引，返回的物体]
    private Func<int,GameObject> m_onCreateItemAction;
    //销毁物体回掉  [索引，对应的物体]
    private Action<int,GameObject> m_onDestroyItemAction;
    //离开scroll回掉 [索引，对应的物体]
    //这个可以不传就用默认的
    private Action<int,GameObject> m_onOutScrollAction;
    //进入scroll回掉 [索引，返回的物体]
    //这个可以不传就用默认的
    private Func<int,GameObject> m_onInScrollAction;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="onScrollChangeAction">scroll改变回掉 必须传</param>
    /// <param name="createItem">创建回掉 必须传</param>
    /// <param name="destroyAction">销毁回掉 选择传递</param>
    /// <param name="onOutScrollAction">离开scroll回掉 可以不传 没有特殊需求不用传</param>
    /// <param name="onInScrollAction">进入scroll回掉 可以不传 没有特殊需求不用传</param>
    public void InitInfo(Action<int,GameObject> onScrollChangeAction,
        Func<int,GameObject> createItem,Action<int,GameObject> destroyAction,
        Action<int,GameObject> onOutScrollAction = null,
        Func<int,GameObject> onInScrollAction = null)
    {
        if(onScrollChangeAction == null)
        {
            Debug.LogError("itemChangeAction can not is null");
            return;
        }
        if(createItem == null)
        {
            Debug.LogError("createItem can not is null");
            return;
        }

        m_onScrollChangeAction = onScrollChangeAction;
        m_onCreateItemAction = createItem;
        m_onDestroyItemAction = destroyAction;
        m_onOutScrollAction = onOutScrollAction;
        m_onInScrollAction = onInScrollAction;
    }

    public void UpdateInfo(int num)
    {
        Reset();
        AllCount = num;
        OnValueChange(Vector2.zero);
    }


    public void OnValueChange(Vector2 pos)
    {
        int index = GetPosIndex();
        if(m_index != index && index > -1)
        {
            m_index = index;
            for(int i = m_defaulInScrollList.Count; i > 0; i--)
            {
                LoopScrollItem item = m_defaulInScrollList[i - 1];
                if(item.Index < index * MaxPerLine || (item.Index >= (index + ViewCount) * MaxPerLine))
                {
                    m_defaulInScrollList.Remove(item);
                    item.gameObject.SetActive(false);
                    if(m_onOutScrollAction != null)
                    {
                        m_onOutScrollAction(item.Index,item.gameObject);
                    }
                    else
                    {
                        m_defaultOutScrollList.Add(item);
                    }
                }
            }
            for(int i = m_index * MaxPerLine; i < (m_index + ViewCount) * MaxPerLine; i++)
            {
                if(i < 0) continue;
                if(i > m_allCount - 1) continue;
                bool isOk = false;
                foreach(LoopScrollItem item in m_defaulInScrollList)
                {
                    if(item.Index == i) isOk = true;
                }
                if(isOk) continue;
                CreateItem(i);
            }
        }
    }

    /// <summary>
    /// 提供给外部的方法，添加指定位置的Item
    /// </summary>
    public void AddItem(int index)
    {
        if(index > m_allCount)
        {
            Debug.LogError("添加错误:" + index);
            return;
        }
        AddItemIntoPanel(index);
        AllCount += 1;
    }

    /// <summary>
    /// 提供给外部的方法，删除指定位置的Item
    /// </summary>
    public void DelItem(int index)
    {
        if(index < 0 || index > m_allCount - 1)
        {
            Debug.LogError("删除错误:" + index);
            return;
        }
        DelItemFromPanel(index);
        AllCount -= 1;
    }

    private void AddItemIntoPanel(int index)
    {
        for(int i = 0; i < m_defaulInScrollList.Count; i++)
        {
            LoopScrollItem item = m_defaulInScrollList[i];
            if(item.Index >= index) item.Index += 1;
        }
        CreateItem(index);
    }

    private void DelItemFromPanel(int index)
    {
        int maxIndex = -1;
        int minIndex = int.MaxValue;
        for(int i = m_defaulInScrollList.Count; i > 0; i--)
        {
            LoopScrollItem item = m_defaulInScrollList[i - 1];
            if(item.Index == index)
            {
                if(m_onDestroyItemAction != null)
                {
                    m_onDestroyItemAction(index,item.gameObject);
                }
                else
                {
                    GameObject.Destroy(item.gameObject);
                    m_defaulInScrollList.Remove(item);
                }
            }
            if(item.Index > maxIndex)
            {
                maxIndex = item.Index;
            }
            if(item.Index < minIndex)
            {
                minIndex = item.Index;
            }
            if(item.Index > index)
            {
                item.Index -= 1;
            }
        }
        if(maxIndex < AllCount - 1)
        {
            CreateItem(maxIndex);
        }
    }

    private void CreateItem(int index)
    {
        LoopScrollItem itemBase;
        if(m_onInScrollAction != null)
        {
            GameObject obj = m_onInScrollAction(index);
            if(obj == null)
            {
                obj = m_onCreateItemAction(index);
                Utility.SetParent(obj,Content);
                itemBase = Utility.RequireComponent<LoopScrollItem>(obj);
            }
            else
            {
                obj.SetActive(true);
                itemBase = obj.GetComponent<LoopScrollItem>();
            }
        }
        else
        {
            if(m_defaultOutScrollList.Count > 0)
            {
                itemBase = m_defaultOutScrollList[0];
                itemBase.gameObject.SetActive(true);
                m_defaultOutScrollList.RemoveAt(0);
            }
            else
            {
                GameObject obj = m_onCreateItemAction(index);
                Utility.SetParent(obj,Content);
                itemBase = Utility.RequireComponent<LoopScrollItem>(obj);
            }
        }

        itemBase.Scroller = this;
        itemBase.Index = index;
       // itemBase.gameObject.name = index.ToString();
        m_defaulInScrollList.Add(itemBase);

        m_onScrollChangeAction(index,itemBase.gameObject);
    }

    private int GetPosIndex()
    {
        switch(Movement)
        {
            case Arrangement.Horizontal:
                return Mathf.FloorToInt(Content.anchoredPosition.x / -(CellWidth + CellPadidingX));
            case Arrangement.Vertical:
                return Mathf.FloorToInt(Content.anchoredPosition.y / (CellHeight + CellPadidingY));
        }
        return 0;
    }

    public Vector3 GetPosition(int i)
    {
        switch(Movement)
        {
            case Arrangement.Horizontal:
                int indexX = i / MaxPerLine;
                return new Vector3(CellWidth * indexX + indexX * CellPadidingX+LeftPadding,
                   -(CellHeight + CellPadidingY) * (i % MaxPerLine) - TopPadding,
                   0f);
            case Arrangement.Vertical:
                int indexOfX = i % MaxPerLine;
                return new Vector3(CellWidth * indexOfX + indexOfX * CellPadidingX + LeftPadding,
                  -(CellHeight + CellPadidingY) * (i / MaxPerLine)-TopPadding,
                  0f);
        }
        return Vector3.zero;
    }

    public int AllCount
    {
        get { return m_allCount; }
        set
        {
            m_allCount = value;
            UpdateTotalWidth();
        }
    }

    private void UpdateTotalWidth()
    {
        int lineCount = Mathf.CeilToInt((float)m_allCount / MaxPerLine);
        switch(Movement)
        {
            case Arrangement.Horizontal:
                Content.sizeDelta = new Vector2(CellWidth * lineCount + CellPadidingX * (lineCount - 1),Content.sizeDelta.y);
                break;
            case Arrangement.Vertical:
                Content.sizeDelta = new Vector2(Content.sizeDelta.x,CellHeight * lineCount + CellPadidingY * (lineCount - 1));
                break;
        }
    }

    private void Reset()
    {
        m_defaulInScrollList.Clear();
        m_defaultOutScrollList.Clear();
        m_index = -1;
        switch(Movement)
        {
            case Arrangement.Horizontal:
                Content.anchoredPosition = new Vector3(0,Content.anchoredPosition.y,0);
                break;
            case Arrangement.Vertical:
                Content.anchoredPosition = new Vector3(Content.anchoredPosition.x,0,0);
                break;

        }
    }

}
