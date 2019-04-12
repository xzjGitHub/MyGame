using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


public class CombatSystemTools
{
    public static float ChanceMax = RandomBuilder.RandomMaxFactor;


    /// <summary>
    /// 获得属性值
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="targetStr"></param>
    /// <returns></returns>
    public static object GetPropertyValue(object obj, string targetStr)
    {
        var memberInfo = GetProperty(obj, targetStr);
        return memberInfo == null ? null : memberInfo.GetValue(obj, null);
    }

    public static FieldInfo GetField(object obj, string targetStr)
    {
        var t = obj.GetType();
        return t.GetField(targetStr, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }

    public static PropertyInfo GetProperty(object obj, string targetStr)
    {
        var t = obj.GetType();
        return t.GetProperty(targetStr, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }

    /// <summary>
    /// 取随机数
    /// </summary>
    /// <param name="_min">最小值</param>
    /// <param name="_max">最大值</param>
    /// <param name="_sum">取个数</param>
    /// <param name="_indexs"></param>
    /// <returns></returns>
    public static List<int> GetRandomSum(int _min = 0, int _max = 1, int _sum = 1, List<int> _indexs = null)
    {
        List<int> sums = new List<int>();
        //
        if (_indexs == null)
        {
            _indexs = new List<int>();
            for (int i = 0; i < _max - _min; i++)
            {
                _indexs.Add(i);
            }
        }
        //
        for (int i = 0; i < _sum; i++)
        {
            sums.Add(_indexs[RandomBuilder.Instance.Random.Next(0, _indexs.Count)]);
            _indexs.Remove(sums.Last());
        }
        return sums;
    }
    //种子最大数10000  最小1
    /// <summary>
    /// 得到随机数
    /// </summary>
    /// <param name="_multiple">最大数</param>
    /// <param name="_listChances">概率列表</param>
    /// <returns>0：第一个、 1：其他</returns>
    public static int GetRandomSumChance(int _multiple = 10000, List<float> _listChances = null)
    {
        //单纯取1-_multiple之间的数
        if (_listChances == null) return RandomBuilder.Instance.Random.Next(0, _multiple + 1);
        //取1-_multiple之间的数和一个最小的概率比较,0：自己、 1：其他
        if (_listChances.Count == 1) return _listChances[0] > new Random().Next(0, _multiple + 1) ? 0 : 1;
        //
        int _temp = RandomBuilder.Instance.Random.Next(0, _multiple + 1);
        //
        List<float> lists = new List<float>();
        lists.AddRange(_listChances);
        lists = lists.OrderBy(a => a).ToList();
        //
        for (int i = 0; i < lists.Count; i++)
        {
            if (_temp >= lists[i]) continue;
            //现在这个概率小于生成的概率 直接返回当前位置
            for (int j = 0; j < _listChances.Count; j++)
            {
                if (_listChances[j] == lists[i]) return j;
            }
        }
        return 0;
    }

    /// <summary>
    /// 获得正态分布随机数
    /// </summary>
    /// <param name="max">最大数</param>
    /// <param name="min">最小数</param>
    /// <returns></returns>
    public static float GetRandom_Normal(float max, float min)
    {
        float[] dou = new float[100];
        for (int i = 0; i < dou.Length; i++)
        {
            dou[i] = RandomBuilder.RandomNum(max, min);
        }

        return Random_Normal(Ave(dou), SIGMA, min, max);
    }


    /// <summary>
    /// 产生(min,max)之间均匀分布的随机数
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private static float AverageRandom(float min, float max)
    {
        return RandomBuilder.RandomNum((float)max, (float)min);
        //int MINnteger = (int)(min * 1000);
        //int MAXnteger = (int)(max * 1000);
        ////
        //int resultInteger = aa.Next(MINnteger, MAXnteger);
        //return resultInteger / 1000.0f;
    }
    /// <summary>
    /// 正态分布概率密度函数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="miu">平均数</param>
    /// <param name="sigma">标准差</param>
    /// <returns></returns>
    private static float Normal(float x, float miu, float sigma)
    {
        return (float)(1.0 / (x * Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * (Math.Log(x) - miu) * (Math.Log(x) - miu) / (2 * sigma * sigma)));
    }
    /// <summary>
    /// 产生正态分布随机数
    /// </summary>
    /// <param name="miu">平均数</param>
    /// <param name="sigma">标准差</param>
    /// <param name="min">最大值</param>
    /// <param name="max">最小值</param>
    /// <returns></returns>
    private static float Random_Normal(float miu, float sigma, float min, float max)
    {
        float x;
        float dScope;
        float y;
        do
        {
            x = AverageRandom(min, max);
            y = Normal(x, miu, sigma);
            dScope = AverageRandom(0, Normal(miu, miu, sigma));
        } while (dScope > y);
        return x;
    }

    //求随机数平均值方法
    private static float Ave(float[] a)
    {
        float sum = 0;
        foreach (float d in a)
        {
            sum = sum + d;
        }
        float ave = sum / a.Length;
        return ave;
    }

    const int SIGMA = 1;
    static Random aa = new Random((int)(DateTime.Now.Ticks / 10000));
}
