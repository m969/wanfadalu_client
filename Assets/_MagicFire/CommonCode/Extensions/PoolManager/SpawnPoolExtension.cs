using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using uFrame.MVVM.ViewModels;
using uFrame.MVVM.Views;
using uFrame.MVVM.Events;
using MagicFire.HuanHuoUFrame;

public static class SpawnPoolExtensions
{
    public static T SpawnView<T>(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel) where T: ViewBase
    {
        var result = spawnPool.Spawn(viewPrefab, viewModel.position, Quaternion.identity);
        var resultView = result.GetComponent<T>();
        if (viewModel != null)
        {
            resultView.Identifier = viewModel.Identifier;
            resultView.ViewModelObject = viewModel;
            if (!string.IsNullOrEmpty(viewModel.Identifier))
            {
                resultView.Identifier = viewModel.Identifier;
            }
        }
        return resultView;
    }

    public static T SpawnViewObject<T>(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel) where T : ViewBase
    {
        var resultView = SpawnView<T>(spawnPool, viewPrefab, viewModel);
        resultView.transform.eulerAngles = new Vector3(viewModel.direction.x, viewModel.direction.z, viewModel.direction.y);
        resultView.transform.position = viewModel.position;
        return resultView;
    }

    public static T SpawnEntityCommonView<T>(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel) where T : EntityCommonView
    {
        var entityCommonView = SpawnView<T>(spawnPool, viewPrefab, viewModel) as T;
        if (entityCommonView != null)
            entityCommonView.ParentSpawnPool = spawnPool;
        return entityCommonView;
    }

    public static T SpawnEntityCommonViewObject<T>(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel) where T : EntityCommonView
    {
        var entityCommonView = SpawnViewObject<T>(spawnPool, viewPrefab, viewModel) as T;
        if (entityCommonView != null)
            entityCommonView.ParentSpawnPool = spawnPool;
        return entityCommonView;
    }

    public static void DespawnView(this SpawnPool spawnPool, ViewBase view)
    {
        spawnPool.Despawn(view.transform);
    }

    public static void DespawnEntityCommonView(this SpawnPool spawnPool, EntityCommonView entityCommonView)
    {
        DespawnView(spawnPool, entityCommonView);
    }
}
