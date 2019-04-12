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

        private int m_currentCharId = -1;

        public void UpdateInfo(CharAttribute attr,Action<EquipPart> clickPart)
        {
            // m_currentCharId = attr.charID;
            if(attr != null)
                m_partData = CharSystem.Instance.GetCharAllEquipPartInfo(attr.charID);

            InitComponent(clickPart);
            LoadChar(attr);
            UpdateCharInfo(attr);

            if(attr != null)
                UpdateAllPartInfo(CharSystem.Instance.GetCharAllEquipPartInfo(attr.charID));
            else
                ClearPart();

            if(attr != null)
                m_currentCharId = attr.charID;
        }

        public EquipmentData GetEquipPartData(EquipPart part)
        {
            return m_partData[part];
        }


        public void Free()
        {
            if(m_currentCharId != -1)
            {
                PlayerPool.Instance.Free(m_currentCharId);
            }
        }
    }
}
