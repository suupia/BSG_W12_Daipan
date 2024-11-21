#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICostValueParam
{
    public int MaxCostValue { get; }
    public int InitializedCostValue { get; }
    public int IncreaseCostTime { get; }
    public int IncreasedCostValue { get; }
}
