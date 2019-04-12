using System.Collections.Generic;
using Altar.Data;
using Barrack.Data;

namespace Altar.Controller
{
    public partial class AltarController
    {
        public bool CanLHCall(int id,bool showTip = false)
        {
            if(CharSystem.Instance.CharAttributeList.Count >= GetMaxCharNum())
            {
                if(showTip)
                    TipManager.Instance.ShowTip("角色数量已经达到上限，无法继续召唤");
                return false;
            }
            Summon_remains summon_Remains = Summon_remainsConfig.GetSummon_Remains(id);
            List<List<int>> cost = summon_Remains.summonFormula;
            for(int i = 0; i < cost.Count; i++)
            {
                int sum = ItemSystem.Instance.GetItemNumByTemplateID(cost[i][0]);
                if(sum < cost[i][1])
                {
                    if(showTip)
                        TipManager.Instance.ShowTip("物品数量不够");
                    return false;
                }
            }

            if(ScriptSystem.Instance.Gold < summon_Remains.goldCost)
            {
                if(showTip)
                    TipManager.Instance.ShowTip("金币不足");
                return false;
            }
            if(ScriptSystem.Instance.Mana < summon_Remains.manaCost)
            {
                if(showTip)
                    TipManager.Instance.ShowTip("魔力不足");
                return false;
            }
            return true;
        }

        public void LHCall(int id,CharData charData)
        {
            Summon_remains summon_Remains = Summon_remainsConfig.GetSummon_Remains(id);
            List<List<int>> cost = summon_Remains.summonFormula;
            for(int i = 0; i < cost.Count; i++)
            {
                ItemSystem.Instance.RemoveItemByuTemplateId(cost[i][0],cost[i][1]);
            }
            ScriptSystem.Instance.SubGold(summon_Remains.goldCost);
            ScriptSystem.Instance.SubMana(summon_Remains.manaCost);

            CharAttribute attr = new CharAttribute(charData);
            CharSystem.Instance.AddChar(new CharAttribute(charData));

            TipManager.Instance.ShowTip("成功复活角色： " + attr.char_template.charName);

            BarrackSystem.Instance.AddToFuHuoList(id);

            GetCharPanel panel = UIPanelManager.Instance.Show<GetCharPanel>();
            panel.UpdateInfo(new CharAttribute(charData));
        }

        public CharAttribute GetChar(int id)
        {
            CharAttribute att = AltarSystem.Instance.GetItemCallCharAttr(id);
            if(att != null)
            {
                return att;
            }
            Summon_remains summon_Remains = Summon_remainsConfig.GetSummon_Remains(id);
            CharCreate charCreate = new CharCreate(GetCharRndomId(summon_Remains));
            charCreate.charLevel = 1;
            CharAttribute attr = CharSystem.Instance.CreateChar(charCreate,false);
            AltarSystem.Instance.AddCharInfoToItemCallChar(id,attr.GetCharData());
            return attr;
        }

        private int GetCharRndomId(Summon_remains sr)
        {
            int random = UnityEngine.Random.Range(0,10000);
            List<int> list = new List<int>();
            for(int i = 0; i < sr.selectChance.Count; i++)
            {
                int sum = 0;
                for(int j = 0; j <= i; j++)
                {
                    sum += sr.selectChance[j];
                }
                list.Add(sum);
            }
            int index = Utility.GetIndex(list,random);
            return sr.summonChar[index];
        }
    }
}
