using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace RPG.Battle.System
{
    public sealed class BattleManager : MonoBehaviour
    {
        [Inject] private IPhaseController _phaseController;

        private void Start()
        {
            _phaseController.MoveNextPhase();
        }
        
        
    }   
}