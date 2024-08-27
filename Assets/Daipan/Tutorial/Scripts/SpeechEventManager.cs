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
        Speech Speech { get; }
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

    public sealed record Speech(string Message, string SpriteKey)
    {
        public static implicit operator Speech(string message)
        {
            return new Speech(message, string.Empty);
        }
    }

    public sealed record SequentialEvent : ISpeechEvent
    {
        public int Id { get; } = -1;
        public Speech Speech { get; }
        public SpeechEventEnum SpeechEventEnum { get; }
        ISpeechEvent? NextEvent { get; set; }
        Func<bool> CanMove { get; } = () => true;

        public SequentialEvent(int id, Speech speech, SpeechEventEnum speechEventEnum, Func<bool> canMove)
        {
            Id = id;
            Speech = speech;
            SpeechEventEnum = speechEventEnum;
            CanMove = canMove;
        }

        public SequentialEvent(int id, Speech speech, SpeechEventEnum speechEventEnum)
            : this(id, speech, speechEventEnum, () => true)
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
        public Speech Speech { get; } = new(string.Empty, string.Empty); 
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

    public static class SpeechContentByLanguage
    {
        public static List<Speech> UICatIntroduce(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "Hi, I'm a cat!",
                    "I'll support your stream!",
                    "Let's start the game explanation...!"
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "やぁ、初めまして！僕はネコ！",
                    "君の配信をサポートするよ！",
                    "じゃあ、まずこのゲームの説明...!"
                },
                _ => new List<Speech>()
            };
        }

        public static List<Speech> RedEnemyTutorial(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "A red enemy is coming!",
                    "Press the red button!",
                    "That's right! Good job!"
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "青い敵が来たね！",
                    new("青色のボタンを押そう！", "speech_blue"),
                    "そうそう！上手！"
                },
                _ => new List<Speech>()
            };
        }

        public static List<Speech> SequentialEnemyTutorial(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "A lot of enemies are coming!",
                    "Press the corresponding button!",
                    "You have a talent for streaming!"
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "今度はたくさんの敵が来たね！",
                    "対応するボタンを押そう！",
                    "いいかんじ！"
                },
                _ => new List<Speech>()
            };
        }

        public static List<Speech> TotemEnemyTutorial(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "A totem enemy is coming!",
                    "Press the corresponding button!",
                    "You have a talent for streaming!"
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "わ！トーテムポールだ！",
                    "対応するボタンを同時押しだ！",
                    "最高！君、配信の才能あるよ！"
                },
                _ => new List<Speech>()
            };
        }

        public static List<Speech> ShowWhiteCommentsTutorial(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "The stream is getting exciting...!"
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "配信盛り上がっているネ...！"
                },
                _ => new List<Speech>()
            };
        }

        public static List<Speech> ShowAntiCommentsTutorial(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "Oh no! It's an anti...! What should I do... ; ;"
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "うわ！？アンチだ...！？どうしよ...；；"
                },
                _ => new List<Speech>()
            };
        }

        public static List<Speech> DaipanCutscene(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "Wow..."
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "わっ..."
                },
                _ => new List<Speech>()
            };
        }

        public static List<Speech> CatSpeaksAfterDaipan(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    "A, Amazing...!",
                    "You have a talent for streaming!",
                    "Daipan streamer...! You can do it... This will sell~~~!!!",
                    "...Alright! Let's start the actual stream soon!"
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    "す、スゴイ...！",
                    "君、配信の才能あるよ！",
                    "台パン配信者...！い、いける...これは売れるぞ〜〜！！！",
                    "...ヨシ！そろそろ本番の配信をしようか！"
                },
                _ => new List<Speech>()
            };
        }
    }

    public static class SpeechEventBuilder
    {
        public static ISpeechEvent BuildUICatIntroduce(LanguageEnum language)
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.UICatIntroduce(language)[0],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(1, SpeechContentByLanguage.UICatIntroduce(language)[1],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(2, SpeechContentByLanguage.UICatIntroduce(language)[2],
                        SpeechEventEnum.Listening),
                    new EndEvent()
                };
            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildBlueEnemyTutorial(
            BlueEnemyTutorial blueEnemyTutorial
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.RedEnemyTutorial(language)[0],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(1, SpeechContentByLanguage.RedEnemyTutorial(language)[1],
                        SpeechEventEnum.Practical, () => blueEnemyTutorial.IsSuccess),
                    new SequentialEvent(2, SpeechContentByLanguage.RedEnemyTutorial(language)[2],
                        SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildSequentialEnemyTutorial(
            SequentialEnemyTutorial sequentialEnemyTutorial
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.SequentialEnemyTutorial(language)[0],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(1, SpeechContentByLanguage.SequentialEnemyTutorial(language)[1],
                        SpeechEventEnum.Practical, () => sequentialEnemyTutorial.IsSuccess),
                    new SequentialEvent(2, SpeechContentByLanguage.SequentialEnemyTutorial(language)[2],
                        SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildTotemEnemyTutorial(
            TotemEnemyTutorial totemEnemyTutorial
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.SequentialEnemyTutorial(language)[0],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(1, SpeechContentByLanguage.SequentialEnemyTutorial(language)[1],
                        SpeechEventEnum.Practical, () => totemEnemyTutorial.IsSuccess),
                    new SequentialEvent(2, SpeechContentByLanguage.SequentialEnemyTutorial(language)[2],
                        SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
            speechEvents[2].SetNextEvent(speechEvents[3]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildShowWitheCommentsTutorial(
            ShowWhiteCommentsTutorial showWhiteCommentsTutorial
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.ShowWhiteCommentsTutorial(language)[0],
                        SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildShowAntiCommentsTutorial(
            ShowAntiCommentsTutorial showAntiCommentsTutorial
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.ShowAntiCommentsTutorial(language)[0],
                        SpeechEventEnum.Listening),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildShowDaipanCutsceneTutorial(
            DaipanCutscene daipanCutscene
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.DaipanCutscene(language)[0],
                        SpeechEventEnum.Practical, () => daipanCutscene.IsDaipaned),
                    new EndEvent()
                };

            speechEvents[0].SetNextEvent(speechEvents[1]);
            return speechEvents[0];
        }

        public static ISpeechEvent BuildCatSpeaksAfterDaipan(
            CatSpeaksAfterDaipan catSpeaksAfterDaipan
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.CatSpeaksAfterDaipan(language)[0],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(1, SpeechContentByLanguage.CatSpeaksAfterDaipan(language)[1],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(2, SpeechContentByLanguage.CatSpeaksAfterDaipan(language)[2],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(3, SpeechContentByLanguage.CatSpeaksAfterDaipan(language)[3],
                        SpeechEventEnum.Listening),
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
            Debug.Log($"CurrentEvent.Message = {CurrentEvent.Speech}");
            var (result, nextEvent) = CurrentEvent.MoveNext();
            if (result) CurrentEvent = nextEvent;
            Debug.Log($"NextEvent.Message = {CurrentEvent.Speech}");
            return result;
        }
    }
}