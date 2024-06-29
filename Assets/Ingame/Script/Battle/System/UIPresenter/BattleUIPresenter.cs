using System;
using RPG.Battle.Player;
using RPG.Battle.UI;
using UniRx;
using UnityEngine;

namespace RPG.Battle.System
{
    public sealed class BattleUIPresenter : MonoBehaviour
    {
        [Header("Model")]
        [SerializeField] private PlayerAttackController _playerAttack;

        [Header("View")]
        [SerializeField] private ScreenSpaceEnemyUI _enemyTarget;
        [SerializeField] private UISkillSelectController _skill;

        private void Awake()
        {
            _enemyTarget.OnTargetSelected.Subscribe(skills => _playerAttack.Attack(_skill.CurrentSkill, skills));
        }
    }
}