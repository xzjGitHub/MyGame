using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class DataOperation
{
    private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

    /// <summary>
    /// 判断文件是否存在
    /// </summary>
    public static bool IsFileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    /// <summary>
    /// 判断文件夹是否存在
    /// </summary>
    public static bool IsDirectoryExists(string _path)
    {
        return Directory.Exists(_path);
    }

    /// <summary>
    /// 创建一个文本文件    
    /// </summary>
    /// <param name="fileName">文件路径</param>
    /// <param name="content">文件内容</param>
    public static void CreateFile(string fileName, string content)
    {
        StreamWriter streamWriter = File.CreateText(fileName);
        streamWriter.Write(content);
        streamWriter.Close();
    }

    /// <summary>
    /// 创建一个文件夹
    /// </summary>
    private static void CreateDirectory(string fileName)
    {
        //文件夹存在则返回
        if (IsDirectoryExists(fileName))
        {
            return;
        }

        Directory.CreateDirectory(fileName);
    }
    /// <summary>
    /// 删除文件夹
    /// </summary>
    public static void DeleteDirectory(string _path)
    {
        //文件夹存在则返回
        if (!IsDirectoryExists(_path))
        {
            return;
        }

        Directory.Delete(_path, true);
    }



    public static bool SetDataJosn(string dirpath, string fileName, object pObject)
    {
        try
        {
            //创建存档文件夹
            CreateDirectory(dirpath);
            //将对象序列化为字符串
            string toSave = SerializeObject(pObject);
            //对字符串进行加密,32位加密密钥
            //     toSave = RijndaelEncrypt(toSave, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            //定义存档文件路径
            fileName = dirpath + "/" + fileName;
            StreamWriter streamWriter = File.CreateText(fileName);
            streamWriter.Write(toSave);
            streamWriter.Close();
            //   AssetDatabase.Refresh();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool SetDataProtoBuf<T>(string dirpath, string fileName, T pObject)
    {
        try
        {
            //创建存档文件夹
            CreateDirectory(dirpath);
            //定义存档文件路径
            fileName = dirpath + "/" + fileName;
            ProtobufDemo.GeneralSerialize(pObject, fileName);
            return true;
        }
        catch (Exception e)
        {
            LogHelperLSK.LogWarning(e);
            return false;
        }
    }

    public static object GetData(string fileName, Type pType)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }

        try
        {
            StreamReader streamReader = File.OpenText(fileName);
            string data = streamReader.ReadToEnd();
            //对数据进行解密，32位解密密钥
            //     data = RijndaelDecrypt(data, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            streamReader.Close();
            return DeserializeObject(data, pType);
        }
        catch (Exception)
        {
            return null;
        }

    }

    public static object GetDataJosn(string fileName, Type pType)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }

        try
        {
            StreamReader streamReader = File.OpenText(fileName);
            string data = streamReader.ReadToEnd();
            //对数据进行解密，32位解密密钥
            //     data = RijndaelDecrypt(data, "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            streamReader.Close();
            return DeserializeObject(data, pType);
        }
        catch (Exception e)
        {
            LogHelperLSK.LogError(e);
            return null;
        }

    }
    public static object GetDataProtoBuf<T>(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return null;
        }

        try
        {
            object _obj = ProtobufDemo.GeneralDeserialize<T>(fileName);
            return _obj;
        }
        catch (Exception e)
        {
            LogHelperLSK.LogWarning(e);
            LogHelperLSK.LogError(fileName);
            return null;
        }

    }


    /// <summary>
    /// Rijndael加密算法
    /// </summary>
    /// <param name="pString">待加密的明文</param>
    /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
    /// <param name="iv">iv向量,长度为128（byte[16])</param>
    /// <returns></returns>
    private static string RijndaelEncrypt(string pString, string pKey)
    {
        //密钥
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
        //待加密明文数组
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);

        //Rijndael解密算法
        RijndaelManaged rDel = new RijndaelManaged
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        ICryptoTransform cTransform = rDel.CreateEncryptor();

        //返回加密后的密文
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// ijndael解密算法
    /// </summary>
    /// <param name="pString">待解密的密文</param>
    /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
    /// <param name="iv">iv向量,长度为128（byte[16])</param>
    /// <returns></returns>
    private static string RijndaelDecrypt(string pString, string pKey)
    {
        //解密密钥
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
        //待解密密文数组
        byte[] toEncryptArray = Convert.FromBase64String(pString);

        //Rijndael解密算法
        RijndaelManaged rDel = new RijndaelManaged
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        ICryptoTransform cTransform = rDel.CreateDecryptor();

        //返回解密后的明文
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return UTF8Encoding.UTF8.GetString(resultArray);
    }


    /// <summary>
    /// 将一个对象序列化为字符串
    /// </summary>
    /// <returns>The object.</returns>
    /// <param name="pObject">对象</param>
    /// <param name="pType">对象类型</param>
    private static string SerializeObject(object pObject)
    {
        //序列化后的字符串
        string serializedString = string.Empty;
        //使用Json.Net进行序列化
        serializedString = JsonConvert.SerializeObject(pObject, jsonSerializerSettings);
        return serializedString;
    }

    /// <summary>
    /// 将一个字符串反序列化为对象
    /// </summary>
    /// <returns>The object.</returns>
    /// <param name="pString">字符串</param>
    /// <param name="pType">对象类型</param>
    private static object DeserializeObject(string pString, Type pType)
    {
        //反序列化后的对象
        object deserializedObject = null;
        //使用Json.Net进行反序列化
        deserializedObject = JsonConvert.DeserializeObject(pString, pType, jsonSerializerSettings);
        return deserializedObject;
    }
}

