using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Battle.UI
{
    public sealed class BattleSelectController : AbstractUIAnimation
    {
        [SerializeField] private RectTransform _animRoot;
        [SerializeField] private RectTransform _openAncher;
        [SerializeField] private RectTransform _closeAncher;
        [SerializeField] private float _openAndCloseSec = 1;

        [SerializeField] private GameObject _selectButton;

        private Tweener _animTweener;
        
        private void Awake()
        {
            gameObject.SetActive(false);
            _animRoot.position = _closeAncher.position;
        }

        public override void OpenUI()
        {
            _animTweener?.Complete();
            
            EventSystem.current.SetSelectedGameObject(_selectButton);
            _animTweener = _animRoot.DOMove(_openAncher.position, _openAndCloseSec).OnStart(() => gameObject.SetActive(true));
        }

        public override void CloseUI()
        {
            _animTweener?.Complete();
            
            _animTweener = _animRoot.DOMove(_closeAncher.position, _openAndCloseSec).OnComplete(() => gameObject.SetActive(false));
        }
    }   
}