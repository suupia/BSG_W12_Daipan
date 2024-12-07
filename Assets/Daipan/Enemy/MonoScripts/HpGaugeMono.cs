#nullable enable
using UnityEngine;

namespace Daipan.Enemy.MonoScripts
{
    public sealed class HpGaugeMono : MonoBehaviour
    {
        [SerializeField] SpriteRenderer hpGaugeSpriteRenderer = null!;
        Material? _hpGaugeMaterial;
        [SerializeField] Sprite[] HpSprite=null!;
        [SerializeField] SpriteRenderer Hp = null!;

        void Awake()
        {
            _hpGaugeMaterial = hpGaugeSpriteRenderer.material;
        }

        public void SetRatio(float ratio,int HP)
        {
            if (_hpGaugeMaterial == null)
            {
                Debug.LogWarning("_hpGaugeMaterial is null");
                return;
            }
            Debug.Log("hp="+HP);
            Hp.sprite=HpSprite[HP];
            _hpGaugeMaterial.SetFloat("_Ratio", ratio);

        }
    } 
}
