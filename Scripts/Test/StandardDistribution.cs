  using System;
  using System.Collections.Generic;
  using System.Linq;
  using UnityEngine;
  using Random = System.Random;

/// <summary>
    /// 提供正态分布的数据和图片
    /// </summary>
    public class StandardDistribution
    {

    static void Main(string[] args)
    {
        double E = 10;//正态分布期望
        double D = 25;//正态分布方差
        double[] ZTFBArr = new double[1000];
        Random u1 = new Random();
        Random u2 = new Random();
        for (int i = 0; i < 1000; i++)
        {
            double? result = GetZTFB(u1.NextDouble(), u2.NextDouble(), E, D);
            if (result != null)
                ZTFBArr[i] = (double)result;
        }
        Console.WriteLine(ZTFBArr.Length);

        for (int i = 0; i < ZTFBArr.Length; i++)
        {
            Console.WriteLine(ZTFBArr[i]);
        }

        double teste = GetE(ZTFBArr);
        double testd = GetD(ZTFBArr, teste);

        Console.WriteLine(string.Format("计算期望:{0} 生成期望:{1}", E, teste));
        Console.WriteLine(string.Format("计算方差:{0} 生成方差:{1}", D, testd));

    }
    /// <summary>
    /// 计算数组期望值
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    private static double GetE(double[] arr)
    {
        double teste;//测试方差
        double sumresult = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            sumresult += arr[i];
        }
        teste = sumresult / arr.Length;
        return teste;
    }
    /// <summary>
    /// 计算数组的方差
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="teste">期望值</param>
    /// <returns></returns>
    private static double GetD(double[] arr, double teste)
    {
        double sumd = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            sumd += Math.Pow(arr[i] - teste, 2);
        }
        return sumd / arr.Length;
    }
    /// <summary>
    /// 返回正态分布的值
    /// </summary>
    /// <param name="u1">第一个均匀分布值</param>
    /// <param name="u2">第二个均匀分布值</param>
    /// <param name="e">正态期望</param>
    /// <param name="d">正态方差</param>
    /// <returns>分布值或者null</returns>
    private static double? GetZTFB(double u1, double u2, double e, double d)
    {
        double? result = null;
        try
        {
            result = e + Math.Sqrt(d) * Math.Sqrt((-2) * (Math.Log(u1) / Math.Log(Math.E))) * Math.Cos(2 * Math.PI * u2);
        }
        catch (Exception ex)
        {
            result = null;
        }
        return result;
    }

       
        /// <summary>
        /// 样本数据
        /// </summary>
        public List<double> Xs { get; private set; }

        public StandardDistribution(List<double> Xs)
        {
            this.Xs = Xs;

            Average = Xs.Average();
            Variance = GetVariance(Xs);

            if (Variance == 0) throw new Exception("方差为0");//此时不需要统计 因为每个样本数据都相同，可以在界面做相应提示

            StandardVariance = Math.Sqrt(Variance);
        }

        /// <summary>
        /// 方差/标准方差的平方
        /// </summary>
        public double Variance { get; private set; }

        /// <summary>
        /// 标准方差
        /// </summary>
        public double StandardVariance { get; private set; }

        /// <summary>
        /// 算数平均值/数学期望
        /// </summary>
        public double Average { get; private set; }

        /// <summary>
        /// 1/ (2π的平方根)的值
        /// </summary>
        public static double InverseSqrt2PI = 1 / Math.Sqrt(2 * Math.PI);

        /// <summary>
        /// 获取指定X值的Y值  计算正太分布的公式
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double GetGaussianDistributionY(double x)
        {
            double PowOfE = -(Math.Pow(Math.Abs(x - Average), 2) / (2 * Variance));

            double result = (StandardDistribution.InverseSqrt2PI / StandardVariance) * Math.Pow(Math.E, PowOfE);

            return result;
        }


        /// <summary>
        /// 获取整型列表的方差
        /// </summary>
        /// <param name="src">要计算方差的数据列表</param>
        /// <returns></returns>
        public static double GetVariance(List<double> src)
        {
            double average = src.Average();
            double SumOfSquares = 0;
            src.ForEach(x => { SumOfSquares += Math.Pow(x - average, 2); });
            return SumOfSquares / src.Count;//方差
        }

        /// <summary>
        /// 获取整型列表的方差
        /// </summary>
        /// <param name="src">要计算方差的数据列表</param>
        /// <returns></returns>
        public static float GetVariance(List<float> src)
        {
            float average = src.Average();
            double SumOfSquares = 0;
            src.ForEach(x => { SumOfSquares += Math.Pow(x - average, 2); });
            return (float)SumOfSquares / src.Count;//方差
        }
    }