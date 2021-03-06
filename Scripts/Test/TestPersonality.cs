﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

public class TestPersonality : MonoBehaviour
{

    private void Start()
    {
        new CoroutineUtil(Test1());
        //new CoroutineUtil(Test2());
        //new CoroutineUtil(Test3());
        //new CoroutineUtil(Test4());
        //new CoroutineUtil(Test5());
        //new CoroutineUtil(PrintLog1());
        //new CoroutineUtil(PrintLog2());
        //new CoroutineUtil(PrintLog3());
        //new CoroutineUtil(LogShiftText());
    }

    /// <summary>
    /// 第一步
    /// </summary>
    /// <returns></returns>
    public IEnumerator Test1()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        List<PassiveSkill_template> tempsList = PassiveSkill_templateConfig.GetPassiveSkill_Templates();
        for (int i = 0; i < tempsList.Count; i++)
        {
            for (int j = 0; j < tempsList.Count; j++)
            {
                for (int k = 0; k < tempsList.Count; k++)
                {
                    for (int l = 0; l < tempsList.Count; l++)
                    {

                        TestTeam combatTeamInfo = new TestTeam();
                        List<TestChar> combatUnits = combatTeamInfo.testChars;
                        combatTeamInfo.testChars.Add(new TestChar(0, tempsList[i].passiveSkillID));
                        combatTeamInfo.testChars.Add(new TestChar(1, tempsList[j].passiveSkillID));
                        combatTeamInfo.testChars.Add(new TestChar(2, tempsList[k].passiveSkillID));
                        combatTeamInfo.testChars.Add(new TestChar(3, tempsList[l].passiveSkillID));
                        combatTeamInfo.PersonalityTake();
                        //是否可用 总激励数>=4，且角色激励数>=2的角色>=1
                        if (combatTeamInfo.FinalEncourageSum >= 4 && combatTeamInfo.GetMoreThenValueSum(1) >= 1)
                        {
                            usableTeams.Add(combatTeamInfo);
                        }
                        //总激励数>4的为异常组合 或 1名角色的激励数>2的为异常组合
                        if (combatTeamInfo.FinalEncourageSum > 4 || combatTeamInfo.GetMoreThenValueSum(2) > 0)
                        {
                            aberrantTeams.Add(combatTeamInfo);
                        }
                    }
                }
                yield return null;
            }
            LogHelper_MC.Log(i);

        }
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError("运行的分钟=" + timespan.TotalMinutes);
        //
        LogHelper_MC.LogError("usableTeams的总数" + usableTeams.Count);
        LogHelper_MC.LogError("aberrantTeams的总数" + aberrantTeams.Count);

