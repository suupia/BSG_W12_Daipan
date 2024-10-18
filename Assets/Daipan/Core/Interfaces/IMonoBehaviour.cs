#nullable enable
using UnityEngine;

namespace Daipan.Core.Interfaces;

public interface IMonoBehaviour
{
    public GameObject GameObject { get; }
    public Transform Transform { get; }
}