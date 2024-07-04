#nullable enable
using System;

namespace Daipan.Stream.Scripts
{
    public sealed class StreamData
    {
        // Timer  
        public Func<double> GetMaxTime { get; init; } = () => 10.0;
    }
}