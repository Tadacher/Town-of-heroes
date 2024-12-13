﻿using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using static Infrastructure.MetaCitySave;

namespace Infrastructure
{
    /// <summary>
    /// loads and saves meta city state
    /// </summary>
    public class MetaCitySaveLoader
    {
        public MetaCitySave SaveObject { get; set; }

        private const string _path = "/MetaMap.json";
        private const string _initialMapPath = "/MetaMapInit.json";

        private string _json = null;
        public MetaCitySaveLoader()
        {
            if (File.Exists(Application.persistentDataPath + _path))
            {
                _json = File.ReadAllText(Application.persistentDataPath + _path);
                try
                {
                    SaveObject = JsonConvert.DeserializeObject<MetaCitySave>(_json);
                }
                catch(Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
            else if (File.Exists(Application.persistentDataPath + _initialMapPath))
            {
                _json = File.ReadAllText(Application.persistentDataPath + _initialMapPath);
                SaveObject = JsonConvert.DeserializeObject<MetaCitySave>(_json);
            }
            else
                Debug.LogError("NO INITIAL MAP FOUND");
        }
        public void Save(MetaCitySaveEntry[] save)
        {
            if (SaveObject == null)
                SaveObject = new(save);

            else 
                SaveObject.FlatGrid = save;
            
            try
            {
                _json = JsonConvert.SerializeObject(SaveObject);
                File.WriteAllText(Application.persistentDataPath + _path, _json);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
    public class MetaCitySave
    {
        /// <summary>
        /// must be flat array cuz json cannot save multi-dimensional arrays
        /// </summary>
        public MetaCitySaveEntry[] FlatGrid;

        public MetaCitySave(MetaCitySaveEntry[] grid)
        {
            FlatGrid = grid;
        }
        public class MetaCitySaveEntry
        {
            public Type BuildingType;
            public int BuildingLevel;

            public MetaCitySaveEntry(Type buildingType, int buildingLevel)
            {
                BuildingType = buildingType;
                BuildingLevel = buildingLevel;
            }
        }
    }
}