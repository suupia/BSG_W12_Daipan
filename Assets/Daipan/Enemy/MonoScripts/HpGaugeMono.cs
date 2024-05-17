#nullable enable
using UnityEngine;

public sealed class HpGaugeMono : MonoBehaviour
{
    [SerializeField] SpriteRenderer hpGaugeSpriteRenderer = null!;
    Material? _hpGaugeMaterial;

    void Awake()
    {
        _hpGaugeMaterial = hpGaugeSpriteRenderer.material;
    }

    public void SetRatio(float ratio)
    {
        if (_hpGaugeMaterial == null)
        {
            Debug.LogWarning("_hpGaugeMaterial is null");
            return;
        }

        _hpGaugeMaterial.SetFloat("_Ratio", ratio);
    }
}