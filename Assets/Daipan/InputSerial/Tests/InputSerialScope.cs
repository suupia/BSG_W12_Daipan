#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Enemy.Tests
{
    public sealed class InputSerialScope : LifetimeScope
    {
        [SerializeField] SerialInput serialInput = null!;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(serialInput);

        }
    }

}