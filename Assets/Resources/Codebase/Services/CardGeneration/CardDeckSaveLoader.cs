using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Services.CardGeneration
{
    public class CardDeckSaveLoader
    {
        public List<Type> CardDeckSave { get; private set; }

        private const string _path = "/CardDeck.json";
        private string _json = null;
        public CardDeckSaveLoader()
        {
            if (File.Exists(Application.persistentDataPath + _path))
                LoadSave();
            else
            {
                CardDeckSave = new List<Type>();
                Debug.LogError($"No savefile found at {Application.persistentDataPath + _path}");
            }
        }


        public void Save()
        {
            try
            {
                _json = JsonConvert.SerializeObject(CardDeckSave);
                File.WriteAllText(Application.persistentDataPath + _path, _json);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        private void LoadSave()
        {
            _json = File.ReadAllText(Application.persistentDataPath + _path);
            CardDeckSave = JsonConvert.DeserializeObject<List<Type>>(_json);
        }
    }
}