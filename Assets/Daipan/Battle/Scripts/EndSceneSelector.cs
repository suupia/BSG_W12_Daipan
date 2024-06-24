#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Stream.Scripts;
using Daipan.Stream.Tests;
using R3;
using UnityEngine;

namespace Daipan.Battle.scripts
{
    public class EndSceneSelector : IStart, IDisposable
    {
        readonly ViewerNumber _viewerNumber;
        readonly DaipanExecutor _daipanExecutor;

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
            //SetUp();
        }

        void SetUp()
        {
            _disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    ChangeToInsideTheBox(_viewerNumber);
                    ChangeToThanksgiving(_viewerNumber);
                    ChangeToOrdinary1(_viewerNumber);
                    ChangeToOrdinary2(_viewerNumber);
                    ChangeToOrdinary3(_viewerNumber);
                    ChangeToSacredLady(_daipanExecutor);
                    ChangeToFlame(_daipanExecutor);
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

        static void ChangeToSacredLady(DaipanExecutor daipanExecutor)
        {
            Debug.Log("Check ChangeToSacredLady");
            if (daipanExecutor.DaipanNum<=10)
            {
                Debug.Log("Change to SacredLady");
                ResultShower.ShowResult(SceneName.SacredLady);
            }
        }

        static void ChangeToFlame(DaipanExecutor dipanExecutor)
        {
            Debug.Log("Check ChangeToFlame");
            if (dipanExecutor.DaipanNum >= 10)
            {
                Debug.Log("Change to Flame");
                ResultShower.ShowResult(SceneName.Flame);
            }
            
        }

        static void ChangeToOrdinary1(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToOrdinary1");
            if(viewerNumber.Number >= 1000&&viewerNumber.Number<=10000)
            {
                Debug.Log("Change to Ordinary1");
                ResultShower.ShowResult(SceneName.Ordinary1);
            }
        }

        static void ChangeToOrdinary2(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToOrdinary2");
            if (viewerNumber.Number >= 10000 && viewerNumber.Number <= 100000)
            {
                Debug.Log("Change to Ordinary2");
                ResultShower.ShowResult(SceneName.Ordinary2);
            }
        }

        static void ChangeToOrdinary3(ViewerNumber viewerNumber)
        {
            Debug.Log("Check ChangeToOrdinary3");
            if (viewerNumber.Number >= 100000 && viewerNumber.Number <= 1000000)
            {
                Debug.Log("Change to Ordinary3");
                ResultShower.ShowResult(SceneName.Ordinary3);
            }
        }
        // todo: 対応する遷移の処理を追加する
        // その時必要な依存関係は注入するようにする。



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