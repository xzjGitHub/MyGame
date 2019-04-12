using UnityEngine;
using System.Collections;
using UnityEditor;

public class Reset : EditorWindow
{
    [MenuItem("UI检查/Reset")]
    static void Rest()
    {
        EditorWindow.GetWindow<Reset>("Reset", true);
    }

    private GameObject go = null;
    private void OnGUI()
    {
        go = EditorGUILayout.ObjectField("panel", go, typeof(GameObject), true) as GameObject;
        if( GUILayout.Button("位置四舍五入取整")) {
            if( go == null ) {
                EditorUtility.DisplayDialog("出错啦", "请先选择一个物体！", "我错了！");
                return;
            }
            RestPos(go.transform);
            EditorUtility.SetDirty(go);
            EditorUtility.DisplayDialog("系统提示", "帮你搞定了", "感谢，感谢！");
        }

        if( GUILayout.Button("旋转度数四舍五入取整")) {
            if( go == null ) {
                EditorUtility.DisplayDialog("出错啦", "请先选择一个物体！", "我错了！");
                return;
            }
            RestRation(go.transform);
            EditorUtility.SetDirty(go);
            EditorUtility.DisplayDialog("系统提示", "帮你搞定了", "感谢，感谢！");
        }

        if( GUILayout.Button("缩放四舍五入取整") ) {
            if( go == null ) {
                EditorUtility.DisplayDialog("出错啦", "请先选择一个物体！", "我错了！");
                return;
            }
            RestScale(go.transform);
            EditorUtility.SetDirty(go);
            EditorUtility.DisplayDialog("系统提示", "帮你搞定了", "感谢，感谢！");
        }

        if( GUILayout.Button("ResetAll") ) {
            if( go == null ) {
                EditorUtility.DisplayDialog("出错啦", "请先选择一个物体！", "我错了！");
                return;
            }
            RestPos(go.transform);
            RestRation(go.transform);
           // RestScale(go.transform);
            EditorUtility.SetDirty(go);
            EditorUtility.DisplayDialog("系统提示", "帮你搞定了", "感谢，感谢！");
        }
    }

    void RestPos(Transform trans)
    {
        trans.localPosition = new Vector3(Mathf.FloorToInt(trans.localPosition.x + 0.5f),
            Mathf.FloorToInt(trans.localPosition.y + 0.5f), 0);
        foreach( Transform childTrans in trans ) {
            RestPos(childTrans);
        }
    }

    void RestRation(Transform trans)
    {
        trans.localEulerAngles = new Vector3(Mathf.FloorToInt(trans.localEulerAngles.x + 0.5f),
            Mathf.FloorToInt(trans.localEulerAngles.y + 0.5f),
            Mathf.FloorToInt(trans.localEulerAngles.z + 0.5f));
        foreach( Transform childTrans in trans ) {
            RestRation(childTrans);
        }
    }

    void RestScale(Transform trans)
    {
        trans.localScale =Vector3.one;
        foreach( Transform childTrans in trans ) {
            RestScale(childTrans);
        }
    }
}
