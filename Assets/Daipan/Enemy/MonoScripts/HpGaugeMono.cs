#nullable enable
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class HpGaugeMono : MonoBehaviour
    {
        [SerializeField] SpriteRenderer hpGaugeSpriteRenderer = null!;
        Material? _hpGaugeMaterial;
        [SerializeField] Sprite[] HpSprite = null!;
        [SerializeField] SpriteRenderer Hp = null!;

        void Awake()
        {
            _hpGaugeMaterial = hpGaugeSpriteRenderer.material;
        }

        public void SetRatio(float ratio, int hp)
        {
            if (_hpGaugeMaterial == null)
            {
                Debug.LogWarning("_hpGaugeMaterial is null");
                return;
            }

            Debug.Log("hp=" + hp);
            if (0 <= hp && hp < HpSprite.Length)
            {
                Hp.sprite = HpSprite[hp];
            }
            else
            {
                Debug.LogWarning("hp is out of range");
            }
            _hpGaugeMaterial.SetFloat("_Ratio", ratio);
        }
    }
}