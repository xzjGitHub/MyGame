using System;
using System.Collections.Generic;
using Barrack.View;
using UnityEngine;
using UnityEngine.UI;

namespace Char.View
{
    public enum CharType
    {
        All,
        Defense,
        OutPut,
        Assist
    }

    public class CharListView: MonoBehaviour
    {
        private GameObject m_normal;
        private GameObject m_select;
        private GameObject m_select1;

        private Text m_charNum;

        private TogGroup m_tog;
        private PlayerType m_currentType = PlayerType.None;

        private CharList m_charList;
        private bool m_hasClickRemoveBtn;
        private bool m_canRemove = false;

        private Action<CharAttribute> m_clickCharAction;

        private int m_currentSelectChar = -1;

        public void InitComponent(Action<CharAttribute> action)
        {
            m_clickCharAction = action;
            m_normal = transform.Find("RemoveBtn/NormalState").gameObject;
            m_select = transform.Find("RemoveBtn/SelectState").gameObject;
            m_select1 = transform.Find("RemoveBtn/SelectBg").gameObject;

            m_charList = Utility.RequireComponent<CharList>(transform.Find("ListParent/CharList").gameObject);
            m_charList.InitComponent();

            m_charNum = transform.Find("CharNum/Num").GetComponent<Text>();

            m_tog = transform.Find("Tag").GetComponent<TogGroup>();
            m_tog.Init(ClickTag);

            Utility.AddButtonListener(transform.Find("RemoveBtn/Btn"),ClickRemoveBtn);
        }


        private void OnDisable()
        {
            m_hasClickRemoveBtn = false;
            UpdateInfo();
            //if(m_currentSelectChar != -1)
            //{
            //    m_charList.UpdateCharSelectShow(m_currentSelectChar,false);
            //    m_currentSelectChar = -1;
            //}
        }


        private void ClickTag(int index)
        {
            if(m_currentType == (PlayerType)index)
            {
                return;
            }
            m_canRemove = false;
            m_charList.FreePool();
            List<CharAttribute> list = CharSystem.Instance.GetCharterListByType((PlayerType)index);
            m_charList.InitList(list,ClickChar,SysClick,true,m_hasClickRemoveBtn);
            if(list.Count== 0)
                SysClick(null);

            UpdateCharNumShow(CharSystem.Instance.GetCharterListByType(PlayerType.All).Count);

            m_currentType = (PlayerType)index;
        }


        private void ClickChar(CharAttribute attr)
        {
            if(m_hasClickRemoveBtn)
            {
                RemoveChar(attr);
            }
            else
            {
                SysClick(attr);
            }
            m_canRemove = true;
        }


        private void SysClick(CharAttribute attr)
        {
            if(attr != null && m_currentSelectChar == attr.charID)
            {
                return;
            }
            if(m_currentSelectChar != -1)
            {
                m_charList.UpdateCharSelectShow(m_currentSelectChar,false);
            }
            m_currentSelectChar = attr == null ? -1 : attr.charID;
            m_charList.UpdateCharSelectShow(m_currentSelectChar,true);
            if(m_clickCharAction != null)
            {
                m_clickCharAction(attr);
            }
        }

        private void RemoveChar(CharAttribute attr)
        {
            bool canRemove = attr.char_template.canbeDisband != 0 && attr.Status == CharStatus.Idle;
            string title = "";
            string des = "";
            bool showCancel = false;

            Action sureAction = null;

            if(attr.Status == CharStatus.GoldProduce ||
                attr.Status == CharStatus.EnchantResearch ||
                attr.Status == CharStatus.EquipResearch)
            {
                title = MC_StringConfig.Tips_temple271;
                des = MC_StringConfig.Tips_temple272;
            }
            else
            {
                if(canRemove)
                {
                    title = MC_StringConfig.Tips_temple261;
                    des = MC_StringConfig.Tips_temple262;
                    sureAction = () =>
                    {
                        CharSystem.Instance.RemoveChar(attr.charID);
                        m_charList.FreeChar(attr.charID);
                        if(m_charList.GetCount() > 0)
                            m_charList.SysClick();
                        else
                            SysClick(null);
                        UpdateCharNumShow(CharSystem.Instance.GetCharterListByType(PlayerType.All).Count);
                    };
                    showCancel = true;
                }
                else
                {
                    title = MC_StringConfig.Tips_temple281;
                    des = MC_StringConfig.Tips_temple282;
                }
            }
            ConfirmPanelUtil.ShowConfirmPanel(des,title,sureAction,null,true,showCancel);
        }

        private void ClickRemoveBtn()
        {
            m_hasClickRemoveBtn = !m_hasClickRemoveBtn;
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            m_charList.UpdateRemoveBtnShow(m_hasClickRemoveBtn);
            UpdateStateShow();
        }

        private void UpdateStateShow()
        {
            m_normal.SetActive(!m_hasClickRemoveBtn);
            m_select.SetActive(m_hasClickRemoveBtn);
            m_select1.SetActive(m_hasClickRemoveBtn);
        }

        private void UpdateCharNumShow(int nowNum)
        {
            m_charNum.text = nowNum + "/" + ControllerCenter.Instance.AltarController.GetMaxCharNum(); ;
        }

        public void Free()
        {
            m_currentSelectChar = -1;
            m_charList.FreePool();
        }
    }
}
