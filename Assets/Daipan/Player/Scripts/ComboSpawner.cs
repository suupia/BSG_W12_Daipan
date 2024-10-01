#nullable enable
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;

namespace Daipan.Player.Scripts;

public class ComboSpawner
{
    readonly IPrefabLoader<ComboViewMono> _comboViewMonoPrefabLoader; 
    public ComboSpawner(IPrefabLoader<ComboViewMono> comboViewMonoPrefabLoader)
    {
        _comboViewMonoPrefabLoader = comboViewMonoPrefabLoader;
    }
 
    public void SpawnCombo(int comboCount, Vector3 position)
    {
        // todo : コンボを生成する
        Debug.Log($"Combo: {comboCount}を生成");
        var comboPrefab = _comboViewMonoPrefabLoader.Load();
        var comboViewMono = Object.Instantiate(comboPrefab, position, Quaternion.identity);
        comboViewMono.UpdateComboText(comboCount);
    }
}