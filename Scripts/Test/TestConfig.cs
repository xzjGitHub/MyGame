﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;



public class TestConfig : MonoBehaviour
{

    public int Sum = 6;

    //求随机数平均值方法
    private double Ave(double[] a)
    {
        double sum = 0;
        foreach (double d in a)
        {
            sum = sum + d;
        }
        double ave = sum / a.Length;
        return ave;
    }

    //求随机数方差方法
    private double Var(double[] v)
    {
        double sum1 = 0;
        for (int i = 0; i < v.Length; i++)
        {
            double temp = v[i] * v[i];
            sum1 = sum1 + temp;
        }
        double sum = 0;
        foreach (double d in v)
        {
            sum = sum + d;
        }
        double var = sum1 / v.Length - (sum / v.Length) * (sum / v.Length);
        return var;
    }








    void Start()
    {

    }

    public bool isStart = false;
    public float minNum = 0;
    public float maxNum = 100;

    private void Update()
    {
        if (!isStart) return;
        StartRandom_Normal();
    }

    private void StartRandom_Normal()
    {
        isStart = false;
        test1(aa);
    }



    void Awake()
    {
        //   ConfigManager.Instance.DefaultConfigs = new List<Type>() { typeof(ErrorCodeConfig), typeof(Error) };
        //ConfigManager.Instance.Init(delegate (int a, int b) { LogHelperLSK.Log("Progress: " + a + "     " + b); }, delegate () { LogHelperLSK.Log("Finshed"); });
    }

    void test1(Random ran)
    {
        double[] dou = new double[100];
        for (int i = 0; i < dou.Length; i++)
        {
            dou[i] = ran.Next((int)MIN, (int)MAX);
        }
        double averesult = Ave(dou);
        double varresult = Var(dou);
        //

        List<double> list = new List<double>();
        for (int i = 0; i < Sum; i++)
        {
            list.Add(Random_Normal(averesult, SIGMA, minNum, maxNum));
        }
        string _str = string.Empty;
        foreach (var item in list)
        {
            _str += item + "\n";
        }
        LogHelperLSK.Log(_str);
    }

    /// <summary>
    /// 正态分布随机数
    /// </summary>
    const int N = 100;

    const int MAX = 50;
    const double MIN = 0.1;
    const int MIU = 40;
    const int SIGMA = 1;
    static Random aa = new Random((int)(DateTime.Now.Ticks / 10000));

    /// <summary>
    /// 产生(min,max)之间均匀分布的随机数
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private double AverageRandom(double min, double max)
    {
        int MINnteger = (int)(min * 1000);
        int MAXnteger = (int)(max * 1000);
        try
        {
            int resultInteger = aa.Next(MINnteger, MAXnteger);
            return resultInteger / 1000.0;
        }
        catch (Exception e)
        {
            LogHelperLSK.LogError(MINnteger + "   " + MAXnteger);
            return 0;
        }
    }
    /// <summary>
    /// 正态分布概率密度函数
    /// </summary>
    /// <param name="x"></param>
    /// <param name="miu">平均数</param>
    /// <param name="sigma">标准差</param>
    /// <returns></returns>
    private double Normal(double x, double miu, double sigma)
    {
        return 1.0 / (x * Math.Sqrt(2 * Math.PI) * sigma) * Math.Exp(-1 * (Math.Log(x) - miu) * (Math.Log(x) - miu) / (2 * sigma * sigma));
    }
    /// <summary>
    /// 产生正态分布随机数
    /// </summary>
    /// <param name="miu">平均数</param>
    /// <param name="sigma">标准差</param>
    /// <param name="min">最大值</param>
    /// <param name="max">最小值</param>
    /// <returns></returns>
    private double Random_Normal(double miu, double sigma, double min, double max)
    {
        double x;
        double dScope;
        double y;
        do
        {
            x = AverageRandom(min, max);
            y = Normal(x, miu, sigma);
            dScope = AverageRandom(0, Normal(miu, miu, sigma));
        } while (dScope > y);
        return x;
    }


}



