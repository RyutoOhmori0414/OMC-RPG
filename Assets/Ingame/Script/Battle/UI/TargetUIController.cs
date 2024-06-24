using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.UI
{
    public sealed class TargetUIController : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Camera _camera;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _camera = Camera.main;
        }

        public void UpdatePosition(Vector3 targetPos)
        {
            _rectTransform.position = RectTransformUtility.WorldToScreenPoint(_camera, targetPos);
        }
    }   
}