using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Battle.Player;
using RPG.Battle.UI;
using UnityEngine;

namespace RPG.Battle.System
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private ScreenSpaceEnemyUI _enemyUI;
        [SerializeField] private PlayerController[] _players;

        private void Awake()
        {
            _enemyUI.SetPlayers(Array.ConvertAll(_players, x => new ScreenSpaceEnemyUI.TargetData(x.transform, x)));
        }
    }
}
