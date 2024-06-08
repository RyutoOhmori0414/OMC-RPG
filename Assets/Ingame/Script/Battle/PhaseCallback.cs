namespace RPG.Battle
{
    public struct PhaseCallback
    {
        public int LoopCount { get; }
        
        public BattlePhase LastBattlePhase { get; }
        
        public BattlePhase CurrentBattlePhase { get; }

        public PhaseCallback(int loopCount, BattlePhase lastPhase, BattlePhase currentBattlePhase)
        {
            LoopCount = loopCount;
            LastBattlePhase = lastPhase;
            CurrentBattlePhase = currentBattlePhase;
        }
    }
}
