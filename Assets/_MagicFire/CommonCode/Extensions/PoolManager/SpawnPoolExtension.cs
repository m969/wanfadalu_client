using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;
using uFrame.MVVM.ViewModels;
using uFrame.MVVM.Views;
using uFrame.MVVM.Events;

public static class SpawnPoolExtensions
{
    public static ViewBase SpawnView(this SpawnPool spawnPool, Transform viewPrefab, ViewModel viewModel)
    {
        var result = spawnPool.Spawn(viewPrefab);
        var resultView = result.GetComponent<ViewBase>();

        if (viewModel != null)
        {
            resultView.Identifier = viewModel.Identifier;
            resultView.ViewModelObject = viewModel;
        }
        else if (!string.IsNullOrEmpty(viewModel.Identifier))
        {
            resultView.Identifier = viewModel.Identifier;
        }

        resultView.CreateEventData = new ViewCreatedEvent()
        {
            IsInstantiated = true,
            View = resultView,
            Scene = null,
        };

        //if (scene != null)
        //    resultView.transform.parent = scene.transform;

        return resultView;
    }
}
