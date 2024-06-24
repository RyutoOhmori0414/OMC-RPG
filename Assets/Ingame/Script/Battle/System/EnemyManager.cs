using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Battle.Enemy;
using RPG.Battle.UI;
using UnityEngine;

namespace RPG.Battle.System
{
    public sealed class EnemyUIManager : MonoBehaviour
    {
        [SerializeField] private ScreenSpaceEnemyUI _enemyUI = default;
        [SerializeField] private EnemyController[] _enemies = default;

        private void Awake()
        {
            _enemyUI.SetEnemies(Array.ConvertAll(
                _enemies, 
                x => new ScreenSpaceEnemyUI.EnemyUIData(x.transform, x)
                ));
        }
    }   
}