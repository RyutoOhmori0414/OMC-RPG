using UnityEngine;

namespace RPG.Battle.Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "RPG/Data/EnemyData")]
    public sealed class EnemyDataScriptableObject : ScriptableObject
    {
        [Header("ゴッドモード")]
        [SerializeField] private bool _isGodMode = false;
        public bool IsGodMode => _isGodMode;
        [Header("EnemyData")]
        [SerializeField] private int _enemyHp = 100;
        public int EnemyHp => _enemyHp;

        [SerializeField] private int _attack = 20;
        public int Attack => _attack;

        [SerializeField] private int _defense = 5;
        public int Defense => _defense;

        [SerializeField] private SkillAttributeEnum _weakAttribute = SkillAttributeEnum.None;
        public SkillAttributeEnum WeakAttribute => _weakAttribute;
    }   
}