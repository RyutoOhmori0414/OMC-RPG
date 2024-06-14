namespace RPG.Battle
{
    public struct PhaseCallback
    {
        public int LoopCount { get; }
        
        public bool IsTurnChanged { get; }
        
        public BattlePhase LastBattlePhase { get; }
        
        public BattlePhase CurrentBattlePhase { get; }

        public PhaseCallback(int loopCount, BattlePhase lastPhase, BattlePhase currentBattlePhase, bool isTurnChanged)
        {
            LoopCount = loopCount;
            LastBattlePhase = lastPhase;
            CurrentBattlePhase = currentBattlePhase;
            IsTurnChanged = isTurnChanged;
        }
    }
}
