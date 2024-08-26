#nullable enable
using System;
using UnityEngine;

namespace Daipan.Tutorial.MonoScripts
{
    [Serializable]
    public class SerializableKeyPair<TKey, TValue>
    {
        [SerializeField] TKey key;
        [SerializeField] TValue value;
        public TKey Key => key;
        public TValue Value => value;
    }

    public class TutorialSpeechSpritesMono : MonoBehaviour
    {
        [SerializeField] SerializableKeyPair<string, Sprite>[] tutorialSprites = null!;
    }
}