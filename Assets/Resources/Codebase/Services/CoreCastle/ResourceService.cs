using Infrastructure;
using System;
using UnityEngine;

namespace Progress
{
    public class ResourceService
    {
        private ResourceData _data;
        private readonly ResourcesSaveLoader _resourcesSaveLoader;

        public event Action<int> OnWoodGathered;
        public event Action<int> OnFoodGathered;
        public event Action<int> OnStoneGathered;
        public event Action<int> OnScrollsGathered;

        public ResourceService(ResourcesSaveLoader resourcesSaveLoader)
        {
            if(resourcesSaveLoader.ResourceSave == null)
            {
                Debug.LogError("No resource savefile found!");
                return;
            }
            var save = resourcesSaveLoader.ResourceSave;
            _data = new(
                woodPieces: save.WoodPieces, 
                stonePieces: save.StonePieces, 
                foodPieces: save.FoodPieces,
                scrolls: save.Scrolls,
                waves: save.Waves);
            _resourcesSaveLoader = resourcesSaveLoader;
        }

        public void Save() => _resourcesSaveLoader.Save(GetResourceData());

        public void GatherWood(int wood)
        {
            _data.WoodPieces += wood;
            OnWoodGathered?.Invoke(_data.WoodPieces);
        }

        public void GatherStone(int stone)
        {
            _data.StonePieces += stone;
            OnStoneGathered?.Invoke(_data.StonePieces);
        }

        public void GatherFood(int food)
        {
            _data.FoodPieces += food;
            OnFoodGathered?.Invoke(_data.FoodPieces);
        }
        public void GatherScrolls(int scrolls)
        {
            _data.Scrolls += scrolls;
            OnScrollsGathered?.Invoke(_data.Scrolls);
        }

        public void CountWave()
        {
            _data.Waves++;
            Debug.Log(_data.Waves);
        }

        public void ClearWave() => _data.Waves = 0;
        public ResourceData GetResourceData() => _data;

        public void Refresh()
        {
            OnWoodGathered?.Invoke(_data.WoodPieces);
            OnStoneGathered?.Invoke(_data.StonePieces);
            OnFoodGathered?.Invoke(_data.FoodPieces);
        }

        public void SubtractResources(ResourceData cost)
        {
            _data.WoodPieces -= cost.WoodPieces;
            OnFoodGathered?.Invoke(_data.WoodPieces);

            _data.FoodPieces -= cost.FoodPieces;
            OnFoodGathered?.Invoke(_data.FoodPieces);

            _data.StonePieces -= cost.StonePieces;
            OnStoneGathered?.Invoke(_data.StonePieces);
        }        
    }
}