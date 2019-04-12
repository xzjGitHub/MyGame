using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;


/// <summary>
/// 随机种子生成器
/// </summary>
public class RandomBuilder
{

    public const float RandomMaxFactor = 10000;

    public static RandomBuilder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RandomBuilder();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 获得随机种子
    /// </summary>
    /// <returns></returns>
    public Random Random
    {
        get
        {
            if (randoms.Count == 0)
            {
                randoms.Clear();
                tick = DateTime.Now.Ticks;
                randoms.Add(new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32)));
            }
            ////移除之前用过的
            //if (randoms.Count > 0)
            //{
            //    randoms.Remove(randoms[0]);
            //}
            ////检查是否为空
            //if (randoms.Count != 0) return randoms[0];
            //int iSeed = 10;
            //for (int i = 0; i < randomMaxSum; i++)
            //{
            //    long tick = DateTime.Now.Ticks;
            //    for (int j = 0; j < 100; j++)
            //    {
            //        new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            //    }
            //    randoms.Add(new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32)
            //        /*(int)DateTime.Now.Ticks + (int)Math.Pow((i + 1) * 1234, 3)*/));
            //}
            //直接给第一个
            return randoms[0];
        }
    }


    /// <summary>
    /// 获得随机数
    /// </summary>
    /// <returns></returns>
    public static float RandomNum(List<float> ranges)
    {
        return RandomNum(ranges[0], ranges[1]);
    }
    /// <summary>
    /// 获得随机数
    /// </summary>
    /// <returns></returns>
    public static int RandomNum(List<int> ranges)
    {
        return RandomNum(ranges[0], ranges[1]);
    }

    /// <summary>
    /// 获得随机数
    /// </summary>
    /// <returns></returns>
    public static float RandomNum(float[] ranges)
    {
        return RandomNum(ranges[0], ranges[1]);
    }
    /// <summary>
    /// 获得随机数
    /// </summary>
    /// <returns></returns>
    public static float RandomNum(int[] ranges)
    {
        return RandomNum(ranges[0], ranges[1]);
    }

    /// <summary>
    /// 获得随机数
    /// </summary>
    /// <param name="minNum">最小数</param>
    /// <param name="maxNum">最大数</param>
    /// <returns></returns>
    public static int RandomNum(int maxNum, int minNum = 0)
    {
        return UnityEngine.Random.Range(maxNum < minNum ? maxNum : minNum, (maxNum < minNum ? minNum + 1 : maxNum + 1));
        //  return maxNum < minNum ? 0 : Instance.Random.Next(minNum, maxNum);
    }

    /// <summary>
    /// 获得随机数
    /// </summary>
    /// <param name="minNum">最小数</param>
    /// <param name="maxNum">最大数</param>
    /// <returns></returns>
    public static float RandomNum(float maxNum, float minNum = 0)
    {
        return UnityEngine.Random.Range(maxNum < minNum ? maxNum : minNum, (maxNum < minNum ? minNum : maxNum));
    }

    /// <summary>
    /// 列表中随机取max-min随机个数
    /// </summary>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <param name="lists">列表</param>
    /// <returns></returns>
    public static List<T> RandomCount_Chances<T>(int min = 0, int max = 1, List<T> lists = null)
    {
        if (lists == null)
        {
            return null;
        }
        //
        return RandomList(RandomNum(max, min), lists);
    }

    /// <summary>
    /// 列表中随机取sum个
    /// </summary>
    /// <param name="sum">数量</param>
    /// <param name="lists">列表</param>
    /// <returns></returns>
    public static List<T> RandomList<T>(int sum, List<T> lists = null)
    {
        //为空
        if (lists == null || lists.Count == 0)
        {
            return null;
        }
        //需要数量大于或和列表总数相等
        if (sum >= lists.Count)
        {
            return lists;
        }
        //备份原始类别
        List<T> temp = Copylist(lists);
        //新建列表
        List<T> sums = new List<T>();
        try
        {
            int _index;
            //得到随机出来的列表
            for (int i = 0; i < sum; i++)
            {
                _index = RandomNum(temp.Count - 1);
                sums.Add(temp[_index]);
                temp.RemoveAt(_index);
            }
            return sums;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// 概率是否选中__chance为概率___-1未选中
    /// </summary>
    /// <param name="maxNum">最大数</param>
    /// <param name="chancesList">概率列表</param>
    /// <returns>返回得到的索引</returns>
    public static int RandomIndex_Chances(float chance, float maxNum = RandomMaxFactor)
    {
        return RandomIndex_Chances(new List<float> { chance, maxNum - chance }, maxNum);
    }
    /// <summary>
    /// 概率列表中取随机Index_-1未选中
    /// </summary>
    /// <param name="maxNum">最大数</param>
    /// <param name="chancesList">概率列表</param>
    /// <returns>返回得到的索引</returns>
    public static int RandomIndex_Chances(int chance, float maxNum = RandomMaxFactor)
    {
        return RandomIndex_Chances(new List<float> { chance, maxNum - chance }, maxNum);
    }

    /// <summary>
    /// 概率列表中取随机索引__chancesList为几率列表___-1未选中
    /// </summary>
    /// <param name="maxNum">最大数</param>
    /// <param name="chancesList">概率列表</param>
    /// <returns>返回得到的索引</returns>
    public static int RandomIndex_Chances(List<float> chancesList, float maxNum = RandomMaxFactor)
    {
        if (chancesList == null || chancesList.Count == 0)
        {
            return -1;
        }
        try
        {
            //得到最大值
            float _maxNum =/* chancesList.Count != 1 ? values.Sum() : (float)*/maxNum;
            //得到随机值
            float _num = RandomNum(_maxNum);
            if (chancesList.Count == 1)
            {
                return _num <= chancesList[0] ? 0 : -1;
            }
            //新建列表
            List<float> _temps = new List<float>
            {
                chancesList[0]
            };
            for (int i = 1; i < chancesList.Count; i++)
            {
                _temps.Add(_temps[i - 1] + chancesList[i]);
            }
            //判断
            for (int i = 0; i < _temps.Count; i++)
            {
                if (_num > _temps[i])
                {
                    continue;
                }

                return i;
            }
            //都没有选中
            return -1;
        }
        catch (Exception) { return -1; }
    }

    /// <summary>
    ///  概率列表中取随机索引_chancesList为几率列表___-1未选中
    /// </summary>
    /// <param name="maxNum">最大数</param>
    /// <param name="chancesList">概率列表</param>
    /// <returns>返回得到的索引</returns>
    public static int RandomIndex_Chances(List<int> chancesList, float maxNum = RandomMaxFactor)
    {
        List<float> _list = chancesList.Select(item => (float)item).ToList();
        return RandomIndex_Chances(_list, maxNum);
    }
    /// <summary>
    /// 随机索引_chancesList为几率列表
    /// </summary>
    /// <param name="chancesList"></param>
    /// <param name="sum"></param>
    /// <param name="maxNum"></param>
    /// <returns></returns>
    public static List<int> RandomIndexs(List<int> chancesList, int sum = 1, float maxNum = RandomMaxFactor)
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < sum; i++)
        {
            temp.Add(RandomIndex_Chances(chancesList, maxNum));
        }
        return temp;
    }
    /// <summary>
    /// 随机索引_chancesList为几率列表
    /// </summary>
    /// <param name="chancesList"></param>
    /// <param name="sum"></param>
    /// <param name="maxNum"></param>
    /// <returns></returns>
    public static List<int> RandomIndexs(List<float> chancesList, int sum = 1, float maxNum = RandomMaxFactor)
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < sum; i++)
        {
            temp.Add(RandomIndex_Chances(chancesList, maxNum));
        }
        return temp;
    }

    /// <summary>
    ///  列表中取随机sum个
    /// </summary>
    /// <param name="sum">个数</param>
    /// <param name="values">值列表</param>
    /// <returns>返回得到的索引</returns>
    public static List<float> RandomValues(List<float> values, int sum)
    {
        List<float> temp = new List<float>();
        if (values.Count < sum)
        {
            sum = values.Count;
        }

        for (int i = 0; i < sum; i++)
        {
            List<float> _list = values.Select(item => item).ToList();
            int _index = RandomNum(_list.Count - 1);
            temp.Add(values[_index]);
            values.RemoveAt(_index);
        }
        return temp;
    }
    /// <summary>
    ///  列表中取随机sum个
    /// </summary>
    /// <param name="sum">个数</param>
    /// <param name="chancesList">概率列表</param>
    /// <returns>返回得到的索引</returns>
    public static List<int> RandomValues(List<int> chancesList, int sum)
    {
        return RandomValues(chancesList.Select(item => (float)item).ToList(), sum).Select(a => (int)a).ToList();
    }

    #region 正态分布随机数

    /// <summary>
    /// 获得正态分布随机数
    /// </summary>
    /// <param name="max">最大数</param>
    /// <param name="min">最小数</param>
    /// <returns></returns>
    public static float Random_Normal(float max, float min)
    {
        return Random_Normal(new float[] { min, max });
        //  return Random_Normal(Ave(dou), SIGMA, min, max);
    }
    /// <summary>
    /// 获得正态分布随机数
    /// </summary>
    /// <param name="max">最大数</param>
    /// <param name="min">最小数</param>
    /// <returns></returns>
    public static float Random_Normal(float[] ranges)
    {
        return Random_Normal(ranges, 1)[0];
        //  return Random_Normal(Ave(dou), SIGMA, min, max);
    }
    /// <summary>
    /// 获得正态分布随机数
    /// </summary>
    /// <param name="max">最大数</param>
    /// <param name="min">最小数</param>
    /// <returns></returns>
    public static List<float> Random_Normal(float max, float min, int sum)
    {
        return Random_Normal(new float[] { min, max }, sum);
        //  return Random_Normal(Ave(dou), SIGMA, min, max);
    }

    /// <summary>
    /// 获得正态分布随机数
    /// </summary>
    /// <param name="max">最大数</param>
    /// <param name="min">最小数</param>
    /// <returns></returns>
    public static List<float> Random_Normal(float[] ranges, int sum = 1)
    {
        float temp;
        //格式化范围------左边小于右边
        if (ranges[0] > ranges[1])
        {
            temp = ranges[0];
            ranges[0] = ranges[1];
            ranges[1] = temp;
        }
        //新建对称范围
        float max = Math.Abs(ranges[0]) > Math.Abs(ranges[1]) ? Math.Abs(ranges[0]) : Math.Abs(ranges[1]);
        float[] newRanges = { -max, max };
        List<float> randoms = Randoms(newRanges, 10000);
        List<float> values = new List<float>();
        int phase = 0;
        float ave = Ave(randoms);
        float stdDev = CalculateStdDev(randoms);
        for (int i = 0; i < sum; i++)
        {
            do
            {
                temp = Get_Normal(ave, stdDev, ref phase);
            } while (temp < ranges[0] || temp > ranges[1]); //清除范围以外的数
            values.Add(temp);
        }

        return values;
        //  return Random_Normal(Ave(dou), SIGMA, min, max);
    }

    #endregion


    /// <summary>
    /// 获得正态分布随机数
    /// </summary>
    /// <param name="max">最大数</param>
    /// <param name="min">最小数</param>
    /// <returns></returns>
    public static List<float> Randoms(float max, float min, int sum = 10000)
    {
        float temp = max;
        if (temp < min)
        {
            max = min;
            min = temp;
        }
        List<float> dou = new List<float>();
        for (int i = 0; i < sum; i++)
        {
            dou.Add(RandomNum(max, min));
        }

        return dou;
        //  return Random_Normal(Ave(dou), SIGMA, min, max);
    }

    /// <summary>
    /// 获得正态分布随机数
    /// </summary>
    /// <param name="max">最大数</param>
    /// <param name="min">最小数</param>
    /// <returns></returns>
    public static List<float> Randoms(float[] ranges, int sum = 10000)
    {
        List<float> dou = new List<float>();
        for (int i = 0; i < sum; i++)
        {
            dou.Add(RandomNum(ranges[0], ranges[1]));
        }

        return dou;
        //  return Random_Normal(Ave(dou), SIGMA, min, max);
    }


    private static float GetRandom(int[] a, int[] b)
    {
        int min = Math.Max(a[0], b[0]);
        int max = Math.Min(a[1], b[1]);
        float temp = max - min;
        float aProb = temp / (a[1] - a[0]);
        float bProb = temp / (b[1] - b[0]);
        float prob = aProb * bProb;
        //    float prob = 1f / (max - min);
        //     prob = aProb * aProb;
        return prob * 100;
    }


    /// <summary>
    /// 选择随机次数
    /// </summary>
    /// <param name="maxCount">最大次数</param>
    /// <param name="selectChance">选中概率</param>
    /// <returns></returns>
    public static int SelectRandomCount(int maxCount, float selectChance, float maxChance = 10000)
    {
        if (maxCount == 0)
        {
            return 0;
        }
        int count = 0;
        while (count <= maxCount)
        {
            if (RandomIndex_Chances(new List<float> { selectChance, maxChance - selectChance }) != 0)
            {
                break;
            }

            count++;
        }
        return count;
    }

    /// <summary>
    /// 复制列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    private static List<T> Copylist<T>(List<T> list)
    {
        if (list == null)
        {
            return null;
        }

        List<T> _temp = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            T item = list[i];
            _temp.Add(item);
        }
        return _temp;
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
            miu = 0;
            x = AverageRandom(min, max);
            y = Normal(x, miu, sigma);
            dScope = AverageRandom(0, Normal(miu, miu, sigma));
        } while (dScope > y);
        return x;
    }



    /// <summary>
    /// 平均值标准差
    /// </summary>
    /// <param name="ave"></param>
    /// <param name="stdDev"></param>
    /// <returns></returns>
    private static float Get_Normal(float ave, float stdDev, ref int phase)
    {
        float r, u1, u2;

        u1 = RandomNum(0f, 1f);
        u2 = RandomNum(0f, 1f);
        if (phase == 0)
        {

            //  r = (float)(Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2));
            r = (float)(Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2));
        }
        else
        {
            //  r = (float)(Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2));
            r = (float)(Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2));
        }
        phase = 1 - phase;
        //  r = (float)(ave + stdDev * Math.Sqrt(-2 * Math.Log(u1/*, Math.E*/)) * Math.Cos(2 * Math.PI * u2));
        r = r * stdDev + ave;

        return (int)(r * 100) / 100f;
    }

    /// <summary>
    /// 产生(min,max)之间均匀分布的随机数
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private static float AverageRandom(float min, float max)
    {
        return RandomNum(max, min);
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
        return (float)(1.0 / (Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * (x - miu) * (x - miu) / (2 * sigma * sigma)));
        // return (float)(1.0 / (x * Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * (Math.Log(x) - miu) * (Math.Log(x) - miu) / (2* sigma * sigma)));
    }
    /// <summary>
    /// 求随机数平均值方法
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static float Ave(List<float> a)
    {
        float sum = 0;
        foreach (float d in a)
        {
            sum = sum + d;
        }
        float ave = sum / a.Count;
        return ave;
    }

    /// <summary>
    /// 计算平均数
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static float CalculateStdDev(List<float> values)
    {
        float ret = 0;
        if (values.Count > 0)
        {
            //  计算平均数   
            double avg = values.Average();
            //  计算各数值与平均数的差值的平方，然后求和 
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            //  除以数量，然后开方
            ret = (float)Math.Sqrt(sum / values.Count());
        }
        return ret;
    }

    //利用Guid产生随机数
    private static int GetRandomNumber(int min, int max)
    {
        int rtn = 0;
        Random r = new Random();
        byte[] buffer = Guid.NewGuid().ToByteArray();
        int iSeed = BitConverter.ToInt32(buffer, 0);
        r = new Random(iSeed);
        rtn = r.Next(min, max + 1);
        return rtn;
    }

    //min,max为生成数组元素的大小区间，len为数组长度，sum为数组之和
    public static int[] GetArr(int min, int max, int len, int sum)
    {
        int[] arr = new int[len];
        for (int i = 0; i < len; i++)
        {
            arr[i] = RandomNum(min, max);
        }
        int dif = arr.Sum() - sum;
        if (dif != 0)
        {
            int index, v, d = dif > 0 ? -1 : 1;
            while (true)
            {
                index = RandomNum(0, len - 1);
                v = arr[index] + d;
                if (v >= min && v <= max)
                {
                    arr[index] = v;
                    if (arr.Sum() == sum)
                    {
                        break;
                    }
                }
            }
        }
        return arr;
    }


    //
    private const int SIGMA = 1;
    private static readonly Random aa = new Random((int)(DateTime.Now.Ticks / 10000));
    private const int randomMaxSum = 5;
    private List<Random> randoms = new List<Random>();
    private static RandomBuilder _instance;
    private long tick;
}

