﻿using System.Collections.Generic;

public partial class UIPanelManager
{
    /// <summary>
    /// 界面路径管理类
    /// </summary>
    class PanelPath
    {
        private Dictionary<string,string> m_dict;

        public PanelPath()
        {
            m_dict = new Dictionary<string,string>();
            InitPath();
        }

        private void InitPath()
        {
            PanelConfigData data = UnityEngine.Resources.Load<PanelConfigData>(PanelDefine.PanelCofigResPath);
            for(int i = 0; i < data.PanelPathList.Count; i++)
            {
                m_dict.Add(data.PanelPathList[i].Name,data.PanelPathList[i].Path);
            }
        }

        public string GetPanelPath(string panelName)
        {
            if(!m_dict.ContainsKey(panelName))
            {
                LogHelperLSK.LogError("获取界面路径出错，界面名称是: " + panelName);
                return "";
            }
            return m_dict[panelName];
        }
    }
}

