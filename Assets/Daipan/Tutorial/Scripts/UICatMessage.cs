#nullable enable
using System;
using Daipan.Option.Scripts;

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
}

