public partial class SkillAttribute
{
    public int skillId;

    public int skillEffect;

    public SkillAttribute(int _id,CharAttribute _charAttribute)
    {
        skillId = _id;
        charAttribute = _charAttribute;
    }

}

