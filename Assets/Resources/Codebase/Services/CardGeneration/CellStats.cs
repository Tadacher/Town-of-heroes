using UnityEngine;
using WorldCells;

namespace Services.CardGeneration
{
    [CreateAssetMenu(fileName = "CellStats", menuName = "ScriptableObjects/CellStats", order = 1)]
    public class CellStats : ScriptableObject
    {
        public CellBiomeTypes[] CellTags;
    }
}