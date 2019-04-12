using System;

namespace MCCombat
{
    public class CombatSystemTool
    {
        /// <summary>
        /// 是否周期释放
        /// </summary>
        /// <param name="frequency"></param>
        /// <param name="nowNum"></param>
        /// <param name="lastNum"></param>
        /// <returns></returns>
        public static bool IsSkillCastFrequency(int frequency, int nowNum, int lastNum)
        {
            if (frequency <= 1 || lastNum == 0)
            {
                return true;
            }
            if ((nowNum - 1) / frequency != (lastNum - 1) / frequency)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 是否随机释放
        /// </summary>
        /// <param name="isRandom"></param>
        /// <param name="castFrequency"></param>
        /// <param name="nowNum"></param>
        /// <returns></returns>
        public static bool IsSkillRandom(int isRandom, int castFrequency, int nowNum)
        {
            if (isRandom < 1)
            {
                return true;
            }
            if (castFrequency <= 1)
            {
                return true;
            }
            //
            int currentCounter = nowNum % castFrequency;
            float castChance = 10000;
            if (currentCounter != 0)
            {
                castChance = (int)((float)Math.Pow((1f / castFrequency) / (1 - 1f / castFrequency), (currentCounter - 1)) * 10000f);
            }
            return RandomBuilder.RandomIndex_Chances(castChance) == 0;
        }

        public static bool IsSkillCooldown()
        {
            return false;
        }

        public static bool IsCanAction(CombatUnit combatUnit)
        {
            if (combatUnit.hp<=0)
            {
                return false;
            }
            foreach (var item in combatUnit.States)
            {
                if (item.SkillEffect==SkillEffect.JiYun)
                {
                    return false;
                }
            }
            return true;
        }
    }
}


