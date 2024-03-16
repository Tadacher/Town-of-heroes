using Infrastructure;
using System;
using UnityEngine;

namespace Progress
{
    public class ResourceService
    {
        ResourceData _data;

        public event Action<int> OnWoodGathered;
        public event Action<int> OnFoodGathered;
        public event Action<int> OnStoneGathered;

        public ResourceService(ResourcesSaveLoader resourcesSaveLoader)
        {
            if(resourcesSaveLoader.ResourceSave == null)
            {
                Debug.LogError("No resource savefile found!");
                return;
            }
            var save = resourcesSaveLoader.ResourceSave;
            _data = new(
                save.WoodPieces, 
                save.StonePieces, 
                save.FoodPieces);
           
        }

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
            _data.StonePieces += food;
            OnFoodGathered?.Invoke(_data.StonePieces);
        }

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