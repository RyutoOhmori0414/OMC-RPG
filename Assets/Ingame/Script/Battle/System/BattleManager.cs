using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RPG.Battle.Player;
using RPG.Battle.UI;
using UnityEngine;
using VContainer;

namespace RPG.Battle.System
{
    public sealed class BattleManager : MonoBehaviour
    {
        [SerializeField] private LoadManager _loadManager = default;
        [SerializeField] private PlayerSkillManager _skillManager = default;
        [SerializeField] private UISkillSelectController _uiSkill = default;
        [SerializeField] private BattleSelectController _battleSelect = default;
        
        [Inject] private IPhaseController _phaseController;

        private void Awake()
        {
            _loadManager.OnLoadEnd += OnLoadEnd;
        }

        private void Start()
        {
            _phaseController.MoveNextPhase();
        }

        private void OnLoadEnd()
        {
            _uiSkill.SetSkillData(_skillManager.PlayerSkill);
            _battleSelect.OpenUI();
        }
    }   
}