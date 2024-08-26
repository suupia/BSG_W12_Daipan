#nullable enable
using System;
using UnityEngine;

namespace Daipan.Tutorial.MonoScripts
{
    [Serializable]
    public class SerializableKeyPair<TKey, TValue>
    {
        [SerializeField] private TKey key;
        [SerializeField] private TValue value;

        public TKey Key => key;
        public TValue Value => value;
    }
    public class TutorialSpeechSpritesMono : MonoBehaviour
    {
        [SerializeField] SerializableKeyPair<string,Sprite>[] tutorialSprites = null!;
    } 
}

