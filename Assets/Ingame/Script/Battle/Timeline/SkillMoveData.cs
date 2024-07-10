using UnityEngine;

namespace RPG.Battle.Timeline
{
    public struct SkillMoveData
    {
        public Transform SkillUserTransform { get; }
        public Transform SkillTargetTransform { get; }

        public SkillMoveData(Transform user, Transform target)
        {
            SkillUserTransform = user;
            SkillTargetTransform = target;
        }
    }
}
