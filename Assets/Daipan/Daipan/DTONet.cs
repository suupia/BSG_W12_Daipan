#nullable enable
using Fusion;

namespace Daipan.Daipan
{
    public class DTONet : NetworkBehaviour
    {
        [Networked] public double IrritatedValue { get; set; }
        [Networked] public NetworkBool TestFlag { get; set; }
    }
}