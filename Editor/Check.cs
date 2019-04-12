using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckRaycastUtil: EditorWindow
{

    private List<Image> imageList;
    private List<RawImage> rawimageList;
    private List<Text> textList;

    private bool includeInactive = true;
    private Transform[] selectTrans;
    private Vector2 scrollPosition;
    static CheckRaycastUtil window;

    [MenuItem("UI检查/检查UI raycastTarget属性")]
    static void ModifyAtlas()
    {
        window = (CheckRaycastUtil)EditorWindow.GetWindow(typeof(CheckRaycastUtil),false,"检查UI raycastTarget属性");
        window.Show();
    }

    private void OnEnable()
    {
        imageList = new List<Image>();
        rawimageList = new List<RawImage>();
        textList = new List<Text>();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        includeInactive = EditorGUILayout.Toggle("包含隐藏物体：",includeInactive);

        EditorGUILayout.Space();

        if(GUILayout.Button("检查选中UI raycastTarget属性（不包含按钮）"))
        {
            CheckSelected();
        }

        EditorGUILayout.Space();

        if(GUILayout.Button("关闭所有raycastTarget属性"))
        {
            RemoveRaycast();
        }

        int allCount = imageList.Count + rawimageList.Count + textList.Count;

        if(allCount > 0)
        {
            int index = 0;
            scrollPosition = GUI.BeginScrollView(new Rect(0,120,Screen.width,500),scrollPosition,new Rect(0,120,Screen.width,allCount * 20));
            for(int i = 0; i < imageList.Count; i++)
            {
                index++;
                imageList[i] = EditorGUI.ObjectField(new Rect(30,120 + index * 20,Screen.width,20),imageList[i].name,imageList[i],typeof(Image),true) as Image;
                imageList[i].raycastTarget = EditorGUI.Toggle(new Rect(5,120 + index * 20,20,20),imageList[i].raycastTarget);
            }
            for(int i = 0; i < rawimageList.Count; i++)
            {
                index++;
                rawimageList[i] = EditorGUI.ObjectField(new Rect(30,120 + index * 20,Screen.width,20),rawimageList[i].name,rawimageList[i],typeof(RawImage),true) as RawImage;
                rawimageList[i].raycastTarget = EditorGUI.Toggle(new Rect(5,120 + index * 20,20,20),rawimageList[i].raycastTarget);
            }
            for(int i = 0; i < textList.Count; i++)
            {
                index++;
                textList[i] = EditorGUI.ObjectField(new Rect(30,120 + index * 20,Screen.width,20),textList[i].name,textList[i],typeof(Text),true) as Text;
                textList[i].raycastTarget = EditorGUI.Toggle(new Rect(5,120 + index * 20,20,20),textList[i].raycastTarget);
            }

            GUI.EndScrollView();
        }
    }

    /// <summary>
    /// 检查已选中的项
    /// </summary>
    void CheckSelected()
    {
        imageList.Clear();
        rawimageList.Clear();
        textList.Clear();
        selectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        if(selectTrans == null)
        {
            Debug.LogError("没有选中UI。");
            return;
        }
        for(int i = 0; i < selectTrans.Length; i++)
        {
            Image[] images = selectTrans[i].GetComponentsInChildren<Image>(includeInactive);
            if(images != null)
            {
                for(int j = 0; j < images.Length; j++)
                {
                    if(images[j] != null && images[j].gameObject.GetComponent<Button>() == null && images[j].gameObject.GetComponent<ScrollRect>() == null)
                        imageList.Add(images[j]);
                }
            }

            RawImage[] rawimages = selectTrans[i].GetComponentsInChildren<RawImage>(includeInactive);
            if(rawimages != null)
            {
                for(int j = 0; j < rawimages.Length; j++)
                {
                    if(rawimages[j] != null && rawimages[j].gameObject.GetComponent<Button>() == null && rawimages[j].gameObject.GetComponent<ScrollRect>() == null)
                        rawimageList.Add(rawimages[j]);
                }
            }


            Text[] texts = selectTrans[i].GetComponentsInChildren<Text>(includeInactive);
            if(texts != null)
            {
                for(int j = 0; j < texts.Length; j++)
                {
                    if(texts[j] != null && texts[j].gameObject.GetComponent<Button>() == null && texts[j].gameObject.GetComponent<ScrollRect>() == null)
                        textList.Add(texts[j]);
                }
            }
        }
    }

    /// <summary>
    /// 移除UI射线
    /// </summary>
    void RemoveRaycast()
    {
        for(int i = 0; i < imageList.Count; i++)
        {
            imageList[i].raycastTarget = false;
        }
        for(int i = 0; i < rawimageList.Count; i++)
        {
            rawimageList[i].raycastTarget = false;
        }
        for(int i = 0; i < textList.Count; i++)
        {
            textList[i].raycastTarget = false;
        }
    }
}