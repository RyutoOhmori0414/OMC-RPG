using MessagePipe;
using VContainer.Unity;

namespace RPG.Battle.System
{
    public sealed class BattlePhaseProvider : IStartable ,IPhaseController
    {
        private BattlePhase _currentPhase = BattlePhase.None;

        private int _currentLoopCount = 0;

        private readonly IPublisher<PhaseCallback> _publisher;

        public BattlePhaseProvider(IPublisher<PhaseCallback> publisher)
        {
            _publisher = publisher;
        }

        private void Publish()
        {
            var last = _currentPhase++;
            bool isTurnChanged = false;

            if (_currentPhase > BattlePhase.StatusEffect)
            {
                _currentPhase = BattlePhase.EnemyAttack;
                _currentLoopCount++;
                isTurnChanged = true;
            }

            _publisher.Publish(new (_currentLoopCount, last, _currentPhase, isTurnChanged));
        }

        public void Start() => Publish();

        public void MoveNextPhase() => Publish();
    }   
}