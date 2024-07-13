#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Option.Scripts;
using Daipan.Tutorial.Interfaces;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    public interface ISpeechEvent
    {
        int Id { get; }
        string Message { get; }
        SpeechEventEnum SpeechEventEnum { get; }
        (bool, ISpeechEvent) MoveNext();
        void SetNextEvent(params ISpeechEvent[] nextEvents);
    }

    public enum SpeechEventEnum
    {
        None,
        Listening, // 聞くタイプのチュートリアル
        Practical // 実践するタイプのチュートリアル
    }

    public sealed record SequentialEvent : ISpeechEvent
    {
        public int Id { get; } = -1;
        public string Message { get; } = string.Empty;
        public SpeechEventEnum SpeechEventEnum { get; }
        ISpeechEvent? NextEvent { get; set; }
        Func<bool> CanMove { get; } = () => true;

        public SequentialEvent(int id, string message, SpeechEventEnum speechEventEnum, Func<bool> canMove)
        {
            Id = id;
            Message = message;
            SpeechEventEnum = speechEventEnum;
            CanMove = canMove;
        }

        public SequentialEvent(int id, string message, SpeechEventEnum speechEventEnum)
            : this(id, message, speechEventEnum, () => true)
        {
        }

        public (bool, ISpeechEvent) MoveNext()
        {
            var result = CanMove();
            if (!result) return (false, this);
            if (NextEvent == null) return (false, this);
            return (true, NextEvent);
        }

        public void SetNextEvent(params ISpeechEvent[] nextEvents)
        {
            if (nextEvents.Length != 1) throw new ArgumentException("NextEvent must be one");
            NextEvent = nextEvents[0];
        }
    }

    public sealed record EndEvent : ISpeechEvent
    {
        public int Id => -1;
        public string Message => string.Empty;
        public SpeechEventEnum SpeechEventEnum => SpeechEventEnum.None;

        public (bool, ISpeechEvent) MoveNext()
        {
            return (false, this);
        }

        public void SetNextEvent(params ISpeechEvent[] nextEvents)
        {
            throw new NotImplementedException();
        }
    }

    public static class SpeechEventBuilder
    {
        public static ISpeechEvent BuildUICatIntroduce()
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "やぁ、初めまして！僕はネコ！", SpeechEventEnum.Listening),
                    new SequentialEvent(1, "君の配信をサポートするよ！", SpeechEventEnum.Listening),
                    new SequentialEvent(2, "じゃあ、まずこのゲームの説明...！", SpeechEventEnum.Listening),
                    new EndEvent()
                };
            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildRedEnemyTutorial(
            RedEnemyTutorial redEnemyTutorial
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "赤い敵が来たね！", SpeechEventEnum.Listening),
                    new SequentialEvent(1, "赤色のボタンを押そう！", SpeechEventEnum.Practical, () => redEnemyTutorial.IsSuccess),
                    new SequentialEvent(2, "そうそう！上手！", SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildSequentialEnemyTutorial(
            SequentialEnemyTutorial sequentialEnemyTutorial
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "今度はたくさんの敵が来たね！", SpeechEventEnum.Listening),
                    new SequentialEvent(1, "対応するボタンを押そう！", SpeechEventEnum.Practical,
                        () => sequentialEnemyTutorial.IsSuccess),
                    new SequentialEvent(2, "君、配信の才能あるよ！", SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildShowWitheCommentsTutorial(
            ShowWhiteCommentsTutorial showWhiteCommentsTutorial
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "配信盛り上がっているネ...！", SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildShowAntiCommentsTutorial(
            ShowAntiCommentsTutorial showAntiCommentsTutorial
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "うわ！？アンチだ...！？どうしよ...；；", SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildShowDaipanCutsceneTutorial(
            DaipanCutscene daipanCutscene
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "わっ...", SpeechEventEnum.Practical),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildCatSpeaksAfterDaipan(
            CatSpeaksAfterDaipan catSpeaksAfterDaipan
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "す、スゴイ...！", SpeechEventEnum.Listening),
                    new SequentialEvent(1, "君、配信の才能あるよ！", SpeechEventEnum.Listening),
                    new SequentialEvent(2, "台パン配信者...！い、いける...これは売れるぞ〜〜！！！", SpeechEventEnum.Listening),
                    new SequentialEvent(3, "...ヨシ！そろそろ本番の配信をしようか！", SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            speechEvents[3].SetNextEvent(speechEvents[4]);
            return speechEvents[0];
        }
    }


    public class SpeechEventManager
    {
        ISpeechEvent? _currentEvent;

        public ISpeechEvent CurrentEvent
        {
            get
            {
                if (_currentEvent == null)
                {
                    Debug.LogWarning("CurrentEvent is null");
                    return new EndEvent();
                }

                return _currentEvent;
            }
            private set => _currentEvent = value;
        }

        public void SetSpeechEvent(ISpeechEvent speechEvent)
        {
            CurrentEvent = speechEvent;
        }

        public SpeechEventEnum GetSpeechEventEnum()
        {
            return CurrentEvent.SpeechEventEnum;
        }

        public bool IsEnd()
        {
            return CurrentEvent is EndEvent;
        }

        public bool MoveNext()
        {
            Debug.Log($"CurrentEvent.Message = {CurrentEvent.Message}");
            var (result, nextEvent) = CurrentEvent.MoveNext();
            if (result) CurrentEvent = nextEvent;
            Debug.Log($"NextEvent.Message = {CurrentEvent.Message}");
            return result;
        }
    }
}