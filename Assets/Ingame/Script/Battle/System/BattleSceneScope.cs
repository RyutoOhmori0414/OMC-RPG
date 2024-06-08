using System.Collections;
using System.Collections.Generic;
using MessagePipe;
using UnityEngine;
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