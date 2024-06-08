using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "RPG/Data/PlayerData")]
    public sealed class PlayerDataScriptableObject : ScriptableObject
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
    }   
}