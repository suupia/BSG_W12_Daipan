#nullable enable
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using VContainer;
using VContainer.Unity;

namespace Daipan.Core.Scripts
{
    public sealed class Starter : IInitializable
    {
        IEnumerable<IStart> _starts = null!;

        void IInitializable.Initialize()
        {
            foreach (var start in _starts) start.Start();
        }

        [Inject]
        public void Initialize(
            IEnumerable<IStart> starts
        )
        {
            _starts = starts;
        }
    }
}