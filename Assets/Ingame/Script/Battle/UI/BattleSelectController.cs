using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace RPG.Battle.UI
{
    public sealed class BattleSelectController : AbstractUIAnimation
    {
        [SerializeField] private RectTransform _animRoot;
        [SerializeField] private RectTransform _openAncher;
        [SerializeField] private RectTransform _closeAncher;
        [SerializeField] private float _openAndCloseSec = 1;

        private void Awake()
        {
            gameObject.SetActive(false);
            _animRoot.position = _closeAncher.position;
        }

        public override void OpenUI()
        {
            _animRoot.DOMove(_openAncher.position, _openAndCloseSec).OnStart(() => gameObject.SetActive(true));
        }

        public override void CloseUI()
        {
            _animRoot.DOMove(_closeAncher.position, _openAndCloseSec).OnComplete(() => gameObject.SetActive(false));
        }
    }   
}