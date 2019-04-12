using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Char.View
{
    public partial class CharInfo
    {
        private GameObject m_sureBtn;
        private GameObject m_canRenameTag;
        private GameObject m_nameMask;
        private Transform m_charPos;
        private InputField m_name;
        private Text m_rank;
        private Text m_quility;
        private Text m_level;

        private ContentSizeFitter m_sizeFitter;
        private CoroutineUtil m_cor;

        private bool m_hasInit;

        private Dictionary<EquipPart,EquipPartInfo> m_partInfo = new Dictionary<EquipPart,EquipPartInfo>();

        private Action<EquipPart> m_clickPart;
        private CharAttribute m_attr;

        private string m_nameValue;

        private void InitComponent(Action<EquipPart> clickPart)
        {
            m_clickPart = clickPart;
            if(!m_hasInit)
            {
                m_nameMask = transform.Find("CharInfo/NameMask").gameObject;
                m_canRenameTag = transform.Find("CharInfo/Tag").gameObject;

                m_charPos = transform.Find("PlayerPos");
                m_name = transform.Find("CharInfo/Name").GetComponent<InputField>();
                m_name.onValueChanged.AddListener(OnNameChane);

                m_rank = transform.Find("CharInfo/Info/Content/Rank").GetComponent<Text>();
                m_quility = transform.Find("CharInfo/Info/Content/Qui").GetComponent<Text>();
                m_level = transform.Find("CharInfo/Info/Content/Level").GetComponent<Text>();
                m_sureBtn = transform.Find("CharInfo/Sure").gameObject;
                m_sureBtn.SetActive(false);
                Utility.AddButtonListener(m_sureBtn.transform,ClickSureBtn);

                m_sizeFitter = transform.Find("CharInfo/Info/Content").GetComponent<ContentSizeFitter>();

                InitPart();
                m_hasInit = true;
            }
        }

        private void InitPart()
        {
            EquipPartInfo wuqi = Utility.RequireComponent<EquipPartInfo>(transform.Find("Equip/Wuqi").gameObject);
            wuqi.InitComponent(ClickPart,EquipPart.WuQi);

            EquipPartInfo kuijia = Utility.RequireComponent<EquipPartInfo>(transform.Find("Equip/KuiJia").gameObject);
            kuijia.InitComponent(ClickPart,EquipPart.KuiJia);

            EquipPartInfo xianglian = Utility.RequireComponent<EquipPartInfo>(transform.Find("Equip/XiangLian").gameObject);
            xianglian.InitComponent(ClickPart,EquipPart.XiangLian);

            EquipPartInfo jiezhi = Utility.RequireComponent<EquipPartInfo>(transform.Find("Equip/Jiezhi").gameObject);
            jiezhi.InitComponent(ClickPart,EquipPart.JieZhi);

            m_partInfo[EquipPart.WuQi] = wuqi;
            m_partInfo[EquipPart.KuiJia] = kuijia;
            m_partInfo[EquipPart.XiangLian] = xianglian;
            m_partInfo[EquipPart.JieZhi] = jiezhi;

        }

        private void UpdateCharInfo(CharAttribute attr)
        {
            m_attr = attr;

            m_nameValue = attr == null ? string.Empty : attr.CharName;
            m_name.text = attr == null ? string.Empty : attr.CharName;

            if(attr == null)
            {
                m_nameValue = string.Empty;
                m_name.text = string.Empty;

                m_canRenameTag.SetActive(false);
                m_nameMask.SetActive(false);

                m_rank.text = string.Empty;
                m_quility.text = string.Empty;
                m_level.text = string.Empty;
            }
            else
            {
                m_nameValue = attr.CharName;
                m_name.text = attr.CharName;

                m_canRenameTag.SetActive(!attr.IsRename);
                m_nameMask.SetActive(attr.IsRename);
                string des = "【{0}】";
                if(attr.IsCommander)
                {
                    des = "【{0}】【指挥官】";
                }
                m_rank.text = string.Format(des,CharaterUti.GetRankName(attr.CharRank));
                m_quility.text = "资质: " + attr.charQuality;
                m_level.text = "等级: " + attr.charLevel;
            }
            m_sureBtn.SetActive(false);
            UpdatePosInfo();
        }

        private void UpdateAllPartInfo(Dictionary<EquipPart,EquipmentData> info)
        {
            List<EquipPart> keys = new List<EquipPart>();
            keys.AddRange(m_partInfo.Keys);
            for(int i = 0; i < keys.Count; i++)
            {
                UpdatePartInfo(keys[i],info[keys[i]]);
            }
        }

        private void ClearPart()
        {
            m_partInfo[EquipPart.WuQi].UpdateInfo(null);
            m_partInfo[EquipPart.KuiJia].UpdateInfo(null);
            m_partInfo[EquipPart.XiangLian].UpdateInfo(null);
            m_partInfo[EquipPart.JieZhi].UpdateInfo(null);

            m_partInfo[EquipPart.WuQi].CanClickPart = false;
            m_partInfo[EquipPart.KuiJia].CanClickPart = false;
            m_partInfo[EquipPart.XiangLian].CanClickPart = false;
            m_partInfo[EquipPart.JieZhi].CanClickPart = false;
        }

        public void UpdatePartInfo(EquipPart part,EquipmentData data)
        {
            m_partInfo[part].UpdateInfo(data);
            m_partData[part] = data;
            m_partInfo[part].CanClickPart = true;
        }

        private void LoadChar(CharAttribute attr)
        {
            Free();
            if(attr != null)
            {
                GameObject obj = PlayerPool.Instance.GetPlayer(attr);
                if(obj != null)
                {
                    Utility.SetParent(obj,m_charPos,true,new Vector3(0.6f,0.6f,0));
                }
            }
        }

        private void ClickPart(EquipPart part)
        {
            m_clickPart(part);
        }

        private void ClickSureBtn()
        {
            ConfirmPanelUtil.ShowConfirmPanel(MC_StringConfig.Tips_temple552,MC_StringConfig.Tips_temple551,
                () =>
                {
                    string name = m_name.text;
                    m_attr.IsRename = true;
                    m_attr.CharName = name;

                    m_sureBtn.SetActive(false);
                    m_canRenameTag.SetActive(false);
                    m_nameMask.SetActive(true);
                },
                () =>
                {
                    m_name.text = m_nameValue;
                    m_sureBtn.SetActive(false);
                });
        }

        private void OnNameChane(string s)
        {
            m_sureBtn.SetActive(true);
        }

        public bool IsInChangeNameState()
        {
            return m_sureBtn.activeSelf;
        }

        public void CancelChangeName()
        {
            m_name.text = m_nameValue;
            m_sureBtn.SetActive(false);
        }

        private void UpdatePosInfo()
        {
            if(m_cor != null)
            {
                if(m_cor.Running)
                {
                    m_cor.Stop();
                }
            }
            m_cor = new CoroutineUtil(UpdatePos(),false);
            m_cor.Start();
        }

        private IEnumerator UpdatePos()
        {
            yield return null;
            m_sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            m_sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
}
