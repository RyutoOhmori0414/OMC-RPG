using MessagePipe;
using RPG.Input;
using VContainer;
using VContainer.Unity;

namespace RPG.Battle.System
{
    public class BattleSceneScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var option = builder.RegisterMessagePipe();

            builder.RegisterMessageBroker<PhaseCallback>(option);
            builder.RegisterMessageBroker<IngameInputParams>(option);
            
            builder.RegisterEntryPoint<BattlePhaseProvider>(Lifetime.Scoped);
            builder.RegisterEntryPoint<IngameInputEventProvider>(Lifetime.Scoped);
        }
    }   
}