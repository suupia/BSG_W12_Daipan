#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Option.Scripts;
using UnityEngine;

namespace Daipan.Tutorial.Scripts
{
    internal class UICatMessage
    {
        readonly LanguageConfig _languageConfig;
        int CurrentMessageIndex { get; set; } = -1;
        public UICatMessage(LanguageConfig languageConfig)
        {
            _languageConfig = languageConfig;
        }

        public enum TutorialContent
        {
            RedEnemyTutorial,
            SequentialEnemyTutorial,
        }
        
        public string GetNextMessage()
        {
            CurrentMessageIndex++;
            return _languageConfig.CurrentLanguage switch
            {
                LanguageConfig.LanguageEnum.English => string.Empty,
                LanguageConfig.LanguageEnum.Japanese => GetJapaneseMessage(TutorialContent.RedEnemyTutorial, CurrentMessageIndex),
                _ => string.Empty
            };
        }
        
        // string GetEnglishMessage(int index)
        // {
        //     return index switch
        //     {
        //         0 => "Hello, nice to meet you! I am a cat!",
        //         1 => "I will support your stream!",
        //         2 => "First, let me explain the game...!",
        //         3 => "A red enemy has appeared!",
        //         4 => "Press the red button!",
        //         _ => string.Empty
        //     };
        // }
        
        string GetJapaneseMessage(TutorialContent tutorialContent, int index)
        {
            return
                tutorialContent switch
                {
                    TutorialContent.RedEnemyTutorial => index switch
                    {
                        0 => "やぁ、初めまして！僕はネコ！",
                        1 => "君の配信をサポートするよ！",
                        2 => "じゃあ、まずこのゲームの説明...！",
                        3 => "赤い敵が来たね！",
                        4 => "赤色のボタンを押そう！",
                        _ => string.Empty
                    }, 
                    TutorialContent.SequentialEnemyTutorial => index switch
                    {
                        0 => "赤い敵が来たね！",
                        1 => "赤色のボタンを押そう！",
                        
                        _ => string.Empty
                    },
                    _ => string.Empty
                };
                
        }
        
    }
    
    public interface ISpeechEvent
    {
        int Id { get; }
        string Message { get; }
        void Execute();
        (bool, ISpeechEvent) MoveNext();
        void SetNextEvent(params ISpeechEvent[] nextEvents);
    }

    public record SequentialEvent : ISpeechEvent
    {
        public int Id { get; }
        public string Message { get; }
        Action Action { get; }
        ISpeechEvent? NextEvent { get; set; }

        bool IsCompleted { get; set; }
        public SequentialEvent(int id,string message)
        {
            Id = id;
            Message = message;
            Action = () => { };
        }
        
        public SequentialEvent(int id,string message, Action action)
        {
            Id = id;
            Message = message;
            Action = action;
        }

        public void Execute()
        {
           Debug.Log(Message);
           Action();
           IsCompleted = true;
        }

        public (bool, ISpeechEvent) MoveNext()
        {
            if(!IsCompleted) return  (false, this);
            if (NextEvent == null) return (false, this);
            return (true, NextEvent);
        }

        public void SetNextEvent(ISpeechEvent[] nextEvents)
        {
            if(nextEvents.Length != 1) throw new ArgumentException("NextEvent must be one");
            NextEvent = nextEvents[0];
        }
    }

    public  record ConditionalEvent : ISpeechEvent
    {
       public  int Id { get; }
        public string Message { get; }
        Func<bool> Condition { get; }
        ISpeechEvent? TrueEvent { get; set; }
        ISpeechEvent? FalseEvent { get; set; }
        bool IsCompleted { get; set; }
        public ConditionalEvent(int id, string message, Func<bool> condition)
        {
            Id = id;
            Message = message; 
            Condition = condition;
        }

        
        public void Execute()
        {
            Debug.Log(Message);
            IsCompleted = true;
        }
        
        public (bool, ISpeechEvent) MoveNext()
        {
            if(!IsCompleted) return  (false, this);
            if (TrueEvent == null || FalseEvent == null) return (false, this);
            return (true, Condition() ? TrueEvent : FalseEvent);
        }
        
        public void SetNextEvent(ISpeechEvent[] nextEvents)
        {
            if(nextEvents.Length != 2) throw new ArgumentException("NextEvent must be two");
            TrueEvent = nextEvents[0];
            FalseEvent = nextEvents[1];
        }
        
            
    }

    public class SpeechEventBuilder
    {
        readonly EnemySpawnerTutorial _enemySpawnerTutorial;
        
        public SpeechEventBuilder(EnemySpawnerTutorial enemySpawnerTutorial)
        {
            _enemySpawnerTutorial = enemySpawnerTutorial;
        }
        
        public ISpeechEvent Build()
        {
            List<ISpeechEvent> speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, "やぁ、初めまして！僕はネコ！"),
                    new SequentialEvent(1, "君の配信をサポートするよ！"),
                    new SequentialEvent(2, "じゃあ、まずこのゲームの説明...！"),
                    new SequentialEvent(3, "赤い敵が来たね！", () => _enemySpawnerTutorial.SpawnRedEnemy()),
                    new ConditionalEvent(4, "赤色のボタンを押そう！", () => true),
                    new SequentialEvent(5, "そうそう！上手！"),
                    new SequentialEvent(6, "それは違うボタンだよ！もう一回！"),
                };
            
            
            speechEvents.ElementAt(0).SetNextEvent(speechEvents.ElementAt(1));
            speechEvents.ElementAt(1).SetNextEvent(speechEvents.ElementAt(2));
            speechEvents.ElementAt(2).SetNextEvent(speechEvents.ElementAt(3));
            speechEvents.ElementAt(3).SetNextEvent(speechEvents.ElementAt(4));
            speechEvents.ElementAt(4).SetNextEvent(speechEvents.ElementAt(5), speechEvents.ElementAt(6));
           
            return speechEvents.ElementAt(0); 
        }
    }
    

    public class SpeechEventManager
    {
        ISpeechEvent CurrentEvent { get; set; } 
        readonly SpeechEventBuilder _speechEventBuilder;
        
        public SpeechEventManager(SpeechEventBuilder speechEventBuilder)
        {
            _speechEventBuilder = speechEventBuilder;
            CurrentEvent = _speechEventBuilder.Build();
        }
        
        public (bool IsMoveNext,ISpeechEvent CurrentEvent) Execute()
        {
            var currentEvent = CurrentEvent;
            currentEvent.Execute();
            var (result, nextEvent) = currentEvent.MoveNext(); 
            if(result) CurrentEvent = nextEvent;
            return ( result, currentEvent);
        }
    }
}

