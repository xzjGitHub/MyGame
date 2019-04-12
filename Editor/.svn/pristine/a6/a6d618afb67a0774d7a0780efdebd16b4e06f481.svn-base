using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

public class UnityEditorLogOpenAsset 
{

    /// <summary>
    /// 解决代码定位问题
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="line"></param>
    /// <returns></returns>
#if UNITY_EDITOR
    [UnityEditor.Callbacks.OnOpenAsset]
    static bool OnOpenAsset(int instanceID, int line)
    {
        string stackTrace = GetStackTrace();
        if (!string.IsNullOrEmpty(stackTrace) && stackTrace.Contains("LogHelperLSK:Log"))
        {
            Match matches = Regex.Match(stackTrace, @"\(at (.+)\)", RegexOptions.IgnoreCase);
            string pathline = "";

            while (matches.Success)
            {
                pathline = matches.Groups[1].Value;

                if (!pathline.Contains("LogOutOperation.cs"))
                {
                    int splitIndex = pathline.LastIndexOf(":");
                    string path = pathline.Substring(0, splitIndex);
                    line = System.Convert.ToInt32(pathline.Substring(splitIndex + 1));
                    string fullPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
                    fullPath += path;
                    UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fullPath.Replace("/", "\\"), line);
                    break;
                }

                matches = matches.NextMatch();
            }
            return true;
        }
        return false;
    }

    static string GetStackTrace()
    {
        var ConsoleWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
        var fieldInfo = ConsoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
        var consoleWindowInstance = fieldInfo.GetValue(null);

        if (consoleWindowInstance != null)
        {
            if ((object)EditorWindow.focusedWindow == consoleWindowInstance)
            {
                var ListViewStateType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ListViewState");
                fieldInfo = ConsoleWindowType.GetField("m_ListView", BindingFlags.Instance | BindingFlags.NonPublic);
                //  var listView = fieldInfo.GetValue(consoleWindowInstance);
                /// Get row in ListViewState
                fieldInfo = ListViewStateType.GetField("row", BindingFlags.Instance | BindingFlags.Public);
                //  int row = (int)fieldInfo.GetValue(listView);
                /// Get m_ActiveText in ConsoleWindow
                fieldInfo = ConsoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
                string activeText = fieldInfo.GetValue(consoleWindowInstance).ToString();
                return activeText;
            }
        }
        return null;
    }

#endif

}