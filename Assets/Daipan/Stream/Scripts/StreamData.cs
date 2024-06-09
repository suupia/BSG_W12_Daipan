#nullable enable
//using Unity.Plastic.Antlr3.Runtime.Misc;
using System;

namespace Daipan.Stream.Scripts
{
    public class StreamData
    {
        // Timer  
        public Func<double> GetMaxTime { get; init; } = () => 10.0;
    }
}