using System;
using DG.Tweening;
using MessagePipe;
using RPG.Battle.Skill;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class UISkillSelectController : AbstractUIAnimation
    {
        [SerializeField] private ScreenSpaceEnemyUI _enemyUI;
        [SerializeField] private InputSystemUIInputModule _inputModule;
        [Header("DoTween周り")]
        [SerializeField] private RectTransform _animRoot;
        [SerializeField] private RectTransform _openAncher;
        [SerializeField] private RectTransform _closeAncher;
        [SerializeField] private float _openAndCloseSec = 1F;
        
        [Header("FirstSelect")]
        [SerializeField] private GameObject _select;
        [Space]
        [SerializeField] private SkillButtonController[] _skillButtons;
        [SerializeField] private UnityEvent _onCancelUnityEvent;

        public SkillData CurrentSkill { get; private set; }
        
        private Tweener _animTweener;
        private Image _panel = default;
        private SkillData[] _skills;
        private InputAction _cancel;

        private bool _isOpen = false;

        private void Awake()
        {
            gameObject.SetActive(false);
            _animRoot.position = _closeAncher.position;

            _cancel = _inputModule.cancel.ToInputAction();

            foreach (var button in _skillButtons)
            {
                button.OnSkillSelect.Subscribe(_enemyUI.OpenTargetButton).AddTo(_enemyUI);
                button.OnSkillSelect.Subscribe(_ => CloseUI()).AddTo(this);
                button.OnSkillSelect.Subscribe(skill => CurrentSkill = skill).AddTo(this);
            }
        }

        public void SetSkillData(SkillData[] skillData)
        {
            _skills = skillData;
  　　　　　　　　　　　　　　　　　　　
            for (int i = 0; i < _skillButtons.Length; i++)
            {
                _skillButtons[i].gameObject.SetActive(true);
                
                if (i < _skills.Length)
                {
                    _skillButtons[i].SetSkill(_skills[i]);
                }
                else
                {
                    _skillButtons[i].gameObject.SetActive(false);
                }
            }
        }

        public override void OpenUI()
        {
            _animTweener?.Complete();
            
            _isOpen = true;
            EventSystem.current.SetSelectedGameObject(_select);
            _animTweener = _animRoot.DOMove(_openAncher.position, _openAndCloseSec)
                .OnStart(() => gameObject.SetActive(true))
                .OnComplete(() => _cancel.started += CancelClose);
        }

        public override void CloseUI()
        {
            _animTweener?.Complete();
            
            _isOpen = false;
            _animTweener = _animRoot.DOMove(_closeAncher.position, _openAndCloseSec)
                .OnStart(() => _cancel.started -= CancelClose)
                .OnComplete(() => gameObject.SetActive(false));
        }

        private void CancelClose(InputAction.CallbackContext callback)
        {
            CloseUI();
            _onCancelUnityEvent.Invoke();
        }

        private void OnDestroy()
        {
            _cancel.Dispose();
        }
    }
}
