using System.Collections.Generic;
using UnityEngine;

public partial class UIPanelManager
{
    /// <summary>
    /// 界面资源管理类
    /// </summary>
    class PanelRes
    {
        private Dictionary<string,GameObject> m_allPanelObj;

        public PanelRes()
        {
            m_allPanelObj = new Dictionary<string,GameObject>();
        }

        public void AddPanelObj(string panelName,GameObject obj)
        {
            if(obj == null)
            {
                LogHelperLSK.LogError("界面资源为空:  panelName" + panelName);
                return;
            }

            if(!m_allPanelObj.ContainsKey(panelName))
            {
                m_allPanelObj.Add(panelName,obj);
            }
            else
            {
                m_allPanelObj[panelName] = obj;
            }
        }

        public void RemovePanelObj(string panelName)
        {
            if(!m_allPanelObj.ContainsKey(panelName))
            {
                LogHelperLSK.LogError("m_allPanelObj字典不存在这个界面:  panelName" + panelName);
                return;
            }
            UnityEngine.GameObject.DestroyImmediate(m_allPanelObj[panelName]);
            m_allPanelObj.Remove(panelName);
        }


        public GameObject GetPanelObj(string panelName)
        {
            GameObject panelGameObject = null;
            if(m_allPanelObj.ContainsKey(panelName))
            {
                panelGameObject = m_allPanelObj[panelName];
            }
            if(panelGameObject == null)
                LogHelperLSK.LogError("界面资源为空出错: panelName: " + panelName);
            return panelGameObject;
        }

        public GameObject GetPanelObj(string panelName,string path)
        {
            GameObject panelGameObject = null;
            if(m_allPanelObj.ContainsKey(panelName))
            {
                panelGameObject = m_allPanelObj[panelName];
            }
            if(panelGameObject == null)
            {
                panelGameObject = ResourceLoadUtil.LoadPanelByPath(path);
            }
            if(panelGameObject == null)
                LogHelperLSK.LogError("加载界面资源出错: panelName: " + panelName + " path: " + path);
            return panelGameObject;
        }


        public void DestroyAllPanelNotContain(List<string> list)
        {
            List<string> objKeys = new List<string>();
            objKeys.AddRange(m_allPanelObj.Keys);
            for(int i = 0; i < objKeys.Count; i++)
            {
                if(list != null && list.Contains(objKeys[i]))
                {
                    continue;
                }
                UnityEngine.GameObject.DestroyImmediate(m_allPanelObj[objKeys[i]]);
                m_allPanelObj.Remove(objKeys[i]);
            }
        }
    }
}

