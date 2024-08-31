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
        Practical, // 実践するタイプのチュートリアル
        NoInput, // 入力がないタイプのチュートリアル
    }

    public sealed record Speech
    {
        public string Message { get; }
        public string SpriteKey { get; }

        public Speech(string message) : this(message, string.Empty)
        {
        }

        public Speech(string message, string spriteKey)
        {
            Message = message;
            SpriteKey = spriteKey;
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
                    new("Hi, I'm a cat!"),
                    new("I'll support your stream!"),
                    new("Let's start the game explanation...!")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("やぁ、初めまして！僕はネコ！"),
                    new("君の配信をサポートするよ！"),
                    new("じゃあ、まずこのゲームの説明...!")
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
                    new("A red enemy is coming!"),
                    new("Press the red button!"),
                    new("That's right! Good job!")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("青い敵が来たね！"),
                    new("青色のボタンを押そう！", "speech_blue"),
                    new("そうそう！上手！")
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
                    new("A lot of enemies are coming!"),
                    new("Press the corresponding button!"),
                    new("You have a talent for streaming!")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("今度はたくさんの敵が来たね！"),
                    new("対応するボタンを押そう！", "speech_all"),
                    new("いいかんじ！")
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
                    new("A totem enemy is coming!"),
                    new("Press the corresponding button!"),
                    new("You have a talent for streaming!")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("わ！トーテムポールだ！"),
                    new("対応するボタンを同時押しだ！", "speech_red_blue"),
                    new("最高！君、配信の才能あるよ！")
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
                    new("The stream is getting exciting...!")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("配信盛り上がっているネ...！")
                },
                _ => new List<Speech>()
            };
        }
        
        public static List<Speech> ShowForcedMissTutorial(LanguageEnum language)
        {
            return language switch
            {
                LanguageEnum.English => new List<Speech>
                {
                    new(""),
                    new("Oh no! You missed! If you miss, anti-comments will appear!")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new(""),
                    new("ありゃ、ミスしちゃった。\nミスするとアンチコメントが流れちゃうよ！")
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
                    new("Oh no! It's an anti...! What should I do... ; ;")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("うわ！？アンチだ...！？どうしよ...；；")
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
                    new("Wow...")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("わっ...")
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
                    new("A, Amazing...!"),
                    new("You have a talent for streaming!"),
                    new("Daipan streamer...! You can do it... This will sell~~~!!!"),
                    new("...Alright! Let's start the actual stream soon!")
                },
                LanguageEnum.Japanese => new List<Speech>
                {
                    new("す、スゴイ...！"),
                    new("君、配信の才能あるよ！"),
                    new("台パン配信者...！い、いける...\nこれは売れるぞ〜〜！！！"),
                    new("...ヨシ！\nそろそろ本番の配信をしようか！")
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
                    new SequentialEvent(0, SpeechContentByLanguage.TotemEnemyTutorial(language)[0],
                        SpeechEventEnum.Listening),
                    new SequentialEvent(1, SpeechContentByLanguage.TotemEnemyTutorial(language)[1],
                        SpeechEventEnum.Practical, () => totemEnemyTutorial.IsSuccess),
                    new SequentialEvent(2, SpeechContentByLanguage.TotemEnemyTutorial(language)[2],
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

        public static ISpeechEvent BuildForcedMissTutorial(
            ForcedMissTutorial forcedMissTutorial
            , LanguageEnum language
        )
        {
            var speechEvents =
                new List<ISpeechEvent>
                {
                    new SequentialEvent(0, SpeechContentByLanguage.ShowForcedMissTutorial(language)[0],
                        SpeechEventEnum.NoInput),
                    new SequentialEvent(1, SpeechContentByLanguage.ShowForcedMissTutorial(language)[1],
                        SpeechEventEnum.NoInput, () => forcedMissTutorial.IsMissed), // ForcedMissTutorialの中でMoveNext()を呼ぶ
                    new EndEvent()
                };
            speechEvents[0].SetNextEvent(speechEvents[1]);
            speechEvents[1].SetNextEvent(speechEvents[2]);
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