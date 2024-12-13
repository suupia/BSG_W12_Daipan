#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Stream.MonoScripts
{
    // ただ数字を持つだけのクラス
    public class DigitSpritesMono : MonoBehaviour
    {
        [SerializeField] List<Sprite> numberSprites = new(); // 0~9までの画像 
        [SerializeField] Sprite transparentSprite = null!; // 透明画像

        public Sprite this[int index] => numberSprites[index];
        public Sprite TransparentSprite => transparentSprite;
    }
}