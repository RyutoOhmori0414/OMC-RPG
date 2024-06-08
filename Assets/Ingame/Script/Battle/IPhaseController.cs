using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle
{
    public interface IPhaseController
    {
        /// <summary>次のPhaseに進む</summary>
        public void MoveNextPhase();
    }
}