public class ProtobufDemo
{
    private static readonly string abcStr = "buvcfgahopqidejwxrstyklmnz";

    /// <summary>  
    /// 加密序列化  
    /// </summary>  
    /// <param name="obj">要序列化的对象</param>  
    /// <param name="path">保存路径</param>  
    /// <returns></returns>  
    public static void GeneralSerialize<T>(T obj, string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        //
        using (FileStream fileStream = File.Create(path))
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider
            {
                Key = ASCIIEncoding.ASCII.GetBytes(abcStr[6].ToString() + abcStr[0].ToString() + abcStr[17].ToString() + abcStr[10].ToString() + abcStr[20].ToString() + abcStr[8].ToString() + abcStr[4].ToString() + abcStr[12].ToString()),
                IV = ASCIIEncoding.ASCII.GetBytes(abcStr[16].ToString() + abcStr[10].ToString() + abcStr[7].ToString() + abcStr[1].ToString() + abcStr[2].ToString() + abcStr[18].ToString() + abcStr[14].ToString() + abcStr[21].ToString())
            };
            CryptoStream crStream = new CryptoStream(fileStream, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
            Serializer.Serialize<T>(crStream, obj);
            crStream.Close();
            fileStream.Close();
        }
    }

    /// <summary>  
    /// 反序列化  
    /// </summary>  
    /// <param name="path">文件路径</param>  
    /// <returns></returns>  
    public static T GeneralDeserialize<T>(string path)
    {
        T retObj;
        using (FileStream file = File.OpenRead(path))
        {
            DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider
            {
                Key = ASCIIEncoding.ASCII.GetBytes(abcStr[6].ToString() + abcStr[0].ToString() + abcStr[17].ToString() + abcStr[10].ToString() + abcStr[20].ToString() + abcStr[8].ToString() + abcStr[4].ToString() + abcStr[12].ToString()),
                IV = ASCIIEncoding.ASCII.GetBytes(abcStr[16].ToString() + abcStr[10].ToString() + abcStr[7].ToString() + abcStr[1].ToString() + abcStr[2].ToString() + abcStr[18].ToString() + abcStr[14].ToString() + abcStr[21].ToString())
            };
            CryptoStream crStream = new CryptoStream(file, cryptic.CreateDecryptor(), CryptoStreamMode.Read);
            retObj = Serializer.Deserialize<T>(crStream);
            crStream.Close();
            file.Close();
        }
        return retObj;
    }


}