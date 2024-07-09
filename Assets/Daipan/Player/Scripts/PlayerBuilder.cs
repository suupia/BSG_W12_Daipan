#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using R3;

namespace Daipan.Player.Scripts
{
    public class PlayerBuilder : IDisposable
    {
        readonly WaveState _waveState;
        readonly IPlayerHpParamData _playerHpParamData;
        readonly EndSceneSelector _endSceneSelector;
        readonly List<IDisposable> _disposables = new();

        public PlayerBuilder(
            WaveState waveState
            ,PlayerHpParamData playerHpParamData
            ,EndSceneSelector endSceneSelector
        )
        {
            _waveState = waveState;
            _playerHpParamData = playerHpParamData; 
            _endSceneSelector = endSceneSelector;
        }
       public PlayerMono Build(PlayerMono playerMono)
       {
           _disposables.Add(Observable.EveryValueChanged(_waveState, x => x.CurrentWave)
               .Subscribe(_ => playerMono.Hp = new Hp(_playerHpParamData.GetMaxHp())));

           _disposables.Add(Observable.EveryUpdate()
               .Subscribe(_ =>
               {
                   if (playerMono.Hp.Value <= 0) _endSceneSelector.TransitToEndScene();
               }));
           return playerMono;
       }
       
       public void Dispose()
       {
           foreach (var disposable in _disposables)
           {
               disposable.Dispose();
           }
       }
       
       ~PlayerBuilder()
       {
           Dispose();
       }
    } 
}

