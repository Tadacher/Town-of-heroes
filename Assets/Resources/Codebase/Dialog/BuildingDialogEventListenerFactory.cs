using System;
using System.Collections.Generic;

public class BuildingDialogEventListenerFactory 
{
    private readonly IBuildingPlacedEventProvider _buildingPlacedEventProvider;
    private readonly DialogService _dialogService;
    private readonly DialogDatabase _dialogDatabase;
    private readonly HashSet<AbstractDialogEventListener> _listeners = new();
    public BuildingDialogEventListenerFactory(
                                      DialogDatabase dialogDatabase,
                                      IBuildingPlacedEventProvider buildingPlacedEventProvider,
                                      DialogService dialogService)
    {
        _dialogDatabase = dialogDatabase;
        _buildingPlacedEventProvider = buildingPlacedEventProvider;
        _dialogService = dialogService;
        CreateListeners();
    }

    public void CreateListeners()
    {
        foreach (var data in _dialogDatabase._dialogDatas)
        {

            if (data.EventType == DialogData.DialogEventType.Building)
            {
                CreateAndAddPlacedListener(data);
            }
        }
    }

    private void CreateAndAddPlacedListener(DialogData data)
    {
        var listener = new BuildingPlacedDialogEventListener(_buildingPlacedEventProvider, data, _dialogService);
        _listeners.Add(listener);
    }
}

