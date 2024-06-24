using System;
using UnityEngine;

namespace RPG.Battle.Skill
{
    [Serializable]
    public sealed class MultiBuffSkill : ISkill
    {
        [SerializeField] private float _attackMultiplier = 1.0F;
        [SerializeField] private int _buffTurn = 3;
        [SerializeField] private IDamageable.BuffData.BuffType _buffType = IDamageable.BuffData.BuffType.Attack;

        public void UseSkill(IDamageable user, IDamageable[] targets)
        {
            foreach (var target in targets)
            {
                target.SetBuff(new IDamageable.BuffData(_buffTurn, _attackMultiplier, _buffType));   
            }
        }
    }   
}