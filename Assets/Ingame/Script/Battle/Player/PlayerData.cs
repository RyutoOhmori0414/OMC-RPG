using UnityEngine;

namespace RPG.Battle.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "RPG/Data/PlayerData")]
    public sealed class PlayerData : ScriptableObject
    {
        [Header("ゴッドモード")]
        [SerializeField] private bool _isGodMode = false;
        public bool IsGodMode => _isGodMode;
        [Header("PlayerData")]
        [SerializeField] private int _playerHp = 100;
        public int PlayerHp => _playerHp;

        [SerializeField] private int _attack = 20;
        public int Attack => _attack;

        [SerializeField] private int _defense = 5;
        public int Defense => _defense;

        [SerializeField] private SkillAttributeEnum _weakAttribute = SkillAttributeEnum.None;
        public SkillAttributeEnum WeakAttribute => _weakAttribute;
    }   
}