using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PanelInfo
{
    public string Name;
    public string Path;

    public PanelInfo(string name,string path)
    {
        Name = name;
        Path = path;
    }
}


public class PanelConfigData: ScriptableObject
{
    [SerializeField]
    public List<PanelInfo> PanelPathList = new List<PanelInfo>();
}
