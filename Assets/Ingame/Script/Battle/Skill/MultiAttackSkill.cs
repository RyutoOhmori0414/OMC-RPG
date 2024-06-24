using System;
using UnityEngine;

namespace RPG.Battle.Skill
{
    [Serializable]
    public sealed class MultiAttackSkill : ISkill
    {
        [SerializeField] private SkillAttributeEnum _skillAttribute = SkillAttributeEnum.None;
        [SerializeField] private float _attackMultiplier = 1.0F;
        [SerializeField] private float _weakMultiplier = 1.5F;

        public void UseSkill(IDamageable user, IDamageable[] targets)
        {
            foreach (var target in targets)
            {
                var damage = user.Attack * _attackMultiplier;

                if ((target.WeakAttribute & _skillAttribute) != SkillAttributeEnum.None)
                {
                    damage *= _weakMultiplier;
                }

                target.SendDamage((int)damage - target.Defense);   
            }
        }
    }
}