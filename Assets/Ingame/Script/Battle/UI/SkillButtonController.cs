using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Battle.Skill;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class SkillButtonController : MonoBehaviour
    {
        [SerializeField] private Text _buttonText = default;

        private SkillData _skill;
        private Action<SkillData> _onSkillSelect;
        private Button _button;
        
        public event Action<SkillData> OnSkillSelect
        {
            add => _onSkillSelect += value;
            remove => _onSkillSelect -= value;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickReceived);
        }

        private void OnClickReceived() => _onSkillSelect?.Invoke(_skill);

        public void SetSkill(SkillData skill)
        {
            _skill = skill;

            _buttonText.text = skill.SkillName;
        }
    }   
}