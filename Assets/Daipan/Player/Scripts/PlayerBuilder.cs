#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using R3;

namespace Daipan.Player.Scripts
{
    public class PlayerBuilder : IDisposable, IPlayerBuilder
    {
        readonly WaveState _waveState;
        readonly IPlayerHpParamData _playerHpParamData;
        readonly ResultState _resultState;
        readonly CompositeDisposable _disposable = new();

        public PlayerBuilder(
            WaveState waveState
            , IPlayerHpParamData playerHpParamData
            , ResultState resultState
        )
        {
            _waveState = waveState;
            _playerHpParamData = playerHpParamData;
            _resultState = resultState;
        }

        public PlayerMono Build(PlayerMono playerMono)
        {
            _disposable.Add(Observable.EveryValueChanged(_waveState, x => x.CurrentWaveIndex)
                .Subscribe(_ => playerMono.Hp = new Hp(_playerHpParamData.GetMaxHp())));

            _disposable.Add(Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (playerMono.Hp.Value <= 0) _resultState.ShowResult();
                }));
            return playerMono;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        ~PlayerBuilder()
        {
            Dispose();
        }
    }
}