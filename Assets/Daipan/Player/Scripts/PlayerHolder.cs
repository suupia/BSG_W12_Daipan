#nullable enable
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerHolder
    {
        PlayerMono? _player;

        public PlayerMono PlayerMono
        {
            get
            {
                if (_player == null)
                {
                    Debug.LogWarning($"_player is null");
                    return null!;
                }

                return _player;
            }
            set => _player = value;
        }
    }
}