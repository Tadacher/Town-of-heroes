﻿using System;
using UnityEngine;

public class CardDescriptionService
{
    private const string _soPath_Tower = "StatsAndSettings/TowerDescriptions/";
    private const string _soPath_WorldCell = "StatsAndSettings/WorldCellDescriptions/";
    /// <summary>
    /// loads tower describition from Assets
    /// </summary>
    /// <param name="towerType">use Type.Name </param>
    public TowerCardInfoConfig LoadCardDescription(Type towerType)
    {
        return Resources.Load<TowerCardInfoConfig>(_soPath_Tower + towerType.Name);
    }

    internal WorldCellCardInfoConfig LoadWorldCellCardDescription(Type worldCellType)
    {
        return Resources.Load<WorldCellCardInfoConfig>(_soPath_WorldCell + worldCellType.Name);
    }
}