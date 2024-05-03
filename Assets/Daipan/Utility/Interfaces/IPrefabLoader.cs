#nullable enable
using UnityEngine;

namespace Stream.Utility
{
    public interface IPrefabLoader<out T> where T : Object
    {
        T Load();
    }
}