#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Battle.scripts
{
    public sealed class EndSceneSelector : IStart, IDisposable
    {
        readonly ViewerNumber _viewerNumber;
        readonly DaipanExecutor _daipanExecutor;

        PlayerMono? _playerMono;
        IDisposable? _disposable; 
        
        public EndSceneSelector(
            ViewerNumber viewerNumber
            , DaipanExecutor daipanExecutor
            )
        {
            _viewerNumber = viewerNumber;
            _daipanExecutor = daipanExecutor;
        }

        void IStart.Start()
        {
            SetUp();
        }

        void SetUp()
        {
            _disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (_playerMono == null)
                    {
                        _playerMono = UnityEngine.Object.FindObjectOfType<PlayerMono>();
                        if (_playerMono == null) return;
                    }
                    
                    ChangeToInsideTheBox(_viewerNumber);
                    ChangeToThanksgiving(_viewerNumber);
                    ChangeToNoobGamer(_playerMono.Hp);
                    ChangeToProGamer(_playerMono.Hp);
                    ChangeToSacredLady(_daipanExecutor);
                    ChangeToBacklash(_daipanExecutor);
                    ChangeToStrugglingStreamer(_viewerNumber);
                    
                });
        }

        static void ChangeToInsideTheBox(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToInsideTheBox");
            if(viewerNumber.Number <= 500)  // todo : receive parameter from inspector
            {
                Debug.Log("Change to InsideTheBox");
                ResultShower.ShowResult(SceneName.InsideTheBox);
            }
        }
       
        static void ChangeToThanksgiving(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToThanksgiving");
            if(viewerNumber.Number >= 1000)  // todo : receive parameter from inspector
            {
                Debug.Log("Change to Thanksgiving");
                ResultShower.ShowResult(SceneName.Thanksgiving);
            }
        }

        static void ChangeToNoobGamer(Hp hp)
        {
            Debug.Log("Check ChangeToBottomYoutuber");
            if (hp.Value <= 0)
            {
                Debug.Log("Change to BottomYoutuber");
                ResultShower.ShowResult(SceneName.BottomYoutuber);
            }
        }

        static void ChangeToProGamer(Hp hp)
        {
            Debug.Log("Check ChangeToProGamer");
            if(hp.Value >= 50)
            {
                Debug.Log("Change To ProGamer");
                ResultShower.ShowResult(SceneName.ProGamer);

            }
        }

        static void ChangeToSacredLady(DaipanExecutor daipanExecutor)
        {
            Debug.Log("Check ChangeToSacredLady");
            if (daipanExecutor.DaipanCount<=10)
            {
                Debug.Log("Change to SacredLady");
                ResultShower.ShowResult(SceneName.SacredLady);
            }
        }

        static void ChangeToBacklash(DaipanExecutor dipanExecutor)
        {
            Debug.Log("Check ChangeToFlame");
            if (dipanExecutor.DaipanCount >= 10)
            {
                Debug.Log("Change to Flame");
                ResultShower.ShowResult(SceneName.Flame);
            }
            
        }

        static void ChangeToStrugglingStreamer(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToOrdinary1");
            if(viewerNumber.Number >= 1000&&viewerNumber.Number<=10000)
            {
                Debug.Log("Change to Ordinary1");
                ResultShower.ShowResult(SceneName.Ordinary1);
            }
        }



        public void Dispose()
        {
            _disposable?.Dispose();
        }
        
        ~EndSceneSelector()
        {
            Dispose();
        }

    }
}