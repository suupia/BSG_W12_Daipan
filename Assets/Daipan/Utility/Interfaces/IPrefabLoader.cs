#nullable enable
using UnityEngine;

namespace Daipan.Stream.Scripts.Utility
{
    public interface IPrefabLoader<out T> where T : Object
    {
        T Load();
    }
}