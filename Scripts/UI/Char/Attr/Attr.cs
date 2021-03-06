﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Char.View
{
    public class Attr: MonoBehaviour
    {
        private GameObject m_prefab;

        private ContentSizeFitter m_sizeFitter;

        private CharAttribute m_attr;
        private CoroutineUtil m_cor;

        private void OnEnable()
        {
            UpdatePosInfo();
        }

        private void OnDisable()
        {
            //if(m_cor != null)
            //{
            //    if(m_cor.Running)
            //    {
            //        m_cor.Stop();
            //    }
            //}
        }

        public void Free()
        {
            PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.CharAttrItem1);
        }

        public void InitComponent()
        {
            m_prefab = transform.Find("CharAttr").gameObject;
            m_prefab.SetActive(false);
            m_sizeFitter = transform.parent.GetComponent<ContentSizeFitter>();
        }

        public IEnumerator UpdateInfo(CharAttribute attr)
        {
            m_attr = attr;

            Free();

            Char_template info = Char_templateConfig.GetTemplate(attr.templateID);
            for(int i = 0; i < info.attributeSet.Count; i++)
            {
                Char_display dis = Char_displayConfig.GetChar_display(info.attributeSet[i]);
                if(dis == null)
                    continue;
                if(dis.attributeCategory == 2)
                    continue;
                GameObject obj = PrefabPool.Instance.GetObjSync(StringDefine.ObjectPooItemKey.CharAttrItem1,m_prefab);
                Utility.SetParent(obj,transform);
                obj.name = dis.attributeID.ToString();

                Image icon = obj.transform.Find("Icon").GetComponent<Image>();
                icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharAttrIcon,dis.attributeIcon);
                icon.SetNativeSize();
                obj.transform.Find("Num").GetComponent<Text>().text =
                    Utility.GetNumberPoint(CharAttributeUtil.GetAttrValue(attr,dis.attributeName),2).ToString();

                GameObject tip = obj.transform.Find("Tip").gameObject;
                tip.SetActive(false);
                CharAttrTip attrTip = Utility.RequireComponent<CharAttrTip>(tip);
                attrTip.InitComponent();

                GameObject btn = obj.transform.Find("Btn").gameObject;
                Utility.AddButtonListener(obj.transform.Find("Btn"),() =>
                {
                    tip.SetActive(true);
                    attrTip.UpdateInfo(dis.attributeID,m_attr);
                });

                yield return null;
            }
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
