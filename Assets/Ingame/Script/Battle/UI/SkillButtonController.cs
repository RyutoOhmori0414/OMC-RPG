using System;
using RPG.Battle.Skill;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class SkillButtonController : MonoBehaviour
    {
        [SerializeField] private Text _buttonText = default;

        public IObservable<SkillData> OnSkillSelect => _onSkillSelect;
        
        private SkillData _skill;
        private readonly Subject<SkillData> _onSkillSelect = new ();
        private Button _button;
        

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnSkillButtonClickReceiver);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnSkillButtonClickReceiver);
        }

        private void OnDestroy()
        {
            _onSkillSelect.Dispose();
        }

        private void OnSkillButtonClickReceiver() => _onSkillSelect.OnNext(_skill);

        public void SetSkill(SkillData skill)
        {
            _skill = skill;

            _buttonText.text = skill.SkillName;
        }
    }   
}