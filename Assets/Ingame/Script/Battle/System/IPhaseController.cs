namespace RPG.Battle.System
{
    public interface IPhaseController
    {
        /// <summary>次のPhaseに進む</summary>
        public void MoveNextPhase();
    }
}