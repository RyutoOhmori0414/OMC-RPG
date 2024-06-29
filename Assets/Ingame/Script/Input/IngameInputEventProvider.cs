using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace RPG.Input
{
    public sealed class IngameInputEventProvider : IInitializable, ITickable
    {
        [Inject] private IPublisher<IngameInputParams> _inputPublisher;

        private IngameInput _input;
        
        public void Initialize()
        {
            _input = new();
            _input.Enable();
        }
        
        public void Tick()
        {
            _inputPublisher.Publish(new (_input));
        }
    }
}
