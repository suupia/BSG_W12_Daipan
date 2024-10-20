#nullable enable
using Fusion;

namespace Daipan.Core
{
    public class DTONet : NetworkBehaviour
    {
        [Networked] public double IrritatedValue { get; set; }
        [Networked] public NetworkBool testFlag { get; set; }
    } 
}

