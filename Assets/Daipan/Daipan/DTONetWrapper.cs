#nullable enable
using Fusion;
using UnityEngine;

namespace Daipan.Daipan;

public class DTONetWrapper
{
    DTONet? _dtoNet;
    DTONet? DTONet => _dtoNet ??= Object.FindObjectOfType<DTONet>();

    public double IrritatedValue
    {
        get => DTONet?.IrritatedValue ?? 0;
        set
        {
            if (DTONet != null) DTONet.IrritatedValue = value;
        }
    }

    public NetworkBool TestFlag => DTONet?.TestFlag ?? false;
}