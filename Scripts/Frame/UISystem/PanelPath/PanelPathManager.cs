using System.Collections.Generic;


public partial class UIPanelManager
{
    private Dictionary<string,string> m_dict = new Dictionary<string,string>();

    private void InitPath()
    {
        PanelConfigData data =UnityEngine.Resources.Load<PanelConfigData>(PanelDefine.PanelCofigResPath);
        for(int i = 0; i < data.PanelPathList.Count; i++)
        {
            m_dict.Add(data.PanelPathList[i].Name,data.PanelPathList[i].Path);
        }
    }

    private string GetPanelPath(string panelName)
    {
        if(!m_dict.ContainsKey(panelName))
        {
            LogHelperLSK.LogError("获取界面路径出错，界面名称是: " + panelName);
            return "";
        }
        return m_dict[panelName];
    }
}

