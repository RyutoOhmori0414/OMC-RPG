using System;
using RPG.Battle.Skill;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class ScreenSpaceEnemyUI : MonoBehaviour
    {
        [SerializeField] private TargetUIController _enemyButtonPrefab;
        [SerializeField] private Image _targetImage;
        [SerializeField] private InputSystemUIInputModule _inputModule;
        [SerializeField] private UnityEvent _onCancelUnityEvent;

        public IObservable<IDamageable[]> OnTargetSelected => _onTargetSelected;

        private readonly Subject<IDamageable[]> _onTargetSelected = new();
        private InputAction _cancel;
        private AbstractUITargetBase _currentTarget;
        private readonly UISingleTarget _singleEnemyTarget = new();
        private readonly UISingleTarget _singlePlayerTarget = new();

        private bool _isOpen = false;

        private void Awake()
        {
            _targetImage.gameObject.SetActive(false);
            _cancel = _inputModule.cancel.ToInputAction();
            
            _onTargetSelected.Subscribe(_ => CloseTargetButton()).AddTo(this);

            _singleEnemyTarget.Init(_enemyButtonPrefab, transform, _targetImage, _onTargetSelected);
            _singlePlayerTarget.Init(_enemyButtonPrefab, transform, _targetImage, _onTargetSelected);
        }

        public void SetEnemies(TargetData[] enemies)
        {
            _singleEnemyTarget.Targets = enemies;
        }

        public void SetPlayers(TargetData[] players)
        {
            _singlePlayerTarget.Targets = players;
        }

        public void OpenTargetButton(SkillData skillData)
        {
            if (skillData.Skill is SingleAttackSkill) _currentTarget = _singleEnemyTarget;
            else if (skillData.Skill is HealSkill) _currentTarget = _singlePlayerTarget;
            
            _currentTarget.CreateTargetButton();
            _cancel.performed += CancelClose;
            _isOpen = true;
        }

        private void CloseTargetButton()
        {
            _currentTarget.DestroyTargetButton();
            _cancel.performed -= CancelClose;
            _isOpen = false;
        }

        private void CancelClose(InputAction.CallbackContext callback)
        {
            CloseTargetButton();
            _onCancelUnityEvent.Invoke();
        }

        private void OnDestroy()
        {
            _cancel.Dispose();
        }

        public sealed class TargetData
        {
            public Transform Transform { get; }
            public IDamageable Data { get; }

            public TargetData(Transform transform, IDamageable data)
            {
                Transform = transform;
                Data = data;
            }
        }
    }
}