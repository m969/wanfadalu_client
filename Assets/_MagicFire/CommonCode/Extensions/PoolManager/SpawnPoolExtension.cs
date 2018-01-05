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
    public static ViewBase SpawnView(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel)
    {
        var result = spawnPool.Spawn(viewPrefab, viewModel.position, Quaternion.identity);
        var resultView = result.GetComponent<ViewBase>();
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

    public static ViewBase SpawnViewObject(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel)
    {
        var resultView = SpawnView(spawnPool, viewPrefab, viewModel);
        resultView.transform.eulerAngles = new Vector3(viewModel.direction.x, viewModel.direction.z, viewModel.direction.y);
        resultView.transform.position = viewModel.position;
        return resultView;
    }

    public static EntityCommonView SpawnEntityCommonView(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel)
    {
        var entityCommonView = SpawnView(spawnPool, viewPrefab, viewModel) as EntityCommonView;
        if (entityCommonView != null)
            entityCommonView.ParentSpawnPool = spawnPool;
        return entityCommonView;
    }

    public static EntityCommonView SpawnEntityCommonViewObject(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel)
    {
        var entityCommonView = SpawnViewObject(spawnPool, viewPrefab, viewModel) as EntityCommonView;
        if (entityCommonView != null)
            entityCommonView.ParentSpawnPool = spawnPool;
        return entityCommonView;
    }

    public static void DespawnView(this SpawnPool spawnPool, ViewBase view)
    {
        if (view.ViewModelObject != null)
            view.ViewModelObject = null;
        if (view.gameObject.activeInHierarchy == true)
            spawnPool.Despawn(view.transform);
    }

    public static void DespawnEntityCommonView(this SpawnPool spawnPool, EntityCommonView entityCommonView)
    {
        DespawnView(spawnPool, entityCommonView);
    }
}
