using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using UnityEngine.UI;

//  资源信息
public class AssetInfo
{
    //  资源路径
    public string assetPath;
    //  资源crc32校验码
    public string assetCrc32;
}

//  资源配置
public class AssetConfig
{
    //  资源信息列表
    Dictionary<string, AssetInfo> assetList = new Dictionary<string, AssetInfo>();
    //  是否已经加载配置
    bool isLoading_ = false;
    //  是否已经加载本地配置
    public bool isLoadingLocal = false;
    //  是否陈功加载本地配置
    public bool isLocalOK = false;
    //  是否陈功加载配置
    bool isOK_ = false;
    //  是否正在加载配置
    public bool isLoading
    {
        get { return isLoading_; }
    }
    public bool isOK
    {
        get { return isOK_; }
    }
   //添加资源
    public void AddAsset(string assetPath, string assetCrc32)
    {
        assetList.Remove(assetPath);

        AssetInfo assetInfo = new AssetInfo();
        assetInfo.assetPath = assetPath;
        assetInfo.assetCrc32 = assetCrc32;
        assetList.Add(assetInfo.assetPath, assetInfo);
    }
    //查找资源
    public AssetInfo FindAsset(string assetPath)
    {
        AssetInfo assetInfo = null;
        if (!assetList.TryGetValue(assetPath, out assetInfo))
        {
            return null;
        }
        return assetInfo;
    }
     //资源是否存在
    public bool IsAssetExist(string assetPath)
    {
        return assetList.ContainsKey(assetPath);
    }

    //  从文本加载资源信息，可以是本地文件file://，也可以是远程文件http://
	public IEnumerator Load(string configPath,List<string> storeAssetList = null)
	{
        isLoading_ = true;
        isOK_ = false;
        assetList.Clear();
        configPath += ".xml";
	
        using (WWW www = new WWW(configPath))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                www.Dispose();
                isLoading_ = false;
                yield break;
            }

            try
            {
                assetList.Clear();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(www.text);      //加载配置文件

                XmlNodeList nodeList = doc.SelectNodes("/Files/File");
                foreach (XmlNode node in nodeList)
				{
                    XmlElement xItem = (XmlElement)node;

                    string assetPath = xItem.GetAttribute("FileName");

					if(storeAssetList != null)
                    {
                        storeAssetList.Add(assetPath);
                    }
                    string assetCrc32 = xItem.GetAttribute("CRC32");
                    AddAsset(assetPath, assetCrc32);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                www.Dispose();
                isLoading_ = false;
                yield break;
            }

            www.Dispose();
            isOK_ = true;
            isLoading_ = false;
        }
    }

    //  从文本加载资源信息，可以是本地文件file://，也可以是远程文件http://
    public IEnumerator LoadLocalConfig(string configPath, List<string> storeAssetList = null)
    {
        isLoadingLocal = true;
        isLocalOK = false;
        assetList.Clear();
        configPath += ".xml";

        using (WWW www = new WWW(configPath))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                www.Dispose();
                isLoadingLocal = false;
                yield break;
            }

            try
            {
                assetList.Clear();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(www.text);      //加载配置文件

                XmlNodeList nodeList = doc.SelectNodes("/Files/File");
                foreach (XmlNode node in nodeList)
                {
                    XmlElement xItem = (XmlElement)node;

                    string assetPath = xItem.GetAttribute("FileName");

                    if (storeAssetList != null)
                    {
                        storeAssetList.Add(assetPath);
                    }
                    string assetCrc32 = xItem.GetAttribute("CRC32");
                    AddAsset(assetPath, assetCrc32);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                www.Dispose();
                isLoadingLocal = false;
                yield break;
            }

            www.Dispose();
            isLocalOK = true;
            isLoadingLocal = false;
        }
    }


   //添加保存配置文件
    public void Save(string configPath)
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
        doc.AppendChild(decl);
        XmlElement xRoot = doc.CreateElement("Files");
        doc.AppendChild(xRoot);

        foreach (KeyValuePair<string, AssetInfo> pair in assetList)
        {
            AssetInfo assetInfo = pair.Value;

            XmlElement xItem = doc.CreateElement("File");
            xItem.SetAttribute("FileName", assetInfo.assetPath);
            xItem.SetAttribute("CRC32", assetInfo.assetCrc32);
            xRoot.AppendChild(xItem);
        }

        StreamWriter sw = new StreamWriter(configPath+".xml", false, new UTF8Encoding(false));
        doc.Save(sw);
        sw.Close();
    }
}
