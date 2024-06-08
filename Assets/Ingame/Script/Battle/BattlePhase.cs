using System;

namespace RPG.Battle
{
    [Serializable]
    public enum BattlePhase
    {
        None = -1,
        PlayerSelect,
        PlayerAttack,
        EnemyAttack,
        StatusEffect
    }
}
