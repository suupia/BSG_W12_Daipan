#nullable enable
using Daipan.LevelDesign.Combo.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Player.Scripts;

public class ComboSpawner
{
    readonly IObjectResolver _container;
    readonly IPrefabLoader<ComboInstantViewMono> _comboViewMonoPrefabLoader;
    readonly ComboParamsManager _comboParamsManager;
    public ComboSpawner(
        IObjectResolver container
        , IPrefabLoader<ComboInstantViewMono> comboViewMonoPrefabLoader
        , ComboParamsManager comboParamsManager)
    {
        _container = container;
        _comboViewMonoPrefabLoader = comboViewMonoPrefabLoader;
        _comboParamsManager = comboParamsManager;
    }
 
    public void SpawnCombo(int comboCount, Vector3 position)
    {
        // todo : コンボを生成する
        Debug.Log($"Combo: {comboCount}を生成");
        var comboPrefab = _comboViewMonoPrefabLoader.Load();
        var comboViewMono = _container.Instantiate(comboPrefab, position, Quaternion.identity, _comboParamsManager.GetComboParent());
        comboViewMono.ShowComboText(comboCount);
    }
}