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
    public class PlayerBuilderTutorial : IDisposable, IPlayerBuilder
    {
        readonly WaveState _waveState;
        readonly IPlayerHpParamData _playerHpParamData;
        readonly List<IDisposable> _disposables = new();

        public PlayerBuilderTutorial(
            WaveState waveState
            ,IPlayerHpParamData playerHpParamData
        )
        {
            _waveState = waveState;
            _playerHpParamData = playerHpParamData;
        }
       public PlayerMono Build(PlayerMono playerMono)
       {
           _disposables.Add(Observable.EveryValueChanged(_waveState, x => x.CurrentWaveIndex)
               .Subscribe(_ => playerMono.SetHpMax()));

           return playerMono;
       }
       
       public void Dispose()
       {
           foreach (var disposable in _disposables)
           {
               disposable.Dispose();
           }
       }
       
       ~PlayerBuilderTutorial()
       {
           Dispose();
       }
    } 
}

