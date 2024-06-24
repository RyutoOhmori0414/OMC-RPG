using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Battle.UI
{
    public sealed class ScreenSpaceEnemyUI : MonoBehaviour
    {
        [SerializeField] private TargetUIController _target;
        private EnemyUIData[] _enemies;

        public void SetEnemies(EnemyUIData[] enemies)
        {
            _enemies = enemies;
        }

        private void Update()
        {
        }

        public sealed class EnemyUIData
        {
            public Transform Transform { get; }
            public IDamageable Data { get; }

            public TargetUIController UI { get; set; }

            public EnemyUIData(Transform transform, IDamageable data)
            {
                Transform = transform;
                Data = data;
            }
        }
    }
}