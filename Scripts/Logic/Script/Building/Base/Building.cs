using ProtoBuf;
using System.Collections.Generic;

/// <summary>
/// 建筑基类
/// </summary>
public abstract class Building : IGameData
{
    public abstract void SaveData(string parentPath = null);

    public abstract void ReadData(string parentPath = null);

    public abstract void Init();


    public string parentPath;
}
