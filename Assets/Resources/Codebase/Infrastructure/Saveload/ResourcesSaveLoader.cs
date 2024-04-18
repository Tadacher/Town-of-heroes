using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Progress;

namespace Infrastructure
{
    public class ResourcesSaveLoader
    {
        public ResourceData ResourceSave;
        private const string _path = "/Resources.json";
        private string _json = null;

        public ResourcesSaveLoader()
        {
            if (File.Exists(Application.persistentDataPath + _path))
            {
                _json = File.ReadAllText(Application.persistentDataPath + _path);
                ResourceSave = JsonConvert.DeserializeObject<ResourceData>(_json);
            }
            else
                Debug.LogError($"No resources savefile found at {Application.persistentDataPath + _path}");
        }
        public void Save(ResourceData save)
        {
            ResourceSave = save;

            try
            {
                _json = JsonConvert.SerializeObject(ResourceSave);
                File.WriteAllText(Application.persistentDataPath + _path, _json);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}