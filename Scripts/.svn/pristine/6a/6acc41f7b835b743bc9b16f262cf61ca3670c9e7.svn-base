using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;

namespace Altar.Data
{

    /// <summary>
    /// 召唤祭坛系统
    /// </summary>
    public class AltarSystem: Building
    {
        private static AltarSystem instance;

        public static AltarSystem Instance
        {
            get { return instance; }
        }

        private const string AltarPath = "Altar";

        //
        private AltarData altarData;
        //

        public AltarSystem()
        {
            instance = this;
        }

        public override void Init()
        {
            if(altarData == null) altarData = new AltarData();
        }

        /// <summary>
        /// 读取存档信息
        /// </summary>
        /// <param name="parentPath"></param>
        public override void ReadData(string parentPath)
        {
            this.parentPath = parentPath;
            altarData = GameDataManager.ReadData<AltarData>(parentPath + AltarPath) as AltarData;
        }

        public override void SaveData(string parentPath)
        {
            GameDataManager.SaveData(parentPath,AltarPath,altarData);
        }


        /// <summary>
        /// 获取可召唤列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetAllAltarList()
        {
            return altarData.altarChars;
        }

        /// <summary>
        /// 初始化可召唤列表
        /// </summary>
        /// <param name="charId"></param>
        public void InitAltarCharList(int charId)
        {
            altarData.altarChars.Add(charId);
        }

        /// <summary>
        /// 从召唤列表里面移除角色
        /// </summary>
        /// <param name="charId"></param>
        public void RemoveCharFromAltarList(int charId)
        {
            if(altarData.altarChars.Contains(charId))
            {
                altarData.altarChars.Remove(charId);
            }
        }

        public Dictionary<string,CharData> GetNowCallList()
        {
            return altarData.NowCharDcit;
        }

        public Dictionary<string,CharData> GetNextCallList()
        {
            return altarData.NextCharDcit;
        }

        public bool HasCall(string uid)
        {
            return altarData.HaveCallChar.Contains(uid);
        }

        public void AddToHasCall(string uid)
        {
            if(altarData.NowCharDcit.ContainsKey(uid))
                altarData.HaveCallChar.Add(uid);
        }

        /// <summary>
        /// 更新本周
        /// </summary>
        /// <param name="list"></param>
        public void UpdateNowCallChars(Dictionary<string,CharData> list)
        {
            if(list.Count == 0)
                return;
            altarData.NowCharDcit.Clear();
            foreach(var item in altarData.NextCharDcit)
            {
                altarData.NowCharDcit.Add(item.Key,item.Value);
            }
            altarData.HaveCallChar.Clear();
        }

        /// <summary>
        /// 更新下周
        /// </summary>
        /// <param name="list"></param>
        public void UpdateNextCallChars(Dictionary<string,CharData> dict)
        {
            altarData.NextCharDcit.Clear();
            foreach(var item in dict)
            {
                altarData.NextCharDcit.Add(item.Key,item.Value);
            }
        }

        /// <summary>
        /// 获取普通召唤角色列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,CharAttribute> GetNormalCallChars()
        {
            Dictionary<string,CharAttribute> list = new Dictionary<string,CharAttribute>();
            foreach(var item in altarData.NowCharDcit)
            {
                list.Add(item.Key,new CharAttribute(item.Value));
            }
            return list;
        }

        public CharAttribute GetItemCallCharAttr(int id)
        {
            if (altarData.ItemCharDcit.ContainsKey(id))
            {
                return new CharAttribute(altarData.ItemCharDcit[id]);
            }
            return null;
        }

        public void AddCharInfoToItemCallChar(int id, CharData data)
        {
            if (!altarData.ItemCharDcit.ContainsKey(id))
            {
                altarData.ItemCharDcit.Add(id, data);
            }
        }
    }
}