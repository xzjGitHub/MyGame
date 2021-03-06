﻿using Res;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum EquipPart
{
    WuQi = 1,
    KuiJia,
    TouKui,
    JiaoBu,
    XiangLian,
    JieZhi
}


namespace Char.View
{
    public partial class CharInfo: MonoBehaviour
    {
        private Dictionary<EquipPart,EquipmentData> m_partData;
      //  private int m_currentCharId = -1;
        private RequestInfo m_equestInfo;

        public void UpdateInfo(CharAttribute attr,Action<EquipPart> clickPart)
        {
            if(attr == null)
            {
                ClearPart();
            }
            else
            {
                m_partData = CharSystem.Instance.GetCharAllEquipPartInfo(attr.charID);
                //初始化组件
                InitComponent(clickPart);
                //加载角色
                LoadChar(attr);
                //更新角色信息
                UpdateCharInfo(attr);
                //更新部位信息
                UpdateAllPartInfo(m_partData);
                //保存当前点击角色
               // m_currentCharId = attr.charID;
            }
        }

        public EquipmentData GetEquipPartData(EquipPart part)
        {
            return m_partData[part];
        }

        public bool CanLoadNext()
        {
            if(m_equestInfo == null)
                return true;
            return m_equestInfo.IsDone;
        }

        public void Free()
        {
            //if(m_currentCharId != -1)
            //{
            //    PlayerPool.Instance.Free(m_currentCharId);
            //}
            if(m_charObj != null)
            {
                PrefabPool.Instance.Free(m_charPrefabAssetName,m_charObj);
                m_charObj = null;
            }
        }
    }
}
