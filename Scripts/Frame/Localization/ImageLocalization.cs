using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class ImageLocalization : AbsLocalization
    {
        private Image m_image;

        public override void Init()
        {
            m_image = GetComponent<Image>();
            if(m_image != null)
            {
                Debug.LogError("本地化Image组件出错，没有找到Image组件");
                return;
            }
            m_absLocalization = GetComponent<ImageLocalization>();
            if(GameDef.CanRuntimeChangeLanguage)
                LocalizationManager.Instance.Register(m_absLocalization);

            if (m_absLocalization != null)
                Localize();
        }

        #region 

        public override void Localize()
        {
            m_image.sprite = LocalizationManager.Instance.GetCurLocalSprite(LocalizerId);
        }
        #endregion
    }
}