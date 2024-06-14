using MessagePipe;
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

            builder.RegisterEntryPoint<BattlePhaseProvider>(Lifetime.Scoped);
        }
    }   
}