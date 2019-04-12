using UnityEngine;

namespace Localization
{
    public abstract class AbsLocalization:MonoBehaviour
    {
        public int LocalizerId;

        protected AbsLocalization m_absLocalization;

        private void Awake()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 本地化操作
        /// </summary>
        public abstract void Localize();

        /// <summary>
        /// 改变id
        /// </summary>
        /// <param name="id"></param>
        public void SetLocalId(int id)
        {
            LocalizerId = id;
            Localize();
        }
    }
}