using System;
using UnityEngine;

namespace RPG.Battle
{
    public interface IDamageable
    {
        public Transform Transform { get; }
        public Transform FrontTransform { get; }
        public int MaxHp { get; }
        public int CurrentHp { get; }
        public SkillAttributeEnum WeakAttribute { get; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public void SendDamage(int damage);
        public void Heal(int healHp);

        public void SetBuff(BuffData buffData);

        public struct BuffData
        {
            public int BuffTurn { get; private set; }
            
            public float Multiplier { get; }
            
            public BuffType Type { get; }

            public bool IsEnableBuff => BuffTurn > 0;

            public BuffData(int buffTurn, float multiplier, BuffType type)
            {
                BuffTurn = buffTurn;
                Multiplier = multiplier;
                Type = type;
            }

            public void PassTurn()
            {
                BuffTurn++;
            }

            [Serializable]
            public enum BuffType
            {
                Attack,
                Defense
            }
        }
    }
}