using UnityEngine;
using WorldCells;

namespace Services.CardGeneration
{
    [CreateAssetMenu(fileName = "CellStats", menuName = "ScriptableObjects/CellStats", order = 1)]
    public class CellStats : ScriptableObject
    {
        public string Name;
        public string Description;
        public CellBiomeTypes[] CellTags;
    }
}