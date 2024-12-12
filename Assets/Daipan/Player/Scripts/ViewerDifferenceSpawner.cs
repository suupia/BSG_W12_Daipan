#nullable enable
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Player.Scripts;

public class ViewerDifferenceSpawner
{
    readonly IObjectResolver _container;
    readonly IPrefabLoader<ViewerDifferenceViewMono> _viewerViewMonoPrefabLoader;
    public ViewerDifferenceSpawner(
        IObjectResolver container
        , IPrefabLoader<ViewerDifferenceViewMono> viewerViewMonoPrefabLoader
    )
    {
        _container = container;
        _viewerViewMonoPrefabLoader = viewerViewMonoPrefabLoader;
    }

    public void SpawnViewer(int diff, Vector3 position)
    {
        Debug.Log($"Spawning ViewerDifference with {diff}");
        var viewerPrefab = _viewerViewMonoPrefabLoader.Load();
        var viewerViewMono = _container.Instantiate(viewerPrefab, position, Quaternion.identity);
        viewerViewMono.ShowViewerDifferenceText(diff);
    }
}