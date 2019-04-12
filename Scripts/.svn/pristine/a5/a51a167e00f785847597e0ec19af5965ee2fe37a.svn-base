using UnityEngine;
using System.Collections.Generic;

namespace Localization
{
    public class LocalizationManager : Singleton<LocalizationManager>
    {
        private List<AbsLocalization> m_localizerList;
        private SystemLanguage m_curLanguage;

        private LocalizationManager()
        {
            CurLanguage = Application.systemLanguage;
        }

        public SystemLanguage CurLanguage
        {
            get { return m_curLanguage; }
            set
            {
                m_curLanguage = value;
                OnLanguageChanged();
            }
        }

        public void Register(AbsLocalization absLocalization)
        {
            if (m_localizerList == null)
                m_localizerList = new List<AbsLocalization>();
            m_localizerList.Add(absLocalization);
        }

        private void OnLanguageChanged()
        {
            for (int i = 0; i < m_localizerList.Count; i++)
            {
                m_localizerList[i].Localize();
            }
        }

        public string GetCurLocalText(int localizerId)
        {
            //todo
            switch (CurLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseTraditional:
                    return "";
                case SystemLanguage.ChineseSimplified:
                    return "";
                default:
                    return "";
            }
        }

        public Sprite GetCurLocalSprite(int localizerId)
        {
            //todo
            switch (CurLanguage)
            {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseTraditional:
                    return null;
                case SystemLanguage.ChineseSimplified:
                    return null;
                default:
                    return null;
            }
        }
    }
}