
public class CharaterUti
{
    public static UnityEngine.Sprite GetRankSprite(int charRank)
    {
        return GetRankSprite((CharRank)charRank);
    }

    public static UnityEngine.Sprite GetRankSprite(CharRank charRank)
    {
        switch(charRank)
        {
            case CharRank.E:
                return ResourceLoadUtil.LoadSprite(ResourceType.CharRank,StringDefine.SpriteNameDefine.E);
            case CharRank.D:
                return ResourceLoadUtil.LoadSprite(ResourceType.CharRank,StringDefine.SpriteNameDefine.D);
            case CharRank.C:
                return ResourceLoadUtil.LoadSprite(ResourceType.CharRank,StringDefine.SpriteNameDefine.C);
            case CharRank.B:
                return ResourceLoadUtil.LoadSprite(ResourceType.CharRank,StringDefine.SpriteNameDefine.B);
            case CharRank.A:
                return ResourceLoadUtil.LoadSprite(ResourceType.CharRank,StringDefine.SpriteNameDefine.A);
            case CharRank.S:
                return ResourceLoadUtil.LoadSprite(ResourceType.CharRank,StringDefine.SpriteNameDefine.S);
            default:
                return null;
        }
    }


    public static string GetRankName(CharRank charRank)
    {
        switch(charRank)
        {
            case CharRank.E:
                return "E级";
            case CharRank.D:
                return "D级";
            case CharRank.C:
                return "C级";
            case CharRank.B:
                return "B级";
            case CharRank.A:
                return "A级";
            case CharRank.S:
                return "S级";
            default:
                return "新手";
        }
    }
}
