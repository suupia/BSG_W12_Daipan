#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.InputSerial.Tests
{
    public sealed class InputSerialScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SerialInput>(Lifetime.Scoped);
            builder.Register<InputSerialManager>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<InputTest>();
        }
    }

}