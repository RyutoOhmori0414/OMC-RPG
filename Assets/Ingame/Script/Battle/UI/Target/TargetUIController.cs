using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public sealed class TargetUIController : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        public Button Button { get; private set; }
        public IObservable<BaseEventData> OnSelectEvent => _onSelectSubject;
        public IObservable<BaseEventData> OnDeselectEvent => _onDeselectSubject;
        public IObservable<IDamageable> OnClickSubjectEvent => _onClickSubject;
        
        private ScreenSpaceEnemyUI.TargetData _targetEnemy;
        private RectTransform _rectTransform;
        private Camera _camera;
        private readonly Subject<BaseEventData> _onSelectSubject = new();
        private readonly Subject<BaseEventData> _onDeselectSubject = new();
        private readonly Subject<IDamageable> _onClickSubject = new();
        
        private void Awake()
        {
            Button = GetComponent<Button>();
            _rectTransform = GetComponent<RectTransform>();
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClickListener);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClickListener);
        }

        private void OnClickListener() => _onClickSubject.OnNext(_targetEnemy.Data);
        
        private void Update()
        {
            UpdatePosition();
        }

        private void OnDestroy()
        {
            _onSelectSubject.Dispose();
            _onDeselectSubject.Dispose();
        }

        public void SetEnemyTransform(ScreenSpaceEnemyUI.TargetData targetEnemy)
        {
            _targetEnemy = targetEnemy;
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _rectTransform.position = RectTransformUtility.WorldToScreenPoint(_camera, _targetEnemy.Transform.position);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _onSelectSubject.OnNext(eventData);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _onDeselectSubject.OnNext(eventData);
        }
    }   
}