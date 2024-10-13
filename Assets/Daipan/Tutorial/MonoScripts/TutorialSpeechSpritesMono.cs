#nullable enable
using System;
using UnityEngine;

namespace Daipan.Tutorial.MonoScripts
{
    [Serializable]
    public class SerializableKeyPair<TKey, TValue>
    {
        [SerializeField] TKey? key;
        [SerializeField] TValue? value;
        public TKey? Key => key;
        public TValue? Value => value;
    }

    public class TutorialSpeechSpritesMono : MonoBehaviour
    {
        [SerializeField] SerializableKeyPair<string, Sprite>[] tutorialSprites = null!;
        public Sprite GetSprite(string key)
        {
            foreach (var pair in tutorialSprites)
            {
                if (pair.Key == key)
                {
                    return pair.Value!;
                }
            }
            throw new Exception($"TutorialSpeechSpritesMono: key not found: {key}");
        } 
    }
}