using System;
using UnityEngine;

namespace RPG.Battle.Skill
{
    [Serializable]
    public sealed class SingleAttackSkill : ISkill
    {
        [SerializeField] private SkillAttributeEnum _skillAttribute = SkillAttributeEnum.None;
        [SerializeField] private float _attackMultiplier = 1.0F;
        [SerializeField] private float _weakMultiplier = 1.5F;

        public void UseSkill(IDamageable user, IDamageable[] targets)
        {
            var damage = user.Attack * _attackMultiplier;

            if ((targets[0].WeakAttribute & _skillAttribute) != SkillAttributeEnum.None)
            {
                damage *= _weakMultiplier;
            }

            targets[0].SendDamage((int)damage - targets[0].Defense);
        }
    }   
}