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
        void Execute();
        (bool, ISpeechEvent) MoveNext();
        void SetNextEvent(params ISpeechEvent[] nextEvents);
    }

    public abstract record AbstractSpeechEvent : ISpeechEvent, IDisposable
    {
        public int Id { get; protected init; }
        public string Message { get; protected init; }
        public SpeechEventEnum SpeechEventEnum { get; protected init; }
        protected Func<bool> ExecuteAction { get; set; }
        protected bool IsCompleted { get; set; }
        protected readonly IList<IDisposable> Disposables = new List<IDisposable>();
        public void Dispose()
        {
            foreach (var disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
        public abstract void Execute();
        public abstract (bool, ISpeechEvent) MoveNext();
        public abstract void SetNextEvent(params ISpeechEvent[] nextEvents);
    }
    
    public enum SpeechEventEnum
    {
        None,
        Listening, // 聞くタイプのチュートリアル
        Practical, // 実践するタイプのチュートリアル
    }

    public sealed record SequentialEvent : AbstractSpeechEvent
    {
        ISpeechEvent? NextEvent { get; set; }

        public SequentialEvent(
            int id
            ,string message
            ,SpeechEventEnum speechEventEnum
            )
        {
            Id = id;
            Message = message;
            SpeechEventEnum = speechEventEnum;
            ExecuteAction = () => true;
        }
        
        public SequentialEvent(
            int id
            ,string message
            ,SpeechEventEnum speechEventEnum
            ,Func<bool> executeAction
            )
        {
            Id = id;
            Message = message;
            SpeechEventEnum = speechEventEnum;
            ExecuteAction = executeAction;
        }

        public override void Execute()
        {
           Debug.Log($"Id: {Id} Message: {Message}");
           IsCompleted = ExecuteAction();
        }

        public override (bool, ISpeechEvent) MoveNext()
        {
            if(!IsCompleted) return  (false, this);
            if (NextEvent == null) return (false, this);
            return (true, NextEvent);
        }

        public override void SetNextEvent(params ISpeechEvent[] nextEvents)
        {
            if(nextEvents.Length != 1) throw new ArgumentException("NextEvent must be one");
            NextEvent = nextEvents[0];
        }
    }

    public sealed record ConditionalEvent : AbstractSpeechEvent
    {
        Func<bool> Condition { get; }
        ISpeechEvent? TrueEvent { get; set; }
        ISpeechEvent? FalseEvent { get; set; }
        public ConditionalEvent(
            int id
            ,string message
            ,SpeechEventEnum speechEventEnum
            ,Func<bool> executeAction
            ,Func<bool> condition
            )
        {
            Id = id;
            Message = message; 
            SpeechEventEnum = speechEventEnum;
            ExecuteAction = executeAction;
            Condition = condition;
        }

        public override void Execute()
        {
            Debug.Log($"Id: {Id} Message: {Message}");
            IsCompleted = ExecuteAction();
        }
        
        public override (bool, ISpeechEvent) MoveNext()
        {
            if(!IsCompleted) return  (false, this);
            if (TrueEvent == null || FalseEvent == null) return (false, this);
            return (true, Condition() ? TrueEvent : FalseEvent);
        }
        
        public override void SetNextEvent(params ISpeechEvent[] nextEvents)
        {
            if(nextEvents.Length != 2) throw new ArgumentException("NextEvent must be two");
            TrueEvent = nextEvents[0];
            FalseEvent = nextEvents[1];
        }
            
    }
    public sealed record EndEvent : AbstractSpeechEvent
    {
        public override void Execute()
        {
            Debug.Log($"Id: {Id} Message: {Message}");
        }

        public override (bool, ISpeechEvent) MoveNext()
        {
            return (false, this);
        }

        public override void SetNextEvent(params ISpeechEvent[] nextEvents)
        {
            throw new NotImplementedException();
        }
    }

    public static class SpeechEventBuilder
    {
        public static ISpeechEvent BuildUICatIntroduce()
        {
            List<ISpeechEvent> speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "やぁ、初めまして！僕はネコ！",SpeechEventEnum.Listening),
                    new SequentialEvent(1, "君の配信をサポートするよ！",SpeechEventEnum.Listening),
                    new SequentialEvent(2, "じゃあ、まずこのゲームの説明...！", SpeechEventEnum.Listening),
                };
            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(new EndEvent());

            return speechEvents[0]; 
        }
        
        public static ISpeechEvent BuildRedEnemyTutorial(
            RedEnemyTutorial redEnemyTutorial
            , EnemySpawnerTutorial enemySpawnerTutorial
            )
        {
            List<ISpeechEvent> speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "赤い敵が来たね！", SpeechEventEnum.Listening,() =>
                    {
                        enemySpawnerTutorial.SpawnRedEnemy();
                        return true;
                    }),
                    new ConditionalEvent(1, "赤色のボタンを押そう！", SpeechEventEnum.Practical
                        , () =>
                        {
                             return true;
                        },() => redEnemyTutorial.IsSuccess == true),
                    new SequentialEvent(2, "そうそう！上手！", SpeechEventEnum.Listening),
                    new SequentialEvent(3, "それは違うボタンだよ！もう一回！", SpeechEventEnum.Listening
                        , () =>
                        {
                            return true;
                        }
                        ),
                };
            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2], speechEvents[3]);
            speechEvents[2].SetNextEvent(new EndEvent());
            speechEvents[3].SetNextEvent(speechEvents[1]);

            return speechEvents[0]; 
        }
    }
    

    public class SpeechEventManager
    {
        ISpeechEvent? CurrentEvent { get; set; } 

        public void SetSpeechEvent(ISpeechEvent speechEvent)
        {
            CurrentEvent = speechEvent;
        }
        public SpeechEventEnum GetSpeechEventEnum()
        {
            if (CurrentEvent == null)
            {
                Debug.LogWarning("CurrentEvent is null");
                return SpeechEventEnum.None;
            }

            return CurrentEvent.SpeechEventEnum;
        }
        
        public bool IsEnd()
        {
            if (CurrentEvent == null)
            {
                Debug.LogWarning("CurrentEvent is null");
                return false;
            }

            return CurrentEvent is EndEvent;
        }

        public (bool IsMoveNext,ISpeechEvent CurrentEvent) Execute()
        {
            if (CurrentEvent == null)
            {
                Debug.LogWarning("CurrentEvent is null");
                return (false, null!);
            }
            var currentEvent = CurrentEvent;
            currentEvent.Execute();
            var (result, nextEvent) = currentEvent.MoveNext(); 
            if(result) CurrentEvent = nextEvent;
            Debug.Log($"currentEvent: {currentEvent.Message} nextEvent: {nextEvent.Message}");
            return ( result, currentEvent);
        }
    }
}

