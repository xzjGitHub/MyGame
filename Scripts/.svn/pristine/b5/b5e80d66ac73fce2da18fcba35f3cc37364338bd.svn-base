namespace CombatUIData
{
    public class PlayCharActionData
    {
        public CRCastSkill castSkill;
        public int charIndex;
        public int skillId;
        public CRTargetInfo targetInfo;
        public int actionIndex;
        public PlayCharActionData(CRCastSkill _castSkill, int _targetIndex, int _actionIndex = -1)
        {
            castSkill = _castSkill;
            charIndex = _castSkill.castCharId;
            skillId = _castSkill.castSkillId;
            targetInfo = _castSkill.targetInfos[_targetIndex];
            actionIndex = _actionIndex;
        }
    }

    /// <summary>
    /// 播放技能特效数据
    /// </summary>
    public class PlaySkillEffectData
    {
        //   public CRCastSkill castSkill;
        public int charIndex;
        public int skillId;
        public int targetId;

        public PlaySkillEffectData(int _char, int _skill, int _targetId)
        {
            charIndex = _char;
            skillId = _skill;
            targetId = _targetId;
        }
    }

    public class PlayCommonSkillData
    {
        public int hitTeam;
        public int charIndex;
        public int commonSkillId;
        public int hitIndex;
        public int targetId;
        public int targetIndex;
        public PlayCommonSkillData(int _hitTeam, int _char, int _skill, int _hit, int _targetIndex, int _target)
        {
            hitTeam = _hitTeam;
            charIndex = _char;
            commonSkillId = _skill;
            hitIndex = _hit;
            targetIndex = _targetIndex;
            targetId = _target;
        }
    }

    public class CommonSkillData
    {
        public int skillId;
        public int charIndex;
        public int targetIndex;
        public CommonSkillData(int _skill, int _char, int _targetIndex)
        {
            skillId = _skill;
            charIndex = _char;
            targetIndex = _targetIndex;
        }
    }

    public class PlayCommomHitData
    {
        public int fireTeam;
        public int charIndex;
        public int commonSkillId;
        public int targetIndex;
        public PlayCommomHitData(int _fireTeam, int _char, int _skill, int _target)
        {
            fireTeam = _fireTeam;
            charIndex = _char;
            commonSkillId = _skill;
            targetIndex = _target;
        }
    }

    public class PlaySkillHitData
    {
        public int castTeam;
        public int charIndex;
        public int skillId;
        public int targetIndex;
        public PlaySkillHitData(int _team, int _char, int _skill, int _target)
        {
            castTeam = _team;
            charIndex = _char;
            skillId = _skill;
            targetIndex = _target;
        }
    }

    public class PlayExecStateData
    {
        public int charIndex;
        public CRExecState execState;

        public PlayExecStateData(int _char, CRExecState _state)
        {
            charIndex = _char;
            execState = _state;
        }
    }

}


