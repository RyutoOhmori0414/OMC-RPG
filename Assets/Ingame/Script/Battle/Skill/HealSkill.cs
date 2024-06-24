using System;
using UnityEngine;

namespace RPG.Battle.Skill
{
    [Serializable]
    public sealed class HealSkill : ISkill
    {
        [SerializeField] private float _healMultiplier = 1.0F;

        public void UseSkill(IDamageable user, IDamageable[] targets)
        {
            user.Heal((int)(user.Attack * _healMultiplier));
        }
    }   
}