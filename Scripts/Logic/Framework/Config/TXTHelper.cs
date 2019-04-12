using System.IO;
using UnityEngine;


namespace LskConfig
{

    /// <summary>
    /// Txt文档数据类必须实现的接口
    /// 解析Txt文档的字符串赋值给对应数据对象
    /// </summary>
    public interface IReader
    {
        void Reader(string content);
    }

    public class TXTHelper
    {
        /// <summary>
        /// 反序列化Txt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T FormatConfig<T>(string path,bool isResource =false) where T : class, new()
        {
            string str = string.Empty;
            if (isResource )
            {
                path = path.Replace(".txt", "");
                var temp = (TextAsset)Resources.Load(path, typeof(TextAsset));
                str = temp.text;
                str=str.Replace("\\n","\n");
            }
            else
            {
                //不存在就创建目录 
                if (!System.IO.File.Exists(path))
                {
                    LogHelperLSK.LogError("文件不存在");
                    return null;
                }
                str = File.ReadAllText(path);
            }
            T data = new T();
            ((IReader)data).Reader(str);
            return data;
        }

    }
}
