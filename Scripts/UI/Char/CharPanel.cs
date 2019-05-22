﻿
namespace Char.View
{
    public partial class CharPanel: UIPanelBehaviour
    {
        private void ClickCharCallBack(CharAttribute attr)
        {
            m_char = attr;
            m_charInfo.UpdateInfo(attr,ClickPart);
        }

        #region part

        private void ClickPart(EquipPart part)
        {
            EquipmentData data = m_charInfo.GetEquipPartData(part);
            if(data == null)
            {
                m_equipObj.SetActive(true);
                m_charListObj.SetActive(false);
                m_attrObj.SetActive(false);
                m_tog.ClickTog(1);
                m_equipPanel.ClickTag(GetTagIndex(part));
            }
            else
            {
                //显示一个
                EquipTiPanel singleTip = UIPanelManager.Instance.Show<EquipTiPanel>(CavasType.PopUI);
                singleTip.UpdateInfo(new EquipAttribute(data),null,ClickUnLoadCallBack);
            }
        }

        private int GetTagIndex(EquipPart part)
        {
            switch(part)
            {
                case EquipPart.WuQi:
                    return 1;
                case EquipPart.KuiJia:
                case EquipPart.TouKui:
                case EquipPart.JiaoBu:
                    return 2;
                case EquipPart.XiangLian:
                case EquipPart.JieZhi:
                    return 3;
                default:
                    return 0;
            }
        }

        #endregion

        #region equip
        private void ClickEquipCallBack(ItemAttribute attr)
        {
            EquipAttribute equipAttr = attr as EquipAttribute;
            Equip_template equip = Equip_templateConfig.GetEquip_template(equipAttr.equipRnd.templateID);
            EquipmentData data = m_charInfo.GetEquipPartData((EquipPart)equip.equipSlot);
            if(data == null)
            {
                //显示一个
                EquipTiPanel singleTip = UIPanelManager.Instance.Show<EquipTiPanel>(CavasType.PopUI);
                singleTip.UpdateInfo(equipAttr,ClickEquipCallBack,null);
            }
            else
            {
                //点击的是当前准备的装备
                if(data.itemID == attr.itemID)
                {
                    //显示一个
                    EquipTiPanel singleTip = UIPanelManager.Instance.Show<EquipTiPanel>(CavasType.PopUI);
                    singleTip.UpdateInfo(equipAttr,null,ClickUnLoadCallBack);
                }
                else
                {
                    //显示两个
                    EquipTiPanel2 twoTip = UIPanelManager.Instance.Show<EquipTiPanel2>(CavasType.PopUI);
                    twoTip.UpdateInfo(new EquipAttribute(data),equipAttr,ClickEquipCallBack,ClickUnLoadCallBack);

                }
            }
        }

        //装备回掉
        private void ClickEquipCallBack(EquipAttribute attr)
        {
            if(!CanEquip(attr))
                return;

            //如果当前位置有装备了 需要先卸载下
            Equip_template equip = Equip_templateConfig.GetEquip_template(attr.equipRnd.templateID);
            EquipmentData data = m_charInfo.GetEquipPartData((EquipPart)equip.equipSlot);
            if(data != null)
            {
                CharSystem.Instance.CharStripEquipment(data.itemID,m_char.charID);
                //更新装备列表所属角色信息
                m_equipPanel.m_equipList.UpdateBelongCharInfo(data.itemID);
            }

            //如果这件装备之前有人穿了 需要卸下
            if(attr.charID != 0)
            {
                CharSystem.Instance.CharStripEquipment(attr.itemID,attr.charID);
            }

            //穿戴给该角色
            CharSystem.Instance.CharWearEquipment(attr.itemID,m_char.charID);

            //更新部位信息
            Equip_template tem = Equip_templateConfig.GetEquip_template(attr.equipRnd.templateID);
            m_charInfo.UpdatePartInfo((EquipPart)tem.equipSlot,attr.GetItemData() as EquipmentData);

            //更新装备列表所属角色信息
            m_equipPanel.m_equipList.UpdateBelongCharInfo(attr.itemID);

            m_detialInfo.UpdateInfo(m_char);
        }

        private bool CanEquip(EquipAttribute attr)
        {
            if(attr.EquipState == EquipState.Enchanting)
            {
                TipManager.Instance.ShowTip("装备正在附魔，无法装备");
                return false;
            }
            if(attr.EquipState == EquipState.Researching)
            {
                TipManager.Instance.ShowTip("装备正在研究，无法装备");
                return false;
            }

            Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
            if(m_char.charLevel < item.charLevelReq)
            {
                TipManager.Instance.ShowTip(MC_StringConfig.Tips_temple3);
                return false;
            }

            if(m_char.templateID != item.charIDReq && item.charIDReq != 0)
            {
                TipManager.Instance.ShowTip(MC_StringConfig.Tips_temple4);
                return false;
            }
            return true;
        }

        //卸载回掉
        private void ClickUnLoadCallBack(EquipAttribute attr)
        {
            CharSystem.Instance.CharStripEquipment(attr.itemID,attr.charID);

            //更新部位信息
            Equip_template tem = Equip_templateConfig.GetEquip_template(attr.equipRnd.templateID);
            m_charInfo.UpdatePartInfo((EquipPart)tem.equipSlot,null);

            //更新装备列表所属角色信息
            m_equipPanel.m_equipList.UpdateBelongCharInfo(attr.itemID);

            m_detialInfo.UpdateInfo(m_char);
        }

        #endregion

    }
}
