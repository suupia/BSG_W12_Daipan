#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using R3;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerBuilder : IDisposable, IPlayerBuilder
    {
        readonly WaveState _waveState;
        readonly ResultState _resultState;
        readonly CompositeDisposable _disposable = new();

        public PlayerBuilder(
            WaveState waveState
            , ResultState resultState
        )
        {
            _waveState = waveState;
            _resultState = resultState;
        }

        public PlayerMono Build(PlayerMono playerMono)
        {
            _disposable.Clear(); 
            _disposable.Add(Observable.EveryValueChanged(_waveState, x => x.CurrentWaveIndex)
                .Subscribe(_ => playerMono.SetHpMax()));

            _disposable.Add(Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (playerMono.Hp.Value <= 0)
                    {
                        Debug.Log("Player is dead");
                        _resultState.ShowResult();
                    }
                }));
            return playerMono;
        }

        public void Dispose()
        {
            _disposable.Dispose();
            Debug.Log($"Dispose {nameof(PlayerBuilder)}");
        }

        ~PlayerBuilder()
        {
            Dispose();
        }
    }
}