        int sum = usableTeams.Count;
        int index = 0;
        while (sum > 0)
        {
            int tempNum = usableTeams.Count >= 1000 ? 1000 : usableTeams.Count;
            List<TestTeam> temps = new List<TestTeam>();
            for (int i = 0; i < tempNum; i++)
            {
                temps.Add(usableTeams[i]);
            }
            for (int i = tempNum - 1; i >= 0; i--)
            {
                usableTeams.RemoveAt(i);
            }
            GameDataManager.SaveCombatTestData<TestData>(new TestData(temps), Testphase1, UsableTeamName + index);
            yield return null;
            sum = usableTeams.Count;
            index++;
        }
        GameDataManager.SaveCombatTestData<TestData>(new TestData(aberrantTeams), AberrantTeamName);
    }
    /// <summary>
    /// 第二步
    /// </summary>
    /// <returns></returns>
    public IEnumerator Test2()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        usableTeams.Clear();
        for (int i = 0; i < 108; i++)
        {
            usableTeams.AddRange((GameDataManager.GetCombatTestData<TestData>(Testphase1 + UsableTeamName + i) as TestData).teams);
        }
        yield return null;
        int sum = 0;
        //操作正常组合
        //2.1 将所有合法组合中的第1名角色删除，再次检测：
        for (int i = 0; i < usableTeams.Count; i++)
        {
            combatTeamInfo = usableTeams[i];
            //如果删除第1名角色后，总激励数>=3，且角色激励数>=2的角色>=1
            int temp = combatTeamInfo.FinalEncourageSum - combatTeamInfo.testChars[0].finalEncourage;
            sum = combatTeamInfo.GetMoreThenValueSum(1);
            if (combatTeamInfo.testChars[0].FinalEncourage > 1)
            {
                sum--;
            }
            if (temp >= 3 && sum >= 1)
            {
                //删除第一个
                combatTeamInfo.testChars.RemoveAt(0);
                //重置索引
                for (int j = 0; j < combatTeamInfo.testChars.Count; j++)
                {
                    combatTeamInfo.testChars[j].initIndex = j;
                }
            }
        }
        //2.2 清除所有完全重复数据，仅保留1条（删除ID组合相同，且排列顺序相同的数据）
        bool isHaveSame = true;
        int nowIndex = 0;
        while (isHaveSame)
        {
            //最后一个索引退出去
            if (nowIndex > usableTeams.Count - 1)
            {
                isHaveSame = false;
                break;
            }
            List<int> sameIndexs = GetSameIndexs(nowIndex, usableTeams);
            //移出相同的
            if (sameIndexs.Count > 0)
            {
                foreach (int item in sameIndexs)
                {
                    usableTeams.RemoveAt(item);
                }
            }
            nowIndex++;
        }
        //
        sum = usableTeams.Count;
        int index = 0;
        while (sum > 0)
        {
            int tempNum = usableTeams.Count >= 1000 ? 1000 : usableTeams.Count;
            List<TestTeam> temps = new List<TestTeam>();
            for (int i = 0; i < tempNum; i++)
            {
                temps.Add(usableTeams[i]);
            }
            for (int i = tempNum - 1; i >= 0; i--)
            {
                usableTeams.RemoveAt(i);
            }
            GameDataManager.SaveCombatTestData<TestData>(new TestData(temps), Testphase2, UsableTeamName + index);
            yield return null;
            sum = usableTeams.Count;
            index++;
        }
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        //   time = timespan.TotalMilliseconds;  //  总毫秒数
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }
    /// <summary>
    /// 第三部
    /// </summary>
    /// <returns></returns>
    public IEnumerator Test3()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        usableTeams.Clear();
        for (int i = 0; i < 68; i++)
        {
            usableTeams.AddRange((GameDataManager.GetCombatTestData<TestData>(Testphase2 + UsableTeamName + i) as TestData).teams);
        }
        yield return null;
        int sum = 0;
        //3.1 将所有合法组合中的最后1名角色删除，再次检测：（针对3人的组合，删除第3名角色；针对4人组合，删除第4名角色）
        for (int i = 0; i < usableTeams.Count; i++)
        {
            combatTeamInfo = usableTeams[i];
            //如果删除最后1名角色后，总激励数 >= 总角色数，且角色激励数>=2的角色>=1（因为组合中的角色可能有4、3或2个，所以只能根据总角色数检测）
            int lastIndex = combatTeamInfo.testChars.Count - 1;
            int temp = combatTeamInfo.FinalEncourageSum - combatTeamInfo.testChars[lastIndex].FinalEncourage;
            sum = combatTeamInfo.GetMoreThenValueSum(1);
            if (combatTeamInfo.testChars[lastIndex].FinalEncourage > 1)
            {
                sum--;
            }
            if (temp >= combatTeamInfo.testChars.Count && sum >= 1)
            {
                //删除第一个
                combatTeamInfo.testChars.RemoveAt(lastIndex);
            }
        }
        //3.2 清除所有完全重复数据，仅保留1条
        bool isHaveSame = true;
        int nowIndex = 0;
        while (isHaveSame)
        {
            //最后一个索引退出去
            if (nowIndex > usableTeams.Count - 1)
            {
                isHaveSame = false;
                break;
            }
            List<int> sameIndexs = GetSameIndexs(nowIndex, usableTeams);
            //移出相同的
            if (sameIndexs.Count > 0)
            {
                foreach (int item in sameIndexs)
                {
                    usableTeams.RemoveAt(item);
                }
            }
            nowIndex++;
        }
        //
        sum = usableTeams.Count;
        int index = 0;
        while (sum > 0)
        {
            int tempNum = usableTeams.Count >= 1000 ? 1000 : usableTeams.Count;
            List<TestTeam> temps = new List<TestTeam>();
            for (int i = 0; i < tempNum; i++)
            {
                temps.Add(usableTeams[i]);
            }
            for (int i = tempNum - 1; i >= 0; i--)
            {
                usableTeams.RemoveAt(i);
            }
            GameDataManager.SaveCombatTestData<TestData>(new TestData(temps), Testphase3, UsableTeamName + index);
            yield return null;
            sum = usableTeams.Count;
            index++;
        }

        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }
    /// <summary>
    /// 第四步
    /// </summary>
    /// <returns></returns>
    public IEnumerator Test4()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        usableTeams.Clear();
        for (int i = 0; i < 46; i++)
        {
            usableTeams.AddRange((GameDataManager.GetCombatTestData<TestData>(Testphase3 + UsableTeamName + i) as TestData).teams);
        }
        yield return null;
        //4.1 清理“可能重复”
        //如果多个组合中，组成的被动ID相同
        bool isHaveSame = true;
        int nowIndex = 0;
        int sum = 0;
        while (isHaveSame)
        {
            //最后一个索引退出去
            if (nowIndex > usableTeams.Count - 1)
            {
                isHaveSame = false;
                break;
            }
            List<int> sameIndexs = GetSameIDIndexs(nowIndex, usableTeams);
            //移出相同的
            if (sameIndexs.Count > 0)
            {
                foreach (int item in sameIndexs)
                {
                    usableTeams.RemoveAt(item);
                }
            }
            nowIndex++;
        }
        //
        sum = usableTeams.Count;
        int index = 0;
        while (sum > 0)
        {
            int tempNum = usableTeams.Count >= 1000 ? 1000 : usableTeams.Count;
            List<TestTeam> temps = new List<TestTeam>();
            for (int i = 0; i < tempNum; i++)
            {
                temps.Add(usableTeams[i]);
            }
            for (int i = tempNum - 1; i >= 0; i--)
            {
                usableTeams.RemoveAt(i);
            }
            GameDataManager.SaveCombatTestData<TestData>(new TestData(temps), Testphase4, UsableTeamName + index);
            yield return null;
            sum = usableTeams.Count;
            index++;
        }
        //
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }
    public IEnumerator Test5()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        //log0
        usableTeams.Clear();
        for (int i = 0; i < 4; i++)
        {
            usableTeams.AddRange((GameDataManager.GetCombatTestData<TestData>(Testphase4 + UsableTeamName + i) as TestData).teams);
        }
        yield return null;
        Dictionary<int, List<TestTeam>> temps = new Dictionary<int, List<TestTeam>>();
        List<Personality_template> tempsList = Personality_templateConfig.GetTemplateAll();
        foreach (Personality_template template in tempsList)
        {
            int personalityID = template.personalityID;
            temps.Add(personalityID, new List<TestTeam>());
            //遍历每个组合
            foreach (TestTeam team in usableTeams)
            {
                foreach (TestChar testChar in team.testChars)
                {
                    if (testChar.personality == personalityID && team.FinalEncourageSum >= 2)
                    {
                        //添加该组合
                        temps[personalityID].Add(team);
                        break;
                    }
                }
            }
        }
        //打印日志
        List<int> keys = new List<int>();
        keys.AddRange(temps.Keys);
        int index = 0;
        Dictionary<int, List<TestTeam>> list = new Dictionary<int, List<TestTeam>>();
        for (int i = 0; i < keys.Count; i++)
        {
            list.Clear();
            index = 0;
            int key = keys[i];
            if (index == 0)
            {
                list.Add(key, new List<TestTeam>());
            }
            //
            foreach (TestTeam item in temps[key])
            {
                list[key].Add(item);
                index++;
            }
            GameDataManager.SaveCombatTestData(list, Testphase5, UsableTeamName + i);
        }
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }

    /// <summary>
    /// 输出日志1
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrintLog1()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        //log0
        usableTeams.Clear();
        for (int i = 0; i < 108; i++)
        {
            usableTeams.AddRange((GameDataManager.GetCombatTestData<TestData>(Testphase1 + UsableTeamName + i) as TestData).teams);
        }
        yield return null;
        GetPrintLog1(usableTeams, Log1Path);
        //log1
        aberrantTeams.Clear();
        aberrantTeams.AddRange((GameDataManager.GetCombatTestData<TestData>(AberrantTeamName) as TestData).teams);
        yield return null;
        //
        GetPrintLog2(aberrantTeams, Log2Path);
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }
    /// <summary>
    /// 输出日志2
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrintLog2()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        //log0
        usableTeams.Clear();
        for (int i = 0; i < 4; i++)
        {
            usableTeams.AddRange((GameDataManager.GetCombatTestData<TestData>(Testphase4 + UsableTeamName + i) as TestData).teams);
        }
        yield return null;
        GetPrintLog1(usableTeams, Log3Path);
        yield return null;
        GetPrintLog2(usableTeams, Log4Path);
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }
    /// <summary>
    /// 输出日志3
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrintLog3()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        //log0
        usableTeams.Clear();
        Dictionary<int, List<TestTeam>> temps = new Dictionary<int, List<TestTeam>>();
        for (int i = 0; i < 18; i++)
        {
            List<TestTeam> list = new List<TestTeam>();
            temps = GameDataManager.GetCombatTestData<Dictionary<int, List<TestTeam>>>(Testphase5 + UsableTeamName + i) as Dictionary<int, List<TestTeam>>;

            foreach (List<TestTeam> item in temps.Values)
            {
                list.AddRange(item);
            }
            GetPrintLog2(list, "personality" + temps.First().Key);
        }
        yield return null;
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }

    /// <summary>
    /// 日志转换文本
    /// </summary>
    /// <returns></returns>
    public IEnumerator LogShiftText()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); //  开始监视代码运行时间
        //定义存档路径
        string dirpath = Application.persistentDataPath + GameDataManager.LocalGameData;
        object info;
        for (int i = 1; i < 19; i++)
        {
            info = GameDataManager.GetCombatTestData<TestPrintLog2>(LogPath + "personality"+i) as TestPrintLog2;
            LogShiftTextInfo(info, dirpath + TextPath, "personality" + i, "personality" + i);
            yield return null;
        }
        yield break;
        //
        info = GameDataManager.GetCombatTestData<TestPrintLog1>(LogPath + Log1Path) as TestPrintLog1;
        LogShiftTextInfo(info, dirpath + TextPath, Log1Path, "1、合法组合记录---每种性格的使用次数");
        yield return null;
        info = GameDataManager.GetCombatTestData<TestPrintLog2>(LogPath + Log2Path) as TestPrintLog2;
        LogShiftTextInfo(info, dirpath + TextPath, Log2Path, "2、异常组合记录---每个组合每技能id和激励数量");
        yield return null;
        info = GameDataManager.GetCombatTestData<TestPrintLog1>(LogPath + Log3Path) as TestPrintLog1;
        LogShiftTextInfo(info, dirpath + TextPath, Log3Path, "3、合法组合最终记录---每个组合每技能id和激励数量");
        yield return null;
        info = GameDataManager.GetCombatTestData<TestPrintLog2>(LogPath + Log4Path) as TestPrintLog2;
        LogShiftTextInfo(info, dirpath + TextPath, Log4Path, "4、合法组合最终合计记录---每种性格的使用次数");
        yield return null;
        stopwatch.Stop(); //  停止监视
        TimeSpan timespan = stopwatch.Elapsed; //  获取当前实例测量得出的总时间
        LogHelper_MC.LogError(timespan.TotalMinutes);
    }

    /// <summary>
    /// 日志转换文本
    /// </summary>
    private void LogShiftTextInfo(object info, string filePath, string fileName, string nameStr)
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        filePath += nameStr + ".txt";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        //
        FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write);//创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);
        sw.WriteLine(nameStr);//开始换行 
        //
        if (info is TestPrintLog1)
        {
            sw.WriteLine("技能" + "\t" + "总数");//开始换行 
            foreach (KeyValuePair<int, int> item in (info as TestPrintLog1).skillInfos)
            {
                sw.WriteLine(item.Key + "\t" + item.Value);
            }
        }
        if (info is TestPrintLog2)
        {
            sw.WriteLine("技能1" + "\t" + "技能2" + "\t" + "技能3" + "\t" + "技能4" + "\t"
                + "激励1" + "\t" + "激励2" + "\t" + "激励3" + "\t" + "激励4");
            foreach (KeyValuePair<int, TestLogInfo2> item in (info as TestPrintLog2).logs)
            {
                TestLogInfo2 info2 = item.Value;
                sw.WriteLine(info2.skill1 + "\t" + info2.skill2 + "\t" + info2.skill3 + "\t" + info2.skill4 + "\t"
                  + info2.count1 + "\t" + info2.count2 + "\t" + info2.count3 + "\t" + info2.count4);
            }
        }
        //
        sw.Close();
        fs1.Close();
    }

    /// <summary>
    /// 获得日志1
    /// </summary>
    private void GetPrintLog1(List<TestTeam> testTeams, string logName)
    {
        TestPrintLog1 printLog1 = new TestPrintLog1();
        for (int i = 0; i < testTeams.Count; i++)
        {
            TestTeam testTeam = testTeams[i];
            for (int j = 0; j < testTeam.testChars.Count; j++)
            {
                TestChar testChar = testTeam.testChars[j];
                if (printLog1.skillInfos.ContainsKey(testChar.passiveSkill))
                {
                    continue;
                }
                printLog1.skillInfos.Add(testChar.passiveSkill, GetSkillSum(testTeams, testChar.passiveSkill));
            }
        }
        GameDataManager.SaveCombatTestData<TestPrintLog1>(printLog1, LogPath, logName);
    }
    /// <summary>
    /// 获得日志2
    /// </summary>
    private void GetPrintLog2(List<TestTeam> testTeams, string logName)
    {
        TestPrintLog2 printLog2 = new TestPrintLog2();
        for (int i = 0; i < testTeams.Count; i++)
        {
            TestTeam testTeam = testTeams[i];
            printLog2.logs.Add(i, new TestLogInfo2()
            {
                skill1 = testTeam.testChars.Count > 0 ? testTeam.testChars[0].passiveSkill : 0,
                skill2 = testTeam.testChars.Count > 1 ? testTeam.testChars[1].passiveSkill : 0,
                skill3 = testTeam.testChars.Count > 2 ? testTeam.testChars[2].passiveSkill : 0,
                skill4 = testTeam.testChars.Count > 3 ? testTeam.testChars[3].passiveSkill : 0,
                count1 = testTeam.testChars.Count > 0 ? testTeam.testChars[0].finalEncourage : 0,
                count2 = testTeam.testChars.Count > 1 ? testTeam.testChars[1].finalEncourage : 0,
                count3 = testTeam.testChars.Count > 2 ? testTeam.testChars[2].finalEncourage : 0,
                count4 = testTeam.testChars.Count > 3 ? testTeam.testChars[3].finalEncourage : 0,
            });
        }
        GameDataManager.SaveCombatTestData<TestPrintLog2>(printLog2, LogPath, logName);
    }
    /// <summary>
    /// 获得技能数量
    /// </summary>
    private int GetSkillSum(List<TestTeam> testTeams, int skillID)
    {
        int sum = 0;
        foreach (TestTeam testTeam in testTeams)
        {
            foreach (TestChar testChar in testTeam.testChars)
            {
                if (skillID == testChar.passiveSkill)
                {
                    sum++;
                }
            }
        }
        return sum;
    }
    /// <summary>
    /// 获得相同的
    /// </summary>
    private List<int> GetSameIndexs(int startIndex, List<CombatTeamInfo> lists)
    {
        CombatTeamInfo source = lists[startIndex];
        startIndex++;
        List<int> indexs = new List<int>();
        for (int i = startIndex; i < lists.Count; i++)
        {
            if (source.combatUnits.Count != lists[i].combatUnits.Count)
            {
                continue;
            }
            bool isSame = true;
            for (int j = 0; j < source.combatUnits.Count; j++)
            {
                if (source.combatUnits[j].PersonalityAddPassiveSkill != lists[i].combatUnits[j].PersonalityAddPassiveSkill)
                {
                    isSame = false;
                    break;
                }
            }
            if (isSame)
            {
                indexs.Add(i);
            }
        }
        return indexs.OrderByDescending(a => a).ToList();
    }
    /// <summary>
    /// 获得相同的
    /// </summary>
    private List<int> GetSameIndexs(int startIndex, List<TestTeam> lists)
    {
        TestTeam source = lists[startIndex];
        startIndex++;
        List<int> indexs = new List<int>();
        for (int i = startIndex; i < lists.Count; i++)
        {
            if (source.testChars.Count != lists[i].testChars.Count)
            {
                continue;
            }
            bool isSame = true;
            for (int j = 0; j < source.testChars.Count; j++)
            {
                if (source.testChars[j].PersonalityAddPassiveSkill != lists[i].testChars[j].PersonalityAddPassiveSkill)
                {
                    isSame = false;
                    break;
                }
            }
            if (isSame)
            {
                indexs.Add(i);
            }
        }
        return indexs.OrderByDescending(a => a).ToList();
    }
    /// <summary>
    /// 获得相同的
    /// </summary>
    private List<int> GetSameIDIndexs(int startIndex, List<CombatTeamInfo> lists)
    {
        CombatTeamInfo source = lists[startIndex];
        CombatTeamInfo target;
        startIndex++;
        List<int> indexs = new List<int>();
        for (int i = startIndex; i < lists.Count; i++)
        {
            target = lists[i];
            if (source.combatUnits.Count != target.combatUnits.Count)
            {
                continue;
            }
            bool isSame = true;
            List<int> temps = new List<int>();
            foreach (CombatUnit item in target.combatUnits)
            {
                temps.Add(item.PersonalityAddPassiveSkill);
            }
            for (int j = 0; j < source.combatUnits.Count; j++)
            {
                if (!temps.Contains(source.combatUnits[j].PersonalityAddPassiveSkill))
                {
                    isSame = false;
                    break;
                }
            }
            if (isSame)
            {
                indexs.Add(i);
            }
        }
        return indexs.OrderByDescending(a => a).ToList();
    }
    /// <summary>
    /// 获得相同的
    /// </summary>
    private List<int> GetSameIDIndexs(int startIndex, List<TestTeam> lists)
    {
        TestTeam source = lists[startIndex];
        TestTeam target;
        startIndex++;
        List<int> indexs = new List<int>();
        for (int i = startIndex; i < lists.Count; i++)
        {
            target = lists[i];
            if (source.testChars.Count != target.testChars.Count)
            {
                continue;
            }
            bool isSame = true;
            List<int> temps = new List<int>();
            foreach (TestChar item in target.testChars)
            {
                temps.Add(item.PersonalityAddPassiveSkill);
            }
            for (int j = 0; j < source.testChars.Count; j++)
            {
                if (!temps.Contains(source.testChars[j].PersonalityAddPassiveSkill))
                {
                    isSame = false;
                    break;
                }
            }
            if (isSame)
            {
                indexs.Add(i);
            }
        }
        return indexs.OrderByDescending(a => a).ToList();
    }

    //
    private TestTeam combatTeamInfo;

    private readonly List<TestTeam> combatUnits = new List<TestTeam>();

    private readonly List<TestTeam> combatTeamInfos = new List<TestTeam>();

    private List<TestTeam> usableTeams = new List<TestTeam>();

    private List<TestTeam> aberrantTeams = new List<TestTeam>();
    //
    private const string UsableTeamName = "usableTeam";
    private const string AberrantTeamName = "aberrantTeam";
    private const string Testphase1 = "Testphase1/";
    private const string Testphase2 = "Testphase2/";
    private const string Testphase3 = "Testphase3/";
    private const string Testphase4 = "Testphase4/";
    private const string Testphase5 = "Testphase5/";
    private const string LogPath = "Log/";
    private const string Log1Path = "Log1";
    private const string Log2Path = "Log2";
    private const string Log3Path = "Log3";
    private const string Log4Path = "Log4";
    private const string Log5Path = "Log5";
    private const string TextPath = "Text/";
}

public class TestData
{
    public TestData(List<TestTeam> teams)
    {
        this.teams = teams;
    }

    public List<TestTeam> teams = new List<TestTeam>();

}

public class TestPrintLog1
{
    public Dictionary<int, int> skillInfos = new Dictionary<int, int>();
}

public class TestPrintLog2
{
    public Dictionary<int, TestLogInfo2> logs = new Dictionary<int, TestLogInfo2>();
}

public class TestLogInfo2
{
    public int skill1;
    public int skill2;
    public int skill3;
    public int skill4;
    public int count1;
    public int count2;
    public int count3;
    public int count4;
}